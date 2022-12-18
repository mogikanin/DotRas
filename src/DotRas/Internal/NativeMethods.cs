//--------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Jeff Winn">
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

using System.Diagnostics.CodeAnalysis;


namespace DotRas.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;

    /// <summary>
    /// Contains the remote access service (RAS) API entry points and structure definitions.
    /// </summary>
    internal static class NativeMethods
    {
        #region Fields

        /// <summary>
        /// Defines the default polling interval, in milliseconds, used when disconnecting an active connection.
        /// </summary>
        public const int HangUpPollingInterval = 50;

        /// <summary>
        /// Defines the name of the Advapi32.dll.
        /// </summary>
        public const string AdvApi32Dll = "advapi32.dll";

        /// <summary>
        /// Defines the name of the Gdi32.dll.
        /// </summary>
        public const string Gdi32Dll = "gdi32.dll";

        /// <summary>
        /// Defines the name of the Credui.dll.
        /// </summary>
        public const string CredUIDll = "credui.dll";

        /// <summary>
        /// Defines the name of the Rasapi32.dll.
        /// </summary>
        public const string RasApi32Dll = "rasapi32.dll";

        /// <summary>
        /// Defines the name of the Kernel32.dll.
        /// </summary>
        public const string Kernel32Dll = "kernel32.dll";

        /// <summary>
        /// Defines the name of the RasDlg.dll.
        /// </summary>
        public const string RasDlgDll = "rasdlg.dll";

        #region Lmcons.h Constants

        /// <summary>
        /// Defines the maximum length of the NetBIOS name.
        /// </summary>
        public const int NETBIOS_NAME_LEN = 16;

        /// <summary>
        /// Defines the maximum length of a username.
        /// </summary>
        public const int UNLEN = 256;

        /// <summary>
        /// Defines the maximum length of a password.
        /// </summary>
        public const int PWLEN = 256;

        /// <summary>
        /// Defines the maximum length of a domain name.
        /// </summary>
        public const int DNLEN = 15;

        #endregion

        #region Ras.h Constants

        #region RASDT

        /// <summary>
        /// A modem accessed through a COM port.
        /// </summary>
        public const string RASDT_Modem = "modem";

        /// <summary>
        /// An ISDN card with a corresponding NDISWAN driver installed.
        /// </summary>
        public const string RASDT_Isdn = "isdn";

        /// <summary>
        /// An X.25 card with a corresponding NDISWAN driver installed.
        /// </summary>
        public const string RASDT_X25 = "x25";

        /// <summary>
        /// A virtual private network connection.
        /// </summary>
        public const string RASDT_Vpn = "vpn";

        /// <summary>
        /// A packet assembler/disassembler.
        /// </summary>
        public const string RASDT_Pad = "pad";

        /// <summary>
        /// Generic device type.
        /// </summary>
        public const string RASDT_Generic = "GENERIC";

        /// <summary>
        /// Direct serial connection through a serial port.
        /// </summary>
        public const string RASDT_Serial = "SERIAL";

        /// <summary>
        /// Frame Relay.
        /// </summary>
        public const string RASDT_FrameRelay = "FRAMERELAY";

        /// <summary>
        /// Asynchronous Transfer Mode (ATM).
        /// </summary>
        public const string RASDT_Atm = "ATM";

        /// <summary>
        /// Sonet device type.
        /// </summary>
        public const string RASDT_Sonet = "SONET";

        /// <summary>
        /// Switched 56K access.
        /// </summary>
        public const string RASDT_SW56 = "SW56";

        /// <summary>
        /// An Infrared Data Association (IrDA) compliant device.
        /// </summary>
        public const string RASDT_Irda = "IRDA";

        /// <summary>
        /// Direct parallel connection through a parallel port.
        /// </summary>
        public const string RASDT_Parallel = "PARALLEL";

        /// <summary>
        /// Point-to-Point Protocol over Ethernet.
        /// </summary>
        public const string RASDT_PPPoE = "PPPoE";

        /// <summary>
        ///  Generic Routing Encapsulation (GRE).
        /// </summary>
        public const string RASDT_Gre = "GRE";

        #endregion

        /// <summary>
        /// Defines the maximum length of a device type.
        /// </summary>
        public const int RAS_MaxDeviceType = 16;

        /// <summary>
        /// Defines the maximum length of a phone number.
        /// </summary>
        public const int RAS_MaxPhoneNumber = 128;

        /// <summary>
        /// Defines the maximum length of an IP address.
        /// </summary>
        public const int RAS_MaxIpAddress = 15;

        /// <summary>
        /// Defines the maximum length of an IPX address.
        /// </summary>
        public const int RAS_MaxIpxAddress = 21;

        /// <summary>
        /// Defines the maximum length of an entry name.
        /// </summary>
        public const int RAS_MaxEntryName = 256;

        /// <summary>
        /// Defines the maximum length of a device name.
        /// </summary>
        public const int RAS_MaxDeviceName = 128;

        /// <summary>
        /// Defines the maximum length of a callback number.
        /// </summary>
        public const int RAS_MaxCallbackNumber = RAS_MaxPhoneNumber;

        /// <summary>
        /// Defines the maximum length of an area code.
        /// </summary>
        public const int RAS_MaxAreaCode = 10;

        /// <summary>
        /// Defines the maximum length of a pad type.
        /// </summary>
        public const int RAS_MaxPadType = 32;

        /// <summary>
        /// Defines the maximum length of an X25 address.
        /// </summary>
        public const int RAS_MaxX25Address = 200;

        /// <summary>
        /// Defines the maximum length of a facilities.
        /// </summary>
        public const int RAS_MaxFacilities = 200;

        /// <summary>
        /// Defines the maximum length of a user data.
        /// </summary>
        public const int RAS_MaxUserData = 200;

        /// <summary>
        /// Defines the maximum length of a reply message.
        /// </summary>
        public const int RAS_MaxReplyMessage = 1024;

        /// <summary>
        /// Defines the maximum length of a DNS suffix.
        /// </summary>
        public const int RAS_MaxDnsSuffix = 256;

        /// <summary>
        /// Defines the paused state for a RAS connection state.
        /// </summary>
        public const int RASCS_PAUSED = 0x1000;

        /// <summary>
        /// Defines the done state for a RAS connection state.
        /// </summary>
        public const int RASCS_DONE = 0x2000;

        /// <summary>
        /// Defines the done state for a RAS connection sub-state.
        /// </summary>
        public const int RASCSS_DONE = 0x2000;

        #endregion

        #region WinCred.h Constants

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Defines the maximum length of the caption for the credential user interface.
        /// </summary>
        public const int CREDUI_MAX_CAPTION_LENGTH = 128;

        /// <summary>
        /// Defines the maximum length of the message for the credential user interface.
        /// </summary>
        public const int CREDUI_MAX_MESSAGE_LENGTH = 32767;

        /// <summary>
        /// Defines the maximum length of a username for the credential user interface.
        /// </summary>
        public const int CREDUI_MAX_USERNAME_LENGTH = 256 + 1 + 256;
        
        /// <summary>
        /// Defines the maximum length of a username for the credential user interface.
        /// </summary>
        public const int CREDUI_MAX_PASSWORD_LENGTH = 512 / 2;

#endif

        #endregion

        #region Error Codes

        /// <summary>
        /// Defines the maximum length of a path.
        /// </summary>
        public const int MAX_PATH = 260;

        /// <summary>
        /// The operation was successful.
        /// </summary>
        public const int SUCCESS = 0;

        /// <summary>
        /// The system cannot find the file specified.
        /// </summary>
        public const int ERROR_FILE_NOT_FOUND = 2;

        /// <summary>
        /// The user did not have appropriate permissions to perform the requested action.
        /// </summary>
        public const int ERROR_ACCESS_DENIED = 5;

        /// <summary>
        /// The handle is invalid.
        /// </summary>
        public const int ERROR_INVALID_HANDLE = 6;

        /// <summary>
        /// The parameter is incorrect.
        /// </summary>
        public const int ERROR_INVALID_PARAMETER = 87;

        /////// <summary>
        /////// The filename, directory name, or volume label syntax is incorrect.
        /////// </summary>
        ////public const int ERROR_INVALID_NAME = 123;

        /// <summary>
        /// Cannot create a file when that file already exists.
        /// </summary>
        public const int ERROR_ALREADY_EXISTS = 183;

        /// <summary>
        /// The operation was cancelled by the user.
        /// </summary>
        public const int ERROR_CANCELLED = 1223;

        /////// <summary>
        /////// An operation is pending.
        /////// </summary>
        ////public const int PENDING = (RASBASE + 0);

        /////// <summary>
        /////// An invalid port handle was detected.
        /////// </summary>
        ////public const int ERROR_INVALID_PORT_HANDLE = (RASBASE + 1);

        /////// <summary>
        /////// The specified port is already open.
        /////// </summary>
        ////public const int ERROR_PORT_ALREADY_OPEN = (RASBASE + 2);

        /// <summary>
        /// The caller's buffer is too small.
        /// </summary>
        public const int ERROR_BUFFER_TOO_SMALL = RASBASE + 3;

        /////// <summary>
        /////// Incorrect information was specified.
        /////// </summary>
        ////public const int ERROR_WRONG_INFO_SPECIFIED = (RASBASE + 4);

        /////// <summary>
        /////// The port information cannot be set.
        /////// </summary>
        ////public const int ERROR_CANNOT_SET_PORT_INFO = (RASBASE + 5);

        /////// <summary>
        /////// The specified port is not connected.
        /////// </summary>
        ////public const int ERROR_PORT_NOT_CONNECTED = (RASBASE + 6);

        /////// <summary>
        /////// An invalid event was detected.
        /////// </summary>
        ////public const int ERROR_EVENT_INVALID = (RASBASE + 7);

        /////// <summary>
        /////// A device was specified that does not exist.
        /////// </summary>
        ////public const int ERROR_DEVICE_DOES_NOT_EXIST = (RASBASE + 8);

        /////// <summary>
        /////// A device type was specified that does not exist.
        /////// </summary>
        ////public const int ERROR_DEVICETYPE_DOES_NOT_EXIST = (RASBASE + 9);

        /////// <summary>
        /////// An invalid buffer was specified.
        /////// </summary>
        ////public const int ERROR_BUFFER_INVALID = (RASBASE + 10);

        /////// <summary>
        /////// A route was specified that is not available.
        /////// </summary>
        ////public const int ERROR_ROUTE_NOT_AVAILABLE = (RASBASE + 11);

        /////// <summary>
        /////// A route was specified that is not allocated.
        /////// </summary>
        ////public const int ERROR_ROUTE_NOT_ALLOCATED = (RASBASE + 12);

        /////// <summary>
        /////// An invalid compression was specified.
        /////// </summary>
        ////public const int ERROR_INVALID_COMPRESSION_SPECIFIED = (RASBASE + 13);

        /////// <summary>
        /////// There were insufficient buffers available.
        /////// </summary>
        ////public const int ERROR_OUT_OF_BUFFERS = (RASBASE + 14);

        /////// <summary>
        /////// The specified port was not found.
        /////// </summary>
        ////public const int ERROR_PORT_NOT_FOUND = (RASBASE + 15);

        /////// <summary>
        /////// An asynchronous request is pending.
        /////// </summary>
        ////public const int ERROR_ASYNC_REQUEST_PENDING = (RASBASE + 16);

        /////// <summary>
        /////// The modem (or other connecting device) is already disconnecting.
        /////// </summary>
        ////public const int ERROR_ALREADY_DISCONNECTING = (RASBASE + 17);

        /////// <summary>
        /////// The specified port is not open.
        /////// </summary>
        ////public const int ERROR_PORT_NOT_OPEN = (RASBASE + 18);

        /////// <summary>
        /////// A connection to the remote computer could not be established, so the port used for this connection was closed.
        /////// </summary>
        ////public const int ERROR_PORT_DISCONNECTED = (RASBASE + 19);

        /////// <summary>
        /////// No endpoints could be determined.
        /////// </summary>
        ////public const int ERROR_NO_ENDPOINTS = (RASBASE + 20);

        /// <summary>
        /// The system could not open the phone book file.
        /// </summary>
        public const int ERROR_CANNOT_OPEN_PHONEBOOK = RASBASE + 21;

        /////// <summary>
        /////// The system could not load the phone book file.
        /////// </summary>
        ////public const int ERROR_CANNOT_LOAD_PHONEBOOK = (RASBASE + 22);

        /// <summary>
        /// The system could not find the phone book entry for this connection.
        /// </summary>
        public const int ERROR_CANNOT_FIND_PHONEBOOK_ENTRY = RASBASE + 23;

        /////// <summary>
        /////// The system could not update the phone book file.
        /////// </summary>
        ////public const int ERROR_CANNOT_WRITE_PHONEBOOK = (RASBASE + 24);

        /////// <summary>
        /////// The system found invalid information in the phone book file.
        /////// </summary>
        ////public const int ERROR_CORRUPT_PHONEBOOK = (RASBASE + 25);

        /////// <summary>
        /////// A string could not be loaded.
        /////// </summary>
        ////public const int ERROR_CANNOT_LOAD_STRING = (RASBASE + 26);

        /////// <summary>
        /////// A key could not be found.
        /////// </summary>
        ////public const int ERROR_KEY_NOT_FOUND = (RASBASE + 27);

        /////// <summary>
        /////// The connection was terminated by the remote computer before it could be completed.
        /////// </summary>
        ////public const int ERROR_DISCONNECTION = (RASBASE + 28);

        /////// <summary>
        /////// The connection was closed by the remote computer.
        /////// </summary>
        ////public const int ERROR_REMOTE_DISCONNECTION = (RASBASE + 29);

        /////// <summary>
        /////// The modem (or other connecting device) was disconnected due to hardware failure.
        /////// </summary>
        ////public const int ERROR_HARDWARE_FAILURE = (RASBASE + 30);

        /////// <summary>
        /////// The user disconnected the modem (or other connecting device).
        /////// </summary>
        ////public const int ERROR_USER_DISCONNECTION = (RASBASE + 31);

        /////// <summary>
        /////// An incorrect structure size was detected.
        /////// </summary>
        ////public const int ERROR_INVALID_SIZE = (RASBASE + 32);

        /////// <summary>
        /////// The modem (or other connecting device) is already in use or is not configured properly.
        /////// </summary>
        ////public const int ERROR_PORT_NOT_AVAILABLE = (RASBASE + 33);

        /////// <summary>
        /////// Your computer could not be registered on the remote network.
        /////// </summary>
        ////public const int ERROR_CANNOT_PROJECT_CLIENT = (RASBASE + 34);

        /////// <summary>
        /////// There was an unknown error.
        /////// </summary>
        ////public const int ERROR_UNKNOWN = (RASBASE + 35);

        /////// <summary>
        /////// The device attached to the port is not the one expected.
        /////// </summary>
        ////public const int ERROR_WRONG_DEVICE_ATTACHED = (RASBASE + 36);

        /////// <summary>
        /////// A string was detected that could not be converted.
        /////// </summary>
        ////public const int ERROR_BAD_STRING = (RASBASE + 37);

        /////// <summary>
        /////// The remote server is not responding in a timely fashion.
        /////// </summary>
        ////public const int ERROR_REQUEST_TIMEOUT = (RASBASE + 38);

        /////// <summary>
        /////// No asynchronous net is available.
        /////// </summary>
        ////public const int ERROR_CANNOT_GET_LANA = (RASBASE + 39);

        /////// <summary>
        /////// An error has occurred involving NetBIOS.
        /////// </summary>
        ////public const int ERROR_NETBIOS_ERROR = (RASBASE + 40);

        /////// <summary>
        /////// The server cannot allocate NetBIOS resources needed to support the client.
        /////// </summary>
        ////public const int ERROR_SERVER_OUT_OF_RESOURCES = (RASBASE + 41);

        /////// <summary>
        /////// One of your computer's NetBIOS names is already registered on the remote network.
        /////// </summary>
        ////public const int ERROR_NAME_EXISTS_ON_NET = (RASBASE + 42);

        /////// <summary>
        /////// A network adapter at the server failed.
        /////// </summary>
        ////public const int ERROR_SERVER_GENERAL_NET_FAILURE = (RASBASE + 43);

        /////// <summary>
        /////// You will not receive network message popups.
        /////// </summary>
        ////public const int WARNING_MSG_ALIAS_NOT_ADDED = (RASBASE + 44);

        /////// <summary>
        /////// There was an internal authentication error.
        /////// </summary>
        ////public const int ERROR_AUTH_INTERNAL = (RASBASE + 45);

        /////// <summary>
        /////// The account is not permitted to log on at this time of day.
        /////// </summary>
        ////public const int ERROR_RESTRICTED_LOGON_HOURS = (RASBASE + 46);

        /////// <summary>
        /////// The account is disabled.
        /////// </summary>
        ////public const int ERROR_ACCT_DISABLED = (RASBASE + 47);

        /////// <summary>
        /////// The password for this account has expired.
        /////// </summary>
        ////public const int ERROR_PASSWD_EXPIRED = (RASBASE + 48);

        /////// <summary>
        /////// The account does not have permission to dial in.
        /////// </summary>
        ////public const int ERROR_NO_DIALIN_PERMISSION = (RASBASE + 49);

        /////// <summary>
        /////// The remote access server is not responding.
        /////// </summary>
        ////public const int ERROR_SERVER_NOT_RESPONDING = (RASBASE + 50);

        /////// <summary>
        /////// The modem (or other connecting device) has reported an error.
        /////// </summary>
        ////public const int ERROR_FROM_DEVICE = (RASBASE + 51);

        /////// <summary>
        /////// There was an unrecognized response from the modem (or other connecting device).
        /////// </summary>
        ////public const int ERROR_UNRECOGNIZED_RESPONSE = (RASBASE + 52);

        /////// <summary>
        /////// A macro required by the modem (or other connecting device) was not found in the device.INF file.
        /////// </summary>
        ////public const int ERROR_MACRO_NOT_FOUND = (RASBASE + 53);

        /////// <summary>
        /////// A command or response in the device.INF file section refers to an undefined macro.
        /////// </summary>
        ////public const int ERROR_MACRO_NOT_DEFINED = (RASBASE + 54);

        /////// <summary>
        /////// The message macro was not found in the device.INF file section.
        /////// </summary>
        ////public const int ERROR_MESSAGE_MACRO_NOT_FOUND = (RASBASE + 55);

        /////// <summary>
        /////// The defaultoff macro in the device.INF file section contains an undefined macro.
        /////// </summary>
        ////public const int ERROR_DEFAULTOFF_MACRO_NOT_FOUND = (RASBASE + 56);

        /////// <summary>
        /////// The device.INF file could not be opened.
        /////// </summary>
        ////public const int ERROR_FILE_COULD_NOT_BE_OPENED = (RASBASE + 57);

        /////// <summary>
        /////// The device name in the device.INF or media.INI file is too long.
        /////// </summary>
        ////public const int ERROR_DEVICENAME_TOO_LONG = (RASBASE + 58);

        /////// <summary>
        /////// The media.INI file refers to an unknown device name.
        /////// </summary>
        ////public const int ERROR_DEVICENAME_NOT_FOUND = (RASBASE + 59);

        /////// <summary>
        /////// The device.INF file contains no responses for the command.
        /////// </summary>
        ////public const int ERROR_NO_RESPONSES = (RASBASE + 60);

        /////// <summary>
        /////// The device.INF file is missing a command.
        /////// </summary>
        ////public const int ERROR_NO_COMMAND_FOUND = (RASBASE + 61);

        /////// <summary>
        /////// There was an attempt to set a macro not listed in device.INF file section.
        /////// </summary>
        ////public const int ERROR_WRONG_KEY_SPECIFIED = (RASBASE + 62);

        /////// <summary>
        /////// The media.INI file refers to an unknown device type.
        /////// </summary>
        ////public const int ERROR_UNKNOWN_DEVICE_TYPE = (RASBASE + 63);

        /////// <summary>
        /////// The system has run out of memory.
        /////// </summary>
        ////public const int ERROR_ALLOCATING_MEMORY = (RASBASE + 64);

        /////// <summary>
        /////// The modem (or other connecting device) is not properly configured.
        /////// </summary>
        ////public const int ERROR_PORT_NOT_CONFIGURED = (RASBASE + 65);

        /////// <summary>
        /////// The modem (or other connecting device) is not functioning.
        /////// </summary>
        ////public const int ERROR_DEVICE_NOT_READY = (RASBASE + 66);

        /////// <summary>
        /////// The system was unable to read the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_INI_FILE = (RASBASE + 67);

        /// <summary>
        /// The connection was terminated.
        /// </summary>
        public const int ERROR_NO_CONNECTION = RASBASE + 68;

        /////// <summary>
        /////// The usage parameter in the media.INI file is invalid.
        /////// </summary>
        ////public const int ERROR_BAD_USAGE_IN_INI_FILE = (RASBASE + 69);

        /////// <summary>
        /////// The system was unable to read the section name from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_SECTIONNAME = (RASBASE + 70);

        /////// <summary>
        /////// The system was unable to read the device type from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_DEVICETYPE = (RASBASE + 71);

        /////// <summary>
        /////// The system was unable to read the device name from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_DEVICENAME = (RASBASE + 72);

        /////// <summary>
        /////// The system was unable to read the usage from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_USAGE = (RASBASE + 73);

        /////// <summary>
        /////// The system was unable to read the maximum connection BPS rate from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_MAXCONNECTBPS = (RASBASE + 74);

        /////// <summary>
        /////// The system was unable to read the maximum carrier connection speed from the media.INI file.
        /////// </summary>
        ////public const int ERROR_READING_MAXCARRIERBPS = (RASBASE + 75);

        /////// <summary>
        /////// The phone line is busy.
        /////// </summary>
        ////public const int ERROR_LINE_BUSY = (RASBASE + 76);

        /////// <summary>
        /////// A person answered instead of a modem (or other connecting device).
        /////// </summary>
        ////public const int ERROR_VOICE_ANSWER = (RASBASE + 77);

        /////// <summary>
        /////// The remote computer did not respond.
        /////// </summary>
        ////public const int ERROR_NO_ANSWER = (RASBASE + 78);

        /////// <summary>
        /////// The system could not detect the carrier.
        /////// </summary>
        ////public const int ERROR_NO_CARRIER = (RASBASE + 79);

        /////// <summary>
        /////// There was no dial tone.
        /////// </summary>
        ////public const int ERROR_NO_DIALTONE = (RASBASE + 80);

        /////// <summary>
        /////// The modem (or other connecting device) reported a general error.
        /////// </summary>
        ////public const int ERROR_IN_COMMAND = (RASBASE + 81);

        /////// <summary>
        /////// There was an error in writing the section name.
        /////// </summary>
        ////public const int ERROR_WRITING_SECTIONNAME = (RASBASE + 82);

        /////// <summary>
        /////// There was an error in writing the device type.
        /////// </summary>
        ////public const int ERROR_WRITING_DEVICETYPE = (RASBASE + 83);

        /////// <summary>
        /////// There was an error in writing the device name.
        /////// </summary>
        ////public const int ERROR_WRITING_DEVICENAME = (RASBASE + 84);

        /////// <summary>
        /////// There was an error in writing the maximum connection speed.
        /////// </summary>
        ////public const int ERROR_WRITING_MAXCONNECTBPS = (RASBASE + 85);

        /////// <summary>
        /////// There was an error in writing the maximum carrier speed.
        /////// </summary>
        ////public const int ERROR_WRITING_MAXCARRIERBPS = (RASBASE + 86);

        /////// <summary>
        /////// There was an error in writing the usage.
        /////// </summary>
        ////public const int ERROR_WRITING_USAGE = (RASBASE + 87);

        /////// <summary>
        /////// There was an error in writing the default-off.
        /////// </summary>
        ////public const int ERROR_WRITING_DEFAULTOFF = (RASBASE + 88);

        /////// <summary>
        /////// There was an error in reading the default-off.
        /////// </summary>
        ////public const int ERROR_READING_DEFAULTOFF = (RASBASE + 89);

        /////// <summary>
        /////// The INI file was empty.
        /////// </summary>
        ////public const int ERROR_EMPTY_INI_FILE = (RASBASE + 90);

        /////// <summary>
        /////// Access was denied because the username and/or password was invalid on the domain.
        /////// </summary>
        ////public const int ERROR_AUTHENTICATION_FAILURE = (RASBASE + 91);

        /////// <summary>
        /////// There was a hardware failure in the modem (or other connecting device).
        /////// </summary>
        ////public const int ERROR_PORT_OR_DEVICE = (RASBASE + 92);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_NOT_BINARY_MACRO = (RASBASE + 93);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_DCB_NOT_FOUND = (RASBASE + 94);

        /////// <summary>
        /////// The state machines are not started.
        /////// </summary>
        ////public const int ERROR_STATE_MACHINES_NOT_STARTED = (RASBASE + 95);

        /////// <summary>
        /////// The state machines are already started.
        /////// </summary>
        ////public const int ERROR_STATE_MACHINES_ALREADY_STARTED = (RASBASE + 96);

        /////// <summary>
        /////// The response looping did not complete.
        /////// </summary>
        ////public const int ERROR_PARTIAL_RESPONSE_LOOPING = (RASBASE + 97);

        /////// <summary>
        /////// A response keyname in the device.INF file is not in the expected format.
        /////// </summary>
        ////public const int ERROR_UNKNOWN_RESPONSE_KEY = (RASBASE + 98);

        /////// <summary>
        /////// The modem (or other connecting device) response caused a buffer overflow.
        /////// </summary>
        ////public const int ERROR_RECV_BUF_FULL = (RASBASE + 99);

        /////// <summary>
        /////// The expanded command in the device.INF file is too long.
        /////// </summary>
        ////public const int ERROR_CMD_TOO_LONG = (RASBASE + 100);

        /////// <summary>
        /////// The modem moved to a connection speed not supported by the COM driver.
        /////// </summary>
        ////public const int ERROR_UNSUPPORTED_BPS = (RASBASE + 101);

        /////// <summary>
        /////// Device response received when none expected.
        /////// </summary>
        ////public const int ERROR_UNEXPECTED_RESPONSE = (RASBASE + 102);

        /// <summary>
        /// The connection needs information from you, but the application does not allow user interaction.
        /// </summary>
        public const int ERROR_INTERACTIVE_MODE = RASBASE + 103;

        /////// <summary>
        /////// The callback number is invalid.
        /////// </summary>
        ////public const int ERROR_BAD_CALLBACK_NUMBER = (RASBASE + 104);

        /////// <summary>
        /////// The authorization state is invalid.
        /////// </summary>
        ////public const int ERROR_INVALID_AUTH_STATE = (RASBASE + 105);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_WRITING_INITBPS = (RASBASE + 106);

        /////// <summary>
        /////// There was an error related to the X.25 protocol.
        /////// </summary>
        ////public const int ERROR_X25_DIAGNOSTIC = (RASBASE + 107);

        /////// <summary>
        /////// The account has expired.
        /////// </summary>
        ////public const int ERROR_ACCT_EXPIRED = (RASBASE + 108);

        /////// <summary>
        /////// There was an error changing the password on the domain. The password might have been too short or might 
        /////// have matched a previously used password.
        /////// </summary>
        ////public const int ERROR_CHANGING_PASSWORD = (RASBASE + 109);

        /////// <summary>
        /////// Serial overrun errors were detected while communicating with the modem.
        /////// </summary>
        ////public const int ERROR_OVERRUN = (RASBASE + 110);

        /////// <summary>
        /////// A configuration error on this computer is preventing this connection.
        /////// </summary>
        ////public const int ERROR_RASMAN_CANNOT_INITIALIZE = (RASBASE + 111);

        /////// <summary>
        /////// The two-way port is initializing. Wait a few seconds and redial.
        /////// </summary>
        ////public const int ERROR_BIPLEX_PORT_NOT_AVAILABLE = (RASBASE + 112);

        /////// <summary>
        /////// No active ISDN lines are available.
        /////// </summary>
        ////public const int ERROR_NO_ACTIVE_ISDN_LINES = (RASBASE + 113);

        /////// <summary>
        /////// No ISDN channels are available to make the call.
        /////// </summary>
        ////public const int ERROR_NO_ISDN_CHANNELS_AVAILABLE = (RASBASE + 114);

        /////// <summary>
        /////// Too many errors occurred because of poor phone line quality.
        /////// </summary>
        ////public const int ERROR_TOO_MANY_LINE_ERRORS = (RASBASE + 115);

        /////// <summary>
        /////// The remote access service IP configuration is unusable.
        /////// </summary>
        ////public const int ERROR_IP_CONFIGURATION = (RASBASE + 116);

        /////// <summary>
        /////// No IP addresses are available in the static pool of remote access service IP addresses.
        /////// </summary>
        ////public const int ERROR_NO_IP_ADDRESSES = (RASBASE + 117);

        /////// <summary>
        /////// The connection was terminated because the remote computer did not respond in a timely manner.
        /////// </summary>
        ////public const int ERROR_PPP_TIMEOUT = (RASBASE + 118);

        /////// <summary>
        /////// The connection was terminated by the remote computer.
        /////// </summary>
        ////public const int ERROR_PPP_REMOTE_TERMINATED = (RASBASE + 119);

        /////// <summary>
        /////// A connection to the remote computer could not be established. You might need to change the network 
        /////// settings for this connection.
        /////// </summary>
        ////public const int ERROR_PPP_NO_PROTOCOLS_CONFIGURED = (RASBASE + 120);

        /////// <summary>
        /////// The remote computer did not respond.
        /////// </summary>
        ////public const int ERROR_PPP_NO_RESPONSE = (RASBASE + 121);

        /////// <summary>
        /////// Invalid data was received from the remote computer. This data was ignored.
        /////// </summary>
        ////public const int ERROR_PPP_INVALID_PACKET = (RASBASE + 122);

        /////// <summary>
        /////// The phone number, including prefix and suffix, is too long.
        /////// </summary>
        ////public const int ERROR_PHONE_NUMBER_TOO_LONG = (RASBASE + 123);

        /////// <summary>
        /////// The IPX protocol cannot dial out on the modem (or other connecting device) because this computer is 
        /////// not configured for dialing out (it is an IPX router).
        /////// </summary>
        ////public const int ERROR_IPXCP_NO_DIALOUT_CONFIGURED = (RASBASE + 124);

        /////// <summary>
        /////// The IPX protocol cannot dial in on the modem (or other connecting device) because this computer is 
        /////// not configured for dialing in (the IPX router is not installed).
        /////// </summary>
        ////public const int ERROR_IPXCP_NO_DIALIN_CONFIGURED = (RASBASE + 125);

        /////// <summary>
        /////// The IPX protocol cannot be used for dialing out on more than one modem (or other connecting device) at a time.
        /////// </summary>
        ////public const int ERROR_IPXCP_DIALOUT_ALREADY_ACTIVE = (RASBASE + 126);

        /////// <summary>
        /////// Cannot access TCPCFG.DLL.
        /////// </summary>
        ////public const int ERROR_ACCESSING_TCPCFGDLL = (RASBASE + 127);

        /////// <summary>
        /////// The system cannot find an IP adapter.
        /////// </summary>
        ////public const int ERROR_NO_IP_RAS_ADAPTER = (RASBASE + 128);

        /////// <summary>
        /////// SLIP cannot be used unless the IP protocol is installed.
        /////// </summary>
        ////public const int ERROR_SLIP_REQUIRES_IP = (RASBASE + 129);

        /////// <summary>
        /////// Computer registration is not complete.
        /////// </summary>
        ////public const int ERROR_PROJECTION_NOT_COMPLETE = (RASBASE + 130);

        /// <summary>
        /// The protocol is not configured.
        /// </summary>
        public const int ERROR_PROTOCOL_NOT_CONFIGURED = RASBASE + 131;

        /////// <summary>
        /////// Your computer and the remote computer could not agree on PPP control protocols.
        /////// </summary>
        ////public const int ERROR_PPP_NOT_CONVERGING = (RASBASE + 132);

        /////// <summary>
        /////// A connection to the remote computer could not be completed. You might need to adjust the protocols on 
        /////// this computer.
        /////// </summary>
        ////public const int ERROR_PPP_CP_REJECTED = (RASBASE + 133);

        /////// <summary>
        /////// The PPP link control protocol was terminated.
        /////// </summary>
        ////public const int ERROR_PPP_LCP_TERMINATED = (RASBASE + 134);

        /////// <summary>
        /////// The requested address was rejected by the server.
        /////// </summary>
        ////public const int ERROR_PPP_REQUIRED_ADDRESS_REJECTED = (RASBASE + 135);

        /////// <summary>
        /////// The remote computer terminated the control protocol.
        /////// </summary>
        ////public const int ERROR_PPP_NCP_TERMINATED = (RASBASE + 136);

        /////// <summary>
        /////// Loopback was detected.
        /////// </summary>
        ////public const int ERROR_PPP_LOOPBACK_DETECTED = (RASBASE + 137);

        /////// <summary>
        /////// The server did not assign an address.
        /////// </summary>
        ////public const int ERROR_PPP_NO_ADDRESS_ASSIGNED = (RASBASE + 138);

        /////// <summary>
        /////// The authentication protocol required by the remote server cannot use the stored password.
        /////// </summary>
        ////public const int ERROR_CANNOT_USE_LOGON_CREDENTIALS = (RASBASE + 139);

        /////// <summary>
        /////// An invalid dialing rule was detected.
        /////// </summary>
        ////public const int ERROR_TAPI_CONFIGURATION = (RASBASE + 140);

        /////// <summary>
        /////// The local computer does not support the required data encryption type.
        /////// </summary>
        ////public const int ERROR_NO_LOCAL_ENCRYPTION = (RASBASE + 141);

        /////// <summary>
        /////// The remote computer does not support the required data encryption type.
        /////// </summary>
        ////public const int ERROR_REMOTE_ENCRYPTION = (RASBASE + 142);

        /////// <summary>
        /////// The remote computer requires data encryption.
        /////// </summary>
        ////public const int ERROR_REMOTE_REQUIRES_ENCRYPTION = (RASBASE + 143);

        /////// <summary>
        /////// The system cannot use the IPX network number assigned by the remote computer.
        /////// </summary>
        ////public const int ERROR_IPXCP_NET_NUMBER_CONFLICT = (RASBASE + 144);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_INVALID_SMM = (RASBASE + 145);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_SMM_UNINITIALIZED = (RASBASE + 146);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_NO_MAC_FOR_PORT = (RASBASE + 147);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_SMM_TIMEOUT = (RASBASE + 148);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_BAD_PHONE_NUMBER = (RASBASE + 149);

        /////// <summary>
        /////// Undocumented value.
        /////// </summary>
        ////public const int ERROR_WRONG_MODULE = (RASBASE + 150);

        /////// <summary>
        /////// The callback number contains an invalid character.
        /////// </summary>
        ////public const int ERROR_INVALID_CALLBACK_NUMBER = (RASBASE + 151);

        /////// <summary>
        /////// A syntax error was encountered while processing a script.
        /////// </summary>
        ////public const int ERROR_SCRIPT_SYNTAX = (RASBASE + 152);

        /////// <summary>
        /////// The connection could not be disconnected because it was created by the multi-protocol router.
        /////// </summary>
        ////public const int ERROR_HANGUP_FAILED = (RASBASE + 153);

        /////// <summary>
        /////// The system could not find the multi-link bundle.
        /////// </summary>
        ////public const int ERROR_BUNDLE_NOT_FOUND = (RASBASE + 154);

        /////// <summary>
        /////// The system cannot perform automated dial because this connection has a custom dialer specified.
        /////// </summary>
        ////public const int ERROR_CANNOT_DO_CUSTOMDIAL = (RASBASE + 155);

        /////// <summary>
        /////// This connection is already being dialed.
        /////// </summary>
        ////public const int ERROR_DIAL_ALREADY_IN_PROGRESS = (RASBASE + 156);

        /////// <summary>
        /////// Remote Access Services could not be started automatically.
        /////// </summary>
        ////public const int ERROR_RASAUTO_CANNOT_INITIALIZE = (RASBASE + 157);

        /////// <summary>
        /////// Internet Connection Sharing is already enabled on the connection.
        /////// </summary>
        ////public const int ERROR_CONNECTION_ALREADY_SHARED = (RASBASE + 158);

        /////// <summary>
        /////// An error occurred while the existing Internet Connection Sharing settings were being changed.
        /////// </summary>
        ////public const int ERROR_SHARING_CHANGE_FAILED = (RASBASE + 159);

        /////// <summary>
        /////// An error occurred while routing capabilities were being enabled.
        /////// </summary>
        ////public const int ERROR_SHARING_ROUTER_INSTALL = (RASBASE + 160);

        /////// <summary>
        /////// An error occurred while Internet Connection Sharing was being enabled for the connection.
        /////// </summary>
        ////public const int ERROR_SHARE_CONNECTION_FAILED = (RASBASE + 161);

        /////// <summary>
        /////// An error occurred while the local network was being configured for sharing.
        /////// </summary>
        ////public const int ERROR_SHARING_PRIVATE_INSTALL = (RASBASE + 162);

        /////// <summary>
        /////// Internet Connection Sharing cannot be enabled.  There is more than one LAN connection other than the 
        /////// connection to be shared.
        /////// </summary>
        ////public const int ERROR_CANNOT_SHARE_CONNECTION = (RASBASE + 163);

        /////// <summary>
        /////// No smart card reader is installed.
        /////// </summary>
        ////public const int ERROR_NO_SMART_CARD_READER = (RASBASE + 164);

        /////// <summary>
        /////// Internet Connection Sharing cannot be enabled. A LAN connection is already configured with the IP address 
        /////// that is required for automatic IP addressing.
        /////// </summary>
        ////public const int ERROR_SHARING_ADDRESS_EXISTS = (RASBASE + 165);

        /////// <summary>
        /////// A certificate could not be found. Connections that use the L2TP protocol over IPSec require the installation 
        /////// of a machine certificate, also known as a computer certificate.
        /////// </summary>
        ////public const int ERROR_NO_CERTIFICATE = (RASBASE + 166);

        /////// <summary>
        /////// Internet Connection Sharing cannot be enabled. The LAN connection selected as the private network has more 
        /////// than one IP address configured.  Please reconfigure the LAN connection with a single IP address before 
        /////// enabling Internet Connection Sharing.
        /////// </summary>
        ////public const int ERROR_SHARING_MULTIPLE_ADDRESSES = (RASBASE + 167);

        /////// <summary>
        /////// The connection attempt failed because of failure to encrypt data.
        /////// </summary>
        ////public const int ERROR_FAILED_TO_ENCRYPT = (RASBASE + 168);

        /////// <summary>
        /////// The specified destination is not reachable.
        /////// </summary>
        ////public const int ERROR_BAD_ADDRESS_SPECIFIED = (RASBASE + 169);

        /////// <summary>
        /////// The remote computer rejected the connection attempt.
        /////// </summary>
        ////public const int ERROR_CONNECTION_REJECT = (RASBASE + 170);

        /////// <summary>
        /////// The connection attempt failed because the network is busy.
        /////// </summary>
        ////public const int ERROR_CONGESTION = (RASBASE + 171);

        /////// <summary>
        /////// The remote computer's network hardware is incompatible with the type of call requested.
        /////// </summary>
        ////public const int ERROR_INCOMPATIBLE = (RASBASE + 172);

        /////// <summary>
        /////// The connection attempt failed because the destination number has changed.
        /////// </summary>
        ////public const int ERROR_NUMBERCHANGED = (RASBASE + 173);

        /////// <summary>
        /////// The connection attempt failed because of a temporary failure.
        /////// </summary>
        ////public const int ERROR_TEMPFAILURE = (RASBASE + 174);

        /////// <summary>
        /////// The call was blocked by the remote computer.
        /////// </summary>
        ////public const int ERROR_BLOCKED = (RASBASE + 175);

        /////// <summary>
        /////// The call could not be connected because the remote computer has invoked the Do Not Disturb feature.
        /////// </summary>
        ////public const int ERROR_DONOTDISTURB = (RASBASE + 176);

        /////// <summary>
        /////// The connection attempt failed because the modem (or other connecting device) on the remote computer is out of order.
        /////// </summary>
        ////public const int ERROR_OUTOFORDER = (RASBASE + 177);

        /////// <summary>
        /////// It was not possible to verify the identity of the server.
        /////// </summary>
        ////public const int ERROR_UNABLE_TO_AUTHENTICATE_SERVER = (RASBASE + 178);

        /////// <summary>
        /////// To dial out using this connection you must use a smart card.
        /////// </summary>
        ////public const int ERROR_SMART_CARD_REQUIRED = (RASBASE + 179);

        /// <summary>
        /// An attempted function is not valid for this connection.
        /// </summary>
        public const int ERROR_INVALID_FUNCTION_FOR_ENTRY = RASBASE + 180;

        /////// <summary>
        /////// The connection requires a certificate, and no valid certificate was found.
        /////// </summary>
        ////public const int ERROR_CERT_FOR_ENCRYPTION_NOT_FOUND = (RASBASE + 181);

        /////// <summary>
        /////// Internet Connection Sharing (ICS) and Internet Connection Firewall (ICF) cannot be enabled because
        /////// Routing and Remote Access has been enabled on this computer.
        /////// </summary>
        ////public const int ERROR_SHARING_RRAS_CONFLICT = (RASBASE + 182);

        /////// <summary>
        /////// Internet Connection Sharing cannot be enabled. The LAN connection selected as the private network is 
        /////// either not present, or is disconnected from the network.
        /////// </summary>
        ////public const int ERROR_SHARING_NO_PRIVATE_LAN = (RASBASE + 183);

        /////// <summary>
        /////// You cannot dial using this connection at logon time, because it is configured to use a user name different 
        /////// than the one on the smart card.
        /////// </summary>
        ////public const int ERROR_NO_DIFF_USER_AT_LOGON = (RASBASE + 184);

        /////// <summary>
        /////// You cannot dial using this connection at logon time, because it is not configured to use a smart card.
        /////// </summary>
        ////public const int ERROR_NO_REG_CERT_AT_LOGON = (RASBASE + 185);

        /////// <summary>
        /////// The L2TP connection attempt failed because there is no valid machine certificate on your computer for 
        /////// security authentication.
        /////// </summary>
        ////public const int ERROR_OAKLEY_NO_CERT = (RASBASE + 186);

        /////// <summary>
        /////// The L2TP connection attempt failed because the security layer could not authenticate the remote computer.
        /////// </summary>
        ////public const int ERROR_OAKLEY_AUTH_FAIL = (RASBASE + 187);

        /////// <summary>
        /////// The L2TP connection attempt failed because the security layer could not negotiate compatible parameters 
        /////// with the remote computer.
        /////// </summary>
        ////public const int ERROR_OAKLEY_ATTRIB_FAIL = (RASBASE + 188);

        /////// <summary>
        /////// The L2TP connection attempt failed because the security layer encountered a processing error during initial 
        /////// negotiations with the remote computer.
        /////// </summary>
        ////public const int ERROR_OAKLEY_GENERAL_PROCESSING = (RASBASE + 189);

        /////// <summary>
        /////// The L2TP connection attempt failed because certificate validation on the remote computer failed.
        /////// </summary>
        ////public const int ERROR_OAKLEY_NO_PEER_CERT = (RASBASE + 190);

        /////// <summary>
        /////// The L2TP connection attempt failed because security policy for the connection was not found.
        /////// </summary>
        ////public const int ERROR_OAKLEY_NO_POLICY = (RASBASE + 191);

        /////// <summary>
        /////// The L2TP connection attempt failed because security negotiation timed out.
        /////// </summary>
        ////public const int ERROR_OAKLEY_TIMED_OUT = (RASBASE + 192);

        /////// <summary>
        /////// The L2TP connection attempt failed because an error occurred while negotiating security.
        /////// </summary>
        ////public const int ERROR_OAKLEY_ERROR = (RASBASE + 193);

        /////// <summary>
        /////// The Framed Protocol RADIUS attribute for this user is not PPP.
        /////// </summary>
        ////public const int ERROR_UNKNOWN_FRAMED_PROTOCOL = (RASBASE + 194);

        /////// <summary>
        /////// The Tunnel Type RADIUS attribute for this user is not correct.
        /////// </summary>
        ////public const int ERROR_WRONG_TUNNEL_TYPE = (RASBASE + 195);

        /////// <summary>
        /////// The Service Type RADIUS attribute for this user is neither Framed nor Callback Framed.
        /////// </summary>
        ////public const int ERROR_UNKNOWN_SERVICE_TYPE = (RASBASE + 196);

        /////// <summary>
        /////// A connection to the remote computer could not be established because the modem was not found or was busy.
        /////// </summary>
        ////public const int ERROR_CONNECTING_DEVICE_NOT_FOUND = (RASBASE + 197);

        /////// <summary>
        /////// A certificate could not be found that can be used with this Extensible Authentication Protocol.
        /////// </summary>
        ////public const int ERROR_NO_EAPTLS_CERTIFICATE = (RASBASE + 198);

        /////// <summary>
        /////// Internet Connection Sharing (ICS) cannot be enabled due to an IP address conflict on the network.
        /////// </summary>
        ////public const int ERROR_SHARING_HOST_ADDRESS_CONFLICT = (RASBASE + 199);

        /////// <summary>
        /////// Unable to establish the VPN connection. The VPN server may be unreachable, or security parameters may not 
        /////// be configured properly for this connection.
        /////// </summary>
        ////public const int ERROR_AUTOMATIC_VPN_FAILED = (RASBASE + 200);

        /////// <summary>
        /////// This connection is configured to validate the identity of the access server, but Windows cannot verify the digital 
        /////// certificate sent by the server.
        /////// </summary>
        ////public const int ERROR_VALIDATING_SERVER_CERT = (RASBASE + 201);

        /////// <summary>
        /////// The card supplied was not recognized. Please check that the card is inserted correctly, and fits tightly.
        /////// </summary>
        ////public const int ERROR_READING_SCARD = (RASBASE + 202);

        /////// <summary>
        /////// The PEAP configuration stored in the session cookie does not match the current session configuration.
        /////// </summary>
        ////public const int ERROR_INVALID_PEAP_COOKIE_CONFIG = (RASBASE + 203);

        /////// <summary>
        /////// The PEAP identity stored in the session cookie does not match the current identity.
        /////// </summary>
        ////public const int ERROR_INVALID_PEAP_COOKIE_USER = (RASBASE + 204);

        /////// <summary>
        /////// You cannot dial using this connection at logon time, because it is configured to use logged on user's credentials.
        /////// </summary>
        ////public const int ERROR_INVALID_MSCHAPV2_CONFIG = (RASBASE + 205);

        /////// <summary>
        /////// The VPN connection between your computer and the VPN server could not be completed.
        /////// </summary>
        ////public const int ERROR_VPN_GRE_BLOCKED = (RASBASE + 206);

        /////// <summary>
        /////// The network connection between your computer and the VPN server was interrupted.
        /////// </summary>
        ////public const int ERROR_VPN_DISCONNECT = (RASBASE + 207);

        /////// <summary>
        /////// The network connection between your computer and the VPN server could not be established because the remote server refused the connection.
        /////// </summary>
        ////public const int ERROR_VPN_REFUSED = (RASBASE + 208);

        /////// <summary>
        /////// The network connection between your computer and the VPN server could not be established because the remote server is not responding.
        /////// </summary>
        ////public const int ERROR_VPN_TIMEOUT = (RASBASE + 209);

        /////// <summary>
        /////// A network connection between your computer and the VPN server was started, but the VPN connection was not completed.
        /////// </summary>
        ////public const int ERROR_VPN_BAD_CERT = (RASBASE + 210);

        /////// <summary>
        /////// The network conection between your computer and the VPN server could not be established because the remote server is not responding.
        /////// </summary>
        ////public const int ERROR_VPN_BAD_PSK = (RASBASE + 211);

        /////// <summary>
        /////// The connection was prevented because of a policy configured on your RAS/VPN server.
        /////// </summary>
        ////public const int ERROR_SERVER_POLICY = (RASBASE + 212);

        /////// <summary>
        /////// You have attempted to establish a second broadband connection while a previous broadband connection is already established using the same device or port.
        /////// </summary>
        ////public const int ERROR_BROADBAND_ACTIVE = (RASBASE + 213);

        /////// <summary>
        /////// The underlying Ethernet connectivity required for the broadband connection was not found.
        /////// </summary>
        ////public const int ERROR_BROADBAND_NO_NIC = (RASBASE + 214);

        /////// <summary>
        /////// The broadband network conection could not be established on your computer because the remote server is not responding.
        /////// </summary>
        ////public const int ERROR_BROADBAND_TIMEOUT = (RASBASE + 215);

        /////// <summary>
        /////// A feature or setting you have tried to enable is no longer supported by the remote access service.
        /////// </summary>
        ////public const int ERROR_FEATURE_DEPRECATED = (RASBASE + 216);

        /////// <summary>
        /////// Cannot delete a connection while it is connected.
        /////// </summary>
        ////public const int ERROR_CANNOT_DELETE = (RASBASE + 217);

        /////// <summary>
        /////// The Network Access Protection (NAP) enforcement client could not create system resources for remote access connections.
        /////// </summary>
        ////public const int ERROR_RASQEC_RESOURCE_CREATION_FAILED = (RASBASE + 218);

        /////// <summary>
        /////// The Network Access Protection Agent (NAPAgent) service has been disabled or is not installed on this computer.
        /////// </summary>
        ////public const int ERROR_RASQEC_NAPAGENT_NOT_ENABLED = (RASBASE + 219);

        /////// <summary>
        /////// The Network Access Protection (NAP) enforcement client failed to register with the Network Access Protection Agent (NAPAgent) service.
        /////// </summary>
        ////public const int ERROR_RASQEC_NAPAGENT_NOT_CONNECTED = (RASBASE + 220);

        /////// <summary>
        /////// The Network Access Protection (NAP) enforcement client was unable to process the request because the remote access connection does not exist.
        /////// </summary>
        ////public const int ERROR_RASQEC_CONN_DOESNOTEXIST = (RASBASE + 221);

        /////// <summary>
        /////// The Network Access Protection (NAP) enforcement client did not respond.
        /////// </summary>
        ////public const int ERROR_RASQEC_TIMEOUT = (RASBASE + 222);

        /////// <summary>
        /////// Received Crypto-Binding TLV is invalid.
        /////// </summary>
        ////public const int ERROR_PEAP_CRYPTOBINDING_INVALID = (RASBASE + 223);

        /////// <summary>
        /////// Crypto-Binding TLV is not received.
        /////// </summary>
        ////public const int ERROR_PEAP_CRYPTOBINDING_NOTRECEIVED = (RASBASE + 224);

        /////// <summary>
        /////// Point-to-Point Tunneling Protocol (PPTP) is incompatible with IPv6.
        /////// </summary>
        ////public const int ERROR_INVALID_VPNSTRATEGY = (RASBASE + 225);

        /////// <summary>
        /////// EAPTLS validation of the cached credentials failed.
        /////// </summary>
        ////public const int ERROR_EAPTLS_CACHE_CREDENTIALS_INVALID = (RASBASE + 226);

        /////// <summary>
        /////// The L2TP/IPsec connection cannot be completed because the IKE and AuthIP IPSec Keying Modules service and/or the Base Filtering Engine service is not running.
        /////// </summary>
        ////public const int ERROR_IPSEC_SERVICE_STOPPED = (RASBASE + 227);

        /////// <summary>
        /////// The connection was terminated because of idle timeout.
        /////// </summary>
        ////public const int ERROR_IDLE_TIMEOUT = (RASBASE + 228);

        /////// <summary>
        /////// The modem (or other connecting device) was disconnected due to link failure.
        /////// </summary>
        ////public const int ERROR_LINK_FAILURE = (RASBASE + 229);

        /////// <summary>
        /////// The connection was terminated because user logged off.
        /////// </summary>
        ////public const int ERROR_USER_LOGOFF = (RASBASE + 230);

        /////// <summary>
        /////// The connection was terminated because user switch happened.
        /////// </summary>
        ////public const int ERROR_FAST_USER_SWITCH = (RASBASE + 231);

        /////// <summary>
        /////// The connection was terminated because of hibernation.
        /////// </summary>
        ////public const int ERROR_HIBERNATION = (RASBASE + 232);

        /////// <summary>
        /////// The connection was terminated because the system got suspended.
        /////// </summary>
        ////public const int ERROR_SYSTEM_SUSPENDED = (RASBASE + 233);

        /////// <summary>
        /////// The connection was terminated because Remote Access Connection manager stopped.
        /////// </summary>
        ////public const int ERROR_RASMAN_SERVICE_STOPPED = (RASBASE + 234);

        /////// <summary>
        /////// The L2TP connection attempt failed because the security layer could not authenticate the remote computer.
        /////// </summary>
        ////public const int ERROR_INVALID_SERVER_CERT = (RASBASE + 235);

        /// <summary>
        /// The connection is not configured for network access protection.
        /// </summary>
        public const int ERROR_NOT_NAP_CAPABLE = RASBASE + 236;

        #endregion

        /// <summary>
        /// Defines the invalid handle value.
        /// </summary>
        public static readonly RasHandle INVALID_HANDLE_VALUE = new RasHandle(new IntPtr(-1), false);

        /// <summary>
        /// Defines the base for all RAS error codes.
        /// </summary>
        private const int RASBASE = 600;

        #endregion

        #region Delegates

        /// <summary>
        /// The callback used by the RasDial function when a change of state occurs during a remote access connection process.
        /// </summary>
        /// <param name="callbackId">An application defined value that was passed to the remote access service.</param>
        /// <param name="subEntryId">The one-based subentry index for the phone book entry associated with this connection.</param>
        /// <param name="handle">The handle to the connection.</param>
        /// <param name="message">The type of event that has occurred.</param>
        /// <param name="state">The state the remote access connection process is about to enter.</param>
        /// <param name="errorCode">The error that has occurred. If no error has occurred the value is zero.</param>
        /// <param name="extendedErrorCode">Any extended error information for certain non-zero values of <paramref name="errorCode"/>.</param>
        /// <returns><b>true</b> to continue to receive callback notifications, otherwise <b>false</b>.</returns>
        public delegate bool RasDialFunc2(IntPtr callbackId, int subEntryId, IntPtr handle, int message, RasConnectionState state, int errorCode, int extendedErrorCode);

        /// <summary>
        /// The callback used by the RasPhonebookDlg function that receives notifications of user activity while the dialog box is open.
        /// </summary>
        /// <param name="callbackId">An application defined value that was passed to the RasPhonebookDlg function.</param>
        /// <param name="eventType">The event that occurred.</param>
        /// <param name="text">A string whose value depends on the <paramref name="eventType"/> parameter.</param>
        /// <param name="data">Pointer to an additional buffer argument whose value depends on the <paramref name="eventType"/> parameter.</param>
        public delegate void RasPBDlgFunc(IntPtr callbackId, RASPBDEVENT eventType, string text, IntPtr data);

        #endregion

        #region Enums

        #region CREDUI_FLAGS

        /// <summary>
        /// Defines the credential user interface flags.
        /// </summary>
        [Flags]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum CREDUI_FLAGS
        {
            /// <summary>
            /// No flags have been specified.
            /// </summary>
            None = 0x00,
            
            /// <summary>
            /// Notify the user of insufficient credentials by displaying the 'Logon unsuccessful' balloon tip.
            /// </summary>
            IncorrectPassword = 0x01,

            /// <summary>
            /// Do not store credentials or display check boxes. You can pass CREDUI_FLAGS_SHOW_SAVE_CHECK_BOX with this flag to display the Save check box only, and the result is returned in the pfSave output parameter.
            /// </summary>
            DoNotPersist = 0x02,

            /// <summary>
            /// Populate the combo box with local administrators only.
            /// </summary>
            RequestAdministrator = 0x04,
            
            /// <summary>
            /// Populate the combo box with user name/password only. Do not display certificates or smart cards in the combo box.
            /// </summary>
            ExcludeCertificates = 0x08,

            /// <summary>
            /// Populate the combo box with certificates and smart cards only. Do not allow a user name to be entered.
            /// </summary>
            RequireCertificate = 0x10,

            /// <summary>
            /// If the check box is selected, show the Save check box and return TRUE in the pfSave output parameter, otherwise, return FALSE. Check box uses the value in pfSave by default.
            /// </summary>
            ShowSaveCheckBox = 0x40,

            /// <summary>
            /// Specifies that a user interface will be shown even if the credentials can be returned from an existing credential in credential manager. This flag is permitted only if CREDUI_FLAGS_GENERIC_CREDENTIALS is also specified.
            /// </summary>
            AlwaysShowUI = 0x80,

            /// <summary>
            /// Populate the combo box with certificates or smart cards only. Do not allow a user name to be entered.
            /// </summary>
            RequireSmartCard = 0x100,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            PasswordOnlyOK = 0x200,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            ValidateUserName = 0x400,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            CompleteUserName = 0x800,

            /// <summary>
            /// Do not show the Save check box, but the credential is saved as though the box were shown and selected.
            /// </summary>
            Persist = 0x1000,

            /// <summary>
            /// When this flag is specified, wildcard credentials will not be matched.
            /// </summary>
            ServerCredential = 0x4000,

            /// <summary>
            /// Do not persist unless caller later confirms credential via the CredUIConfirmCredential API.
            /// </summary>
            ExpectConfirmation = 0x20000,

            /// <summary>
            /// Consider the credentials entered by the user to be generic credentials.
            /// </summary>
            GenericCredentials = 0x40000,

            /// <summary>
            /// The credential is a "run-as" credential. The TargetName parameter specifies the name of the command or program being run. It is used for prompting purposes only.
            /// </summary>
            UserNameTargetCredentials = 0x80000,

            /// <summary>
            /// Do not allow the user to change the supplied username.
            /// </summary>
            KeepUserName = 0x100000
        }

        #endregion

        #region RASADP

        /// <summary>
        /// Defines the remote access service (RAS) AutoDial parameters.
        /// </summary>
        public enum RASADP
        {
            /// <summary>
            /// Causes AutoDial to disable a dialog box displayed to the user before dialing a connection.
            /// </summary>
            DisableConnectionQuery = 0,

            /// <summary>
            /// Causes the system to disable all AutoDial connections for the current logon session.
            /// </summary>
            LogOnSessionDisable,

            /// <summary>
            /// Indicates the maximum number of addresses that AutoDial stores in the registry.
            /// </summary>
            SavedAddressesLimit,

            /// <summary>
            /// Indicates a timeout value (in seconds) when an AutoDial connection attempt fails before another connection attempt begins.
            /// </summary>
            FailedConnectionTimeout,

            /// <summary>
            /// Indicates a timeout value (in seconds) when the system displays a dialog box asking the user to confirm that the system should dial.
            /// </summary>
            ConnectionQueryTimeout
        }

        #endregion

        #region RASAPIVERSION

        /// <summary>
        /// Defines the version of a data structure.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum RASAPIVERSION
        {
            /// <summary>
            /// The Windows 2000 version.
            /// </summary>
            Win2K = 1,

            /// <summary>
            /// The Windows XP version.
            /// </summary>
            WinXP = 2,

            /// <summary>
            /// The Windows Vista version (includes Vista SP1). 
            /// </summary>
            WinVista = 3,

            /// <summary>
            /// The Windows 7 version.
            /// </summary>
            Win7 = 4
        }

        #endregion

        #region RASCCPO

        /// <summary>
        /// Defines the remote access service compression control protocol options.
        /// </summary>
        public enum RASCCPO
        {
            /// <summary>
            /// No compression options in use.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Compression without encryption.
            /// </summary>
            CompressionOnly = 0x1,

            /// <summary>
            /// Microsoft Point-to-Point Encryption (MPPE) in stateless mode.
            /// </summary>
            /// <remarks>The session key is changed after every packet. This mode improves performance on high latency networks, or networks that experience significant packet loss.</remarks>
            HistoryLess = 0x2,

            /// <summary>
            /// Microsoft Point-to-Point Encryption (MPPE) using 56 bit keys.
            /// </summary>
            Encryption56Bit = 0x10,

            /// <summary>
            /// Microsoft Point-to-Point Encryption (MPPE) using 40 bit keys.
            /// </summary>
            Encryption40Bit = 0x20,

            /// <summary>
            /// Microsoft Point-to-Point Encryption (MPPE) using 128 bit keys.
            /// </summary>
            Encryption128Bit = 0x40
        }

        #endregion

        #region RASCF

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Defines the remote access connection options.
        /// </summary>
        [Flags]
        public enum RASCF
        {
            /// <summary>
            /// No connection options specified.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Specifies the connection is available to all users.
            /// </summary>
            AllUsers = 0x1,

            /// <summary>
            /// Specifies the credentials used for the connection are the default credentials.
            /// </summary>
            GlobalCredentials = 0x2,

            /// <summary>
            /// Specifies the owner of the connection is known.
            /// </summary>
            OwnerKnown = 0x4,

            /// <summary>
            /// Specifies the owner of the connection matches the current user.
            /// </summary>
            OwnerMatch = 0x8
        }

#endif

        #endregion

        #region RASCM

        /// <summary>
        /// Defines the flags indicating which members of a <see cref="RASCREDENTIALS"/> instance are valid.
        /// </summary>
        [Flags]
        public enum RASCM
        {
            /// <summary>
            /// No options are valid.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// The user name member is valid.
            /// </summary>
            UserName = 0x1,

            /// <summary>
            /// The password member is valid.
            /// </summary>
            Password = 0x2,

            /// <summary>
            /// The domain name member is valid.
            /// </summary>
            Domain = 0x4,
#if (WINXP || WIN2K8 || WIN7 || WIN8)
            /// <summary>
            /// Indicates the credentials are the default credentials for an all-user connection.
            /// </summary>
            DefaultCredentials = 0x8,

            /// <summary>
            /// Indicates a pre-shared key should be retrieved.
            /// </summary>
            PreSharedKey = 0x10,

            /// <summary>
            /// Used to set the pre-shared key on the remote access server.
            /// </summary>
            ServerPreSharedKey = 0x20,

            /// <summary>
            /// Used to set the pre-shared key for a demand dial interface.
            /// </summary>
            DdmPreSharedKey = 0x40
#endif
        }

        #endregion

        #region RASCN

        /// <summary>
        /// Defines the RAS connection event types.
        /// </summary>
        [Flags]
        public enum RASCN
        {
            /// <summary>
            /// Signal when a connection is created.
            /// </summary>
            Connection = 0x1,

            /// <summary>
            /// Signal when a connection has disconnected.
            /// </summary>
            Disconnection = 0x2,

            /// <summary>
            /// Signal when a connection has bandwidth added.
            /// </summary>
            BandwidthAdded = 0x4,

            /// <summary>
            /// Signal when a connection has bandwidth removed. 
            /// </summary>
            BandwidthRemoved = 0x8,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            Dormant = 0x10,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            Reconnection = 0x20
        }

        #endregion

        #region RASEAPF

        /// <summary>
        /// Defines flags that are used to qualify the authentication process.
        /// </summary>
        [Flags]
        public enum RASEAPF
        {
            /// <summary>
            /// No flags are used.
            /// </summary>
            None = 0,

            /// <summary>
            /// Specifies that the authentication protocol should not bring up a graphical user interface. If this flag is not present,
            /// it is okay for the protocol to display a user interface.
            /// </summary>
            NonInteractive = 0x2,

            /// <summary>
            /// Specifies that the user data is obtained from WinLogon.
            /// </summary>
            LogOn = 0x4,

            /// <summary>
            /// Specifies that the user should be prompted for identity information before dialing.
            /// </summary>
            Preview = 0x8
        }

        #endregion

        #region RASEO

        /// <summary>
        /// Defines the connection options for entries.
        /// </summary>
        [Flags]
        public enum RASEO : uint
        {
            /// <summary>
            /// No entry options specified.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// The country id, country code, and area code members are used to construct the phone number.
            /// </summary>
            UseCountryAndAreaCodes = 0x1,

            /// <summary>
            /// The IP address specified by the entry will be used for the connection.
            /// </summary>
            SpecificIPAddress = 0x2,

            /// <summary>
            /// The DNS addresses and WINS addresses specified by the entry will be used for the connection.
            /// </summary>
            SpecificNameServers = 0x4,

            /// <summary>
            /// IP header compression will be used on PPP (Point-to-point) connections.
            /// </summary>
            IPHeaderCompression = 0x8,

            /// <summary>
            /// The default route for IP packets is through the dial-up adapter while the connection is active.
            /// </summary>
            RemoteDefaultGateway = 0x10,

            /// <summary>
            /// The remote access service (RAS) will disable the PPP LCP extensions.
            /// </summary>
            DisableLcpExtensions = 0x20,

            /// <summary>
            /// The remote access service (RAS) displays a terminal window for user input before dialing the connection.
            /// </summary>
            /// <remarks>This member is only used when the entry is dialed by the component.</remarks>
            TerminalBeforeDial = 0x40,

            /// <summary>
            /// The remote access service displays a terminal window for user input after dialing the connection.
            /// </summary>
            /// <remarks>This member is only used when the entry is dialed by the component.</remarks>
            TerminalAfterDial = 0x80,

            /// <summary>
            /// The remote access service (RAS) will display a status monitor in the taskbar.
            /// </summary>
            ModemLights = 0x100,

            /// <summary>
            /// The software compression will be negotiated by the link.
            /// </summary>
            SoftwareCompression = 0x200,

            /// <summary>
            /// Only secure password schemes can be used to authenticate the client with the server.
            /// </summary>
            RequireEncryptedPassword = 0x400,

            /// <summary>
            /// Only the Microsoft secure password scheme (MSCHAP) can be used to authenticate the client with the server.
            /// </summary>
            RequireMSEncryptedPassword = 0x800,

            /// <summary>
            /// Data encryption must be negotiated successfully or the connection should be dropped.
            /// </summary>
            /// <remarks>This flag is ignored unless <see cref="RASEO.RequireMSEncryptedPassword"/> is also set.</remarks>
            RequireDataEncryption = 0x1000,

            /// <summary>
            /// The remote access service (RAS) logs on to the network after the point-to-point connection is established.
            /// </summary>
            NetworkLogOn = 0x2000,

            /// <summary>
            /// The remote access service (RAS) uses the username, password, and domain of the currently logged on user when dialing this entry.
            /// </summary>
            /// <remarks>This flag is ignored unless the <see cref="RASEO.RequireMSEncryptedPassword"/> is also set.</remarks>
            UseLogOnCredentials = 0x4000,

            /// <summary>
            /// Indicates when an alternate phone number connects successfully, that number will become the primary phone number. 
            /// </summary>
            PromoteAlternates = 0x8000,

            /// <summary>
            /// Check for an existing remote file system and remote printer bindings before making a connection to this phone book entry.
            /// </summary>
            SecureLocalFiles = 0x10000,

            /// <summary>
            /// Indicates the Extensible Authentication Protocol (EAP) must be supported for authentication.
            /// </summary>
            RequireEap = 0x20000,

            /// <summary>
            /// Indicates the Password Authentication Protocol (PAP) must be supported for authentication.
            /// </summary>
            RequirePap = 0x40000,

            /// <summary>
            /// Indicates Shiva's Password Authentication Protocol (SPAP) must be supported for authentication.
            /// </summary>
            RequireSpap = 0x80000,

            /// <summary>
            /// The connection will use custom encryption.
            /// </summary>
            Custom = 0x100000,

            /// <summary>
            /// The remote access dialer should display the phone number being dialed.
            /// </summary>
            PreviewPhoneNumber = 0x200000,

            /// <summary>
            /// Indicates all modems on the computer will share the same phone number.
            /// </summary>
            SharedPhoneNumbers = 0x800000,

            /// <summary>
            /// The remote access dialer should display the username and password prior to dialing.
            /// </summary>
            PreviewUserPassword = 0x1000000,

            /// <summary>
            /// The remote access dialer should display the domain name prior to dialing.
            /// </summary>
            PreviewDomain = 0x2000000,

            /// <summary>
            /// The remote access dialer will display its progress while establishing the connection.
            /// </summary>
            ShowDialingProgress = 0x4000000,

            /// <summary>
            /// Indicates the Challenge Handshake Authentication Protocol (CHAP) must be supported for authentication.
            /// </summary>
            RequireChap = 0x8000000,

            /// <summary>
            /// Indicates the Challenge Handshake Authentication Protocol (CHAP) must be supported for authentication.
            /// </summary>
            RequireMSChap = 0x10000000,

            /// <summary>
            /// Indicates the Challenge Handshake Authentication Protocol (CHAP) version 2 must be supported for authentication.
            /// </summary>
            RequireMSChap2 = 0x20000000,

            /// <summary>
            /// Indicates MSCHAP must also send the LanManager hashed password.
            /// </summary>
            /// <remarks>This flag requires that <see cref="RASEO.RequireMSChap"/> must also be set.</remarks>
            RequireWin95MSChap = 0x40000000,

            /// <summary>
            /// The remote access service (RAS) must invoke a custom scripting assembly after establishing a connection to the server.
            /// </summary>
            CustomScript = 0x80000000
        }

        #endregion

        #region RASEO2

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Defines the additional connection options for entries.
        /// </summary>
        [Flags]
        public enum RASEO2 : uint
        {
            /// <summary>
            /// No additional entry options specified.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Prevents remote users from using file and print servers over the connection.
            /// </summary>
            SecureFileAndPrint = 0x1,

            /// <summary>
            /// Equivalent of clearing the Client for Microsoft Networks checkbox in the connection properties 
            /// dialog box on the networking tab.
            /// </summary>
            SecureClientForMSNet = 0x2,

            /// <summary>
            /// Changes the default behavior to not negotiate multilink.
            /// </summary>
            DoNotNegotiateMultilink = 0x4,

            /// <summary>
            /// Use the default credentials to access network resources.
            /// </summary>
            DoNotUseRasCredentials = 0x8,

            /// <summary>
            /// Use a pre-shared key for IPSec authentication.
            /// </summary>
            /// <remarks>This member is only used by L2TP/IPSec VPN connections.</remarks>
            UsePreSharedKey = 0x10,

            /// <summary>
            /// Indicates the connection is to the Internet.
            /// </summary>
            Internet = 0x20,

            /// <summary>
            /// Disables NBT probing for this connection.
            /// </summary>
            DisableNbtOverIP = 0x40,

            /// <summary>
            /// Ignore the device settings specified by the phone book entry.
            /// </summary>
            UseGlobalDeviceSettings = 0x80,

            /// <summary>
            /// Automatically attempts to re-establish the connection if the connection is lost.
            /// </summary>
            ReconnectIfDropped = 0x100,

            /// <summary>
            /// Use the same set of phone numbers for all subentries in a multilink connection.
            /// </summary>
            SharePhoneNumbers = 0x200,
#if (WIN2K8 || WIN7 || WIN8)
            /// <summary>
            /// Indicates the routing compartments feature is enabled.
            /// </summary>
            SecureRoutingCompartment = 0x400,

            /// <summary>
            /// Configures a VPN connection to use the typical settings for authentication and encryption for the RAS connection.
            /// </summary>
            UseTypicalSettings = 0x800,

            /// <summary>
            /// Uses the IPv6 DNS address and alternate DNS address for the connection.
            /// </summary>
            IPv6SpecificNameServer = 0x1000,

            /// <summary>
            /// Indicates the default route for IPv6 packets is through the PPP connection when the connection is active.
            /// </summary>
            IPv6RemoteDefaultGateway = 0x2000,

            /// <summary>
            /// Registers the IP address with the DNS server when connected.
            /// </summary>
            RegisterIPWithDns = 0x4000,

            /// <summary>
            /// The DNS suffix for this connection should be used for DNS registration.
            /// </summary>
            UseDnsSuffixForRegistration = 0x8000,

            /// <summary>
            /// Indicates the administrator is allowed to statically set the interface metric of the IPv4 stack for this interface.
            /// </summary>
            IPv4ExplicitMetric = 0x10000,

            /// <summary>
            /// Indicates the administrator is allowed to statically set the interface metric of the IPv6 stack for this interface.
            /// </summary>
            IPv6ExplicitMetric = 0x20000,

            /// <summary>
            /// The IKE validation check will not be performed.
            /// </summary>
            DisableIkeNameEkuCheck = 0x40000,
#endif
#if (WIN7 || WIN8)
            /// <summary>
            /// Indicates whether a class based route based on the VPN interface IP address will not be added.
            /// </summary>
            DisableClassBasedStaticRoute = 0x80000,

            /// <summary>
            /// Indicates whether the specific IPv6 address will be used for the connection.
            /// </summary>
            /// <remarks>If set, RAS attempts to use the IP address specified by <see cref="RasEntry.IPv6Address"/> as the IPv6 address for the connection.</remarks>
            IPv6SpecificAddress = 0x100000,

            /// <summary>
            /// Indicates whether the client will not be able to change the external IP address of the IKEv2 VPN connection.
            /// </summary>
            DisableMobility = 0x200000,

            /// <summary>
            /// Indicates whether machine certificates are used for IKEv2 authentication.
            /// </summary>
            RequireMachineCertificates = 0x400000,
#endif
#if (WIN8)
            /// <summary>
            /// Use the pre-shared key for the IKEv2 initiator.
            /// </summary>
            UsePreSharedKeyForIkev2Initiator = 0x800000,

            /// <summary>
            /// Use the pre-shared key for the IKEv2 responder.
            /// </summary>
            UsePreSharedKeyForIkev2Responder = 0x1000000,

            /// <summary>
            /// Indicates the credentials should be cached.
            /// </summary>
            CacheCredentials = 0x2000000
#endif
        }

#endif

        #endregion

        #region RASIPO

        /// <summary>
        /// Defines the remote access service (RAS) IPCP options.
        /// </summary>
        [Flags]
        public enum RASIPO
        {
            /// <summary>
            /// No options in use.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Van Jacobson compression.
            /// </summary>
            VJ = 0x1
        }

        #endregion

        #region RASLCPO

        /// <summary>
        /// Defines the Link Control Protocol (LCP) options.
        /// </summary>
        [Flags]
        public enum RASLCPO
        {
            /// <summary>
            /// No LCP options used.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Protocol Field Compression.
            /// </summary>
            Pfc = 0x1,

            /// <summary>
            /// Address and Control Field Compression.
            /// </summary>
            Acfc = 0x2,

            /// <summary>
            /// Short Sequence Number Header Format.
            /// </summary>
            Sshf = 0x4,

            /// <summary>
            /// DES 56-bit encryption.
            /// </summary>
            Des56 = 0x8,

            /// <summary>
            /// Triple DES encryption.
            /// </summary>
            TripleDes = 0x10,

#if (WIN2K8 || WIN7 || WIN8)

            /// <summary>
            /// AES 128-bit encryption.
            /// </summary>
            Aes128 = 0x20,

            /// <summary>
            /// AES 256-bit encryption.
            /// </summary>
            Aes256 = 0x40

#endif
        }

        #endregion

        #region RASNP

        /// <summary>
        /// Defines the network protocols used for negotiation.
        /// </summary>
        [Flags]
        public enum RASNP
        {
            /// <summary>
            /// No network protocol specified.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Negotiate the NetBEUI protocol.
            /// </summary>
            NetBeui = 0x1,

            /// <summary>
            /// Negotiate the IPX protocol.
            /// </summary>
            Ipx = 0x2,

            /// <summary>
            /// Negotiate the IPv4 protocol.
            /// </summary>
            IP = 0x4,

#if (WIN2K8 || WIN7 || WIN8)

            /// <summary>
            /// Negotiate the IPv6 protocol.
            /// </summary>
            IPv6 = 0x8

#endif
        }

        #endregion

        #region RASPBDEVENT

        /// <summary>
        /// Defines the events that occur for RasPhoneBookDialog boxes.
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum RASPBDEVENT
        {
            /// <summary>
            /// Received when the user creates a new phone book entry or copies an existing phone book entry.
            /// </summary>
            AddEntry = 1,

            /// <summary>
            /// Received when the user changes an existing phone book entry.
            /// </summary>
            EditEntry = 2,

            /// <summary>
            /// Received when the user deletes a phone book entry.
            /// </summary>
            RemoveEntry = 3,

            /// <summary>
            /// Received when the user successfully dials an entry.
            /// </summary>
            DialEntry = 4,

            /// <summary>
            /// Received when the user makes changes in the <b>User Preferences</b> property sheet.
            /// </summary>
            EditGlobals = 5,

            /// <summary>
            /// Received during the dialog box initialization when the NoUser flag has been set.
            /// </summary>
            NoUser = 6,

            /// <summary>
            /// Received when the NoUser flag has been set and the user changes the credentials that are supplied during the NoUser event.
            /// </summary>
            NoUserEdit = 7
        }

        #endregion

        #region RASPROJECTION

        /// <summary>
        /// Defines the projection types.
        /// </summary>
        public enum RASPROJECTION
        {
            /// <summary>
            /// Authentication Message Block (AMB) protocol.
            /// </summary>
            Amb = 0x10000,

            /// <summary>
            /// NetBEUI Framer (NBF) protocol.
            /// </summary>
            Nbf = 0x803F,

            /// <summary>
            /// Internetwork Packet Exchange (IPX) control protocol.
            /// </summary>
            Ipx = 0x802B,

            /// <summary>
            /// Internet Protocol (IP) control protocol.
            /// </summary>
            IP = 0x8021,

            /// <summary>
            /// Compression Control Protocol (CCP).
            /// </summary>
            Ccp = 0x80FD,

            /// <summary>
            /// Link Control Protocol (LCP).
            /// </summary>
            Lcp = 0xC021,

            /// <summary>
            /// Internet Protocol Version 6 (IPv6) control protocol.
            /// </summary>
            IPv6 = 0x8057,

            /// <summary>
            /// Serial Line Internet Protocol (SLIP).
            /// </summary>
            Slip = 0x20000
        }

        #endregion

        #region RASPROJECTION_INFO_TYPE

#if (WIN7 || WIN8)
        /// <summary>
        /// Defines the structure type available for projection operations.
        /// </summary>
        public enum RASPROJECTION_INFO_TYPE
        {
            /// <summary>
            /// The projection contains a <see cref="NativeMethods.RASPPP_PROJECTION_INFO"/> structure.
            /// </summary>
            Ppp = 1,

            /// <summary>
            /// The project contains a <see cref="NativeMethods.RASIKEV2_PROJECTION_INFO"/> structure.
            /// </summary>
            IkeV2 = 2
        }
#endif

        #endregion

        #region RASDDFLAG

        /// <summary>
        /// Defines the options available for dial dialog boxes.
        /// </summary>
        [Flags]
        public enum RASDDFLAG : uint
        {
            /// <summary>
            /// Use the values specified by the top and left members to position the dialog box.
            /// </summary>
            PositionDlg = 0x1,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            NoPrompt = 0x2,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            AoacRedial = 0x4,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            LinkFailure = 0x80000000
        }

        #endregion

        #region RASEDFLAG

        /// <summary>
        /// Defines the options available for entry dialog boxes.
        /// </summary>
        [Flags]
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public enum RASEDFLAG
        {
            /// <summary>
            /// Use the values specified by the top and left members to position the dialog box.
            /// </summary>
            PositionDlg = 0x1,

            /// <summary>
            /// Display a wizard for creating a new phone book entry.
            /// </summary>
            NewEntry = 0x2,
#if (!WIN2K8 && !WIN7 && !WIN8)
            /// <summary>
            /// Create a new entry by copying the properties of an existing entry.
            /// </summary>
            CloneEntry = 0x4,
#endif
            /// <summary>
            /// Displays a property sheet for editing the properties of a phone book entry without the ability to rename the entry.
            /// </summary>
            NoRename = 0x8,

            /// <summary>
            /// Reserved for system use.
            /// </summary>
            ShellOwned = 0x40000000,

            /// <summary>
            /// Causes the wizard to go directly to the page for a phone line entry.
            /// </summary>
            NewPhoneEntry = 0x10,

            /// <summary>
            /// Causes the wizard to go directly to the page for a new tunnel entry.
            /// </summary>
            NewTunnelEntry = 0x20,
#if (!WIN2K8 && !WIN7 && !WIN8)
            /// <summary>
            /// Causes the wizard to go directly to the page for a direct-connection entry.
            /// </summary>
            NewDirectEntry = 0x40,
#endif
            /// <summary>
            /// Causes the wizard that creates a new broadband phone book entry.
            /// </summary>
            NewBroadbandEntry = 0x80,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            InternetEntry = 0x100,

            /// <summary>
            /// This member is undocumented.
            /// </summary>
            NAT = 0x200,
#if (WIN2K8 || WIN7 || WIN8)
            /// <summary>
            /// Displays a wizard that allows a user to enable and configure incoming connections.
            /// </summary>
            IncomingConnection = 0x400
#endif
        }

        #endregion

        #region RASIKEV2

#if (WIN7 || WIN8)

        /// <summary>
        /// Defines the Internet Key Exchange (IKEv2) options.
        /// </summary>
        [Flags]
        public enum RASIKEV2
        {
            /// <summary>
            /// No options specified.
            /// </summary>
            None = 0,

            /// <summary>
            /// The client supports the IKEv2 Mobility and Multi-homing (MOBIKE) protocol.
            /// </summary>
            MobileIke = 0x1,

            /// <summary>
            /// The client is behind Network Address Translation (NAT).
            /// </summary>
            ClientBehindNat = 0x2,

            /// <summary>
            /// The server is behind Network Address Translation (NAT).
            /// </summary>
            ServerBehindNat = 0x4
        }

#endif

        #endregion

        #region RASPBDLGFLAG

        /// <summary>
        /// Defines the options available for the phone book dialog box.
        /// </summary>
        [Flags]
        public enum RASPBDFLAG : uint
        {
            /// <summary>
            /// Use the values specified by the top and left members to position the dialog box.
            /// </summary>
            PositionDlg = 0x1,

            /// <summary>
            /// Turns on the close-on-dial option, overriding the user's preference.
            /// </summary>
            ForceCloseOnDial = 0x2,

            /// <summary>
            /// Causes the dialog callback function to receive a NoUser notification when the dialog box is starting up.
            /// </summary>
            NoUser = 0x10,

            /// <summary>
            /// Causes the default window position to be saved on exit.
            /// </summary>
            UpdateDefaults = 0x80000000
        }

        #endregion

        #region RASTUNNELENDPOINTTYPE

#if (WIN7 || WIN8)

        /// <summary>
        /// Defines the endpoint types.
        /// </summary>
        public enum RASTUNNELENDPOINTTYPE
        {
            /// <summary>
            /// The endpoint type is unknown.
            /// </summary>
            Unknown,

            /// <summary>
            /// The endpoint type is IPv4.
            /// </summary>
            IPv4,

            /// <summary>
            /// The endpoint type is IPv6.
            /// </summary>
            IPv6
        }

#endif

        #endregion

        #region RasNotifierType

        /// <summary>
        /// Defines the callback signatures available for the dialing process.
        /// </summary>
        public enum RasNotifierType
        {
            /// <summary>
            /// This member is not supported.
            /// </summary>
            [Obsolete("Use the RasDialFunc2 value.", true)]
            RasDialFunc = 0,

            /// <summary>
            /// This member is not supported.
            /// </summary>
            [Obsolete("Use the RasDialFunc2 value.", true)]
            RasDialFunc1 = 1,

            /// <summary>
            /// The callback function is a <see cref="NativeMethods.RasDialFunc2"/> delegate.
            /// </summary>
            RasDialFunc2 = 2
        }

        #endregion

        #region RDEOPT

        /// <summary>
        /// Defines the remote access dial extensions options.
        /// </summary>
        [Flags]
        public enum RDEOPT
        {
            /// <summary>
            /// No dial extension options have been specified.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Use the prefix and suffix that is in the RAS phone book.
            /// </summary>
            /// <remarks>If no entry name was specified during dialing, this member is ignored.</remarks>
            UsePrefixSuffix = 0x1,

            /// <summary>
            /// Accept paused states.
            /// </summary>
            /// <remarks>If this flag has not been set, the dialer reports a fatal error if it enters a paused state.</remarks>
            PausedStates = 0x2,

            /// <summary>
            /// Ignore the modem speaker setting that is in the RAS phone book, and use the setting specified by the <see cref="SetModemSpeaker"/> flag.
            /// </summary>
            IgnoreModemSpeaker = 0x4,

            /// <summary>
            /// Sets the modem speaker on.
            /// </summary>
            SetModemSpeaker = 0x8,

            /// <summary>
            /// Ignore the software compression setting that is in the RAS phone book, and uses the setting specified by the <see cref="SetSoftwareCompression"/> flag.
            /// </summary>
            IgnoreSoftwareCompression = 0x10,

            /// <summary>
            /// Use software compression.
            /// </summary>
            SetSoftwareCompression = 0x20,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            DisableConnectedUI = 0x40,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            DisableReconnectUI = 0x80,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            DisableReconnect = 0x100,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            NoUser = 0x200,

            /// <summary>
            /// Used internally by the <see cref="RasDialDialog"/> class so that a Windows-95-style logon script is executed in a terminal window visible to the user.
            /// </summary>
            /// <remarks>Applications should not set this flag.</remarks>
            PauseOnScript = 0x400,

            /// <summary>
            /// Undocumented flag.
            /// </summary>
            Router = 0x800,

            /// <summary>
            /// Dial normally instead of calling the RasCustomDial entry point of the custom dialer.
            /// </summary>
            CustomDial = 0x1000,

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            /// <summary>
            /// Specifies that RasDial should invoke a custom-scripting DLL after establishing the connection to the server.
            /// </summary>
            UseCustomScripting = 0x2000

#endif
        }

        #endregion

        #endregion

        #region Structures

        #region CREDUI_INFO

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Describes a dialog box used to obtain user credentials.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct CREDUI_INFO
        {
            public int size;
            public IntPtr hwndOwner;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string message;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string caption;
            public IntPtr bannerBitmap;
        }

#endif

        #endregion

        #region RAS_STATS

        /// <summary>
        /// Describes statistics for a remote access service (RAS) connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RAS_STATS
        {
            public int size;
            public uint bytesTransmitted;
            public uint bytesReceived;
            public uint framesTransmitted;
            public uint framesReceived;
            public uint crcError;
            public uint timeoutError;
            public uint alignmentError;
            public uint hardwareOverrunError;
            public uint framingError;
            public uint bufferOverrunError;
            public uint compressionRatioIn;
            public uint compressionRatioOut;
            public uint linkSpeed;
            public uint connectionDuration;
        }

        #endregion

        #region RASAMB

        /// <summary>
        /// Describes the result of a remote access server (RAS) Authentication Message Block (AMB) projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASAMB
        {
            public int size;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NETBIOS_NAME_LEN + 1)]
            public string netBiosError;
            public byte lana;
        }

        #endregion

        #region RASAUTODIALENTRY

        /// <summary>
        /// Describes an AutoDial entry associated with a network address in the AutoDial mapping database.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASAUTODIALENTRY
        {
            public int size;
            public int options;
            public int dialingLocation;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string entryName;
        }

        #endregion

        #region RASCTRYINFO

        /// <summary>
        /// Describes country/region specific dialing information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RASCTRYINFO
        {
            public int size;
            public int countryId;
            public int nextCountryId;
            public int countryCode;
            public int countryNameOffset;
        }

        #endregion

        #region RASEAPUSERIDENTITY

        /// <summary>
        /// Describes extensible authentication protocol (EAP) user identity information for a particular user.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASEAPUSERIDENTITY
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = UNLEN + 1)]
            public string userName;
            public int sizeOfEapData;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] eapData;
        }

        #endregion

        #region RASEAPINFO

        /// <summary>
        /// Describes user-specific Extensible Authentication Protocol (EAP) information.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASEAPINFO
        {
            public int sizeOfEapData;
            public IntPtr eapData;
        }

        #endregion

        #region RASNAPSTATE

