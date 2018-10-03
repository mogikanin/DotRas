//--------------------------------------------------------------------------
// <copyright file="RasLcpInfo.cs" company="Jeff Winn">
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
    /// Contains the result of a Link Control Protocol (LCP) multilink projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>This object is created from an <see cref="RasProjectionType.Lcp"/> projection operation on a connection.</remarks>
    /// <example>
    /// This example shows how to perform an LCP projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasLcpInfo info = (RasLcpInfo)connection.GetProjectionInfo(RasProjectionType.Lcp);
    ///     if (info != null)
    ///     {
    ///         // info now contains the LCP projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasLcpInfo = CType(connection.GetProjectionInfo(RasProjectionType.Lcp), RasLcpInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the LCP projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasLcpInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasLcpInfo"/> class.
        /// </summary>
        /// <param name="bundled"><b>true</b> if the connection is composed of multiple links, otherwise <b>false</b>.</param>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="authenticationProtocol">The authentication protocol used to authenticate the client.</param>
        /// <param name="authenticationData">The authentication data about the authentication protocol used by the client.</param>
        /// <param name="eapTypeId">The type id of the Extensible Authentication Protocol (EAP) used to authenticate the local computer.</param>
        /// <param name="serverAuthenticatonProtocol">The authentication protocol used to authenticate the server.</param>
        /// <param name="serverAuthenticationData">The authentication data about the authentication protocol used by the server.</param>
        /// <param name="serverEapTypeId">The type id of the Extensible Authentication Protocol (EAP) used to authenticate the remote computer.</param>
        /// <param name="multilink"><b>true</b> if the connection supports multilink, otherwise <b>false</b>.</param>
        /// <param name="terminateReason">The reason the client terminated the connection.</param>
        /// <param name="serverTerminateReason">The reason the server terminated the connection.</param>
        /// <param name="replyMessage">The message (if any) from the authentication protocol success/failure packet.</param>
        /// <param name="options">The additional options for the local computer.</param>
        /// <param name="serverOptions">The additional options for the remote computer.</param>
        internal RasLcpInfo(bool bundled, int errorCode, RasLcpAuthenticationType authenticationProtocol, RasLcpAuthenticationDataType authenticationData, int eapTypeId, RasLcpAuthenticationType serverAuthenticatonProtocol, RasLcpAuthenticationDataType serverAuthenticationData, int serverEapTypeId, bool multilink, int terminateReason, int serverTerminateReason, string replyMessage, RasLcpOptions options, RasLcpOptions serverOptions)
        {
            Bundled = bundled;
            ErrorCode = errorCode;
            AuthenticationProtocol = authenticationProtocol;
            AuthenticationData = authenticationData;
            EapTypeId = eapTypeId;
            ServerAuthenticationProtocol = serverAuthenticatonProtocol;
            ServerAuthenticationData = serverAuthenticationData;
            ServerEapTypeId = serverEapTypeId;
            Multilink = multilink;
            TerminateReason = terminateReason;
            ServerTerminateReason = serverTerminateReason;
            ReplyMessage = replyMessage;
            Options = options;
            ServerOptions = serverOptions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the connection is composed of multiple links.
        /// </summary>
        public bool Bundled
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error code (if any) that occurred.
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication protocol used to authenticate the client.
        /// </summary>
        public RasLcpAuthenticationType AuthenticationProtocol
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication data about the authentication protocol used by the client.
        /// </summary>
        public RasLcpAuthenticationDataType AuthenticationData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type id of the Extensible Authentication Protocol (EAP) used to authenticate the local computer.
        /// </summary>
        /// <remarks>This member is valid only if <see cref="RasLcpInfo.AuthenticationProtocol"/> is <see cref="RasLcpAuthenticationType.Eap"/>.</remarks>
        public int EapTypeId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication protocol used to authenticate the server.
        /// </summary>
        public RasLcpAuthenticationType ServerAuthenticationProtocol
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication data about the authentication protocol used by the server.
        /// </summary>
        public RasLcpAuthenticationDataType ServerAuthenticationData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type id of the Extensible Authentication Protocol (EAP) used to authenticate the remote computer.
        /// </summary>
        /// <remarks>This member is valid only if <see cref="RasLcpInfo.ServerAuthenticationProtocol"/> is <see cref="RasLcpAuthenticationType.Eap"/>.</remarks>
        public int ServerEapTypeId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether the connection supports multilink.
        /// </summary>
        public bool Multilink
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the reason the client terminated the connection.
        /// </summary>
        /// <remarks>This member always has a return value of zero.</remarks>
        public int TerminateReason
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the reason the server terminated the connection.
        /// </summary>
        /// <remarks>This member always has a return value of zero.</remarks>
        public int ServerTerminateReason
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message (if any) from the authentication protocol success/failure packet.
        /// </summary>
        public string ReplyMessage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the additional options for the local computer.
        /// </summary>
        public RasLcpOptions Options
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the additional options for the remote computer.
        /// </summary>
        public RasLcpOptions ServerOptions
        {
            get;
            private set;
        }

        #endregion
    }
}