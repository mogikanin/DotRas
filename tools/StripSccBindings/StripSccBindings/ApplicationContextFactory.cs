//--------------------------------------------------------------------------
// <copyright file="ApplicationContextFactory.cs" company="Jeff Winn">
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

namespace StripSccBindings
{
    using System;

    /// <summary>
    /// Contains factory methods for creating application context objects.
    /// </summary>
    internal static class ApplicationContextFactory
    {
        /// <summary>
        /// Creates a new <see cref="ApplicationContext"/> based on the command line arguments provided.
        /// </summary>
        /// <param name="args">The command-line arguments to use to generate the context.</param>
        /// <returns>A new <see cref="ApplicationContext"/> object.</returns>
        public static ApplicationContext Create(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }

            ApplicationContext retval = new ApplicationContext();
            retval.Provider = args[0].ToLower();
            
            // Check for any options that are being set.
            for (int index = 1; index < args.Length - 1; index++)
            {
                string[] tokens = args[index].Split(':');

                switch (tokens[0].ToLower())
                {
                    case "-recursive":
                        retval.EnableFolderRecursion = true;
                        break;

                    case "-filter":
                        if (tokens.Length == 1 || string.IsNullOrEmpty(tokens[1]))
                        {
                            throw new Exception("The filter settings were not provided.");
                        }

                        retval.Filter = tokens[1];
                        break;
                }
            }

            // Parse the path out of the arguments.
            retval.Path = args[args.Length - 1];

            return retval;
        }
    }
}