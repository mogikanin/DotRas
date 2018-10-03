//--------------------------------------------------------------------------
// <copyright file="RasIsolationState.cs" company="Jeff Winn">
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

using JetBrains.Annotations;

namespace DotRas
{
#if (WIN2K8 || WIN7 || WIN8)

    /// <summary>
    /// Describes the the isolation state of a remote access service (RAS) connection.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows Vista and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    [PublicAPI]
    public enum RasIsolationState
    {
        /// <summary>
        /// The connection isolation state is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The connection isolation state is not restricted.
        /// </summary>
        NotRestricted,

        /// <summary>
        /// The connection isolation state is in probation.
        /// </summary>
        InProbation,

        /// <summary>
        /// The connection isolation state is restricted access.
        /// </summary>
        RestrictedAccess
    }

#endif
}