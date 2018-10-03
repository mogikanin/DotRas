//--------------------------------------------------------------------------
// <copyright file="StringBufferedPInvokeMock.cs" company="Jeff Winn">
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
    using System.Text;
    using DotRas.Internal;

    /// <summary>
    /// Provides the implementation for string buffered p/invoke mock objects.
    /// </summary>
    /// <typeparam name="TInput">The type of object used as input for the call.</typeparam>
    internal class StringBufferedPInvokeMock<TInput> : StructBufferedPInvokeMock<TInput, string>
        where TInput : StructBufferedPInvokeParams
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the offset at which copying begins.
        /// </summary>
        public int Offset
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the mock call.
        /// </summary>
        /// <param name="value">An <typeparamref name="TInput"/> object containing call information.</param>
        public override void Execute(TInput value)
        {
            var sb = new StringBuilder();
            foreach (var item in this.Result)
            {
                sb.Append(item).Append('\x00');
            }

            var expectedSize = (sb.Length * 2) + this.Offset;

            if (value.BufferSize.ToInt64() < expectedSize)
            {
                value.BufferSize = new IntPtr(expectedSize);
            }
            else if (this.Result.Length > 0)
            {
                var lpAddresses = IntPtr.Zero;
                try
                {
                    lpAddresses = Marshal.StringToHGlobalUni(sb.ToString());
                    new UnsafeNativeMethods().CopyMemoryImpl(new IntPtr(value.Address.ToInt64() + this.Offset), lpAddresses, new IntPtr(sb.Length * 2));
                }
                finally
                {
                    if (lpAddresses != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(lpAddresses);
                    }
                }

                value.Count = new IntPtr(this.Result.Length);
            }
        }

        #endregion
    }
}