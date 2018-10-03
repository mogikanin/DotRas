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
            RasUpdateCredential expected = RasUpdateCredential.User;

            RasDialer dialer = new RasDialer();
            dialer.AutoUpdateCredentials = expected;

            RasUpdateCredential actual = dialer.AutoUpdateCredentials;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Credentials property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CredentialsTest()
        {
            NetworkCredential expected = new NetworkCredential("Test", "User");

            RasDialer target = new RasDialer();
            target.Credentials = expected;

            NetworkCredential actual = target.Credentials;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneBookPath property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneBookPathTest()
        {
            string expected = "C:\\Test.pbk";

            RasDialer target = new RasDialer();
            target.PhoneBookPath = expected;

            string actual = target.PhoneBookPath;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the EntryName property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EntryNameTest()
        {
            string expected = "Test Entry";

            RasDialer target = new RasDialer();
            target.EntryName = expected;

            string actual = target.EntryName;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the PhoneNumber property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void PhoneNumberTest()
        {
            string expected = "555-555-1234";

            RasDialer target = new RasDialer();
            target.PhoneNumber = expected;

            string actual = target.PhoneNumber;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CallbackNumber property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CallbackNumberTest()
        {
            string expected = "555-555-1234";

            RasDialer target = new RasDialer();
            target.CallbackNumber = expected;

            string actual = target.CallbackNumber;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the SubEntryId property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void SubEntryIdTest()
        {
            int expected = 1;

            RasDialer target = new RasDialer();
            target.SubEntryId = expected;

            int actual = target.SubEntryId;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Options property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsTest()
        {
            RasDialOptions expected = new RasDialOptions();

            RasDialer target = new RasDialer();
            target.Options = expected;

            RasDialOptions actual = target.Options;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the Options property to ensure an instance is returned even when the value is supposed to be null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OptionsNullTest()
        {
            RasDialer target = new RasDialer();
            target.Options = null;

            RasDialOptions actual = target.Options;

            Assert.IsNotNull(actual);
        }

        /// <summary>
        /// Tests the EapOptions property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EapOptionsTest()
        {
            RasEapOptions expected = new RasEapOptions(true, false, true);

            RasDialer target = new RasDialer();
            target.EapOptions = expected;

            RasEapOptions actual = target.EapOptions;

            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// Tests the EapOptions property to ensure a value is returned if the property is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void EapOptionsNullTest()
        {
            RasDialer target = new RasDialer();
            target.EapOptions = null;

            RasEapOptions actual = target.EapOptions;

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

            RasDialer target = new RasDialer();
            target.Owner = expected;

            IWin32Window actual = target.Owner;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the Timeout property to ensure the same value is returned as what was set.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void TimeoutTest()
        {
            int expected = 1000;

            RasDialer target = new RasDialer();
            target.Timeout = expected;

            int actual = target.Timeout;

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
            RasDialer dialer = new RasDialer();
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
            RasDialer dialer = new RasDialer();
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
            RasDialer dialer = new RasDialer();
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
            RasDialer dialer = new RasDialer();
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
            RasDialer dialer = new RasDialer();
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
            RasDialer dialer = new RasDialer();
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