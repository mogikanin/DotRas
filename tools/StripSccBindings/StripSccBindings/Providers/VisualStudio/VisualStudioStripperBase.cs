//--------------------------------------------------------------------------
// <copyright file="VisualStudioStripperBase.cs" company="Jeff Winn">
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
    /// Provides a base implementation for Visual Studio strippers in the extensible stripper model. This class must be inherited.
    /// </summary>
    internal abstract class VisualStudioStripperBase : StripperBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.VisualStudioStripperBase"/> class.
        /// </summary>
        protected VisualStudioStripperBase()
        {
        }

        #endregion
    }
}