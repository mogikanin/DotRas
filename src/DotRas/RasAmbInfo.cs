//--------------------------------------------------------------------------
// <copyright file="RasAmbInfo.cs" company="Jeff Winn">
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
    /// Contains the results of a Authentication Message Block (AMB) projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>This object is created from an <see cref="RasProjectionType.Amb"/> projection operation on a connection.</remarks>
    /// <example>
    /// This example shows how to perform an AMB projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasAmbInfo info = (RasAmbInfo)connection.GetProjectionInfo(RasProjectionType.Amb);
    ///     if (info != null)
    ///     {
    ///         // info now contains the AMB projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasAmbInfo = CType(connection.GetProjectionInfo(RasProjectionType.Amb), RasAmbInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the AMB projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasAmbInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAmbInfo"/> class.
        /// </summary>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="netBiosErrorMessage">The NetBIOS error message (if applicable).</param>
        /// <param name="lana">The NetBIOS network adapter identifier on which the remote access connection was established.</param>
        internal RasAmbInfo(int errorCode, string netBiosErrorMessage, byte lana)
        {
            ErrorCode = errorCode;
            NetBiosErrorMessage = netBiosErrorMessage;
            Lana = lana;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the error code (if any) that occurred.
        /// </summary>
        /// <remarks>This member indicates the actual fatal error (if any) that occurred during the control protocol negotiation, the error that prevented the projection from completing successfully.</remarks>
        public int ErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the NetBIOS error message (if applicable).
        /// </summary>
        public string NetBiosErrorMessage
        {
            get;
        }

        /// <summary>
        /// Gets the NetBIOS network adapter identifier on which the remote access connection was established.
        /// </summary>
        /// <remarks>This member contains <see cref="Byte.MaxValue"/> if a connection was not established.</remarks>
        public byte Lana
        {
            get;
        }

        #endregion
    }
}