//--------------------------------------------------------------------------
// <copyright file="LuidTest.cs" company="Jeff Winn">
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

namespace DotRas.IntegrationTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.Luid"/> and is intended to contain all associated integration tests.
    /// </summary>
    [TestClass]
    public class LuidTest : IntegrationTest
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

        #region NewLuid

        /// <summary>
        /// Tests the NewLuid method to ensure a new Luid is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void NewLuid()
        {
            var actual = Luid.NewLuid();

            Assert.AreNotEqual(Luid.Empty, actual);
        }

        #endregion

        #endregion
    }
}