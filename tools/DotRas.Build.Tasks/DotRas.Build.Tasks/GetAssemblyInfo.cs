//--------------------------------------------------------------------------
// <copyright file="GetAssemblyInfo.cs" company="Jeff Winn">
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
    using System.IO;
    using System.Reflection;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Represents a task to get assembly information. This class cannot be inherited.
    /// </summary>
    public sealed class GetAssemblyInfo : Task
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Build.Tasks.GetAssemblyInfo"/> class.
        /// </summary>
        public GetAssemblyInfo()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the file path for the assembly.
        /// </summary>
        [Required]
        public string FilePath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the major version.
        /// </summary>
        [Output]
        public string MajorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the minor version.
        /// </summary>
        [Output]
        public string MinorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the build number.
        /// </summary>
        [Output]
        public string Build
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the revision number.
        /// </summary>
        [Output]
        public string Revision
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the assembly version.
        /// </summary>
        [Output]
        public string AssemblyVersion
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <returns><b>true</b> if the task executed successfully, otherwise <b>false</b>.</returns>
        public override bool Execute()
        {
            bool retval = false;

            try
            {
                this.Log.LogMessage("Starting GetAssemblyInfo task.");

                if (!File.Exists(this.FilePath))
                {
                    this.Log.LogError("The file '{0}' could not be found.", this.FilePath);
                }
                else
                {
                    Assembly assembly = Assembly.LoadFile(this.FilePath);
                    if (assembly == null)
                    {
                        this.Log.LogError("Could not load the assembly '{0}'.", this.FilePath);
                    }
                    else
                    {
                        Version version = assembly.GetName().Version;

                        this.MajorVersion = version.Major.ToString();
                        this.MinorVersion = version.Minor.ToString();
                        this.Build = version.Build.ToString();
                        this.Revision = version.Revision.ToString();

                        this.AssemblyVersion = version.ToString();

                        retval = true;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Log.LogError(ex.ToString());
            }
            finally
            {
                this.Log.LogMessage("Exiting GetAssemblyInfo task.");
            }

            return retval;
        }

        #endregion
    }
}