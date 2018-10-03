//--------------------------------------------------------------------------
// <copyright file="RasGetAutodialEnableParams.cs" company="Jeff Winn">
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

namespace DotRas.Internal
{
    /// <summary>
    /// Provides information for the RasGetAutodialEnable API call.
    /// </summary>
    internal class RasGetAutodialEnableParams
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the dialing location.
        /// </summary>
        public int DialingLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialing location is enabled.
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        #endregion
    }
}