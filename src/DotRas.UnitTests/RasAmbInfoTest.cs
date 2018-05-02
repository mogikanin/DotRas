//--------------------------------------------------------------------------
// <copyright file="RasAmbInfoTest.cs" company="Jeff Winn">
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
    using DotRas;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasAmbInfo"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasAmbInfoTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasAmbInfoTest"/> class.
        /// </summary>
        public RasAmbInfoTest()
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
        /// Tests the NetBiosErrorMessage property to ensure the value returned is the same as was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NetBiosErrorMessageTest()
        {
            int errorCode = 0;
            string netBiosErrorMessage = "This is a test message.";
            byte lana = 0;

            RasAmbInfo target = new RasAmbInfo(errorCode, netBiosErrorMessage, lana);
            string actual = target.NetBiosErrorMessage;

            Assert.AreEqual(netBiosErrorMessage, actual);
        }

        /// <summary>
        /// Tests the Lana property to ensure the value returned is the same as was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void LanaTest()
        {
            int errorCode = 0;
            string netBiosErrorMessage = string.Empty;
            byte lana = 100;

            RasAmbInfo target = new RasAmbInfo(errorCode, netBiosErrorMessage, lana);
            byte actual = target.Lana;

            Assert.AreEqual(lana, actual);
        }

        /// <summary>
        /// Tests the ErrorCode property to ensure the value returned is the same as was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ErrorCodeTest()
        {
            int errorCode = 100;
            string netBiosErrorMessage = string.Empty;
            byte lana = 0;

            RasAmbInfo target = new RasAmbInfo(errorCode, netBiosErrorMessage, lana);
            int actual = target.ErrorCode;

            Assert.AreEqual(errorCode, actual);
        }

        /// <summary>
        /// Tests the constructor to ensure no exceptions are thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RasAmbInfoConstructorTest()
        {
            int errorCode = 0;
            string netBiosErrorMessage = string.Empty;
            byte lana = 0;
           
            RasAmbInfo target = new RasAmbInfo(errorCode, netBiosErrorMessage, lana);

            Assert.AreEqual(errorCode, target.ErrorCode);
            Assert.AreEqual(netBiosErrorMessage, target.NetBiosErrorMessage);
            Assert.AreEqual(lana, target.Lana);
        }

        #endregion
    }
}