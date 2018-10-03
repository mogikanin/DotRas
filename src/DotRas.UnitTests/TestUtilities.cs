//--------------------------------------------------------------------------
// <copyright file="TestUtilities.cs" company="Jeff Winn">
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

namespace DotRas.UnitTests
{
    using System.Net;
    using DotRas.Design;
    using DotRas.Internal;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Contains utility methods for the test assembly.
    /// </summary>
    internal static class TestUtilities
    {
#if (WIN7 || WIN8)
        /// <summary>
        /// Asserts an endpoint matches an IP address.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertEndPoint(NativeMethods.RASTUNNELENDPOINT expected, IPAddress actual)
        {
            if (expected.type == NativeMethods.RASTUNNELENDPOINTTYPE.Unknown && actual == null)
            {
                return;
            }

            int length = expected.type == NativeMethods.RASTUNNELENDPOINTTYPE.IPv4 ? 4 : 16;
            byte[] bytes = actual.GetAddressBytes();

            for (int index = 0; index < length; index++)
            {
                Assert.AreEqual<byte>(expected.addr[index], bytes[index]);
            }
        }
#endif

        /// <summary>
        /// Asserts a <see cref="DotRas.RasEntry"/> object.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertEntry(RasEntry expected, RasEntry actual)
        {
            CollectionAssert.AreEqual(expected.AlternatePhoneNumbers, actual.AlternatePhoneNumbers);

            Assert.AreEqual(expected.AreaCode, actual.AreaCode);

#pragma warning disable 0618
            Assert.AreEqual(expected.AutoDialDll, actual.AutoDialDll);
            Assert.AreEqual(expected.AutoDialFunc, actual.AutoDialFunc);
#pragma warning restore 0618

            Assert.AreEqual(expected.Channels, actual.Channels);
            Assert.AreEqual(expected.CountryCode, actual.CountryCode);
            Assert.AreEqual(expected.CountryId, actual.CountryId);
            Assert.AreEqual(expected.CustomAuthKey, actual.CustomAuthKey);
            Assert.AreEqual(expected.CustomDialDll, actual.CustomDialDll);

            AssertDevice(expected.Device, actual.Device);

            Assert.AreEqual(expected.DialExtraPercent, actual.DialExtraPercent);
            Assert.AreEqual(expected.DialExtraSampleSeconds, actual.DialExtraSampleSeconds);
            Assert.AreEqual(expected.DialMode, actual.DialMode);
            Assert.AreEqual(expected.DnsAddress, actual.DnsAddress);
            Assert.AreEqual(expected.DnsAddressAlt, actual.DnsAddressAlt);
            Assert.AreEqual(expected.EncryptionType, actual.EncryptionType);
            Assert.AreEqual(expected.EntryType, actual.EntryType);
            Assert.AreEqual(expected.FrameSize, actual.FrameSize);
            Assert.AreEqual(expected.FramingProtocol, actual.FramingProtocol);
            Assert.AreEqual(expected.HangUpExtraPercent, actual.HangUpExtraPercent);
            Assert.AreEqual(expected.HangUpExtraSampleSeconds, actual.HangUpExtraSampleSeconds);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.IdleDisconnectSeconds, actual.IdleDisconnectSeconds);
            Assert.AreEqual(expected.IPAddress, actual.IPAddress);
            Assert.AreEqual(expected.Name, actual.Name);

            AssertRasNetworkProtocols(expected.NetworkProtocols, actual.NetworkProtocols);
            AssertRasEntryOptions(expected.Options, actual.Options);

            if ((expected.Owner != null && actual.Owner == null) || (expected.Owner == null && actual.Owner != null))
            {
                Assert.Fail("The entry owner did not match.");
            }

            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Script, actual.Script);

            AssertRasCollection<RasSubEntry>(expected.SubEntries, actual.SubEntries);

