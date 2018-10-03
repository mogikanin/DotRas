//--------------------------------------------------------------------------
// <copyright file="RasAutoDialAddressCollection.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using Design;
    using Internal;

    /// <summary>
    /// Represents a strongly-typed collection of <see cref="DotRas.RasAutoDialAddress"/> objects. This class cannot be inherited.
    /// </summary>
    public sealed class RasAutoDialAddressCollection : RasCollection<RasAutoDialAddress>
    {
        #region Fields

        private RasAutoDialAddressItemCollection _lookUpTable;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasAutoDialAddressCollection"/> class.
        /// </summary>
        internal RasAutoDialAddressCollection()
        {
            _lookUpTable = new RasAutoDialAddressItemCollection();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an entry from the collection.
        /// </summary>
        /// <param name="address">The address to get.</param>
        /// <returns>An <see cref="RasAutoDialAddress"/> object.</returns>
        public RasAutoDialAddress this[string address] => _lookUpTable[address];

        #endregion

        #region Methods

        /// <summary>
        /// Loads the collection of AutoDial addresses.
        /// </summary>
        internal void Load()
        {
            try
            {
                BeginLock();
                IsInitializing = true;

                Clear();

                var addresses = RasHelper.Instance.GetAutoDialAddresses();
                if (addresses != null && addresses.Count > 0)
                {
                    foreach (var t in addresses)
                    {
                        var item = RasHelper.Instance.GetAutoDialAddress(t);
                        if (item != null)
                        {
                            Add(item);
                        }
                    }
                }
            }
            finally
            {
                IsInitializing = false;
                EndLock();
            }
        }

        /// <summary>
        /// Clears the items in the collection.
        /// </summary>
        protected override void ClearItems()
        {
            while (Count > 0)
            {
                RemoveAt(0);
            }
        }

        /// <summary>
        /// Inserts the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index at which the item will be inserted.</param>
        /// <param name="item">An <see cref="RasAutoDialAddress"/> to insert.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "The argument has been validated.")]
        protected override void InsertItem(int index, RasAutoDialAddress item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            if (IsInitializing)
            {
                _lookUpTable.Insert(index, item);
            }
            else
            {
                if (RasHelper.Instance.SetAutoDialAddress(item.Address, item.Entries))
                {
                    // The item has been added to the database, retrieve the item again to ensure entries match what 
                    // is already in the database. Removing an item from the database does not clear existing entries.
                    item = RasHelper.Instance.GetAutoDialAddress(item.Address);

                    _lookUpTable.Insert(index, item);
                }
            }

            base.InsertItem(index, item);
        }

        /// <summary>
        /// Removes the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        protected override void RemoveItem(int index)
        {
            if (!IsInitializing)
            {
                var item = this[index];
                if (item != null)
                {
                    if (RasHelper.Instance.SetAutoDialAddress(item.Address, null))
                    {
                        _lookUpTable.RemoveAt(index);
                    }
                }
            }

            base.RemoveItem(index);
        }

        #endregion

        #region RasAutoDialAddressItemCollection Class

        /// <summary>
        /// Represents a collection of <see cref="DotRas.RasAutoDialAddress"/> objects keyed by the entry address.
        /// </summary>
        private class RasAutoDialAddressItemCollection : KeyedCollection<string, RasAutoDialAddress>
        {
            #region Constructors

            #endregion

            #region Methods

            /// <summary>
            /// Extracts the key for the <see cref="DotRas.RasAutoDialAddress"/> object.
            /// </summary>
            /// <param name="item">An <see cref="DotRas.RasAutoDialAddress"/> from which to extract the key.</param>
            /// <returns>The key for the <paramref name="item"/> specified.</returns>
            /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "The argument has been validated.")]
            protected override string GetKeyForItem(RasAutoDialAddress item)
            {
                if (item == null)
                {
                    ThrowHelper.ThrowArgumentNullException("item");
                }

                return item.Address;
            }

            #endregion
        }

        #endregion
    }
}