//--------------------------------------------------------------------------
// <copyright file="VSSolutionFileStripper.cs" company="Jeff Winn">
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

namespace StripSccBindings.Providers.VisualStudio
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides a stripper used to remove source control bindings from a Visual Studio solution (.sln) file.
    /// </summary>
    internal abstract class VSSolutionFileStripper : VisualStudioStripperBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.VSSolutionFileStripper"/> class.
        /// </summary>
        public VSSolutionFileStripper()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="AfterStripBindings"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        protected override void OnAfterStripBindings(EventArgs e)
        {
            string vsssccFileName = this.Context.File.FullName.Substring(0, this.Context.File.FullName.Length - this.Context.File.Extension.Length) + ".vssscc";

            if (File.Exists(vsssccFileName))
            {
                File.Delete(vsssccFileName);
            }

            base.OnAfterStripBindings(e);
        }

        #endregion
    }
}