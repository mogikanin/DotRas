//--------------------------------------------------------------------------
// <copyright file="RasHandle.cs" company="Jeff Winn">
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



namespace DotRas
{
    using System;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Represents a wrapper class for remote access service (RAS) handles. This class cannot be inherited.
    /// </summary>
    
    public sealed class RasHandle : SafeHandleZeroOrMinusOneIsInvalid, IEquatable<RasHandle>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasHandle"/> class.
        /// </summary>
        public RasHandle()
            : base(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasHandle"/> class.
        /// </summary>
        /// <param name="handle">The handle to use.</param>
        /// <param name="isMultilink"><b>true</b> if the handle is a single-link in a multi-link connection.</param>
        internal RasHandle(IntPtr handle, bool isMultilink)
            : base(false)
        {
            IsMultilink = isMultilink;

            SetHandle(handle);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the handle is for a single-link in a multi-link connection.
        /// </summary>
        public bool IsMultilink
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether two instances of <see cref="DotRas.RasHandle"/> are equal.
        /// </summary>
        /// <param name="objA">The first <see cref="DotRas.RasHandle"/> to compare.</param>
        /// <param name="objB">The second <see cref="DotRas.RasHandle"/> to compare.</param>
        /// <returns><b>true</b> if <paramref name="objA"/> equals <paramref name="objB"/>, otherwise <b>false</b>.</returns>
        public static bool operator ==(RasHandle objA, RasHandle objB)
        {
            return Equals(objA, objB);
        }

        /// <summary>
        /// Determines whether two instances of <see cref="DotRas.RasHandle"/> are not equal.
        /// </summary>
        /// <param name="objA">The first <see cref="DotRas.RasHandle"/> to compare.</param>
        /// <param name="objB">The second <see cref="DotRas.RasHandle"/> to compare.</param>
        /// <returns><b>true</b> if <paramref name="objA"/> does not equal <paramref name="objB"/>, otherwise <b>false</b>.</returns>
        public static bool operator !=(RasHandle objA, RasHandle objB)
        {
            return !(objA == objB);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="System.Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="System.Object"/>.</param>
        /// <returns><b>true</b> if <paramref name="obj"/> is equal to the current object, otherwise <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            var handle = obj as RasHandle;

            if (!ReferenceEquals(handle, null))
            {
                return Equals(handle);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="DotRas.RasHandle"/> is equal to the current <see cref="DotRas.RasHandle"/>.
        /// </summary>
        /// <param name="other">The <see cref="DotRas.RasHandle"/> to compare with the current <see cref="DotRas.RasHandle"/>.</param>
        /// <returns><b>true</b> if <paramref name="other"/> is equal to the current object, otherwise <b>false</b>.</returns>
        public bool Equals(RasHandle other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(other, this))
            {
                return true;
            }

            return handle == other.handle;
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>The hash code for the instance.</returns>
        public override int GetHashCode()
        {
            return handle.GetHashCode();
        }

        /// <summary>
        /// Releases the handle.
        /// </summary>
        /// <returns><b>true</b> if the handle was released successfully, otherwise <b>false</b>.</returns>
        /// <remarks>This method will never release the handle, doing so would disconnect the client when the object is finalized.</remarks>
        protected override bool ReleaseHandle()
        {
            return true;
        }

        #endregion
    }
}