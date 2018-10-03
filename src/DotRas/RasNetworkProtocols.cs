//--------------------------------------------------------------------------
// <copyright file="RasNetworkProtocols.cs" company="Jeff Winn">
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

    /// <summary>
    /// Represents network protocols for a remote access service (RAS) entry. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class RasNetworkProtocols : ICloneable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasNetworkProtocols"/> class.
        /// </summary>
        public RasNetworkProtocols()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the NetBEUI protocol will be negotiated.
        /// </summary>
        [Obsolete("This member is no longer supported.")]
        public bool NetBeui
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the IPX protocol will be negotiated.
        /// </summary>
        public bool Ipx
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the IP protocol will be negotiated.
        /// </summary>
        public bool IP
        {
            get;
            set;
        }

#if (WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Gets or sets a value indicating whether the IPv6 protocol will be negotiated.
        /// </summary>
        /// <remarks><b>Windows Vista and later:</b> This property is available.</remarks>
        public bool IPv6
        {
            get;
            set;
        }

#endif

        #endregion

        #region Methods

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>A new <see cref="DotRas.RasNetworkProtocols"/> object.</returns>
        public object Clone()
        {
            var retval = new RasNetworkProtocols();

#pragma warning disable 0618
            retval.NetBeui = NetBeui;
#pragma warning restore 0618
            retval.Ipx = Ipx;
            retval.IP = IP;

#if (WIN2K8 || WIN7 || WIN8)

            retval.IPv6 = IPv6;

#endif

            return retval;
        }

        #endregion
    }
}