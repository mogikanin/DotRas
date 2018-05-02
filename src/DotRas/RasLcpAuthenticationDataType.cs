//--------------------------------------------------------------------------
// <copyright file="RasLcpAuthenticationDataType.cs" company="Jeff Winn">
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
    /// Defines the Link Control Protocol (LCP) authentication data types.
    /// </summary>
    public enum RasLcpAuthenticationDataType
    {
        /// <summary>
        /// No authentication data used.
        /// </summary>
        None = 0,

        /// <summary>
        /// MD5 Challenge Handshake Authentication Protocol.
        /// </summary>
        MD5Chap = 0x05,

        /// <summary>
        /// Challenge Handshake Authentication Protocol.
        /// </summary>
        MSChap = 0x80,

        /// <summary>
        /// Challenge Handshake Authentication Protocol version 2.
        /// </summary>
        MSChap2 = 0x81
    }
}