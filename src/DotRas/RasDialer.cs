//--------------------------------------------------------------------------
// <copyright file="RasDialer.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;
    using Design;
    using Diagnostics;
    using Internal;
    using Properties;
    using Timer = System.Threading.Timer;

    /// <summary>
    /// Provides an interface to the remote access service (RAS) dialer. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Failure to dispose of this component may result in a connection attempt hanging, which will require the machine to be restarted before the connection is released.
    /// </para>
    /// <para>
    /// When using <b>RasDialer</b> to dial connections asynchronously, ensure the SynchronizingObject property is set if thread synchronization is required. If this is not done, you may get cross-thread exceptions thrown from the component. This is typically needed with applications that have an interface; for example, Windows Forms or Windows Presentation Foundation (WPF).
    /// </para>
    /// <para>
    /// If an exception occurs while processing an event while dialing asynchronously, that exception will be raised through the Error event on <b>RasDialer</b>. This approach allows the application to continue processing the notifications from Windows, which would otherwise cause the state machine to be corrupted and require the machine to be rebooted.
    /// </para>
    /// <para>
    /// The events raised by this component occur only during a dialing operation, if you need to monitor connection changes made outside of dialing a connection use the <see cref="DotRas.RasConnectionWatcher"/> component.
    /// </para>
    /// <para>
    /// <b>Known Limitations</b>
    /// <list type="bullet">
    /// <item>Due to limitations with Windows, <b>RasDialer</b> cannot handle expired passwords or have the ability to change a password while connecting.</item>
    /// <item>This component can only handle dialing one connection at a time. If your application needs to dial multiple connections simultaneously, use multiple instances of the component.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how to use a <b>RasDialer</b> component to synchronously dial an existing entry.
    /// <code lang="C#">
    /// using (RasDialer dialer = new RasDialer())
    /// {
    ///    dialer.EntryName = "My Connection";
    ///    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
    ///    dialer.Credentials = new NetworkCredential("Test", "User");
    ///    dialer.Dial();
    /// }
    /// </code>
    /// <code lang="VB.NET">
    /// Dim dialer As RasDialer
    /// Try
    ///    dialer = New RasDialer
    ///    dialer.EntryName = "My Connection"
    ///    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)
    ///    dialer.Credentials = new NetworkCredential("Test", "User")
    ///    dialer.Dial()
    /// Finally
    ///    If (dialer IsNot Nothing) Then
    ///        dialer.Dispose()
    ///    End If
    /// End Try
    /// </code>
    /// </example>
    public sealed partial class RasDialer : RasComponent
    {
        #region Fields

        private readonly object syncRoot = new object();

        private bool _isBusy;
        private RasDialOptions _options;
        private RasEapOptions _eapOptions;

        private RasHandle handle;        
        private AsyncOperation asyncOp;
        private SendOrPostCallback dialCompletedCallback;
        private NativeMethods.RasDialFunc2 rasDialCallback;
        private TimerCallback timeoutCallback;
        private Timer timer;
        private IntPtr eapDataAddress;
        private byte[] eapUserData;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialer"/> class.
        /// </summary>
        public RasDialer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialer"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain the component.</param>
        public RasDialer(IContainer container)
            : base(container)
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the connection state changes.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RDStateChangedDesc")]
        public event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Occurs when the asynchronous dial operation has completed.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RDDialCompletedDesc")]
        public event EventHandler<DialCompletedEventArgs> DialCompleted;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the component is busy dialing a connection.
        /// </summary>
        [Browsable(false)]
        public bool IsBusy
        {
            get
            {
                bool retval;

                lock (syncRoot)
                {
                    retval = _isBusy;
                }

                return retval;
            }

            private set => _isBusy = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the saved credentials will be updated upon completion of a successful connection attempt.
        /// </summary>
        /// <remarks>
        /// If the entry is stored in a custom location or the current users profile, the credentials cannot be stored in the all users profile. However, if an entry is stored in the all users profile then the credentials may be stored in the current users profile or the all users profile.
        /// </remarks>
        [DefaultValue(typeof(RasUpdateCredential), "None")]
        [SRCategory("CatBehavior")]
        [SRDescription("RDAutoUpdateCredentialsDesc")]
        public RasUpdateCredential AutoUpdateCredentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the polling interval, in milliseconds, to determine whether the connection has successfully disconnected during an asynchronous connection attempt.
        /// </summary>
        [DefaultValue(NativeMethods.HangUpPollingInterval)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDHangUpPollingIntervalDesc")]
        public int HangUpPollingInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        [Browsable(false)]
        public NetworkCredential Credentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the full path (including filename) of a phone book.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("RDPhoneBookPathDesc")]
        public string PhoneBookPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the entry to dial.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDEntryNameDesc")]
        public string EntryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number to dial.
        /// </summary>
        /// <remarks>This value is not required when an entry name has been provided. Additionally, it will override any existing phone number if an entry name has been provided.</remarks>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDPhoneNumberDesc")]
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the callback number.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("RDCallbackNumberDesc")]
        public string CallbackNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the application defined callback id.
        /// </summary>
        [Browsable(false)]
        public IntPtr CallbackId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the one-based index of the subentry to dial.
        /// </summary>
        [DefaultValue(0)]
        [SRCategory("CatData")]
        [SRDescription("RDSubEntryIdDesc")]
        public int SubEntryId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RDEntryOptionsDesc")]
        public RasDialOptions Options
        {
            get => _options ?? (_options = new RasDialOptions());
            set => _options = value;
        }        

        /// <summary>
        /// Gets or sets the extensible authentication protocol (EAP) options.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RDEapOptionsDesc")]
        public RasEapOptions EapOptions
        {
            get => _eapOptions ?? (_eapOptions = new RasEapOptions());
            set => _eapOptions = value;
        }

#if !NO_UI
        /// <summary>
        /// Gets or sets the parent window.
        /// </summary>
        /// <remarks>This object is used for dialog box creation and centering when a security DLL has been defined.</remarks>
        [DefaultValue(null)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDOwnerDesc")]
        public System.Windows.Forms.IWin32Window Owner
        {
            get;
            set;
        }
#endif

        /// <summary>
        /// Gets or sets the length of time (in milliseconds) until the asynchronous connection attempt times out.
        /// </summary>
        [DefaultValue(System.Threading.Timeout.Infinite)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDTimeoutDesc")]
        public int Timeout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether stored credentials will be allowed if the credentials have not been provided.
        /// </summary>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDAllowUseStoredCredentialsDesc")]
        public bool AllowUseStoredCredentials
        {
            get;
            set;
        }

#if (WIN7 || WIN8)

        /// <summary>
        /// Gets or sets the interface index on top of which the Virtual Private Network (VPN) connection will be dialed.
        /// </summary>
        /// <remarks><b>Windows 7 and later:</b> This property is available.</remarks>
        [DefaultValue(0)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDInterfaceIndexDesc")]
        public int InterfaceIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether point-to-point (PPP) authentication is skipped.
        /// </summary>
        /// <remarks><b>Windows 7 and later:</b> This property is available.</remarks>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDSkipPppAuthDesc")]
        public bool SkipPppAuthentication
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a pointer to the authentication cookie.
        /// </summary>
        /// <remarks><b>Windows 7 and later:</b> This property is available.</remarks>
        [Browsable(false)]
        public IntPtr AuthenticationCookie
        {
            get;
            set;
        }

#endif

#endregion

#region Methods

        /// <summary>
        /// Dials the connection.
        /// </summary>
        /// <returns>The handle of the connection.</returns>
        /// <exception cref="System.InvalidOperationException">A phone number or an entry name with phone book path is required to dial.</exception>
        public RasHandle Dial()
        {
            return InternalDial(false);
        }

        /// <summary>
        /// Dials the connection asynchronously.
        /// </summary>
        /// <returns>The handle of the connection.</returns>
        /// <exception cref="System.InvalidOperationException">A phone number or an entry name with phone book path is required to dial.</exception>
        public RasHandle DialAsync()
        {
            return InternalDial(true);
        }

        /// <summary>
        /// Cancels the asynchronous dial operation.
        /// </summary>
        public void DialAsyncCancel()
        {
            lock (syncRoot)
            {
                if (IsBusy)
                {
                    Abort();
                    PostCompleted(null, true, false, false);
                }
            }
        }

        /// <summary>
        /// Sets the EAP data used by the connection attempt.
        /// </summary>
        /// <param name="data">A byte array containing EAP data.</param>
        public void SetEapUserData(byte[] data)
        {
            eapUserData = data;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DotRas.RasDialer"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (IsBusy)
                {
                    // The component is currently dialing a connection, abort the connection attempt.
                    Abort();

                    handle = null;
                    asyncOp = null;
                }

                if (timer != null)
                {
                    timer.Dispose();
                    timer = null;
                }

                ReleaseEapIdentity();
                Credentials = null;

                dialCompletedCallback = null;
                rasDialCallback = null;
                timeoutCallback = null;
            }          

            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected override void InitializeComponent()
        {
            Timeout = System.Threading.Timeout.Infinite;
            HangUpPollingInterval = NativeMethods.HangUpPollingInterval;

            dialCompletedCallback = DialCompletedCallback;
            timeoutCallback = TimeoutCallback;
            rasDialCallback = RasDialCallback;

            base.InitializeComponent();
        }

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RDEOPT"/> flags for the component.
        /// </summary>
        /// <returns>The <see cref="NativeMethods.RDEOPT"/> flags.</returns>
        private NativeMethods.RDEOPT GetRasDialOptions()
        {
            var value = NativeMethods.RDEOPT.None;

            if (Options != null)
            {
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.UsePrefixSuffix, NativeMethods.RDEOPT.UsePrefixSuffix);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.PausedStates, NativeMethods.RDEOPT.PausedStates);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(!string.IsNullOrEmpty(EntryName) && Options.SetModemSpeaker, NativeMethods.RDEOPT.IgnoreModemSpeaker);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.SetModemSpeaker, NativeMethods.RDEOPT.SetModemSpeaker);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(!string.IsNullOrEmpty(EntryName) && Options.SetSoftwareCompression, NativeMethods.RDEOPT.IgnoreSoftwareCompression);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.SetSoftwareCompression, NativeMethods.RDEOPT.SetSoftwareCompression);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.DisableConnectedUI, NativeMethods.RDEOPT.DisableConnectedUI);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.DisableReconnectUI, NativeMethods.RDEOPT.DisableReconnectUI);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.NoUser, NativeMethods.RDEOPT.NoUser);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.Router, NativeMethods.RDEOPT.Router);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.CustomDial, NativeMethods.RDEOPT.CustomDial);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(Options.UseCustomScripting, NativeMethods.RDEOPT.UseCustomScripting);