            Assert.AreEqual(expected.VpnStrategy, actual.VpnStrategy);
            Assert.AreEqual(expected.WinsAddress, actual.WinsAddress);
            Assert.AreEqual(expected.WinsAddressAlt, actual.WinsAddressAlt);
            Assert.AreEqual(expected.X25Address, actual.X25Address);
            Assert.AreEqual(expected.X25Facilities, actual.X25Facilities);
            Assert.AreEqual(expected.X25PadType, actual.X25PadType);
            Assert.AreEqual(expected.X25UserData, actual.X25UserData);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            Assert.AreEqual(expected.DnsSuffix, actual.DnsSuffix);
            Assert.AreEqual(expected.TcpWindowSize, actual.TcpWindowSize);
            Assert.AreEqual(expected.PrerequisitePhoneBook, actual.PrerequisitePhoneBook);
            Assert.AreEqual(expected.PrerequisiteEntryName, actual.PrerequisiteEntryName);
            Assert.AreEqual(expected.RedialCount, actual.RedialCount);
            Assert.AreEqual(expected.RedialPause, actual.RedialPause);

#endif
#if (WIN2K8 || WIN7 || WIN8)

            Assert.AreEqual(expected.IPv6DnsAddress, actual.IPv6DnsAddress);
            Assert.AreEqual(expected.IPv6DnsAddressAlt, actual.IPv6DnsAddressAlt);
            Assert.AreEqual(expected.IPv4InterfaceMetric, actual.IPv4InterfaceMetric);
            Assert.AreEqual(expected.IPv6InterfaceMetric, actual.IPv6InterfaceMetric);

#endif
        }

        /// <summary>
        /// Asserts a <see cref="DotRas.RasNetworkProtocols"/> object.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertRasNetworkProtocols(RasNetworkProtocols expected, RasNetworkProtocols actual)
        {
            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(expected.IP, actual.IP);

#if (WIN2K8 || WIN7 || WIN8)
                Assert.AreEqual(expected.IPv6, actual.IPv6);
#endif
                Assert.AreEqual(expected.Ipx, actual.Ipx);

#pragma warning disable 0618                
                Assert.AreEqual(expected.NetBeui, actual.NetBeui);
#pragma warning restore 0618

#if (WIN2K8 || WIN7 || WIN8)

                Assert.AreEqual(expected.IPv6, actual.IPv6);

#endif
            }
        }

        /// <summary>
        /// Asserts a <see cref="DotRas.RasEntryOptions"/> object.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertRasEntryOptions(RasEntryOptions expected, RasEntryOptions actual)
        {
            if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                Assert.Fail();
            }
            else
            {
                Assert.AreEqual(expected.CustomEncryption, actual.CustomEncryption);
                Assert.AreEqual(expected.CustomScript, actual.CustomScript);
                Assert.AreEqual(expected.DisableLcpExtensions, actual.DisableLcpExtensions);
                Assert.AreEqual(expected.IPHeaderCompression, actual.IPHeaderCompression);
                Assert.AreEqual(expected.ModemLights, actual.ModemLights);
                Assert.AreEqual(expected.NetworkLogOn, actual.NetworkLogOn);
                Assert.AreEqual(expected.PreviewDomain, actual.PreviewDomain);
                Assert.AreEqual(expected.PreviewPhoneNumber, actual.PreviewPhoneNumber);
                Assert.AreEqual(expected.PreviewUserPassword, actual.PreviewUserPassword);
                Assert.AreEqual(expected.PromoteAlternates, actual.PromoteAlternates);
                Assert.AreEqual(expected.RemoteDefaultGateway, actual.RemoteDefaultGateway);
                Assert.AreEqual(expected.RequireChap, actual.RequireChap);
                Assert.AreEqual(expected.RequireDataEncryption, actual.RequireDataEncryption);
                Assert.AreEqual(expected.RequireEap, actual.RequireEap);
                Assert.AreEqual(expected.RequireEncryptedPassword, actual.RequireEncryptedPassword);
                Assert.AreEqual(expected.RequireMSChap, actual.RequireMSChap);
                Assert.AreEqual(expected.RequireMSChap2, actual.RequireMSChap2);
                Assert.AreEqual(expected.RequireMSEncryptedPassword, actual.RequireMSEncryptedPassword);
                Assert.AreEqual(expected.RequirePap, actual.RequirePap);
                Assert.AreEqual(expected.RequireSpap, actual.RequireSpap);
                Assert.AreEqual(expected.RequireWin95MSChap, actual.RequireWin95MSChap);
                Assert.AreEqual(expected.SecureLocalFiles, actual.SecureLocalFiles);
                Assert.AreEqual(expected.SharedPhoneNumbers, actual.SharedPhoneNumbers);
                Assert.AreEqual(expected.ShowDialingProgress, actual.ShowDialingProgress);
                Assert.AreEqual(expected.SoftwareCompression, actual.SoftwareCompression);
                Assert.AreEqual(expected.TerminalAfterDial, actual.TerminalAfterDial);
                Assert.AreEqual(expected.TerminalBeforeDial, actual.TerminalBeforeDial);
                Assert.AreEqual(expected.UseLogOnCredentials, actual.UseLogOnCredentials);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

                Assert.AreEqual(expected.SecureFileAndPrint, actual.SecureFileAndPrint);
                Assert.AreEqual(expected.SecureClientForMSNet, actual.SecureClientForMSNet);
                Assert.AreEqual(expected.DoNotNegotiateMultilink, actual.DoNotNegotiateMultilink);
                Assert.AreEqual(expected.DoNotUseRasCredentials, actual.DoNotUseRasCredentials);
                Assert.AreEqual(expected.UsePreSharedKey, actual.UsePreSharedKey);
                Assert.AreEqual(expected.Internet, actual.Internet);
                Assert.AreEqual(expected.DisableNbtOverIP, actual.DisableNbtOverIP);
                Assert.AreEqual(expected.UseGlobalDeviceSettings, actual.UseGlobalDeviceSettings);
                Assert.AreEqual(expected.ReconnectIfDropped, actual.ReconnectIfDropped);
                Assert.AreEqual(expected.SharePhoneNumbers, actual.SharePhoneNumbers);

#endif
#if (WIN2K8 || WIN7 || WIN8)

                Assert.AreEqual(expected.SecureRoutingCompartment, actual.SecureRoutingCompartment);
                Assert.AreEqual(expected.UseTypicalSettings, actual.UseTypicalSettings);
                Assert.AreEqual(expected.IPv6RemoteDefaultGateway, actual.IPv6RemoteDefaultGateway);
                Assert.AreEqual(expected.RegisterIPWithDns, actual.RegisterIPWithDns);
                Assert.AreEqual(expected.UseDnsSuffixForRegistration, actual.UseDnsSuffixForRegistration);
                Assert.AreEqual(expected.DisableIkeNameEkuCheck, actual.DisableIkeNameEkuCheck);

#endif
#if (WIN7 || WIN8)

                Assert.AreEqual(expected.DisableClassBasedStaticRoute, actual.DisableClassBasedStaticRoute);
                Assert.AreEqual(expected.DisableMobility, actual.DisableMobility);
                Assert.AreEqual(expected.RequireMachineCertificates, actual.RequireMachineCertificates);

#endif
            }
        }

        /// <summary>
        /// Asserts a <see cref="DotRas.Design.RasCollection&lt;TObject&gt;"/> object.
        /// </summary>
        /// <typeparam name="TObject">The type of object contained in the collection.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertRasCollection<TObject>(RasCollection<TObject> expected, RasCollection<TObject> actual)
            where TObject : class
        {
            if ((expected != null && actual == null) || (expected == null && actual != null) || (expected != null && actual != null && expected.Count != actual.Count))
            {
                Assert.Fail("The collections do not match.");
            }
            else if (expected != null && actual != null)
            {
                for (int index = 0; index < expected.Count; index++)
                {
                    Assert.AreEqual<TObject>(expected[index], actual[index]);
                }
            }
        }

        /// <summary>
        /// Asserts a <see cref="DotRas.RasDevice"/> object.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        public static void AssertDevice(RasDevice expected, RasDevice actual)
        {
            if ((expected != null && actual == null) || (expected == null && actual != null))
            {
                Assert.Fail("The devices did not match.");
            }
            else if (expected != null && actual != null)
            {
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.DeviceType, actual.DeviceType);
            }
        }
    }
}