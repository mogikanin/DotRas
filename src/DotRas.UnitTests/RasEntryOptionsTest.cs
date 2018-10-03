//--------------------------------------------------------------------------
// <copyright file="RasEntryOptionsTest.cs" company="Jeff Winn">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DotRas.UnitTests.Constants;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasEntryOptions"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasEntryOptionsTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasEntryOptionsTest"/> class.
        /// </summary>
        public RasEntryOptionsTest()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Tests the UseCountryAndAreaCodes property to ensure the same value that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UseCountryAndAreaCodesTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UseCountryAndAreaCodes = expected;

            var actual = target.UseCountryAndAreaCodes;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the IPHeaderCompression property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPHeaderCompressionTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.IPHeaderCompression = expected;

            var actual = target.IPHeaderCompression;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RemoteDefaultGateway property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RemoteDefaultGatewayTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RemoteDefaultGateway = expected;

            var actual = target.RemoteDefaultGateway;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DisableLcpExtensions property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DisableLcpExtensionsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DisableLcpExtensions = expected;

            var actual = target.DisableLcpExtensions;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the TerminalBeforeDial property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TerminalBeforeDialTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.TerminalBeforeDial = expected;

            var actual = target.TerminalBeforeDial;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the TerminalAfterDial property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TerminalAfterDialTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.TerminalAfterDial = expected;

            var actual = target.TerminalAfterDial;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the ModemLights property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ModemLightsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.ModemLights = expected;

            var actual = target.ModemLights;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the SoftwareCompression property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SoftwareCompressionTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SoftwareCompression = expected;

            var actual = target.SoftwareCompression;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireEncryptedPassword property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireEncryptedPasswordTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireEncryptedPassword = expected;

            var actual = target.RequireEncryptedPassword;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireMSEncryptedPassword property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireMSEncryptedPasswordTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireMSEncryptedPassword = expected;

            var actual = target.RequireMSEncryptedPassword;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireDataEncryption property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireDataEncryptionTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireDataEncryption = expected;

            var actual = target.RequireDataEncryption;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the NetworkLogOn property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetworkLogOnTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.NetworkLogOn = expected;

            var actual = target.NetworkLogOn;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UseLogOnCredentials property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UseLogOnCredentialsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UseLogOnCredentials = expected;

            var actual = target.UseLogOnCredentials;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the PromoteAlternates property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PromoteAlternatesTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.PromoteAlternates = expected;

            var actual = target.PromoteAlternates;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the SecureLocalFiles property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SecureLocalFilesTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SecureLocalFiles = expected;

            var actual = target.SecureLocalFiles;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireEap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireEapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireEap = expected;

            var actual = target.RequireEap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequirePap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequirePapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequirePap = expected;

            var actual = target.RequirePap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireSpap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireSpapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireSpap = expected;

            var actual = target.RequireSpap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the CustomEncryption property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomEncryptionTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.CustomEncryption = expected;

            var actual = target.CustomEncryption;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the PreviewPhoneNumber property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PreviewPhoneNumberTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.PreviewPhoneNumber = expected;

            var actual = target.PreviewPhoneNumber;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the SharedPhoneNumbers property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SharedPhoneNumbersTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SharedPhoneNumbers = expected;

            var actual = target.SharedPhoneNumbers;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the PreviewUserPassword property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PreviewUserPasswordTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.PreviewUserPassword = expected;

            var actual = target.PreviewUserPassword;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the PreviewDomain property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PreviewDomainTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.PreviewDomain = expected;

            var actual = target.PreviewDomain;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the ShowDialingProgress property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ShowDialingProgressTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.ShowDialingProgress = expected;

            var actual = target.ShowDialingProgress;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireChap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireChapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireChap = expected;

            var actual = target.RequireChap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireMSChap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireMSChapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireMSChap = expected;

            var actual = target.RequireMSChap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireMSChap2 property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireMSChap2Test()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireMSChap2 = expected;

            var actual = target.RequireMSChap2;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RequireWin95MSChap property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireWin95MSChapTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireWin95MSChap = expected;

            var actual = target.RequireWin95MSChap;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the CustomScript property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomScriptTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.CustomScript = expected;

            var actual = target.CustomScript;

            Assert.AreEqual<bool>(expected, actual);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the SecureFileAndPrint property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SecureFileAndPrintTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SecureFileAndPrint = expected;

            var actual = target.SecureFileAndPrint;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the SecureClientForMSNet property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SecureClientForMSNetTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SecureClientForMSNet = expected;

            var actual = target.SecureClientForMSNet;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DoNotNegotiateMultilink property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DoNotNegotiateMultilinkTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DoNotNegotiateMultilink = expected;

            var actual = target.DoNotNegotiateMultilink;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DoNotUseRasCredentials property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DoNotUseRasCredentialsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DoNotUseRasCredentials = expected;

            var actual = target.DoNotUseRasCredentials;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UsePreSharedKey property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UsePreSharedKeyTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UsePreSharedKey = expected;

            var actual = target.UsePreSharedKey;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the Internet property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void InternetTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.Internet = expected;

            var actual = target.Internet;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DisableNbtOverIP property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DisableNbtOverIPTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DisableNbtOverIP = expected;

            var actual = target.DisableNbtOverIP;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UseGlobalDeviceSettings property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UseGlobalDeviceSettingsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UseGlobalDeviceSettings = expected;

            var actual = target.UseGlobalDeviceSettings;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the ReconnectIfDropped property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ReconnectIfDroppedTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.ReconnectIfDropped = expected;

            var actual = target.ReconnectIfDropped;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the SharePhoneNumbers property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SharePhoneNumbersTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SharePhoneNumbers = expected;

            var actual = target.SharePhoneNumbers;

            Assert.AreEqual<bool>(expected, actual);
        }

