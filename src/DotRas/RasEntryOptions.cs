//--------------------------------------------------------------------------
// <copyright file="RasEntryOptions.cs" company="Jeff Winn">
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
    /// Represents options for a remote access service (RAS) entry. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class RasEntryOptions : ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEntryOptions"/> class.
        /// </summary>
        public RasEntryOptions()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the country and area codes are used to construct the phone number.
        /// </summary>
        /// <remarks>This value corresponds to the <b>Use dialing rules</b> checkbox on the connection properties dialog box.</remarks>
        public bool UseCountryAndAreaCodes
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IP header compression will be used on PPP (Point-to-Point) connections.
        /// </summary>
        public bool IPHeaderCompression
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote default gateway will be used.
        /// </summary>
        /// <remarks>This value corresponds to the <b>Use default gateway on remote network</b> checkbox in the TCP/IP settings dialog box.</remarks>
        public bool RemoteDefaultGateway
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service will disable the PPP LCP extensions.
        /// </summary>
        /// <remarks>This option causes RAS to disable the PPP LCP extensions defined in RFC 1570. This may be necessary for certain older PPP implementations, but interferes with features such as server callback. Do not set this option unless specifically required.</remarks>
        public bool DisableLcpExtensions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service displays a terminal window for user input before dialing the connection.
        /// </summary>
        /// <remarks>This option only works if the entry is dialed by the <see cref="RasDialDialog"/> component. This option has no effect if the entry is dialed by <see cref="RasDialer"/>.</remarks>
        public bool TerminalBeforeDial
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service displays a terminal window for user input after dialing the connection.
        /// </summary>
        /// <remarks>This option only works if the entry is dialed by the <see cref="RasDialDialog"/> component. This option has no effect if the entry is dialed by <see cref="RasDialer"/>.</remarks>
        public bool TerminalAfterDial
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service will display a status monitor in the taskbar.
        /// </summary>
        public bool ModemLights
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether software compression will be negotiated by the link.
        /// </summary>
        public bool SoftwareCompression
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether only secure password schemes can be used to authenticate the client.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this option is set, only secure password schemes can be used to authenticate the client with the server. This prevents the PPP driver from using the PAP plain-text authentication protocol (PAP) to authenticate the client, however MD5-CHAP and SPAP are supported. Clear this option for increased interoperability, and set it for increased security. Setting this option also sets the <see cref="RequireSpap"/>, <see cref="RequireChap"/>, <see cref="RequireMSChap"/>, and <see cref="RequireMSChap2"/> options.
        /// </para>
        /// <para>
        /// This option corresponds to the <b>Require Encrypted Password</b> checkbox in the Security settings dialog box.
        /// </para>
        /// </remarks>
        public bool RequireEncryptedPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether only the Microsoft secure password scheme (MSCHAP) can be used to authenticate the client.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this option is set, only the Microsoft secure password scheme, MSCHAP, can be used to authenticate the client with the server. This flag prevents the PPP driver from using the PPP plain-text authentication protocol (PAP), MD5-CHAP, or SPAP. Setting this option also sets the <see cref="RequireMSChap"/> and <see cref="RequireMSChap2"/> options.
        /// </para>
        /// <para>
        /// This option corresponds to the <b>Require Microsoft Encrypted Password</b> checkbox in the Security settings dialog box.
        /// </para>
        /// </remarks>
        public bool RequireMSEncryptedPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether data encryption must be negotiated successfully or the connection should be dropped.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option corresponds to the <b>Require Data Encryption</b> checkbox in the Security settings dialog box.
        /// </para>
        /// <para>
        /// This option is ignored unless the <see cref="RequireMSEncryptedPassword"/> option is also set.
        /// </para>
        /// </remarks>
        public bool RequireDataEncryption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service logs onto the network after the PPP (Point-to-Point) connection is established.
        /// </summary>
        public bool NetworkLogOn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access uses the currently logged on user credentials when dialing this entry.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option corresponds to the <b>Use Current Username and Password</b> checkbox in the Security settings dialog box.
        /// </para>
        /// <para>
        /// This option is ignored unless the <see cref="RequireMSEncryptedPassword"/> option is also set.
        /// </para>
        /// </remarks>
        public bool UseLogOnCredentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether alternate numbers are promoted to the primary number when connected successfully.
        /// </summary>
        /// <remarks>This option corresponds to the <b>Move successful numbers to the top of the list</b> checkbox in the AlterNate Phone Numbers dialog box.</remarks>
        public bool PromoteAlternates
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an existing remote file system and remote printer bindings are located before making a connection.
        /// </summary>
        public bool SecureLocalFiles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Extensible Authentication Protocol (EAP) must be supported for authentication.
        /// </summary>
        public bool RequireEap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Password Authentication Protocol (PAP) must be supported for authentication.
        /// </summary>
        public bool RequirePap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Shiva's Password Authentication Protocol (SPAP) must be supported for authentication.
        /// </summary>
        public bool RequireSpap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the connection will use custom encryption.
        /// </summary>
        public bool CustomEncryption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access dialer should display the phone number being dialed.
        /// </summary>
        public bool PreviewPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether all modems on the computer will share the same phone number.
        /// </summary>
        public bool SharedPhoneNumbers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access dialer should display the username and password prior to dialing.
        /// </summary>
        public bool PreviewUserPassword
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access dialer should display the domain name prior to dialing.
        /// </summary>
        /// <remarks>This option corresponds to the <b>Include Windows logon domain</b> checkbox in the connection properties dialog box.</remarks>
        public bool PreviewDomain
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access dialer should display its progress while establishing the connection.
        /// </summary>
        public bool ShowDialingProgress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Challenge Handshake Authentication Protocol (CHAP) must be supported for authentication.
        /// </summary>
        public bool RequireChap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Microsoft Challenge Handshake Authentication Protocol (MSCHAP) must be supported for authentication.
        /// </summary>
        public bool RequireMSChap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Microsoft Challenge Handshake Authentication Protocol (MSCHAP) version 2 must be supported for authentication.
        /// </summary>
        public bool RequireMSChap2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether MSCHAP must also send the LAN Manager hashed password.
        /// </summary>
        /// <remarks>This option also requires the <see cref="RequireMSChap"/> option is set.</remarks>
        public bool RequireWin95MSChap
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service must invoke a custom scripting assembly after establishing a connection.
        /// </summary>
        public bool CustomScript
        {
            get;
            set;
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets or sets a value indicating whether users will be prevented from using file and print servers over the connection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option is the equivalent of clearing the <b>File and Print Sharing for Microsoft Networks</b> checkbox in the connection properties dialog box.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool SecureFileAndPrint
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the client will be secured for Microsoft networks.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option is the equivalent of clearing the <b>Client for Microsoft Networks</b> checkbox in the connection properties dialog box.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool SecureClientForMSNet
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default behavior is not to negotiate multilink.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option is the equivalent of clearing the <b>Negotiate multi-link for single-link connection</b> checkbox in the connection properties dialog box on the PPP settings dialog.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool DoNotNegotiateMultilink
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the default credentials to access network resources.
        /// </summary>
        /// <remarks>
        /// <b>Windows XP and later:</b> This property is available.
        /// </remarks>
        public bool DoNotUseRasCredentials
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a pre-shared key is used for IPSec authentication.
        /// </summary>
        /// <remarks>
        /// <b>Windows XP and later:</b> This property is available.
        /// </remarks>
        public bool UsePreSharedKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entry connects to the Internet.
        /// </summary>
        /// <remarks>
        /// <b>Windows XP and later:</b> This property is available.
        /// </remarks>
        public bool Internet
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether NBT probing is disabled for this connection.
        /// </summary>
        /// <remarks>
        /// <b>Windows XP and later:</b> This property is available.
        /// </remarks>
        public bool DisableNbtOverIP
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the global device settings will be used by the entry.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option causes the entry device to be ignored.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool UseGlobalDeviceSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote access service should attempt to re-establish the connection if the connection is lost.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option corresponds to the <b>Redial if line is dropped</b> checkbox in the connection properties dialog box.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool ReconnectIfDropped
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the same set of phone numbers are used for all subentries in a multilink connection.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option corresponds to the <b>All devices use the same number</b> checkbox in the connection properties dialog box.
        /// </para>
        /// <para>
        /// <b>Windows XP and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool SharePhoneNumbers
        {
            get;
            set;
        }

#endif
#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets or sets a value indicating whether the routing compartments feature is enabled.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool SecureRoutingCompartment
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a connection should be configured to use the typical settings for authentication and encryption.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool UseTypicalSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the remote default gateway will be used on an IPv6 connection.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool IPv6RemoteDefaultGateway
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the IP address should be registered with the DNS server when connected.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool RegisterIPWithDns
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the DNS suffix for this connection should be used for DNS registration.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool UseDnsSuffixForRegistration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the IKE validation check will not be performed.
        /// </summary>
        /// <remarks>
        /// <b>Windows Vista and later:</b> This property is available.
        /// </remarks>
        public bool DisableIkeNameEkuCheck
        {
            get;
            set;
        }

#endif
#if (WIN7 || WIN8)

        /// <summary>
        /// Gets or sets a value indicating whether a class based route based on the VPN interface IP address will not be added.
        /// </summary>
        /// <remarks>
        /// <b>Windows 7 and later:</b> This property is available.
        /// </remarks>
        public bool DisableClassBasedStaticRoute
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the client will not be able to change the external IP address of the IKEv2 VPN connection.
        /// </summary>
        /// <remarks>
        /// <b>Windows 7 and later:</b> This property is available.
        /// </remarks>
        public bool DisableMobility
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether machine certificates are used for IKEv2 authentication.
        /// </summary>
        /// <remarks>
        /// <b>Windows 7 and later:</b> This property is available.
        /// </remarks>
        public bool RequireMachineCertificates
        {
            get;
            set;
        }

#endif

#if (WIN8)

        /// <summary>
        /// Gets or sets a value indicating whether a pre-shared key is used by the initiator for IPSec authentication.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This flag is only used by IKEv2 tunnel based demand-dial connections.
        /// </para>
        /// <para>
        /// <b>Windows 8 and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool UsePreSharedKeyForIkeV2Initiator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a pre-shared key is used by the responder for IPSec authentication.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This flag is only used by IKEv2 tunnel based demand-dial connections.
        /// </para>
        /// <para>
        /// <b>Windows 8 and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool UsePreSharedKeyForIkeV2Responder
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user credentials will be cached at the end of a successful authentication.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This option corresponds to the <b>Remember my credentials</b> checkbox in the connection properties dialog box.
        /// </para>
        /// <para>
        /// <b>Windows 8 and later:</b> This property is available.
        /// </para>
        /// </remarks>
        public bool CacheCredentials
        {
            get;
            set;
        }

#endif

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasEntryOptions"/> object.</returns>
        public object Clone()
        {
            RasEntryOptions retval = new RasEntryOptions();

            retval.UseCountryAndAreaCodes = this.UseCountryAndAreaCodes;
            retval.IPHeaderCompression = this.IPHeaderCompression;
            retval.RemoteDefaultGateway = this.RemoteDefaultGateway;
            retval.DisableLcpExtensions = this.DisableLcpExtensions;
            retval.TerminalBeforeDial = this.TerminalBeforeDial;
            retval.TerminalAfterDial = this.TerminalAfterDial;
            retval.ModemLights = this.ModemLights;
            retval.SoftwareCompression = this.SoftwareCompression;
            retval.RequireEncryptedPassword = this.RequireEncryptedPassword;
            retval.RequireMSEncryptedPassword = this.RequireMSEncryptedPassword;
            retval.RequireDataEncryption = this.RequireDataEncryption;
            retval.NetworkLogOn = this.NetworkLogOn;
            retval.UseLogOnCredentials = this.UseLogOnCredentials;
            retval.PromoteAlternates = this.PromoteAlternates;
            retval.SecureLocalFiles = this.SecureLocalFiles;
            retval.RequireEap = this.RequireEap;
            retval.RequirePap = this.RequirePap;
            retval.RequireSpap = this.RequireSpap;
            retval.CustomEncryption = this.CustomEncryption;
            retval.PreviewPhoneNumber = this.PreviewPhoneNumber;
            retval.SharedPhoneNumbers = this.SharedPhoneNumbers;
            retval.PreviewUserPassword = this.PreviewUserPassword;
            retval.PreviewDomain = this.PreviewDomain;
            retval.ShowDialingProgress = this.ShowDialingProgress;
            retval.RequireChap = this.RequireChap;
            retval.RequireMSChap = this.RequireMSChap;
            retval.RequireMSChap2 = this.RequireMSChap2;
            retval.RequireWin95MSChap = this.RequireWin95MSChap;
            retval.CustomScript = this.CustomScript;

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            retval.SecureFileAndPrint = this.SecureFileAndPrint;
            retval.SecureClientForMSNet = this.SecureClientForMSNet;
            retval.DoNotNegotiateMultilink = this.DoNotNegotiateMultilink;
            retval.DoNotUseRasCredentials = this.DoNotUseRasCredentials;
            retval.UsePreSharedKey = this.UsePreSharedKey;
            retval.Internet = this.Internet;
            retval.DisableNbtOverIP = this.DisableNbtOverIP;
            retval.UseGlobalDeviceSettings = this.UseGlobalDeviceSettings;
            retval.ReconnectIfDropped = this.ReconnectIfDropped;
            retval.SharePhoneNumbers = this.SharePhoneNumbers;

#endif
#if (WIN2K8 || WIN7 || WIN8)

            retval.SecureRoutingCompartment = this.SecureRoutingCompartment;
            retval.UseTypicalSettings = this.UseTypicalSettings;
            retval.IPv6RemoteDefaultGateway = this.IPv6RemoteDefaultGateway;
            retval.RegisterIPWithDns = this.RegisterIPWithDns;
            retval.UseDnsSuffixForRegistration = this.UseDnsSuffixForRegistration;
            retval.DisableIkeNameEkuCheck = this.DisableIkeNameEkuCheck;

#endif
#if (WIN7 || WIN8)

            retval.DisableClassBasedStaticRoute = this.DisableClassBasedStaticRoute;
            retval.DisableMobility = this.DisableMobility;
            retval.RequireMachineCertificates = this.RequireMachineCertificates;

#endif
#if (WIN8)

            retval.UsePreSharedKeyForIkeV2Initiator = this.UsePreSharedKeyForIkeV2Initiator;
            retval.UsePreSharedKeyForIkeV2Responder = this.UsePreSharedKeyForIkeV2Responder;
            retval.CacheCredentials = this.CacheCredentials;

#endif

            return retval;
        }

        #endregion
    }
}