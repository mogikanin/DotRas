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
            var name = "Test Entry";

            var target = new RasEntry(name);

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
            var name = string.Empty;

            var target = new RasEntry(name);
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

            var target = new RasEntry(name);
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
            var name = "Test Entry";
            var expected = "12345";

            var target = new RasEntry(name);
            target.X25UserData = expected;

            var actual = target.X25UserData;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25PadType property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25PadTypeTest()
        {
            var name = "Test Entry";
            var expected = "12345";

            var target = new RasEntry(name);
            target.X25PadType = expected;

            var actual = target.X25PadType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25Facilities property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25FacilitiesTest()
        {
            var name = "Test Entry";
            var expected = "12345";

            var target = new RasEntry(name);
            target.X25Facilities = expected;

            var actual = target.X25Facilities;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the X25Address property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void X25AddressTest()
        {
            var name = "Test Entry";
            var expected = "12345";

            var target = new RasEntry(name);
            target.X25Address = expected;

            var actual = target.X25Address;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the options property to ensure an instance is always returned even when the object is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsAlwaysDefaultTest()
        {
            var name = "Test Entry";

            var target = new RasEntry(name);

            var actual = target.Options;

            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Tests the options property when using a default instance matches that of the default constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsDefaultTest()
        {
            var name = "Test Entry";

            var expected = new RasEntryOptions();

            var target = new RasEntry(name);
            var actual = target.Options;

            TestUtilities.AssertRasEntryOptions(expected, actual);
        }

        /// <summary>
        /// Tests the WinsAddressAlt property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void WinsAddressAltTest()
        {
            var name = "Test Entry";
            var expected = IPAddress.Loopback;

            var target = new RasEntry(name);
            target.WinsAddressAlt = expected;

            var actual = target.WinsAddressAlt;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the WinsAddress property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void WinsAddressTest()
        {
            var name = "Test Entry";
            var expected = IPAddress.Loopback;

            var target = new RasEntry(name);
            target.WinsAddress = expected;

            var actual = target.WinsAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the VpnStrategy property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void VpnStrategyTest()
        {
            var name = "Test Entry";
            var expected = RasVpnStrategy.L2tpFirst;

            var target = new RasEntry(name);
            target.VpnStrategy = expected;

            var actual = target.VpnStrategy;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SubEntries property to ensure the property returns an empty collection.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SubEntriesTest()
        {
            var name = "Test Entry";
            var expected = 0;

            var target = new RasEntry(name);
            var actual = target.SubEntries.Count;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Script property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ScriptTest()
        {
            var name = "Test Entry";
            var expected = string.Empty;

            var target = new RasEntry(name);
            target.Script = expected;

            var actual = target.Script;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneNumber property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneNumberTest()
        {
            var name = "Test Entry";
            var expected = "127.0.0.1";

            var target = new RasEntry(name);
            target.PhoneNumber = expected;

            var actual = expected;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Owner property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OwnerTest()
        {
            var name = "Test Entry";
            var expected = new RasPhoneBook();

            var target = new RasEntry(name);
            target.Owner = expected;

            var actual = target.Owner;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the NetworkProtocols property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetworkProtocolsTest()
        {
            var name = "Test Entry";
            var expected = new RasNetworkProtocols()
            {
                IP = true
            };

            var target = new RasEntry(name);
            target.NetworkProtocols = expected;

            var actual = target.NetworkProtocols;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the Name property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NameTest()
        {
            var expected = "Test Entry";

            var target = new RasEntry(expected);

            var actual = target.Name;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPAddress property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressTest()
        {
            var name = "Test Entry";
            var expected = IPAddress.Loopback;

            var target = new RasEntry(name);
            target.IPAddress = expected;

            var actual = target.IPAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IdleDisconnectSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IdleDisconnectSecondsTest()
        {
            var name = "Test Entry";
            var expected = RasIdleDisconnectTimeout.Disabled;

            var target = new RasEntry(name);
            target.IdleDisconnectSeconds = expected;

            var actual = target.IdleDisconnectSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Id property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IdTest()
        {
            var name = "Test Entry";
            var expected = Guid.NewGuid();

            var target = new RasEntry(name);
            target.Id = expected;

            var actual = target.Id;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the HangUpExtraSampleSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpExtraSampleSecondsTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.HangUpExtraSampleSeconds = expected;

            var actual = target.HangUpExtraSampleSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the HangUpExtraPercent property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpExtraPercentTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.HangUpExtraPercent = expected;

            var actual = target.HangUpExtraPercent;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the FramingProtocol property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void FramingProtocolTest()
        {
            var name = "Test Entry";
            var expected = RasFramingProtocol.Ppp;

            var target = new RasEntry(name);
            target.FramingProtocol = expected;

            var actual = target.FramingProtocol;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the FramingProtocol property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void FrameSizeTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.FrameSize = expected;

            var actual = target.FrameSize;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EntryType property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryTypeTest()
        {
            var name = "Test Entry";
            var expected = RasEntryType.Vpn;

            var target = new RasEntry(name);
            target.EntryType = expected;

            var actual = target.EntryType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EncryptionType property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EncryptionTypeTest()
        {
            var name = "Test Entry";
            var expected = RasEncryptionType.Require;

            var target = new RasEntry(name);
            target.EncryptionType = expected;

            var actual = target.EncryptionType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DnsAddressAlt property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DnsAddressAltTest()
        {
            var name = "Test Entry";
            var expected = IPAddress.Loopback;

            var target = new RasEntry(name);
            target.DnsAddressAlt = expected;

            var actual = target.DnsAddressAlt;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DnsAddress property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DnsAddressTest()
        {
            var name = "Test Entry";
            var expected = IPAddress.Loopback;

            var target = new RasEntry(name);
            target.DnsAddress = expected;

            var actual = target.DnsAddress;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialMode property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialModeTest()
        {
            var name = "Test Entry";
            var expected = RasDialMode.DialAsNeeded;

            var target = new RasEntry(name);
            target.DialMode = expected;

            var actual = target.DialMode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialExtraSampleSeconds property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialExtraSampleSecondsTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.DialExtraSampleSeconds = expected;

            var actual = target.DialExtraSampleSeconds;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DialExtraPercent property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DialExtraPercentTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.DialExtraPercent = expected;

            var actual = target.DialExtraPercent;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Device property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DeviceTest()
        {
            var name = "Test Entry";
            var expected = RasDevice.Create(name, RasDeviceType.Vpn);

            var target = new RasEntry(name);
            target.Device = expected;

            var actual = target.Device;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CustomDialDll property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomDialDllTest()
        {
            var name = "Test Entry";
            var expected = "Test.dll";

            var target = new RasEntry(name);
            target.CustomDialDll = expected;

            var actual = target.CustomDialDll;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CustomAuthKey property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CustomAuthKeyTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.CustomAuthKey = expected;

            var actual = target.CustomAuthKey;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CountryId property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CountryIdTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.CountryId = expected;

            var actual = target.CountryId;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CountryCode property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CountryCodeTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.CountryCode = expected;

            var actual = target.CountryCode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Channels property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ChannelsTest()
        {
            var name = "Test Entry";
            var expected = int.MaxValue;

            var target = new RasEntry(name);
            target.Channels = expected;

            var actual = target.Channels;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AutoDialFunc property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AutoDialFuncTest()
        {
            var name = "Test Entry";
            var expected = "TestFunc";

#pragma warning disable 0618
            var target = new RasEntry(name);
            target.AutoDialFunc = expected;

            var actual = target.AutoDialFunc;
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
            var name = "Test Entry";
            var expected = "Test.dll";

#pragma warning disable 0618
            var target = new RasEntry(name);
            target.AutoDialDll = expected;

            var actual = target.AutoDialDll;
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
            var name = "Test Entry";
            var expected = "123";

            var target = new RasEntry(name);
            target.AreaCode = expected;

            var actual = target.AreaCode;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AlternatePhoneNumbers property to ensure it matches the value passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AlternatePhoneNumbersTest()
        {
            var name = "Test Entry";
            var expected = new Collection<string>();
            expected.Add("555-555-1234");
            expected.Add("555-555-2345");

            var target = new RasEntry(name);
            target.AlternatePhoneNumbers = expected;

            var actual = target.AlternatePhoneNumbers;

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
            var name = "Test Entry";
            var expected = IPAddress.IPv6Loopback;

            var target = new RasEntry(name);
            target.IPv6Address = expected;

            var actual = target.IPv6Address;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPv6PrefixLength property to ensure the value returned matches the value passed into the property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPv6PrefixLengthTest()
        {
            var name = "Test Entry";
            var expected = 10;

            var target = new RasEntry(name);
            target.IPv6PrefixLength = expected;

            var actual = target.IPv6PrefixLength;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the NetworkOutageTime property to ensure the value returned matches the value passed into the property.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetworkOutageTimeTest()
        {
            var name = "Test Entry";
            var expected = 1000;

            var target = new RasEntry(name);
            target.NetworkOutageTime = expected;

            var actual = target.NetworkOutageTime;

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
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ValidateEntryName(It.IsAny<string>(), It.IsAny<string>())).Returns(NativeMethods.ERROR_ALREADY_EXISTS);

            var entryName = "Test Entry";
            var phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            var expected = true;
            var actual = RasEntry.Exists(entryName, phoneBookPath);

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
            var phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

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
            var entryName = string.Empty;
            var phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

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
            var entryName = "Test Entry";
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
            var entryName = "Test Entry";
            var phoneBookPath = string.Empty;

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
            var entryName = "Test Entry";
            var phoneBookPath = string.Format("C:\\{0}\\{1}.pbk", Guid.NewGuid(), Guid.NewGuid());

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
            var name = "Test Entry";

            var target = new RasEntry(name);
            target.UpdateCredentials(new NetworkCredential("Test", "User"));
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials are updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentialsTest()
        {
            var name = "Test Entry";
            var credentials = new NetworkCredential("Test", "User");

            var pbk = new RasPhoneBook();

            var target = new RasEntry(name);
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var result = target.UpdateCredentials(credentials);

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
            var name = "Test Entry";

            var pbk = new RasPhoneBook();

            var target = new RasEntry(name);
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var result = target.UpdateCredentials(RasPreSharedKey.Client, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the DDM pre-shared key is updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials2DdmPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var result = target.UpdateCredentials(RasPreSharedKey.Ddm, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the client pre-shared key is updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials2ServerPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var result = target.UpdateCredentials(RasPreSharedKey.Server, "value");

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials for all users are updated successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCredentials1Test()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var result = target.UpdateCredentials(new NetworkCredential("Test", "User"), true);

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
            var name = "Test Entry";
            var credentials = new NetworkCredential("Test", "User");

            var target = new RasEntry(name);
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
            var name = "Test Entry";
            NetworkCredential credentials = null;

            var target = new RasEntry(name);
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
            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var result = target.UpdateCredentials(new NetworkCredential("Test", "User"), false);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the InternalSetCredentials method to ensure it will properly update credentials for all users.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void InternalSetCredentialsForAllUsersTest()
        {
            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), false)).Returns(true);

            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var result = target.UpdateCredentials(new NetworkCredential("Test", "User"), true);

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
            var name = "Test Entry";

            var target = new RasEntry(name);
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
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;
            
            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetEntryProperties(pbk, target)).Returns(true);

            var result = target.Update();

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
            var name = "Test Entry";

            var target = new RasEntry(name);
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
            var name = "Test Entry";
            string newEntryName = null;

            var target = new RasEntry(name);
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
            var name = "Test Entry";
            var newEntryName = string.Empty;

            var target = new RasEntry(name);
            target.Rename(newEntryName);
        }

        /// <summary>
        /// Tests the Rename method to ensure an entry not in a phone book can be renamed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RenameEntryNotInPhoneBookTest()
        {
            var name = "Test Entry";
            var newEntryName = "New Entry";
            var expected = true;

            var target = new RasEntry(name);
            var actual = target.Rename(newEntryName);

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
            var name = "Test Entry";
            var newEntryName = ".\\Test*!";
            var pbk = new RasPhoneBook();

            var target = new RasEntry(name);
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
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
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var expected = new NetworkCredential("Test", "User");

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            var actual = target.GetCredentials();

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
            var name = "Test Entry";

            var target = new RasEntry(name);
            var actual = target.GetCredentials(RasPreSharedKey.Client);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1ClientPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var expected = new NetworkCredential(string.Empty, "********", string.Empty);

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            var actual = target.GetCredentials(RasPreSharedKey.Client);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1DdmPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var expected = new NetworkCredential(string.Empty, "********", string.Empty);

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            var actual = target.GetCredentials(RasPreSharedKey.Ddm);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TryGetCredentials method to ensure the expected pre-shared key is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentials1ServerPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var expected = new NetworkCredential(string.Empty, "********", string.Empty);

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCM>())).Returns(expected);

            var actual = target.GetCredentials(RasPreSharedKey.Server);

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
            var name = "Test Entry";

            var target = new RasEntry(name);
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
            var name = "Test Entry";
            var device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

            var expected = new RasEntry(name)
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

            var actual = RasEntry.CreateBroadbandEntry(name, device);

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
            var device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

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
            var name = string.Empty;
            var device = RasDevice.Create("WAN Miniport (PPPOE)", RasDeviceType.PPPoE);

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
            var name = "Test Entry";
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
            var name = string.Empty;
            var serverAddress = "127.0.0.1";
            var strategy = RasVpnStrategy.Default;
            var device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

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
            var name = "Test Entry";
            var serverAddress = string.Empty;
            var strategy = RasVpnStrategy.Default;
            var device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

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
            var name = "Test Entry";
            var serverAddress = "127.0.0.1";
            var strategy = RasVpnStrategy.Default;
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
            var name = "Test Entry";
            var serverAddress = "127.0.0.1";
            var strategy = RasVpnStrategy.L2tpFirst;
            var device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);

            var expected = new RasEntry(name);
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

            var actual = RasEntry.CreateVpnEntry(name, serverAddress, strategy, device);

            TestUtilities.AssertEntry(expected, actual);
        }

        /// <summary>
        /// Tests the CreateDialUpEntry method to ensure a new default dialup entry is created.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateDialUpEntryTest()
        {
            var name = "Test Entry";
            var phoneNumber = "555-555-1234";
            var device = RasDevice.Create("My Modem", RasDeviceType.Modem);

            var expected = new RasEntry(name);
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

            var actual = RasEntry.CreateDialUpEntry(name, phoneNumber, device);

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
            var name = string.Empty;
            var phoneNumber = "555-555-1234";
            var device = RasDevice.Create("My Modem", RasDeviceType.Modem);

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
            var name = "Test Entry";
            var phoneNumber = string.Empty;
            var device = RasDevice.Create("My Modem", RasDeviceType.Modem);

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
            var name = "Test Entry";
            var phoneNumber = "555-555-1234";
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
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            var actual = target.ClearCredentials();

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
            var name = "Test Entry";

            var target = new RasEntry(name);
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
            var name = "Test Entry";

            var target = new RasEntry(name);
            target.ClearCredentials(RasPreSharedKey.Client);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the client pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1ClientPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            var result = target.ClearCredentials(RasPreSharedKey.Client);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the DDM pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1DdmPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            var result = target.ClearCredentials(RasPreSharedKey.Ddm);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearCredentials method to ensure the server pre-shared key is cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearCredentials1ServerPreSharedKeyTest()
        {
            var pbk = new RasPhoneBook();

            var target = new RasEntry("Test Entry");
            target.Owner = pbk;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<NativeMethods.RASCREDENTIALS>(), true)).Returns(true);

            var result = target.ClearCredentials(RasPreSharedKey.Server);

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
            var target = new RasEntry("Test");

            var actual = (RasEntry)target.Clone();

            Assert.AreEqual(target.Name, actual.Name);
        }

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure the proper object is returned when called.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCustomAuthDataTest()
        {
            var expected = new byte[] { 0, 1 };

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetCustomAuthData(It.IsAny<string>(), It.IsAny<string>())).Returns(expected);

            var target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            var actual = target.GetCustomAuthData();

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure the proper object is returned when called.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetEapUserDataTest()
        {
            var expected = new byte[] { 0, 1 };

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);

            var target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            var actual = target.GetEapUserData();

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
            var target = new RasEntry("Test Entry");

            target.GetEapUserData();
        }

        /// <summary>
        /// Tests the UpdateCustomAuthData to ensure the custom authentication data is updated as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateCustomAuthDataTest()
        {
            var expected = true;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetCustomAuthData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(expected);

            var target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            var actual = target.UpdateCustomAuthData(new byte[] { 0, 1, 2, 3, 4, 5 });

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
            var target = new RasEntry("Test Entry");
            target.UpdateCustomAuthData(new byte[] { 0 });
        }

        /// <summary>
        /// Tests the UpdateEapUserData method to ensure the EAP data is updated as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateEapUserDataTest()
        {
            var expected = true;

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.SetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(expected);

            var target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            var actual = target.UpdateEapUserData(new byte[] { 0 });

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
            var target = new RasEntry("Test Entry");
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
            var target = new RasEntry("Test Entry");
            target.Owner = new RasPhoneBook();

            target.UpdateEapUserData(null);
        }

        #endregion

        #endregion
    }
}