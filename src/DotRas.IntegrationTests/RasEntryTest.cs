//--------------------------------------------------------------------------
// <copyright file="RasEntryTest.cs" company="Jeff Winn">
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
    using System;
    using System.Linq;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasEntry"/> and is intended to contain all associated integration tests.
    /// </summary>
    [TestClass]
    public class RasEntryTest : IntegrationTest
    {
        #region Fields

        private string _entryName;
        private string _phoneBookPath;
        private RasPhoneBook _phoneBook;
        private RasEntry _entry;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasEntryTest"/> class.
        /// </summary>
        public RasEntryTest()
        {
        }

        #endregion

        #region Methods

        #region ~ Test Methods Init

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        [TestCategory("Integration")]
        public override void Initialize()
        {
            base.Initialize();

            this._phoneBookPath = System.IO.Path.GetTempFileName();
            this._entryName = Guid.NewGuid().ToString();

            this._phoneBook = new RasPhoneBook();
            this._phoneBook.Open(this._phoneBookPath);

            this._entry = new RasEntry(this._entryName);
            this._entry.Device = RasDevice.GetDevices().Where(o => o.Name.Contains("(PPTP)") && o.DeviceType == RasDeviceType.Vpn).FirstOrDefault();
            this._entry.EncryptionType = RasEncryptionType.Require;
            this._entry.EntryType = RasEntryType.Vpn;
            this._entry.FramingProtocol = RasFramingProtocol.Ppp;
            this._entry.NetworkProtocols.IP = true;
            this._entry.NetworkProtocols.IPv6 = true;
            this._entry.PhoneNumber = IPAddress.Loopback.ToString();
            this._entry.VpnStrategy = RasVpnStrategy.Default;

            this._phoneBook.Entries.Add(this._entry);
        }

        /// <summary>
        /// Performs cleanup for the current test instance.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            if (this._phoneBook.Entries.Contains(this._entry))
            {
                this._phoneBook.Entries.Remove(this._entry);
            }

            this._phoneBook.Dispose();
            System.IO.File.Delete(this._phoneBookPath);
        }

        #endregion

        #region ClearCredentials

        /// <summary>
        /// Tests the ClearCredentials method to ensure the credentials will be cleared as expected.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void ClearCredentialsTest()
        {
            var expected = true;
            var actual = this._entry.ClearCredentials();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region GetCredentials

        /// <summary>
        /// Tests the GetCredentials method to ensure the expected credentials are returned.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void GetCredentialsTest()
        {
            var expected = new NetworkCredential("User", "Password", "Domain");

            // Update the credentials so they can be retrieved correctly.
            this._entry.UpdateCredentials(expected);

            var actual = this._entry.GetCredentials();

            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.Domain, actual.Domain);
        }

        #endregion

        #region Options

        /// <summary>
        /// Tests the IPv6RemoteDefaultGateway options property to ensure the expected value is persisting to the phonebook correctly.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void IPv6RemoteDefaultGatewayAfterReloadTest()
        {
            var expected = true;

            this._entry.Options.IPv6RemoteDefaultGateway = expected;
            this._entry.Update();

            this._entry = null;
            this._phoneBook.Dispose();
            this._phoneBook = null;

            this._phoneBook = new RasPhoneBook();
            this._phoneBook.Open(this._phoneBookPath);

            this._entry = this._phoneBook.Entries[this._entryName];

            var actual = this._entry.Options.IPv6RemoteDefaultGateway;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Tests the Remove method to ensure the entry is removed from the phone book.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void RemoveTest()
        {
            var expected = true;
            var actual = this._entry.Remove();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Rename

        /// <summary>
        /// Tests the Rename method to ensure the entry will be renamed as expected.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void RenameTest()
        {
            var name = this._entry.Name;
            var newEntryName = Guid.NewGuid().ToString();

            var expected = true;
            var actual = this._entry.Rename(newEntryName);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(this._entry.Name, newEntryName);
        }

        #endregion

        #region Update

        /// <summary>
        /// Tests the Update method to ensure the entry will update properly.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void UpdateTest()
        {
            var expected = true;
            var actual = this._entry.Update();

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region UpdateCredentials

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials will update.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void UpdateCredentialsTest()
        {
            var credentials = new NetworkCredential("Test", "User", "Domain");
            var expected = true;

            var actual = this._entry.UpdateCredentials(credentials);

            Assert.AreEqual(expected, actual);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Tests the UpdateCredentials method to ensure the credentials will update for the current user.
        /// </summary>
        [TestMethod]
        public void UpdateCredentialsForUserTest()
        {
            NetworkCredential credentials = new NetworkCredential("Test", "User", "Domain");
            bool expected = true;

            bool actual = this._entry.UpdateCredentials(credentials, false);

            Assert.AreEqual(expected, actual);
        }

#endif

        #endregion

        #endregion
    }
}