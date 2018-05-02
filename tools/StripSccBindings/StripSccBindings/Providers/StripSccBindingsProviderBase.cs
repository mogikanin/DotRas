//--------------------------------------------------------------------------
// <copyright file="StripSccBindingsProviderBase.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace StripSccBindings.Providers
{
    using System;
    using System.Configuration.Provider;
    using System.IO;

    /// <summary>
    /// Provides a base implementation of a provider used to strip source control bindings.
    /// </summary>
    internal abstract class StripSccBindingsProviderBase : ProviderBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.StripSccBindingsProviderBase"/> class.
        /// </summary>
        protected StripSccBindingsProviderBase()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contextual information about the stripping process.
        /// </summary>
        public ApplicationContext Context
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the stripping process.
        /// </summary>
        public void Start()
        {
            this.ProcessFolder(this.Context.Path);
        }

        /// <summary>
        /// When overridden in a derived class, processes the file.
        /// </summary>
        /// <param name="file">An <see cref="System.IO.FileInfo"/> to process.</param>
        protected abstract void ProcessFile(FileInfo file);

        /// <summary>
        /// Processes the folder for the path specified.
        /// </summary>
        /// <param name="path">The path to process.</param>
        private void ProcessFolder(string path)
        {
            string[] files = null;

            if (!string.IsNullOrEmpty(this.Context.Filter))
            {
                files = Directory.GetFiles(path, this.Context.Filter);
            }
            else
            {
                files = Directory.GetFiles(path);
            }

            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    try
                    {
                        this.ProcessFile(new FileInfo(file));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("An error occurred while processing the file '{0}'", file), ex);
                    }
                }
            }

            if (this.Context.EnableFolderRecursion)
            {
                foreach (string directory in Directory.GetDirectories(path))
                {
                    this.ProcessFolder(directory);
                }
            }
        }

        #endregion
    }
}