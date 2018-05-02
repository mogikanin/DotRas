//--------------------------------------------------------------------------
// <copyright file="RasEntry.cs" company="Jeff Winn">
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
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using DotRas.Internal;
    using DotRas.Properties;

    /// <summary>
    /// Represents a remote access service (RAS) entry. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The static methods for creating entries on this object are not required to create an entry, however they do contain default information set by Windows for each platform.
    /// </para>
    /// <para><b>Known Limitations</b>
    /// <list type="bullet">
    /// <item>The methods exposed on this class typically require the entry to belong to a phone book before they can be called.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how to create a VPN entry and add it to a phone book.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasPhoneBook pbk = new RasPhoneBook())
    /// {
    ///     pbk.Open();
    ///     RasEntry entry = RasEntry.CreateVpnEntry("VPN Connection", "127.0.0.1", RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));
    ///     if (entry != null)
    ///     {
    ///         pbk.Entries.Add(entry);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim pbk As New RasPhoneBook
    /// pbk.Open()
    /// Dim entry As RasEntry = RasEntry.CreateVpnEntry("VPN Connection", "127.0.0.1", RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn)
    /// If entry IsNot Nothing Then
    ///     pbk.Entries.Add(entry)
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [DebuggerDisplay("Name = {Name}, PhoneNumber = {PhoneNumber}")]
    public sealed class RasEntry : MarshalByRefObject, ICloneable
    {
        #region Fields

        private RasEntryOptions options;
        private RasNetworkProtocols networkProtocols;
        private Collection<string> alternatePhoneNumbers;
        private RasSubEntryCollection subEntries;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEntry"/> class.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        public RasEntry(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ThrowHelper.ThrowArgumentException("name", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            this.Name = name;            
            this.AreaCode = string.Empty;
#pragma warning disable 0618
            this.AutoDialDll = string.Empty;
            this.AutoDialFunc = string.Empty;
#pragma warning restore 0618
            this.CustomDialDll = string.Empty;
            this.DnsAddress = IPAddress.Any;
            this.DnsAddressAlt = IPAddress.Any;
            this.IPAddress = IPAddress.Any;
            this.Script = string.Empty;            
            this.WinsAddress = IPAddress.Any;
            this.WinsAddressAlt = IPAddress.Any;
            this.X25PadType = string.Empty;
            this.X25Address = string.Empty;
            this.X25Facilities = string.Empty;
            this.X25UserData = string.Empty;

#if (WINXP || WIN2K8 || WIN7 || WIN8)
            this.DnsSuffix = string.Empty;
            this.PrerequisiteEntryName = string.Empty;
            this.PrerequisitePhoneBook = string.Empty;
#endif

#if (WIN2K8 || WIN7 || WIN8)
            this.IPv6DnsAddress = IPAddress.IPv6Any;
            this.IPv6DnsAddressAlt = IPAddress.IPv6Any;
#endif

#if (WIN7 || WIN8)
            this.IPv6Address = IPAddress.IPv6Any;
#endif            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the owner of the entry.
        /// </summary>
        public RasPhoneBook Owner
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the name of the entry.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the entry options.
        /// </summary>
        public RasEntryOptions Options
        {
            get
            {
                if (this.options == null)
                {
                    this.options = new RasEntryOptions();
                }

                return this.options;
            }

            set
            {
                this.options = value;
            }
        }

        /// <summary>
        /// Gets or sets the country/region identifier.
        /// </summary>
        public int CountryId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country/region code portion of the phone number.
        /// </summary>
        public int CountryCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        public string AreaCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of alternate phone numbers that are dialed in the order listed if the primary number fails.
        /// </summary>
        public Collection<string> AlternatePhoneNumbers
        {
            get
            {
                if (this.alternatePhoneNumbers == null)
                {
                    this.alternatePhoneNumbers = new Collection<string>();
                }

                return this.alternatePhoneNumbers;
            }

            internal set
            {
                this.alternatePhoneNumbers = value;
            }
        }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <remarks>Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following IP address' option to be set for TCP/IPv4.</remarks>
        public IPAddress IPAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IP address of the DNS server.
        /// </summary>
        /// <remarks>Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following DNS server addresses' option to be set for TCP/IPv4.</remarks>
        public IPAddress DnsAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IP address of an alternate DNS server.
        /// </summary>
        /// <remarks>Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following DNS server addresses' option to be set for TCP/IPv4.</remarks>
        public IPAddress DnsAddressAlt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IP address of the WINS server.
        /// </summary>
        public IPAddress WinsAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IP address of an alternate WINS server.
        /// </summary>
        public IPAddress WinsAddressAlt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the network protocol frame size.
        /// </summary>
        /// <remarks>This member is ignored unless <see cref="RasEntry.FramingProtocol"/> sets the <see cref="RasFramingProtocol.Slip"/> flag.</remarks>
        public int FrameSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the network protocols to negotiate.
        /// </summary>
        public RasNetworkProtocols NetworkProtocols
        {
            get
            {
                if (this.networkProtocols == null)
                {
                    this.networkProtocols = new RasNetworkProtocols();
                }

                return this.networkProtocols;
            }

            set
            {
                this.networkProtocols = value;
            }
        }

        /// <summary>
        /// Gets or sets the framing protocol used by the server.
        /// </summary>
        /// <remarks>To use compressed SLIP, set the <see cref="RasFramingProtocol.Slip"/> flag, and set the <see cref="RasEntryOptions.IPHeaderCompression"/> flag on the <see cref="RasEntry.Options"/> property.</remarks>
        public RasFramingProtocol FramingProtocol
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path of the script file.
        /// </summary>
        /// <remarks>To indicate a SWITCH.INF script name, set the first character to "[".</remarks>
        public string Script
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path to the custom-dial DLL.
        /// </summary>
        [Obsolete("This member is no longer used. The CustomDialDll property should be used instead.")]
        public string AutoDialDll
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the callback function for the customized AutoDial handler.
        /// </summary>
        [Obsolete("This member is no longer used.")]
        public string AutoDialFunc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the remote access device.
        /// </summary>
        /// <remarks>To retrieve a list of available devices, use the <see cref="RasDevice.GetDevices"/> method.</remarks>
        public RasDevice Device
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the X.25 PAD type.
        /// </summary>
        /// <remarks>This member should be an empty string unless the entry should dial using an X.25 PAD. This member maps to a section name in PAD.INF.</remarks>
        public string X25PadType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the X.25 address to connect to.
        /// </summary>
        /// <remarks>This member should be an empty string unless the entry should dial using an X.25 PAD or native X.25 device.</remarks>
        public string X25Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the facilities to request from the X.25 host upon connection.
        /// </summary>
        /// <remarks>This member is ignored if the <see cref="RasEntry.X25Address"/> is an empty string.</remarks>
        public string X25Facilities
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the additional connection information supplied to the X.25 host upon connection.
        /// </summary>
        /// <remarks>This member is ignored if the <see cref="RasEntry.X25Address"/> is an empty string.</remarks>
        public string X25UserData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        /// <remarks>This member is reserved for future use.</remarks>
        public int Channels
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the collection of multilink subentries associated with this entry.
        /// </summary>
        public RasSubEntryCollection SubEntries
        {
            get
            {
                if (this.subEntries == null)
                {
                    this.subEntries = new RasSubEntryCollection(this);
                }

                return this.subEntries;
            }
        }

        /// <summary>
        /// Gets or sets the dial mode for the multilink subentries associated with this entry.
        /// </summary>
        public RasDialMode DialMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the percent of total bandwidth that must be used before additional subentries are dialed.
        /// </summary>
        /// <remarks>This member is ignored unless <see cref="RasEntry.DialMode"/> sets the <see cref="RasDialMode.DialAsNeeded"/> flag.</remarks>
        public int DialExtraPercent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of seconds the number of seconds before additional subentries are connected.
        /// </summary>
        /// <remarks>This member is ignored unless <see cref="RasEntry.DialMode"/> sets the <see cref="RasDialMode.DialAsNeeded"/> flag.</remarks>
        public int DialExtraSampleSeconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the percent of total bandwidth used before subentries are disconnected.
        /// </summary>
        /// <remarks>This member is ignored unless <see cref="RasEntry.DialMode"/> sets the <see cref="RasDialMode.DialAsNeeded"/> flag.</remarks>
        public int HangUpExtraPercent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of seconds before subentries are disconnected.
        /// </summary>
        /// <remarks>This member is ignored unless <see cref="RasEntry.DialMode"/> sets the <see cref="RasDialMode.DialAsNeeded"/> flag.</remarks>
        public int HangUpExtraSampleSeconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of seconds after which the connection is terminated due to inactivity.
        /// </summary>
        /// <remarks>See the <see cref="DotRas.RasIdleDisconnectTimeout"/> class for possible values.</remarks>
        public int IdleDisconnectSeconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of phone book entry.
        /// </summary>
        public RasEntryType EntryType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of encryption to use with the connection.
        /// </summary>
        /// <remarks>This member does not affect how passwords are encrypted. Whether passwords are encrypted and how passwords are encrypted is determined by the authentication protocol.</remarks>
        public RasEncryptionType EncryptionType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the authentication key provided to the Extensible Authentication Protocol (EAP) vendor.
        /// </summary>
        public int CustomAuthKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the id of the phone book entry.
        /// </summary>
        public Guid Id
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the path for the dynamic link library (DLL) that implements the custom-dialing functions.
        /// </summary>
        public string CustomDialDll
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the VPN strategy to use when dialing a VPN connection.
        /// </summary>
        public RasVpnStrategy VpnStrategy
        {
            get;
            set;
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets or sets the Domain Name Service (DNS) suffix for the connection.
        /// </summary>
        /// <remarks><b>Windows XP and later:</b> This property is available.</remarks>
        public string DnsSuffix
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the TCP window size for all TCP sessions that run over this connection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Windows Vista and later: This member is ignored, the TCP window size is determined by the TCP stack.</b>
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public int TcpWindowSize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path to a phone book file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This member is only used for virtual private network (VPN) connections.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public string PrerequisitePhoneBook
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the entry name that will be dialed from the prerequisite phone book.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This member is only used for virtual private network (VPN) connections.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public string PrerequisiteEntryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of times RAS attempts to redial a connection.
        /// </summary>
        /// <remarks><b>Windows XP and later:</b> This property is available.</remarks>
        public int RedialCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of seconds to wait between redial attempts.
        /// </summary>
        /// <remarks><b>Windows XP and later:</b> This property is available.</remarks>
        public int RedialPause
        {
            get;
            set;
        }

#endif

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets or sets the IPv6 address of the preferred DNS server.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following DNS server addresses' option to be set for TCP/IPv6.
        /// </para>
        /// <para>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public IPAddress IPv6DnsAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IPv6 address of the alternate DNS server.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following DNS server addresses' option to be set for TCP/IPv6.
        /// </para>
        /// <para>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public IPAddress IPv6DnsAddressAlt
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the metric of the IPv4 stack for this interface.
        /// </summary>
        /// <remarks><b>Windows Vista and later:</b> This property is available.</remarks>
        public int IPv4InterfaceMetric
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the metric of the IPv6 stack for this interface.
        /// </summary>
        /// <remarks><b>Windows Vista and later:</b> This property is available.</remarks>
        public int IPv6InterfaceMetric
        {
            get;
            set;
        }

#endif

#if (WIN7 || WIN8)

        /// <summary>
        /// Gets or sets the IPv6 address.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this member to a value other than <see cref="IPAddress.Any"/> will cause the 'Use the following IP address' option to be set for TCP/IPv6.
        /// </para>
        /// <para>
        /// <b>Windows 7 and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public IPAddress IPv6Address
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the IPv6 subnet prefix length.
        /// </summary>
        /// <remarks><b>Windows 7 and later:</b> This property is available.</remarks>
        public int IPv6PrefixLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the amount of time, in minutes, that IKEv2 packets will be transmitted without a response before the connection is considered lost.
        /// </summary>
        /// <remarks><b>Windows 7 and later:</b> This property is available.</remarks>
        public int NetworkOutageTime
        {
            get;
            set;
        }

#endif

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether an entry exists within the phone book at the path specified.
        /// </summary>
        /// <param name="entryName">The name of the entry to check.</param>
        /// <param name="phoneBookPath">The full path (including filename) of the phone book.</param>
        /// <returns><b>true</b> if the entry exists, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentException"><paramref name="entryName"/> and/or <paramref name="phoneBookPath"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.IO.FileNotFoundException"><paramref name="phoneBookPath"/> was not found.</exception>
        /// <example>
        /// The following example shows how to test whether an entry exists within a phone book.
        /// <code lang="C#">
        /// <![CDATA[
        /// bool exists = RasEntry.Exists("VPN Connection", "C:\\MyPhoneBook.pbk");
        /// ]]>
        /// </code>
        /// <code lang="VB.NET">
        /// <![CDATA[
        /// Dim exists As Boolean = RasEntry.Exists("VPN Connection", "C:\MyPhoneBook.pbk")
        /// ]]>
        /// </code>
        /// </example>
        public static bool Exists(string entryName, string phoneBookPath)
        {
            if (string.IsNullOrEmpty(entryName))
            {
                ThrowHelper.ThrowArgumentException("entryName", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (string.IsNullOrEmpty(phoneBookPath))
            {
                ThrowHelper.ThrowArgumentException("phoneBookPath", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (!File.Exists(phoneBookPath))
            {
                ThrowHelper.ThrowFileNotFoundException(phoneBookPath, Resources.Argument_FileNotFound);
            }

            return SafeNativeMethods.Instance.ValidateEntryName(phoneBookPath, entryName) == NativeMethods.ERROR_ALREADY_EXISTS;
        }

        /// <summary>
        /// Creates a new dial-up entry.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="phoneNumber">The phone number to dial.</param>
        /// <param name="device">An <see cref="DotRas.RasDevice"/> to use for connecting.</param>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> or <paramref name="phoneNumber"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="device"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static RasEntry CreateDialUpEntry(string name, string phoneNumber, RasDevice device)
        {
            if (string.IsNullOrEmpty(name))
            {
                ThrowHelper.ThrowArgumentException("name", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (string.IsNullOrEmpty(phoneNumber))
            {
                ThrowHelper.ThrowArgumentException("phoneNumber", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (device == null)
            {
                ThrowHelper.ThrowArgumentNullException("device");
            }

            RasEntry entry = new RasEntry(name);

            entry.Device = device;
            entry.DialMode = RasDialMode.None;
            entry.EntryType = RasEntryType.Phone;
            entry.FramingProtocol = RasFramingProtocol.Ppp;
            entry.IdleDisconnectSeconds = RasIdleDisconnectTimeout.Default;
            entry.NetworkProtocols.IP = true;

#if (WINXP || WIN2K8 || WIN7 || WIN8)
            entry.RedialCount = 3;
            entry.RedialPause = 60;
#endif
#if (WIN2K8 || WIN7 || WIN8)
            entry.NetworkProtocols.IPv6 = true;
#endif

            entry.PhoneNumber = phoneNumber;
            entry.VpnStrategy = RasVpnStrategy.Default;

            return entry;
        }

        /// <summary>
        /// Creates a new virtual private network (VPN) entry.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="serverAddress">The server address to connect to.</param>
        /// <param name="strategy">The virtual private network (VPN) strategy of the connection.</param>
        /// <param name="device">An <see cref="DotRas.RasDevice"/> to use for connecting.</param>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        /// <remarks>The device for this connection is typically a WAN Miniport (L2TP) or WAN Miniport (PPTP).</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> or <paramref name="serverAddress"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="device"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static RasEntry CreateVpnEntry(string name, string serverAddress, RasVpnStrategy strategy, RasDevice device)
        {
            return CreateVpnEntry(name, serverAddress, strategy, device, true);
        }

        /// <summary>
        /// Creates a new virtual private network (VPN) entry.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="serverAddress">The server address to connect to.</param>
        /// <param name="strategy">The virtual private network (VPN) strategy of the connection.</param>
        /// <param name="device">An <see cref="DotRas.RasDevice"/> to use for connecting.</param>
        /// <param name="useRemoteDefaultGateway"><b>true</b> if the connection should use the remote default gateway, otherwise <b>false</b>.</param>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        /// <remarks>The device for this connection is typically a WAN Miniport (L2TP) or WAN Miniport (PPTP).</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> or <paramref name="serverAddress"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="device"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static RasEntry CreateVpnEntry(string name, string serverAddress, RasVpnStrategy strategy, RasDevice device, bool useRemoteDefaultGateway)
        {
            if (string.IsNullOrEmpty(name))
            {
                ThrowHelper.ThrowArgumentException("name", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (string.IsNullOrEmpty(serverAddress))
            {
                ThrowHelper.ThrowArgumentException("serverAddress", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (device == null)
            {
                ThrowHelper.ThrowArgumentNullException("device");
            }

            RasEntry entry = new RasEntry(name);

            entry.Device = device;
            entry.EncryptionType = RasEncryptionType.Require;
            entry.EntryType = RasEntryType.Vpn;
            entry.FramingProtocol = RasFramingProtocol.Ppp;
            entry.NetworkProtocols.IP = true;
            entry.Options.ModemLights = true;

#if (WIN7 || WIN8)
            if (strategy == RasVpnStrategy.IkeV2First || strategy == RasVpnStrategy.IkeV2Only)
            {
                entry.Options.RequireDataEncryption = true;
                entry.Options.RequireEap = true;
                entry.Options.RequireEncryptedPassword = false;
            }
            else
            {
                entry.Options.RequireEncryptedPassword = true;
            }
#else
            entry.Options.RequireEncryptedPassword = true;
#endif

            entry.Options.PreviewUserPassword = true;
            entry.Options.PreviewDomain = true;
            entry.Options.ShowDialingProgress = true;

            if (useRemoteDefaultGateway)
            {
                entry.Options.RemoteDefaultGateway = true;

#if (WIN2K8 || WIN7 || WIN8)
                entry.Options.IPv6RemoteDefaultGateway = true;
#endif
            }

#if (WINXP || WIN2K8 || WIN7 || WIN8)
            entry.RedialCount = 3;
            entry.RedialPause = 60;

            entry.Options.DoNotNegotiateMultilink = true;
            entry.Options.ReconnectIfDropped = true;
#endif
#if (WIN2K8 || WIN7 || WIN8)
            entry.Options.UseTypicalSettings = true;
            entry.NetworkProtocols.IPv6 = true;
#endif
            entry.PhoneNumber = serverAddress;
            entry.VpnStrategy = strategy;

            return entry;
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Creates a new broadband entry.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="device">An <see cref="DotRas.RasDevice"/> to use for connecting.</param>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        /// <remarks>
        /// <para>
        /// The device for this connection is typically a WAN Miniport (PPPOE) device.
        /// </para>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="device"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static RasEntry CreateBroadbandEntry(string name, RasDevice device)
        {
            return RasEntry.CreateBroadbandEntry(name, device, true);
        }

        /// <summary>
        /// Creates a new broadband entry.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="device">An <see cref="DotRas.RasDevice"/> to use for connecting.</param>
        /// <param name="useRemoteDefaultGateway"><b>true</b> if the connection should use the remote default gateway, otherwise <b>false</b>.</param>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        /// <remarks>
        /// <para>
        /// The device for this connection is typically a WAN Miniport (PPPOE) device.
        /// </para>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentException"><paramref name="name"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="device"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static RasEntry CreateBroadbandEntry(string name, RasDevice device, bool useRemoteDefaultGateway)
        {
            if (string.IsNullOrEmpty(name))
            {
                ThrowHelper.ThrowArgumentException("name", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (device == null)
            {
                ThrowHelper.ThrowArgumentNullException("device");
            }

            RasEntry entry = new RasEntry(name);

            entry.Device = device;
            entry.EncryptionType = RasEncryptionType.Optional;
            entry.EntryType = RasEntryType.Broadband;
            entry.Options.SecureFileAndPrint = true;
            entry.Options.SecureClientForMSNet = true;
            entry.Options.DoNotNegotiateMultilink = true;
            entry.Options.DoNotUseRasCredentials = true;
            entry.Options.Internet = true;
            entry.Options.DisableNbtOverIP = true;
            entry.Options.ModemLights = true;
            entry.Options.SecureLocalFiles = true;
            entry.Options.RequirePap = true;
            entry.Options.PreviewUserPassword = true;
            entry.Options.ShowDialingProgress = true;
            entry.Options.RequireChap = true;
            entry.Options.RequireMSChap2 = true;
            entry.Options.ReconnectIfDropped = true;
            entry.FramingProtocol = RasFramingProtocol.Ppp;
            entry.NetworkProtocols.IP = true;
            entry.PhoneNumber = string.Empty;
            entry.RedialCount = 3;
            entry.RedialPause = 60;

            if (useRemoteDefaultGateway)
            {
                entry.Options.RemoteDefaultGateway = true;

#if (WIN2K8 || WIN7 || WIN8)
                entry.Options.IPv6RemoteDefaultGateway = true;
#endif
            }

#if (WIN2K8 || WIN7 || WIN8)
            entry.NetworkProtocols.IPv6 = true;
#endif

            return entry;
        }

#endif

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasEntry"/> object.</returns>
        public object Clone()
        {
            RasEntry retval = new RasEntry(this.Name);

            if (this.AlternatePhoneNumbers != null && this.AlternatePhoneNumbers.Count > 0)
            {
                retval.AlternatePhoneNumbers = new Collection<string>();
                foreach (string value in this.AlternatePhoneNumbers)
                {
                    retval.AlternatePhoneNumbers.Add(value);
                }
            }

            retval.AreaCode = this.AreaCode;

#pragma warning disable 0618
            retval.AutoDialDll = this.AutoDialDll;
            retval.AutoDialFunc = this.AutoDialFunc;
#pragma warning restore 0618

            retval.Channels = this.Channels;
            retval.CountryCode = this.CountryCode;
            retval.CountryId = this.CountryId;
            retval.CustomAuthKey = this.CustomAuthKey;
            retval.CustomDialDll = this.CustomDialDll;
            retval.Device = this.Device;
            retval.DialExtraPercent = this.DialExtraPercent;
            retval.DialExtraSampleSeconds = this.DialExtraSampleSeconds;
            retval.DialMode = this.DialMode;
            retval.DnsAddress = this.DnsAddress;
            retval.DnsAddressAlt = this.DnsAddressAlt;
            retval.EncryptionType = this.EncryptionType;
            retval.EntryType = this.EntryType;
            retval.FrameSize = this.FrameSize;
            retval.FramingProtocol = this.FramingProtocol;
            retval.HangUpExtraPercent = this.HangUpExtraPercent;
            retval.HangUpExtraSampleSeconds = this.HangUpExtraSampleSeconds;
            retval.IdleDisconnectSeconds = this.IdleDisconnectSeconds;
            retval.IPAddress = this.IPAddress;
            retval.NetworkProtocols = this.NetworkProtocols;
            retval.Options = (RasEntryOptions)this.Options.Clone();
            retval.PhoneNumber = this.PhoneNumber;
            retval.Script = this.Script;

            if (this.SubEntries != null && this.SubEntries.Count > 0)
            {
                foreach (RasSubEntry subEntry in this.SubEntries)
                {
                    retval.SubEntries.Add((RasSubEntry)subEntry.Clone());
                }
            }

            retval.VpnStrategy = this.VpnStrategy;
            retval.WinsAddress = this.WinsAddress;
            retval.WinsAddressAlt = this.WinsAddressAlt;
            retval.X25Address = this.X25Address;
            retval.X25Facilities = this.X25Facilities;
            retval.X25PadType = this.X25PadType;
            retval.X25UserData = this.X25UserData;

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            retval.DnsSuffix = this.DnsSuffix;
            retval.TcpWindowSize = this.TcpWindowSize;
            retval.PrerequisitePhoneBook = this.PrerequisitePhoneBook;
            retval.PrerequisiteEntryName = this.PrerequisiteEntryName;
            retval.RedialCount = this.RedialCount;
            retval.RedialPause = this.RedialPause;

#endif
#if (WIN2K8 || WIN7 || WIN8)

            retval.IPv6DnsAddress = this.IPv6DnsAddress;
            retval.IPv6DnsAddressAlt = this.IPv6DnsAddressAlt;
            retval.IPv4InterfaceMetric = this.IPv4InterfaceMetric;
            retval.IPv6InterfaceMetric = this.IPv6InterfaceMetric;

#endif
#if (WIN7 || WIN8)

            retval.IPv6Address = this.IPv6Address;
            retval.IPv6PrefixLength = this.IPv6PrefixLength;
            retval.NetworkOutageTime = this.NetworkOutageTime;

#endif

            return retval;
        }

        /// <summary>
        /// Clears the stored credentials for the entry.
        /// </summary>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public bool ClearCredentials()
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            NativeMethods.RASCREDENTIALS credentials = new NativeMethods.RASCREDENTIALS();
            credentials.userName = string.Empty;
            credentials.password = string.Empty;
            credentials.domain = string.Empty;

            credentials.options = NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain;

            return RasHelper.Instance.SetCredentials(this.Owner.Path, this.Name, credentials, true);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Clears the stored credentials for the entry.
        /// </summary>
        /// <param name="key">The pre-shared key whose value to clear.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <remarks>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public bool ClearCredentials(RasPreSharedKey key)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            NativeMethods.RASCREDENTIALS credentials = new NativeMethods.RASCREDENTIALS();
            credentials.password = string.Empty;

            switch (key)
            {
                case RasPreSharedKey.Client:
                    credentials.options = NativeMethods.RASCM.PreSharedKey;
                    break;

                case RasPreSharedKey.Ddm:
                    credentials.options = NativeMethods.RASCM.DdmPreSharedKey;
                    break;

                case RasPreSharedKey.Server:
                    credentials.options = NativeMethods.RASCM.ServerPreSharedKey;
                    break;
            }

            return RasHelper.Instance.SetCredentials(this.Owner.Path, this.Name, credentials, true);
        }

#endif

        /// <summary>
        /// Retrieves the credentials for the entry.
        /// </summary>
        /// <returns>The credentials stored in the entry, otherwise a null reference (<b>Nothing</b> in Visual Basic) if the credentials did not exist.</returns>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public NetworkCredential GetCredentials()
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            return RasHelper.Instance.GetCredentials(this.Owner.Path, this.Name, NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Retrieves the credentials for the entry.
        /// </summary>
        /// <param name="key">The pre-shared key to retrieve.</param>
        /// <returns>The credentials stored in the entry, otherwise a null reference (<b>Nothing</b> in Visual Basic) if the credentials did not exist.</returns>
        /// <remarks>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public NetworkCredential GetCredentials(RasPreSharedKey key)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            NativeMethods.RASCM value = NativeMethods.RASCM.None;
            switch (key)
            {
                case RasPreSharedKey.Client:
                    value = NativeMethods.RASCM.PreSharedKey;
                    break;

                case RasPreSharedKey.Server:
                    value = NativeMethods.RASCM.ServerPreSharedKey;
                    break;

                case RasPreSharedKey.Ddm:
                    value = NativeMethods.RASCM.DdmPreSharedKey;
                    break;
            }

            return RasHelper.Instance.GetCredentials(this.Owner.Path, this.Name, value);
        }

#endif

        /// <summary>
        /// Retrieves connection specific authentication information.
        /// </summary>
        /// <returns>A byte array containing the authentication information, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        /// <remarks>This information is not specific to a particular user.</remarks>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public byte[] GetCustomAuthData()
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            return RasHelper.Instance.GetCustomAuthData(this.Owner.Path, this.Name);
        }

        /// <summary>
        /// Retrieves user-specific Extensible Authentication Protocol (EAP) information for the specified phone book entry.
        /// </summary>
        /// <returns>A byte array containing the EAP data, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public byte[] GetEapUserData()
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            return RasHelper.Instance.GetEapUserData(IntPtr.Zero, this.Owner.Path, this.Name);
        }

        /// <summary>
        /// Renames the entry.
        /// </summary>
        /// <param name="newEntryName">The new name of the entry.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentException"><paramref name="newEntryName"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic) or <paramref name="newEntryName"/> is invalid.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The current user does not have permissions to the phone book specified.</exception>
        public bool Rename(string newEntryName)
        {
            if (string.IsNullOrEmpty(newEntryName))
            {
                ThrowHelper.ThrowArgumentException("newEntryName", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            bool retval = false;

            if (this.Owner == null)
            {
                this.Name = newEntryName;
                retval = true;
            }
            else
            {
                if (!RasHelper.Instance.IsValidEntryName(this.Owner, newEntryName))
                {
                    ThrowHelper.ThrowArgumentException("newEntryName", Resources.Argument_InvalidEntryName, "newEntryName", newEntryName);
                }

                if (RasHelper.Instance.RenameEntry(this.Owner, this.Name, newEntryName))
                {
                    this.Owner.Entries.ChangeKey(this, newEntryName);
                    this.Name = newEntryName;

                    retval = true;
                }
            }

            return retval;
        }

        /// <summary>
        /// Removes the entry from the phone book.
        /// </summary>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        public bool Remove()
        {
            bool retval = false;

            if (this.Owner != null)
            {
                retval = this.Owner.Entries.Remove(this);
            }

            return retval;
        }

        /// <summary>
        /// Updates the entry.
        /// </summary>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public bool Update()
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            return RasHelper.Instance.SetEntryProperties(this.Owner, this);
        }

        /// <summary>
        /// Updates the custom authentication data.
        /// </summary>
        /// <param name="data">A byte array containing the custom authentication data.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public bool UpdateCustomAuthData(byte[] data)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            return RasHelper.Instance.SetCustomAuthData(this.Owner.Path, this.Name, data);
        }

        /// <summary>
        /// Updates the user-specific Extensible Authentication Protocol (EAP) information in the registry.
        /// </summary>
        /// <param name="data">A byte array containing the EAP data.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="data"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        public bool UpdateEapUserData(byte[] data)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            if (data == null)
            {
                ThrowHelper.ThrowArgumentNullException("data");
            }

            return RasHelper.Instance.SetEapUserData(IntPtr.Zero, this.Owner.Path, this.Name, data);
        }

        /// <summary>
        /// Updates the user credentials for the entry.
        /// </summary>
        /// <param name="value">An <see cref="System.Net.NetworkCredential"/> object containing user credentials.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public bool UpdateCredentials(NetworkCredential value)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            return Utilities.UpdateCredentials(this.Owner.Path, this.Name, value, RasUpdateCredential.User);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Updates the user credentials for the entry.
        /// </summary>
        /// <param name="value">An <see cref="System.Net.NetworkCredential"/> object containing user credentials.</param>
        /// <param name="storeCredentialsForAllUsers"><b>true</b> if the credentials should be stored for all users, otherwise <b>false</b>.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <remarks>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.InvalidOperationException">The entry is not associated with a phone book.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public bool UpdateCredentials(NetworkCredential value, bool storeCredentialsForAllUsers)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            return Utilities.UpdateCredentials(this.Owner.Path, this.Name, value, storeCredentialsForAllUsers ? RasUpdateCredential.AllUsers : RasUpdateCredential.User);
        }

        /// <summary>
        /// Updates the user credentials for the entry.
        /// </summary>
        /// <param name="key">The pre-shared key to update.</param>
        /// <param name="value">The value to set.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <remarks>
        /// <para>
        /// <b>Windows XP and later: This method is supported.</b>
        /// </para>
        /// </remarks>
        public bool UpdateCredentials(RasPreSharedKey key, string value)
        {
            if (this.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            NativeMethods.RASCREDENTIALS credentials = new NativeMethods.RASCREDENTIALS();
            credentials.password = value;

            switch (key)
            {
                case RasPreSharedKey.Client:
                    credentials.options = NativeMethods.RASCM.PreSharedKey;
                    break;

                case RasPreSharedKey.Server:
                    credentials.options = NativeMethods.RASCM.ServerPreSharedKey;
                    break;

                case RasPreSharedKey.Ddm:
                    credentials.options = NativeMethods.RASCM.DdmPreSharedKey;
                    break;
            }

            return RasHelper.Instance.SetCredentials(this.Owner.Path, this.Name, credentials, false);
        }

#endif

        #endregion
    }
}