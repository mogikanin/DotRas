//--------------------------------------------------------------------------
// <copyright file="RasCcpInfoTest.cs" company="Jeff Winn">
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
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasCcpInfo"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasCcpInfoTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasCcpInfoTest"/> class.
        /// </summary>
        public RasCcpInfoTest()
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
        /// Tests the ServerOptions property to ensure it matches what was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ServerOptionsTest()
        {
            var errorCode = int.MaxValue;
            var compressionAlgorithm = RasCompressionType.Mppc;
            var options = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var serverCompressionAlgorithm = RasCompressionType.Stac;
            var expected = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, compressionAlgorithm, options, serverCompressionAlgorithm, expected);

            RasCompressionOptions actual;
            actual = target.ServerOptions;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the ServerCompressionAlgorithm property to ensure it matches what was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ServerCompressionAlgorithmTest()
        {
            var errorCode = int.MaxValue;
            var compressionAlgorithm = RasCompressionType.Mppc;
            var options = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var expected = RasCompressionType.Stac;
            var serverOptions = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, compressionAlgorithm, options, expected, serverOptions);

            RasCompressionType actual;
            actual = target.ServerCompressionAlgorithm;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Options property to ensure it matches what was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsTest()
        {
            var errorCode = int.MaxValue;
            var compressionAlgorithm = RasCompressionType.Mppc;
            var expected = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var serverCompressionAlgorithm = RasCompressionType.Stac;
            var serverOptions = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, compressionAlgorithm, expected, serverCompressionAlgorithm, serverOptions);

            RasCompressionOptions actual;
            actual = target.Options;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the ErrorCode property to ensure it matches what was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ErrorCodeTest()
        {
            var errorCode = int.MaxValue;
            var compressionAlgorithm = RasCompressionType.Mppc;
            var options = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var serverCompressionAlgorithm = RasCompressionType.Stac;
            var serverOptions = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, compressionAlgorithm, options, serverCompressionAlgorithm, serverOptions);

            int actual;
            actual = target.ErrorCode;

            Assert.AreEqual(errorCode, actual);
        }

        /// <summary>
        /// Tests the CompressionAlgorithm property to ensure it matches what was passed into the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CompressionAlgorithmTest()
        {
            var errorCode = int.MaxValue;
            var expected = RasCompressionType.Mppc;
            var options = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var serverCompressionAlgorithm = RasCompressionType.Stac;
            var serverOptions = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, expected, options, serverCompressionAlgorithm, serverOptions);

            RasCompressionType actual;
            actual = target.CompressionAlgorithm;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the RasCcpInfo constructor to ensure no exceptions are thrown.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RasCcpInfoConstructorTest()
        {
            var errorCode = int.MaxValue;
            var compressionAlgorithm = RasCompressionType.Mppc;
            var options = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);
            var serverCompressionAlgorithm = RasCompressionType.Stac;
            var serverOptions = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            var target = new RasCcpInfo(errorCode, compressionAlgorithm, options, serverCompressionAlgorithm, serverOptions);

            Assert.IsNotNull(target);
        }

        #endregion
    }
}