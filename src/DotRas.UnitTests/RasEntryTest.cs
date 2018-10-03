//--------------------------------------------------------------------------
// <copyright file="RasEntryTest.cs" company="Jeff Winn">
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
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Net;
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasEntry"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasEntryTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasEntryTest"/> class.
        /// </summary>
        public RasEntryTest()
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

        #region Constructor Tests

        /// <summary>
        /// Tests the RasEntry constructor to ensure an object is created successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RasEntryConstructorTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);

            Assert.AreEqual(name, target.Name);
        }

        /// <summary>
        /// Tests the RasEntry constructor to ensure an argument exception is thrown when an empty entry name is passed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RasEntryConstructorArgumentExceptionTest()
        {
            string name = string.Empty;

            RasEntry target = new RasEntry(name);
        }

        /// <summary>
        /// Tests the RasEntry constructor to ensure an ArgumentException is thrown when the entry name is empty.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RasEntryNullEntryNameConstructorTest()
        {
            string name = null;

            RasEntry target = new RasEntry(name);
        }

        #endregion

        #region Property Tests

        /// <summary>
        /// Tests the X25UserData property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25UserDataTest()
        {
            string name = "Test Entry";
            string expected = "12345";

            RasEntry target = new RasEntry(name);
            target.X25UserData = expected;

            string actual = target.X25UserData;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25PadType property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25PadTypeTest()
        {
            string name = "Test Entry";
            string expected = "12345";

            RasEntry target = new RasEntry(name);
            target.X25PadType = expected;

            string actual = target.X25PadType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25Facilities property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25FacilitiesTest()
        {
            string name = "Test Entry";
            string expected = "12345";

            RasEntry target = new RasEntry(name);
            target.X25Facilities = expected;

            string actual = target.X25Facilities;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25Address property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25AddressTest()
        {
            string name = "Test Entry";
            string expected = "12345";

            RasEntry target = new RasEntry(name);
            target.X25Address = expected;

            string actual = target.X25Address;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the options property to ensure an instance is always returned even when the object is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsAlwaysDefaultTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);

            RasEntryOptions actual = target.Options;

            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Tests the options property when using a default instance matches that of the default constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsDefaultTest()
        {
            string name = "Test Entry";

            RasEntryOptions expected = new RasEntryOptions();

            RasEntry target = new RasEntry(name);
            RasEntryOptions actual = target.Options;

            TestUtilities.AssertRasEntryOptions(expected, actual);
        }

        /// <summary>
        /// Tests the WinsAddressAlt property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void WinsAddressAltTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.Loopback;

            RasEntry target = new RasEntry(name);
            target.WinsAddressAlt = expected;

            IPAddress actual = target.WinsAddressAlt;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the WinsAddress property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void WinsAddressTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.Loopback;

            RasEntry target = new RasEntry(name);
            target.WinsAddress = expected;

            IPAddress actual = target.WinsAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the VpnStrategy property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void VpnStrategyTest()
        {
            string name = "Test Entry";
            RasVpnStrategy expected = RasVpnStrategy.L2tpFirst;

            RasEntry target = new RasEntry(name);
            target.VpnStrategy = expected;

            RasVpnStrategy actual = target.VpnStrategy;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SubEntries property to ensure the property returns an empty collection.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SubEntriesTest()
        {
            string name = "Test Entry";
            int expected = 0;

            RasEntry target = new RasEntry(name);
            int actual = target.SubEntries.Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Script property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ScriptTest()
        {
            string name = "Test Entry";
            string expected = string.Empty;

            RasEntry target = new RasEntry(name);
            target.Script = expected;

            string actual = target.Script;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneNumber property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneNumberTest()
        {
            string name = "Test Entry";
            string expected = "127.0.0.1";

            RasEntry target = new RasEntry(name);
            target.PhoneNumber = expected;

            string actual = expected;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Owner property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OwnerTest()
        {
            string name = "Test Entry";
            RasPhoneBook expected = new RasPhoneBook();

            RasEntry target = new RasEntry(name);
            target.Owner = expected;

            RasPhoneBook actual = target.Owner;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the NetworkProtocols property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetworkProtocolsTest()
        {
            string name = "Test Entry";
            RasNetworkProtocols expected = new RasNetworkProtocols()
            {
                IP = true
            };

            RasEntry target = new RasEntry(name);
            target.NetworkProtocols = expected;

            RasNetworkProtocols actual = target.NetworkProtocols;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the Name property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NameTest()
        {
            string expected = "Test Entry";

            RasEntry target = new RasEntry(expected);

            string actual = target.Name;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPAddress property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.Loopback;

            RasEntry target = new RasEntry(name);
            target.IPAddress = expected;

            IPAddress actual = target.IPAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IdleDisconnectSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IdleDisconnectSecondsTest()
        {
            string name = "Test Entry";
            int expected = RasIdleDisconnectTimeout.Disabled;

            RasEntry target = new RasEntry(name);
            target.IdleDisconnectSeconds = expected;

            int actual = target.IdleDisconnectSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Id property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IdTest()
        {
            string name = "Test Entry";
            Guid expected = Guid.NewGuid();

            RasEntry target = new RasEntry(name);
            target.Id = expected;

            Guid actual = target.Id;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the HangUpExtraSampleSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpExtraSampleSecondsTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.HangUpExtraSampleSeconds = expected;

            int actual = target.HangUpExtraSampleSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the HangUpExtraPercent property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpExtraPercentTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.HangUpExtraPercent = expected;

            int actual = target.HangUpExtraPercent;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the FramingProtocol property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void FramingProtocolTest()
        {
            string name = "Test Entry";
            RasFramingProtocol expected = RasFramingProtocol.Ppp;

            RasEntry target = new RasEntry(name);
            target.FramingProtocol = expected;

            RasFramingProtocol actual = target.FramingProtocol;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the FramingProtocol property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void FrameSizeTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.FrameSize = expected;

            int actual = target.FrameSize;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EntryType property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryTypeTest()
        {
            string name = "Test Entry";
            RasEntryType expected = RasEntryType.Vpn;

            RasEntry target = new RasEntry(name);
            target.EntryType = expected;

            RasEntryType actual = target.EntryType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EncryptionType property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EncryptionTypeTest()
        {
            string name = "Test Entry";
            RasEncryptionType expected = RasEncryptionType.Require;

            RasEntry target = new RasEntry(name);
            target.EncryptionType = expected;

            RasEncryptionType actual = target.EncryptionType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DnsAddressAlt property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DnsAddressAltTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.Loopback;

            RasEntry target = new RasEntry(name);
            target.DnsAddressAlt = expected;

            IPAddress actual = target.DnsAddressAlt;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DnsAddress property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DnsAddressTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.Loopback;

            RasEntry target = new RasEntry(name);
            target.DnsAddress = expected;

            IPAddress actual = target.DnsAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialMode property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialModeTest()
        {
            string name = "Test Entry";
            RasDialMode expected = RasDialMode.DialAsNeeded;

            RasEntry target = new RasEntry(name);
            target.DialMode = expected;

            RasDialMode actual = target.DialMode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialExtraSampleSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialExtraSampleSecondsTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.DialExtraSampleSeconds = expected;

            int actual = target.DialExtraSampleSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialExtraPercent property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialExtraPercentTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.DialExtraPercent = expected;

            int actual = target.DialExtraPercent;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Device property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DeviceTest()
        {
            string name = "Test Entry";
            RasDevice expected = RasDevice.Create(name, RasDeviceType.Vpn);

            RasEntry target = new RasEntry(name);
            target.Device = expected;

            RasDevice actual = target.Device;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CustomDialDll property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomDialDllTest()
        {
            string name = "Test Entry";
            string expected = "Test.dll";

            RasEntry target = new RasEntry(name);
            target.CustomDialDll = expected;

            string actual = target.CustomDialDll;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CustomAuthKey property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomAuthKeyTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.CustomAuthKey = expected;

            int actual = target.CustomAuthKey;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CountryId property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CountryIdTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.CountryId = expected;

            int actual = target.CountryId;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CountryCode property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CountryCodeTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.CountryCode = expected;

            int actual = target.CountryCode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Channels property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ChannelsTest()
        {
            string name = "Test Entry";
            int expected = int.MaxValue;

            RasEntry target = new RasEntry(name);
            target.Channels = expected;

            int actual = target.Channels;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AutoDialFunc property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AutoDialFuncTest()
        {
            string name = "Test Entry";
            string expected = "TestFunc";

#pragma warning disable 0618
            RasEntry target = new RasEntry(name);
            target.AutoDialFunc = expected;

            string actual = target.AutoDialFunc;
#pragma warning restore 0618

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AutoDialDll property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AutoDialDllTest()
        {
            string name = "Test Entry";
            string expected = "Test.dll";

#pragma warning disable 0618
            RasEntry target = new RasEntry(name);
            target.AutoDialDll = expected;

            string actual = target.AutoDialDll;
#pragma warning restore 0618

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AreaCode property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AreaCodeTest()
        {
            string name = "Test Entry";
            string expected = "123";

            RasEntry target = new RasEntry(name);
            target.AreaCode = expected;

            string actual = target.AreaCode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AlternatePhoneNumbers property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AlternatePhoneNumbersTest()
        {
            string name = "Test Entry";
            Collection<string> expected = new Collection<string>();
            expected.Add("555-555-1234");
            expected.Add("555-555-2345");

            RasEntry target = new RasEntry(name);
            target.AlternatePhoneNumbers = expected;

            Collection<string> actual = target.AlternatePhoneNumbers;

            CollectionAssert.AreEqual(expected, actual);
        }

#if (WIN7 || WIN8)

        /// <summary>
        /// Tests the IPv6Address property to ensure the value returned matches the value passed into the property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPv6AddressTest()
        {
            string name = "Test Entry";
            IPAddress expected = IPAddress.IPv6Loopback;

            RasEntry target = new RasEntry(name);
            target.IPv6Address = expected;

            IPAddress actual = target.IPv6Address;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPv6PrefixLength property to ensure the value returned matches the value passed into the property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPv6PrefixLengthTest()
        {
            string name = "Test Entry";
            int expected = 10;

            RasEntry target = new RasEntry(name);
            target.IPv6PrefixLength = expected;

            int actual = target.IPv6PrefixLength;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the NetworkOutageTime property to ensure the value returned matches the value passed into the property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetworkOutageTimeTest()
        {
            string name = "Test Entry";
            int expected = 1000;

            RasEntry target = new RasEntry(name);
            target.NetworkOutageTime = expected;

            int actual = target.NetworkOutageTime;

            Assert.AreEqual(expected, actual);
        }

#endif

        #endregion

        #region Method Tests

        /// <summary>
        /// Tests the Exists method to ensure the entry exists.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryExistsTest()
        {
            Mock<ISafeNativeMethods> mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ValidateEntryName(It.IsAny<string>(), It.IsAny<string>())).Returns(NativeMethods.ERROR_ALREADY_EXISTS);

            string entryName = "Test Entry";
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            bool expected = true;
            bool actual = RasEntry.Exists(entryName, phoneBookPath);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Exists method to ensure an exception is thrown when the entry name is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void EntryExistsNullEntryNameTest()
        {
            string entryName = null;
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            RasEntry.Exists(entryName, phoneBookPath);
        }

        /// <summary>
        /// Tests the Exists method to ensure an exception is thrown when the phone book path is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void EntryExistsEmptyEntryNameTest()
        {
            string entryName = string.Empty;
            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            RasEntry.Exists(entryName, phoneBookPath);
        }

        /// <summary>
        /// Tests the Exists method to ensure an exception is thrown when the phone book path is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void EntryExistsNullPhoneBookPathTest()
        {
            string entryName = "Test Entry";
            string phoneBookPath = null;

            RasEntry.Exists(entryName, phoneBookPath);
        }

        /// <summary>
        /// Tests the Exists method to ensure an exception is thrown when the phone book path is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void EntryExistsEmptyPhoneBookPathTest()
        {
            string entryName = "Test Entry";
            string phoneBookPath = string.Empty;

            RasEntry.Exists(entryName, phoneBookPath);
        }

        /// <summary>
        /// Tests the Exists method to ensure an exception is thrown when the phone book could not be found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(FileNotFoundException))]
        public void EntryExistsInvalidFileNameTest()
        {
            string entryName = "Test Entry";
            string phoneBookPath = string.Format("C:\\{0}\\{1}.pbk", Guid.NewGuid(), Guid.NewGuid());

            RasEntry.Exists(entryName, phoneBookPath);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure an InvalidOperationException is thrown when the entry does not belong to a phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateCredentialsInvalidOperationExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.UpdateCredentials(new NetworkCredential("Test", "User"));
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials are updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentialsTest()
        {
            string name = "Test Entry";
            NetworkCredential credentials = new NetworkCredential("Test", "User");

            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry(name);
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            bool result = target.UpdateCredentials(credentials);

            Assert.IsTrue(result);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the client pre-shared key is updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials2ClientPreSharedKeyTest()
        {
            string name = "Test Entry";

            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry(name);
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            bool result = target.UpdateCredentials(RasPreSharedKey.Client, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the DDM pre-shared key is updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials2DdmPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            bool result = target.UpdateCredentials(RasPreSharedKey.Ddm, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the client pre-shared key is updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials2ServerPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            bool result = target.UpdateCredentials(RasPreSharedKey.Server, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials for all users are updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials1Test()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            bool result = target.UpdateCredentials(new NetworkCredential("Test", "User"), true);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure an exception is thrown when the owner is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateCredentials1NullOwnerTest()
        {
            string name = "Test Entry";
            NetworkCredential credentials = new NetworkCredential("Test", "User");

            RasEntry target = new RasEntry(name);
            target.UpdateCredentials(credentials, true);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure an exception is thrown when the credentials are a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCredentials1NullCredentialsTest()
        {
            string name = "Test Entry";
            NetworkCredential credentials = null;

            RasEntry target = new RasEntry(name);
            target.Owner = new RasPhoneBook();

            target.UpdateCredentials(credentials, true);
        }

        /// <summary>
        /// Tests the InternalSetCredentials method to ensure it will properly update user credentials.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void InternalSetCredentialsTest()
        {
            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            bool result = target.UpdateCredentials(new NetworkCredential("Test", "User"), false);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the InternalSetCredentials method to ensure it will properly update credentials for all users.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void InternalSetCredentialsForAllUsersTest()
        {
            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            bool result = target.UpdateCredentials(new NetworkCredential("Test", "User"), true);

            Assert.IsTrue(result);
        }

#endif

        /// <summary>
        /// Tests the UpdateCredentials method to ensure it throws a null reference exception when null credentials are passed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateCredentialsWithNullCredentialsArgumentNullExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.Owner = new RasPhoneBook();

            target.UpdateCredentials(null);
        }

        /// <summary>
        /// Tests the Update method to ensure it properly updates the entry.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;
            
            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetEntryProperties(pbk, target)).Returns(true);

            bool result = target.Update();

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the update method to ensure an exception is thrown when the entry does not belong to a phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateInvalidOperationExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.Update();
        }

        /// <summary>
        /// Tests the Rename method to ensure an exception is thrown when the new entry name is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameNullEntryNameExceptionTest()
        {
            string name = "Test Entry";
            string newEntryName = null;

            RasEntry target = new RasEntry(name);
            target.Rename(newEntryName);
        }

        /// <summary>
        /// Tests the Rename method to ensure an exception is thrown when the entry name is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameEmptyEntryNameExceptionTest()
        {
            string name = "Test Entry";
            string newEntryName = string.Empty;

            RasEntry target = new RasEntry(name);
            target.Rename(newEntryName);
        }

        /// <summary>
        /// Tests the Rename method to ensure an entry not in a phone book can be renamed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RenameEntryNotInPhoneBookTest()
        {
            string name = "Test Entry";
            string newEntryName = "New Entry";
            bool expected = true;

            RasEntry target = new RasEntry(name);
            bool actual = target.Rename(newEntryName);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(newEntryName, target.Name);
        }

        /// <summary>
        /// Tests the Rename method to ensure it will rename an entry and update the name property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RenameTest()
        {
            ////string name = "Blah";

            ////RasEntry target = new RasEntry("Test");
            ////target.Owner = new RasPhoneBook();

            ////Mock<IRasHelper> mock = new Mock<IRasHelper>();
            ////RasHelper.Instance = mock.Object;

            ////mock.Setup(o => o.IsValidEntryName(It.IsAny<RasPhoneBook>(), It.IsAny<string>())).Returns(true);
            ////mock.Setup(o => o.RenameEntry(It.IsAny<RasPhoneBook>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            ////bool expected = true;
            ////bool actual = target.Rename(name);

            ////Assert.AreEqual(expected, actual);
            ////Assert.AreEqual(name, target.Name);
        }

        /// <summary>
        /// Tests the Rename method with an invalid entry name.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void RenameInvalidEntryNameTest()
        {
            string name = "Test Entry";
            string newEntryName = ".\\Test*!";
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry(name);
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.IsValidEntryName(pbk, newEntryName)).Returns(false);

            target.Rename(newEntryName);
        }

        /// <summary>
        /// Tests the Remove method to ensure it removes an entry.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RemoveTest()
        {
            ////string name = "Test Entry";
            ////RasPhoneBook owner = new RasPhoneBook();
            ////bool expected = true;

            ////RasEntry target = new RasEntry(name);
            ////target.Owner = owner;

            ////Isolate.WhenCalled(() => target.Owner.Entries.Remove(null)).WillReturn(true);

            ////bool actual = target.Remove();

            ////Assert.AreEqual(expected, actual);

            ////Isolate.Verify.WasCalledWithExactArguments(() => target.Owner.Entries.Remove(target));
        }

        /// <summary>
        /// Tests the GetCredentials method to ensure the credentials are returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentialsTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            NetworkCredential expected = new NetworkCredential("Test", "User");

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            NetworkCredential actual = target.GetCredentials();

            Assert.AreEqual(expected, actual);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the TryGetCredentials method to ensure an invalid operation exception is thrown when the phone book is not set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetCredentials1InvalidOperationExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            NetworkCredential actual = target.GetCredentials(RasPreSharedKey.Client);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1ClientPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            NetworkCredential expected = new NetworkCredential(string.Empty, "********", string.Empty);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            NetworkCredential actual = target.GetCredentials(RasPreSharedKey.Client);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1DdmPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            NetworkCredential expected = new NetworkCredential(string.Empty, "********", string.Empty);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            NetworkCredential actual = target.GetCredentials(RasPreSharedKey.Ddm);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1ServerPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            NetworkCredential expected = new NetworkCredential(string.Empty, "********", string.Empty);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            NetworkCredential actual = target.GetCredentials(RasPreSharedKey.Server);

            Assert.AreEqual(expected, actual);
        }

#endif

        /// <summary>
        /// Tests the GetCredentials method to ensure an InvalidOperationException is thrown when the entry is not in a phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetCredentialsInvalidOperationExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.GetCredentials();
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the CreateBroadbandEntry method to ensure the entry is created properly.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateBroadbandEntryTest()
        {
            string name = "Test Entry";
            RasDevice device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

            RasEntry expected = new RasEntry(name)
            {
                Device = device,
                EncryptionType = RasEncryptionType.Optional,
                EntryType = RasEntryType.Broadband,
                FramingProtocol = RasFramingProtocol.Ppp,
#if (WIN2K8 || WIN7 || WIN8)
                NetworkProtocols = new RasNetworkProtocols()
                {
                    IP = true,
                    IPv6 = true
                },
#else
                NetworkProtocols = new RasNetworkProtocols()
                {
                    IP = true
                },
#endif
                PhoneNumber = string.Empty,
                RedialCount = 3,
                RedialPause = 60
            };

            expected.Options.RemoteDefaultGateway = true;
            expected.Options.ModemLights = true;
            expected.Options.SecureLocalFiles = true;
            expected.Options.RequirePap = true;
            expected.Options.PreviewUserPassword = true;
            expected.Options.ShowDialingProgress = true;
            expected.Options.RequireChap = true;
            expected.Options.RequireMSChap2 = true;
            expected.Options.SecureFileAndPrint = true;
            expected.Options.SecureClientForMSNet = true;
            expected.Options.DoNotNegotiateMultilink = true;
            expected.Options.DoNotUseRasCredentials = true;
            expected.Options.Internet = true;
            expected.Options.DisableNbtOverIP = true;
            expected.Options.ReconnectIfDropped = true;

#if (WIN2K8 || WIN7 || WIN8)

            expected.Options.IPv6RemoteDefaultGateway = true;

#endif

            RasEntry actual = RasEntry.CreateBroadbandEntry(name, device);

            TestUtilities.AssertEntry(expected, actual);
        }

        /// <summary>
        /// Tests the CreateBroadbandEntry method to ensure an ArgumentException is thrown when the entry name is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBroadbandEntryWithNullEntryNameArgumentExceptionTest()
        {
            string name = null;
            RasDevice device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

            RasEntry.CreateBroadbandEntry(name, device);
        }

        /// <summary>
        /// Tests the CreateBroadbandEntry method to ensure an ArgumentException is thrown when the entry name is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBroadbandEntryWithEmptyEntryNameArgumentExceptionTest()
        {
            string name = string.Empty;
            RasDevice device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

            RasEntry.CreateBroadbandEntry(name, device);
        }

        /// <summary>
        /// Tests the CreateBroadbandEntry method to ensure an ArgumentNullException is thrown when a device is not provided.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateBroadbandEntryWithNullDeviceArgumentNullExceptionTest()
        {
            string name = "Test Entry";
            RasDevice device = null;

            RasEntry.CreateBroadbandEntry(name, device);
        }

#endif

        /// <summary>
        /// Tests the CreateVpnEntry method to ensure an ArgumentException is thrown when the entry name is empty.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVpnEntryWithEmptyNameArgumentExceptionTest()
        {
            string name = string.Empty;
            string serverAddress = "127.0.0.1";
            RasVpnStrategy strategy = RasVpnStrategy.Default;
            RasDevice device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

            RasEntry.CreateVpnEntry(name, serverAddress, strategy, device);
        }

        /// <summary>
        /// Tests the CreateVpnEntry method to ensure an ArgumentException is thrown when the server address is empty.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVpnEntryWithEmptyServerAddressArgumentExceptionTest()
        {
            string name = "Test Entry";
            string serverAddress = string.Empty;
            RasVpnStrategy strategy = RasVpnStrategy.Default;
            RasDevice device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

            RasEntry.CreateVpnEntry(name, serverAddress, strategy, device);
        }

        /// <summary>
        /// Tests the CreateVpnEntry method to ensure an ArgumentNullException is thrown when device is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateVpnEntryWithNullDeviceArgumentExceptionTest()
        {
            string name = "Test Entry";
            string serverAddress = "127.0.0.1";
            RasVpnStrategy strategy = RasVpnStrategy.Default;
            RasDevice device = null;

            RasEntry.CreateVpnEntry(name, serverAddress, strategy, device);
        }

        /// <summary>
        /// Tests the CreateVpnEntry method to ensure a new default VPN entry is created.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateVpnEntryTest()
        {
            string name = "Test Entry";
            string serverAddress = "127.0.0.1";
            RasVpnStrategy strategy = RasVpnStrategy.L2tpFirst;
            RasDevice device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

            RasEntry expected = new RasEntry(name);
            expected.Device = device;
            expected.EncryptionType = RasEncryptionType.Require;
            expected.EntryType = RasEntryType.Vpn;
            expected.FramingProtocol = RasFramingProtocol.Ppp;
            expected.NetworkProtocols.IP = true;
            
            expected.Options.RemoteDefaultGateway = true;
            expected.Options.ModemLights = true;
            expected.Options.RequireEncryptedPassword = true;
            expected.Options.PreviewUserPassword = true;
            expected.Options.PreviewDomain = true;
            expected.Options.ShowDialingProgress = true;

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            expected.RedialCount = 3;
            expected.RedialPause = 60;
            expected.Options.DoNotNegotiateMultilink = true;
            expected.Options.ReconnectIfDropped = true;

#endif
#if (WIN2K8 || WIN7 || WIN8)

            expected.Options.IPv6RemoteDefaultGateway = true;
            expected.Options.UseTypicalSettings = true;
            expected.NetworkProtocols.IPv6 = true;

#endif

            expected.PhoneNumber = serverAddress;
            expected.VpnStrategy = strategy;

            RasEntry actual = RasEntry.CreateVpnEntry(name, serverAddress, strategy, device);

            TestUtilities.AssertEntry(expected, actual);
        }

        /// <summary>
        /// Tests the CreateDialUpEntry method to ensure a new default dialup entry is created.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateDialUpEntryTest()
        {
            string name = "Test Entry";
            string phoneNumber = "555-555-1234";
            RasDevice device = RasDevice.Create("My Modem", RasDeviceType.Modem);

            RasEntry expected = new RasEntry(name);
            expected.Device = device;
            expected.DialMode = RasDialMode.None;
            expected.EntryType = RasEntryType.Phone;
            expected.FramingProtocol = RasFramingProtocol.Ppp;
            expected.IdleDisconnectSeconds = RasIdleDisconnectTimeout.Default;
            expected.NetworkProtocols.IP = true;
#if (WINXP || WIN2K8 || WIN7 || WIN8)
            expected.RedialCount = 3;
            expected.RedialPause = 60;
#endif
#if (WIN2K8 || WIN7 || WIN8)
            expected.NetworkProtocols.IPv6 = true;
#endif
            expected.PhoneNumber = phoneNumber;
            expected.VpnStrategy = RasVpnStrategy.Default;

            RasEntry actual = RasEntry.CreateDialUpEntry(name, phoneNumber, device);

            TestUtilities.AssertEntry(expected, actual);
        }

        /// <summary>
        /// Tests the CreateDialUpEntry method to ensure an exception is thrown for empty entry names.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDialUpEntryWithEmptyNameTest()
        {
            string name = string.Empty;
            string phoneNumber = "555-555-1234";
            RasDevice device = RasDevice.Create("My Modem", RasDeviceType.Modem);

            RasEntry.CreateDialUpEntry(name, phoneNumber, device);
        }

        /// <summary>
        /// Tests the CreateDialUpEntry method to ensure an exception is thrown for empty phone numbers.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDialUpEntryWithEmptyPhoneNumberTest()
        {
            string name = "Test Entry";
            string phoneNumber = string.Empty;
            RasDevice device = RasDevice.Create("My Modem", RasDeviceType.Modem);

            RasEntry.CreateDialUpEntry(name, phoneNumber, device);
        }

        /// <summary>
        /// Tests the CreateDialUpEntry method to ensure an exception is thrown for null devices.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateDialUpEntryWithNullDeviceTest()
        {
            string name = "Test Entry";
            string phoneNumber = "555-555-1234";
            RasDevice device = null;

            RasEntry.CreateDialUpEntry(name, phoneNumber, device);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the credentials get cleared as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentialsTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            bool actual = target.ClearCredentials();

            Assert.IsTrue(actual);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure an InvalidOperationException is thrown when the entry does not belong to a phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClearCredentialsInvalidOperationExceptionTest()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.ClearCredentials();
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the ClearCredentials method to ensure an invalid operation exception is thrown when the entry does not belong to a phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClearCredentialsInvalidOperationException()
        {
            string name = "Test Entry";

            RasEntry target = new RasEntry(name);
            target.ClearCredentials(RasPreSharedKey.Client);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the client pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1ClientPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            bool result = target.ClearCredentials(RasPreSharedKey.Client);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the DDM pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1DdmPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            bool result = target.ClearCredentials(RasPreSharedKey.Ddm);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the server pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1ServerPreSharedKeyTest()
        {
            RasPhoneBook pbk = new RasPhoneBook();

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = pbk;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            bool result = target.ClearCredentials(RasPreSharedKey.Server);

            Assert.IsTrue(result);
        }

#endif

        /// <summary>
        /// Tests the Clone method to ensure an object is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CloneTest()
        {
            RasEntry target = new RasEntry("Test");

            RasEntry actual = (RasEntry)target.Clone();

            Assert.AreEqual(target.Name, actual.Name);
        }

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure the proper object is returned when called.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCustomAuthDataTest()
        {
            byte[] expected = new byte[] { 0, 1 };

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCustomAuthData(It.IsAny<string>(), It.IsAny<string>())).Returns(expected);

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            byte[] actual = target.GetCustomAuthData();

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure the proper object is returned when called.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetEapUserDataTest()
        {
            byte[] expected = new byte[] { 0, 1 };

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            byte[] actual = target.GetEapUserData();

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure an InvalidOperationException is thrown when the target owner is not set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetEapUserDataNullPhoneBookTest()
        {
            RasEntry target = new RasEntry("Test Entry");

            target.GetEapUserData();
        }

        /// <summary>
        /// Tests the UpdateCustomAuthData to ensure the custom authentication data is updated as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCustomAuthDataTest()
        {
            bool expected = true;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCustomAuthData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(expected);

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            bool actual = target.UpdateCustomAuthData(new byte[] { 0, 1, 2, 3, 4, 5 });

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the UpdateCustomAuthData method to ensure an InvalidOperationException is thrown when the phone book is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateCustomAuthDataNullPhoneBookTest()
        {
            RasEntry target = new RasEntry("Test Entry");
            target.UpdateCustomAuthData(new byte[] { 0 });
        }

        /// <summary>
        /// Tests the UpdateEapUserData method to ensure the EAP data is updated as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateEapUserDataTest()
        {
            bool expected = true;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(expected);

            RasEntry target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            bool actual = target.UpdateEapUserData(new byte[] { 0 });

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the UpdateEapUserData method to ensure an InvalidOperationException is thrown when the phone book is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UpdateEapUserDataNullPhoneBookTest()
        {
            RasEntry target = new RasEntry("Test Entry");
            target.UpdateEapUserData(new byte[] { 0 });
        }

        /// <summary>
        /// Tests the UpdateEapUserData method to ensure an ArgumentNullException is thrown when the data is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UpdateEapUserDataNullDataTest()
        {
            RasEntry target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            target.UpdateEapUserData(null);
        }

        #endregion

        #endregion
    }
}