#endif

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the SecureRoutingCompartment property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SecureRoutingCompartmentTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.SecureRoutingCompartment = expected;

            var actual = target.SecureRoutingCompartment;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UseTypicalSettings property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UseTypicalSettingsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UseTypicalSettings = expected;

            var actual = target.UseTypicalSettings;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the IPv6RemoteDefaultGateway property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPv6RemoteDefaultGatewayTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.IPv6RemoteDefaultGateway = expected;

            var actual = target.IPv6RemoteDefaultGateway;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the RegisterIPWithDns property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RegisterIPWithDnsTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RegisterIPWithDns = expected;

            var actual = target.RegisterIPWithDns;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UseDnsSuffixForRegistration property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UseDnsSuffixForRegistrationTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.UseDnsSuffixForRegistration = expected;

            var actual = target.UseDnsSuffixForRegistration;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DisableIkeNameEkuCheck property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DisableIkeNameEkuCheckTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DisableIkeNameEkuCheck = expected;

            var actual = target.DisableIkeNameEkuCheck;

            Assert.AreEqual<bool>(expected, actual);
        }

#endif

#if (WIN7 || WIN8)

        /// <summary>
        /// Tests the DisableClassBasedStaticRoute property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DisableClassBasedStaticRouteTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DisableClassBasedStaticRoute = expected;

            var actual = target.DisableClassBasedStaticRoute;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DisableMobility property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DisableMobilityTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.DisableMobility = expected;

            var actual = target.DisableMobility;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the DisableMobility property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RequireMachineCertificatesTest()
        {
            var expected = true;

            var target = new RasEntryOptions();
            target.RequireMachineCertificates = expected;

            var actual = target.RequireMachineCertificates;

            Assert.AreEqual<bool>(expected, actual);
        }

#endif

