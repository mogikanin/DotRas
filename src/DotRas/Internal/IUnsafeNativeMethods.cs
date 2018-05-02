//--------------------------------------------------------------------------
// <copyright file="IUnsafeNativeMethods.cs" company="Jeff Winn">
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

    /// <summary>
    /// Defines the members for potentially dangerous unmanaged code entry points.
    /// </summary>
    internal interface IUnsafeNativeMethods
    {
        #region Methods

        /// <summary>
        /// Copies a memory block from one location to another.
        /// </summary>
        /// <param name="destination">A pointer to the starting address of the move destination.</param>
        /// <param name="source">A pointer to the starting address of the block of memory to be moved.</param>
        /// <param name="length">The size of the memory block to move, in bytes.</param>
        void CopyMemoryImpl(IntPtr destination, IntPtr source, IntPtr length);

        /// <summary>
        /// Deletes an entry from a phone book.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of the entry to be deleted.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int DeleteEntry(string phoneBookPath, string entryName);

#if (WINXP || WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Deletes a subentry from the specified phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of the entry to be deleted.</param>
        /// <param name="subEntryId">The one-based index of the subentry to delete.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int DeleteSubEntry(string phoneBookPath, string entryName, int subEntryId);
#endif

        /// <summary>
        /// Establishes a remote access connection using a specified phone book entry. This function displays a stream of dialog boxes that indicate the state of the connection operation.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="phoneNumber">The phone number that overrides the numbers stored in the phone book entry.</param>
        /// <param name="info">A <see cref="NativeMethods.RASDIALDLG"/> structure containing input and output parameters.</param>
        /// <returns><b>true</b> if the function establishes a remote access connection, otherwise <b>false</b>.</returns>
        bool DialDlg(string phoneBookPath, string entryName, string phoneNumber, ref NativeMethods.RASDIALDLG info);

        /// <summary>
        /// Displays a dialog box used to manipulate phone book entries.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry to be created or modified.</param>
        /// <param name="info">An <see cref="NativeMethods.RASENTRYDLG"/> structure containing additional input/output parameters.</param>
        /// <returns><b>true</b> if the user creates, copies, or edits an entry, otherwise <b>false</b>.</returns>
        bool EntryDlg(string phoneBookPath, string entryName, ref NativeMethods.RASENTRYDLG info);

        /// <summary>
        /// Lists all addresses in the AutoDial mapping database.
        /// </summary>
        /// <param name="value">An <see cref="StructBufferedPInvokeParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>        
        int EnumAutodialAddresses(StructBufferedPInvokeParams value);

        /// <summary>
        /// Lists all entry names in a remote access phone-book.
        /// </summary>
        /// <param name="reserved">Reserved; this parameter must be a null reference.</param>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">Pointer to a buffer that, on output, receives an array of <see cref="NativeMethods.RASENTRYNAME"/> structures.</param>
        /// <param name="bufferSize">Upon return, contains the size in bytes of the buffer specified by <paramref name="entryName"/>. Upon return contains the number of bytes required to successfully complete the call.</param>
        /// <param name="count">Upon return, contains the number of phone book entries written to the buffer specified by <paramref name="entryName"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int EnumEntries(IntPtr reserved, string phoneBookPath, IntPtr entryName, ref IntPtr bufferSize, ref IntPtr count);

        /// <summary>
        /// Retrieves user credentials associated with a specified remote access phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="credentials">Pointer to a <see cref="NativeMethods.RASCREDENTIALS"/> structure that upon return contains the requested credentials for the phone book entry.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetCredentials(string phoneBookPath, string entryName, IntPtr credentials);

        /// <summary>
        /// Retrieves information about the entries associated with a network address in the AutoDial mapping database.
        /// </summary>
        /// <param name="value">An <see cref="RasGetAutodialAddressParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetAutodialAddress(RasGetAutodialAddressParams value);

        /// <summary>
        /// Indicates whether the AutoDial feature is enabled for a specific TAPI dialing location.
        /// </summary>
        /// <param name="value">An <see cref="RasGetAutodialEnableParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetAutodialEnable(RasGetAutodialEnableParams value);

        /// <summary>
        /// Retrieves the value of an AutoDial parameter.
        /// </summary>
        /// <param name="value">An <see cref="RasGetAutodialParamParams"/> containing call data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetAutodialParam(RasGetAutodialParamParams value);

        /// <summary>
        /// Retrieves information for an existing phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="entry">Pointer to a buffer that, upon return, contains a <see cref="NativeMethods.RASENTRY"/> structure containing entry information.</param>
        /// <param name="bufferSize">Specifies the size of the <paramref name="entry"/> buffer.</param>
        /// <param name="deviceInfo">The parameter is not used.</param>
        /// <param name="deviceInfoSize">The parameter is not used.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetEntryProperties(string phoneBookPath, string entryName, IntPtr entry, ref IntPtr bufferSize, IntPtr deviceInfo, IntPtr deviceInfoSize);

        /// <summary>
        /// Retrieves information about a subentry for the specified phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="index">The one-based index of the subentry to retrieve.</param>
        /// <param name="subentry">Pointer to a buffer that, upon return, contains a <see cref="NativeMethods.RASSUBENTRY"/> structure containing subentry information.</param>
        /// <param name="bufferSize">Upon return, contains the size in bytes of the buffer specified by <paramref name="subentry"/>. Upon return contains the number of bytes required to successfully complete the call.</param>
        /// <param name="deviceConfig">The parameter is not used.</param>
        /// <param name="deviceBufferSize">The parameter is not used.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int GetSubEntryProperties(string phoneBookPath, string entryName, int index, IntPtr subentry, ref IntPtr bufferSize, IntPtr deviceConfig, IntPtr deviceBufferSize);

        /// <summary>
        /// Displays the main dial-up networking dialog box.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference, the default phone book is used.</param>
        /// <param name="entryName">The name of the phone book entry to initially highlight.</param>
        /// <param name="info">An <see cref="NativeMethods.RASPBDLG"/> structure containing additional input/output parameters.</param>
        /// <returns><b>true</b> if the user dials an entry successfully, otherwise <b>false</b>.</returns>
        bool PhonebookDlg(string phoneBookPath, string entryName, ref NativeMethods.RASPBDLG info);

        /// <summary>
        /// Renames an existing entry in a phone book.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="oldEntryName">The name of the entry to rename.</param>
        /// <param name="newEntryName">The new name of the entry.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int RenameEntry(string phoneBookPath, string oldEntryName, string newEntryName);

        /// <summary>
        /// Updates an address in the AutoDial mapping database.
        /// </summary>
        /// <param name="address">The address for which information is being updated.</param>
        /// <param name="reserved">Reserved. This value must be zero.</param>
        /// <param name="addresses">Pointer to an array of <see cref="NativeMethods.RASAUTODIALENTRY"/> structures.</param>
        /// <param name="bufferSize">Upon return, contains the size in bytes of the buffer specified by <paramref name="addresses"/>. Upon return contains the number of bytes required to successfully complete the call.</param>
        /// <param name="count">Upon return, contains the number of phone book entries written to the buffer specified by <paramref name="addresses"/>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetAutodialAddress(string address, int reserved, IntPtr addresses, int bufferSize, int count);

        /// <summary>
        /// Enables or disables the AutoDial feature for a specific TAPI dialing location.
        /// </summary>
        /// <param name="dialingLocation">The TAPI dialing location to update.</param>
        /// <param name="enabled"><b>true</b> to enable the AutoDial feature, otherwise <b>false</b> to disable it.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetAutodialEnable(int dialingLocation, bool enabled);

        /// <summary>
        /// Sets the value of an AutoDial parameter.
        /// </summary>
        /// <param name="key">The parameter whose value to set.</param>
        /// <param name="value">A pointer containing the new value of the parameter.</param>
        /// <param name="bufferSize">The size of the buffer.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetAutodialParam(NativeMethods.RASADP key, IntPtr value, int bufferSize);

        /// <summary>
        /// Sets the user credentials for a phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry whose credentials to set.</param>
        /// <param name="credentials">Pointer to an <see cref="NativeMethods.RASCREDENTIALS"/> object containing user credentials.</param>
        /// <param name="clearCredentials"><b>true</b> clears existing credentials by setting them to an empty string, otherwise <b>false</b>.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetCredentials(string phoneBookPath, string entryName, IntPtr credentials, bool clearCredentials);

        /// <summary>
        /// Sets the custom authentication data.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry whose credentials to set.</param>
        /// <param name="customAuthData">Pointer to a buffer that contains the custom authentication data.</param>
        /// <param name="sizeOfCustomAuthData">On input specifies the size in bytes of the buffer pointed to by <paramref name="sizeOfCustomAuthData"/>, upon output receives the size of the buffer needed to contain the EAP data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetCustomAuthData(string phoneBookPath, string entryName, IntPtr customAuthData, int sizeOfCustomAuthData);

        /// <summary>
        /// Store user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry in the registry.
        /// </summary>
        /// <param name="handle">The handle to a primary or impersonation access token.</param>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The entry name to validate.</param>
        /// <param name="eapData">Pointer to a buffer that receives the retrieved EAP data for the user.</param>
        /// <param name="sizeOfEapData">On input specifies the size in bytes of the buffer pointed to by <paramref name="eapData"/>, upon output receives the size of the buffer needed to contain the EAP data.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetEapUserData(IntPtr handle, string phoneBookPath, string entryName, IntPtr eapData, int sizeOfEapData);

        /// <summary>
        /// Sets the connection information for an entry within a phone book, or creates a new phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="entry">Pointer to a buffer that, upon return, contains a <see cref="NativeMethods.RASENTRY"/> structure containing entry information.</param>
        /// <param name="bufferSize">Specifies the size of the <paramref name="entry"/> buffer.</param>
        /// <param name="device">The parameter is not used.</param>
        /// <param name="deviceBufferSize">The parameter is not used.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetEntryProperties(string phoneBookPath, string entryName, IntPtr entry, int bufferSize, IntPtr device, int deviceBufferSize);

        /// <summary>
        /// Sets the subentry connection information of a specified phone book entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path and filename of a phone book file. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of an existing entry within the phone book.</param>
        /// <param name="index">The one-based index of the subentry to set.</param>
        /// <param name="subentry">Pointer to a buffer that, upon return, contains a <see cref="NativeMethods.RASSUBENTRY"/> structure containing subentry information.</param>
        /// <param name="bufferSize">Specifies the size of the <paramref name="subentry"/> buffer.</param>
        /// <param name="deviceConfig">The parameter is not used.</param>
        /// <param name="deviceConfigSize">The parameter is not used.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int SetSubEntryProperties(string phoneBookPath, string entryName, int index, IntPtr subentry, int bufferSize, IntPtr deviceConfig, int deviceConfigSize);

#if (WIN7 || WIN8)
        /// <summary>
        /// Updates the tunnel endpoints of an Internet Key Exchange (IKEv2) connection.
        /// </summary>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="updateData">Pointer to a <see cref="NativeMethods.RASUPDATECONN"/> structure that contains the new tunnel endpoints for the connection.</param>
        /// <returns>If the function succeeds, the return value is zero.</returns>
        int UpdateConnection(RasHandle handle, IntPtr updateData);
#endif

        #endregion
    }
}