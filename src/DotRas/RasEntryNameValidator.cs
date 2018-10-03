//--------------------------------------------------------------------------
// <copyright file="RasEntryNameValidator.cs" company="Jeff Winn">
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
    using Internal;
    using Properties;

    /// <summary>
    /// Validates the format of an entry name for a phone book. This class cannot be inherited.
    /// </summary>
    /// <remarks>The name must contain at least one non-whitespace alphanumeric character.</remarks>
    /// <example>
    /// The following example shows how to use the <b>RasEntryNameValidator</b> to validate an entry does not already exist within a phone book. After the <see cref="RasEntryNameValidator.Validate"/> method has been called, the <see cref="RasEntryNameValidator.IsValid"/> property will indicate whether the entry name is valid.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasEntryNameValidator validator = new RasEntryNameValidator();
    /// validator.EntryName = "VPN Connection";
    /// validator.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
    /// validator.AllowExistingEntries = false;
    /// validator.Validate();
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim validator As New RasEntryNameValidator
    /// validator.EntryName = "VPN Connection"
    /// validator.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)
    /// validator.AllowExistingEntries = False
    /// validator.Validate()
    /// ]]>
    /// </code>
    /// </example>
    public sealed class RasEntryNameValidator
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasEntryNameValidator"/> class.
        /// </summary>
        public RasEntryNameValidator()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the error code, if any, that occurred during validation.
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entry name is valid.
        /// </summary>
        public bool IsValid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether existing entries will be allowed.
        /// </summary>
        public bool AllowExistingEntries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether nonexistent phone books are allowed.
        /// </summary>
        public bool AllowNonExistentPhoneBook
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the entry name to validate.
        /// </summary>
        public string EntryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone book path to validate the entry name against.
        /// </summary>
        public string PhoneBookPath
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the condition it checks, and updates the <see cref="IsValid"/> property.
        /// </summary>
        public void Validate()
        {
            try
            {
                var errorCode = SafeNativeMethods.Instance.ValidateEntryName(PhoneBookPath, EntryName);

                if (errorCode == NativeMethods.SUCCESS || (AllowExistingEntries && errorCode == NativeMethods.ERROR_ALREADY_EXISTS) || (AllowNonExistentPhoneBook && errorCode == NativeMethods.ERROR_CANNOT_OPEN_PHONEBOOK))
                {
                    ErrorCode = NativeMethods.SUCCESS;
                    ErrorMessage = null;
                }
                else
                {
                    ErrorCode = errorCode;
                    ErrorMessage = RasHelper.Instance.GetRasErrorString(errorCode);
                }

                IsValid = ErrorCode == NativeMethods.SUCCESS;
            }
            catch (EntryPointNotFoundException)
            {
                ThrowHelper.ThrowNotSupportedException(Resources.Exception_NotSupportedOnPlatform);
            }
        }

        #endregion
    }
}