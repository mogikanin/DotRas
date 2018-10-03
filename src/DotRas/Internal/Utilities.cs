//--------------------------------------------------------------------------
// <copyright file="Utilities.cs" company="Jeff Winn">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using Diagnostics;

    /// <summary>
    /// Contains utility methods for the assembly.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Marshals data from an unmanaged memory block to a newly allocated managed object.
        /// </summary>
        /// <typeparam name="T">The type of managed object.</typeparam>
        /// <param name="ptr">A pointer to an unmanaged block of memory.</param>
        /// <returns>The managed object.</returns>
        public static T PtrToStructure<T>(IntPtr ptr)
        {
            T result = default(T);

            try
            {
                result = (T)Marshal.PtrToStructure(ptr, typeof(T));
                return result;
            }
            finally
            {
                DiagnosticTrace.Default.TraceEvent(TraceEventType.Verbose, new MarshalStructTraceEvent(result));
            }
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The full path (including filename) of the file.</param>
        public static void CreateFile(FileInfo file)
        {
            if (!file.Exists)
            {
                if (!file.Directory.Exists)
                {
                    // The path where the file should be placed does not entirely exist
                    file.Directory.Create();
                }

                // The file does not exist, create the new file.
                using (FileStream fs = new FileStream(file.FullName, FileMode.Create))
                {
                }
            }
        }

        /// <summary>
        /// Indicates whether a flag has been set.
        /// </summary>
        /// <param name="input">The input to check.</param>
        /// <param name="value">The value to locate.</param>
        /// <returns><b>true</b> if the flag has been set; otherwise <b>false</b>.</returns>
        public static bool HasFlag(Enum input, Enum value)
        {
            return (Convert.ToUInt64(input, CultureInfo.InvariantCulture) & Convert.ToUInt64(value, CultureInfo.InvariantCulture)) != 0;
        }

        /// <summary>
        /// Sets the flag.
        /// </summary>
        /// <param name="condition">The condition that must pass for the flag to be set.</param>
        /// <param name="value">The value to set.</param>
        /// <returns>The value that should be combined to the flag.</returns>
        public static ulong SetFlag(bool condition, Enum value)
        {
            return condition ? Convert.ToUInt64(value, CultureInfo.InvariantCulture) : 0;
        }

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RASEO"/> flags for the entry specified.
        /// </summary>
        /// <param name="entry">The entry whose options to retrieve.</param>
        /// <returns>The <see cref="NativeMethods.RASEO"/> flags.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="entry"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static NativeMethods.RASEO GetRasEntryOptions(RasEntry entry)
        {
            NativeMethods.RASEO options = NativeMethods.RASEO.None;

            if (entry != null)
            {
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.UseCountryAndAreaCodes, NativeMethods.RASEO.UseCountryAndAreaCodes);
                options |= (NativeMethods.RASEO)SetFlag(!IsIPAddressNullOrAnyAddress(entry.IPAddress), NativeMethods.RASEO.SpecificIPAddress);
                options |= (NativeMethods.RASEO)SetFlag(!IsIPAddressNullOrAnyAddress(entry.DnsAddress) || !IsIPAddressNullOrAnyAddress(entry.DnsAddressAlt) || !IsIPAddressNullOrAnyAddress(entry.WinsAddress) || !IsIPAddressNullOrAnyAddress(entry.WinsAddressAlt), NativeMethods.RASEO.SpecificNameServers);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.IPHeaderCompression, NativeMethods.RASEO.IPHeaderCompression);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RemoteDefaultGateway, NativeMethods.RASEO.RemoteDefaultGateway);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.DisableLcpExtensions, NativeMethods.RASEO.DisableLcpExtensions);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.TerminalBeforeDial, NativeMethods.RASEO.TerminalBeforeDial);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.TerminalAfterDial, NativeMethods.RASEO.TerminalAfterDial);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.ModemLights, NativeMethods.RASEO.ModemLights);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.SoftwareCompression, NativeMethods.RASEO.SoftwareCompression);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireEncryptedPassword, NativeMethods.RASEO.RequireEncryptedPassword);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireMSEncryptedPassword, NativeMethods.RASEO.RequireMSEncryptedPassword);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireDataEncryption, NativeMethods.RASEO.RequireDataEncryption);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.NetworkLogOn, NativeMethods.RASEO.NetworkLogOn);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.UseLogOnCredentials, NativeMethods.RASEO.UseLogOnCredentials);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.PromoteAlternates, NativeMethods.RASEO.PromoteAlternates);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.SecureLocalFiles, NativeMethods.RASEO.SecureLocalFiles);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireEap, NativeMethods.RASEO.RequireEap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequirePap, NativeMethods.RASEO.RequirePap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireSpap, NativeMethods.RASEO.RequireSpap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.CustomEncryption, NativeMethods.RASEO.Custom);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.PreviewPhoneNumber, NativeMethods.RASEO.PreviewPhoneNumber);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.SharedPhoneNumbers, NativeMethods.RASEO.SharedPhoneNumbers);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.PreviewUserPassword, NativeMethods.RASEO.PreviewUserPassword);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.PreviewDomain, NativeMethods.RASEO.PreviewDomain);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.ShowDialingProgress, NativeMethods.RASEO.ShowDialingProgress);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireChap, NativeMethods.RASEO.RequireChap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireMSChap, NativeMethods.RASEO.RequireMSChap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireMSChap2, NativeMethods.RASEO.RequireMSChap2);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.RequireWin95MSChap, NativeMethods.RASEO.RequireWin95MSChap);
                options |= (NativeMethods.RASEO)SetFlag(entry.Options.CustomScript, NativeMethods.RASEO.CustomScript);
            }

            return options;
        }

        /// <summary>
        /// Sets the options on a <see cref="RasEntry"/> for the flags specified.
        /// </summary>
        /// <param name="entry">The entry whose options to set.</param>
        /// <param name="value">The flags of the entry.</param>
        public static void SetRasEntryOptions(RasEntry entry, NativeMethods.RASEO value)
        {
            if (entry != null)
            {
                RasEntryOptions options = entry.Options;

                options.UseCountryAndAreaCodes = HasFlag(value, NativeMethods.RASEO.UseCountryAndAreaCodes);
                options.IPHeaderCompression = HasFlag(value, NativeMethods.RASEO.IPHeaderCompression);
                options.RemoteDefaultGateway = HasFlag(value, NativeMethods.RASEO.RemoteDefaultGateway);
                options.DisableLcpExtensions = HasFlag(value, NativeMethods.RASEO.DisableLcpExtensions);
                options.TerminalBeforeDial = HasFlag(value, NativeMethods.RASEO.TerminalBeforeDial);
                options.TerminalAfterDial = HasFlag(value, NativeMethods.RASEO.TerminalAfterDial);
                options.ModemLights = HasFlag(value, NativeMethods.RASEO.ModemLights);
                options.SoftwareCompression = HasFlag(value, NativeMethods.RASEO.SoftwareCompression);
                options.RequireEncryptedPassword = HasFlag(value, NativeMethods.RASEO.RequireEncryptedPassword);
                options.RequireMSEncryptedPassword = HasFlag(value, NativeMethods.RASEO.RequireMSEncryptedPassword);
                options.RequireDataEncryption = HasFlag(value, NativeMethods.RASEO.RequireDataEncryption);
                options.NetworkLogOn = HasFlag(value, NativeMethods.RASEO.NetworkLogOn);
                options.UseLogOnCredentials = HasFlag(value, NativeMethods.RASEO.UseLogOnCredentials);
                options.PromoteAlternates = HasFlag(value, NativeMethods.RASEO.PromoteAlternates);
                options.SecureLocalFiles = HasFlag(value, NativeMethods.RASEO.SecureLocalFiles);
                options.RequireEap = HasFlag(value, NativeMethods.RASEO.RequireEap);
                options.RequirePap = HasFlag(value, NativeMethods.RASEO.RequirePap);
                options.RequireSpap = HasFlag(value, NativeMethods.RASEO.RequireSpap);
                options.CustomEncryption = HasFlag(value, NativeMethods.RASEO.Custom);
                options.PreviewPhoneNumber = HasFlag(value, NativeMethods.RASEO.PreviewPhoneNumber);
                options.SharedPhoneNumbers = HasFlag(value, NativeMethods.RASEO.SharedPhoneNumbers);
                options.PreviewUserPassword = HasFlag(value, NativeMethods.RASEO.PreviewUserPassword);
                options.PreviewDomain = HasFlag(value, NativeMethods.RASEO.PreviewDomain);
                options.ShowDialingProgress = HasFlag(value, NativeMethods.RASEO.ShowDialingProgress);
                options.RequireChap = HasFlag(value, NativeMethods.RASEO.RequireChap);
                options.RequireMSChap = HasFlag(value, NativeMethods.RASEO.RequireMSChap);
                options.RequireMSChap2 = HasFlag(value, NativeMethods.RASEO.RequireMSChap2);
                options.RequireWin95MSChap = HasFlag(value, NativeMethods.RASEO.RequireWin95MSChap);
                options.CustomScript = HasFlag(value, NativeMethods.RASEO.CustomScript);
            }
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Sets the connection options on a <see cref="RasConnection"/> for the flags specified.
        /// </summary>
        /// <param name="connection">The connection whose connection options to set.</param>
        /// <param name="value">The flags of the entry.</param>
        public static void SetRasConnectionOptions(RasConnection connection, NativeMethods.RASCF value)
        {
            if (connection != null)
            {
                RasConnectionOptions options = connection.ConnectionOptions;

                options.AllUsers = HasFlag(value, NativeMethods.RASCF.AllUsers);
                options.GlobalCredentials = HasFlag(value, NativeMethods.RASCF.GlobalCredentials);
                options.OwnerKnown = HasFlag(value, NativeMethods.RASCF.OwnerKnown);
                options.OwnerMatch = HasFlag(value, NativeMethods.RASCF.OwnerMatch);
            }
        }

#endif

        /// <summary>
        /// Sets the network protocols on a <see cref="RasEntry"/> for the flags specified.
        /// </summary>
        /// <param name="entry">The entry whose options to set.</param>
        /// <param name="value">The flags of the entry.</param>
        public static void SetRasNetworkProtocols(RasEntry entry, NativeMethods.RASNP value)
        {
            if (entry != null)
            {
                RasNetworkProtocols protocols = entry.NetworkProtocols;

#pragma warning disable 0618
                protocols.NetBeui = HasFlag(value, NativeMethods.RASNP.NetBeui);
#pragma warning restore 0618
                protocols.Ipx = HasFlag(value, NativeMethods.RASNP.Ipx);
                protocols.IP = HasFlag(value, NativeMethods.RASNP.IP);
#if (WIN2K8 || WIN7 || WIN8)

                protocols.IPv6 = HasFlag(value, NativeMethods.RASNP.IPv6);

#endif
            }
        }

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RASNP"/> flags for the network protocols specified.
        /// </summary>
        /// <param name="value">The network protocols whose flags to retrieve.</param>
        /// <returns>The <see cref="NativeMethods.RASNP"/> flags.</returns>
        public static NativeMethods.RASNP GetRasNetworkProtocols(RasNetworkProtocols value)
        {
            NativeMethods.RASNP protocols = NativeMethods.RASNP.None;

            if (value != null)
            {
#pragma warning disable 0618
                protocols |= (NativeMethods.RASNP)SetFlag(value.NetBeui, NativeMethods.RASNP.NetBeui);
#pragma warning restore 0618
                protocols |= (NativeMethods.RASNP)SetFlag(value.Ipx, NativeMethods.RASNP.Ipx);
                protocols |= (NativeMethods.RASNP)SetFlag(value.IP, NativeMethods.RASNP.IP);
#if (WIN2K8 || WIN7 || WIN8)

                protocols |= (NativeMethods.RASNP)SetFlag(value.IPv6, NativeMethods.RASNP.IPv6);

#endif
            }

            return protocols;
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Retrieves the <see cref="NativeMethods.RASEO2"/> flags for the entry specified.
        /// </summary>
        /// <param name="entry">The entry whose options to retrieve.</param>
        /// <returns>The <see cref="NativeMethods.RASEO2"/> flags.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="entry"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static NativeMethods.RASEO2 GetRasEntryExtendedOptions(RasEntry entry)
        {
            if (entry == null)
            {
                ThrowHelper.ThrowArgumentNullException("entry");
            }

            NativeMethods.RASEO2 options = NativeMethods.RASEO2.None;

            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.SecureFileAndPrint, NativeMethods.RASEO2.SecureFileAndPrint);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.SecureClientForMSNet, NativeMethods.RASEO2.SecureClientForMSNet);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DoNotNegotiateMultilink, NativeMethods.RASEO2.DoNotNegotiateMultilink);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DoNotUseRasCredentials, NativeMethods.RASEO2.DoNotUseRasCredentials);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.UsePreSharedKey, NativeMethods.RASEO2.UsePreSharedKey);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.Internet, NativeMethods.RASEO2.Internet);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DisableNbtOverIP, NativeMethods.RASEO2.DisableNbtOverIP);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.UseGlobalDeviceSettings, NativeMethods.RASEO2.UseGlobalDeviceSettings);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.ReconnectIfDropped, NativeMethods.RASEO2.ReconnectIfDropped);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.SharePhoneNumbers, NativeMethods.RASEO2.SharePhoneNumbers);

