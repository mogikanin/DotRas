//--------------------------------------------------------------------------
// <copyright file="RasCollection.cs" company="Jeff Winn">
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

using JetBrains.Annotations;

namespace DotRas.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using Internal;
    using Properties;

    /// <summary>
    /// Provides the abstract base class for a remote-capable generic collection. This class must be inherited.
    /// </summary>
    /// <typeparam name="TObject">The type of object contained in the collection.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    [PublicAPI]
    public abstract class RasCollection<TObject> : MarshalByRefObject, ICollection<TObject>
        where TObject : class
    {
        #region Fields

        /// <summary>
        /// Defines the object used to synchronize the collection.
        /// </summary>
        private readonly object _syncRoot = new object();

        private List<TObject> _items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasCollection&lt;TObject&gt;"/> class.
        /// </summary>
        protected RasCollection()
        {
            _items = new List<TObject>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of entries contained in the collection.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets or sets a value indicating whether the collection is initializing.
        /// </summary>
        public bool IsInitializing { get; protected set; }

        /// <summary>
        /// Gets an entry from the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the entry to get.</param>
        /// <returns>An <typeparamref name="TObject"/> object.</returns>
        public TObject this[int index] => _items[index];

        #endregion

        #region Methods

        /// <summary>
        /// Adds the item to the collection.
        /// </summary>
        /// <param name="item">An <typeparamref name="TObject"/> to add. This argument cannot be a null (<b>Nothing</b> in Visual Basic).</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public void Add(TObject item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            InsertItem(_items.Count, item);
        }

        /// <summary>
        /// Clears the items in the collection.
        /// </summary>
        public void Clear()
        {
            ClearItems();
        }        

        /// <summary>
        /// Determines whether the phone book contains the entry specified.
        /// </summary>
        /// <param name="item">The object to locate within the collection.</param>
        /// <returns><b>true</b> if the item was found, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public bool Contains(TObject item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            return _items.Contains(item);
        }

        /// <summary>
        /// Searches for the specified item within the collection.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>The zero-based index of the item if found; otherwise, -1.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public int IndexOf(TObject item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            return _items.IndexOf(item);
        }

        /// <summary>
        /// Copies the entries to a <see cref="System.Array"/> starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="System.Array"/> that is the destination of the elements copied from collection. The <see cref="System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="array"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero.</exception>
        public void CopyTo(TObject[] array, int arrayIndex)
        {
            if (arrayIndex < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException("arrayIndex", arrayIndex, Resources.Argument_ValueCannotBeLessThanZero);
            }

            _items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes an entry from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><b>true</b> if the item was removed successfully, otherwise <b>false</b>.</returns>
        /// <exception cref="System.ArgumentNullException"><paramref name="item"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public bool Remove(TObject item)
        {
            if (item == null)
            {
                ThrowHelper.ThrowArgumentNullException("item");
            }

            var retval = false;

            var index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                retval = true;
            }

            return retval;
        }

        /// <summary>
        /// Removes the entry at the specified index in the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the entry to remove.</param>
        /// <exception cref="System.InvalidOperationException">The phone book of the entry collection has not been opened.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public void RemoveAt(int index)
        {
            RemoveItem(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerator&lt;TObject&gt;"/> to iterate through the collection.</returns>
        public IEnumerator<TObject> GetEnumerator()
        {
            return new RasCollectionEnumerator(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"/> to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RasCollectionEnumerator(this);
        }

        /// <summary>
        /// Begins the monitor to lock the collection from being modified by another thread.
        /// </summary>
        internal void BeginLock()
        {
            Monitor.Enter(_syncRoot);
        }

        /// <summary>
        /// Ends the monitor locking the collection from being modified by another thread.
        /// </summary>
        internal void EndLock()
        {
            Monitor.Exit(_syncRoot);
        }

        /// <summary>
        /// Clears the items in the collection.
        /// </summary>
        protected virtual void ClearItems()
        {            
            _items.Clear();
        }

        /// <summary>
        /// Inserts the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index at which the item will be inserted.</param>
        /// <param name="item">An <typeparamref name="TObject"/> to insert.</param>
        protected virtual void InsertItem(int index, TObject item)
        {
            _items.Insert(index, item);
        }

        /// <summary>
        /// Removes the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        protected virtual void RemoveItem(int index)
        {
            _items.RemoveAt(index);
        }

        #endregion

        #region RasCollectionEnumerator Class

        /// <summary>
        /// Provides simple iteration over a <see cref="DotRas.Design.RasCollection&lt;TObject&gt;"/> collection.
        /// </summary>
        private class RasCollectionEnumerator : IEnumerator<TObject>
        {
            #region Fields

            private RasCollection<TObject> _c;
            private int _index = -1;

            #endregion

            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RasCollectionEnumerator"/> class.
            /// </summary>
            /// <param name="c">The <see cref="DotRas.Design.RasCollection&lt;TObject&gt;"/> to enumerate.</param>
            public RasCollectionEnumerator(RasCollection<TObject> c)
            {
                if (c == null)
                {
                    ThrowHelper.ThrowArgumentNullException("c");
                }

                _c = c;
                _c.BeginLock();
            }

            /// <summary>
            /// Finalizes an instance of the <see cref="RasCollectionEnumerator"/> class.
            /// </summary>
            ~RasCollectionEnumerator()
            {
                Dispose(false);
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the current object.
            /// </summary>
            public TObject Current { get; private set; }

            /// <summary>
            /// Gets the current object.
            /// </summary>
            object IEnumerator.Current => Current;

            #endregion

            #region Methods

            /// <summary>
            /// Disposes of the resources used by the enumerator.
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns><b>true</b> if the enumerator was successfully advanced to the next element; <b>false</b> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                _index++;
                if (_index == _c.Count)
                {
                    _index = -1;
                    Current = null;

                    return false;
                }

                Current = _c[_index];
                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _index = -1;
                Current = null;
            }

            /// <summary>
            /// Disposes of the resources used by the enumerator.
            /// </summary>
            /// <param name="disposing"><b>true</b> to release the managed and unmanaged resources, <b>false</b> to release unmanaged resources only.</param>
            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _c.EndLock();

                    _index = -1;
                    Current = null;
                }
            }

            #endregion
        }

        #endregion
    }
}