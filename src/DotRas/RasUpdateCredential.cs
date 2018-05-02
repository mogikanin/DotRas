//--------------------------------------------------------------------------
// <copyright file="RasUpdateCredential.cs" company="Jeff Winn">
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
    /// Defines where user credentials can be saved for a phone book.
    /// </summary>
    public enum RasUpdateCredential
    {
        /// <summary>
        /// No credentials should be updated.
        /// </summary>
        None = 0,

        /// <summary>
        /// Update the credentials in the current user profile.
        /// </summary>
        User,

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Update the credentials in the all users profile, if available.
        /// <para>
        /// <b>Windows XP or later:</b> This value is supported.
        /// </para>
        /// </summary>
        AllUsers

#endif
    }
}