#if (WIN2K8 || WIN7 || WIN8)

            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.SecureRoutingCompartment, NativeMethods.RASEO2.SecureRoutingCompartment);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.UseTypicalSettings, NativeMethods.RASEO2.UseTypicalSettings);
            options |= (NativeMethods.RASEO2)SetFlag(!IsIPAddressNullOrAnyAddress(entry.IPv6DnsAddress) || !IsIPAddressNullOrAnyAddress(entry.IPv6DnsAddressAlt), NativeMethods.RASEO2.IPv6SpecificNameServer);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.IPv6RemoteDefaultGateway, NativeMethods.RASEO2.IPv6RemoteDefaultGateway);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.RegisterIPWithDns, NativeMethods.RASEO2.RegisterIPWithDns);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.UseDnsSuffixForRegistration, NativeMethods.RASEO2.UseDnsSuffixForRegistration);
            options |= (NativeMethods.RASEO2)SetFlag(entry.IPv4InterfaceMetric != 0, NativeMethods.RASEO2.IPv4ExplicitMetric);
            options |= (NativeMethods.RASEO2)SetFlag(entry.IPv6InterfaceMetric != 0, NativeMethods.RASEO2.IPv6ExplicitMetric);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DisableIkeNameEkuCheck, NativeMethods.RASEO2.DisableIkeNameEkuCheck);

