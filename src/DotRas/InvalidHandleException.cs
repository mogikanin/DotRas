//--------------------------------------------------------------------------
// <copyright file="InvalidHandleException.cs" company="Jeff Winn">
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
    /// The exception that is thrown when an invalid connection handle is used.
    /// </summary>
    [Serializable]
    public class InvalidHandleException : Exception
    {
        #region Fields

        private RasHandle _handle;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        public InvalidHandleException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="handle">The <see cref="DotRas.RasHandle"/> that caused the exception.</param>
        public InvalidHandleException(RasHandle handle)
            : this(handle, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        public InvalidHandleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="handle">The <see cref="DotRas.RasHandle"/> that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        public InvalidHandleException(RasHandle handle, string message)
            : this(handle, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public InvalidHandleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="handle">The <see cref="DotRas.RasHandle"/> that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or null reference (<b>Nothing</b> in Visual Basic) if no inner exception is specified.</param>
        public InvalidHandleException(RasHandle handle, string message, Exception innerException)
            : base(message, innerException)
        {
            Handle = handle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.InvalidHandleException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected InvalidHandleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Handle = (RasHandle)info.GetValue("Handle", typeof(RasHandle));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="DotRas.RasHandle"/> that caused the exception.
        /// </summary>
        public RasHandle Handle
        {
            get => _handle;
            private set => _handle = value;
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
            if (info != null)
            {
                info.AddValue("Handle", Handle, typeof(RasHandle));
            }

            base.GetObjectData(info, context);
        }

        #endregion
    }
}