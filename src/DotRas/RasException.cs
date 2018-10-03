//--------------------------------------------------------------------------
// <copyright file="RasException.cs" company="Jeff Winn">
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
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using Internal;

    /// <summary>
    /// The exception that is thrown when a remote access service (RAS) error occurs.
    /// </summary>
    [Serializable]
    public class RasException : Exception
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasException"/> class.
        /// </summary>
        public RasException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public RasException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code that caused the exception.</param>
        public RasException(int errorCode)
            : base(RasHelper.Instance.GetRasErrorString(errorCode))
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public RasException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected RasException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorCode = (int)info.GetValue("ErrorCode", typeof(int));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the error code that caused the exception.
        /// </summary>
        public int ErrorCode { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Overridden. Populates a <see cref="System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info != null)
            {
                info.AddValue("ErrorCode", ErrorCode, typeof(int));
            }

            base.GetObjectData(info, context);
        }

        #endregion
    }
}