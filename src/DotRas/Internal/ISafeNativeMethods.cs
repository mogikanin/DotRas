//--------------------------------------------------------------------------
// <copyright file="ISafeNativeMethods.cs" company="Jeff Winn">
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
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Defines the members for safe unmanaged code entry points.
    /// </summary>
    internal interface ISafeNativeMethods
    {
        #region Methods

        /// <summary>
        /// Allocates a new locally unique identifier.
        /// </summary>
        /// <param name="pLuid">Pointer to a <see cref="DotRas.Luid"/> structure that upon return, receives the generated LUID instance.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int AllocateLocallyUniqueIdImpl(IntPtr pLuid);

        /// <summary>
        /// Clears any accumulated statistics for the specified RAS connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int ClearConnectionStatistics(RasHandle handle);

        /// <summary>
        /// Clears any accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to clear statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int ClearLinkStatistics(RasHandle handle, int subEntryId);

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
        int Dial(IntPtr extensions, string phoneBookPath, IntPtr dialParameters, NativeMethods.RasNotifierType notifierType, Delegate notifier, out RasHandle handle);

        /// <summary>
        /// Lists all active remote access service (RAS) connections.
        /// </summary>
        /// <param name="value">An <see cref="StructBufferedPInvokeParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int EnumConnections(StructBufferedPInvokeParams value);

        /// <summary>
        /// Lists all available remote access capable devices.
        /// </summary>
        /// <param name="value">An <see cref="StructBufferedPInvokeParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int EnumDevices(StructBufferedPInvokeParams value);

        /// <summary>
        /// Frees the memory buffer returned by the <see cref="SafeNativeMethods.RasGetEapUserIdentity"/> method.
        /// </summary>
        /// <param name="identity">Pointer to the <see cref="NativeMethods.RASEAPUSERIDENTITY"/> structure.</param>
        void FreeEapUserIdentity(IntPtr identity);
        
        /// <summary>
        /// Frees all system resources associated with an object.
        /// </summary>
        /// <param name="handle">The handle to the object.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        bool FreeObject(IntPtr handle);

        /// <summary>
        /// Retrieves accumulated statistics for the specified connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="statistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetConnectionStatistics(RasHandle handle, IntPtr statistics);

        /// <summary>
        /// Retrieves information on the current status of the specified remote access connection handle.
        /// </summary>
        /// <param name="handle">The handle to check.</param>
        /// <param name="connectionStatus">Pointer to a <see cref="NativeMethods.RASCONNSTATUS"/> structure that upon return contains the status information for the handle specified by <paramref name="handle"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetConnectStatus(RasHandle handle, IntPtr connectionStatus);

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows telephony list of countries/regions.
        /// </summary>
        /// <param name="countries">Pointer to a <see cref="NativeMethods.RASCTRYINFO"/> structure that upon output receives the country/region dialing information.</param>
        /// <param name="bufferSize">Pointer to a variable that, on input, specifies the size, in bytes, of the buffer pointed to by <paramref name="countries"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetCountryInfo(IntPtr countries, ref IntPtr bufferSize);

        /// <summary>
        /// Retrieves user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry.
        /// </summary>
        /// <param name="value">An <see cref="RasGetEapUserDataParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetEapUserData(RasGetEapUserDataParams value);

        /// <summary>
        /// Retrieves connection specific authentication information.
        /// </summary>
        /// <param name="value">An <see cref="RasGetCustomAuthDataParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetCustomAuthData(RasGetCustomAuthDataParams value);

        /// <summary>
        /// Retrieves Extensible Authentication Protocol (EAP) identity information for the current user.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="flags">Specifies any flags that qualify the authentication process.</param>
        /// <param name="hwnd">Handle to the parent window for the UI dialog.</param>
        /// <param name="identity">Pointer to a buffer that upon return contains the EAP user identity information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetEapUserIdentity(string phoneBookPath, string entryName, NativeMethods.RASEAPF flags, IntPtr hwnd, ref IntPtr identity);

        /// <summary>
        /// Returns an error message string for a specified RAS error value.
        /// </summary>
        /// <param name="errorCode">The error value of interest.</param>
        /// <param name="result">The buffer that will receive the error string.</param>
        /// <param name="bufferSize">Specifies the size, in characters, of the buffer pointed to by <paramref name="result"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetErrorString(int errorCode, string result, int bufferSize);

        /// <summary>
        /// Retrieves accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to retrieve statistics.</param>
        /// <param name="statistics">Pointer to a <see cref="NativeMethods.RAS_STATS"/> structure which will receive the statistics.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetLinkStatistics(RasHandle handle, int subEntryId, IntPtr statistics);

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Retrieves the network access protection (NAP) status for a remote access connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="state">Pointer to a <see cref="NativeMethods.RASNAPSTATE"/> structure </param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetNapStatus(RasHandle handle, IntPtr state);

#endif

        /// <summary>
        /// Obtains information about a remote access projection operation for a specified remote access component protocol.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="projectionType">The <see cref="NativeMethods.RASPROJECTION"/> that identifies the protocol of interest.</param>
        /// <param name="projection">Pointer to a buffer that receives the information.</param>
        /// <param name="bufferSize">On input specifies the size in bytes of the buffer pointed to by <paramref name="projection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetProjectionInfo(RasHandle handle, NativeMethods.RASPROJECTION projectionType, IntPtr projection, ref IntPtr bufferSize);

#if (WIN7 || WIN8)

        /// <summary>
        /// Obtains information about a remote access projection operation for all RAS connections on the local client.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="projection">Pointer to a <see cref="NativeMethods.RAS_PROJECTION_INFO"/> structure that receives the projection information for the RAS connections.</param>
        /// <param name="bufferSize">On input specifies the size in bytes of the buffer pointed to by <paramref name="projection"/>, upon output receives the size of the buffer needed to contain the projection information.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetProjectionInfoEx(RasHandle handle, IntPtr projection, ref IntPtr bufferSize);

#endif

        /// <summary>
        /// Retrieves a connection handle for a subentry of a multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The one-based index of the subentry to whose handle to retrieve.</param>
        /// <param name="result">Upon return, contains the handle to the subentry connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetSubEntryHandle(RasHandle handle, int subEntryId, out IntPtr result);

        /// <summary>
        /// Terminates a remote access connection.
        /// </summary>
        /// <param name="handle">The handle to terminate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int HangUp(RasHandle handle);

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
        int PromptForCredentials(IntPtr uiInfo, string targetName, IntPtr reserved, int authError, StringBuilder userName, int userNameMaxChars, StringBuilder password, int passwordMaxChars, ref bool saveChecked, NativeMethods.CREDUI_FLAGS flags);

#endif

        /// <summary>
        /// Specifies an event object that the system sets to the signaled state when a RAS connection changes.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="eventHandle">The handle of an event object.</param>
        /// <param name="flags">Specifies the RAS event that causes the system to signal the event specified by the <paramref name="eventHandle"/> parameter.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int RegisterConnectionNotification(RasHandle handle, SafeHandle eventHandle, NativeMethods.RASCN flags);

        /// <summary>
        /// Indicates whether the entry name is valid for the phone book specified.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int ValidateEntryName(string phoneBookPath, string entryName);

        #endregion
    }
}