#if (WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Describes the network access protection (NAP) state of a connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RASNAPSTATE
        {
            public int size;
            public int flags;
            public RasIsolationState isolationState;
            public FILETIME probationTime;
        }
#endif

        #endregion

        #region RASPPPCCP

        /// <summary>
        /// Describes the results of a Compression Control Protocol (CCP) projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RASPPPCCP
        {
            public int size;
            public int errorCode;
            public RasCompressionType compressionAlgorithm;
            public RASCCPO options;
            public RasCompressionType serverCompressionAlgorithm;
            public RASCCPO serverOptions;
        }

        #endregion

        #region RASPPPIP

        /// <summary>
        /// Describes the results of a PPP IP projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASPPPIP
        {
            public int size;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxIpAddress + 1)]
            public string ipAddress;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxIpAddress + 1)]
            public string serverIPAddress;
            public RASIPO options;
            public RASIPO serverOptions;
        }

        #endregion

        #region RASPPPIPX

        /// <summary>
        /// Describes the results of a PPP IPX projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASPPPIPX
        {
            public int size;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxIpxAddress + 1)]
            public string ipxAddress;
        }

        #endregion

        #region RASPPPLCP

        /// <summary>
        /// Describes the results of a PPP Link Control Protocol (LCP) multilink projection operation
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RASPPPLCP
        {
            public int size;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bundled;
            public int errorCode;
            public RasLcpAuthenticationType authenticationProtocol;
            public RasLcpAuthenticationDataType authenticationData;
            public int eapTypeId;
            public RasLcpAuthenticationType serverAuthenticationProtocol;
            public RasLcpAuthenticationDataType serverAuthenticationData;
            public int serverEapTypeId;
            [MarshalAs(UnmanagedType.Bool)]
            public bool multilink;
            public int terminateReason;
            public int serverTerminateReason;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxReplyMessage)]
            public string replyMessage;
            public RASLCPO options;
            public RASLCPO serverOptions;
        }

        #endregion

        #region RASPPPNBF

        /// <summary>
        /// Describes the result of a PPP NetBEUI Framer (NBF) projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASPPPNBF
        {
            public int size;
            public int errorCode;
            public int netBiosErrorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NETBIOS_NAME_LEN + 1)]
            public string netBiosErrorMessage;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NETBIOS_NAME_LEN + 1)]
            public string workstationName;
            public byte lana;
        }

        #endregion

        #region RASSLIP

        /// <summary>
        /// Describes the result of a Serial Line Internet Protocol (SLIP) projection operation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RASSLIP
        {
            public int size;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxIpAddress + 1)]
            public string ipAddress;
        }

        #endregion

        #region RASPPPIPV6

