//--------------------------------------------------------------------------
// <copyright file="RasConnectionSubState.cs" company="Jeff Winn">
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
    using Internal;

#if (WIN7 || WIN8)

    /// <summary>
    /// Defines the states for Internet Key Exchange version 2 (IKEv2) virtual private network (VPN) tunnel connections.
    /// </summary>
    /// <remarks>These states are not available to other tunneling protocols.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "Values are defined in the Windows SDK.")]
    public enum RasConnectionSubState
    {
        /// <summary>
        /// The connection state does not have a sub-state.
        /// </summary>
        None = 0,

        /// <summary>
        /// The underlying internet interface of the connection is down and the connection is waiting for an internet interface to come online.
        /// </summary>
        Dormant,

        /// <summary>
        /// The internet interface has come online and the connection is switching over to this new interface.
        /// </summary>
        Reconnecting,

        /// <summary>
        /// The connection has switched over successfully to the new internet interface.
        /// </summary>
        Reconnected = NativeMethods.RASCSS_DONE
    }

#endif
}