//--------------------------------------------------------------------------
// <copyright file="RasPppInfo.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.Net;

#if (WIN7 || WIN8)

    /// <summary>
    /// Contains the result of a Point-to-Point (PPP) projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object is created from a <see cref="RasProjectionType.Ppp"/> projection operation on a connection.
    /// </para>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows 7 and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to perform a PPP projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasPppInfo info = (RasPppInfo)connection.GetProjectionInfo(RasProjectionType.Ppp);
    ///     if (info != null)
    ///     {
    ///         // info now contains the PPP projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasPppInfo = CType(connection.GetProjectionInfo(RasProjectionType.Ppp), RasPppInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the PPP projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [PublicAPI]
    public sealed class RasPppInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasPppInfo"/> class.
        /// </summary>
        /// <param name="ipv4NegotiationErrorCode">The result of a PPP IPv4 network control protocol negotiation.</param>
        /// <param name="ipAddress">The IP address of the local client.</param>
        /// <param name="serverIPAddress">The IP address of the remote server.</param>
        /// <param name="options">The Internet Protocol Control Protocol (IPCP) options for the local client.</param>
        /// <param name="serverOptions">The Internet Protocol Control Protocol (IPCP) options for the remote server.</param>
        /// <param name="ipv6NegotiationErrorCode">The result of a PPP IPv6 network control protocol negotiation.</param>
        /// <param name="interfaceIdentifier">The 64-bit IPv6 interface identifier of the client.</param>
        /// <param name="serverInterfaceIdentifier">the 64-bit IPv6 interface identifier of the server.</param>
        /// <param name="isBundled"><b>true</b> if the connection is composed of multiple links, otherwise <b>false</b>.</param>
        /// <param name="isMultilink"><b>true</b> if the connection supports multiple links, otherwise <b>false</b>.</param>
        /// <param name="authenticationProtocol">The authentication protocol used to authenticate the local client.</param>
        /// <param name="authenticationData">The authentication data used by the local client.</param>
        /// <param name="serverAuthenticationProtocol">The authentication protocol used by the remote server.</param>
        /// <param name="serverAuthenticationData">The authentication data used by the remote server.</param>
        /// <param name="eapTypeId">The type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the local client.</param>
        /// <param name="serverEapTypeId">The type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the remote server.</param>
        /// <param name="lcpOptions">The Link Control Protocol (LCP) options in use by the local client.</param>
        /// <param name="serverLcpOptions">The Link Control Protocol (LCP) options in use by the remote server.</param>
        /// <param name="ccpCompressionAlgorithm">The compression algorithm in use by the local client.</param>
        /// <param name="serverCcpCompressionAlgorithm">The compression algorithm in use by the remote server.</param>
        /// <param name="ccpOptions">The compression options on the local client.</param>
        /// <param name="serverCcpOptions">The compression options on the remote server.</param>
        internal RasPppInfo(int ipv4NegotiationErrorCode, IPAddress ipAddress, IPAddress serverIPAddress, RasIPOptions options, RasIPOptions serverOptions, int ipv6NegotiationErrorCode, ReadOnlyCollection<byte> interfaceIdentifier, ReadOnlyCollection<byte> serverInterfaceIdentifier, bool isBundled, bool isMultilink, RasLcpAuthenticationType authenticationProtocol, RasLcpAuthenticationDataType authenticationData, RasLcpAuthenticationType serverAuthenticationProtocol, RasLcpAuthenticationDataType serverAuthenticationData, int eapTypeId, int serverEapTypeId, RasLcpOptions lcpOptions, RasLcpOptions serverLcpOptions, RasCompressionType ccpCompressionAlgorithm, RasCompressionType serverCcpCompressionAlgorithm, RasCompressionOptions ccpOptions, RasCompressionOptions serverCcpOptions)
        {
            IPv4NegotiationErrorCode = ipv4NegotiationErrorCode;
            IPAddress = ipAddress;
            ServerIPAddress = serverIPAddress;
            Options = options;
            ServerOptions = serverOptions;
            IPv6NegotiationErrorCode = ipv6NegotiationErrorCode;
            InterfaceIdentifier = interfaceIdentifier;
            ServerInterfaceIdentifier = serverInterfaceIdentifier;
            IsBundled = isBundled;
            IsMultilink = isMultilink;
            AuthenticationProtocol = authenticationProtocol;
            AuthenticationData = authenticationData;
            ServerAuthenticationProtocol = serverAuthenticationProtocol;
            ServerAuthenticationData = serverAuthenticationData;
            EapTypeId = eapTypeId;
            ServerEapTypeId = serverEapTypeId;
            LcpOptions = lcpOptions;
            ServerLcpOptions = serverLcpOptions;
            CcpCompressionAlgorithm = ccpCompressionAlgorithm;
            ServerCcpCompressionAlgorithm = serverCcpCompressionAlgorithm;
            CcpOptions = ccpOptions;
            ServerCcpOptions = serverCcpOptions;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of a PPP IPv4 network control protocol negotiation.
        /// </summary>
        public int IPv4NegotiationErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the IP address of the local client.
        /// </summary>
        public IPAddress IPAddress
        {
            get;
        }

        /// <summary>
        /// Gets the IP address of the remote server.
        /// </summary>
        public IPAddress ServerIPAddress
        {
            get;
        }

        /// <summary>
        /// Gets the Internet Protocol Control Protocol (IPCP) options for the local client.
        /// </summary>
        public RasIPOptions Options
        {
            get;
        }

        /// <summary>
        /// Gets the Internet Protocol Control Protocol (IPCP) options for the remote server.
        /// </summary>
        public RasIPOptions ServerOptions
        {
            get;
        }

        /// <summary>
        /// Gets the result of a PPP IPv6 network control protocol negotiation.
        /// </summary>
        public int IPv6NegotiationErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the 64-bit IPv6 interface identifier of the client.
        /// </summary>
        public ReadOnlyCollection<byte> InterfaceIdentifier
        {
            get;
        }

        /// <summary>
        /// Gets the 64-bit IPv6 interface identifier of the server.
        /// </summary>
        public ReadOnlyCollection<byte> ServerInterfaceIdentifier
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the connection is composed of multiple links.
        /// </summary>
        public bool IsBundled
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether the connection supports multiple links.
        /// </summary>
        public bool IsMultilink
        {
            get;
        }

        /// <summary>
        /// Gets the authentication protocol used to authenticate the local client.
        /// </summary>
        public RasLcpAuthenticationType AuthenticationProtocol
        {
            get;
        }

        /// <summary>
        /// Gets the authentication data used by the local client.
        /// </summary>
        public RasLcpAuthenticationDataType AuthenticationData
        {
            get;
        }

        /// <summary>
        /// Gets the authentication protocol used by the remote server.
        /// </summary>
        public RasLcpAuthenticationType ServerAuthenticationProtocol
        {
            get;
        }

        /// <summary>
        /// Gets the authentication data used by the remote server.
        /// </summary>
        public RasLcpAuthenticationDataType ServerAuthenticationData
        {
            get;
        }

        /// <summary>
        /// Gets the type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the local client.
        /// </summary>
        public int EapTypeId
        {
            get;
        }

        /// <summary>
        /// Gets the type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the remote server.
        /// </summary>
        public int ServerEapTypeId
        {
            get;
        }

        /// <summary>
        /// Gets the Link Control Protocol (LCP) options in use by the local client.
        /// </summary>
        public RasLcpOptions LcpOptions
        {
            get;
        }

        /// <summary>
        /// Gets the Link Control Protocol (LCP) options in use by the remote server.
        /// </summary>
        public RasLcpOptions ServerLcpOptions
        {
            get;
        }

        /// <summary>
        /// Gets the compression algorithm in use by the local client.
        /// </summary>
        public RasCompressionType CcpCompressionAlgorithm
        {
            get;
        }

        /// <summary>
        /// Gets the compression algorithm in use by the remote server.
        /// </summary>
        public RasCompressionType ServerCcpCompressionAlgorithm
        {
            get;
        }

        /// <summary>
        /// Gets the compression options on the local client.
        /// </summary>
        public RasCompressionOptions CcpOptions
        {
            get;
        }

        /// <summary>
        /// Gets the compression options on the remote server.
        /// </summary>
        public RasCompressionOptions ServerCcpOptions
        {
            get;
        }

        #endregion
    }

#endif
}