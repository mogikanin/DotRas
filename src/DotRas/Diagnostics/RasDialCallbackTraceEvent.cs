//--------------------------------------------------------------------------
// <copyright file="RasDialCallbackTraceEvent.cs" company="Jeff Winn">
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

namespace DotRas.Diagnostics
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents the trace event for a RasDialCallback notification. This class cannot be inherited.
    /// </summary>
    internal sealed class RasDialCallbackTraceEvent : TraceEvent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDialCallbackTraceEvent"/> class.
        /// </summary>
        /// <param name="result">The result of the callback.</param>
        /// <param name="callbackId">The callback id.</param>
        /// <param name="subEntryId">The subentry id.</param>
        /// <param name="dangerousHandle">The dangerous handle.</param>
        /// <param name="message">The message.</param>
        /// <param name="state">The state.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="extendedErrorCode">The extended error code.</param>
        public RasDialCallbackTraceEvent(bool result, IntPtr callbackId, int subEntryId, IntPtr dangerousHandle, int message, RasConnectionState state, int errorCode, int extendedErrorCode)
        {
            Result = result;
            CallbackId = callbackId;
            SubEntryId = subEntryId;
            DangerousHandle = dangerousHandle;
            Message = message;
            State = state;
            ErrorCode = errorCode;
            ExtendedErrorCode = extendedErrorCode;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the result is true or false.
        /// </summary>
        public bool Result
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the callback id.
        /// </summary>
        public IntPtr CallbackId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the subentry id.
        /// </summary>
        public int SubEntryId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dangerous handle.
        /// </summary>
        public IntPtr DangerousHandle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public int Message
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection state.
        /// </summary>
        public RasConnectionState State
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int ErrorCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the extended error code.
        /// </summary>
        public int ExtendedErrorCode
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the event.
        /// </summary>
        /// <returns>The serialized event data.</returns>
        public override string Serialize()
        {
            var sb = new StringBuilder();

            sb.Append("EventType: RasDialCallback").AppendLine();
            sb.AppendFormat("Result: {0}", Result).AppendLine();
            sb.AppendFormat("CallbackId: {0}", CallbackId).AppendLine();
            sb.AppendFormat("SubEntryId: {0}", SubEntryId).AppendLine();
            sb.AppendFormat("DangerousHandle: {0}", DangerousHandle).AppendLine();
            sb.AppendFormat("Message: {0}", Message).AppendLine();
            sb.AppendFormat("State: {0}", State).AppendLine();
            sb.AppendFormat("ErrorCode: {0}", ErrorCode).AppendLine();
            sb.AppendFormat("ExtendedErrorCode: {0}", ExtendedErrorCode).AppendLine();

            return sb.ToString();
        }

        #endregion
    }
}