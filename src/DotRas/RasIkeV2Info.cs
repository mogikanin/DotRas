//--------------------------------------------------------------------------
// <copyright file="RasIkeV2Info.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.Net;

#if (WIN7 || WIN8)

    /// <summary>
    /// Contains the result of an Internet Key Exchange (IKEv2) projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This object is created from an <see cref="RasProjectionType.IkeV2"/> projection operation on a connection.
    /// </para>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows 7 and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to perform an IKEv2 projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasIkeV2Info info = (RasIkeV2Info)connection.GetProjectionInfo(RasProjectionType.IkeV2);
    ///     if (info != null)
    ///     {
    ///         // info now contains the IKEv2 projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasIkeV2Info = CType(connection.GetProjectionInfo(RasProjectionType.IkeV2), RasIkeV2Info)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the IKEv2 projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasIkeV2Info
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIkeV2Info"/> class.
        /// </summary>
        /// <param name="ipv4NegotiationErrorCode">The result of a PPP IPv4 network control protocol negotiation.</param>
        /// <param name="ipv4Address">The IP address of the local client.</param>
        /// <param name="serverIPv4Address">The IP address of the remote server.</param>
        /// <param name="ipv6NegotiationErrorCode">The result of a PPP IPv6 network control protocol negotiation.</param>
        /// <param name="ipv6Address">The IPv6 address of the local client.</param>
        /// <param name="serverIPv6Address">The IPv6 address of the remote server.</param>
        /// <param name="prefixLength">The length of the IPv6 prefix.</param>
        /// <param name="authenticationProtocol">The authentication protocol used to authenticate the remote server.</param>
        /// <param name="eapTypeId">The type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the client.</param>
        /// <param name="options">The IKEv2 options.</param>
        /// <param name="encryptionMethod">The encryption method used by the connection.</param>
        /// <param name="serverIPv4Addresses">The collection of available IPv4 addresses the server can switch to on the IKEv2 connection.</param>
        /// <param name="serverIPv6Addresses">The collection of available IPv6 addresses the server can switch to on the IKEv2 connection.</param>
        internal RasIkeV2Info(int ipv4NegotiationErrorCode, IPAddress ipv4Address, IPAddress serverIPv4Address, int ipv6NegotiationErrorCode, IPAddress ipv6Address, IPAddress serverIPv6Address, int prefixLength, RasIkeV2AuthenticationType authenticationProtocol, int eapTypeId, RasIkeV2Options options, RasIPSecEncryptionType encryptionMethod, Collection<IPAddress> serverIPv4Addresses, Collection<IPAddress> serverIPv6Addresses)
        {
            this.IPv4NegotiationErrorCode = ipv4NegotiationErrorCode;
            this.IPv4Address = ipv4Address;
            this.ServerIPv4Address = serverIPv4Address;
            this.IPv6NegotiationErrorCode = ipv6NegotiationErrorCode;
            this.IPv6Address = ipv6Address;
            this.ServerIPv6Address = serverIPv6Address;
            this.PrefixLength = prefixLength;
            this.AuthenticationProtocol = authenticationProtocol;
            this.EapTypeId = eapTypeId;
            this.Options = options;
            this.EncryptionMethod = encryptionMethod;
            this.ServerIPv4Addresses = serverIPv4Addresses;
            this.ServerIPv6Addresses = serverIPv6Addresses;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of a PPP IPv4 network control protocol negotiation.
        /// </summary>
        public int IPv4NegotiationErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the IPv4 address of the local client.
        /// </summary>
        public IPAddress IPv4Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the IPv4 address of the remote server.
        /// </summary>
        public IPAddress ServerIPv4Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the result of a PPP IPv6 network control protocol negotiation.
        /// </summary>
        public int IPv6NegotiationErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the IPv6 address of the local client.
        /// </summary>
        public IPAddress IPv6Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the IPv6 address of the remote server.
        /// </summary>
        public IPAddress ServerIPv6Address
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the length of the IPv6 prefix.
        /// </summary>
        public int PrefixLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the authentication protocol used to authenticate the remote server.
        /// </summary>
        public RasIkeV2AuthenticationType AuthenticationProtocol
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type identifier of the Extensible Authentication Protocol (EAP) used to authenticate the client.
        /// </summary>
        /// <remarks>This member is only valid if <see cref="RasIkeV2Info.AuthenticationProtocol"/> is <see cref="RasIkeV2AuthenticationType.Eap"/>.</remarks>
        public int EapTypeId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the IKEv2 options.
        /// </summary>
        public RasIkeV2Options Options
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the encryption method used by the connection.
        /// </summary>
        public RasIPSecEncryptionType EncryptionMethod
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of available IPv4 addresses the server can switch to on the IKEv2 connection.
        /// </summary>
        public Collection<IPAddress> ServerIPv4Addresses
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of available IPv6 addresses the server can switch to on the IKEv2 connection.
        /// </summary>
        public Collection<IPAddress> ServerIPv6Addresses
        {
            get;
            private set;
        }

        #endregion
    }

#endif
}