//--------------------------------------------------------------------------
// <copyright file="UnitTest.cs" company="Jeff Winn">
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
    using DotRas.Internal;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides the base class for all unit tests. This class must be inherited.
    /// </summary>
    public abstract class UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTest"/> class.
        /// </summary>
        protected UnitTest()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        public virtual void Initialize()
        {
            SafeNativeMethods.Instance = null;
            UnsafeNativeMethods.Instance = null;
            RasHelper.Instance = null;
        }

        #endregion
    }
}