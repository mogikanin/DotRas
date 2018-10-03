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
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Pfc);
            var actual = target.Pfc;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Acfc property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AcfcTest()
        {
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Acfc);
            var actual = target.Acfc;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Sshf property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SshfTest()
        {
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Sshf);
            var actual = target.Sshf;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Des56 property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Des56Test()
        {
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Des56);
            var actual = target.Des56;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the TripleDes property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TripleDesTest()
        {
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.TripleDes);
            var actual = target.TripleDes;

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
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Aes128);
            var actual = target.Aes128;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Aes256 property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Aes256Test()
        {
            var expected = true;

            var target = new RasLcpOptions(Internal.NativeMethods.RASLCPO.Aes256);
            var actual = target.Aes256;

            Assert.AreEqual(expected, actual);
        }

#endif

        #endregion

        #endregion
    }
}