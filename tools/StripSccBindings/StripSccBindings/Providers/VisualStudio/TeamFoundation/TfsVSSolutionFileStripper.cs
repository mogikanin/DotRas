//--------------------------------------------------------------------------
// <copyright file="TfsVSSolutionFileStripper.cs" company="Jeff Winn">
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

namespace StripSccBindings.Providers.VisualStudio.TeamFoundation
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides a stripper used to remove Team Foundation Server source control bindings from a Visual Studio (.sln) solution file. This class cannot be inherited.
    /// </summary>
    internal sealed class TfsVSSolutionFileStripper : VSSolutionFileStripper
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Providers.VisualStudio.TeamFoundation.TfsVSSolutionFileStripper"/> class.
        /// </summary>
        public TfsVSSolutionFileStripper()
        {
        }

        #endregion

        /// <summary>
        /// Strips the bindings from the file.
        /// </summary>
        protected override void InternalStripBindings()
        {
            string content = File.ReadAllText(this.Context.File.FullName, Encoding.UTF8);

            Regex regex = new Regex(
                @"
                \tGlobalSection\(TeamFoundationVersionControl\).*\r\n #begin of SCC section
                (
                (.+\r\n)*?             #any number of lines
                \t\tSccProjectUniqueName\d+\s*=\s*(?<name>.+)\r\n  #unique name
                (.+\r\n)*?             #any number of lines
                \t\tSccLocalPath\d+\s*=\s*(?<local>.+)\r\n         #local name
                )*                #rinse and repeat for all projects
                (.+\r\n)*?        #any number of lines
                \tEndGlobalSection\r\n  #end of SCC section
                ", 
                 RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace);

            Match match = regex.Match(content);
            if (match.Success)
            {
                content = content.Replace(match.Value, string.Empty);

                // Write the content back to the file.
                File.WriteAllText(this.Context.File.FullName, content, Encoding.UTF8);
            }
        }
    }
}