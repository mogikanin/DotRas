//--------------------------------------------------------------------------
// <copyright file="StructBufferedPInvokeParams.cs" company="Jeff Winn">
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
    /// Provides the implementation for buffered p/invoke information classes which return a count of the number of structs in the buffer.
    /// </summary>
    internal class StructBufferedPInvokeParams : BufferedPInvokeParams
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructBufferedPInvokeParams"/> class.
        /// </summary>
        public StructBufferedPInvokeParams()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of items at the memory address.
        /// </summary>
        public IntPtr Count
        {
            get;
            set;
        }

        #endregion
    }
}