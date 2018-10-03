//--------------------------------------------------------------------------
// <copyright file="SafeNativeMethods.cs" company="Jeff Winn">
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

namespace DotRas.Internal
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;
    using Diagnostics;

    /// <summary>
    /// Contains the safe remote access service (RAS) API function declarations.
    /// </summary>
    internal class SafeNativeMethods : ISafeNativeMethods
    {
        #region Fields

        /// <summary>
        /// Contains the instance used to handle calls.
        /// </summary>
        private static ISafeNativeMethods instance;

        #endregion

        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the instance of the <see cref="ISafeNativeMethods"/> class to handle calls.
        /// </summary>
        public static ISafeNativeMethods Instance
        {
            get => instance ?? (instance = new SafeNativeMethods());
            set => instance = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Allocates a new locally unique identifier.
        /// </summary>
        /// <param name="pLuid">Pointer to a <see cref="DotRas.Luid"/> structure that upon return, receives the generated LUID instance.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int AllocateLocallyUniqueIdImpl(IntPtr pLuid)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.AdvApi32Dll, "AllocateLocallyUniqueId");
            evt.Data.Add("pLuid", pLuid);

            var retval = 0;

            try
            {
                var ret = AllocateLocallyUniqueId(pLuid);
                if (!ret)
                {
                    retval = Marshal.GetLastWin32Error();
                    evt.ResultCode = retval;
                }
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }            

            return retval;
        }

        /// <summary>
        /// Clears any accumulated statistics for the specified RAS connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int ClearConnectionStatistics(RasHandle handle)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasClearConnectionStatistics");
            evt.Data.Add("handle", handle);

            var result = 0;

            try
            {
                result = RasClearConnectionStatistics(handle);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Clears any accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to clear statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int ClearLinkStatistics(RasHandle handle, int subEntryId)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasClearLinkStatistics");
            evt.Data.Add("handle", handle);
            evt.Data.Add("subEntryId", subEntryId);

            var result = 0;

            try
            {
                result = RasClearLinkStatistics(handle, subEntryId);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Establishes a remote access connection between a client and a server.
        /// </summary>
        /// <param name="extensions">Pointer to a <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure containing extended feature information.</param>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="dialParameters">Pointer to a <see cref="NativeMethods.RASDIALPARAMS"/> structure containing calling parameters for the connection.</param>
        /// <param name="notifierType">Specifies the nature of the <paramref name="notifier"/> argument. If <paramref name="notifier"/> is null (<b>Nothing</b> in Visual Basic) this argument is ignored.</param>
        /// <param name="notifier">Specifies the callback used during the dialing process.</param>
        /// <param name="handle">Upon return, contains the handle to the RAS connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int Dial(IntPtr extensions, string phoneBookPath, IntPtr dialParameters, NativeMethods.RasNotifierType notifierType, Delegate notifier, out RasHandle handle)
        {
            handle = null;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasDial");
            evt.Data.Add("extensions", extensions);
            evt.Data.Add("phoneBookPath", phoneBookPath);
            evt.Data.Add("dialParameters", dialParameters);
            evt.Data.Add("notifierType", notifierType);
            evt.Data.Add("notifier", notifier);
            evt.Data.Add("handle-IN", handle);

            var result = 0;

            try
            {
                result = RasDial(extensions, phoneBookPath, dialParameters, notifierType, notifier, out handle);

                evt.ResultCode = result;
                evt.Data.Add("handle-OUT", handle);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Frees all system resources associated with an object.
        /// </summary>
        /// <param name="handle">The handle to the object.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        public bool FreeObject(IntPtr handle)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.Gdi32Dll, "DeleteObject");
            evt.Data.Add("handle", handle);

            var result = false;

            try
            {
                result = DeleteObject(handle);
                evt.ResultCode = result ? 1 : 0;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Lists all active remote access service (RAS) connections.
        /// </summary>
        /// <param name="value">An <see cref="StructBufferedPInvokeParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public int EnumConnections(StructBufferedPInvokeParams value)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            var bufferSize = value.BufferSize;
            var count = value.Count;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasEnumConnections");
            evt.Data.Add("address", value.Address);
            evt.Data.Add("bufferSize-IN", bufferSize);
            evt.Data.Add("count-IN", count);

            var result = 0;

            try
            {
                result = RasEnumConnections(value.Address, ref bufferSize, ref count);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);
                evt.Data.Add("count-OUT", count);

                value.BufferSize = bufferSize;
                value.Count = count;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Lists all available remote access capable devices.
        /// </summary>
        /// <param name="value">An <see cref="StructBufferedPInvokeParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public int EnumDevices(StructBufferedPInvokeParams value)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            var bufferSize = value.BufferSize;
            var count = value.Count;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasEnumDevices");
            evt.Data.Add("address", value.Address);
            evt.Data.Add("bufferSize-IN", bufferSize);
            evt.Data.Add("count-IN", count);

            var result = 0;

            try
            {
                result = RasEnumDevices(value.Address, ref bufferSize, ref count);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);
                evt.Data.Add("count-OUT", count);

                value.BufferSize = bufferSize;
                value.Count = count;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Frees the memory buffer returned by the <see cref="SafeNativeMethods.RasGetEapUserIdentity"/> method.
        /// </summary>
        /// <param name="identity">Pointer to the <see cref="NativeMethods.RASEAPUSERIDENTITY"/> structure.</param>
        public void FreeEapUserIdentity(IntPtr identity)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasFreeEapUserIdentity");
            evt.Data.Add("identity", identity);

            try
            {
                RasFreeEapUserIdentity(identity);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }
        }

        /// <summary>
        /// Retrieves accumulated statistics for the specified connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="statistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetConnectionStatistics(RasHandle handle, IntPtr statistics)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetConnectionStatistics");
            evt.Data.Add("handle", handle);
            evt.Data.Add("statistics", statistics);

            var result = 0;

            try
            {
                result = RasGetConnectionStatistics(handle, statistics);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Retrieves information on the current status of the specified remote access connection handle.
        /// </summary>
        /// <param name="handle">The handle to check.</param>
        /// <param name="connectionStatus">Pointer to a <see cref="NativeMethods.RASCONNSTATUS"/> structure that upon return contains the status information for the handle specified by <paramref name="handle"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetConnectStatus(RasHandle handle, IntPtr connectionStatus)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetConnectStatus");
            evt.Data.Add("handle", handle);
            evt.Data.Add("connectionStatus", connectionStatus);

            var result = 0;

            try
            {
                result = RasGetConnectStatus(handle, connectionStatus);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows telephony list of countries/regions.
        /// </summary>
        /// <param name="countries">Pointer to a <see cref="NativeMethods.RASCTRYINFO"/> structure that upon output receives the country/region dialing information.</param>
        /// <param name="bufferSize">Pointer to a variable that, on input, specifies the size, in bytes, of the buffer pointed to by <paramref name="countries"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetCountryInfo(IntPtr countries, ref IntPtr bufferSize)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetCountryInfo");
            evt.Data.Add("countries", countries);
            evt.Data.Add("bufferSize-IN", bufferSize);

            var result = 0;

            try
            {
                result = RasGetCountryInfo(countries, ref bufferSize);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Retrieves connection specific authentication information.
        /// </summary>
        /// <param name="value">An <see cref="RasGetCustomAuthDataParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public int GetCustomAuthData(RasGetCustomAuthDataParams value)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            var bufferSize = value.BufferSize;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetCustomAuthData");
            evt.Data.Add("phoneBookPath", value.PhoneBookPath);
            evt.Data.Add("entryName", value.EntryName);
            evt.Data.Add("address", value.Address);
            evt.Data.Add("bufferSize-IN", bufferSize);

            var result = 0;

            try
            {
                result = RasGetCustomAuthData(value.PhoneBookPath, value.EntryName, value.Address, ref bufferSize);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);

                value.BufferSize = bufferSize;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Retrieves user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry.
        /// </summary>
        /// <param name="value">An <see cref="RasGetEapUserDataParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public int GetEapUserData(RasGetEapUserDataParams value)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            var bufferSize = value.BufferSize;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetEapUserData");
            evt.Data.Add("userToken", value.UserToken);
            evt.Data.Add("phoneBookPath", value.PhoneBookPath);
            evt.Data.Add("entryName", value.EntryName);
            evt.Data.Add("address", value.Address);
            evt.Data.Add("bufferSize-IN", bufferSize);

            var result = 0;

            try
            {
                result = RasGetEapUserData(value.UserToken, value.PhoneBookPath, value.EntryName, value.Address, ref bufferSize);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);

                value.BufferSize = bufferSize;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Retrieves Extensible Authentication Protocol (EAP) identity information for the current user.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="flags">Specifies any flags that qualify the authentication process.</param>
        /// <param name="hwnd">Handle to the parent window for the UI dialog.</param>
        /// <param name="identity">Pointer to a buffer that upon return contains the EAP user identity information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetEapUserIdentity(string phoneBookPath, string entryName, NativeMethods.RASEAPF flags, IntPtr hwnd, ref IntPtr identity)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetEapUserIdentity");
            evt.Data.Add("phoneBookPath", phoneBookPath);
            evt.Data.Add("entryName", entryName);
            evt.Data.Add("flags", flags);
            evt.Data.Add("hwnd", hwnd);
            evt.Data.Add("identity-IN", identity);

            var result = 0;

            try
            {
                result = RasGetEapUserIdentity(phoneBookPath, entryName, flags, hwnd, ref identity);
                evt.ResultCode = result;
                evt.Data.Add("identity-OUT", identity);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Returns an error message string for a specified RAS error value.
        /// </summary>
        /// <param name="errorCode">The error value of interest.</param>
        /// <param name="result">The buffer that will receive the error string.</param>
        /// <param name="bufferSize">Specifies the size, in characters, of the buffer pointed to by <paramref name="result"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetErrorString(int errorCode, string result, int bufferSize)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetErrorString");
            evt.Data.Add("errorCode", errorCode);
            evt.Data.Add("result", result);
            evt.Data.Add("bufferSize", bufferSize);

            var ret = 0;

            try
            {
                ret = RasGetErrorString(errorCode, result, bufferSize);
                evt.ResultCode = ret;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return ret;
        }

        /// <summary>
        /// Retrieves accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to retrieve statistics.</param>
        /// <param name="statistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetLinkStatistics(RasHandle handle, int subEntryId, IntPtr statistics)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetLinkStatistics");
            evt.Data.Add("handle", handle);
            evt.Data.Add("subEntryId", subEntryId);
            evt.Data.Add("statistics", statistics);

            var result = 0;

            try
            {
                result = RasGetLinkStatistics(handle, subEntryId, statistics);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Retrieves the network access protection (NAP) status for a remote access connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="state">Pointer to a <see cref="NativeMethods.RASNAPSTATE"/> structure </param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetNapStatus(RasHandle handle, IntPtr state)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetNapStatus");
            evt.Data.Add("handle", handle);
            evt.Data.Add("state", state);

            var result = 0;

            try
            {
                result = RasGetNapStatus(handle, state);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#endif

        /// <summary>
        /// Obtains information about a remote access projection operation for a specified remote access component protocol.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="projectionType">The <see cref="NativeMethods.RASPROJECTION"/> that identifies the protocol of interest.</param>
        /// <param name="projection">Pointer to a buffer that receives the information.</param>
        /// <param name="bufferSize">On input specifies the size in bytes of the buffer pointed to by <paramref name="projection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetProjectionInfo(RasHandle handle, NativeMethods.RASPROJECTION projectionType, IntPtr projection, ref IntPtr bufferSize)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetProjectionInfo");
            evt.Data.Add("handle", handle);
            evt.Data.Add("projectionType", projectionType);
            evt.Data.Add("projection", projection);
            evt.Data.Add("bufferSize-IN", bufferSize);

            var result = 0;

            try
            {
                result = RasGetProjectionInfo(handle, projectionType, projection, ref bufferSize);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#if (WIN7 || WIN8)

        /// <summary>
        /// Obtains information about a remote access projection operation for all RAS connections on the local client.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="projection">Pointer to a <see cref="NativeMethods.RAS_PROJECTION_INFO"/> structure that receives the projection information for the RAS connections.</param>
        /// <param name="bufferSize">On input specifies the size in bytes of the buffer pointed to by <paramref name="projection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetProjectionInfoEx(RasHandle handle, IntPtr projection, ref IntPtr bufferSize)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetProjectionInfoEx");
            evt.Data.Add("handle", handle);
            evt.Data.Add("projection", projection);
            evt.Data.Add("bufferSize-IN", bufferSize);

            var result = 0;

            try
            {
                result = RasGetProjectionInfoEx(handle, projection, ref bufferSize);
                evt.ResultCode = result;
                evt.Data.Add("bufferSize-OUT", bufferSize);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#endif

        /// <summary>
        /// Retrieves a connection handle for a subentry of a multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The one-based index of the subentry to whose handle to retrieve.</param>
        /// <param name="result">Upon return, contains the handle to the subentry connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int GetSubEntryHandle(RasHandle handle, int subEntryId, out IntPtr result)
        {
            result = IntPtr.Zero;

            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasGetSubEntryHandle");
            evt.Data.Add("handle", handle);
            evt.Data.Add("subEntryId", subEntryId);
            evt.Data.Add("result-IN", result);

            var ret = 0;

            try
            {
                ret = RasGetSubEntryHandle(handle, subEntryId, out result);
                evt.ResultCode = ret;
                evt.Data.Add("result-OUT", result);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return ret;
        }

        /// <summary>
        /// Terminates a remote access connection.
        /// </summary>
        /// <param name="handle">The handle to terminate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int HangUp(RasHandle handle)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasHangUp");
            evt.Data.Add("handle", handle);

            var result = 0;

            try
            {
                result = RasHangUp(handle);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials from a user.
        /// </summary>
        /// <param name="uiInfo">Pointer to a <see cref="NativeMethods.CREDUI_INFO"/> structure that contains information for customizing the appearance of the dialog box.</param>
        /// <param name="targetName">The name of the target for the credentials.</param>
        /// <param name="reserved">Reserved for future use.</param>
        /// <param name="authError">Specifies why the credential dialog box is needed.</param>
        /// <param name="userName">A string that contains the username for the credentials.</param>
        /// <param name="userNameMaxChars">The maximum number of characters that can be copied to <paramref name="userName"/> including the terminating null character.</param>
        /// <param name="password">A string that contains the password for the credentials.</param>
        /// <param name="passwordMaxChars">The maximum number of characters that can be copied to <paramref name="password"/> including the terminating null character.</param>
        /// <param name="saveChecked">Specifies the initial state of the save checkbox and receives the state of the save checkbox after the user has responded to the dialog.</param>
        /// <param name="flags">Specifies special behavior for this function.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int PromptForCredentials(IntPtr uiInfo, string targetName, IntPtr reserved, int authError, StringBuilder userName, int userNameMaxChars, StringBuilder password, int passwordMaxChars, ref bool saveChecked, NativeMethods.CREDUI_FLAGS flags)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.CredUIDll, "CredUIPromptForCredentials");
            evt.Data.Add("uiInfo", uiInfo);
            evt.Data.Add("targetName", targetName);
            evt.Data.Add("reserved", reserved);
            evt.Data.Add("authError", authError);
            evt.Data.Add("userName", userName);
            evt.Data.Add("userNameMaxChars", userNameMaxChars);
            evt.Data.Add("password", "********");
            evt.Data.Add("passwordMaxChars", passwordMaxChars);
            evt.Data.Add("saveChecked-IN", saveChecked);
            evt.Data.Add("flags", flags);

            var result = 0;

            try
            {
                result = CredUIPromptForCredentials(uiInfo, targetName, reserved, authError, userName, userNameMaxChars, password, passwordMaxChars, ref saveChecked, flags);
                evt.ResultCode = result;
                evt.Data.Add("saveChecked-OUT", saveChecked);
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

#endif

        /// <summary>
        /// Specifies an event object that the system sets to the signaled state when a RAS connection changes.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="eventHandle">The handle of an event object.</param>
        /// <param name="flags">Specifies the RAS event that causes the system to signal the event specified by the <paramref name="eventHandle"/> parameter.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int RegisterConnectionNotification(RasHandle handle, SafeHandle eventHandle, NativeMethods.RASCN flags)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasConnectionNotification");
            evt.Data.Add("handle", handle);
            evt.Data.Add("eventHandle", eventHandle);
            evt.Data.Add("flags", flags);

            var result = 0;

            try
            {
                result = RasConnectionNotification(handle, eventHandle, flags);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Indicates whether the entry name is valid for the phone book specified.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        public int ValidateEntryName(string phoneBookPath, string entryName)
        {
            var evt = new PInvokeCallTraceEvent(NativeMethods.RasApi32Dll, "RasValidateEntryName");
            evt.Data.Add("phoneBookPath", phoneBookPath);
            evt.Data.Add("entryName", entryName);

            var result = 0;

            try
            {
                result = RasValidateEntryName(phoneBookPath, entryName);
                evt.ResultCode = result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, evt);
            }

            return result;
        }

        /// <summary>
        /// Allocates a new locally unique identifier.
        /// </summary>
        /// <param name="pLuid">Pointer to a <see cref="DotRas.Luid"/> structure that upon return, receives the generated LUID instance.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        [DllImport(NativeMethods.AdvApi32Dll, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocateLocallyUniqueId(
            [Out] IntPtr pLuid);

        /// <summary>
        /// Deletes an object freeing all system resources associated with that object.
        /// </summary>
        /// <param name="hObject">The handle to the object.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        [DllImport(NativeMethods.Gdi32Dll)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject(
            IntPtr hObject);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials from a user.
        /// </summary>
        /// <param name="pUiInfo">Pointer to a <see cref="NativeMethods.CREDUI_INFO"/> structure that contains information for customizing the appearance of the dialog box.</param>
        /// <param name="pszTargetName">The name of the target for the credentials.</param>
        /// <param name="reserved">Reserved for future use.</param>
        /// <param name="dwAuthError">Specifies why the credential dialog box is needed.</param>
        /// <param name="pszUserName">A string that contains the username for the credentials.</param>
        /// <param name="ulUserNameMaxChars">The maximum number of characters that can be copied to <paramref name="pszUserName"/> including the terminating null character.</param>
        /// <param name="pszPassword">A string that contains the password for the credentials.</param>
        /// <param name="ulPasswordMaxChars">The maximum number of characters that can be copied to <paramref name="pszPassword"/> including the terminating null character.</param>
        /// <param name="pfSave">Specifies the initial state of the save checkbox and receives the state of the save checkbox after the user has responded to the dialog.</param>
        /// <param name="dwFlags">Specifies special behavior for this function.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.CredUIDll, CharSet = CharSet.Unicode)]
        private static extern int CredUIPromptForCredentials(
            IntPtr pUiInfo,
            string pszTargetName,
            IntPtr reserved,
            int dwAuthError,
            StringBuilder pszUserName,
            int ulUserNameMaxChars,
            StringBuilder pszPassword,
            int ulPasswordMaxChars,
            [MarshalAs(UnmanagedType.Bool)]
            ref bool pfSave,
            NativeMethods.CREDUI_FLAGS dwFlags);

#endif

        /// <summary>
        /// Clears any accumulated statistics for the specified RAS connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasClearConnectionStatistics(
            RasHandle hRasConn);

        /// <summary>
        /// Clears any accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to clear statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasClearLinkStatistics(
            RasHandle hRasConn,
            int subEntryId);

        /// <summary>
        /// Specifies an event object that the system sets to the signaled state when a RAS connection changes.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="hEvent">The handle of an event object.</param>
        /// <param name="dwFlags">Specifies the RAS event that causes the system to signal the event specified by the <paramref name="hEvent"/> parameter.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasConnectionNotification(
            RasHandle hRasConn,
            SafeHandle hEvent,
            NativeMethods.RASCN dwFlags);

        /// <summary>
        /// Establishes a remote access connection between a client and a server.
        /// </summary>
        /// <param name="lpRasDialExtensions">Pointer to a <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure containing extended feature information.</param>
        /// <param name="lpszPhonebook">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="lpRasDialParams">Pointer to a <see cref="NativeMethods.RASDIALPARAMS"/> structure containing calling parameters for the connection.</param>
        /// <param name="dwNotifierType">Specifies the nature of the <paramref name="lpvNotifier"/> argument. If <paramref name="lpvNotifier"/> is null (<b>Nothing</b> in Visual Basic) this argument is ignored.</param>
        /// <param name="lpvNotifier">Specifies the callback used during the dialing process.</param>
        /// <param name="lphRasConn">Upon return, contains the handle to the RAS connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasDial(
            IntPtr lpRasDialExtensions,
            string lpszPhonebook,
            IntPtr lpRasDialParams,
            NativeMethods.RasNotifierType dwNotifierType,
            Delegate lpvNotifier,
            out RasHandle lphRasConn);

        /// <summary>
        /// Lists all active remote access service (RAS) connections.
        /// </summary>
        /// <param name="lpRasConn">Pointer to a buffer that, on output, receives an array of <see cref="DotRas.RasConnection"/> structures.</param>
        /// <param name="lpCb">Upon return, contains the size in bytes of the buffer specified by <paramref name="lpRasConn"/>. Upon return contains the number of bytes required to successfully complete the call.</param>
        /// <param name="lpcConnections">Upon return, contains the number of phone book entries written to the buffer specified by <paramref name="lpRasConn"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasEnumConnections(
            [In, Out] IntPtr lpRasConn,
            ref IntPtr lpCb,
            ref IntPtr lpcConnections);

        /// <summary>
        /// Lists all available remote access capable devices.
        /// </summary>
        /// <param name="lpRasDevInfo">Pointer to a buffer that, on output, receives an array of <see cref="NativeMethods.RASDEVINFO"/> structures.</param>
        /// <param name="lpCb">Upon return, contains the size in bytes of the buffer specified by <paramref name="lpRasDevInfo"/>. Upon return contains the number of bytes required to successfully complete the call.</param>
        /// <param name="lpcDevices">Upon return, contains the number of device entries written to the buffer specified by <paramref name="lpRasDevInfo"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasEnumDevices(
            [In, Out] IntPtr lpRasDevInfo,
            ref IntPtr lpCb,
            ref IntPtr lpcDevices);

        /// <summary>
        /// Frees the memory buffer returned by the <see cref="SafeNativeMethods.RasGetEapUserIdentity"/> method.
        /// </summary>
        /// <param name="lpRasEapUserIdentity">Pointer to the <see cref="NativeMethods.RASEAPUSERIDENTITY"/> structure.</param>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern void RasFreeEapUserIdentity(
            IntPtr lpRasEapUserIdentity);

        /// <summary>
        /// Retrieves accumulated statistics for the specified connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="lpStatistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetConnectionStatistics(
            RasHandle hRasConn,
            IntPtr lpStatistics);

        /// <summary>
        /// Retrieves information on the current status of the specified remote access connection handle.
        /// </summary>
        /// <param name="hRasConn">The handle to check.</param>
        /// <param name="lpRasConnStatus">Pointer to a <see cref="DotRas.RasConnectionStatus"/> structure that upon return contains the status information for the handle specified by <paramref name="hRasConn"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetConnectStatus(
            RasHandle hRasConn,
            IntPtr lpRasConnStatus);

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows telephony list of countries/regions.
        /// </summary>
        /// <param name="lpRasCtryInfo">Pointer to a <see cref="NativeMethods.RASCTRYINFO"/> structure that upon output receives the country/region dialing information.</param>
        /// <param name="lpdwSize">Pointer to a variable that, on input, specifies the size, in bytes, of the buffer pointed to by <paramref name="lpRasCtryInfo"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetCountryInfo(
            [In, Out] IntPtr lpRasCtryInfo,
            ref IntPtr lpdwSize);

        /// <summary>
        /// Retrieves connection specific authentication information.
        /// </summary>
        /// <param name="lpszPhonebook">The full path (including filename) of the phone book containing the entry.</param>
        /// <param name="lpszEntryName">The name of the entry whose credentials to retrieve.</param>
        /// <param name="pCustomAuthData">Pointer to a buffer that upon output receives the authentication data.</param>
        /// <param name="pdwSizeOfCustomAuthData">Pointer to a variable that, on input, specifies the size, in bytes, of the buffer pointed to by <paramref name="pCustomAuthData"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetCustomAuthData(
            string lpszPhonebook,
            string lpszEntryName,
            [In, Out] IntPtr pCustomAuthData,
            ref IntPtr pdwSizeOfCustomAuthData);

        /// <summary>
        /// Retrieves Extensible Authentication Protocol (EAP) identity information for the current user.
        /// </summary>
        /// <param name="lpszPhonebook">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="lpszEntryName">The name of an existing entry within the phone book.</param>
        /// <param name="dwFlags">Specifies any flags that qualify the authentication process.</param>
        /// <param name="hwnd">Handle to the parent window for the UI dialog.</param>
        /// <param name="lpRasEapUserIdentity">Pointer to a buffer that upon return contains the EAP user identity information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetEapUserIdentity(
            string lpszPhonebook,
            string lpszEntryName,
            NativeMethods.RASEAPF dwFlags,
            IntPtr hwnd,
            ref IntPtr lpRasEapUserIdentity);

        /// <summary>
        /// Returns an error message string for a specified RAS error value.
        /// </summary>
        /// <param name="uErrorValue">The error value of interest.</param>
        /// <param name="lpszErrorString">The buffer that will receive the error string.</param>
        /// <param name="cBufSize">Specifies the size, in characters, of the buffer pointed to by <paramref name="lpszErrorString"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetErrorString(
            int uErrorValue,
            [In, Out] string lpszErrorString,
            int cBufSize);

        /// <summary>
        /// Retrieves accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to retrieve statistics.</param>
        /// <param name="lpRasStatistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetLinkStatistics(
            RasHandle hRasConn,
            int subEntryId,
            IntPtr lpRasStatistics);

