//--------------------------------------------------------------------------
// <copyright file="RasProjectionType.cs" company="Jeff Winn">
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
    /// <summary>
    /// Defines the projection types.
    /// </summary>
    /// <remarks>The projection types defined here are used on a projection operation on an active connection.</remarks>
    public enum RasProjectionType
    {
        /// <summary>
        /// Authentication Message Block (AMB) protocol.
        /// </summary>
        Amb,

        /// <summary>
        /// NetBEUI Framer (NBF) protocol.
        /// </summary>
        Nbf,

        /// <summary>
        /// Internetwork Packet Exchange (IPX) control protocol.
        /// </summary>
        Ipx,

        /// <summary>
        /// Internet Protocol (IP) control protocol.
        /// </summary>
        IP,

        /// <summary>
        /// Compression Control Protocol (CCP).
        /// </summary>
        Ccp,

        /// <summary>
        /// Link Control Protocol (LCP).
        /// </summary>
        Lcp,

        /// <summary>
        /// Serial Line Internet Protocol (SLIP).
        /// <para>
        /// <b>Windows Vista or later:</b> This value is no longer supported.
        /// </para>
        /// </summary>
        Slip,

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Internet Protocol Version 6 (IPv6) control protocol.
        /// <para>
        /// <b>Windows Vista or later:</b> This value is supported.
        /// </para>
        /// </summary>
        IPv6,

#endif

#if (WIN7 || WIN8)

        /// <summary>
        /// Point-to-Point protocol (PPP).
        /// <para>
        /// <b>Windows 7 or later:</b> This value is supported.
        /// </para>
        /// </summary>
        Ppp,

        /// <summary>
        /// Internet Key Exchange (IKEv2) protocol.
        /// <para>
        /// <b>Windows 7 or later:</b> This value is supported.
        /// </para>
        /// </summary>
        IkeV2

#endif
    }
}