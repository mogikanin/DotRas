//--------------------------------------------------------------------------
// <copyright file="RasCompressionOptionsTest.cs" company="Jeff Winn">
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
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasCompressionOptions"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasCompressionOptionsTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasCompressionOptionsTest"/> class.
        /// </summary>
        public RasCompressionOptionsTest()
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

        #region Constructor

        /// <summary>
        /// Tests the flag properties to ensure they are set if the flags are passed to the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void MultipleFlagsConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.HistoryLess | NativeMethods.RASCCPO.Encryption128Bit);

            Assert.IsFalse(target.CompressionOnly);
            Assert.IsTrue(target.HistoryLess);
            Assert.IsFalse(target.Encryption56Bit);
            Assert.IsFalse(target.Encryption40Bit);
            Assert.IsTrue(target.Encryption128Bit);
        }
        
        /// <summary>
        /// Tests the CompressionOnly property to ensure if the flag is passed to the constructor it is set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CompressionOnlyConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.CompressionOnly);

            Assert.IsTrue(target.CompressionOnly);
            Assert.IsFalse(target.HistoryLess);
            Assert.IsFalse(target.Encryption56Bit);
            Assert.IsFalse(target.Encryption40Bit);
            Assert.IsFalse(target.Encryption128Bit);
        }

        /// <summary>
        /// Tests the HistoryLess property to ensure if the flag is passed to the constructor it is set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void HistoryLessConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.HistoryLess);

            Assert.IsFalse(target.CompressionOnly);
            Assert.IsTrue(target.HistoryLess);
            Assert.IsFalse(target.Encryption56Bit);
            Assert.IsFalse(target.Encryption40Bit);
            Assert.IsFalse(target.Encryption128Bit);
        }

        /// <summary>
        /// Tests the Encryption56Bit property to ensure if the flag is passed to the constructor it is set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Encryption56BitConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption56Bit);

            Assert.IsFalse(target.CompressionOnly);
            Assert.IsFalse(target.HistoryLess);
            Assert.IsTrue(target.Encryption56Bit);
            Assert.IsFalse(target.Encryption40Bit);
            Assert.IsFalse(target.Encryption128Bit);
        }

        /// <summary>
        /// Tests the Encryption40Bit property to ensure if the flag is passed to the constructor it is set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Encryption40BitConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption40Bit);

            Assert.IsFalse(target.CompressionOnly);
            Assert.IsFalse(target.HistoryLess);
            Assert.IsFalse(target.Encryption56Bit);
            Assert.IsTrue(target.Encryption40Bit);
            Assert.IsFalse(target.Encryption128Bit);
        }

        /// <summary>
        /// Tests the Encryption40Bit property to ensure if the flag is passed to the constructor it is set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void Encryption128BitConstructorTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.Encryption128Bit);

            Assert.IsFalse(target.CompressionOnly);
            Assert.IsFalse(target.HistoryLess);
            Assert.IsFalse(target.Encryption56Bit);
            Assert.IsFalse(target.Encryption40Bit);
            Assert.IsTrue(target.Encryption128Bit);
        }

        #endregion

        #region Clone

        /// <summary>
        /// Tests the clone method to ensure the result matches the object instance.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CloneTest()
        {
            var target = new RasCompressionOptions(NativeMethods.RASCCPO.CompressionOnly | NativeMethods.RASCCPO.Encryption40Bit | NativeMethods.RASCCPO.HistoryLess);

            var actual = (RasCompressionOptions)target.Clone();

            Assert.AreEqual(target.CompressionOnly, actual.CompressionOnly);
            Assert.AreEqual(target.HistoryLess, actual.HistoryLess);
            Assert.AreEqual(target.Encryption56Bit, actual.Encryption56Bit);
            Assert.AreEqual(target.Encryption40Bit, actual.Encryption40Bit);
            Assert.AreEqual(target.Encryption128Bit, actual.Encryption128Bit);
        }

        #endregion

        #endregion
    }
}