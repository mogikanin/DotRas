//--------------------------------------------------------------------------
// <copyright file="RasDialogStyle.cs" company="Jeff Winn">
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
    /// <summary>
    /// Specifies the dialog styles for a <see cref="DotRas.RasEntryDialog"/> component.
    /// </summary>
    public enum RasDialogStyle
    {
        /// <summary>
        /// Create a new entry.
        /// </summary>
        Create,

        /// <summary>
        /// Edit an existing entry.
        /// </summary>
        Edit
    }
}