//--------------------------------------------------------------------------
// <copyright file="RasAutoDialManager.cs" company="Jeff Winn">
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
    using DotRas.Internal;

    /// <summary>
    /// Provides methods to interact with the remote access service (RAS) AutoDial mapping database. This class cannot be inherited.
    /// </summary>
    public sealed class RasAutoDialManager : Component
    {
        #region Fields

        private RasAutoDialAddressCollection _addresses;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialManager"/> class.
        /// </summary>
        public RasAutoDialManager()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialManager"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain this component.</param>
        public RasAutoDialManager(IContainer container)
        {
            if (container != null)
            {
                container.Add(this);
            }

            this.InitializeComponent();
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets the collection of addresses in the AutoDial database.
        /// </summary>
        [Browsable(false)]
        public RasAutoDialAddressCollection Addresses
        {
            get
            {
                if (this._addresses == null)
                {
                    this._addresses = new RasAutoDialAddressCollection();
                    this._addresses.Load();
                }

                return this._addresses;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether AutoDial displays a dialog box to query the user before creating a connection.
        /// </summary>
        /// <remarks><b>true</b> and the AutoDial database has the entry to dial, AutoDial creates a connection without displaying the dialog box.</remarks>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RADMDisableConnectionQueryDesc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public bool DisableConnectionQuery
        {
            get
            {
                return RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.DisableConnectionQuery) != 0;
            }

            set
            {
                int actual = 0;

                if (value)
                {
                    actual = 1;
                }

                RasHelper.Instance.SetAutoDialParameter(NativeMethods.RASADP.DisableConnectionQuery, actual);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the system disables all AutoDial connections for the current logon session.
        /// </summary>
        /// <remarks><b>true</b> if the AutoDial connections are disabled, otherwise <b>false</b>.</remarks>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RADMLogOnSessionDisableDesc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public bool LogOnSessionDisable
        {
            get
            {
                return RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.LogOnSessionDisable) != 0;
            }

            set
            {
                int actual = 0;

                if (value)
                {
                    actual = 1;
                }

                RasHelper.Instance.SetAutoDialParameter(NativeMethods.RASADP.LogOnSessionDisable, actual);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of addresses that AutoDial stores in the registry.
        /// </summary>
        /// <remarks>The default value is 100.</remarks>
        [DefaultValue(100)]
        [SRCategory("CatBehavior")]
        [SRDescription("RADMSavedAddressesLimitDesc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public int SavedAddressesLimit
        {
            get
            {
                return RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.SavedAddressesLimit);
            }

            set
            {
                RasHelper.Instance.SetAutoDialParameter(NativeMethods.RASADP.SavedAddressesLimit, value);
            }
        }

        /// <summary>
        /// Gets or sets the length of time (in seconds) between AutoDial connection attempts.
        /// </summary>
        /// <remarks>When an AutoDial connection attempt fails, the AutoDial service disables subsequent attempts to reach the same address for the timeout period. The default value is 5 seconds.</remarks>
        [DefaultValue(5)]
        [SRCategory("CatBehavior")]
        [SRDescription("RADMFailedConnectionTimeoutDesc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public int FailedConnectionTimeout
        {
            get
            {
                return RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.FailedConnectionTimeout);
            }

            set
            {
                RasHelper.Instance.SetAutoDialParameter(NativeMethods.RASADP.FailedConnectionTimeout, value);
            }
        }

        /// <summary>
        /// Gets or sets the length of time (in seconds) before the connection attempt is aborted.
        /// </summary>
        /// <remarks>Before attempting an AutoDial connection, the system will display a dialog asking the user to confirm the system should dial.</remarks>
        [DefaultValue(60)]
        [SRCategory("CatBehavior")]
        [SRDescription("RADMConnectionQueryTimeoutDesc")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public int ConnectionQueryTimeout
        {
            get
            {
                return RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout);
            }

            set
            {
                RasHelper.Instance.SetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the AutoDial status for a specific TAPI dialing location.
        /// </summary>
        /// <param name="dialingLocation">The TAPI dialing location to update.</param>
        /// <param name="enabled"><b>true</b> to enable AutoDial, otherwise <b>false</b> to disable it.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public bool UpdateAutoDialStatus(int dialingLocation, bool enabled)
        {
            return RasHelper.Instance.SetAutoDialEnable(dialingLocation, enabled);
        }

        /// <summary>
        /// Indicates whether AutoDial is enabled for a specific TAPI dialing location.
        /// </summary>
        /// <param name="dialingLocation">The dialing location whose AutoDial status to retrieve.</param>
        /// <returns><b>true</b> if the AutoDial feature is currently enabled for the dialing location, otherwise <b>false</b>.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The design is ok.")]
        public bool IsAutoDialEnabled(int dialingLocation)
        {
            return RasHelper.Instance.GetAutoDialEnable(dialingLocation);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Added for designer support.")]
        private void InitializeComponent()
        {
        }

        #endregion
    }
}