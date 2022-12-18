//--------------------------------------------------------------------------
// <copyright file="IRasHelper.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.Net;

    /// <summary>
    /// Defines the members for the remote access service (RAS) helper.
    /// </summary>
    internal interface IRasHelper
    {
        #region Methods

        /// <summary>
        /// Generates a new locally unique identifier.
        /// </summary>
        /// <returns>A new <see cref="DotRas.Luid"/> structure.</returns>
        Luid AllocateLocallyUniqueId();

        /// <summary>
        /// Establishes a remote access connection between a client and a server.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="parameters">A <see cref="NativeMethods.RASDIALPARAMS"/> structure containing calling parameters for the connection.</param>
        /// <param name="extensions">A <see cref="NativeMethods.RASDIALEXTENSIONS"/> structure containing extended feature information.</param>
        /// <param name="callback">A <see cref="NativeMethods.RasDialFunc2"/> delegate to notify during the connection process.</param>
        /// <param name="eapOptions">Specifies options to use during authentication.</param>
        /// <returns>The handle of the connection.</returns>
        RasHandle Dial(string phoneBookPath, NativeMethods.RASDIALPARAMS parameters, NativeMethods.RASDIALEXTENSIONS extensions, NativeMethods.RasDialFunc2 callback, NativeMethods.RASEAPF eapOptions);

        /// <summary>
        /// Indicates the current AutoDial status for a specific TAPI dialing location.
        /// </summary>
        /// <param name="dialingLocation">The dialing location whose status to check.</param>
        /// <returns><b>true</b> if the AutoDial feature is currently enabled for the dialing location, otherwise <b>false</b>.</returns>
        bool GetAutoDialEnable(int dialingLocation);

        /// <summary>
        /// Retrieves the value of an AutoDial parameter.
        /// </summary>
        /// <param name="parameter">The parameter whose value to retrieve.</param>
        /// <returns>The value of the parameter.</returns>
        int GetAutoDialParameter(NativeMethods.RASADP parameter);

        /// <summary>
        /// Clears any accumulated statistics for the specified remote access connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>        
        bool ClearConnectionStatistics(RasHandle handle);

        /// <summary>
        /// Clears any accumulated statistics for the specified link in a remote access multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The subentry index that corresponds to the link for which to clear statistics.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        bool ClearLinkStatistics(RasHandle handle, int subEntryId);

        /// <summary>
        /// Deletes an entry from a phone book.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including file name) of the phone book.</param>
        /// <param name="entryName">The name of the entry to delete.</param>
        /// <returns><b>true</b> if the entry was deleted, otherwise <b>false</b>.</returns>
        bool DeleteEntry(string phoneBookPath, string entryName);

#if (WINXP || WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Deletes a subentry from the specified phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including file name) of the phone book.</param>
        /// <param name="entryName">The name of the entry containing the subentry to be deleted.</param>
        /// <param name="subEntryId">The one-based index of the subentry to delete.</param>
        /// <returns><b>true</b> if the function succeeds, otherwise <b>false</b>.</returns>
        bool DeleteSubEntry(string phoneBookPath, string entryName, int subEntryId);
#endif

        /// <summary>
        /// Retrieves a read-only list of active connections.
        /// </summary>
        /// <returns>A new read-only collection of <see cref="DotRas.RasConnection"/> objects, or an empty collection if no active connections were found.</returns>
        ReadOnlyCollection<RasConnection> GetActiveConnections();

        /// <summary>
        /// Retrieves information about the entries associated with a network address in the AutoDial mapping database.
        /// </summary>
        /// <param name="address">The address to retrieve.</param>
        /// <returns>A new <see cref="DotRas.RasAutoDialAddress"/> object.</returns>        
        RasAutoDialAddress GetAutoDialAddress(string address);

        /// <summary>
        /// Retrieves a collection of addresses in the AutoDial mapping database.
        /// </summary>
        /// <returns>A new collection of <see cref="DotRas.RasAutoDialAddress"/> objects, or an empty collection if no addresses were found.</returns>       
        Collection<string> GetAutoDialAddresses();

        /// <summary>
        /// Retrieves the connection status for the handle specified.
        /// </summary>
        /// <param name="handle">The remote access connection handle to retrieve.</param>
        /// <returns>A <see cref="DotRas.RasConnectionStatus"/> object containing connection status information.</returns>
        RasConnectionStatus GetConnectionStatus(RasHandle handle);

        /// <summary>
        /// Retrieves user credentials associated with a specified remote access phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of the phone book containing the entry.</param>
        /// <param name="entryName">The name of the entry whose credentials to retrieve.</param>
        /// <param name="options">The options to request.</param>
        /// <returns>The credentials stored in the entry, otherwise a null reference (<b>Nothing</b> in Visual Basic) if the credentials did not exist.</returns>
        NetworkCredential GetCredentials(string phoneBookPath, string entryName, NativeMethods.RASCM options);

        /// <summary>
        /// Retrieves connection specific authentication information.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of the phone book containing the entry.</param>
        /// <param name="entryName">The name of the entry whose credentials to retrieve.</param>
        /// <returns>A byte array containing the authentication information, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        byte[] GetCustomAuthData(string phoneBookPath, string entryName);

        /// <summary>
        /// Lists all available remote access capable devices.
        /// </summary>
        /// <returns>A new collection of <see cref="DotRas.RasDevice"/> objects.</returns>        
        ReadOnlyCollection<RasDevice> GetDevices();

        /// <summary>
        /// Retrieves user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry.
        /// </summary>
        /// <param name="userToken">The handle of a Windows account token. This token is usually retrieved through a call to unmanaged code, such as a call to the Win32 API LogonUser function.</param>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <returns>A byte array containing the EAP data, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        byte[] GetEapUserData(IntPtr userToken, string phoneBookPath, string entryName);

        /// <summary>
        /// Retrieves the entry properties for an entry within a phone book.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> containing the entry.</param>
        /// <param name="entryName">The name of an entry to retrieve.</param>
        /// <returns>A <see cref="DotRas.RasEntry"/> object.</returns>
        RasEntry GetEntryProperties(RasPhoneBook phoneBook, string entryName);

        /// <summary>
        /// Retrieves a connection handle for a subentry of a multilink connection.
        /// </summary>
        /// <param name="handle">The handle of the connection.</param>
        /// <param name="subEntryId">The one-based index of the subentry to whose handle to retrieve.</param>
        /// <returns>The handle of the subentry if available, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        RasHandle GetSubEntryHandle(RasHandle handle, int subEntryId);

        /// <summary>
        /// Retrieves the subentry properties for an entry within a phone book.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> containing the entry.</param>
        /// <param name="entry">The <see cref="DotRas.RasEntry"/> containing the subentry.</param>
        /// <param name="subEntryId">The zero-based index of the subentry to retrieve.</param>
        /// <returns>A new <see cref="DotRas.RasSubEntry"/> object.</returns>
        RasSubEntry GetSubEntryProperties(RasPhoneBook phoneBook, RasEntry entry, int subEntryId);

        /// <summary>
        /// Retrieves a list of entry names within a phone book.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> whose entry names to retrieve.</param>
        /// <returns>An array of <see cref="NativeMethods.RASENTRYNAME"/> structures, or a null reference if the phone-book was not found.</returns>
        NativeMethods.RASENTRYNAME[] GetEntryNames(RasPhoneBook phoneBook);

        /// <summary>
        /// Retrieves accumulated statistics for the specified connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <returns>A <see cref="DotRas.RasLinkStatistics"/> structure containing connection statistics.</returns>        
        RasLinkStatistics GetConnectionStatistics(RasHandle handle);

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows Telephony list of countries/regions for a specific country id.
        /// </summary>
        /// <param name="countryId">The country id to retrieve.</param>
        /// <param name="nextCountryId">Upon output, contains the next country id from the list; otherwise zero for the last country/region in the list.</param>
        /// <returns>A new <see cref="DotRas.RasCountry"/> object.</returns>        
        RasCountry GetCountry(int countryId, out int nextCountryId);

        /// <summary>
        /// Retrieves accumulated statistics for the specified link in a RAS multilink connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="subEntryId">The one-based index that corresponds to the link for which to retrieve statistics.</param>
        /// <returns>A <see cref="DotRas.RasLinkStatistics"/> structure containing connection statistics.</returns>
        RasLinkStatistics GetLinkStatistics(RasHandle handle, int subEntryId);

        /// <summary>
        /// Terminates a remote access connection.
        /// </summary>
        /// <param name="handle">The remote access connection handle to terminate.</param>
        /// <param name="pollingInterval">The length of time, in milliseconds, the thread must be paused while polling whether the connection has terminated.</param>
        /// <param name="closeAllReferences"><b>true</b> to disconnect all connection references, otherwise <b>false</b>.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="pollingInterval"/> must be greater than or equal to zero.</exception>
        /// <exception cref="DotRas.InvalidHandleException"><paramref name="handle"/> is not a valid handle.</exception>
        void HangUp(RasHandle handle, int pollingInterval, bool closeAllReferences);

        /// <summary>
        /// Indicates whether a connection is currently active.
        /// </summary>
        /// <param name="handle">The handle to check.</param>
        /// <returns><b>true</b> if the connection is active, otherwise <b>false</b>.</returns>
        bool IsConnectionActive(RasHandle handle);

        /// <summary>
        /// Frees the memory buffer of an EAP user identity.
        /// </summary>
        /// <param name="rasEapUserIdentity">The <see cref="NativeMethods.RASEAPUSERIDENTITY"/> structure to free.</param>        
        void FreeEapUserIdentity(NativeMethods.RASEAPUSERIDENTITY rasEapUserIdentity);

        /// <summary>
        /// Retrieves any Extensible Authentication Protocol (EAP) user identity information if available.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry in the phone book being connected.</param>
        /// <param name="eapOptions">Specifies options to use during authentication.</param>
        /// <param name="owner">The parent window for the UI dialog (if needed).</param>
        /// <param name="identity">Upon return, contains the Extensible Authentication Protocol (EAP) user identity information.</param>
        /// <returns><b>true</b> if the user identity information was returned, otherwise <b>false</b>.</returns>
        bool TryGetEapUserIdentity(string phoneBookPath, string entryName, NativeMethods.RASEAPF eapOptions,
#if !NO_UI
            System.Windows.Forms.IWin32Window owner, 
#endif
            out NativeMethods.RASEAPUSERIDENTITY identity);

#if (WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Retrieves the network access protection (NAP) status for a remote access connection.
        /// </summary>
        /// <param name="handle">The handle of the connection.</param>
        /// <returns>A <see cref="DotRas.RasNapStatus"/> object containing the NAP status.</returns>        
        RasNapStatus GetNapStatus(RasHandle handle);
#endif

        /// <summary>
        /// Retrieves information about a remote access projection operation for a connection.
        /// </summary>
        /// <param name="handle">The handle of the connection.</param>
        /// <param name="projection">The protocol of interest.</param>
        /// <returns>The resulting projection information, otherwise null reference (<b>Nothing</b> in Visual Basic) if the protocol was not found.</returns>
        object GetProjectionInfo(RasHandle handle, NativeMethods.RASPROJECTION projection);

#if (WIN7 || WIN8)
        /// <summary>
        /// Retrieves extended information about a remote access projection operation for a connection.
        /// </summary>
        /// <param name="handle">The handle of the connection.</param>
        /// <returns>The resulting projection information, otherwise null reference (<b>Nothing</b> in Visual Basic) if the protocol was not found.</returns>
        object GetProjectionInfoEx(RasHandle handle);
#endif

        /// <summary>
        /// Retrieves an error message for a specified RAS error code.
        /// </summary>
        /// <param name="errorCode">The error code to retrieve.</param>
        /// <returns>An <see cref="System.String"/> with the error message, otherwise a null reference (<b>Nothing</b> in Visual Basic) if the error code was not found.</returns>        
        string GetRasErrorString(int errorCode);

        /// <summary>
        /// Indicates whether the entry name is valid for the phone book specified.
        /// </summary>
        /// <param name="phoneBook">An <see cref="DotRas.RasPhoneBook"/> to validate the name against.</param>
        /// <param name="entryName">The name of an entry to check.</param>
        /// <returns><b>true</b> if the entry name is valid, otherwise <b>false</b>.</returns>
        bool IsValidEntryName(RasPhoneBook phoneBook, string entryName);

        /// <summary>
        /// Indicates whether the entry name is valid for the phone book specified.
        /// </summary>
        /// <param name="phoneBook">An <see cref="DotRas.RasPhoneBook"/> to validate the name against.</param>
        /// <param name="entryName">The name of an entry to check.</param>
        /// <param name="acceptableResults">Any additional results that are considered acceptable results from the call.</param>
        /// <returns><b>true</b> if the entry name is valid, otherwise <b>false</b>.</returns>
        bool IsValidEntryName(RasPhoneBook phoneBook, string entryName, params int[] acceptableResults);

        /// <summary>
        /// Renames an existing entry in a phone book.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> containing the entry to be renamed.</param>
        /// <param name="entryName">The name of an entry to rename.</param>
        /// <param name="newEntryName">The new name of the entry.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool RenameEntry(RasPhoneBook phoneBook, string entryName, string newEntryName);

        /// <summary>
        /// Updates an address in the AutoDial mapping database.
        /// </summary>
        /// <param name="address">The address to update.</param>
        /// <param name="entries">A collection of <see cref="DotRas.RasAutoDialEntry"/> objects containing the entries for the <paramref name="address"/> specified.</param>
        /// <returns><b>true</b> if the update was successful, otherwise <b>false</b>.</returns>        
        bool SetAutoDialAddress(string address, Collection<RasAutoDialEntry> entries);

        /// <summary>
        /// Enables or disables the AutoDial feature for a specific TAPI dialing location.
        /// </summary>
        /// <param name="dialingLocation">The TAPI dialing location to update.</param>
        /// <param name="enabled"><b>true</b> to enable the AutoDial feature, otherwise <b>false</b> to disable it.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>        
        bool SetAutoDialEnable(int dialingLocation, bool enabled);

        /// <summary>
        /// Sets the value of an AutoDial parameter.
        /// </summary>
        /// <param name="parameter">The parameter whose value to set.</param>
        /// <param name="value">The new value of the parameter.</param>        
        void SetAutoDialParameter(NativeMethods.RASADP parameter, int value);

        /// <summary>
        /// Sets the custom authentication data.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <param name="data">A byte array containing the custom authentication data.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool SetCustomAuthData(string phoneBookPath, string entryName, byte[] data);

        /// <summary>
        /// Store user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry in the registry.
        /// </summary>
        /// <param name="handle">The handle to a primary or impersonation access token.</param>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <param name="data">A byte array containing the EAP data.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool SetEapUserData(IntPtr handle, string phoneBookPath, string entryName, byte[] data);

        /// <summary>
        /// Sets the entry properties for an existing phone book entry, or creates a new entry.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> that will contain the entry.</param>
        /// <param name="value">An <see cref="DotRas.RasEntry"/> object whose properties to set.</param>        
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool SetEntryProperties(RasPhoneBook phoneBook, RasEntry value);

        /// <summary>
        /// Sets the subentry properties for an existing subentry, or creates a new subentry.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> that will contain the entry.</param>
        /// <param name="entry">The <see cref="DotRas.RasEntry"/> whose subentry to set.</param>
        /// <param name="subEntryId">The zero-based index of the subentry to set.</param>
        /// <param name="value">An <see cref="DotRas.RasSubEntry"/> object whose properties to set.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool SetSubEntryProperties(RasPhoneBook phoneBook, RasEntry entry, int subEntryId, RasSubEntry value);

        /// <summary>
        /// Sets the user credentials for a phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry whose credentials to set.</param>
        /// <param name="credentials">An <see cref="NativeMethods.RASCREDENTIALS"/> object containing user credentials.</param>
        /// <param name="clearCredentials"><b>true</b> clears existing credentials by setting them to an empty string, otherwise <b>false</b>.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        bool SetCredentials(string phoneBookPath, string entryName, NativeMethods.RASCREDENTIALS credentials, bool clearCredentials);

#if (WIN7 || WIN8)
        /// <summary>
        /// Updates the tunnel endpoints of an Internet Key Exchange (IKEv2) connection.
        /// </summary>
        /// <param name="handle">The handle of the connection.</param>
        /// <param name="interfaceIndex">The new interface index of the endpoint.</param>
        /// <param name="localEndPoint">The new local client endpoint of the connection.</param>
        /// <param name="remoteEndPoint">The new remote server endpoint of the connection.</param>
        void UpdateConnection(RasHandle handle, int interfaceIndex, IPAddress localEndPoint, IPAddress remoteEndPoint);
#endif

#endregion
    }
}