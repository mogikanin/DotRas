//--------------------------------------------------------------------------
// <copyright file="StateChangedEventArgs.cs" company="Jeff Winn">
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

    /// <summary>
    /// Provides data for the <see cref="RasDialer.StateChanged"/> event.
    /// </summary>
    
    public class StateChangedEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.StateChangedEventArgs"/> class.
        /// </summary>
        /// <param name="callbackId">The application defined value that was specified during dialing.</param>
        /// <param name="subEntryId">The one-based index for the phone book entry associated with this connection.</param>
        /// <param name="handle">The handle of the connection.</param>
        /// <param name="state">The state the remote access connection is about to enter.</param>
        /// <param name="errorCode">The error code (if any) that occurred.</param>
        /// <param name="errorMessage">The error message of the <paramref name="errorCode"/> that occurred.</param>
        /// <param name="extendedErrorCode">The extended error code (if any) that occurred.</param>
        internal StateChangedEventArgs(IntPtr callbackId, int subEntryId, RasHandle handle, RasConnectionState state, int errorCode, string errorMessage, int extendedErrorCode)
        {
            CallbackId = callbackId;
            SubEntryId = subEntryId;
            Handle = handle;
            State = state;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ExtendedErrorCode = extendedErrorCode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the application defined callback id.
        /// </summary>
        public IntPtr CallbackId
        {
            get;
        }

        /// <summary>
        /// Gets the one-based index for the phone book entry associated with this connection.
        /// </summary>
        public int SubEntryId
        {
            get;
        }

        /// <summary>
        /// Gets the handle of the connection.
        /// </summary>
        public RasHandle Handle
        {
            get;
        }

        /// <summary>
        /// Gets the state the remote access connection is about to enter.
        /// </summary>
        public RasConnectionState State
        {
            get;
        }

        /// <summary>
        /// Gets the error code (if any) that occurred.
        /// </summary>
        public int ErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the error message for the <see cref="ErrorCode"/> that occurred.
        /// </summary>
        public string ErrorMessage
        {
            get;
        }

        /// <summary>
        /// Gets the extended error code (if any) that occurred.
        /// </summary>
        public int ExtendedErrorCode
        {
            get;
        }

        #endregion
    }
}