//--------------------------------------------------------------------------
// <copyright file="RasPhoneBookType.cs" company="Jeff Winn">
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
    /// Defines the phone book types.
    /// </summary>
    public enum RasPhoneBookType
    {
        /// <summary>
        /// The phone book is in a custom location.
        /// </summary>
        Custom = -1,

        /// <summary>
        /// The phone book is in the user's profile.
        /// </summary>
        User = 0,

        /// <summary>
        /// The phone book is a system phone book and is in the All Users profile.
        /// </summary>
        AllUsers = 1
    }
}