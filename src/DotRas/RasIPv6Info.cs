//--------------------------------------------------------------------------
// <copyright file="RasIPv6Info.cs" company="Jeff Winn">
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
    using System.Net;

#if (WIN2K8 || WIN7 || WIN8)

    /// <summary>
    /// Contains the result of an IPv6 projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>    
    /// This object is created from an <see cref="RasProjectionType.IPv6"/> projection operation on a connection.
    /// </para>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows Vista and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// This example shows how to perform an IPv6 projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasIPv6Info info = (RasIPv6Info)connection.GetProjectionInfo(RasProjectionType.IPv6);
    ///     if (info != null)
    ///     {
    ///         // info now contains the IPv6 projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasIPv6Info = CType(connection.GetProjectionInfo(RasProjectionType.IPv6), RasIPv6Info)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the IPv6 projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasIPv6Info
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIPv6Info"/> class.
        /// </summary>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="localInterfaceIdentifier">The local 64-bit IPv6 interface identifier.</param>
        /// <param name="peerInterfaceIdentifier">The remote 64-bit IPv6 interface identifier.</param>
        /// <param name="localCompressionProtocol">The local compression protocol.</param>
        /// <param name="peerCompressionProtocol">The remote compression protocol.</param>
        internal RasIPv6Info(int errorCode, long localInterfaceIdentifier, long peerInterfaceIdentifier, short localCompressionProtocol, short peerCompressionProtocol)
        {
            ErrorCode = errorCode;
            LocalInterfaceIdentifier = localInterfaceIdentifier;
            PeerInterfaceIdentifier = peerInterfaceIdentifier;
            LocalCompressionProtocol = localCompressionProtocol;
            PeerCompressionProtocol = peerCompressionProtocol;
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
            private set;
        }

        /// <summary>
        /// Gets the local 64-bit IPv6 interface identifier.
        /// </summary>
        public long LocalInterfaceIdentifier
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the remote 64-bit IPv6 interface identifier.
        /// </summary>
        public long PeerInterfaceIdentifier
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the local compression protocol.
        /// </summary>
        /// <remarks>Reserved for future use.</remarks>
        public short LocalCompressionProtocol
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the remote compression protocol.
        /// </summary>
        /// <remarks>Reserved for future use.</remarks>
        public short PeerCompressionProtocol
        {
            get;
            private set;
        }

        #endregion
    }

#endif
}