#if (WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Describes the results of a Point-to-Point (PPP) IPv6 projection operation..
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASPPPIPV6
        {
            public int size;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] localInterfaceIdentifier;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] peerInterfaceIdentifier;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] localCompressionProtocol;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] peerCompressionProtocol;
        }
#endif

        #endregion

        #region RASCONN

        /// <summary>
        /// Describes a remote access connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASCONN
        {
            public int size;
            public IntPtr handle;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string entryName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string deviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string deviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string phoneBook;
            public int subEntryId;
            public Guid entryId;
#if (WINXP || WIN2K8 || WIN7 || WIN8)
            public RASCF connectionOptions;
            public Luid sessionId;
#endif
#if (WIN2K8 || WIN7 || WIN8)
            public Guid correlationId;
#endif
        }

        #endregion

        #region RASCONNSTATUS

        /// <summary>
        /// Describes the current status of a remote access connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASCONNSTATUS
        {
            public int size;
            public RasConnectionState connectionState;
            public int errorCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string deviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string deviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPhoneNumber + 1)]
            public string phoneNumber;

#if (WIN7 || WIN8)
            public RASTUNNELENDPOINT localEndPoint;
            public RASTUNNELENDPOINT remoteEndPoint;
            public RasConnectionSubState connectionSubState;
