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

namespace DotRas.IntegrationTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using DotRas;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasDialer"/> and is intended to contain all associated integration tests.
    /// </summary>
    [TestClass]
    public class RasDialerTest : IntegrationTest
    {
        #region Fields

        /// <summary>
        /// Holds the name of the entry that will be used to perform connection validation.
        /// </summary>
        private static string entryName;

        /// <summary>
        /// Holds the name of an entry that will need to be resolved by the DNS servers.
        /// </summary>
        private static string invalidEntryName;
        private static string phonebookPath;
        private static Guid entryId;
        private static Guid invalidEntryId;

        private RasDialer target;
        private System.Threading.EventWaitHandle waitHandle;
        private System.Threading.EventWaitHandle stateWaitHandle;
        private Exception error;
        private bool timedOut;
        private bool connected;
        private bool cancelled;
        private RasConnectionState signalOnState;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDialerTest"/> class.
        /// </summary>
        public RasDialerTest()
        {
        }

        #endregion

        #region Methods

        #region ~ Test Class Init

        /// <summary>
        /// Initializes the test class.
        /// </summary>
        /// <param name="context">The text context.</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            entryName = Guid.NewGuid().ToString();
            invalidEntryName = Guid.NewGuid().ToString();
            phonebookPath = Path.GetTempFileName();

            var pbk = new RasPhoneBook();
            pbk.Open(phonebookPath);

            entryId = TestUtilities.CreateValidVpnEntry(pbk, entryName);
            
            // The invalid server must be a valid DNS name that would need to be resolved.
            invalidEntryId = TestUtilities.CreateInvalidVpnEntry(pbk, invalidEntryName);
        }

        /// <summary>
        /// Performs cleanup for the test class.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanUp()
        {
            if (File.Exists(phonebookPath))
            {
                File.Delete(phonebookPath);
            }
        }

        #endregion

        #region ~ Test Methods Init

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            this.error = null;
            this.cancelled = false;
            this.connected = false;
            this.timedOut = false;

            this.target = new RasDialer();
            this.target.PhoneBookPath = phonebookPath;
            this.target.DialCompleted += new EventHandler<DialCompletedEventArgs>(this.Target_DialCompleted);
            this.target.Error += new EventHandler<System.IO.ErrorEventArgs>(this.Target_Error);
            this.target.StateChanged += new EventHandler<StateChangedEventArgs>(this.Target_StateChanged);

            this.stateWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            this.waitHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);

            base.Initialize();
        }

        /// <summary>
        /// Performs cleanup of the test instance.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            if (this.target != null)
            {
                this.target.Dispose();
            }

            if (this.waitHandle != null)
            {
                this.waitHandle.Dispose();
            }

            if (this.stateWaitHandle != null)
            {
                this.stateWaitHandle.Dispose();
            }

            HangUpConnectionById(entryId);
            HangUpConnectionById(invalidEntryId);
        }

        #endregion

        #region DialAsync

        /// <summary>
        /// Tests the DialAsync method to ensure the connection will succeed.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void DialAsyncConnectTest()
        {
            this.target.EntryName = entryName;
            this.target.Credentials = TestUtilities.GetValidCredentials();
            this.target.DialAsync();

            this.waitHandle.WaitOne();                       

            if (this.error != null)
            {
                Assert.Fail(this.error.ToString());
            }

            Assert.IsFalse(this.timedOut);
            Assert.IsFalse(this.cancelled);
            Assert.IsTrue(this.connected);
        }

        /// <summary>
        /// Tests the DialAsync method to ensure an exception is returned when the username is not valid.
        /// </summary>
        [TestMethod]
        public void DialAsyncInvalidUserNameTest()
        {
            this.target.EntryName = entryName;
            this.target.Credentials = new NetworkCredential("invaliduser", "blahblah");
            this.target.DialAsync();

            this.waitHandle.WaitOne();

            Assert.IsFalse(this.connected);
            Assert.IsFalse(this.cancelled);
            Assert.IsFalse(this.timedOut);
            Assert.IsInstanceOfType(this.error, typeof(RasDialException));
        }

        /// <summary>
        /// Tests the DialAsync method to ensure an exception is returned when the password is not valid.
        /// </summary>
        [TestMethod]
        public void DialAsyncInvalidPasswordTest()
        {
            this.target.EntryName = entryName;
            this.target.Credentials = new System.Net.NetworkCredential("testuser", string.Empty);
            this.target.DialAsync();

            this.waitHandle.WaitOne();

            Assert.IsFalse(this.connected);
            Assert.IsFalse(this.cancelled);
            Assert.IsFalse(this.timedOut);
            Assert.IsInstanceOfType(this.error, typeof(RasDialException));
        }

        /// <summary>
        /// Tests the DialAsync method to ensure the connection times out if not connected within the period specified.
        /// </summary>
        [TestMethod]
        public void DialAsyncTimeoutTest()
        {
            this.target.EntryName = invalidEntryName;
            this.target.Credentials = TestUtilities.GetValidCredentials();
            this.target.Timeout = 2000;
            this.target.DialAsync();

            this.waitHandle.WaitOne();

            Assert.IsTrue(this.timedOut);
            Assert.IsFalse(this.connected);
            Assert.IsFalse(this.cancelled);
            Assert.IsInstanceOfType(this.error, typeof(TimeoutException));
        }       

        #endregion

        #region DialAsyncCancel

        /// <summary>
        /// Tests the DialAsyncCancel method to ensure it works while dialing a connection asynchronously.
        /// </summary>
        [TestMethod]
        [TestCategory("ServerIntegration")]
        public void DialAsyncCancelTest()
        {
            // Set the state at which to signal the connection should be cancelled.
            this.signalOnState = RasConnectionState.DeviceConnected;

            this.target.EntryName = entryName;
            this.target.Credentials = TestUtilities.GetValidCredentials();
            this.target.DialAsync();

            // Wait until the state of the connection has reached the appropriate point.
            this.stateWaitHandle.WaitOne();

            this.target.DialAsyncCancel();

            // Now wait for the call to be completed to prevent a race condition.
            this.waitHandle.WaitOne();

            Assert.IsTrue(this.cancelled);
            Assert.IsFalse(this.connected);
            Assert.IsFalse(this.timedOut);
            Assert.IsNull(this.error);
        }

        #endregion

        /// <summary>
        /// Disconnects an active connection for the entry id specified.
        /// </summary>
        /// <param name="entryId">The entry id to disconnect.</param>
        private static void HangUpConnectionById(Guid entryId)
        {
            var connection = RasConnection.GetActiveConnections().Where(o => o.EntryId == entryId).FirstOrDefault();
            if (connection != null)
            {
                connection.HangUp();
            }
        }

        private void Target_Error(object sender, System.IO.ErrorEventArgs e)
        {
            this.error = e.GetException();

            this.waitHandle.Set();
        }

        private void Target_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.State == this.signalOnState)
            {
                this.stateWaitHandle.Set();
            }
        }

        private void Target_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            this.cancelled = e.Cancelled;
            this.connected = e.Connected;
            this.timedOut = e.TimedOut;
            this.error = e.Error;

            this.waitHandle.Set();
        }

        #endregion
    }
}