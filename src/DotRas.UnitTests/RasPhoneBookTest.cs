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

namespace DotRas.UnitTests
{
    using System;
    using System.IO;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasPhoneBook"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasPhoneBookTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.UnitTests.RasPhoneBookTest"/> class.
        /// </summary>
        public RasPhoneBookTest()
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

        /// <summary>
        /// Tests the Open method to ensure an <see cref="System.ArgumentException"/> is thrown when the phonebook path is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenArgumentExceptionEmptyStringTest()
        {
            RasPhoneBook target = new RasPhoneBook();
            target.Open(string.Empty);
        }

        /// <summary>
        /// Tests the Open method to ensure an <see cref="System.ArgumentException"/> is thrown when the phonebook path is null.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenArgumentExceptionNullStringTest()
        {
            RasPhoneBook target = new RasPhoneBook();
            target.Open(null);
        }

        /// <summary>
        /// Tests the Open method to ensure an <see cref="System.ArgumentException"/> is thrown when the phonebook path does not contain a valid filename.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void OpenInvalidFileNameExceptionTest()
        {
            string phoneBookPath = @"C:\Blah\";

            RasPhoneBook target = new RasPhoneBook();
            target.Open(phoneBookPath);
        }

        /// <summary>
        /// Tests the Open(string) method to ensure the phone book type is correctly set when passing in the path of the All Users' profile phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OpenAllUsersProfileFileTest()
        {
            RasPhoneBookType expected = RasPhoneBookType.AllUsers;

            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            RasPhoneBook target = new RasPhoneBook();
            target.Open(phoneBookPath);

            RasPhoneBookType actual = target.PhoneBookType;

            Assert.AreEqual<RasPhoneBookType>(expected, actual);
        }

        /// <summary>
        /// Tests the Open(string) method to ensure the phone book type is correctly set when passing in the path of the Users' profile phone book.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void OpenUserProfileFileTest()
        {
            RasPhoneBookType expected = RasPhoneBookType.User;

            string phoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);

            RasPhoneBook target = new RasPhoneBook();
            target.Open(phoneBookPath);

            RasPhoneBookType actual = target.PhoneBookType;

            Assert.AreEqual<RasPhoneBookType>(expected, actual);
        }

        #endregion
    }
}