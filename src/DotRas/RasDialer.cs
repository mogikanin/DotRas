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
    using System.Windows.Forms;
    using DotRas.Design;
    using DotRas.Diagnostics;
    using DotRas.Internal;
    using DotRas.Properties;
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
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialer"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain the component.</param>
        public RasDialer(IContainer container)
            : base(container)
        {
            this.InitializeComponent();
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
                bool retval = false;

                lock (this.syncRoot)
                {
                    retval = this._isBusy;
                }

                return retval;
            }

            private set
            {
                this._isBusy = value;
            }
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
            get
            {
                if (this._options == null)
                {
                    this._options = new RasDialOptions();
                }

                return this._options;
            }

            set
            {
                this._options = value;
            }
        }        

        /// <summary>
        /// Gets or sets the extensible authentication protocol (EAP) options.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RDEapOptionsDesc")]
        public RasEapOptions EapOptions
        {
            get
            {
                if (this._eapOptions == null)
                {
                    this._eapOptions = new RasEapOptions();
                }

                return this._eapOptions;
            }

            set
            {
                this._eapOptions = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent window.
        /// </summary>
        /// <remarks>This object is used for dialog box creation and centering when a security DLL has been defined.</remarks>
        [DefaultValue(null)]
        [SRCategory("CatBehavior")]
        [SRDescription("RDOwnerDesc")]
        public IWin32Window Owner
        {
            get;
            set;
        }

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
            return this.InternalDial(false);
        }

        /// <summary>
        /// Dials the connection asynchronously.
        /// </summary>
        /// <returns>The handle of the connection.</returns>
        /// <exception cref="System.InvalidOperationException">A phone number or an entry name with phone book path is required to dial.</exception>
        public RasHandle DialAsync()
        {
            return this.InternalDial(true);
        }

        /// <summary>
        /// Cancels the asynchronous dial operation.
        /// </summary>
        public void DialAsyncCancel()
        {
            lock (this.syncRoot)
            {
                if (this.IsBusy)
                {
                    this.Abort();
                    this.PostCompleted(null, true, false, false);
                }
            }
        }

        /// <summary>
        /// Sets the EAP data used by the connection attempt.
        /// </summary>
        /// <param name="data">A byte array containing EAP data.</param>
        public void SetEapUserData(byte[] data)
        {
            this.eapUserData = data;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DotRas.RasDialer"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.IsBusy)
                {
                    // The component is currently dialing a connection, abort the connection attempt.
                    this.Abort();

                    this.handle = null;
                    this.asyncOp = null;
                }

                if (this.timer != null)
                {
                    this.timer.Dispose();
                    this.timer = null;
                }

                this.ReleaseEapIdentity();
                this.Credentials = null;

                this.dialCompletedCallback = null;
                this.rasDialCallback = null;
                this.timeoutCallback = null;
            }          

            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected override void InitializeComponent()
        {
            this.Timeout = System.Threading.Timeout.Infinite;
            this.HangUpPollingInterval = NativeMethods.HangUpPollingInterval;

            this.dialCompletedCallback = new SendOrPostCallback(this.DialCompletedCallback);
            this.timeoutCallback = new TimerCallback(this.TimeoutCallback);
            this.rasDialCallback = new NativeMethods.RasDialFunc2(this.RasDialCallback);

            base.InitializeComponent();
        }

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RDEOPT"/> flags for the component.
        /// </summary>
        /// <returns>The <see cref="NativeMethods.RDEOPT"/> flags.</returns>
        private NativeMethods.RDEOPT GetRasDialOptions()
        {
            NativeMethods.RDEOPT value = NativeMethods.RDEOPT.None;

            if (this.Options != null)
            {
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.UsePrefixSuffix, NativeMethods.RDEOPT.UsePrefixSuffix);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.PausedStates, NativeMethods.RDEOPT.PausedStates);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(!string.IsNullOrEmpty(this.EntryName) && this.Options.SetModemSpeaker, NativeMethods.RDEOPT.IgnoreModemSpeaker);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.SetModemSpeaker, NativeMethods.RDEOPT.SetModemSpeaker);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(!string.IsNullOrEmpty(this.EntryName) && this.Options.SetSoftwareCompression, NativeMethods.RDEOPT.IgnoreSoftwareCompression);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.SetSoftwareCompression, NativeMethods.RDEOPT.SetSoftwareCompression);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.DisableConnectedUI, NativeMethods.RDEOPT.DisableConnectedUI);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.DisableReconnectUI, NativeMethods.RDEOPT.DisableReconnectUI);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.NoUser, NativeMethods.RDEOPT.NoUser);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.Router, NativeMethods.RDEOPT.Router);
                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.CustomDial, NativeMethods.RDEOPT.CustomDial);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

                value |= (NativeMethods.RDEOPT)Utilities.SetFlag(this.Options.UseCustomScripting, NativeMethods.RDEOPT.UseCustomScripting);

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
            NativeMethods.RASEAPF value = NativeMethods.RASEAPF.None;

            if (this.EapOptions != null)
            {
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(this.EapOptions.NonInteractive, NativeMethods.RASEAPF.NonInteractive);
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(this.EapOptions.LogOn, NativeMethods.RASEAPF.LogOn);
                value |= (NativeMethods.RASEAPF)Utilities.SetFlag(this.EapOptions.Preview, NativeMethods.RASEAPF.Preview);
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
            if (string.IsNullOrEmpty(this.PhoneNumber) && (string.IsNullOrEmpty(this.EntryName) || string.IsNullOrEmpty(this.PhoneBookPath)))
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_PhoneNumberOrEntryNameRequired);
            }

            lock (this.syncRoot)
            {
                // NOTE: The synchronization object MUST be locked prior to testing of the component is already busy.
                // WARNING! Ensure no exceptions are thrown because existing dial attempts are already in progress. Doing so leaves the
                // connection open and cannot be closed if the application is terminated.
                if (!this.IsBusy)
                {                    
                    this.IsBusy = true;

                    try
                    {
                        NativeMethods.RASDIALPARAMS parameters = this.BuildDialParams();
                        NativeMethods.RASDIALEXTENSIONS extensions = this.BuildDialExtensions();

                        if (!string.IsNullOrEmpty(this.EntryName))
                        {
                            byte[] data = null;

                            if (this.eapUserData != null)
                            {
                                data = this.eapUserData;
                            }
                            else
                            {
                                data = RasHelper.Instance.GetEapUserData(IntPtr.Zero, this.PhoneBookPath, this.EntryName);
                            }

                            if (data != null)
                            {
                                this.eapDataAddress = Marshal.AllocHGlobal(data.Length);
                                Marshal.Copy(data, 0, this.eapDataAddress, data.Length);

                                extensions.eapInfo.eapData = this.eapDataAddress;
                                extensions.eapInfo.sizeOfEapData = data.Length;
                            }
                        }

                        NativeMethods.RasDialFunc2 callback = null;
                        if (asynchronous)
                        {
                            callback = this.rasDialCallback;

                            this.asyncOp = AsyncOperationManager.CreateOperation(null);

                            if (this.timer != null)
                            {
                                // Dispose of any existing timer if the component is being reused.
                                this.timer.Dispose();
                                this.timer = null;
                            }

                            if (this.Timeout != System.Threading.Timeout.Infinite)
                            {
                                // A timeout has been requested, create the timer used to handle the connection timeout.
                                this.timer = new Timer(this.timeoutCallback, null, this.Timeout, System.Threading.Timeout.Infinite);
                            }
                        }
                        
                        this.handle = RasHelper.Instance.Dial(this.PhoneBookPath, parameters, extensions, callback, this.GetRasEapOptions());

                        if (!asynchronous)
                        {
                            this.SaveCredentialsToPhoneBook();
                            this.ReleaseEapIdentity();

                            // The synchronous dialing operation has completed, reset the dialing flag so the component can be reused.
                            this.IsBusy = false;
                        }
                    }
                    catch (Exception)
                    {
                        // An exception was thrown when the component was attempting to dial a connection. Release the EAP identity and reset the dialing flag so the component can be reused.
                        this.ReleaseEapIdentity();
                        this.IsBusy = false;

                        throw;
                    }
                }
            }

            return this.handle;
        }

        /// <summary>
        /// Builds a new <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure based on the component settings.
        /// </summary>
        /// <returns>A new <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure.</returns>
        private NativeMethods.RASDIALEXTENSIONS BuildDialExtensions()
        {
            NativeMethods.RASDIALEXTENSIONS result = new NativeMethods.RASDIALEXTENSIONS();
            result.options = this.GetRasDialOptions();

#if (WIN7 || WIN8)
            result.skipPppAuth = this.SkipPppAuthentication;

            if (this.AuthenticationCookie != IntPtr.Zero)
            {
                result.devSpecificInfo = new NativeMethods.RASDEVSPECIFICINFO();
                result.devSpecificInfo.cookie = this.AuthenticationCookie;
            }
#endif

            result.handle = this.Owner != null ? this.Owner.Handle : IntPtr.Zero;

            return result;
        }

        /// <summary>
        /// Builds a new <see cref="NativeMethods.RASDIALPARAMS"/> structure based on the component settings.
        /// </summary>
        /// <returns>A new <see cref="NativeMethods.RASDIALPARAMS"/> structure.</returns>
        private NativeMethods.RASDIALPARAMS BuildDialParams()
        {
            NativeMethods.RASDIALPARAMS result = new NativeMethods.RASDIALPARAMS();

            result.callbackId = this.CallbackId;
            result.subEntryId = this.SubEntryId;

#if (WIN7 || WIN8)
            result.interfaceIndex = this.InterfaceIndex;
#endif

            if (!string.IsNullOrEmpty(this.CallbackNumber))
            {
                result.callbackNumber = this.CallbackNumber;
            }

            if (!string.IsNullOrEmpty(this.EntryName))
            {
                result.entryName = this.EntryName;
            }

            if (!string.IsNullOrEmpty(this.PhoneNumber))
            {
                result.phoneNumber = this.PhoneNumber;
            }

            if (this.Credentials == null && this.AllowUseStoredCredentials)
            {
                // Attempt to use any credentials stored for the entry since the caller didn't explicitly specify anything.
                NetworkCredential storedCredentials = RasHelper.Instance.GetCredentials(this.PhoneBookPath, this.EntryName, NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);

                if (storedCredentials != null)
                {
                    result.userName = storedCredentials.UserName;
                    result.password = storedCredentials.Password;
                    result.domain = storedCredentials.Domain;

                    storedCredentials = null;
                }
            }
            else if (this.Credentials != null)
            {
                result.userName = this.Credentials.UserName;
                result.password = this.Credentials.Password;
                result.domain = this.Credentials.Domain;
            }

            return result;
        }

        /// <summary>
        /// Releases the EAP user identity in use by the component.
        /// </summary>
        private void ReleaseEapIdentity()
        {
            if (this.eapDataAddress != IntPtr.Zero)
            {
                // Free the unmanaged resources used for the EAP memory block.
                Marshal.FreeHGlobal(this.eapDataAddress);
                this.eapDataAddress = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Aborts the dial operation currently in progress.
        /// </summary>
        private void Abort()
        {
            lock (this.syncRoot)
            {
                if (this.handle != null && !this.handle.IsInvalid)
                {
                    // NOTE: Do NOT check whether the connection is active prior to attempting to disconnect it. If the network connection fails and the handle is not
                    // released, the connection state will become corrupted and require the application to restart.                    
                    RasHelper.Instance.HangUp(this.handle, this.HangUpPollingInterval, false);
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="RasDialer.DialCompleted"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.DialCompletedEventArgs"/> containing event data.</param>
        private void OnDialCompleted(DialCompletedEventArgs e)
        {
            this.RaiseEvent<DialCompletedEventArgs>(this.DialCompleted, e);
        }

        /// <summary>
        /// Raises the <see cref="RasDialer.StateChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.StateChangedEventArgs"/> containing event data.</param>
        private void OnStateChanged(StateChangedEventArgs e)
        {
            this.RaiseEvent<StateChangedEventArgs>(this.StateChanged, e);
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
            lock (this.syncRoot)
            {
                if (connected)
                {
                    // The client has connected successfully, attempt to update the credentials.
                    this.SaveCredentialsToPhoneBook();
                }

                this.ReleaseEapIdentity();
                this.IsBusy = false;

                if (this.asyncOp != null)
                {
                    this.asyncOp.PostOperationCompleted(this.dialCompletedCallback, new DialCompletedEventArgs(this.handle, error, cancelled, timedOut, connected, null));

                    this.asyncOp = null;
                    this.handle = null;
                    this.timer = null;
                }
            }
        }

        /// <summary>
        /// Signaled by the asynchronous operation when the operation has completed.
        /// </summary>
        /// <param name="state">The object passed to the delegate.</param>
        private void DialCompletedCallback(object state)
        {
            DialCompletedEventArgs e = (DialCompletedEventArgs)state;
            this.OnDialCompleted(e);
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
            lock (this.syncRoot)
            {
                try
                {
                    if (this.IsBusy)
                    {
                        this.Abort();
                        this.PostCompleted(new TimeoutException(Resources.Exception_OperationTimedOut), false, true, false);
                    }
                }
                catch (Exception ex)
                {
                    this.OnError(new System.IO.ErrorEventArgs(ex));
                }
            }
        }

        /// <summary>
        /// Attempts to save the credentials to the phonebook where the entry being dialed exists.
        /// </summary>
        private void SaveCredentialsToPhoneBook()
        {
            if (this.AutoUpdateCredentials != RasUpdateCredential.None && this.Credentials != null && !string.IsNullOrEmpty(this.EntryName) && !string.IsNullOrEmpty(this.PhoneBookPath))
            {
                // The client has completed negotiation, update the credentials if requested.
                Utilities.UpdateCredentials(this.PhoneBookPath, this.EntryName, this.Credentials, this.AutoUpdateCredentials);
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
            bool retval = true;

            lock (this.syncRoot)
            {
                RasHandle connectionHandle = null;

                try
                {
                    connectionHandle = new RasHandle(dangerousHandle, subEntryId > 1);

                    if (!this.IsBusy)
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

                        StateChangedEventArgs e = new StateChangedEventArgs(callbackId, subEntryId, connectionHandle, state, errorCode, errorMessage, extendedErrorCode);
                        this.OnStateChanged(e);

                        if (errorCode != NativeMethods.SUCCESS)
                        {
                            this.Abort();
                            this.PostCompleted(new RasDialException(errorCode, extendedErrorCode), false, false, false);

                            retval = false;
                        }
                        else if (state == RasConnectionState.Connected)
                        {
                            this.PostCompleted(null, false, false, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.OnError(new System.IO.ErrorEventArgs(ex));
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