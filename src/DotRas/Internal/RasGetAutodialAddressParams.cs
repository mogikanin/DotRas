//--------------------------------------------------------------------------
// <copyright file="RasGetAutodialAddressParams.cs" company="Jeff Winn">
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
    /// Provides information for the RasGetAutodialAddress API call.
    /// </summary>
    internal class RasGetAutodialAddressParams : StructBufferedPInvokeParams
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasGetAutodialAddressParams"/> class.
        /// </summary>
        public RasGetAutodialAddressParams()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Autodial address for which information is being requested.
        /// </summary>
        public string AutodialAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the reserved value.
        /// </summary>
        /// <remarks>This value must be <see cref="IntPtr.Zero"/>.</remarks>
        public IntPtr Reserved
        {
            get;
            set;
        }

        #endregion
    }
}