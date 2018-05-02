//--------------------------------------------------------------------------
// <copyright file="StripSccBindings.cs" company="Jeff Winn">
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
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Provides a task to execute the StripSccBindings tool. This class cannot be inherited.
    /// </summary>
    public sealed class StripSccBindings : ToolTask
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Build.Tasks.StripSccBindings"/> class.
        /// </summary>
        public StripSccBindings()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the provider to use.
        /// </summary>
        [Required]
        public string Provider
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        [Required]
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether recursion should be used.
        /// </summary>
        public bool Recursive
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        protected override string ToolName
        {
            get { return "StripSccBindings.exe"; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the full path to the tool.
        /// </summary>
        /// <returns>The full path to the tool.</returns>
        protected override string GenerateFullPathToTool()
        {
            return this.ToolPath;
        }

        /// <summary>
        /// Generates the command line commands for the tool.
        /// </summary>
        /// <returns>The command line commands for the tool.</returns>
        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();

            builder.AppendSwitch(this.Provider);
            builder.AppendSwitchIfTrue(this.Recursive, "-recursive");
            builder.AppendFileNameIfNotNull(this.Path);

            return builder.ToString();
        }

        #endregion
    }
}