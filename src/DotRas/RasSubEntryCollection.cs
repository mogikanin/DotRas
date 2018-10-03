//--------------------------------------------------------------------------
// <copyright file="RasSubEntryCollection.cs" company="Jeff Winn">
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
    using Design;
    using Internal;
    using Properties;

    /// <summary>
    /// Represents a strongly-typed collection of <see cref="DotRas.RasSubEntry"/> objects. This class cannot be inherited.
    /// </summary>
    public sealed class RasSubEntryCollection : RasOwnedCollection<RasEntry, RasSubEntry>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasSubEntryCollection"/> class.
        /// </summary>
        /// <param name="owner">An <see cref="DotRas.RasEntry"/> that owns the collection.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="owner"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        internal RasSubEntryCollection(RasEntry owner)
            : base(owner)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the subentries for the owning entry into the collection.
        /// </summary>
        /// <param name="phoneBook">The <see cref="DotRas.RasPhoneBook"/> that is being loaded.</param>
        /// <param name="count">The number of entries that need to be loaded.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="phoneBook"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        internal void Load(RasPhoneBook phoneBook, int count)
        {
            if (phoneBook == null)
            {
                ThrowHelper.ThrowArgumentNullException("phoneBook");
            }

            if (count <= 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException("count", count, Resources.Argument_ValueCannotBeLessThanOrEqualToZero);
            }

            try
            {
                BeginLock();
                IsInitializing = true;

                for (var index = 0; index < count; index++)
                {
                    var subEntry = RasHelper.Instance.GetSubEntryProperties(phoneBook, Owner, index);
                    if (subEntry != null)
                    {
                        Add(subEntry);
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
        /// Inserts the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index at which the item will be inserted.</param>
        /// <param name="item">An <see cref="RasSubEntry"/> to insert.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.InvalidOperationException">The phone book of the entry collection has not been opened.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "The argument has been validated.")]
        protected sealed override void InsertItem(int index, RasSubEntry item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            if (!IsInitializing && (Owner == null || Owner.Owner == null))
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_PhoneBookNotOpened);
            }

            item.Owner = Owner;

            if (IsInitializing)
            {
                base.InsertItem(index, item);
            }
            else
            {
                if (RasHelper.Instance.SetSubEntryProperties(Owner.Owner, Owner, index, item))
                {
                    base.InsertItem(index, item);
                }
            }
        }

        /// <summary>
        /// Removes the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="System.InvalidOperationException">The phone book of the entry collection has not been opened.</exception>
        protected sealed override void RemoveItem(int index)
        {
            if (!IsInitializing && (Owner == null || Owner.Owner == null))
            {
                ThrowHelper.ThrowInvalidOperationException(Resources.Exception_PhoneBookNotOpened);
            }

            if (IsInitializing)
            {
                base.RemoveItem(index);
            }
            else
            {
#if (WINXP || WIN2K8 || WIN7 || WIN8)
                if (RasHelper.Instance.DeleteSubEntry(Owner.Owner.Path, Owner.Name, index + 2))
                {
                    base.RemoveItem(index);
                }
#else
                // There is no remove subentry item call for Windows 2000. The subentry should be lost once the entry
                // is overwritten when it's saved.
                base.RemoveItem(index);
#endif
            }
        }

        #endregion
    }
}