#endif
#if (WIN7 || WIN8)

            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DisableClassBasedStaticRoute, NativeMethods.RASEO2.DisableClassBasedStaticRoute);
            options |= (NativeMethods.RASEO2)SetFlag(!IsIPAddressNullOrAnyAddress(entry.IPv6Address), NativeMethods.RASEO2.IPv6SpecificAddress);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.DisableMobility, NativeMethods.RASEO2.DisableMobility);
            options |= (NativeMethods.RASEO2)SetFlag(entry.Options.RequireMachineCertificates, NativeMethods.RASEO2.RequireMachineCertificates);

#endif
#if (WIN8)

            options |= (NativeMethods.RASEO2)Utilities.SetFlag(entry.Options.UsePreSharedKeyForIkeV2Initiator, NativeMethods.RASEO2.UsePreSharedKeyForIkev2Initiator);
            options |= (NativeMethods.RASEO2)Utilities.SetFlag(entry.Options.UsePreSharedKeyForIkeV2Responder, NativeMethods.RASEO2.UsePreSharedKeyForIkev2Responder);
            options |= (NativeMethods.RASEO2)Utilities.SetFlag(entry.Options.CacheCredentials, NativeMethods.RASEO2.CacheCredentials);

#endif

            return options;
        }

        /// <summary>
        /// Sets the extended options on a <see cref="RasEntry"/> for the flags specified.
        /// </summary>
        /// <param name="entry">The entry whose options to set.</param>
        /// <param name="value">The flags of the entry.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="entry"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static void SetRasEntryExtendedOptions(RasEntry entry, NativeMethods.RASEO2 value)
        {
            if (entry == null)
            {
                ThrowHelper.ThrowArgumentNullException("entry");
            }

            RasEntryOptions options = entry.Options;

            options.SecureFileAndPrint = HasFlag(value, NativeMethods.RASEO2.SecureFileAndPrint);
            options.SecureClientForMSNet = HasFlag(value, NativeMethods.RASEO2.SecureClientForMSNet);
            options.DoNotNegotiateMultilink = HasFlag(value, NativeMethods.RASEO2.DoNotNegotiateMultilink);
            options.DoNotUseRasCredentials = HasFlag(value, NativeMethods.RASEO2.DoNotUseRasCredentials);
            options.UsePreSharedKey = HasFlag(value, NativeMethods.RASEO2.UsePreSharedKey);
            options.Internet = HasFlag(value, NativeMethods.RASEO2.Internet);
            options.DisableNbtOverIP = HasFlag(value, NativeMethods.RASEO2.DisableNbtOverIP);
            options.UseGlobalDeviceSettings = HasFlag(value, NativeMethods.RASEO2.UseGlobalDeviceSettings);
            options.ReconnectIfDropped = HasFlag(value, NativeMethods.RASEO2.ReconnectIfDropped);
            options.SharePhoneNumbers = HasFlag(value, NativeMethods.RASEO2.SharePhoneNumbers);

#if (WIN2K8 || WIN7 || WIN8)

            options.SecureRoutingCompartment = HasFlag(value, NativeMethods.RASEO2.SecureRoutingCompartment);
            options.UseTypicalSettings = HasFlag(value, NativeMethods.RASEO2.UseTypicalSettings);
            options.IPv6RemoteDefaultGateway = HasFlag(value, NativeMethods.RASEO2.IPv6RemoteDefaultGateway);
            options.RegisterIPWithDns = HasFlag(value, NativeMethods.RASEO2.RegisterIPWithDns);
            options.UseDnsSuffixForRegistration = HasFlag(value, NativeMethods.RASEO2.UseDnsSuffixForRegistration);
            options.DisableIkeNameEkuCheck = HasFlag(value, NativeMethods.RASEO2.DisableIkeNameEkuCheck);

#endif
#if (WIN7 || WIN8)

            options.DisableClassBasedStaticRoute = HasFlag(value, NativeMethods.RASEO2.DisableClassBasedStaticRoute);
            options.DisableMobility = HasFlag(value, NativeMethods.RASEO2.DisableMobility);
            options.RequireMachineCertificates = HasFlag(value, NativeMethods.RASEO2.RequireMachineCertificates);

#endif
#if (WIN8)

            options.UsePreSharedKeyForIkeV2Initiator = Utilities.HasFlag(value, NativeMethods.RASEO2.UsePreSharedKeyForIkev2Initiator);
            options.UsePreSharedKeyForIkeV2Responder = Utilities.HasFlag(value, NativeMethods.RASEO2.UsePreSharedKeyForIkev2Responder);
            options.CacheCredentials = Utilities.HasFlag(value, NativeMethods.RASEO2.CacheCredentials);

#endif
        }

