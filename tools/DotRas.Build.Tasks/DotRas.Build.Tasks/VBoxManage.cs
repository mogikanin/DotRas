//--------------------------------------------------------------------------
// <copyright file="VBoxManage.cs" company="Jeff Winn">
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
    /// Provides a task to control the Sun VirtualBox virtual machines. This class cannot be inherited.
    /// </summary>
    public sealed class VBoxManage : ToolTask
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Build.Tasks.VBoxManage"/> class.
        /// </summary>
        public VBoxManage()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the command to execute.
        /// </summary>
        [Required]
        public string Command
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the additional parameters for the command.
        /// </summary>
        public string Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        protected override string ToolName
        {
            get { return "VBoxManage.exe"; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates the full path to the tool.
        /// </summary>
        /// <returns>The full path to the tool.</returns>
        protected override string GenerateFullPathToTool()
        {
            string path = null;

            if (!string.IsNullOrEmpty(this.ToolPath))
            {
                path = this.ToolPath;
            }
            else
            {
                path = Environment.GetEnvironmentVariable("VBOX_INSTALL_PATH", EnvironmentVariableTarget.Machine);
                if (!string.IsNullOrEmpty(path))
                {
                    throw new Exception("The installation path for Sun VirtualBox could not be located in the environment variables.");
                }
            }

            return path;
        }

        /// <summary>
        /// Generates the command line commands for the tool.
        /// </summary>
        /// <returns>The command line commands for the tool.</returns>
        protected override string GenerateCommandLineCommands()
        {
            CommandLineBuilder builder = new CommandLineBuilder();

            builder.AppendSwitch(this.Command);
            builder.AppendSwitch(this.Parameters);

            return builder.ToString();
        }

        #endregion
    }
}