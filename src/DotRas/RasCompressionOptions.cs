//--------------------------------------------------------------------------
// <copyright file="RasCompressionOptions.cs" company="Jeff Winn">
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
    using DotRas.Internal;

    /// <summary>
    /// Represents remote access service (RAS) compression options. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class RasCompressionOptions : ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasCompressionOptions"/> class.
        /// </summary>
        internal RasCompressionOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasCompressionOptions"/> class.
        /// </summary>
        /// <param name="value">The flags value to set.</param>
        internal RasCompressionOptions(NativeMethods.RASCCPO value)
        {
            this.CompressionOnly = Utilities.HasFlag(value, NativeMethods.RASCCPO.CompressionOnly);
            this.HistoryLess = Utilities.HasFlag(value, NativeMethods.RASCCPO.HistoryLess);
            this.Encryption56Bit = Utilities.HasFlag(value, NativeMethods.RASCCPO.Encryption56Bit);
            this.Encryption40Bit = Utilities.HasFlag(value, NativeMethods.RASCCPO.Encryption40Bit);
            this.Encryption128Bit = Utilities.HasFlag(value, NativeMethods.RASCCPO.Encryption128Bit);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether compression without encryption will be used.
        /// </summary>
        public bool CompressionOnly
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether Microsoft Point-to-Point Encryption (MPPE) is in stateless mode.
        /// </summary>
        public bool HistoryLess
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether Microsoft Point-to-Point Encryption (MPPE) is using 56-bit keys.
        /// </summary>
        public bool Encryption56Bit
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether Microsoft Point-to-Point Encryption (MPPE) is using 40-bit keys.
        /// </summary>
        public bool Encryption40Bit
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether Microsoft Point-to-Point Encryption (MPPE) is using 128-bit keys.
        /// </summary>
        public bool Encryption128Bit
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasCompressionOptions"/> object.</returns>
        public object Clone()
        {
            RasCompressionOptions retval = new RasCompressionOptions();

            retval.CompressionOnly = this.CompressionOnly;
            retval.HistoryLess = this.HistoryLess;
            retval.Encryption56Bit = this.Encryption56Bit;
            retval.Encryption40Bit = this.Encryption40Bit;
            retval.Encryption128Bit = this.Encryption128Bit;

            return retval;
        }

        #endregion
    }
}