//--------------------------------------------------------------------------
// <copyright file="StripperBase.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace StripSccBindings.Providers
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides a base implementation for the extensible stripper model. This class must be inherited.
    /// </summary>
    internal abstract class StripperBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.StripperBase"/> class.
        /// </summary>
        protected StripperBase()
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs before the file has been stripped of the source control bindings.
        /// </summary>
        public event EventHandler<EventArgs> BeforeStripBindings;

        /// <summary>
        /// Occurs after the file has been stripped of the source control bindings.
        /// </summary>
        public event EventHandler<EventArgs> AfterStripBindings;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contextual information for the stripper.
        /// </summary>
        public StripperContext Context
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Strips the bindings.
        /// </summary>
        public void StripBindings()
        {
            this.OnBeforeStripBindings(EventArgs.Empty);

            this.InternalStripBindings();

            this.OnAfterStripBindings(EventArgs.Empty);
        }

        /// <summary>
        /// When overridden in a derived type, strips the bindings from the file.
        /// </summary>
        protected abstract void InternalStripBindings();

        /// <summary>
        /// Raises the <see cref="AfterStripBindings"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        protected virtual void OnBeforeStripBindings(EventArgs e)
        {
            if (this.BeforeStripBindings != null)
            {
                this.BeforeStripBindings(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="AfterStripBindings"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        protected virtual void OnAfterStripBindings(EventArgs e)
        {
            if (this.AfterStripBindings != null)
            {
                this.AfterStripBindings(this, e);
            }
        }

        #endregion
    }
}