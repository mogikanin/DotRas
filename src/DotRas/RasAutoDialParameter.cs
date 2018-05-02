namespace DotRas
{
    using System;

    /// <summary>
    /// Defines the remote access service (RAS) AutoDial parameters.
    /// </summary>
    public enum RasAutoDialParameter
    {
        /// <summary>
        /// Causes AutoDial to disable a dialog box displayed to the user before dialing a connection.
        /// </summary>
        DisableConnectionQuery = 0,

        /// <summary>
        /// Causes the system to disable all AutoDial connections for the current logon session.
        /// </summary>
        LogOnSessionDisable,

        /// <summary>
        /// Indicates the maximum number of addresses that AutoDial stores in the registry.
        /// </summary>
        SavedAddressesLimit,

        /// <summary>
        /// Indicates a timeout value (in seconds) when an AutoDial connection attempt fails before another connection attempt begins.
        /// </summary>
        FailedConnectionTimeout,

        /// <summary>
        /// Indicates a timeout value (in seconds) when the system displays a dialog box asking the user to confirm that the system should dial.
        /// </summary>
        ConnectionQueryTimeout
    }
}