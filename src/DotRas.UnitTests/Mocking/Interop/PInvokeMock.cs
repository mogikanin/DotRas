//--------------------------------------------------------------------------
// <copyright file="PInvokeMock.cs" company="Jeff Winn">
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

namespace DotRas.UnitTests.Mocking.Interop
{
    using System;

    /// <summary>
    /// Provides the base implementation for p/invoke mock objects. This class must be inherited.
    /// </summary>
    internal abstract class PInvokeMock : MockBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PInvokeMock"/> class.
        /// </summary>
        protected PInvokeMock()
        {
        }

        #endregion
    }
}