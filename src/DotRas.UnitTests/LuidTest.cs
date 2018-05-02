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
    using System;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LuidTest"/> class.
        /// </summary>
        public LuidTest()
        {
        }

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
            int expected = 200;

            Luid target = new Luid(100, 100);

            int actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetHashCode method to ensure it returns int.MaxValue when the value exceeds that of the integer type.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetHashCodeTestForMaxValueReturned()
        {
            int expected = int.MaxValue;

            Luid target = new Luid(int.MaxValue, int.MaxValue);

            int actual = target.GetHashCode();

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
            Luid expected = new Luid(1, 1);

            Mock<IRasHelper> mock = new Mock<IRasHelper>();
            RasHelper.Instance = mock.Object;

            mock.Setup(o => o.AllocateLocallyUniqueId()).Returns(expected);

            Luid actual = Luid.NewLuid();

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
            bool expected = false;

            Luid objA = new Luid(1, 1);
            Luid objB = new Luid(1, 1);

            bool actual = objA != objB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the equality operator.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualityOperatorTest()
        {
            bool expected = false;

            Luid objA = new Luid(1, 1);
            Luid objB = new Luid(1, 2);

            bool actual = objA == objB;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTest()
        {
            bool expected = false;

            Luid target = new Luid(1, 1);
            Luid other = new Luid(2, 2);

            bool actual = target.Equals(other);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method with a null reference object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTestWithNullObject()
        {
            bool expected = false;

            Luid target = new Luid(1, 1);
            object obj = null;

            bool actual = target.Equals(obj);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Equals method with a Luid that has been boxed.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EqualsTestWithBoxedLuidObject()
        {
            bool expected = true;

            Luid target = new Luid(1, 1);
            Luid other = new Luid(1, 1);

            bool actual = target.Equals((object)other);

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