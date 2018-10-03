//--------------------------------------------------------------------------
// <copyright file="RasSubEntry.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Internal;
    using Properties;

    /// <summary>
    /// Represents a subentry of a remote access service (RAS) entry. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to create a multilink dial-up entry and add it to a phone book.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasPhoneBook pbk = new RasPhoneBook())
    /// {
    ///     pbk.Open();
    ///     RasEntry entry = RasEntry.CreateDialUpEntry("Dial-Up Connection", "555-111-1234", RasDevice.GetDeviceByName("Internal Modem", RasDeviceType.Modem));
    ///     if (entry != null)
    ///     {
    ///         entry.DialMode = RasDialMode.DialAll;
    ///         entry.SubEntries.Add(new RasSubEntry() { PhoneNumber = "555-111-2345", Device = RasDevice.GetDeviceByName("Interal Modem #2", RasDeviceType.Modem) });
    ///         pbk.Entries.Add(entry);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim pbk As New RasPhoneBook
    /// pbk.Open()
    /// Dim entry As RasEntry = RasEntry.CreateDialUpEntry("Dial-Up Connection", "555-111-1234", RasDevice.GetDeviceByName("Internal Modem", RasDeviceType.Modem))
    /// If entry IsNot Nothing Then
    ///     entry.DialMode = RasDialMode.DialAll
    ///     Dim subentry As New RasSubEntry
    ///     subentry.PhoneNumber = "555-111-2345"
    ///     subentry.Device = RasDevice.GetDeviceByName("Internal Modem #2", RasDeviceType.Modem)
    ///     entry.SubEntries.Add(subentry)
    ///     pbk.Entries.Add(entry)
    /// End If
    /// ]]>
    /// </code>
    /// </example>
    [DebuggerDisplay("PhoneNumber = {PhoneNumber}")]
    public sealed class RasSubEntry : MarshalByRefObject, ICloneable
    {
        #region Fields

        private Collection<string> alternatePhoneNumbers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasSubEntry"/> class.
        /// </summary>
        public RasSubEntry()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the owner of the subentry.
        /// </summary>
        public RasEntry Owner
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the remote access device.
        /// </summary>
        /// <remarks>To retrieve a list of available devices, use the <see cref="RasDevice.GetDevices"/> method.</remarks>
        public RasDevice Device
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a collection of alternate phone numbers that are dialed in the order listed if the primary number fails.
        /// </summary>
        public Collection<string> AlternatePhoneNumbers
        {
            get => alternatePhoneNumbers ?? (alternatePhoneNumbers = new Collection<string>());
            internal set => alternatePhoneNumbers = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this <see cref="RasSubEntry"/>.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasSubEntry"/> object.</returns>
        public object Clone()
        {
            var retval = new RasSubEntry();

            if (AlternatePhoneNumbers.Count > 0)
            {
                foreach (var value in AlternatePhoneNumbers)
                {
                    retval.AlternatePhoneNumbers.Add(value);
                }
            }

            retval.Device = Device;
            retval.PhoneNumber = PhoneNumber;

            return retval;
        }

        /// <summary>
        /// Removes the subentry from the phone book.
        /// </summary>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        public bool Remove()
        {
            var retval = false;

            if (Owner != null)
            {
                retval = Owner.SubEntries.Remove(this);
            }

            return retval;
        }

        /// <summary>
        /// Updates the subentry.
        /// </summary>
        /// <returns><b>true</b> if the operation was successful, otherwise <b>false</b>.</returns>
        /// <exception cref="System.InvalidOperationException">The collection is not associated with a phone book.</exception>
        public bool Update()
        {
            if (Owner == null || Owner.Owner == null)
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_EntryNotInPhoneBook);
            }

            var retval = false;

            var index = Owner.SubEntries.IndexOf(this);
            if (index != -1)
            {
                retval = RasHelper.Instance.SetSubEntryProperties(Owner.Owner, Owner, index, this);
            }

            return retval;
        }

        #endregion
    }
}