#if (WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Retrieves the network access protection (NAP) status for a remote access connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="pNapState">Pointer to a <see cref="NativeMethods.RASNAPSTATE"/> structure </param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetNapStatus(
            RasHandle hRasConn,
            IntPtr pNapState);
#endif

        /// <summary>
        /// Obtains information about a remote access projection operation for a specified remote access component protocol.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="rasprojection">The <see cref="NativeMethods.RASPROJECTION"/> that identifies the protocol of interest.</param>
        /// <param name="lpProjection">Pointer to a buffer that receives the information specified by the <paramref name="rasprojection"/> parameter.</param>
        /// <param name="lpCb">On input specifies the size in bytes of the buffer pointed to by <paramref name="lpProjection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetProjectionInfo(
            RasHandle hRasConn,
            NativeMethods.RASPROJECTION rasprojection,
            IntPtr lpProjection,
            ref IntPtr lpCb);

#if (WIN7 || WIN8)
        /// <summary>
        /// Obtains information about a remote access projection operation for all RAS connections on the local client.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="pRasProjection">Pointer to a <see cref="NativeMethods.RAS_PROJECTION_INFO"/> structure that receives the projection information for the RAS connections.</param>
        /// <param name="lpdwSize">On input specifies the size in bytes of the buffer pointed to by <paramref name="pRasProjection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetProjectionInfoEx(
            RasHandle hRasConn,
            IntPtr pRasProjection,
            ref IntPtr lpdwSize);
