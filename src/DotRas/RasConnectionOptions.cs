//--------------------------------------------------------------------------
// <copyright file="RasConnectionOptions.cs" company="Jeff Winn">
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

#if (WINXP || WIN2K8 || WIN7 || WIN8)

    /// <summary>
    /// Represents connection options for a remote access service (RAS) connection. This class cannot be inherited.
    /// </summary>
    [Serializable]
    public sealed class RasConnectionOptions
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasConnectionOptions"/> class.
        /// </summary>
        internal RasConnectionOptions()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the connection is available to all users.
        /// </summary>
        public bool AllUsers
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the credentials used for the connection are the default credentials.
        /// </summary>
        public bool GlobalCredentials
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the owner of the connection is known.
        /// </summary>
        public bool OwnerKnown
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a value indicating whether the owner of the connection matches the current user.
        /// </summary>
        public bool OwnerMatch
        {
            get;
            internal set;
        }

        #endregion
    }

#endif
}