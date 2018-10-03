//--------------------------------------------------------------------------
// <copyright file="RasIPOptions.cs" company="Jeff Winn">
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

    /// <summary>
    /// Defines the remote access service (RAS) IPCP options.
    /// </summary>
    [Serializable]
    public class RasIPOptions : ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIPOptions"/> class.
        /// </summary>
        internal RasIPOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIPOptions"/> class.
        /// </summary>
        /// <param name="value">The flags value to set.</param>
        internal RasIPOptions(NativeMethods.RASIPO value)
        {
            VJ = Utilities.HasFlag(value, NativeMethods.RASIPO.VJ);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether Van Jacobson compression is used.
        /// </summary>
        public bool VJ
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasLcpOptions"/> object.</returns>
        public object Clone()
        {
            var retval = new RasIPOptions {VJ = VJ};
            return retval;
        }

        #endregion
    }
}