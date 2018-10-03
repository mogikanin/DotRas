//--------------------------------------------------------------------------
// <copyright file="BufferedPInvokeParams.cs" company="Jeff Winn">
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

namespace DotRas.Internal
{
    using System;

    /// <summary>
    /// Provides the base implementation for buffered p/invoke information classes.
    /// </summary>
    internal class BufferedPInvokeParams
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the memory address to the data.
        /// </summary>
        public IntPtr Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the buffer size.
        /// </summary>
        public IntPtr BufferSize
        {
            get;
            set;
        }

        #endregion
    }
}