#endif
        }

        #endregion

        #region RASCREDENTIALS

        /// <summary>
        /// Describes user credentials associated with a phone book entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASCREDENTIALS
        {
            public int size;
            public RASCM options;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = UNLEN + 1)]
            public string userName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PWLEN + 1)]
            public string password;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DNLEN + 1)]
            public string domain;
        }

        #endregion

        #region RASDEVINFO

        /// <summary>
        /// Describes a TAPI device capable of establishing a remote access service connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct RASDEVINFO
        {
            public int size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string name;
        }

        #endregion

        #region RASDEVSPECIFICINFO

        /// <summary>
        /// Describes a cookie for server validation and bypass point-to-point (PPP) authentication.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASDEVSPECIFICINFO
        {
            public int size;
            public IntPtr cookie;
        }

        #endregion

        #region RASDIALDLG

        /// <summary>
        /// Specifies additional input and output parameters for the RasDialDlg function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RASDIALDLG
        {
            public int size;
            public IntPtr hwndOwner;
            public RASDDFLAG flags;
            public int left;
            public int top;
            public int subEntryId;
            public int error;
            public IntPtr reserved;
            public IntPtr reserved2;
        }

        #endregion

        #region RASDIALEXTENSIONS

        /// <summary>
        /// Contains information about extended features of the RasDial function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASDIALEXTENSIONS
        {
            public int size;
            public RDEOPT options;
            public IntPtr handle;
            public IntPtr reserved;
            public IntPtr reserved1;
            public RASEAPINFO eapInfo;
#if (WIN7 || WIN8)
            public bool skipPppAuth;
            public RASDEVSPECIFICINFO devSpecificInfo;
#endif
        }

        #endregion

        #region RASDIALPARAMS

        /// <summary>
        /// Contains information used by RasDial to establish a remote access connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASDIALPARAMS
        {
            public int size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string entryName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPhoneNumber + 1)]
            public string phoneNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxCallbackNumber + 1)]
            public string callbackNumber;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = UNLEN + 1)]
            public string userName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PWLEN + 1)]
            public string password;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DNLEN + 1)]
            public string domain;
            public int subEntryId;
            public IntPtr callbackId;
