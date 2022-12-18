//--------------------------------------------------------------------------
// <copyright file="RasIPInfo.cs" company="Jeff Winn">
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

    /// <summary>
    /// Contains the result of an IP projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>This object is created from an <see cref="RasProjectionType.IP"/> projection operation on a connection.</remarks>
    /// <example>
    /// This example shows how to perform an IP projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasIPInfo info = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
    ///     if (info != null)
    ///     {
    ///         // info now contains the IP projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasIPInfo = CType(connection.GetProjectionInfo(RasProjectionType.IP), RasIPInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the IP projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    
    public sealed class RasIPInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIPInfo"/> class.
        /// </summary>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="ipAddress">The client IP address.</param>
        /// <param name="serverIPAddress">The server IP address.</param>
        /// <param name="options">The IPCP options for the local computer.</param>
        /// <param name="serverOptions">The IPCP options for the remote computer.</param>
        internal RasIPInfo(int errorCode, IPAddress ipAddress, IPAddress serverIPAddress, RasIPOptions options, RasIPOptions serverOptions)
        {
            ErrorCode = errorCode;
            IPAddress = ipAddress;
            ServerIPAddress = serverIPAddress;
            Options = options;
            ServerOptions = serverOptions;
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
        /// Gets the client IP address.
        /// </summary>
        public IPAddress IPAddress
        {
            get;
        }

        /// <summary>
        /// Gets the server IP address.
        /// </summary>
        public IPAddress ServerIPAddress
        {
            get;
        }

        /// <summary>
        /// Gets the IPCP options for the local computer.
        /// </summary>
        public RasIPOptions Options
        {
            get;
        }

        /// <summary>
        /// Gets the IPCP options for the remote computer.
        /// </summary>
        public RasIPOptions ServerOptions
        {
            get;
        }

        #endregion
    }
}