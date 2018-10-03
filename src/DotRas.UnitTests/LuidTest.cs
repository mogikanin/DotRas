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

namespace DotRas.UnitTests
{
    using DotRas;
    using DotRas.Internal;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using DotRas.UnitTests.Constants;

    /// <summary>
    /// This is a test class for <see cref="DotRas.Luid"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class LuidTest : UnitTest
    {
        #region Constructors

        #endregion

        #region Methods

        #region GetHashCode

        /// <summary>
        /// Tests the GetHashCode method.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetHashCodeTest()
        {
            var expected = 200;

            var target = new Luid(100, 100);

            var actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetHashCode method to ensure it returns int.MaxValue when the value exceeds that of the integer type.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetHashCodeTestForMaxValueReturned()
        {
            var expected = int.MaxValue;

            var target = new Luid(int.MaxValue, int.MaxValue);

            var actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region NewLuid

        /// <summary>
        /// Tests the NewLuid method to ensure a new Luid is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NewLuidTest()
        {
            var expected = new Luid(1, 1);

            var mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.AllocateLocallyUniqueId()).Returns(expected);

            var actual = Luid.NewLuid();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Tests the inequality operator.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void InequalityOperatorTest()
        {
            var expected = false;

            var objA = new Luid(1, 1);
            var objB = new Luid(1, 1);

            var actual = objA != objB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the equality operator.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualityOperatorTest()
        {
            var expected = false;

            var objA = new Luid(1, 1);
            var objB = new Luid(1, 2);

            var actual = objA == objB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTest()
        {
            var expected = false;

            var target = new Luid(1, 1);
            var other = new Luid(2, 2);

            var actual = target.Equals(other);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method with a null reference object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTestWithNullObject()
        {
            var expected = false;

            var target = new Luid(1, 1);
            object obj = null;

            var actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method with a Luid that has been boxed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTestWithBoxedLuidObject()
        {
            var expected = true;

            var target = new Luid(1, 1);
            var other = new Luid(1, 1);

            var actual = target.Equals((object)other);

            Assert.AreEqual(expected, actual);
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