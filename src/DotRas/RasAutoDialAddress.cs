//--------------------------------------------------------------------------
// <copyright file="RasAutoDialAddress.cs" company="Jeff Winn">
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

    /// <summary>
    /// Represents a network address in the AutoDial mapping database. This class cannot be inherited.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("Address = {Address}")]
    public sealed class RasAutoDialAddress
    {
        #region Fields

        private string _address;
        private Collection<RasAutoDialEntry> _entries;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialAddress"/> class.
        /// </summary>
        /// <param name="address">The network address.</param>
        public RasAutoDialAddress(string address)
        {
            this._address = address;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the network address.
        /// </summary>
        public string Address
        {
            get { return this._address; }
        }

        /// <summary>
        /// Gets the collection of entries associated with the address.
        /// </summary>
        public Collection<RasAutoDialEntry> Entries
        {
            get
            {
                if (this._entries == null)
                {
                    this._entries = new Collection<RasAutoDialEntry>();
                }

                return this._entries;
            }
        }

        #endregion
    }
}