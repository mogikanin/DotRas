//--------------------------------------------------------------------------
// <copyright file="RasNapStatus.cs" company="Jeff Winn">
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
    using System;

#if (WIN2K8 || WIN7 || WIN8)

    /// <summary>
    /// Represents the current network access protection (NAP) status of a remote access connection. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows Vista and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to retrieve the NAP status for an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasNapStatus info = connection.GetNapStatus();
    ///     if (info != null)
    ///     {
    ///         // info now has the NAP status information.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasNapStatus = connection.GetNapStatus()
    ///     If info IsNot Nothing Then
    ///         ' info now has the NAP status information.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [PublicAPI]
    public sealed class RasNapStatus
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasNapStatus"/> class.
        /// </summary>
        /// <param name="isolationState">The isolation state for the remote access connection.</param>
        /// <param name="probationTime">The time required for the connection to come out of quarantine.</param>
        internal RasNapStatus(RasIsolationState isolationState, DateTime probationTime)
        {
            IsolationState = isolationState;
            ProbationTime = probationTime;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the isolation state.
        /// </summary>
        public RasIsolationState IsolationState
        {
            get;
        }

        /// <summary>
        /// Gets the probation time.
        /// </summary>
        /// <remarks>Specifies the time required for the connection to come out of quarantine after which the connection will be dropped.</remarks>
        public DateTime ProbationTime
        {
            get;
        }

        #endregion
    }

#endif
}