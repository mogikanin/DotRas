//--------------------------------------------------------------------------
// <copyright file="TraceEvent.cs" company="Jeff Winn">
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

namespace DotRas.Diagnostics
{
    /// <summary>
    /// Provides the base class for trace events. This class must be inherited.
    /// </summary>
    internal abstract class TraceEvent
    {
        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the event.
        /// </summary>
        /// <returns>The serialized event data.</returns>
        public abstract string Serialize();

        #endregion
    }
}