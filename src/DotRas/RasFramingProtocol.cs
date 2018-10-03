//--------------------------------------------------------------------------
// <copyright file="RasFramingProtocol.cs" company="Jeff Winn">
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

using JetBrains.Annotations;

namespace DotRas
{
    using System;

    /// <summary>
    /// Defines the framing protocols.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "The Windows SDK indicates the values are not flags.")]
    [PublicAPI]
    public enum RasFramingProtocol
    {
        /// <summary>
        /// No framing protocol specified.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// Point-to-point (PPP) protocol.
        /// </summary>
        Ppp = 0x1,

        /// <summary>
        /// Serial Line Internet Protocol (SLIP).
        /// </summary>
        Slip = 0x2,

        /// <summary>
        /// This member is no longer supported.
        /// </summary>
        [Obsolete("This member is no longer supported.", false)]
        Ras = 0x4
    }
}