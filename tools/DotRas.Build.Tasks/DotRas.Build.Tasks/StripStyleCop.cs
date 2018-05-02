//--------------------------------------------------------------------------
// <copyright file="StripStyleCop.cs" company="Jeff Winn">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Provides a task to remove StyleCop imports from Visual Studio project files (.*proj). This class cannot be inherited.
    /// </summary>
    public sealed class StripStyleCop : Task
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Build.Tasks.StripStyleCop"/> class.
        /// </summary>
        public StripStyleCop()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path of the project files.
        /// </summary>
        [Required]
        public ITaskItem[] ProjectFiles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the files whose StyleCop import statement has been removed.
        /// </summary>
        [Output]
        public ITaskItem[] Outputs
        {
            get;
            set;
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

            List<ITaskItem> output = new List<ITaskItem>();

            try
            {
                this.Log.LogMessage("Starting StripStyleCop task.");

                foreach (ITaskItem item in this.ProjectFiles)
                {
                    string identity = item.GetMetadata("identity");

                    try
                    {
                        XDocument doc = XDocument.Load(identity);
                        XNamespace xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";

                        var result = from e in doc.Descendants(xmlns + "Import")
                                     where e.Attribute("Project").Value.Contains("StyleCop")
                                     select e;

                        if (result != null && result.Count() > 0)
                        {
                            result.Remove();

                            doc.Save(identity);
                            output.Add(item);
                        }
                    }
                    catch (Exception)
                    {
                        this.Log.LogWarning("An unhandled exception occurred while processing the file '{0}'.", identity);
                    }
                }

                this.Outputs = output.ToArray();

                retval = true;
            }
            catch (Exception ex)
            {
                this.Log.LogError(ex.ToString());
            }
            finally
            {
                this.Log.LogMessage("Exiting StripStyleCop task.");
            }

            return retval;
        }
        
        #endregion
    }
}