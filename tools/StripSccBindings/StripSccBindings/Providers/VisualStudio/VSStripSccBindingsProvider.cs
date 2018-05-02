//--------------------------------------------------------------------------
// <copyright file="VSStripSccBindingsProvider.cs" company="Jeff Winn">
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

namespace StripSccBindings.Providers.VisualStudio
{
    using System;

    /// <summary>
    /// Provides a base implementation of a provider to strip Visual Studio source control bindings.
    /// </summary>
    internal abstract class VSStripSccBindingsProvider : StripSccBindingsProviderBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.VSStripSccBindingsProvider"/> class.
        /// </summary>
        protected VSStripSccBindingsProvider()
        {
        }

        #endregion

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="file">An <see cref="System.IO.FileInfo"/> to process.</param>
        protected override void ProcessFile(System.IO.FileInfo file)
        {
            StripperBase stripper = null;

            switch (file.Extension.ToLower())
            {
                case ".sln":
                    stripper = this.CreateSolutionFileStripper();
                    break;

                case ".csproj":
                case ".vbproj":
                case ".wixproj":
                case ".wdproj":
                    stripper = this.CreateProjectFileStripper();
                    break;

                default:
                    return;
            }

            if (stripper != null)
            {
                StripperContext context = new StripperContext();
                context.File = file;

                stripper.Context = context;
                stripper.StripBindings();

                Console.WriteLine("{0} processed.", file.FullName);
            }
        }

        /// <summary>
        /// Creates a new stripper to strip solution files.
        /// </summary>
        /// <returns>A new <see cref="VSSolutionFileStripper"/> object.</returns>
        protected virtual VisualStudioStripperBase CreateSolutionFileStripper()
        {
            throw new NotSupportedException("The solution stripper is specific to each provider.");
        }

        /// <summary>
        /// Creates a new stripper to strip project files.
        /// </summary>
        /// <returns>A new <see cref="VSProjectFileStripper"/> object.</returns>
        protected virtual VisualStudioStripperBase CreateProjectFileStripper()
        {
            return new VSProjectFileStripper();
        }
    }
}