//--------------------------------------------------------------------------
// <copyright file="RasPhoneBookTest.cs" company="Jeff Winn">
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
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasPhoneBook"/> and is intended to contain all related integration tests.
    /// </summary>
    [TestClass]
    public class RasPhoneBookTest : IntegrationTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasPhoneBookTest"/> class.
        /// </summary>
        public RasPhoneBookTest()
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

        /// <summary>
        /// Tests the RasPhoneBook component to ensure it can create a custom phone book.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void CreateCustomPhoneBookTest()
        {
            DirectoryInfo tempFolder = TestUtilities.GetTempPath(true);
            string path = null;

            try
            {
                path = Path.Combine(tempFolder.FullName, string.Format("{0}.pbk", TestUtilities.StripNonAlphaNumericChars(Guid.NewGuid().ToString())));

                RasPhoneBook target = new RasPhoneBook();
                target.Open(path);

                RasDevice device = RasDevice.GetDevices().Where(o => o.Name.Contains("(PPTP)") && o.DeviceType == RasDeviceType.Vpn).FirstOrDefault();

                RasEntry entry = RasEntry.CreateVpnEntry("Test Entry", IPAddress.Loopback.ToString(), RasVpnStrategy.Default, device);
                if (entry != null)
                {
                    target.Entries.Add(entry);
                }

                Assert.IsTrue(File.Exists(path), "The phone book file was not found at the expected location. '{0}'", path);
            }
            finally
            {
                if (Directory.Exists(tempFolder.FullName))
                {
                    // The folder was created successfully, delete it before the test completes.
                    Directory.Delete(tempFolder.FullName, true);
                }
            }
        }

        /// <summary>
        /// Tests the Open method to ensure it can properly open a phonebook in a location that does not already exist.
        /// </summary>
        [TestMethod]
        [TestCategory("Integration")]
        public void OpenPhoneBookInCustomFolderThatDoesNotAlreadyExistTest()
        {
            DirectoryInfo tempFolder = TestUtilities.GetTempPath(false);
            string path = null;

            try
            {
                path = Path.Combine(tempFolder.FullName, string.Format("{0}.pbk", TestUtilities.StripNonAlphaNumericChars(Guid.NewGuid().ToString())));

                RasPhoneBook target = new RasPhoneBook();
                target.EnableFileWatcher = true;
                target.Open(path);
            }
            finally
            {
                if (Directory.Exists(tempFolder.FullName))
                {
                    // The folder was created successfully, delete it before the test completes.
                    Directory.Delete(tempFolder.FullName, true);
                }
            }
        }

        #endregion
    }
}