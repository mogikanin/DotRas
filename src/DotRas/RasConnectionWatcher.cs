//--------------------------------------------------------------------------
// <copyright file="RasConnectionWatcher.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Threading;
    using DotRas.Design;
    using DotRas.Internal;
    using DotRas.Properties;

    /// <summary>
    /// Listens to the remote access service (RAS) change notifications and raises events when connections change. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When using <b>RasConnectionWatcher</b> to monitor active connections for changes, ensure the SynchronizingObject property is set if thread synchronization is required. If this is not done, you may get cross-thread exceptions thrown from the component. This is typically needed with applications that have an interface; for example, Windows Forms or Windows Presentation Foundation (WPF).
    /// </para>
    /// <para>
    /// The connected event raised by this component will be raised whenever a connection has connected on the machine. If a valid handle is provided to the <see cref="Handle"/> property, the other events will only receive notifications for that handle however the connected event will remain unchanged.
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how to use a <b>RasConnectionWatcher</b> object to monitor connection changes on a machine.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnectionWatcher watcher = new RasConnectionWatcher();  
    /// public void Begin()
    /// {
    ///    watcher.Connected += new EventHandler<RasConnectionEventArgs>(this.watcher_Connected);
    ///    watcher.Disconnected += new EventHandler<RasConnectionEventArgs>(this.watcher_Disconnected);
    ///    watcher.EnableRaisingEvents = true;
    /// }
    /// private void watcher_Connected(object sender, RasConnectionEventArgs e)
    /// {
    ///    // A connection has successfully connected.
    /// }
    /// private void watcher_Disconnected(object sender, RasConnectionEventArgs)
    /// {
    ///    // A connection has disconnected successfully.
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim watcher As New RasConnectionWatcher
    /// Public Sub Begin()
    ///    AddHandler watcher.Connected, Me.watcher_Connected
    ///    AddHandler watcher.Disconnected, Me.watcher_Disconnected
    ///    watcher.EnableRaisingEvents = True
    /// End Sub
    /// Public Sub watcher_Connected(ByVal sender As Object, ByVal e As RasConnectionEventArgs)
    ///    ' A connection has successfully connected.
    /// End Sub
    /// Public Sub watcher_Disconnected(ByVal sender As Object, ByVal e As RasConnectionEventArgs)
    ///    ' A connection has disconnected successfully.
    /// End Sub
    /// ]]>
    /// </code>
    /// </example>
    public sealed partial class RasConnectionWatcher : RasComponent, ISupportInitialize
    {
        #region Fields

        /// <summary>
        /// Defines the lock object used for cross-thread synchronization.
        /// </summary>
        private readonly object lockObject = new object();

        private bool _initializing;
        private bool _enableRaisingEvents;
        private Collection<RasConnectionWatcherStateObject> _stateObjects;
        private ReadOnlyCollection<RasConnection> _lastState;
        private RasHandle _handle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasConnectionWatcher"/> class.
        /// </summary>
        public RasConnectionWatcher()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasConnectionWatcher"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain the component.</param>
        public RasConnectionWatcher(IContainer container)
            : base(container)
        {
            this.InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a remote access connection is established.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RCWConnectedDesc")]
        public event EventHandler<RasConnectionEventArgs> Connected;

        /// <summary>
        /// Occurs when a remote access connection is disconnected.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RCWDisconnectedDesc")]
        public event EventHandler<RasConnectionEventArgs> Disconnected;

        /// <summary>
        /// Occurs when a remote access connection subentry has connected.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RCWBandwidthAddedDesc")]
        public event EventHandler<EventArgs> BandwidthAdded;

        /// <summary>
        /// Occurs when a remote access connection subentry is disconnected.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RCWBandwidthRemovedDesc")]
        public event EventHandler<EventArgs> BandwidthRemoved;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the connection handle to watch.
        /// </summary>
        [Browsable(false)]
        public RasHandle Handle
        {
            get
            {
                return this._handle;
            }

            set
            {
                if (value != null && Utilities.IsHandleInvalidOrClosed(value))
                {
                    // The value can be a null handle to receive events for all connections on the system.
                    ThrowHelper.ThrowInvalidHandleException(value, Resources.Exception_InvalidOrClosedHandle);
                }

                if (this._handle != value)
                {
                    this._handle = value;

                    this.Restart();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the component is enabled.
        /// </summary>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RCWEnableRaisingEventsDesc")]
        public bool EnableRaisingEvents
        {
            get
            {
                return this._enableRaisingEvents;
            }

            set
            {
                if (this._enableRaisingEvents != value)
                {
                    this._enableRaisingEvents = value;

                    if (!this.IsSuspended())
                    {
                        if (this._enableRaisingEvents)
                        {
                            this.StartRaisingEvents();
                        }
                        else
                        {
                            this.StopRaisingEvents();
                        }
                    }
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Begins the initialization of a <see cref="DotRas.RasConnectionWatcher"/> used on a form or other component.
        /// </summary>
        public void BeginInit()
        {
            bool enabled = this.EnableRaisingEvents;
            this.StopRaisingEvents();

            this.EnableRaisingEvents = enabled;
            this._initializing = true;
        }

        /// <summary>
        /// Ends the initialization of a <see cref="DotRas.RasConnectionWatcher"/> used on a form or other component.
        /// </summary>
        public void EndInit()
        {
            this._initializing = false;
            if (!this.IsSuspended() && this.EnableRaisingEvents)
            {
                this.StartRaisingEvents();
            }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected override void InitializeComponent()
        {
            this._stateObjects = new Collection<RasConnectionWatcherStateObject>();

            base.InitializeComponent();
        }

        /// <summary>
        /// Finds the missing entry within the collections.
        /// </summary>
        /// <param name="collectionA">The collection to search.</param>
        /// <param name="collectionB">The collection to locate the missing entry in.</param>
        /// <returns>The <see cref="DotRas.RasConnection"/> not found in the collection.</returns>
        private static RasConnection FindEntry(ReadOnlyCollection<RasConnection> collectionA, ReadOnlyCollection<RasConnection> collectionB)
        {
            RasConnection entry = null;

            foreach (RasConnection connectionA in collectionA)
            {
                bool found = false;

                foreach (RasConnection connectionB in collectionB)
                {
                    if (connectionA.EntryId == connectionB.EntryId)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    entry = connectionA;
                    break;
                }
            }

            return entry;
        }

        /// <summary>
        /// Starts the watcher raising events.
        /// </summary>
        private void StartRaisingEvents()
        {
            // Retrieve the current state of the connections on the machine. This is used for comparisons to determine what has changed
            // once the events are raised.
            this._lastState = RasConnection.GetActiveConnections();

            try
            {
                this.Register(NativeMethods.RASCN.Connection, NativeMethods.INVALID_HANDLE_VALUE);

                if (this.Handle == null || this.Handle.IsInvalid)
                {
                    this.Register(NativeMethods.RASCN.Disconnection, NativeMethods.INVALID_HANDLE_VALUE);
                }
                else
                {
                    this.Register(NativeMethods.RASCN.Disconnection, this.Handle);
                    this.Register(NativeMethods.RASCN.BandwidthAdded, this.Handle);
                    this.Register(NativeMethods.RASCN.BandwidthRemoved, this.Handle);
                }
            }
            catch (EntryPointNotFoundException)
            {
                ThrowHelper.ThrowNotSupportedException(Resources.Exception_NotSupportedOnPlatform);
            }
        }

        /// <summary>
        /// Stops the watcher from raising events.
        /// </summary>
        private void StopRaisingEvents()
        {
            if (!this.IsSuspended() && this.EnableRaisingEvents)
            {				
                int count = this._stateObjects.Count;

                for (int index = 0; index < count; index++)
                {
                    RasConnectionWatcherStateObject item = this._stateObjects[0];
                    if (item != null)
                    {
                        this.Unregister(item);
                    }
                }
            }
        }

        /// <summary>
        /// Restarts the instance.
        /// </summary>
        private void Restart()
        {
            if (!this.IsSuspended() && this.EnableRaisingEvents)
            {
                this.StopRaisingEvents();
                this.StartRaisingEvents();
            }
        }

        /// <summary>
        /// Registers a change type with the watcher.
        /// </summary>
        /// <param name="changeType">The change type to monitor.</param>
        /// <param name="handle">The handle of the connection to monitor.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The event must be disposed only after the change type is unregistered.")]
        private void Register(NativeMethods.RASCN changeType, RasHandle handle)
        {
            AutoResetEvent waitObject = new AutoResetEvent(false);

            int ret = SafeNativeMethods.Instance.RegisterConnectionNotification(handle, waitObject.SafeWaitHandle, changeType);
            if (ret == NativeMethods.SUCCESS)
            {
                RasConnectionWatcherStateObject stateObject = new RasConnectionWatcherStateObject(changeType);

                stateObject.WaitObject = waitObject;
                stateObject.WaitHandle = ThreadPool.RegisterWaitForSingleObject(waitObject, new WaitOrTimerCallback(this.ConnectionStateChanged), stateObject, Timeout.Infinite, false);

                this._stateObjects.Add(stateObject);
            }
        }

        /// <summary>
        /// Unregisters a state object from being monitored.
        /// </summary>
        /// <param name="item">An <see cref="RasConnectionWatcherStateObject"/> to unregister.</param>
        private void Unregister(RasConnectionWatcherStateObject item)
        {
            if (item != null)
            {
                item.WaitHandle.Unregister(item.WaitObject);
                item.WaitObject.Close();

                this._stateObjects.Remove(item);
            }
        }

        /// <summary>
        /// Determines whether the component is currently suspended.
        /// </summary>
        /// <returns><b>true</b> if the component is suspended, otherwise <b>false</b>.</returns>
        private bool IsSuspended()
        {
            bool retval = true;

            if (!this._initializing)
            {
                retval = this.DesignMode;
            }

            return retval;
        }

        /// <summary>
        /// Notified when the connection state changes.
        /// </summary>
        /// <param name="obj">The state object.</param>
        /// <param name="timedOut"><b>true</b> if the connection timed out, otherwise <b>false</b>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The exception is passed to an error event to prevent an uncatchable exception from crashing the host application.")]
        private void ConnectionStateChanged(object obj, bool timedOut)
        {
            lock (this.lockObject)
            {
                if (this.EnableRaisingEvents)
                {
                    try
                    {
                        // Retrieve the active connections to compare against the last state that was checked.
                        ReadOnlyCollection<RasConnection> connections = RasConnection.GetActiveConnections();
                        RasConnection connection = null;

                        switch (((RasConnectionWatcherStateObject)obj).ChangeType)
                        {
                            case NativeMethods.RASCN.Connection:
                                connection = FindEntry(connections, this._lastState);
                                if (connection != null)
                                {
                                    this.OnConnected(new RasConnectionEventArgs(connection));
                                }

                                this._lastState = connections;

                                break;

                            case NativeMethods.RASCN.Disconnection:
                                // The handle has not been set or it's invalid, the item that has disconnected will need to be
                                // determined.
                                connection = FindEntry(this._lastState, connections);
                                if (connection != null)
                                {
                                    this.OnDisconnected(new RasConnectionEventArgs(connection));
                                }

                                if (this.Handle != null)
                                {
                                    // The handle that was being monitored has been disconnected.
                                    this.Handle = null;
                                }

                                this._lastState = connections;

                                break;

                            case NativeMethods.RASCN.BandwidthAdded:
                                if (this.Handle != null && !this.Handle.IsInvalid)
                                {
                                    this.OnBandwidthAdded(EventArgs.Empty);
                                }

                                break;

                            case NativeMethods.RASCN.BandwidthRemoved:
                                if (this.Handle != null && !this.Handle.IsInvalid)
                                {
                                    this.OnBandwidthRemoved(EventArgs.Empty);
                                }

                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.OnError(new System.IO.ErrorEventArgs(ex));
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="RasConnectionWatcher.Connected"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasConnectionEventArgs"/> containing event data.</param>
        private void OnConnected(RasConnectionEventArgs e)
        {
            this.RaiseEvent<RasConnectionEventArgs>(this.Connected, e);
        }

        /// <summary>
        /// Raises the <see cref="RasConnectionWatcher.Disconnected"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasConnectionEventArgs"/> containing event data.</param>
        private void OnDisconnected(RasConnectionEventArgs e)
        {
            this.RaiseEvent<RasConnectionEventArgs>(this.Disconnected, e);
        }

        /// <summary>
        /// Raises the <see cref="RasConnectionWatcher.BandwidthAdded"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void OnBandwidthAdded(EventArgs e)
        {
            this.RaiseEvent<EventArgs>(this.BandwidthAdded, e);
        }

        /// <summary>
        /// Raises the <see cref="RasConnectionWatcher.BandwidthRemoved"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void OnBandwidthRemoved(EventArgs e)
        {
            this.RaiseEvent<EventArgs>(this.BandwidthRemoved, e);
        }

        #endregion

        #region RasConnectionWatcherStateObject Class

        /// <summary>
        /// Provides state information for remote access connection monitoring. This class cannot be inherited.
        /// </summary>
        private sealed class RasConnectionWatcherStateObject
        {
            #region Fields

            private RegisteredWaitHandle _waitHandle;
            private AutoResetEvent _waitObject;
            private NativeMethods.RASCN _changeType;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RasConnectionWatcherStateObject"/> class.
            /// </summary>
            /// <param name="changeType">The change type being monitored.</param>
            public RasConnectionWatcherStateObject(NativeMethods.RASCN changeType)
            {
                this._changeType = changeType;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the change type being monitored.
            /// </summary>
            public NativeMethods.RASCN ChangeType
            {
                get { return this._changeType; }
            }

            /// <summary>
            /// Gets or sets the wait handle registered for callback operations.
            /// </summary>
            public RegisteredWaitHandle WaitHandle
            {
                get { return this._waitHandle; }
                set { this._waitHandle = value; }
            }

            /// <summary>
            /// Gets or sets the wait object receiving signaling for connection state changes.
            /// </summary>
            public AutoResetEvent WaitObject
            {
                get { return this._waitObject; }
                set { this._waitObject = value; }
            }

            #endregion
        }

        #endregion
    }
}