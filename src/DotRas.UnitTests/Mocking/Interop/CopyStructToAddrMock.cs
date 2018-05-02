//--------------------------------------------------------------------------
// <copyright file="CopyStructToAddrMock.cs" company="Jeff Winn">
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
    using System.Runtime.InteropServices;
    using DotRas.Internal;

    /// <summary>
    /// Provides the implementation for mock objects used to copy a result to a memory address.
    /// </summary>
    /// <typeparam name="T">The type of structure to copy.</typeparam>
    internal sealed class CopyStructToAddrMock<T> : PInvokeMock
        where T : struct
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyStructToAddrMock&lt;T&gt;"/> class.
        /// </summary>
        public CopyStructToAddrMock()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the result of the call.
        /// </summary>
        public T Result
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the mock call.
        /// </summary>
        /// <param name="value">The memory address at which to copy the result.</param>
        public void Execute(IntPtr value)
        {
            T structure = this.Result;

            Marshal.StructureToPtr(structure, value, true);
        }

        #endregion
    }
}