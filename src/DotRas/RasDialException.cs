//--------------------------------------------------------------------------
// <copyright file="RasDialException.cs" company="Jeff Winn">
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

    /// <summary>
    /// The exception that is thrown when a remote access service (RAS) error occurs while dialing a connection.
    /// </summary>
    [Serializable]
    public class RasDialException : RasException
    {
        #region Fields

        private int _extendedErrorCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialException"/> class.
        /// </summary>
       
        public RasDialException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public RasDialException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code that caused the exception.</param>
        /// <param name="extendedErrorCode">The extended error code (if any) that occurred.</param>
        public RasDialException(int errorCode, int extendedErrorCode)
            : base(errorCode)
        {
            ExtendedErrorCode = extendedErrorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public RasDialException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected RasDialException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ExtendedErrorCode = (int)info.GetValue("ExtendedErrorCode", typeof(int));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the extended error code (if any) that occurred.
        /// </summary>
        public int ExtendedErrorCode
        {
            get => _extendedErrorCode;
            private set
            {
                _extendedErrorCode = value;
                Data["ExtendedErrorCode"] = value;
            }
        }

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
            info.AddValue("ExtendedErrorCode", ExtendedErrorCode, typeof(int));
            base.GetObjectData(info, context);
        }

        #endregion
    }
}