#if (WIN7 || WIN8)
            public int interfaceIndex;
#endif
////#if (WIN8)
////            public IntPtr szEncPassword;
////#endif
        }

        #endregion

        #region RASENTRY

        /// <summary>
        /// Describes a phone book entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASENTRY
        {
            public int size;
            public RASEO options;

            // Location and phone number
            public int countryId;
            public int countryCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxAreaCode + 1)]
            public string areaCode;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPhoneNumber + 1)]
            public string phoneNumber;
            public int alternateOffset;

            // PPP/IP
            public RASIPADDR ipAddress;
            public RASIPADDR dnsAddress;
            public RASIPADDR dnsAddressAlt;
            public RASIPADDR winsAddress;
            public RASIPADDR winsAddressAlt;

            // Framing
            public int frameSize;
            public RASNP networkProtocols;
            public RasFramingProtocol framingProtocol;

            // Scripting 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string script;

            // AutoDial
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string autoDialDll;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string autoDialFunc;

            // Device
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string deviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string deviceName;

            // X.25
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPadType + 1)]
            public string x25PadType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxX25Address + 1)]
            public string x25Address;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxFacilities + 1)]
            public string x25Facilities;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxUserData + 1)]
            public string x25UserData;
            public int channels;

            // Reserved
            public int reserved1;
            public int reserved2;

            // Multilink and BAP
            public int subentries;
            public RasDialMode dialMode;
            public int dialExtraPercent;
            public int dialExtraSampleSeconds;
            public int hangUpExtraPercent;
            public int hangUpExtraSampleSeconds;

            // Idle timeout
            public int idleDisconnectSeconds;

            public RasEntryType entryType;
            public RasEncryptionType encryptionType;
            public int customAuthKey;
            public Guid id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string customDialDll;
            public RasVpnStrategy vpnStrategy;

