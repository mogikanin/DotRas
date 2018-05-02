//--------------------------------------------------------------------------
// <copyright file="RasCcpInfo.cs" company="Jeff Winn">
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
    /// Contains the results of a Compression Control Protocol (CCP) projection operation. This class cannot be inherited.
    /// </summary>
    /// <remarks>This object is created from a <see cref="RasProjectionType.Ccp"/> projection operation on a connection.</remarks>
    /// <example>
    /// This example shows how to perform an CCP projection operation on an active connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasConnection connection = RasConnection.GetActiveConnectionByName("My Connection", @"C:\Test.pbk");
    /// if (connection != null)
    /// {
    ///     RasCcpInfo info = (RasCcpInfo)connection.GetProjectionInfo(RasProjectionType.Ccp);
    ///     if (info != null)
    ///     {
    ///         // info now contains the CCP projection data.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim connection As RasConnection = RasConnection.GetActiveConnectionByName("My Connection", "C:\Test.pbk")
    /// If connection IsNot Nothing Then
    ///     Dim info As RasCcpInfo = CType(connection.GetProjectionInfo(RasProjectionType.Ccp), RasCcpInfo)
    ///     If info IsNot Nothing Then
    ///         ' info now contains the CCP projection data.
    ///     End If
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    public sealed class RasCcpInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasCcpInfo"/> class.
        /// </summary>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="compressionAlgorithm">The compression algorithm in use by the client.</param>
        /// <param name="options">The compression options on the client.</param>
        /// <param name="serverCompressionAlgorithm">The compression algorithm in use by the server.</param>
        /// <param name="serverOptions">The compression options on the server.</param>
        internal RasCcpInfo(int errorCode, RasCompressionType compressionAlgorithm, RasCompressionOptions options, RasCompressionType serverCompressionAlgorithm, RasCompressionOptions serverOptions)
        {
            this.ErrorCode = errorCode;
            this.CompressionAlgorithm = compressionAlgorithm;
            this.Options = options;
            this.ServerCompressionAlgorithm = serverCompressionAlgorithm;
            this.ServerOptions = serverOptions;
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
        /// Gets the compression algorithm in use by the client.
        /// </summary>
        public RasCompressionType CompressionAlgorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the compression options on the client.
        /// </summary>
        public RasCompressionOptions Options
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the compression algorithm in use by the server.
        /// </summary>
        public RasCompressionType ServerCompressionAlgorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the compression options on the server.
        /// </summary>
        public RasCompressionOptions ServerOptions
        {
            get;
            private set;
        }

        #endregion
    }
}