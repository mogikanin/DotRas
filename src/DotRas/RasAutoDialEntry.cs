//--------------------------------------------------------------------------
// <copyright file="RasAutoDialEntry.cs" company="Jeff Winn">
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
    using System.Diagnostics;

    /// <summary>
    /// Represents an entry associated with a network address in the AutoDial mapping database. This class cannot be inherited.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("DialingLocation = {DialingLocation}, EntryName = {EntryName}")]
    public sealed class RasAutoDialEntry
    {
        #region Fields

        private int _dialingLocation;
        private string _entryName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialEntry"/> class.
        /// </summary>
        public RasAutoDialEntry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialEntry"/> class.
        /// </summary>
        /// <param name="dialingLocation">The TAPI dialing location.</param>
        /// <param name="entryName">The name of the existing phone book entry to dial.</param>
        public RasAutoDialEntry(int dialingLocation, string entryName)
        {
            _dialingLocation = dialingLocation;
            _entryName = entryName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TAPI dialing location.
        /// </summary>
        /// <remarks>For more information about TAPI dialing locations, see the TAPI Programmer's Reference in the Platform SDK.</remarks>
        public int DialingLocation
        {
            get => _dialingLocation;
            set => _dialingLocation = value;
        }

        /// <summary>
        /// Gets or sets the name of an existing phone book entry to dial.
        /// </summary>
        public string EntryName
        {
            get => _entryName;
            set => _entryName = value;
        }

        #endregion
    }
}