#if (WINXP || WIN2K8 || WIN7 || WIN8)
            public RASEO2 options2;
            public int options3;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDnsSuffix)]
            public string dnsSuffix;
            public int tcpWindowSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string prerequisitePhoneBook;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string prerequisiteEntryName;
            public int redialCount;
            public int redialPause;
#endif
#if (WIN2K8 || WIN7 || WIN8)
            public RASIPV6ADDR ipv6DnsAddress;
            public RASIPV6ADDR ipv6DnsAddressAlt;
            public int ipv4InterfaceMetric;
            public int ipv6InterfaceMetric;
#endif
#if (WIN7 || WIN8)
            public RASIPV6ADDR ipv6Address;
            public int ipv6PrefixLength;
            public int networkOutageTime;
#endif
        }

        #endregion

        #region RASENTRYDLG

        /// <summary>
        /// Specifies additional input and output parameters for the RasEntryDlg function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASENTRYDLG
        {
            public int size;
            public IntPtr hwndOwner;
            public RASEDFLAG flags;
            public int left;
            public int top;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string entryName;
            public int error;
            public IntPtr reserved;
            public IntPtr reserved2;
        }

        #endregion

        #region RASENTRYNAME

        /// <summary>
        /// Describes an entry name from a remote access phone book.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASENTRYNAME
        {
            public int size;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxEntryName + 1)]
            public string name;
            public RasPhoneBookType phoneBookType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH + 1)]
            public string phoneBookPath;
        }

        #endregion

        #region RASIKEV2_PROJECTION_INFO

