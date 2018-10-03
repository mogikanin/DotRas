//--------------------------------------------------------------------------
// <copyright file="RasLcpOptionsTest.cs" company="Jeff Winn">
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
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasLcpOptions"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public sealed class RasLcpOptionsTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasLcpOptionsTest"/> class.
        /// </summary>
        public RasLcpOptionsTest()
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

        #region Property Tests

        /// <summary>
        /// Tests the Pfc property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PfcTest()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Pfc);
            bool actual = target.Pfc;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Acfc property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AcfcTest()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Acfc);
            bool actual = target.Acfc;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Sshf property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SshfTest()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Sshf);
            bool actual = target.Sshf;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Des56 property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Des56Test()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Des56);
            bool actual = target.Des56;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TripleDes property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TripleDesTest()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.TripleDes);
            bool actual = target.TripleDes;

            Assert.AreEqual(expected, actual);
        }

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the Aes128 property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Aes128Test()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Aes128);
            bool actual = target.Aes128;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Aes256 property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Aes256Test()
        {
            bool expected = true;

            RasLcpOptions target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Aes256);
            bool actual = target.Aes256;

            Assert.AreEqual(expected, actual);
        }

#endif

        #endregion

        #endregion
    }
}