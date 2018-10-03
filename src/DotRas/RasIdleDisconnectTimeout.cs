//--------------------------------------------------------------------------
// <copyright file="RasIdleDisconnectTimeout.cs" company="Jeff Winn">
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
    /// Contains constants used to specify remote access service (RAS) idle disconnect timeouts.
    /// </summary>
    public static class RasIdleDisconnectTimeout
    {
        /// <summary>
        /// The idle timeout is the default value.
        /// </summary>
        public const int Default = 0;

        /// <summary>
        /// There is no idle timeout for this connection.
        /// </summary>
        public const int Disabled = int.MaxValue;
    }
}