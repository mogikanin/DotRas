//--------------------------------------------------------------------------
// <copyright file="RasLcpAuthenticationType.cs" company="Jeff Winn">
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
    /// Defines the Link Control Protocol (LCP) authentication protocol types.
    /// </summary>
    
    public enum RasLcpAuthenticationType
    {
        /// <summary>
        /// No authentication protocol used.
        /// </summary>
        None = 0,

        /// <summary>
        /// Password Authentication Protocol.
        /// </summary>
        Pap = 0xC023,

        /// <summary>
        /// Shiva Password Authentication Protocol.
        /// </summary>
        Spap = 0xC027,

        /// <summary>
        /// Challenge Handshake Authentication Protocol.
        /// </summary>
        Chap = 0xC223,

        /// <summary>
        /// Extensible Authentication Protocol.
        /// </summary>
        Eap = 0xC227
    }
}