//--------------------------------------------------------------------------
// <copyright file="RasIkeV2Options.cs" company="Jeff Winn">
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

#if (WIN7 || WIN8)

    /// <summary>
    /// Defines the Internet Key Exchange (IKEv2) options. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Known Limitations:</b>
    /// <list type="bullet">
    /// <item>This type is only available on Windows 7 and later operating systems.</item>
    /// </list>
    /// </para>
    /// </remarks>
    [Serializable]
    public sealed class RasIkeV2Options
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasIkeV2Options"/> class.
        /// </summary>
        internal RasIkeV2Options()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the client supports the IKEv2 Mobility and Multi-homing (MOBIKE) protocol.
        /// </summary>
        public bool MobileIke
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the client is behind Network Address Translation (NAT).
        /// </summary>
        public bool ClientBehindNat
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the server is behind Network Address Translation (NAT).
        /// </summary>
        public bool ServerBehindNat
        {
            get;
            internal set;
        }

        #endregion
    }

#endif
}