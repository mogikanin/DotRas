//--------------------------------------------------------------------------
// <copyright file="RasCommonDialog.cs" company="Jeff Winn">
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

namespace DotRas.Design
{
    using DotRas.Internal;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Specifies the base class used for displaying remote access service (RAS) dialog boxes on the screen. This class must be inherited.
    /// </summary>
    public abstract class RasCommonDialog : CommonDialog
    {
        #region Fields

        private Point _location;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasCommonDialog"/> class.
        /// </summary>
        protected RasCommonDialog()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the dialog has encountered an error.
        /// </summary>
        [SRDescription("RCDErrorDesc")]
        public event EventHandler<RasErrorEventArgs> Error;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the dialog box.
        /// </summary>
        [DefaultValue(typeof(Point), "0,0")]
        [SRCategory("CatLayout")]
        [SRDescription("RCDLocationDesc")]
        public Point Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all <see cref="RasCommonDialog"/> properties to their default values.
        /// </summary>
        public override void Reset()
        {
            this.Location = Point.Empty;
        }

        /// <summary>
        /// Raises the <see cref="Error"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasErrorEventArgs"/> containing event data.</param>
        protected void OnError(RasErrorEventArgs e)
        {
            if (this.Error != null)
            {
                this.Error(this, e);
            }
        }

        #endregion
    }
}