#if (WIN8)

        /// <summary>
        /// Tests the UsePreSharedKeyForIkev2Initiator property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UsePreSharedKeyForIkev2InitiatorTest()
        {
            bool expected = true;

            RasEntryOptions target = new RasEntryOptions();
            target.UsePreSharedKeyForIkeV2Initiator = expected;

            bool actual = target.UsePreSharedKeyForIkeV2Initiator;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the UsePreSharedKeyForIkev2Responder property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UsePreSharedKeyForIkev2ResponderTest()
        {
            bool expected = true;

            RasEntryOptions target = new RasEntryOptions();
            target.UsePreSharedKeyForIkeV2Responder = expected;

            bool actual = target.UsePreSharedKeyForIkeV2Responder;

            Assert.AreEqual<bool>(expected, actual);
        }

        /// <summary>
        /// Tests the CacheCredentials property to ensure the same that was set is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CacheCredentialsTest()
        {
            bool expected = true;

            RasEntryOptions target = new RasEntryOptions();
            target.CacheCredentials = expected;

            bool actual = target.CacheCredentials;

            Assert.AreEqual<bool>(expected, actual);
        }

#endif

        /// <summary>
        /// Tests the clone method to ensure the cloned class matches the original.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CloneTest()
        {
            var expected = new RasEntryOptions()
            {
                UseCountryAndAreaCodes = true,
                IPHeaderCompression = true,
                RemoteDefaultGateway = true,
                DisableLcpExtensions = true,
                TerminalBeforeDial = true,
                TerminalAfterDial = true,
                ModemLights = true,
                SoftwareCompression = true,
                RequireEncryptedPassword = true,
                RequireMSEncryptedPassword = true,
                RequireDataEncryption = true,
                NetworkLogOn = true,
                UseLogOnCredentials = true,
                PromoteAlternates = true,
                SecureLocalFiles = true,
                RequireEap = true,
                RequirePap = true,
                RequireSpap = true,
                CustomEncryption = true,
                PreviewPhoneNumber = true,
                SharedPhoneNumbers = true,
                PreviewUserPassword = true,
                PreviewDomain = true,
                ShowDialingProgress = true,
                RequireChap = true,
                RequireMSChap = true,
                RequireMSChap2 = true,
                RequireWin95MSChap = true,
                CustomScript = true,
#if (WINXP || WIN2K8 || WIN7 || WIN8)
                SecureFileAndPrint = true,
                SecureClientForMSNet = true,
                DoNotNegotiateMultilink = true,
                DoNotUseRasCredentials = true,
                UsePreSharedKey = true,
                Internet = true,
                DisableNbtOverIP = true,
                UseGlobalDeviceSettings = true,
                ReconnectIfDropped = true,
                SharePhoneNumbers = true,
#endif
#if (WIN2K8 || WIN7 || WIN8)
                SecureRoutingCompartment = true,
                UseTypicalSettings = true,
                IPv6RemoteDefaultGateway = true,
                RegisterIPWithDns = true,
                UseDnsSuffixForRegistration = true,
                DisableIkeNameEkuCheck = true,
#endif
#if (WIN7 || WIN8)
                DisableClassBasedStaticRoute = true,
                DisableMobility = true,
                RequireMachineCertificates = true,
#endif
#if (WIN8)
                UsePreSharedKeyForIkeV2Initiator = true,
                UsePreSharedKeyForIkeV2Responder = true,
                CacheCredentials = true
#endif
            };

            var actual = (RasEntryOptions)expected.Clone();

            Assert.IsNotNull(actual);
            Assert.AreNotSame(expected, actual);

            Assert.AreEqual<bool>(expected.UseCountryAndAreaCodes, actual.UseCountryAndAreaCodes);
            Assert.AreEqual<bool>(expected.IPHeaderCompression, actual.IPHeaderCompression);
            Assert.AreEqual<bool>(expected.RemoteDefaultGateway, actual.RemoteDefaultGateway);
            Assert.AreEqual<bool>(expected.DisableLcpExtensions, actual.DisableLcpExtensions);
            Assert.AreEqual<bool>(expected.TerminalBeforeDial, actual.TerminalBeforeDial);
            Assert.AreEqual<bool>(expected.TerminalAfterDial, actual.TerminalAfterDial);
            Assert.AreEqual<bool>(expected.ModemLights, actual.ModemLights);
            Assert.AreEqual<bool>(expected.SoftwareCompression, actual.SoftwareCompression);
            Assert.AreEqual<bool>(expected.RequireEncryptedPassword, actual.RequireEncryptedPassword);
            Assert.AreEqual<bool>(expected.RequireMSEncryptedPassword, actual.RequireMSEncryptedPassword);
            Assert.AreEqual<bool>(expected.RequireDataEncryption, actual.RequireDataEncryption);
            Assert.AreEqual<bool>(expected.NetworkLogOn, actual.NetworkLogOn);
            Assert.AreEqual<bool>(expected.UseLogOnCredentials, actual.UseLogOnCredentials);
            Assert.AreEqual<bool>(expected.PromoteAlternates, actual.PromoteAlternates);
            Assert.AreEqual<bool>(expected.SecureLocalFiles, actual.SecureLocalFiles);
            Assert.AreEqual<bool>(expected.RequireEap, actual.RequireEap);
            Assert.AreEqual<bool>(expected.RequirePap, actual.RequirePap);
            Assert.AreEqual<bool>(expected.RequireSpap, actual.RequireSpap);
            Assert.AreEqual<bool>(expected.CustomEncryption, actual.CustomEncryption);
            Assert.AreEqual<bool>(expected.PreviewPhoneNumber, actual.PreviewPhoneNumber);
            Assert.AreEqual<bool>(expected.SharedPhoneNumbers, actual.SharedPhoneNumbers);
            Assert.AreEqual<bool>(expected.PreviewUserPassword, actual.PreviewUserPassword);
            Assert.AreEqual<bool>(expected.PreviewDomain, actual.PreviewDomain);
            Assert.AreEqual<bool>(expected.ShowDialingProgress, actual.ShowDialingProgress);
            Assert.AreEqual<bool>(expected.RequireChap, actual.RequireChap);
            Assert.AreEqual<bool>(expected.RequireMSChap, actual.RequireMSChap);
            Assert.AreEqual<bool>(expected.RequireMSChap2, actual.RequireMSChap2);
            Assert.AreEqual<bool>(expected.RequireWin95MSChap, actual.RequireWin95MSChap);
            Assert.AreEqual<bool>(expected.CustomScript, actual.CustomScript);

#if (WINXP || WIN2K8 || WIN7 || WIN8)
            Assert.AreEqual<bool>(expected.SecureFileAndPrint, actual.SecureFileAndPrint);
            Assert.AreEqual<bool>(expected.SecureClientForMSNet, actual.SecureClientForMSNet);
            Assert.AreEqual<bool>(expected.DoNotNegotiateMultilink, actual.DoNotNegotiateMultilink);
            Assert.AreEqual<bool>(expected.DoNotUseRasCredentials, actual.DoNotUseRasCredentials);
            Assert.AreEqual<bool>(expected.UsePreSharedKey, actual.UsePreSharedKey);
            Assert.AreEqual<bool>(expected.Internet, actual.Internet);
            Assert.AreEqual<bool>(expected.DisableNbtOverIP, actual.DisableNbtOverIP);
            Assert.AreEqual<bool>(expected.UseGlobalDeviceSettings, actual.UseGlobalDeviceSettings);
            Assert.AreEqual<bool>(expected.ReconnectIfDropped, actual.ReconnectIfDropped);
            Assert.AreEqual<bool>(expected.SharePhoneNumbers, actual.SharePhoneNumbers);
#endif
#if (WIN2K8 || WIN7 || WIN8)
            Assert.AreEqual<bool>(expected.SecureRoutingCompartment, actual.SecureRoutingCompartment);
            Assert.AreEqual<bool>(expected.UseTypicalSettings, actual.UseTypicalSettings);
            Assert.AreEqual<bool>(expected.IPv6RemoteDefaultGateway, actual.IPv6RemoteDefaultGateway);
            Assert.AreEqual<bool>(expected.RegisterIPWithDns, actual.RegisterIPWithDns);
            Assert.AreEqual<bool>(expected.UseDnsSuffixForRegistration, actual.UseDnsSuffixForRegistration);
            Assert.AreEqual<bool>(expected.DisableIkeNameEkuCheck, actual.DisableIkeNameEkuCheck);
#endif
#if (WIN7 || WIN8)
            Assert.AreEqual<bool>(expected.DisableClassBasedStaticRoute, actual.DisableClassBasedStaticRoute);
            Assert.AreEqual<bool>(expected.DisableMobility, actual.DisableMobility);
            Assert.AreEqual<bool>(expected.RequireMachineCertificates, actual.RequireMachineCertificates);
#endif
#if (WIN8)
            Assert.AreEqual<bool>(expected.UsePreSharedKeyForIkeV2Initiator, actual.UsePreSharedKeyForIkeV2Initiator);
            Assert.AreEqual<bool>(expected.UsePreSharedKeyForIkeV2Responder, actual.UsePreSharedKeyForIkeV2Responder);
            Assert.AreEqual<bool>(expected.CacheCredentials, actual.CacheCredentials);
#endif
        }

        #endregion
    }
}