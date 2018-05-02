//--------------------------------------------------------------------------
// <copyright file="RasEapOptions.cs" company="Jeff Winn">
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
    /// Represents extensible authentication protocol (EAP) options for dialing a remote access service (RAS) entry. This class cannot be inherited.
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(RasEapOptionsConverter))]
    public sealed class RasEapOptions
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEapOptions"/> class.
        /// </summary>
        public RasEapOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEapOptions"/> class.
        /// </summary>
        /// <param name="nonInteractive"><b>true</b> if the authentication protocol should not display a graphical user interface, otherwise <b>false</b>.</param>
        /// <param name="logOn"><b>true</b> if the user data is obtained from WinLogon, otherwise <b>false</b>.</param>
        /// <param name="preview"><b>true</b> if the user should be prompted for identity information before dialing, otherwise <b>false</b>.</param>
        public RasEapOptions(bool nonInteractive, bool logOn, bool preview)
        {
            this.NonInteractive = nonInteractive;
            this.LogOn = logOn;
            this.Preview = preview;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the authentication protocol should not display a graphical user interface.
        /// </summary>
        [DefaultValue(false)]
        [SRDescription("REAPONonInteractiveDesc")]
        public bool NonInteractive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user data is obtained from WinLogon.
        /// </summary>
        [DefaultValue(false)]
        [SRDescription("REAPOLogOnDesc")]
        public bool LogOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user should be prompted for identity information before dialing.
        /// </summary>
        [DefaultValue(false)]
        [SRDescription("REAPOPreviewDesc")]
        public bool Preview
        {
            get;
            set;
        }

        #endregion
    }
}