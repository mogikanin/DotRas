//--------------------------------------------------------------------------
// <copyright file="RasLcpOptions.cs" company="Jeff Winn">
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
    /// Represents remote access service (RAS) link control protocol options. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class RasLcpOptions : ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasLcpOptions"/> class.
        /// </summary>
        internal RasLcpOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasLcpOptions"/> class.
        /// </summary>
        /// <param name="value">The flags value to set.</param>
        internal RasLcpOptions(NativeMethods.RASLCPO value)
        {
            Pfc = Utilities.HasFlag(value, NativeMethods.RASLCPO.Pfc);
            Acfc = Utilities.HasFlag(value, NativeMethods.RASLCPO.Acfc);
            Sshf = Utilities.HasFlag(value, NativeMethods.RASLCPO.Sshf);
            Des56 = Utilities.HasFlag(value, NativeMethods.RASLCPO.Des56);
            TripleDes = Utilities.HasFlag(value, NativeMethods.RASLCPO.TripleDes);

#if (WIN2K8 || WIN7 || WIN8)

            Aes128 = Utilities.HasFlag(value, NativeMethods.RASLCPO.Aes128);
            Aes256 = Utilities.HasFlag(value, NativeMethods.RASLCPO.Aes256);

#endif
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether protocol field compression is being used.
        /// </summary>
        public bool Pfc
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether address and control field compression is being used.
        /// </summary>
        public bool Acfc
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether short sequence number header format is being used.
        /// </summary>
        public bool Sshf
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether DES 56-bit encryption is being used.
        /// </summary>
        public bool Des56
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether 3 DES encryption is being used.
        /// </summary>
        public bool TripleDes
        {
            get;
            private set;
        }

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets a value indicating whether AES 128-bit encryption is being used.
        /// </summary>
        /// <remarks><b>Windows Vista and later:</b> This property is available.</remarks>
        public bool Aes128
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether AES 256-bit encryption is being used.
        /// </summary>
        /// <remarks><b>Windows Vista and later:</b> This property is available.</remarks>
        public bool Aes256
        {
            get;
            private set;
        }

#endif

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasLcpOptions"/> object.</returns>
        public object Clone()
        {
            RasLcpOptions retval = new RasLcpOptions();

            retval.Pfc = Pfc;
            retval.Acfc = Acfc;
            retval.Sshf = Sshf;
            retval.Des56 = Des56;
            retval.TripleDes = TripleDes;

#if (WIN2K8 || WIN7 || WIN8)

            retval.Aes128 = Aes128;
            retval.Aes256 = Aes256;

#endif

            return retval;
        }

        #endregion
    }
}