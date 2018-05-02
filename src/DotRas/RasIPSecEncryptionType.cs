//--------------------------------------------------------------------------
// <copyright file="RasIPSecEncryptionType.cs" company="Jeff Winn">
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

#if (WIN7 || WIN8)

    /// <summary>
    /// Defines the IPSec encryption types.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows 7 and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    public enum RasIPSecEncryptionType
    {
        /// <summary>
        /// No encryption type specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// DES encryption.
        /// </summary>
        Des = 1,

        /// <summary>
        /// Triple DES encryption.
        /// </summary>
        TripleDes = 2,

        /// <summary>
        /// AES 128-bit encryption.
        /// </summary>
        Aes128 = 3,

        /// <summary>
        /// AES 192-bit encryption.
        /// </summary>
        Aes192 = 4,

        /// <summary>
        /// AES 256-bit encryption.
        /// </summary>
        Aes256 = 5,

        /// <summary>
        /// Maximum encryption.
        /// </summary>
        Max = 6
    }

#endif
}