//--------------------------------------------------------------------------
// <copyright file="RasPreSharedKey.cs" company="Jeff Winn">
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
#if (WINXP || WIN2K8 || WIN7 || WIN8)

    /// <summary>
    /// Defines the pre-shared keys.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows XP and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    public enum RasPreSharedKey
    {
        /// <summary>
        /// The client pre-shared key.
        /// </summary>
        Client,

        /// <summary>
        /// The server pre-shared key.
        /// </summary>
        Server,

        /// <summary>
        /// The demand-dial interface (DDI) pre-shared key.
        /// </summary>
        Ddm
    }

#endif
}