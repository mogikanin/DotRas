//--------------------------------------------------------------------------
// <copyright file="UtilitiesTest.cs" company="Jeff Winn">
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
    using System.Net;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.Internal.Utilities"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class UtilitiesTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.UnitTests.UtilitiesTest"/> class.
        /// </summary>
        public UtilitiesTest()
        {
        }

        #endregion

        #region Methods

        #region IPAddressIsNullOrAnyAddress

        /// <summary>
        /// Tests the IPAddressIsNullOrAnyAddress null address test returns the expected value. 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressIsNullOrAnyAddressNullAddressTest()
        {
            bool expected = true;
            bool actual = Utilities.IsIPAddressNullOrAnyAddress(null);

            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// Tests the IPAddressIsNullOrAnyAddress null address test returns the expected value. 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressIsNullOrAnyAddressAnyAddressTest()
        {
            bool expected = true;
            bool actual = Utilities.IsIPAddressNullOrAnyAddress(IPAddress.Any);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPAddressIsNullOrAnyAddress null address test returns the expected value. 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressIsNullOrAnyAddressParseAnyAddressTest()
        {
            bool expected = true;
            bool actual = Utilities.IsIPAddressNullOrAnyAddress(IPAddress.Parse("0.0.0.0"));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPAddressIsNullOrAnyAddress null address test returns the expected value. 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressIsNullOrAnyAddressParseIPv6AnyAddressTest()
        {
            bool expected = true;
            bool actual = Utilities.IsIPAddressNullOrAnyAddress(IPAddress.Parse("::"));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the IPAddressIsNullOrAnyAddress null address test returns the expected value. 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void IPAddressIsNullOrAnyAddressIPv6AnyAddressTest()
        {
            bool expected = true;
            bool actual = Utilities.IsIPAddressNullOrAnyAddress(IPAddress.IPv6Any);

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #endregion
    }
}