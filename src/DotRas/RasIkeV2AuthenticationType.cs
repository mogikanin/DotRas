//--------------------------------------------------------------------------
// <copyright file="RasIkeV2AuthenticationType.cs" company="Jeff Winn">
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
#if (WIN7 || WIN8)

    /// <summary>
    /// Defines the Internet Key Exchange (IKEv2) authentication types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows 7 and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    public enum RasIkeV2AuthenticationType
    {
        /// <summary>
        /// No authentication.
        /// </summary>
        None = 0,

        /// <summary>
        /// X.509 Public Key Infrastructure Certificate.
        /// </summary>
        X509Certificate = 1,

        /// <summary>
        /// Extensible Authentication Protocol (EAP).
        /// </summary>
        Eap = 2
    }

#endif
}