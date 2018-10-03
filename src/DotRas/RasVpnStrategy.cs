//--------------------------------------------------------------------------
// <copyright file="RasVpnStrategy.cs" company="Jeff Winn">
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
    /// <summary>
    /// Defines the VPN strategies.
    /// </summary>
    [PublicAPI]
    public enum RasVpnStrategy
    {
        /// <summary>
        /// Dials PPTP first. If PPTP fails, L2TP is attempted.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Dial PPTP only.
        /// </summary>
        PptpOnly = 1,

        /// <summary>
        /// Always dial PPTP first.
        /// </summary>
        PptpFirst = 2,

        /// <summary>
        /// Dial L2TP only.
        /// </summary>
        L2tpOnly = 3,

        /// <summary>
        /// Always dial L2TP first.
        /// </summary>
        L2tpFirst = 4,

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Dial SSTP only.
        /// <para>
        /// <b>Windows Vista or later:</b> This value is supported.
        /// </para>
        /// </summary>
        SstpOnly = 5,

        /// <summary>
        /// Always dial SSTP first.
        /// <para>
        /// <b>Windows Vista or later:</b> This value is supported.
        /// </para>
        /// </summary>
        SstpFirst = 6,

#endif

#if (WIN7 || WIN8)

        /// <summary>
        /// Dial IKEv2 only.
        /// <para>
        /// <b>Windows 7 or later:</b> This value is supported.
        /// </para>
        /// </summary>
        IkeV2Only = 7,

        /// <summary>
        /// Dial IKEv2 first.
        /// <para>
        /// <b>Windows 7 or later:</b> This value is supported.
        /// </para>
        /// </summary>
        IkeV2First = 8

#endif
    }
}