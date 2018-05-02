//--------------------------------------------------------------------------
// <copyright file="RasConnectionTest.cs" company="Jeff Winn">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net;
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasConnection"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasConnectionTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasConnectionTest"/> class.
        /// </summary>
        public RasConnectionTest()
        {
        }

        #endregion

        #region Methods

        #region ~ Test Methods Init

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        #region Handle

        /// <summary>
        /// Tests the Handle property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HandleTest()
        {
            RasHandle expected = new RasHandle(new IntPtr(1), false);

            RasConnection target = new RasConnection();
            target.Handle = expected;

            RasHandle actual = target.Handle;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region EntryName

        /// <summary>
        /// Tests the EntryName property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryNameTest()
        {
            string expected = "Entry Name";

            RasConnection target = new RasConnection();
            target.EntryName = expected;

            string actual = target.EntryName;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Device

        /// <summary>
        /// Tests the Device property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DeviceTest()
        {
            RasDevice expected = RasDevice.Create("PPTP", RasDeviceType.Vpn);

            RasConnection target = new RasConnection();
            target.Device = expected;

            RasDevice actual = target.Device;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region PhoneBookPath

        /// <summary>
        /// Tests the PhoneBookPath property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneBookPathTest()
        {
            string expected = "C:\\Test.pbk";

            RasConnection target = new RasConnection();
            target.PhoneBookPath = expected;

            string actual = target.PhoneBookPath;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region SubEntryId

        /// <summary>
        /// Tests the SubEntryId property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SubEntryIdTest()
        {
            int expected = 2;

            RasConnection target = new RasConnection();
            target.SubEntryId = expected;

            int actual = target.SubEntryId;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region EntryId

        /// <summary>
        /// Tests the EntryId property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryIdTest()
        {
            Guid expected = Guid.NewGuid();

            RasConnection target = new RasConnection();
            target.EntryId = expected;

            Guid actual = target.EntryId;

            Assert.AreEqual(expected, actual);
        }

        #endregion

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        #region ConnectionOptions

        /// <summary>
        /// Tests the ConnectionOptions property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConnectionOptionsTest()
        {
            RasConnectionOptions expected = new RasConnectionOptions()
            {
                AllUsers = true
            };

            RasConnection target = new RasConnection();
            target.ConnectionOptions = expected;

            RasConnectionOptions actual = target.ConnectionOptions;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the ConnectionOptions property to ensure the property always returns an instance of the object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConnectionOptionsNullTest()
        {
            RasConnection target = new RasConnection();
            target.ConnectionOptions = null;

            RasConnectionOptions actual = target.ConnectionOptions;

            Assert.IsNotNull(actual);
        }

        #endregion

        #region SessionId

        /// <summary>
        /// Tests the SessionId property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SessionIdTest()
        {
            Luid expected = Luid.NewLuid();

            RasConnection target = new RasConnection();
            target.SessionId = expected;

            Luid actual = target.SessionId;

            Assert.AreEqual(expected, actual);
        }

        #endregion

#endif
#if (WIN2K8 || WIN7 || WIN8)

        #region CorrelationId

        /// <summary>
        /// Tests the CorrelationId property to ensure the same value that was set to the property is the value that was returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CorrelationIdTest()
        {
            Guid expected = Guid.NewGuid();

            RasConnection target = new RasConnection();
            target.CorrelationId = expected;

            Guid actual = target.CorrelationId;

            Assert.AreEqual(expected, actual);
        }

        #endregion

#endif

        #region GetActiveConnections

        /// <summary>
        /// Tests the GetActiveConnections method to ensure the expected collection of connections is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetActiveConnectionsTest()
        {
            ReadOnlyCollection<RasConnection> expected = new ReadOnlyCollection<RasConnection>(
                new RasConnection[]
                {
                    new RasConnection()
                    {
                        EntryId = Guid.NewGuid(),
                        EntryName = "Test",
                        PhoneBookPath = "C:\\Test.pbk"
                    }
                });

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetActiveConnections()).Returns(expected);

            ReadOnlyCollection<RasConnection> actual = RasConnection.GetActiveConnections();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region GetConnectionStatus

        /// <summary>
        /// Tests the GetConnectionStatus method to ensure the expected connection status information is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetConnectionStatusTest()
        {
            RasConnectionStatus expected = new RasConnectionStatus()
            {
                ConnectionState = RasConnectionState.Connected
            };

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetConnectionStatus(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasConnectionStatus actual = target.GetConnectionStatus();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region GetConnectionStatistics

        /// <summary>
        /// Tests the GetConnectionStatistics method to ensure the expected statistics are returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetConnectionStatisticsTest()
        {
            RasLinkStatistics expected = new RasLinkStatistics(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, TimeSpan.MinValue);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetConnectionStatistics(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasLinkStatistics actual = target.GetConnectionStatistics();

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region ClearConnectionStatistics

        /// <summary>
        /// Tests the ClearConnectionStatistics method to ensure the statistics can be cleared.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearConnectionStatisticsTest()
        {
            bool expected = true;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.ClearConnectionStatistics(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            bool actual = target.ClearConnectionStatistics();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region ClearLinkStatistics

        /// <summary>
        /// Tests the ClearLinkStatistics method to ensure the statistics can be cleared.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ClearLinkStatisticsTest()
        {
            bool expected = true;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.ClearLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(expected);

            RasConnection target = new RasConnection();
            bool actual = target.ClearLinkStatistics();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetLinkStatistics

        /// <summary>
        /// Tests the GetLinkStatistics method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetLinkStatisticsTest()
        {
            RasLinkStatistics expected = new RasLinkStatistics(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, TimeSpan.MinValue);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetLinkStatistics(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasLinkStatistics actual = target.GetLinkStatistics();

            Assert.AreSame(expected, actual);
        }

        #endregion

#if (WIN2K8 || WIN7 || WIN8)

        #region GetNapStatus

        /// <summary>
        /// Tests the GetNapStatus method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetNapStatusTest()
        {
            RasNapStatus expected = new RasNapStatus(RasIsolationState.Unknown, DateTime.MinValue);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetNapStatus(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasNapStatus actual = target.GetNapStatus();

            Assert.AreSame(expected, actual);
        }

        #endregion

#endif

        #region GetProjectionInfo

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoAmbTest()
        {
            RasAmbInfo expected = new RasAmbInfo(0, null, 0);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Amb)).Returns(expected);

            RasConnection target = new RasConnection();
            RasAmbInfo actual = (RasAmbInfo)target.GetProjectionInfo(RasProjectionType.Amb);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoNbfTest()
        {
            RasNbfInfo expected = new RasNbfInfo(0, 0, null, null, 0);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Nbf)).Returns(expected);

            RasConnection target = new RasConnection();
            RasNbfInfo actual = (RasNbfInfo)target.GetProjectionInfo(RasProjectionType.Nbf);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoIpxTest()
        {
            RasIpxInfo expected = new RasIpxInfo(0, IPAddress.Any);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Ipx)).Returns(expected);

            RasConnection target = new RasConnection();
            RasIpxInfo actual = (RasIpxInfo)target.GetProjectionInfo(RasProjectionType.Ipx);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoIPTest()
        {
            RasIPInfo expected = new RasIPInfo(0, IPAddress.Any, IPAddress.Any, new RasIPOptions(NativeMethods.RASIPO.None), new RasIPOptions(NativeMethods.RASIPO.None));

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.IP)).Returns(expected);

            RasConnection target = new RasConnection();
            RasIPInfo actual = (RasIPInfo)target.GetProjectionInfo(RasProjectionType.IP);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoCcpTest()
        {
            RasCcpInfo expected = new RasCcpInfo(
                0, 
                RasCompressionType.None, 
                new RasCompressionOptions(NativeMethods.RASCCPO.None),
                RasCompressionType.None, 
                new RasCompressionOptions(NativeMethods.RASCCPO.None));

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Ccp)).Returns(expected);

            RasConnection target = new RasConnection();
            RasCcpInfo actual = (RasCcpInfo)target.GetProjectionInfo(RasProjectionType.Ccp);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoLcpTest()
        {
            RasLcpInfo expected = new RasLcpInfo(false, 0, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, 0, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, 0, false, 0, 0, null, new RasLcpOptions(), new RasLcpOptions());

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Lcp)).Returns(expected);

            RasConnection target = new RasConnection();
            RasLcpInfo actual = (RasLcpInfo)target.GetProjectionInfo(RasProjectionType.Lcp);

            Assert.AreSame(expected, actual);
        }

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoIPv6Test()
        {
            RasIPv6Info expected = new RasIPv6Info(0, 0, 0, 0, 0);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.IPv6)).Returns(expected);

            RasConnection target = new RasConnection();
            RasIPv6Info actual = (RasIPv6Info)target.GetProjectionInfo(RasProjectionType.IPv6);

            Assert.AreSame(expected, actual);
        }

#endif

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoSlipTest()
        {
            RasSlipInfo expected = new RasSlipInfo(0, IPAddress.Any);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfo(It.IsAny<RasHandle>(), NativeMethods.RASPROJECTION.Slip)).Returns(expected);

            RasConnection target = new RasConnection();
            RasSlipInfo actual = (RasSlipInfo)target.GetProjectionInfo(RasProjectionType.Slip);

            Assert.AreSame(expected, actual);
        }

#if (WIN7 || WIN8)

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure a null reference is returned for requests whose API return value is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoNullApiResultTest()
        {
            RasPppInfo expected = null;

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfoEx(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasPppInfo actual = (RasPppInfo)target.GetProjectionInfo(RasProjectionType.Ppp);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure a null reference is returned whose returning value from the API is for IKEv2.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoPppNullResultTest()
        {
            RasPppInfo expected = null;
            RasIkeV2Info result = new RasIkeV2Info(0, IPAddress.Any, IPAddress.Any, 0, IPAddress.Any, IPAddress.Any, 0, RasIkeV2AuthenticationType.None, 0, new RasIkeV2Options(), RasIPSecEncryptionType.None, null, null);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfoEx(It.IsAny<RasHandle>())).Returns(result);

            RasConnection target = new RasConnection();
            RasPppInfo actual = (RasPppInfo)target.GetProjectionInfo(RasProjectionType.Ppp);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure a null reference is returned whose returning value from the API is for PPP.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoIkeV2NullResultTest()
        {
            RasIkeV2Info expected = null;
            RasPppInfo result = new RasPppInfo(0, IPAddress.Any, IPAddress.Any, new RasIPOptions(NativeMethods.RASIPO.None), new RasIPOptions(NativeMethods.RASIPO.None), 0, null, null, false, false, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, 0, 0, new RasLcpOptions(), new RasLcpOptions(), RasCompressionType.None, RasCompressionType.None, new RasCompressionOptions(), new RasCompressionOptions());

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfoEx(It.IsAny<RasHandle>())).Returns(result);

            RasConnection target = new RasConnection();
            RasIkeV2Info actual = (RasIkeV2Info)target.GetProjectionInfo(RasProjectionType.IkeV2);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoPppTest()
        {
            RasPppInfo expected = new RasPppInfo(0, IPAddress.Any, IPAddress.Any, new RasIPOptions(NativeMethods.RASIPO.None), new RasIPOptions(NativeMethods.RASIPO.None), 0, null, null, false, false, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, RasLcpAuthenticationType.None, RasLcpAuthenticationDataType.None, 0, 0, new RasLcpOptions(), new RasLcpOptions(), RasCompressionType.None, RasCompressionType.None, new RasCompressionOptions(), new RasCompressionOptions());

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfoEx(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasPppInfo actual = (RasPppInfo)target.GetProjectionInfo(RasProjectionType.Ppp);

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the GetProjectionInfo method to ensure data is returned successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetProjectionInfoIkeV2Test()
        {
            RasIkeV2Info expected = new RasIkeV2Info(0, IPAddress.Any, IPAddress.Any, 0, IPAddress.Any, IPAddress.Any, 0, RasIkeV2AuthenticationType.None, 0, new RasIkeV2Options(), RasIPSecEncryptionType.None, null, null);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetProjectionInfoEx(It.IsAny<RasHandle>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasIkeV2Info actual = (RasIkeV2Info)target.GetProjectionInfo(RasProjectionType.IkeV2);

            Assert.AreSame(expected, actual);
        }

#endif

        #endregion

        #region GetSubEntryHandle

        /// <summary>
        /// Tests the GetSubEntryHandle method to ensure an ArgumentException is thrown when the id is zero.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSubEntryHandleArgumentExceptionForZeroTest()
        {
            RasConnection target = new RasConnection();
            target.GetSubEntryHandle(0);
        }

        /// <summary>
        /// Tests the GetSubEntryHandle method to ensure an ArgumentException is thrown when the id is negative.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSubEntryHandleArgumentExceptionForNegativeTest()
        {
            RasConnection target = new RasConnection();
            target.GetSubEntryHandle(-1);
        }

        /// <summary>
        /// Tests the GetSubEntryHandle method to ensure a result is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetSubEntryHandleTest()
        {
            RasHandle expected = new RasHandle(new IntPtr(1), true);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.GetSubEntryHandle(It.IsAny<RasHandle>(), It.IsAny<int>())).Returns(expected);

            RasConnection target = new RasConnection();
            RasHandle actual = target.GetSubEntryHandle(1);

            Assert.AreSame(expected, actual);
        }

        #endregion

        #region HangUp

        /// <summary>
        /// Tests the HangUp method to ensure it was called successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HangUpTest()
        {
            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.HangUp(It.IsAny<RasHandle>(), It.IsAny<int>(), It.IsAny<bool>())).Verifiable();

            RasConnection target = new RasConnection();
            target.HangUp();

            mock.Verify();
        }

        /// <summary>
        /// Tests the HangUp method to ensure an ArgumentOutOfRangeException is thrown when the pollingInterval argument is invalid.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HangUpInvalidPollingIntervalTest()
        {
            RasConnection target = new RasConnection();
            target.HangUp(-1);
        }

        #endregion

#if (WIN7 || WIN8)

        #region UpdateConnection

        /// <summary>
        /// Tests the UpdateConnection method to ensure it was called successfully.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void UpdateConnectionTest()
        {
            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.UpdateConnection(It.IsAny<RasHandle>(), It.IsAny<int>(), It.IsAny<IPAddress>(), It.IsAny<IPAddress>())).Verifiable();

            RasConnection target = new RasConnection();
            target.UpdateConnection(0, IPAddress.Any, IPAddress.Any);

            mock.Verify();
        }

        #endregion

#endif

        #endregion
    }
}