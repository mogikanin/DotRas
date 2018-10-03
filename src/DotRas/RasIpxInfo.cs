//--------------------------------------------------------------------------
// <copyright file="RasIpxInfo.cs" company="Jeff Winn">
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
    /// Contains the result of an IPX projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>This object is created from an <see cref="RasProjectionType.Ipx"/> projection operation on a connection.</remarks>
    /// <example>
    /// This example shows how to perform an IPX projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasIpxInfo info = (RasIpxInfo)connection.GetProjectionInfo(RasProjectionType.Ipx);
    ///     if (info != null)
    ///     {
    ///         // info now contains the IPX projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasIpxInfo = CType(connection.GetProjectionInfo(RasProjectionType.Ipx), RasIpxInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the IPX projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasIpxInfo
    {
        #region Constructors
 
        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIpxInfo"/> class.
        /// </summary>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="ipxAddress">The client IP address on the connection.</param>
        internal RasIpxInfo(int errorCode, IPAddress ipxAddress)
        {
            ErrorCode = errorCode;
            IpxAddress = ipxAddress;
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
        /// Gets the client IP address on the connection.
        /// </summary>
        public IPAddress IpxAddress
        {
            get;
            private set;
        }

        #endregion
    }
}