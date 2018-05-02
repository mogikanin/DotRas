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

namespace DotRas.Design
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using DotRas.Internal;
    using DotRas.Properties;

    /// <summary>
    /// Provides the abstract base class for a remote-capable generic collection. This class must be inherited.
    /// </summary>
    /// <typeparam name="TObject">The type of object contained in the collection.</typeparam>
    [DebuggerDisplay("Count = {Count}")]
    public abstract class RasCollection<TObject> : MarshalByRefObject, ICollection<TObject>
        where TObject : class
    {
        #region Fields

        /// <summary>
        /// Defines the object used to synchronize the collection.
        /// </summary>
        private readonly object _syncRoot = new object();

        private bool _initializing;
        private List<TObject> _items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasCollection&lt;TObject&gt;"/> class.
        /// </summary>
        protected RasCollection()
        {
            this._items = new List<TObject>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of entries contained in the collection.
        /// </summary>
        public int Count
        {
            get { return this._items.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the collection is initializing.
        /// </summary>
        protected bool IsInitializing
        {
            get { return this._initializing; }
            set { this._initializing = value; }
        }

        /// <summary>
        /// Gets an entry from the collection.
        /// </summary>
        /// <param name="index">The zero-based index of the entry to get.</param>
        /// <returns>An <typeparamref name="TObject"/> object.</returns>
        public TObject this[int index]
        {
            get { return this._items[index]; }
        }

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

            this.InsertItem(this._items.Count, item);
        }

        /// <summary>
        /// Clears the items in the collection.
        /// </summary>
        public void Clear()
        {
            this.ClearItems();
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

            return this._items.Contains(item);
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

            return this._items.IndexOf(item);
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
            if (array == null)
            {
                ThrowHelper.ThrowArgumentNullException("array");
            }

            if (arrayIndex < 0)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException("arrayIndex", arrayIndex, Resources.Argument_ValueCannotBeLessThanZero);
            }

            this._items.CopyTo(array, arrayIndex);
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

            bool retval = false;

            int index = this.IndexOf(item);
            if (index != -1)
            {
                this.RemoveAt(index);
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
            this.RemoveItem(index);
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
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new RasCollectionEnumerator(this);
        }

        /// <summary>
        /// Begins the monitor to lock the collection from being modified by another thread.
        /// </summary>
        internal void BeginLock()
        {
            Monitor.Enter(this._syncRoot);
        }

        /// <summary>
        /// Ends the monitor locking the collection from being modified by another thread.
        /// </summary>
        internal void EndLock()
        {
            Monitor.Exit(this._syncRoot);
        }

        /// <summary>
        /// Clears the items in the collection.
        /// </summary>
        protected virtual void ClearItems()
        {            
            this._items.Clear();
        }

        /// <summary>
        /// Inserts the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index at which the item will be inserted.</param>
        /// <param name="item">An <typeparamref name="TObject"/> to insert.</param>
        protected virtual void InsertItem(int index, TObject item)
        {
            this._items.Insert(index, item);
        }

        /// <summary>
        /// Removes the item at the index specified.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        protected virtual void RemoveItem(int index)
        {
            this._items.RemoveAt(index);
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
            private TObject _current;
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

                this._c = c;
                this._c.BeginLock();
            }

            /// <summary>
            /// Finalizes an instance of the <see cref="RasCollectionEnumerator"/> class.
            /// </summary>
            ~RasCollectionEnumerator()
            {
                this.Dispose(false);
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the current object.
            /// </summary>
            public TObject Current
            {
                get { return this._current; }
            }

            /// <summary>
            /// Gets the current object.
            /// </summary>
            object IEnumerator.Current
            {
                get { return this._current; }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Disposes of the resources used by the enumerator.
            /// </summary>
            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns><b>true</b> if the enumerator was successfully advanced to the next element; <b>false</b> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                this._index++;
                if (this._index == this._c.Count)
                {
                    this._index = -1;
                    this._current = null;

                    return false;
                }

                this._current = this._c[this._index];
                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                this._index = -1;
                this._current = null;
            }

            /// <summary>
            /// Disposes of the resources used by the enumerator.
            /// </summary>
            /// <param name="disposing"><b>true</b> to release the managed and unmanaged resources, <b>false</b> to release unmanaged resources only.</param>
            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this._c.EndLock();

                    this._index = -1;
                    this._current = null;
                }
            }

            #endregion
        }

        #endregion
    }
}