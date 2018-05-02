//--------------------------------------------------------------------------
// <copyright file="ApplicationContext.cs" company="Jeff Winn">
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

namespace StripSccBindings
{
    using System;

    /// <summary>
    /// Provides contextual information for the application process. This class cannot be inherited.
    /// </summary>
    internal sealed class ApplicationContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.ApplicationContext"/> class.
        /// </summary>
        public ApplicationContext()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the provider to remove.
        /// </summary>
        public string Provider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether folder recursion is enabled.
        /// </summary>
        public bool EnableFolderRecursion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the file filter settings.
        /// </summary>
        public string Filter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path whose files to strip.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        #endregion
    }
}