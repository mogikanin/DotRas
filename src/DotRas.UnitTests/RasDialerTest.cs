//--------------------------------------------------------------------------
// <copyright file="RasDialerTest.cs" company="Jeff Winn">
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
    using System.Windows.Forms;
    using DotRas;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasDialer"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasDialerTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.UnitTests.RasDialerTest"/> class.
        /// </summary>
        public RasDialerTest()
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

        #region Property Tests

        /// <summary>
        /// Tests the AutoUpdateCredentials property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void AutoUpdateCredentialsTest()
        {
            var expected = RasUpdateCredential.User;

            var dialer = new RasDialer();
            dialer.AutoUpdateCredentials = expected;

            var actual = dialer.AutoUpdateCredentials;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Credentials property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CredentialsTest()
        {
            var expected = new NetworkCredential("Test", "User");

            var target = new RasDialer();
            target.Credentials = expected;

            var actual = target.Credentials;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneBookPath property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneBookPathTest()
        {
            var expected = "C:\\Test.pbk";

            var target = new RasDialer();
            target.PhoneBookPath = expected;

            var actual = target.PhoneBookPath;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EntryName property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryNameTest()
        {
            var expected = "Test Entry";

            var target = new RasDialer();
            target.EntryName = expected;

            var actual = target.EntryName;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneNumber property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneNumberTest()
        {
            var expected = "555-555-1234";

            var target = new RasDialer();
            target.PhoneNumber = expected;

            var actual = target.PhoneNumber;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CallbackNumber property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CallbackNumberTest()
        {
            var expected = "555-555-1234";

            var target = new RasDialer();
            target.CallbackNumber = expected;

            var actual = target.CallbackNumber;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SubEntryId property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SubEntryIdTest()
        {
            var expected = 1;

            var target = new RasDialer();
            target.SubEntryId = expected;

            var actual = target.SubEntryId;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Options property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsTest()
        {
            var expected = new RasDialOptions();

            var target = new RasDialer();
            target.Options = expected;

            var actual = target.Options;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the Options property to ensure an instance is returned even when the value is supposed to be null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsNullTest()
        {
            var target = new RasDialer();
            target.Options = null;

            var actual = target.Options;

            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Tests the EapOptions property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EapOptionsTest()
        {
            var expected = new RasEapOptions(true, false, true);

            var target = new RasDialer();
            target.EapOptions = expected;

            var actual = target.EapOptions;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the EapOptions property to ensure a value is returned if the property is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EapOptionsNullTest()
        {
            var target = new RasDialer();
            target.EapOptions = null;

            var actual = target.EapOptions;

            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Tests the Owner property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OwnerTest()
        {
            IWin32Window expected = null;

            var target = new RasDialer();
            target.Owner = expected;

            var actual = target.Owner;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Timeout property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TimeoutTest()
        {
            var expected = 1000;

            var target = new RasDialer();
            target.Timeout = expected;

            var actual = target.Timeout;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Method Tests

        #region Dial

        /// <summary>
        /// Tests the Dial method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the entry name is provided without a phone number or phonebook path.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialInvalidOperationExceptionTest()
        {
            var dialer = new RasDialer();
            dialer.EntryName = "Test Entry";
            dialer.PhoneBookPath = null;
            dialer.PhoneNumber = null;

            dialer.Dial();
        }

        /// <summary>
        /// Tests the Dial method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the phonebook path is provided without a phone number or entry name.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialInvalidOperationException1Test()
        {
            var dialer = new RasDialer();
            dialer.EntryName = null;
            dialer.PhoneBookPath = "C:\\Test.pbk";
            dialer.PhoneNumber = null;

            dialer.Dial();
        }

        /// <summary>
        /// Tests the Dial method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the a phone number, phonebook path, and entry name have not been provided.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialInvalidOperationException2Test()
        {
            var dialer = new RasDialer();
            dialer.EntryName = null;
            dialer.PhoneBookPath = null;
            dialer.PhoneNumber = null;

            dialer.Dial();
        }

        #endregion

        #region DialAsync

        /// <summary>
        /// Tests the DialAsync method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the entry name is provided without a phone number or phonebook path.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialAsyncInvalidOperationExceptionTest()
        {
            var dialer = new RasDialer();
            dialer.EntryName = "Test Entry";
            dialer.PhoneBookPath = null;
            dialer.PhoneNumber = null;

            dialer.DialAsync();
        }

        /// <summary>
        /// Tests the DialAsync method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the phonebook path is provided without a phone number or entry name.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialAsyncInvalidOperationException1Test()
        {
            var dialer = new RasDialer();
            dialer.EntryName = null;
            dialer.PhoneBookPath = "C:\\Test.pbk";
            dialer.PhoneNumber = null;

            dialer.DialAsync();
        }

        /// <summary>
        /// Tests the DialAsync method to ensure an <see cref="System.InvalidOperationException"/> is thrown when the a phone number, phonebook path, and entry name have not been provided.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void DialAsyncInvalidOperationException2Test()
        {
            var dialer = new RasDialer();
            dialer.EntryName = null;
            dialer.PhoneBookPath = null;
            dialer.PhoneNumber = null;

            dialer.DialAsync();
        }

        #endregion

        #endregion

        #endregion
    }
}