#endif

#if (WIN7 || WIN8)

        /// <summary>
        /// Sets the options on a <see cref="RasIkeV2Options"/> for the flags specified.
        /// </summary>
        /// <param name="options">The options to set.</param>
        /// <param name="value">The flags of the entry.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="options"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static void SetRasIkeV2Options(RasIkeV2Options options, NativeMethods.RASIKEV2 value)
        {
            if (options != null)
            {
                options.MobileIke = HasFlag(value, NativeMethods.RASIKEV2.MobileIke);
                options.ClientBehindNat = HasFlag(value, NativeMethods.RASIKEV2.ClientBehindNat);
                options.ServerBehindNat = HasFlag(value, NativeMethods.RASIKEV2.ServerBehindNat);
            }
        }

#endif

        /// <summary>
        /// Indicates whether the <paramref name="address"/> specified is a null reference or an <see cref="IPAddress.Any"/> address.
        /// </summary>
        /// <param name="address">The address to check.</param>
        /// <returns><b>true</b> if the address is a null reference or an <see cref="IPAddress.Any"/> address; otherwise <b>false</b>.</returns>
        public static bool IsIPAddressNullOrAnyAddress(IPAddress address)
        {
            return address == null || address.Equals(IPAddress.Any) || address.Equals(IPAddress.IPv6Any);
        }

        /// <summary>
        /// Determines whether the handle is invalid or closed.
        /// </summary>
        /// <param name="handle">A <see cref="DotRas.RasHandle"/> to check.</param>
        /// <returns><b>true</b> if the handle is invalid or closed, otherwise <b>false</b>.</returns>
        public static bool IsHandleInvalidOrClosed(RasHandle handle)
        {
            return handle == null || handle.IsInvalid || handle.IsClosed;
        }

        /// <summary>
        /// Creates a new array of <typeparamref name="T"/> objects contained at the pointer specified.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the pointer.</typeparam>
        /// <param name="ptr">An <see cref="System.IntPtr"/> containing data.</param>
        /// <param name="size">The size of each item at the pointer</param>
        /// <param name="count">The number of items at the pointer.</param>
        /// <returns>An new array of <typeparamref name="T"/> objects.</returns>
        public static T[] CreateArrayOfType<T>(IntPtr ptr, int size, int count)
        {
            T[] retval = new T[count];

            for (int pos = 0; pos < count; pos++)
            {
                IntPtr tempPtr = new IntPtr(ptr.ToInt64() + (pos * size));

                retval[pos] = (T)Marshal.PtrToStructure(tempPtr, typeof(T));
            }

            return retval;
        }

        /// <summary>
        /// Copies an existing array to a new pointer.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the array.</typeparam>
        /// <param name="array">The array of objects to copy.</param>
        /// <param name="size">Upon return contains the size of each object in the array.</param>
        /// <param name="totalSize">Upon return contains the total size of the buffer.</param>
        /// <returns>The pointer to the structures.</returns>
        public static IntPtr CopyObjectsToNewPtr<T>(T[] array, ref int size, out int totalSize)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException("array");
            }

            if (size == 0)
            {
                size = Marshal.SizeOf(typeof(T));
            }

            totalSize = array.Length * size;

            IntPtr ptr = Marshal.AllocHGlobal(totalSize);
            CopyObjectsToPtr<T>(array, ptr, ref size);

            return ptr;
        }

        /// <summary>
        /// Copies an existing array to a pointer.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the array.</typeparam>
        /// <param name="array">The array of objects to copy.</param>
        /// <param name="ptr">The <see cref="System.IntPtr"/> the array will be copied to.</param>
        /// <param name="size">Upon return contains the size of each object in the array.</param>
        public static void CopyObjectsToPtr<T>(T[] array, IntPtr ptr, ref int size)
        {
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException("array");
            }

            if (size == 0)
            {
                size = Marshal.SizeOf(typeof(T));
            }

            for (int pos = 0; pos < array.Length; pos++)
            {
                IntPtr tempPtr = new IntPtr(ptr.ToInt64() + (pos * size));

                Marshal.StructureToPtr(array[pos], tempPtr, true);
            }
        }

        /// <summary>
        /// Creates a new collection of strings contained at the pointer specified.
        /// </summary>
        /// <param name="ptr">The <see cref="System.IntPtr"/> where the data is located in memory.</param>
        /// <param name="offset">The offset from <paramref name="ptr"/> where the data is located.</param>
        /// <param name="count">The number of the string in memory.</param>
        /// <returns>A new collection of strings.</returns>
        public static Collection<string> CreateStringCollectionByCount(IntPtr ptr, int offset, int count)
        {
            Collection<string> retval = new Collection<string>();

            IntPtr pItem = new IntPtr(ptr.ToInt64() + offset);
            int index = 0;

            do
            {
                string item = Marshal.PtrToStringUni(pItem);
                if (string.IsNullOrEmpty(item))
                {
                    break;
                }
                else
                {
                    retval.Add(item);

                    pItem = new IntPtr(pItem.ToInt64() + (item.Length * 2) + 2);
                    index++;
                }
            }
            while (index < count);

            return retval;
        }

        /// <summary>
        /// Creates a new collection of strings contained at the pointer specified.
        /// </summary>
        /// <param name="ptr">The <see cref="System.IntPtr"/> where the data is located in memory.</param>
        /// <param name="offset">The offset from <paramref name="ptr"/> where the data is located.</param>
        /// <param name="length">The total length of the string in memory.</param>
        /// <returns>A new collection of strings.</returns>
        public static Collection<string> CreateStringCollectionByLength(IntPtr ptr, int offset, int length)
        {
            Collection<string> retval = new Collection<string>();

            IntPtr pItem = new IntPtr(ptr.ToInt64() + offset);
            int pos = 0;

            do
            {
                string item = Marshal.PtrToStringUni(pItem);
                if (string.IsNullOrEmpty(item))
                {
                    break;
                }
                else
                {
                    retval.Add(item);

                    int currentLength = (item.Length * 2) + 2;

                    pItem = new IntPtr(pItem.ToInt64() + currentLength);
                    pos += currentLength;
                }
            }
            while (pos < length);

            return retval;
        }

        /// <summary>
        /// Copies a string to the pointer at the offset specified.
        /// </summary>
        /// <param name="ptr">The pointer where the string should be copied.</param>
        /// <param name="offset">The offset from the pointer where the string will be copied.</param>
        /// <param name="value">The string to copy to the pointer.</param>
        /// <param name="length">The length of the string to copy.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="value"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static void CopyString(IntPtr ptr, int offset, string value, int length)
        {
            if (value == null)
            {
                ThrowHelper.ThrowArgumentNullException("value");
            }

            IntPtr pDestination = new IntPtr(ptr.ToInt64() + offset);

            IntPtr pSource = IntPtr.Zero;
            try
            {
                pSource = Marshal.StringToHGlobalUni(value);
                if (pSource != IntPtr.Zero)
                {
                    UnsafeNativeMethods.Instance.CopyMemoryImpl(pDestination, pSource, new IntPtr(length));
                }
            }
            finally
            {
                if (pSource != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pSource);
                }
            }
        }

        /// <summary>
        /// Indicates whether the memory address is null or empty.
        /// </summary>
        /// <param name="address">The memory address.</param>
        /// <param name="length">The length of data expected to be at the <paramref name="address"/> specified.</param>
        /// <returns><b>true</b> if the address is a null reference or empty, otherwise <b>false</b>.</returns>
        public static bool IsAddressNullOrEmpty(IntPtr address, int length)
        {
            return address == IntPtr.Zero || (address != IntPtr.Zero && length == 0);
        }

        /// <summary>
        /// Builds a string list from the collection of strings provided.
        /// </summary>
        /// <param name="collection">The collection of strings to use.</param>
        /// <param name="separatorChar">The character used to separate the strings in the collection.</param>
        /// <param name="length">Upon return, contains the length of the resulting string.</param>
        /// <returns>The concatenated collection of strings.</returns>
        public static string BuildStringList(Collection<string> collection, char separatorChar, out int length)
        {
            StringBuilder sb = new StringBuilder();

            if (collection != null && collection.Count > 0)
            {
                foreach (string value in collection)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        sb.Append(value).Append(separatorChar);
                    }
                }

                sb.Append(separatorChar);
            }

            length = sb.Length * 2;

            return sb.ToString();
        }

        /////// <summary>
        /////// Gets a <see cref="NativeMethods.RASIPADDR"/> for the <paramref name="value"/> specified.
        /////// </summary>
        /////// <param name="value">An <see cref="System.Net.IPAddress"/> to use.</param>
        /////// <returns>A new <see cref="NativeMethods.RASIPADDR"/> structure.</returns>
        /////// <exception cref="System.ArgumentException"><paramref name="value"/> is the wrong address family.</exception>
        ////public static NativeMethods.RASIPADDR GetRasIPAddress(IPAddress value)
        ////{
        ////    NativeMethods.RASIPADDR retval = new NativeMethods.RASIPADDR();

        ////    if (value != null)
        ////    {
        ////        if (value.AddressFamily != AddressFamily.InterNetwork)
        ////        {
        ////            ThrowHelper.ThrowArgumentException("value", Resources.Argument_IncorrectAddressFamily);
        ////        }

        ////        if (value == null)
        ////        {
        ////            retval.addr = IPAddress.Any.GetAddressBytes();
        ////        }
        ////        else
        ////        {
        ////            retval.addr = value.GetAddressBytes();
        ////        }
        ////    }

        ////    return retval;
        ////}

        /// <summary>
        /// Copies the objects within the collection to an array of the same type.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="collection">The collection containing the values to convert.</param>
        /// <returns>An array of <typeparamref name="T"/> objects.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="collection"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public static T[] ToArray<T>(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                ThrowHelper.ThrowArgumentNullException("collection");
            }

            List<T> result = new List<T>();

            foreach (T item in collection)
            {
                result.Add(item);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Updates the user credentials for the entry.
        /// </summary>
        /// <param name="phoneBookPath">The full path (including filename) of a phone book. If this parameter is a null reference (<b>Nothing</b> in Visual Basic), the default phone book is used.</param>
        /// <param name="entryName">The name of the entry whose credentials to set.</param>
        /// <param name="credentials">An <see cref="System.Net.NetworkCredential"/> object containing user credentials.</param>
        /// <param name="saveCredentialsToProfile">Indicates where the credentials should be saved.</param>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        public static bool UpdateCredentials(string phoneBookPath, string entryName, NetworkCredential credentials, RasUpdateCredential saveCredentialsToProfile)
        {
            if (saveCredentialsToProfile == RasUpdateCredential.None)
            {
                return false;
            }

            NativeMethods.RASCREDENTIALS value = new NativeMethods.RASCREDENTIALS();
            value.userName = credentials.UserName;
            value.password = credentials.Password;
            value.domain = credentials.Domain;
            value.options = NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain;

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            if (saveCredentialsToProfile == RasUpdateCredential.AllUsers)
            {
                value.options |= NativeMethods.RASCM.DefaultCredentials;
            }

#endif

            return RasHelper.Instance.SetCredentials(phoneBookPath, entryName, value, false);
        }

////#if (WIN2K8 || WIN7 || WIN8)
////        /// <summary>
////        /// Gets a <see cref="NativeMethods.RASIPV6ADDR"/> for the <paramref name="value"/> specified.
////        /// </summary>
////        /// <param name="value">An <see cref="System.Net.IPAddress"/> to use.</param>
////        /// <returns>A new <see cref="NativeMethods.RASIPADDR"/> structure.</returns>
////        /// <exception cref="System.ArgumentException"><paramref name="value"/> is the wrong address family.</exception>
////        public static NativeMethods.RASIPV6ADDR GetRasIPv6Address(IPAddress value)
////        {
////            NativeMethods.RASIPV6ADDR retval = new NativeMethods.RASIPV6ADDR();

////            if (value != null)
////            {
////                if (value.AddressFamily != AddressFamily.InterNetworkV6)
////                {
////                    ThrowHelper.ThrowArgumentException("value", Resources.Argument_IncorrectAddressFamily);
////                }

////                if (value == null)
////                {
////                    retval.addr = IPAddress.IPv6Any.GetAddressBytes();
////                }
////                else
////                {
////                    retval.addr = value.GetAddressBytes();
////                }
////            }

////            return retval;
////        }
////#endif

#if (WIN7 || WIN8)
        /// <summary>
        /// Creates a new collection of IPv4 addresses at the pointer specified.
        /// </summary>
        /// <param name="ptr">The <see cref="System.IntPtr"/> where the addresses are located.</param>
        /// <param name="count">The number of addresses at the pointer.</param>
        /// <returns>A new collection of <see cref="System.Net.IPAddress"/> objects.</returns>
        public static Collection<IPAddress> CreateIPv4AddressCollection(IntPtr ptr, int count)
        {
            Collection<IPAddress> retval = new Collection<IPAddress>();

            if (count > 0)
            {
                int size = Marshal.SizeOf(typeof(NativeMethods.RASIPADDR));

                for (int index = 0; index < count; index++)
                {
                    IntPtr current = new IntPtr(ptr.ToInt64() + (index * size));

                    NativeMethods.RASIPADDR address = PtrToStructure<NativeMethods.RASIPADDR>(current);
                    retval.Add(new IPAddress(address.addr));
                }
            }

            return retval;
        }

        /// <summary>
        /// Creates a new collection of IPv6 addresses at the pointer specified.
        /// </summary>
        /// <param name="ptr">The <see cref="System.IntPtr"/> where the addresses are located.</param>
        /// <param name="count">The number of addresses at the pointer.</param>
        /// <returns>A new collection of <see cref="System.Net.IPAddress"/> objects.</returns>
        public static Collection<IPAddress> CreateIPv6AddressCollection(IntPtr ptr, int count)
        {
            Collection<IPAddress> retval = new Collection<IPAddress>();

            if (count > 0)
            {
                int size = Marshal.SizeOf(typeof(NativeMethods.RASIPV6ADDR));

                for (int index = 0; index < count; index++)
                {
                    IntPtr current = new IntPtr(ptr.ToInt64() + (index * size));

                    NativeMethods.RASIPV6ADDR address = PtrToStructure<NativeMethods.RASIPV6ADDR>(current);
                    retval.Add(new IPAddress(address.addr));
                }
            }

            return retval;
        }

        /////// <summary>
        /////// Gets a <see cref="IPAddress"/> from the tunnel endpoint specified.
        /////// </summary>
        /////// <param name="tunnelEndPoint">The <see cref="NativeMethods.RASTUNNELENDPOINT"/> to convert.</param>
        /////// <returns>A new <see cref="System.Net.IPAddress"/> if available, otherwise a null reference (<b>Nothing</b> in Visual Basic).</returns>
        ////public static IPAddress GetIPAddressFromEndPoint(NativeMethods.RASTUNNELENDPOINT tunnelEndPoint)
        ////{
        ////    IPAddress retval = null;

        ////    if (tunnelEndPoint.type != NativeMethods.RASTUNNELENDPOINTTYPE.Unknown)
        ////    {
        ////        switch (tunnelEndPoint.type)
        ////        {
        ////            case NativeMethods.RASTUNNELENDPOINTTYPE.IPv4:
        ////                byte[] addr = new byte[4];
        ////                Array.Copy(tunnelEndPoint.addr, 0, addr, 0, 4);

        ////                retval = new IPAddress(addr);
        ////                break;

        ////            case NativeMethods.RASTUNNELENDPOINTTYPE.IPv6:
        ////                retval = new IPAddress(tunnelEndPoint.addr);
        ////                break;
        ////        }
        ////    }

        ////    return retval;
        ////}

        /////// <summary>
        /////// Gets a <see cref="NativeMethods.RASTUNNELENDPOINT"/> from the IP address specified.
        /////// </summary>
        /////// <param name="address">The <see cref="System.Net.IPAddress"/> to convert.</param>
        /////// <returns>A new <see cref="NativeMethods.RASTUNNELENDPOINT"/> instance.</returns>
        ////public static NativeMethods.RASTUNNELENDPOINT GetEndPointFromIPAddress(IPAddress address)
        ////{
        ////    NativeMethods.RASTUNNELENDPOINT retval = new NativeMethods.RASTUNNELENDPOINT();

        ////    if (address != null)
        ////    {
        ////        byte[] bytes = new byte[16];
        ////        byte[] actual = address.GetAddressBytes();

        ////        // Transfer the bytes to the 
        ////        Array.Copy(actual, bytes, actual.Length);

        ////        switch (address.AddressFamily)
        ////        {
        ////            case AddressFamily.InterNetwork:
        ////                retval.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4;
        ////                break;

        ////            case AddressFamily.InterNetworkV6:
        ////                retval.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv6;
        ////                break;

        ////            case AddressFamily.Unknown:
        ////                retval.type = NativeMethods.RASTUNNELENDPOINTTYPE.Unknown;
        ////                break;
        ////        }

        ////        retval.addr = bytes;
        ////    }

        ////    return retval;
        ////}
#endif

        /// <summary>
        /// Converts the byte array to a <see cref="System.Int64"/>.
        /// </summary>
        /// <param name="value">The array containing the value.</param>
        /// <param name="startIndex">The starting position within the array.</param>
        /// <returns>The <see cref="System.Int64"/> of the converted value.</returns>
        public static long ConvertBytesToInt64(byte[] value, int startIndex)
        {
            if (value == null || value.Length == 0)
            {
                return 0;
            }

            return BitConverter.ToInt64(value, startIndex);
        }

        /// <summary>
        /// Converts the byte array to a <see cref="System.Int16"/>.
        /// </summary>
        /// <param name="value">The array containing the value.</param>
        /// <param name="startIndex">The starting position within the array.</param>
        /// <returns>The <see cref="System.Int16"/> of the converted value.</returns>
        public static short ConvertBytesToInt16(byte[] value, int startIndex)
        {
            if (value == null || value.Length == 0)
            {
                return 0;
            }

            return BitConverter.ToInt16(value, startIndex);
        }
    }
}