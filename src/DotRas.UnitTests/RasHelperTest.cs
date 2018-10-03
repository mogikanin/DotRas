//--------------------------------------------------------------------------
// <copyright file="RasHelperTest.cs" company="Jeff Winn">
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
    using System.ComponentModel;
    using System.Net;
    using System.Runtime.InteropServices;
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using DotRas.UnitTests.Mocking.Interop;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasHelper"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasHelperTest : UnitTest
    {
        #region Constructors

        #endregion

        #region Methods

        #region AllocateLocallyUniqueId

        /// <summary>
        /// Tests the AllocateLocallyUniqueId method to ensure a value is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AllocateLocallyUniqueIdTest()
        {
            var expected = new Luid(100, 100);

            var target = new CopyStructToAddrMock<Luid>();
            target.Result = expected;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.AllocateLocallyUniqueIdImpl(It.IsAny<IntPtr>())).Callback((IntPtr pLuid) =>
            {
                target.Execute(pLuid);
            }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.AllocateLocallyUniqueId();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the AllocateLocallyUniqueId method to ensure a Win32Exception is thrown when the result from the Win32 API is unexpected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(Win32Exception))]
        public void AllocateLocallyUniqueIdUnexpectedExceptionTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.AllocateLocallyUniqueIdImpl(It.IsAny<IntPtr>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            var result = RasHelper.Instance.AllocateLocallyUniqueId();
        }

        #endregion

        #region ClearConnectionStatistics

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure the statistics are cleared successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearConnectionStatisticsTest()
        {
            var handle = new RasHandle(new IntPtr(1), false);

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearConnectionStatistics(It.IsAny<RasHandle>())).Returns(NativeMethods.SUCCESS);

            var result = RasHelper.Instance.ClearConnectionStatistics(handle);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure an InvalidHandleException is thrown when an invalid handle is used.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidHandleException))]
        public void ClearConnectionStatisticsInvalidHandleTest()
        {
            var handle = new RasHandle(new IntPtr(1), false);

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearConnectionStatistics(It.IsAny<RasHandle>())).Returns(NativeMethods.ERROR_INVALID_HANDLE);

            RasHelper.Instance.ClearConnectionStatistics(handle);
        }

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void ClearConnectionStatisticsUnexpectedResultTest()
        {
            var handle = new RasHandle(new IntPtr(1), false);

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearConnectionStatistics(It.IsAny<RasHandle>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.ClearConnectionStatistics(handle);
        }

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure a NotSupportedException is thrown when an entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void ClearConnectionStatisticsNotSupportedTest()
        {
            var handle = new RasHandle(new IntPtr(1), false);

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearConnectionStatistics(It.IsAny<RasHandle>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.ClearConnectionStatistics(handle);
        }

        #endregion

        #region ClearLinkStatistics

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure it clears the link statistics as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearLinkStatisticsTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(NativeMethods.SUCCESS);

            var result = RasHelper.Instance.ClearLinkStatistics(new RasHandle(new IntPtr(1), false), 1);

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure an ArgumentException is thrown when the subentry id is less than or equal to zero.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void ClearLinkStatisticsSubEntryIdLessThanOrEqualToZeroTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(NativeMethods.SUCCESS);

            RasHelper.Instance.ClearLinkStatistics(new RasHandle(new IntPtr(1), false), 0);
        }

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure an InvalidHandleException is thrown when an invalid handle is used.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidHandleException))]
        public void ClearLinkStatisticsInvalidHandleTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(NativeMethods.ERROR_INVALID_HANDLE);

            RasHelper.Instance.ClearLinkStatistics(new RasHandle(new IntPtr(1), false), 1);
        }

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void ClearLinkStatisticsUnexpectedResultTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.ClearLinkStatistics(new RasHandle(new IntPtr(1), false), 1);
        }

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void ClearLinkStatisticsNotSupportedTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.ClearLinkStatistics(new RasHandle(new IntPtr(1), false), 1);
        }

        #endregion

        #region DeleteEntry

        /// <summary>
        /// Tests the DeleteEntry method to ensure an ArgumentNullException is thrown for null a phoneBook argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEntryNullPhoneBookTest()
        {
            RasHelper.Instance.DeleteEntry(null, "Test Entry");
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure an ArgumentException is thrown for an empty phoneBook argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEntryEmptyPhoneBookTest()
        {
            RasHelper.Instance.DeleteEntry(string.Empty, "Test Entry");
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure an ArgumentException is thrown for an empty entryName argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEntryNullEntryNameTest()
        {
            RasHelper.Instance.DeleteEntry("C:\\Test.pbk", null);
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure an ArgumentException is thrown for an empty entryName argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteEntryEmptyEntryNameTest()
        {
            RasHelper.Instance.DeleteEntry("C:\\Test.pbk", string.Empty);
        }
        
        /// <summary>
        /// Tests the DeleteEntry method to ensure an UnauthorizedAccessException is thrown when an access denied error is returned from the call.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void DeleteEntryAccessDeniedTest()
        {
            var phoneBookPath = "C:\\Test.pbk";
            var entryName = "Test Entry";

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.DeleteEntry(phoneBookPath, entryName)).Returns(NativeMethods.ERROR_ACCESS_DENIED);

            RasHelper.Instance.DeleteEntry(phoneBookPath, entryName);
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure it can properly delete an entry.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DeleteEntryTest()
        {
            var phoneBookPath = "C:\\Test.pbk";
            var entryName = "Test Entry";

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.DeleteEntry(phoneBookPath, entryName)).Returns(0);

            var result = RasHelper.Instance.DeleteEntry(phoneBookPath, entryName);

            Assert.IsTrue(result);
            mock.Verify();
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure a RasException is thrown when an unexpected result occurs.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void DeleteEntryUnexpectedResultTest()
        {
            var phoneBookPath = "C:\\Test.pbk";
            var entryName = "Test Entry";

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.DeleteEntry(phoneBookPath, entryName)).Returns(NativeMethods.ERROR_CANNOT_FIND_PHONEBOOK_ENTRY);

            RasHelper.Instance.DeleteEntry(phoneBookPath, entryName);
        }

        /// <summary>
        /// Tests the DeleteEntry method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void DeleteEntryNotSupportedTest()
        {
            var phoneBookPath = "C:\\Test.pbk";
            var entryName = "Test Entry";

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.DeleteEntry(phoneBookPath, entryName)).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.DeleteEntry(phoneBookPath, entryName);
        }

        #endregion

        #region Dial

        /// <summary>
        /// Tests the Dial method to ensure an InvalidOperationException is thrown when the entry name and phone number are both empty strings.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DialEmptyEntryNameAndPhoneNumberTest()
        {
            RasHelper.Instance.Dial(null, new NativeMethods.RASDIALPARAMS() { entryName = string.Empty, phoneNumber = string.Empty }, new NativeMethods.RASDIALEXTENSIONS(), null, NativeMethods.RASEAPF.None);
        }

        /// <summary>
        /// Tests the Dial method to ensure an InvalidOperationException is thrown when the entry name and phone number are both null references.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DialNullEntryNameAndPhoneNumberTest()
        {
            RasHelper.Instance.Dial(null, new NativeMethods.RASDIALPARAMS() { entryName = null, phoneNumber = null }, new NativeMethods.RASDIALEXTENSIONS(), null, NativeMethods.RASEAPF.None);
        }

        /// <summary>
        /// Tests the Dial method to ensure an InvalidOperationException is thrown when the entry name is a null reference and the phone number is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DialNullEntryNameWithEmptyPhoneNumberTest()
        {
            RasHelper.Instance.Dial(null, new NativeMethods.RASDIALPARAMS() { entryName = null, phoneNumber = string.Empty }, new NativeMethods.RASDIALEXTENSIONS(), null, NativeMethods.RASEAPF.None);
        }

        /// <summary>
        /// Tests the Dial method to ensure an InvalidOperationException is thrown when the entry name is an empty string and the phone number is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void DialEmptyEntryNameWithNullPhoneNumberTest()
        {
            RasHelper.Instance.Dial(null, new NativeMethods.RASDIALPARAMS() { entryName = string.Empty, phoneNumber = null }, new NativeMethods.RASDIALEXTENSIONS(), null, NativeMethods.RASEAPF.None);
        }

        #endregion

        #region GetActiveConnections

        /// <summary>
        /// Tests the GetActiveConnections method to ensure a RasException is thrown when an unexpected result occurs.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetActiveConnectionsUnexpectedResultTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumConnections(It.IsAny<StructBufferedPInvokeParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetActiveConnections();
        }

        /// <summary>
        /// Tests the GetActiveConnections method to ensure it can properly return an empty collection when no active connections are present.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetActiveConnectionsWithNoActiveConnectionsTest()
        {
            var expected = 0;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumConnections(It.IsAny<StructBufferedPInvokeParams>())).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetActiveConnections();

            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        /// Tests the GetActiveConnections method to ensure it can properly return a collection of active connections.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetActiveConnectionsTest()
        {
            var expected = new NativeMethods.RASCONN[]
            {
                new NativeMethods.RASCONN()
                {
                    deviceName = "WAN Miniport (PPTP)",
                    deviceType = NativeMethods.RASDT_Vpn,
                    entryId = Guid.NewGuid(),
                    entryName = "Test VPN Entry",
                    handle = new IntPtr(1),
                    phoneBook = "C:\\Test.pbk",
                    subEntryId = 0,
#if (WINXP || WIN2K8 || WIN7 || WIN8)
                    sessionId = Luid.Empty,
#endif
#if (WIN2K8 || WIN7 || WIN8)
                    correlationId = Guid.NewGuid()
#endif
                },
                new NativeMethods.RASCONN()
                {
                    deviceName = "Standard POTS Modem",
                    deviceType = NativeMethods.RASDT_Modem,
                    entryId = Guid.NewGuid(),
                    entryName = "Test Modem Entry",
                    handle = new IntPtr(2),
                    phoneBook = "C:\\Test.pbk",
                    subEntryId = 0,
#if (WINXP || WIN2K8 || WIN7 || WIN8)
                    sessionId = Luid.Empty,
#endif
#if (WIN2K8 || WIN7 || WIN8)
                    correlationId = Guid.NewGuid()
#endif
                }
            };

            var expectedSize = new IntPtr(Marshal.SizeOf(typeof(NativeMethods.RASCONN)) * expected.Length);

            var target = new StructBufferedPInvokeMock<StructBufferedPInvokeParams, NativeMethods.RASCONN>();
            target.Result = expected;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumConnections(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize != expectedSize))).Callback((StructBufferedPInvokeParams info) =>
                {
                    target.Execute(info);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.EnumConnections(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize == expectedSize))).Callback((StructBufferedPInvokeParams info) =>
                {
                    target.Execute(info);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetActiveConnections();

            Assert.AreEqual(expected.Length, actual.Count);

            for (var index = 0; index < expected.Length; index++)
            {
                var objA = expected[index];
                var objB = actual[index];

                Assert.AreEqual(objA.deviceName, objB.Device.Name);
                Assert.IsTrue(string.Equals(objA.deviceType, objB.Device.DeviceType.ToString(), StringComparison.CurrentCultureIgnoreCase));
                Assert.AreEqual(objA.entryId, objB.EntryId);
                Assert.AreEqual(objA.entryName, objB.EntryName);
                Assert.AreEqual(objA.handle, objB.Handle.DangerousGetHandle());
                Assert.AreEqual(objA.phoneBook, objB.PhoneBookPath);
                Assert.AreEqual(objA.subEntryId, objB.SubEntryId);

#if (WINXP || WIN2K8 || WIN7 || WIN8)
                Assert.AreEqual(objA.sessionId, objB.SessionId);
#endif
#if (WIN2K8 || WIN7 || WIN8)
                Assert.AreEqual(objA.correlationId, objB.CorrelationId);
#endif
            }
        }

        /// <summary>
        /// Tests the GetActiveConnections method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetActiveConnectionsNotSupportedTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumConnections(It.IsAny<StructBufferedPInvokeParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetActiveConnections();
        }

        #endregion

        #region GetAutoDialAddress

        /// <summary>
        /// Tests the GetAutoDialAddress method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetAutoDialAddressUnexpectedResultTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialAddress(It.IsAny<RasGetAutodialAddressParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetAutoDialAddress("Test");
        }

        /// <summary>
        /// Tests the GetAutoDialAddress method to ensure a null reference is returned when the file is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetAutoDialAddressFileNotFoundTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialAddress(It.IsAny<RasGetAutodialAddressParams>())).Returns(NativeMethods.ERROR_FILE_NOT_FOUND);

            var result = RasHelper.Instance.GetAutoDialAddress("Test");

            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the GetAutoDialAddress method to ensure the correct values are returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetAutoDialAddressTest()
        {
            var expected = new NativeMethods.RASAUTODIALENTRY[]
            {
                new NativeMethods.RASAUTODIALENTRY()
                {
                    dialingLocation = 0,
                    entryName = "Test Entry 1",
                    options = 0
                },
                new NativeMethods.RASAUTODIALENTRY()
                {
                    dialingLocation = 1,
                    entryName = "Test Entry 2",
                    options = 1
                }
            };

            var expectedSize = Marshal.SizeOf(typeof(NativeMethods.RASAUTODIALENTRY)) * expected.Length;

            var target = new StructBufferedPInvokeMock<RasGetAutodialAddressParams, NativeMethods.RASAUTODIALENTRY>();
            target.Result = expected;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialAddress(It.Is<RasGetAutodialAddressParams>(i => i.BufferSize.ToInt32() != expectedSize))).Callback((RasGetAutodialAddressParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.GetAutodialAddress(It.Is<RasGetAutodialAddressParams>(i => i.BufferSize.ToInt32() == expectedSize))).Callback((RasGetAutodialAddressParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.SUCCESS);

            var address = "Test";
            var actual = RasHelper.Instance.GetAutoDialAddress(address);

            Assert.AreEqual(address, actual.Address);
            Assert.IsNotNull(actual.Entries);
            Assert.AreEqual(expected.Length, actual.Entries.Count);

            for (var index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(expected[index].dialingLocation, actual.Entries[index].DialingLocation);
                Assert.AreEqual(expected[index].entryName, actual.Entries[index].EntryName);
            }
        }

        /// <summary>
        /// Tests the GetAutoDialAddress method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAutoDialAddressNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialAddress(It.IsAny<RasGetAutodialAddressParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetAutoDialAddress("Test");
        }

        #endregion

        #region GetAutoDialAddresses

        /// <summary>
        /// Tests the GetAutoDialAddresses method to ensure the expected data is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetAutoDialAddressesTest()
        {
            var expected = new string[]
            {
                "1234567890",
                "0987654321"
            };

            var target = new StringBufferedPInvokeMock<StructBufferedPInvokeParams>();
            target.Result = expected;
            target.Offset = expected.Length * IntPtr.Size;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumAutodialAddresses(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize == IntPtr.Zero))).Callback((StructBufferedPInvokeParams info) =>
                {
                    target.Execute(info);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.EnumAutodialAddresses(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize != IntPtr.Zero))).Callback((StructBufferedPInvokeParams info) =>
                {
                    target.Execute(info);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetAutoDialAddresses();

            Assert.AreEqual(expected.Length, actual.Count);

            for (var index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(expected[index], actual[index]);
            }
        }

        /// <summary>
        /// Tests the GetAutoDialAddresses method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetAutoDialAddressesUnexpectedResultTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumAutodialAddresses(It.IsAny<StructBufferedPInvokeParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetAutoDialAddresses();
        }

        /// <summary>
        /// Tests the GetAutoDialAddresses method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAutoDialAddressesNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumAutodialAddresses(It.IsAny<StructBufferedPInvokeParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetAutoDialAddresses();
        }

        #endregion

        #region GetAutoDialEnable

        /// <summary>
        /// Tests the GetAutoDialEnable method to ensure the method returns the value expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetAutoDialEnableTest()
        {
            var expected = true;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;
            
            mock.Setup(o => o.GetAutodialEnable(It.IsAny<RasGetAutodialEnableParams>())).Callback((RasGetAutodialEnableParams value) =>
                {
                    value.Enabled = expected;
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetAutoDialEnable(0);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetAutoDialEnable method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetAutoDialEnableUnexpectedResultTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;
            
            mock.Setup(o => o.GetAutodialEnable(It.IsAny<RasGetAutodialEnableParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetAutoDialEnable(0);
        }

        /// <summary>
        /// Tests the GetAutoDialEnable method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAutoDialEnableNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialEnable(It.IsAny<RasGetAutodialEnableParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetAutoDialEnable(0);
        }

        #endregion

        #region GetAutoDialParameter

        /// <summary>
        /// Tests the GetAutoDialParameter method to ensure the value returned is expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetAutoDialParameterTest()
        {
            var expected = 10;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialParam(It.IsAny<RasGetAutodialParamParams>())).Callback((RasGetAutodialParamParams value) =>
                {
                    Marshal.WriteInt32(value.Address, expected);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetAutoDialParameter method to ensure an InvalidOperationException is thrown when the parameter size returned is incorrect.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAutoDialParameterIncorrectSizeTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialParam(It.IsAny<RasGetAutodialParamParams>())).Callback((RasGetAutodialParamParams value) =>
                {
                    value.BufferSize = sizeof(long);
                }).Returns(NativeMethods.SUCCESS);

            RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout);
        }

        /// <summary>
        /// Tests the GetAutoDialParameter method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetAutoDialParameterUnexpectedResultTest()
        {          
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialParam(It.IsAny<RasGetAutodialParamParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout);
        }

        /// <summary>
        /// Tests the GetAutoDialParameter method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAutoDialParameterNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetAutodialParam(It.IsAny<RasGetAutodialParamParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetAutoDialParameter(NativeMethods.RASADP.ConnectionQueryTimeout);
        }

        #endregion

        #region GetConnectionStatus

        /// <summary>
        /// Tests the GetConnectStatus method to ensure the connection state is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetConnectionStatusTest()
        {
            var expected = new NativeMethods.RASCONNSTATUS()
            {
                connectionState = RasConnectionState.AllDevicesConnected,
                deviceName = "WAN Miniport (PPTP)",
                deviceType = NativeMethods.RASDT_Vpn,
                errorCode = NativeMethods.SUCCESS,
                phoneNumber = IPAddress.Loopback.ToString(),
#if (WIN7 || WIN8)
                localEndPoint = new NativeMethods.RASTUNNELENDPOINT() { addr = new byte[] { 192, 168, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4 },
                remoteEndPoint = new NativeMethods.RASTUNNELENDPOINT() { addr = new byte[] { 192, 168, 2, 254, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4 },
                connectionSubState = RasConnectionSubState.None
#endif
            };

            var target = new CopyStructToAddrMock<NativeMethods.RASCONNSTATUS>();
            target.Result = expected;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetConnectStatus(It.IsAny<RasHandle>(), It.IsAny<IntPtr>())).Callback((RasHandle handle, IntPtr connectionStatus) =>
                {
                    target.Execute(connectionStatus);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetConnectionStatus(new RasHandle(new IntPtr(1), false));

            Assert.AreEqual(expected.connectionState, actual.ConnectionState);
            Assert.AreEqual(expected.deviceName, actual.Device.Name);
            Assert.IsTrue(string.Equals(expected.deviceType, actual.Device.DeviceType.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(expected.errorCode, actual.ErrorCode);
            Assert.AreEqual(null, actual.ErrorMessage);
            Assert.AreEqual(expected.phoneNumber, actual.PhoneNumber);
#if (WIN7 || WIN8)
            TestUtilities.AssertEndPoint(expected.localEndPoint, actual.LocalEndPoint);
            TestUtilities.AssertEndPoint(expected.remoteEndPoint, actual.RemoteEndPoint);
            Assert.IsTrue(actual.LocalEndPoint.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
            Assert.IsTrue(actual.RemoteEndPoint.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
#endif
        }

        /// <summary>
        /// Tests the GetConnectionStatus method to ensure an InvalidHandleException is thrown for invalid handles.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidHandleException))]
        public void GetConnectionStatusInvalidHandleTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetConnectStatus(It.IsAny<RasHandle>(), It.IsAny<IntPtr>())).Returns(NativeMethods.ERROR_INVALID_HANDLE);

            RasHelper.Instance.GetConnectionStatus(NativeMethods.INVALID_HANDLE_VALUE);           
        }

        /// <summary>
        /// Tests the GetConnectionStatus method to ensure an error message is included when the error code has been set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetConnectionStatusErrorCodeTest()
        {
            var expected = new NativeMethods.RASCONNSTATUS()
            {
                connectionState = RasConnectionState.Authenticate,
                deviceName = "WAN Miniport (PPTP)",
                deviceType = NativeMethods.RASDT_Vpn,
                errorCode = NativeMethods.ERROR_PROTOCOL_NOT_CONFIGURED,
                phoneNumber = IPAddress.Loopback.ToString()
            };

            var target = new CopyStructToAddrMock<NativeMethods.RASCONNSTATUS>();
            target.Result = expected;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetConnectStatus(It.IsAny<RasHandle>(), It.IsAny<IntPtr>())).Callback((RasHandle handle, IntPtr status) =>
                {
                    target.Execute(status);
                }).Returns(NativeMethods.SUCCESS);
            mock.Setup(o => o.GetErrorString(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).Callback((int errorCode, string result, int bufferSize) =>
                {
                    new SafeNativeMethods().GetErrorString(errorCode, result, bufferSize);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetConnectionStatus(new RasHandle(new IntPtr(1), false));

            Assert.AreEqual(expected.connectionState, actual.ConnectionState);
            Assert.AreEqual(expected.deviceName, actual.Device.Name);
            Assert.IsTrue(string.Equals(expected.deviceType, actual.Device.DeviceType.ToString(), StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(expected.errorCode, actual.ErrorCode);
            Assert.AreEqual(expected.phoneNumber, actual.PhoneNumber);
            Assert.IsTrue(!string.IsNullOrEmpty(actual.ErrorMessage));            
        }
        
        /// <summary>
        /// Tests the GetConnectionStatus method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetConnectionStatusUnexpectedResultTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetConnectStatus(It.IsAny<RasHandle>(), It.IsAny<IntPtr>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetConnectionStatus(new RasHandle(new IntPtr(1), false));
        }

        /// <summary>
        /// Tests the GetConnectionStatus method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetConnectionStatusNotSupportedTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetConnectStatus(It.IsAny<RasHandle>(), It.IsAny<IntPtr>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetConnectionStatus(new RasHandle(new IntPtr(1), false));
        }

        #endregion

        #region GetCredentials

        /// <summary>
        /// Tests the GetCredentials method to ensure the credentials are returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentialsTest()
        {
            var expected = new NativeMethods.RASCREDENTIALS()
            {
                userName = "User",
                password = "Password",
                domain = "Domain",
                options = NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain
            };

            var target = new CopyStructToAddrMock<NativeMethods.RASCREDENTIALS>();
            target.Result = expected;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>())).Callback((string phoneBookPath, string entryName, IntPtr credentials) =>
                {
                    target.Execute(credentials);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetCredentials("C:\\Test.pbk", "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);

            Assert.AreEqual(expected.userName, actual.UserName);
            Assert.AreEqual(expected.password, actual.Password);
            Assert.AreEqual(expected.domain, actual.Domain);
        }
        
        /// <summary>
        /// Tests the GetCredentials method to ensure a null reference is resulted when the credentials are not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCredentialsNotFoundTest()
        {           
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>())).Returns(NativeMethods.ERROR_FILE_NOT_FOUND);

            var result = RasHelper.Instance.GetCredentials("C:\\Test.pbk", "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);

            Assert.IsNull(result);
        }
        
        /// <summary>
        /// Tests the GetCredentials method to ensure an ArgumentException is thrown for null a phone book argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCredentialsNullPhoneBookTest()
        {
            RasHelper.Instance.GetCredentials(null, "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

        /// <summary>
        /// Tests the GetCredentials method to ensure an ArgumentException is thrown for an empty phone book argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCredentialsEmptyPhoneBookTest()
        {
            RasHelper.Instance.GetCredentials(string.Empty, "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }
        
        /// <summary>
        /// Tests the GetCredentials method to ensure an ArgumentException is thrown for a null entry name argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCredentialsNullEntryNameTest()
        {
            RasHelper.Instance.GetCredentials("C:\\Test.pbk", null, NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

        /// <summary>
        /// Tests the GetCredentials method to ensure an ArgumentException is thrown for an empty entry name argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCredentialsEmptyEntryNameTest()
        {
            RasHelper.Instance.GetCredentials("C:\\Test.pbk", string.Empty, NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

        /// <summary>
        /// Tests the GetCredentials method to ensure a RasException is thrown when an unexpected result occurs.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetCredentialsUnexpectedResultTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetCredentials("C:\\Test.pbk", "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

        /// <summary>
        /// Tests the GetCredentials method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetCredentialsNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetCredentials("C:\\Test.pbk", "Test Entry", NativeMethods.RASCM.UserName | NativeMethods.RASCM.Password | NativeMethods.RASCM.Domain);
        }

        #endregion

        #region GetCustomAuthData

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure an exception is thrown for null phone book paths.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomAuthDataNullPhoneBookPathTest()
        {
            RasHelper.Instance.GetCustomAuthData(null, "Test Entry");
        }

        /// <summary>
        /// /// Tests the GetCustomAuthData method to ensure an exception is thrown for empty phone book paths.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomAuthDataEmptyPhoneBookPathTest()
        {
            RasHelper.Instance.GetCustomAuthData(string.Empty, "Test Entry");
        }

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure an exception is thrown for null entry names.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomAuthDataNullEntryNameTest()
        {
            RasHelper.Instance.GetCustomAuthData("C:\\Test.pbk", null);
        }

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure an exception is thrown for empty entry names.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCustomAuthDataEmptyEntryNameTest()
        {
            RasHelper.Instance.GetCustomAuthData("C:\\Test.pbk", string.Empty);
        }

        /// <summary>
        /// Tests the GetCustomAuthData method to ensure the proper value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetCustomAuthDataTest()
        {
            var expected = new byte[] { 1, 0, 0, 0, 0, 0, 0, 1, 0 };

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            var target = new BufferedPInvokeMock<RasGetCustomAuthDataParams, byte>();
            target.Result = expected;

            mock.Setup(o => o.GetCustomAuthData(It.Is<RasGetCustomAuthDataParams>(i => i.BufferSize.ToInt32() < expected.Length))).Callback((RasGetCustomAuthDataParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.GetCustomAuthData(It.Is<RasGetCustomAuthDataParams>(i => i.BufferSize.ToInt32() == expected.Length))).Callback((RasGetCustomAuthDataParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetCustomAuthData("C:\\Test.pbk", "Test Entry");

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region GetDevices

        /// <summary>
        /// Tests the GetDevices method to ensure the expected devices are returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetDevicesTest()
        {
            var expected = new NativeMethods.RASDEVINFO[]
            {
                new NativeMethods.RASDEVINFO()
                {
                    name = "WAN Miniport (PPTP)",
                    type = NativeMethods.RASDT_Vpn
                },
                new NativeMethods.RASDEVINFO()
                {
                    name = "WAN Miniport (L2TP)",
                    type = NativeMethods.RASDT_Vpn
                },
                new NativeMethods.RASDEVINFO() 
                {
                    name = "POTS Modem",
                    type = NativeMethods.RASDT_Modem
                }
            };

            var expectedSize = new IntPtr(Marshal.SizeOf(typeof(NativeMethods.RASDEVINFO)) * expected.Length);

            var target = new StructBufferedPInvokeMock<StructBufferedPInvokeParams, NativeMethods.RASDEVINFO>();
            target.Result = expected;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumDevices(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize != expectedSize))).Callback((StructBufferedPInvokeParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.EnumDevices(It.Is<StructBufferedPInvokeParams>(i => i.BufferSize == expectedSize))).Callback((StructBufferedPInvokeParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetDevices();

            Assert.AreEqual(expected.Length, actual.Count);

            for (var index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(expected[index].name, actual[index].Name);
                Assert.IsTrue(string.Equals(expected[index].type, actual[index].DeviceType.ToString(), StringComparison.CurrentCultureIgnoreCase));
            }
        }

        /// <summary>
        /// Tests the GetDevices method to ensure an empty collection is returned when no devices are found on the system.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetDevicesWithNoDevicesTest()
        {
            var expected = 0;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumDevices(It.IsAny<StructBufferedPInvokeParams>())).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetDevices();

            Assert.AreEqual(expected, actual.Count);
        }

        /// <summary>
        /// Tests the GetDevices method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetDevicesUnexpectedResultTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumDevices(It.IsAny<StructBufferedPInvokeParams>())).Returns(NativeMethods.ERROR_INVALID_PARAMETER);

            RasHelper.Instance.GetDevices();
        }

        /// <summary>
        /// Tests the GetDevices method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetDevicesNotSupportedTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.EnumDevices(It.IsAny<StructBufferedPInvokeParams>())).Throws<EntryPointNotFoundException>();

            RasHelper.Instance.GetDevices();
        }

        #endregion

        #region GetEapUserData

        /// <summary>
        /// Tests the GetEapUserData method to ensure the proper value is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetEapUserDataTest()
        {
            var expected = new byte[] { 0, 1, 2, 3, 4, 5 };

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            var target = new BufferedPInvokeMock<RasGetEapUserDataParams, byte>();
            target.Result = expected;

            mock.Setup(o => o.GetEapUserData(It.Is<RasGetEapUserDataParams>(i => i.BufferSize.ToInt32() < expected.Length))).Callback((RasGetEapUserDataParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.ERROR_BUFFER_TOO_SMALL);
            mock.Setup(o => o.GetEapUserData(It.Is<RasGetEapUserDataParams>(i => i.BufferSize.ToInt32() == expected.Length))).Callback((RasGetEapUserDataParams value) =>
                {
                    target.Execute(value);
                }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry");

            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure a null result is returned when the output buffer is empty.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetEapUserDataNullResultTest()
        {
            byte[] expected = null;

            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetEapUserData(It.IsAny<RasGetEapUserDataParams>())).Callback((RasGetEapUserDataParams value) =>
            {
                value.BufferSize = IntPtr.Zero;
            }).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.GetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry");

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure a RasException is thrown when an unexpected result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void GetEapUserDataRasExceptionTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetEapUserData(It.IsAny<RasGetEapUserDataParams>())).Returns(NativeMethods.ERROR_CANNOT_FIND_PHONEBOOK_ENTRY);

            RasHelper.Instance.GetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry");
        }

        /// <summary>
        /// Tests the GetEapUserData method to ensure a NotSupportedException is thown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetEapUserDataNotSupportedExceptionTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.GetEapUserData(It.IsAny<RasGetEapUserDataParams>())).Throws(new EntryPointNotFoundException());

            RasHelper.Instance.GetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry");
        }

        #endregion

        #region HangUp

        /// <summary>
        /// Tests the HangUp method to ensure a <see cref="ArgumentNullException"/> is thrown when the handle is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HangUpNullHandleTest()
        {
            RasHelper.Instance.HangUp(null, 0, true);
        }

        /// <summary>
        /// Tests the HangUp method to ensure an <see cref="ArgumentOutOfRangeException"/> is thrown when the polling interval is negative.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HangUpNegativePollingIntervalTest()
        {
            RasHelper.Instance.HangUp(new RasHandle(new IntPtr(1), false), -1, true);
        }

        /// <summary>
        /// Tests the HangUp method to ensure an <see cref="InvalidHandleException"/> is thrown when an invalid handle is used.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(InvalidHandleException))]
        public void HangUpInvalidHandleTest()
        {
            var mock = new Mock<ISafeNativeMethods>();
            mock.Setup(o => o.HangUp(It.IsAny<RasHandle>())).Returns(NativeMethods.ERROR_INVALID_HANDLE);

            SafeNativeMethods.Instance = mock.Object;

            RasHelper.Instance.HangUp(new RasHandle(new IntPtr(1), false), 0, true);
        }

        /// <summary>
        /// Tests the HangUp method to ensure no exception is thrown when the HangUp result is ERROR_NO_CONNECTION.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpNoConnectionErrorTest()
        {
            var safeNativeMethods = new Mock<ISafeNativeMethods>();
            safeNativeMethods.Setup(o => o.HangUp(It.IsAny<RasHandle>())).Returns(NativeMethods.ERROR_NO_CONNECTION);

            SafeNativeMethods.Instance = safeNativeMethods.Object;

            var rasHelper = new Mock<IRasHelper>();
            rasHelper.Setup(o => o.IsConnectionActive(It.IsAny<RasHandle>())).Returns(false);

            RasHelper.Instance = rasHelper.Object;

            var target = new RasHelper();
            target.HangUp(new RasHandle(new IntPtr(1), false), 0, true);
        }

        #endregion

        #region SetCredentials

        /// <summary>
        /// Tests the SetCredentials method to ensure an ArgumentException is thrown for a null phone book argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCredentialsNullPhoneBookTest()
        {
            RasHelper.Instance.SetCredentials(null, "Test Entry", new NativeMethods.RASCREDENTIALS(), true);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure an ArgumentException is thrown for an empty phone book argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCredentialsEmptyPhoneBookTest()
        {
            RasHelper.Instance.SetCredentials(string.Empty, "Test Entry", new NativeMethods.RASCREDENTIALS(), true);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure an ArgumentException is thrown for a null entry name argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCredentialsNullEntryNameTest()
        {
            RasHelper.Instance.SetCredentials("C:\\Test.pbk", null, new NativeMethods.RASCREDENTIALS(), true);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure an ArgumentException is thrown for an empty entry name argument.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCredentialsEmptyEntryNameTest()
        {
            RasHelper.Instance.SetCredentials("C:\\Test.pbk", string.Empty, new NativeMethods.RASCREDENTIALS(), true);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure an UnauthorizedAccessException is thrown when ERROR_ACCESS_DENIED is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void SetCredentialsUnauthorizedExceptionTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<bool>())).Returns(NativeMethods.ERROR_ACCESS_DENIED);

            var credentials = new NativeMethods.RASCREDENTIALS()
            {
                userName = "User",
                password = "Password",
                domain = "Domain"
            };

            var actual = RasHelper.Instance.SetCredentials("C:\\Test.pbk", "Test Entry", credentials, false);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure the credentials are updated as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SetCredentialsTest()
        {
            var expected = true;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<bool>())).Returns(NativeMethods.SUCCESS);

            var credentials = new NativeMethods.RASCREDENTIALS()
            {
                userName = "User",
                password = "Password",
                domain = "Domain"
            };

            var actual = RasHelper.Instance.SetCredentials("C:\\Test.pbk", "Test Entry", credentials, false);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure a RasException is thrown when an unexpected result occurs.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void SetCredentialsUnexpectedResultTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<bool>())).Returns(NativeMethods.ERROR_CANNOT_FIND_PHONEBOOK_ENTRY);

            var credentials = new NativeMethods.RASCREDENTIALS()
            {
                userName = "User",
                password = "Password",
                domain = "Domain"
            };

            RasHelper.Instance.SetCredentials("C:\\Test.pbk", "Test Entry", credentials, false);
        }

        /// <summary>
        /// Tests the SetCredentials method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetCredentialsNotSupportedTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCredentials(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<bool>())).Throws<EntryPointNotFoundException>();

            var credentials = new NativeMethods.RASCREDENTIALS()
            {
                userName = "User",
                password = "Password",
                domain = "Domain"
            };

            RasHelper.Instance.SetCredentials("C:\\Test.pbk", "Test Entry", credentials, false);
        }

        #endregion

        #region SetCustomAuthData

        /// <summary>
        /// Tests the SetCustomAuthDataTest method to ensure the expected value is returned for a proper call.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SetCustomAuthDataTest()
        {
            var expected = true;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCustomAuthData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.SetCustomAuthData("C:\\Test.pbk", "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SetEapUserData method to ensure a RasException is thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void SetCustomAuthDataRasExceptionTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetCustomAuthData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(NativeMethods.ERROR_CANNOT_FIND_PHONEBOOK_ENTRY);

            RasHelper.Instance.SetCustomAuthData("C:\\Test.pbk", "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// Tests the SetCustomAuthData to ensure an exception is thrown for a null phone book path.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCustomAuthDataNullPhoneBookTest()
        {
            RasHelper.Instance.SetCustomAuthData(null, "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// Tests the SetCustomAuthData to ensure an exception is thrown for an empty phone book path.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCustomAuthDataEmptyPhoneBookTest()
        {
            RasHelper.Instance.SetCustomAuthData(string.Empty, "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// Tests the SetCustomAuthData to ensure an exception is thrown for a null entry name.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCustomAuthDataNullEntryNameTest()
        {
            RasHelper.Instance.SetCustomAuthData("C:\\Test.pbk", null, new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// Tests the SetCustomAuthData to ensure an exception is thrown for an empty entry name.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetCustomAuthDataEmptyEntryNameTest()
        {
            RasHelper.Instance.SetCustomAuthData("C:\\Test.pbk", string.Empty, new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        #endregion

        #region SetEapUserData

        /// <summary>
        /// Tests the SetEapUserData method to ensure the expected value is returned for a proper call.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SetEapUserDataTest()
        {
            var expected = true;

            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(NativeMethods.SUCCESS);

            var actual = RasHelper.Instance.SetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SetEapUserData method to ensure a RasException is thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(RasException))]
        public void SetEapUserDataRasExceptionTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Returns(NativeMethods.ERROR_CANNOT_FIND_PHONEBOOK_ENTRY);

            RasHelper.Instance.SetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        /// <summary>
        /// Tests the SetEapUserData method to ensure a NotSupportedException is thrown when the entry point is not found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetEapUserDataNotSupportedExceptionTest()
        {
            var mock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = mock.Object;

            mock.Setup(o => o.SetEapUserData(It.IsAny<IntPtr>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IntPtr>(), It.IsAny<int>())).Throws(new EntryPointNotFoundException());

            RasHelper.Instance.SetEapUserData(IntPtr.Zero, "C:\\Test.pbk", "Test Entry", new byte[] { 0, 1, 2, 3, 4, 5 });
        }

        #endregion

        #region SetEntryProperties

        /// <summary>
        /// Tests the SetEntryProperties method to ensure a null phone book causes an ArgumentNullException to be thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetEntryPropertiesNullPhoneBookArgumentNullExceptionTest()
        {
            RasPhoneBook pbk = null;
            var entry = new RasEntry("Test Entry");

            RasHelper.Instance.SetEntryProperties(pbk, entry);
        }

        /// <summary>
        /// Tests the SetEntryProperties method to ensure a null entry will cause an ArgumentNullException to be thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetEntryPropertiesNullEntryArgumentNullExceptionTest()
        {
            var pbk = new RasPhoneBook();
            RasEntry entry = null;

            RasHelper.Instance.SetEntryProperties(pbk, entry);
        }

        /// <summary>
        /// Tests the SetEntryProperties method to ensure a null phone number on an entry will cause an ArgumentException to be thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void SetEntryPropertiesNullPhoneNumberArgumentExceptionTest()
        {
            var unsafeMock = new Mock<IUnsafeNativeMethods>();
            UnsafeNativeMethods.Instance = unsafeMock.Object;

            var safeMock = new Mock<ISafeNativeMethods>();
            SafeNativeMethods.Instance = safeMock.Object;

            safeMock.Setup(o => o.ValidateEntryName(It.IsAny<string>(), It.IsAny<string>())).Returns(NativeMethods.SUCCESS);

            // Configure the entry so it works correctly, only leaving the phone number null for the test to fail.
            var pbk = new RasPhoneBook();

            var entry = new RasEntry("Test Entry");
            entry.Owner = pbk;
            entry.PhoneNumber = null;
            entry.Device = RasDevice.Create("WAN Miniport (PPTP)", RasDeviceType.Vpn);
            entry.FramingProtocol = RasFramingProtocol.Ppp;
            entry.EntryType = RasEntryType.Vpn;

            RasHelper.Instance.SetEntryProperties(pbk, entry);
        }

        #endregion

        #region Test Methods Init

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #endregion
    }
}