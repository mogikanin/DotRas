//--------------------------------------------------------------------------
// <copyright file="MockBase.cs" company="Jeff Winn">
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

namespace DotRas.UnitTests.Mocking
{
    using System;

    /// <summary>
    /// Provides the base class for all mock objects. This class must be inherited.
    /// </summary>
    internal abstract class MockBase : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MockBase"/> class.
        /// </summary>
        protected MockBase()
        {
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MockBase"/> class.
        /// </summary>
        ~MockBase()
        {
            this.Dispose(false);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Releases all resources used by the mock test class. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the mock test class. 
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }       

        #endregion
    }
}