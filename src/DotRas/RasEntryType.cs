//--------------------------------------------------------------------------
// <copyright file="RasEntryType.cs" company="Jeff Winn">
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
    /// Defines the entry types.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "This enum is not a flag.")]
    public enum RasEntryType
    {
        /// <summary>
        /// No entry type specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Phone line.
        /// </summary>
        Phone = 1,

        /// <summary>
        /// Virtual Private Network.
        /// </summary>
        Vpn = 2,

        /// <summary>
        /// Direct serial or parallel connection.
        /// <para>
        /// <b>Windows Vista or later:</b> This value is no longer supported.
        /// </para>
        /// </summary>
        Direct = 3,

        /// <summary>
        /// Connection Manager (CM) connection.
        /// <para>
        /// <b>Note:</b> This member is reserved for system use only.
        /// </para>
        /// </summary>
        Internet = 4,

#if (WINXP || WIN2K8 || WIN7 || WIN8)

        /// <summary>
        /// Broadband connection.
        /// <para>
        /// <b>Windows XP or later:</b> This value is supported.
        /// </para>
        /// </summary>
        Broadband = 5

#endif
    }
}