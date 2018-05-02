//--------------------------------------------------------------------------
// <copyright file="Sleep.cs" company="Jeff Winn">
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

namespace DotRas.Build.Tasks
{
    using System;
    using System.Threading;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Provides a task to pause the current MSBuild process.
    /// </summary>
    public sealed class Sleep : Task
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Build.Tasks.Sleep"/> class.
        /// </summary>
        public Sleep()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the timeout, in milliseconds.
        /// </summary>
        [Required]
        public int Timeout
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns><b>true</b> if the task was successful, otherwise <b>false</b>.</returns>
        public override bool Execute()
        {
            Thread.Sleep(this.Timeout);

            return true;
        }

        #endregion
    }
}