#endif

        /// <summary>
        /// Retrieves a connection handle for a subentry of a multilink connection.
        /// </summary>
        /// <param name="hRasConn">The handle to the connection.</param>
        /// <param name="subEntryId">The one-based index of the subentry to whose handle to retrieve.</param>
        /// <param name="lphRasConn">Upon return, contains the handle to the subentry connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetSubEntryHandle(
            RasHandle hRasConn,
            int subEntryId,
            out IntPtr lphRasConn);

        /// <summary>
        /// Terminates a remote access connection.
        /// </summary>
        /// <param name="hRasConn">The handle to terminate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll)]
        private static extern int RasHangUp(
            RasHandle hRasConn);

        /// <summary>
        /// Retrieves user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry.
        /// </summary>
        /// <param name="handle">The handle to a primary or impersonation access token.</param>
        /// <param name="lpszPhonebook">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="lpszEntryName">The entry name to validate.</param>
        /// <param name="lpbEapData">Pointer to a buffer that receives the retrieved EAP data for the user.</param>
        /// <param name="pdwSizeOfEapData">On input specifies the size in bytes of the buffer pointed to by <paramref name="lpbEapData"/>, upon output receives the size of the buffer needed to contain the EAP data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasGetEapUserData(
            IntPtr handle,
            string lpszPhonebook,
            string lpszEntryName,
            [Out] IntPtr lpbEapData,
            ref IntPtr pdwSizeOfEapData);

        /// <summary>
        /// Indicates whether the entry name is valid for the phone book specified.
        /// </summary>
        /// <param name="lpszPhonebook">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="lpszEntryName">The entry name to validate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        [DllImport(NativeMethods.RasApi32Dll, CharSet = CharSet.Unicode)]
        private static extern int RasValidateEntryName(
            string lpszPhonebook,
            string lpszEntryName);

        #endregion
    }
}