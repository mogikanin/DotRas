//--------------------------------------------------------------------------
// <copyright file="RasDialMode.cs" company="Jeff Winn">
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
    /// Defines the dial modes.
    /// </summary>
    public enum RasDialMode
    {
        /// <summary>
        /// No dial mode specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Dial all subentries.
        /// </summary>
        DialAll = 1,

        /// <summary>
        /// Dial the number of subentries as additional bandwidth is needed.
        /// </summary>
        DialAsNeeded = 2
    }
}