#endif
            }

            return value;
        }

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RASEAPF"/> flags for the component.
        /// </summary>
        /// <returns>The <see cref="NativeMethods.RASEAPF"/> flags.</returns>
        private NativeMethods.RASEAPF GetRasEapOptions()
        {
            var value = NativeMethods.RASEAPF.None;

            if (EapOptions != null)
            {
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(EapOptions.NonInteractive, NativeMethods.RASEAPF.NonInteractive);
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(EapOptions.LogOn, NativeMethods.RASEAPF.LogOn);
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(EapOptions.Preview, NativeMethods.RASEAPF.Preview);
            }

            return value;
        }

        /// <summary>
        /// Performs the dialing operation.
        /// </summary>
        /// <param name="asynchronous"><b>true</b> if the dialing operation should be asynchronous, otherwise <b>false</b>.</param>
        /// <returns>The handle of the connection.</returns>
        /// <exception cref="System.InvalidOperationException">A phone number or an entry name with phone book path is required to dial.</exception>
        private RasHandle InternalDial(bool asynchronous)
        {
            if (string.IsNullOrEmpty(PhoneNumber) && (string.IsNullOrEmpty(EntryName) || string.IsNullOrEmpty(PhoneBookPath)))
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_PhoneNumberOrEntryNameRequired);
            }

            lock (syncRoot)
            {
                // NOTE: The synchronization object MUST be locked prior to testing of the component is already busy.
                // WARNING! Ensure no exceptions are thrown because existing dial attempts are already in progress. Doing so leaves the
                // connection open and cannot be closed if the application is terminated.
                if (!IsBusy)
                {                    
                    IsBusy = true;

                    try
                    {
                        var parameters = BuildDialParams();
                        var extensions = BuildDialExtensions();

                        if (!string.IsNullOrEmpty(EntryName))
                        {
                            byte[] data = null;

                            data = eapUserData ?? RasHelper.Instance.GetEapUserData(IntPtr.Zero, PhoneBookPath, EntryName);

                            if (data != null)
                            {
                                eapDataAddress = Marshal.AllocHGlobal(data.Length);
                                Marshal.Copy(data, 0, eapDataAddress, data.Length);

                                extensions.eapInfo.eapData = eapDataAddress;
                                extensions.eapInfo.sizeOfEapData = data.Length;
                            }
                        }

                        NativeMethods.RasDialFunc2 callback = null;
                        if (asynchronous)
                        {
                            callback = rasDialCallback;

                            asyncOp = AsyncOperationManager.CreateOperation(null);

                            if (timer != null)
                            {
                                // Dispose of any existing timer if the component is being reused.
                                timer.Dispose();
                                timer = null;
                            }

                            if (Timeout != System.Threading.Timeout.Infinite)
                            {
                                // A timeout has been requested, create the timer used to handle the connection timeout.
                                timer = new Timer(timeoutCallback, null, Timeout, System.Threading.Timeout.Infinite);
                            }
                        }
                        
                        handle = RasHelper.Instance.Dial(PhoneBookPath, parameters, extensions, callback, GetRasEapOptions());

                        if (!asynchronous)
                        {
                            SaveCredentialsToPhoneBook();
                            ReleaseEapIdentity();

                            // The synchronous dialing operation has completed, reset the dialing flag so the component can be reused.
                            IsBusy = false;
                        }
                    }
                    catch (Exception)
                    {
                        // An exception was thrown when the component was attempting to dial a connection. Release the EAP identity and reset the dialing flag so the component can be reused.
                        ReleaseEapIdentity();
                        IsBusy = false;

                        throw;
                    }
                }
            }

            return handle;
        }

        /// <summary>
        /// Builds a new <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure based on the component settings.
        /// </summary>
        /// <returns>A new <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure.</returns>
        private NativeMethods.RASDIALEXTENSIONS BuildDialExtensions()
        {
            var result = new NativeMethods.RASDIALEXTENSIONS();
            result.options = GetRasDialOptions();

#if (WIN7 || WIN8)
            result.skipPppAuth = SkipPppAuthentication;

            if (AuthenticationCookie != IntPtr.Zero)
            {
                result.devSpecificInfo = new NativeMethods.RASDEVSPECIFICINFO {cookie = AuthenticationCookie};
            }
#endif
#if !NO_UI
            result.handle = Owner != null ? Owner.Handle : IntPtr.Zero;
#else
            result.handle = IntPtr.Zero;
#endif

            return result;
        }

        /// <summary>
        /// Builds a new <see cref="NativeMethods.RASDIALPARAMS"/> structure based on the component settings.
        /// </summary>
        /// <returns>A new <see cref="NativeMethods.RASDIALPARAMS"/> structure.</returns>
        private NativeMethods.RASDIALPARAMS BuildDialParams()
        {
            var result = new NativeMethods.RASDIALPARAMS();

            result.callbackId = CallbackId;
            result.subEntryId = SubEntryId;

#if (WIN7 || WIN8)
            result.interfaceIndex = InterfaceIndex;
#endif

            if (!string.IsNullOrEmpty(CallbackNumber))
            {
                result.callbackNumber = CallbackNumber;
            }

            if (!string.IsNullOrEmpty(EntryName))
            {
                result.entryName = EntryName;
            }

            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                result.phoneNumber = PhoneNumber;
            }

            if (Credentials == null && AllowUseStoredCredentials)
            {
                // Attempt to use any credentials stored for the entry since the caller didn't explicitly specify anything.
                var storedCredentials = RasHelper.Instance.GetCredentials(PhoneBookPath, EntryName, NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);

                if (storedCredentials != null)
                {
                    result.userName = storedCredentials.UserName;
                    result.password = storedCredentials.Password;
                    result.domain = storedCredentials.Domain;

                    storedCredentials = null;
                }
            }
            else if (Credentials != null)
            {
                result.userName = Credentials.UserName;
                result.password = Credentials.Password;
                result.domain = Credentials.Domain;
            }

            return result;
        }

        /// <summary>
        /// Releases the EAP user identity in use by the component.
        /// </summary>
        private void ReleaseEapIdentity()
        {
            if (eapDataAddress != IntPtr.Zero)
            {
                // Free the unmanaged resources used for the EAP memory block.
                Marshal.FreeHGlobal(eapDataAddress);
                eapDataAddress = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Aborts the dial operation currently in progress.
        /// </summary>
        private void Abort()
        {
            lock (syncRoot)
            {
                if (handle != null && !handle.IsInvalid)
                {
                    // NOTE: Do NOT check whether the connection is active prior to attempting to disconnect it. If the network connection fails and the handle is not
                    // released, the connection state will become corrupted and require the application to restart.                    
                    RasHelper.Instance.HangUp(handle, HangUpPollingInterval, false);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="RasDialer.DialCompleted"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.DialCompletedEventArgs"/> containing event data.</param>
        private void OnDialCompleted(DialCompletedEventArgs e)
        {
            RaiseEvent(DialCompleted, e);
        }

        /// <summary>
        /// Raises the <see cref="RasDialer.StateChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.StateChangedEventArgs"/> containing event data.</param>
        private void OnStateChanged(StateChangedEventArgs e)
        {
            RaiseEvent(StateChanged, e);
        }

        /// <summary>
        /// Notifies the asynchronous operation in progress the operation has completed.
        /// </summary>
        /// <param name="error">Any error that occurred during the asynchronous operation.</param>
        /// <param name="cancelled"><b>true</b> if the asynchronous operation was cancelled, otherwise <b>false</b>.</param>
        /// <param name="timedOut"><b>true</b> if the operation timed out, otherwise <b>false</b>.</param>
        /// <param name="connected"><b>true</b> if the connection attempt successfully connected, otherwise <b>false</b>.</param>
        private void PostCompleted(Exception error, bool cancelled, bool timedOut, bool connected)
        {
            lock (syncRoot)
            {
                if (connected)
                {
                    // The client has connected successfully, attempt to update the credentials.
                    SaveCredentialsToPhoneBook();
                }

                ReleaseEapIdentity();
                IsBusy = false;

                if (asyncOp != null)
                {
                    asyncOp.PostOperationCompleted(dialCompletedCallback, new DialCompletedEventArgs(handle, error, cancelled, timedOut, connected, null));

                    asyncOp = null;
                    handle = null;
                    timer = null;
                }
            }
        }

        /// <summary>
        /// Signaled by the asynchronous operation when the operation has completed.
        /// </summary>
        /// <param name="state">The object passed to the delegate.</param>
        private void DialCompletedCallback(object state)
        {
            var e = (DialCompletedEventArgs)state;
            OnDialCompleted(e);
        }

        /// <summary>
        /// Signaled by the internal <see cref="System.Threading.Timer"/> when the timeout duration has expired.
        /// </summary>
        /// <param name="state">An object containing application specific information.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception is passed to an error event to prevent an uncatchable exception from crashing the host application.")]
        private void TimeoutCallback(object state)
        {
            // This lock must remain to prevent the timeout occurring before the dialing process has begun if the user
            // sets the timeout at 0 to start immediately.
            lock (syncRoot)
            {
                try
                {
                    if (IsBusy)
                    {
                        Abort();
                        PostCompleted(new TimeoutException(Resources.Exception_OperationTimedOut), false, true, false);
                    }
                }
                catch (Exception ex)
                {
                    OnError(new System.IO.ErrorEventArgs(ex));
                }
            }
        }

        /// <summary>
        /// Attempts to save the credentials to the phonebook where the entry being dialed exists.
        /// </summary>
        private void SaveCredentialsToPhoneBook()
        {
            if (AutoUpdateCredentials != RasUpdateCredential.None && Credentials != null && !string.IsNullOrEmpty(EntryName) && !string.IsNullOrEmpty(PhoneBookPath))
            {
                // The client has completed negotiation, update the credentials if requested.
                Utilities.UpdateCredentials(PhoneBookPath, EntryName, Credentials, AutoUpdateCredentials);
            }
        }

        /// <summary>
        /// Signaled by the remote access service of the current state of the pending connection attempt.
        /// </summary>
        /// <param name="callbackId">An application defined value that was passed to the remote access service.</param>
        /// <param name="subEntryId">The one-based subentry index for the phone book entry associated with this connection.</param>
        /// <param name="dangerousHandle">The native handle to the connection.</param>
        /// <param name="message">The type of event that has occurred.</param>
        /// <param name="state">The state the remote access connection process is about to enter.</param>
        /// <param name="errorCode">The error that has occurred. If no error has occurred the value is zero.</param>
        /// <param name="extendedErrorCode">Any extended error information for certain non-zero values of <paramref name="errorCode"/>.</param>
        /// <returns><b>true</b> to continue to receive callback notifications, otherwise <b>false</b>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The connection handle instance is being disposed of in the finally block.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception is passed to an error event to prevent an uncatchable exception from crashing the host application.")]
        private bool RasDialCallback(IntPtr callbackId, int subEntryId, IntPtr dangerousHandle, int message, RasConnectionState state, int errorCode, int extendedErrorCode)
        {
            var retval = true;

            lock (syncRoot)
            {
                RasHandle connectionHandle = null;

                try
                {
                    connectionHandle = new RasHandle(dangerousHandle, subEntryId > 1);

                    if (!IsBusy)
                    {
                        // The connection is no longer being dialed, stop receiving notifications for this connection attempt.
                        retval = false;
                    }
                    else
                    {
                        string errorMessage = null;
                        if (errorCode != NativeMethods.SUCCESS)
                        {
                            errorMessage = RasHelper.Instance.GetRasErrorString(errorCode);
                        }

                        var e = new StateChangedEventArgs(callbackId, subEntryId, connectionHandle, state, errorCode, errorMessage, extendedErrorCode);
                        OnStateChanged(e);

                        if (errorCode != NativeMethods.SUCCESS)
                        {
                            Abort();
                            PostCompleted(new RasDialException(errorCode, extendedErrorCode), false, false, false);

                            retval = false;
                        }
                        else if (state == RasConnectionState.Connected)
                        {
                            PostCompleted(null, false, false, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError(new System.IO.ErrorEventArgs(ex));
                }
                finally
                {
                    DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, new RasDialCallbackTraceEvent(retval, callbackId, subEntryId, dangerousHandle, message, state, errorCode, extendedErrorCode));

                    if (connectionHandle != null)
                    {
                        connectionHandle.Dispose();
                    }
                }
             }

            return retval;
        }

#endregion
    }
}