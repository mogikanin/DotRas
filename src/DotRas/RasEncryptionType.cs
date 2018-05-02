//--------------------------------------------------------------------------
// <copyright file="RasEncryptionType.cs" company="Jeff Winn">
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
    /// Defines the encryption types.
    /// </summary>
    public enum RasEncryptionType
    {
        /// <summary>
        /// No encryption type specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Require encryption.
        /// </summary>
        Require = 1,

        /// <summary>
        /// Require maximum encryption.
        /// </summary>
        RequireMax = 2,

        /// <summary>
        /// Use encryption if available.
        /// </summary>
        Optional = 3
    }
}