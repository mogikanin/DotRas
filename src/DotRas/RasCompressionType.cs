//--------------------------------------------------------------------------
// <copyright file="RasCompressionType.cs" company="Jeff Winn">
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
    /// Defines the remote access service (RAS) compression algorithms.
    /// </summary>
    public enum RasCompressionType
    {
        /// <summary>
        /// No compression in use.
        /// </summary>
        None = 0x0,
        
        /// <summary>
        /// STAC option 4.
        /// </summary>
        Stac = 0x5,

        /// <summary>
        /// Microsoft Point-to-Point Compression (MPPC) protocol.
        /// </summary>
        Mppc = 0x6
    }
}