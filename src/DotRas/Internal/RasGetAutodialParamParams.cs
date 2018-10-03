//--------------------------------------------------------------------------
// <copyright file="RasGetAutodialParamParams.cs" company="Jeff Winn">
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
    /// Provides information for the RasGetAutodialParam API call.
    /// </summary>
    internal class RasGetAutodialParamParams
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public NativeMethods.RASADP Key
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the memory address to receive the information.
        /// </summary>
        public IntPtr Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the size of the buffer.
        /// </summary>
        public int BufferSize
        {
            get;
            set;
        }

        #endregion
    }
}