#if (WIN7 || WIN8)

        /// <summary>
        /// Contains projection information obtained during Internet Key Exchange (IKE) negotiation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASIKEV2_PROJECTION_INFO
        {
            public int ipv4NegotiationError;
            public RASIPADDR ipv4Address;
            public RASIPADDR ipv4ServerAddress;
            public int ipv6NegotiationError;
            public RASIPV6ADDR ipv6Address;
            public RASIPV6ADDR ipv6ServerAddress;
            public int prefixLength;
            public RasIkeV2AuthenticationType authenticationProtocol;
            public int eapTypeId;
            public RASIKEV2 options;
            public RasIPSecEncryptionType encryptionMethod;
            public int numIPv4ServerAddresses;
            public IntPtr ipv4ServerAddresses;
            public int numIPv6ServerAddresses;
            public IntPtr ipv6ServerAddresses;
        }

#endif

        #endregion

        #region RASIPADDR

        /// <summary>
        /// Describes an IPv4 address.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [TypeConverter(typeof(IPAddressConverter))]
        public struct RASIPADDR
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] addr;
        }

        #endregion

        #region RASIPV6ADDR

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Describes an IPv6 address.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        [TypeConverter(typeof(IPAddressConverter))]
        public struct RASIPV6ADDR
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] addr;
        }

