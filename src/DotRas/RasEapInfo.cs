//--------------------------------------------------------------------------
// <copyright file="RasEapInfo.cs" company="Jeff Winn">
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

namespace DotRas
{
    using System;

    /// <summary>
    /// Represents user specific Extensible Authentication Protocol (EAP) information. This class cannot be inherited.
    /// </summary>
    public sealed class RasEapInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEapInfo"/> class.
        /// </summary>
        public RasEapInfo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEapInfo"/> class.
        /// </summary>
        /// <param name="sizeOfEapData">The size of the binary information pointed to by <paramref name="eapData"/>.</param>
        /// <param name="eapData">The pointer to the binary EAP information.</param>
        public RasEapInfo(int sizeOfEapData, IntPtr eapData)
        {
            this.SizeOfEapData = sizeOfEapData;
            this.EapData = eapData;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the size of the binary information pointed to by the <see cref="RasEapInfo.EapData"/> property.
        /// </summary>
        public int SizeOfEapData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a pointer to the binary data.
        /// </summary>
        public IntPtr EapData
        {
            get;
            set;
        }

        #endregion
    }
}