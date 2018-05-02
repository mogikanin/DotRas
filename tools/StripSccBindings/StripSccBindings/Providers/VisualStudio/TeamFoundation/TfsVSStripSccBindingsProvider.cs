//--------------------------------------------------------------------------
// <copyright file="TfsVSStripSccBindingsProvider.cs" company="Jeff Winn">
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

namespace StripSccBindings.Providers.VisualStudio.TeamFoundation
{
    using System;

    /// <summary>
    /// Provides a provider used to strip Visual Studio Team Foundation Server source control bindings. This class cannot be inherited.
    /// </summary>
    internal sealed class TfsVSStripSccBindingsProvider : VSStripSccBindingsProvider
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.TeamFoundation.TfsVSStripSccBindingsProvider"/> class.
        /// </summary>
        public TfsVSStripSccBindingsProvider()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new stripper to strip solution files.
        /// </summary>
        /// <returns>A new <see cref="TfsVSSolutionFileStripper"/> object.</returns>
        protected override VisualStudioStripperBase CreateSolutionFileStripper()
        {
            return new TfsVSSolutionFileStripper();
        }

        #endregion
    }
}