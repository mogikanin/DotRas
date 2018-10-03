//--------------------------------------------------------------------------
// <copyright file="RasOwnedCollection.cs" company="Jeff Winn">
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
    using Internal;

    /// <summary>
    /// Provides the abstract base class for a remote-capable collection whose objects are owned by other objects. This class must be inherited.
    /// </summary>
    /// <typeparam name="TOwner">The type of object that owns the objects in the collection.</typeparam>
    /// <typeparam name="TObject">The type of object contained in the collection.</typeparam>
    public abstract class RasOwnedCollection<TOwner, TObject> : RasCollection<TObject>
        where TOwner : class
        where TObject : class
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasOwnedCollection&lt;TOwner, TObject&gt;"/> class.
        /// </summary>
        /// <param name="owner">The owner of the collection.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="owner"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        protected RasOwnedCollection(TOwner owner)
        {
            if (owner == null)
            {
                ThrowHelper.ThrowArgumentNullException("owner");
            }

            Owner = owner;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the owner of the collection.
        /// </summary>
        protected TOwner Owner { get; }

        #endregion
    }
}