#endif

        #endregion

        #region RASPBDLG

        /// <summary>
        /// Specifies additional input and output parameters for the RasPhonebookDlg function.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RASPBDLG
        {
            public int size;
            public IntPtr hwndOwner;
            public RASPBDFLAG flags;
            public int left;
            public int top;
            public IntPtr callbackId;
            public RasPBDlgFunc callback;
            public int error;
            public IntPtr reserved;
            public IntPtr reserved2;
        }

        #endregion

        #region RAS_PROJECTION_INFO

#if (WIN7 || WIN8)
        /// <summary>
        /// Contains projection information obtained during Point-to-Point (PPP) negotiation.
        /// </summary>
        /// <remarks>This structure does not match the SDK due to a union on the object and the portability problems that it causes on 64-bit platforms. See the implementation for additional details.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        public struct RAS_PROJECTION_INFO
        {
            public RASAPIVERSION version;
            public RASPROJECTION_INFO_TYPE type;
        }
#endif

        #endregion

        #region RASPPP_PROJECTION_INFO

        /// <summary>
        /// Contains information obtained during Point-to-Point (PPP) negotiation.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RASPPP_PROJECTION_INFO
        {
            public int ipv4NegotiationError;
            public RASIPADDR ipv4Address;
            public RASIPADDR ipv4ServerAddress;
            public RasIPOptions ipv4Options;
            public RasIPOptions ipv4ServerOptions;
            public int ipv6NegotiationError;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] interfaceIdentifier;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] serverInterfaceIdentifier;
            [MarshalAs(UnmanagedType.Bool)]
            public bool bundled;
            [MarshalAs(UnmanagedType.Bool)]
            public bool multilink;
            public RasLcpAuthenticationType authenticationProtocol;
            public RasLcpAuthenticationDataType authenticationData;
            public RasLcpAuthenticationType serverAuthenticationProtocol;
            public RasLcpAuthenticationDataType serverAuthenticationData;
            public int eapTypeId;
            public int serverEapTypeId;
            public RASLCPO lcpOptions;
            public RASLCPO serverLcpOptions;
            public RasCompressionType ccpCompressionAlgorithm;
            public RasCompressionType serverCcpCompressionAlgorithm;
            public RASCCPO ccpOptions;
            public RASCCPO serverCcpOptions;
        }

        #endregion

        #region RASSUBENTRY

        /// <summary>
        /// Describes a subentry of a remote access service (RAS) phone book entry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
        public struct RASSUBENTRY
        {
            public int size;
            public int flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceType + 1)]
            public string deviceType;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxDeviceName + 1)]
            public string deviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = RAS_MaxPhoneNumber + 1)]
            public string phoneNumber;
            public int alternateOffset;
        }

        #endregion

        #region RASTUNNELENDPOINT

#if (WIN7 || WIN8)
        /// <summary>
        /// Describes the endpoint of a virtual private network (VPN) tunnel.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RASTUNNELENDPOINT
        {
            public RASTUNNELENDPOINTTYPE type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] addr;
        }
#endif

        #endregion

        #region RASUPDATECONN

#if (WIN7 || WIN8)
        /// <summary>
        /// Contains data used to update an active remote access service (RAS) connection.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct RASUPDATECONN
        {
            public RASAPIVERSION version;
            public int size;
            public int flags;
            public int interfaceIndex;
            public RASTUNNELENDPOINT localEndPoint;
            public RASTUNNELENDPOINT remoteEndPoint;
        }
#endif

        #endregion

        #endregion
    }
}