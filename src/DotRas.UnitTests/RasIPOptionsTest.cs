//--------------------------------------------------------------------------
// <copyright file="RasIPOptionsTest.cs" company="Jeff Winn">
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
    /// This is a test class for <see cref="DotRas.RasIPOptions"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasIPOptionsTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasIPOptionsTest"/> class.
        /// </summary>
        public RasIPOptionsTest()
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
        /// Tests the VJ property to ensure the value returned is the same as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void VJTest()
        {
            var expected = true;

            var target = new RasIPOptions(Internal.NativeMethods.RASIPO.VJ);
            var actual = target.VJ;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #endregion
    }
}