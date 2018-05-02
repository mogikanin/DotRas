//--------------------------------------------------------------------------
// <copyright file="Program.cs" company="Jeff Winn">
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
    using System.Configuration;
    using StripSccBindings.Configuration;
    using StripSccBindings.Providers;

    /// <summary>
    /// Contains the entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">An array of strings containing arguments from the command line.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("StripSccBindings v1.0 - Source Control Bindings Removal Tool");
            Console.WriteLine("Copyright (c) Jeff Winn. All rights reserved.");
            Console.WriteLine();

            try
            {
                Go(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);

                // Update the exit code so the calling application knows there was a problem with the application.
                Environment.ExitCode = 1;
            }
        }

        /// <summary>
        /// Performs the main process of the application.
        /// </summary>
        /// <param name="args">An array of strings containing arguments from the command line.</param>
        private static void Go(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                // The user did not include any arguments, assume the help information should be displayed.
                DisplayHelp();
                return;
            }

            ApplicationContext context = ApplicationContextFactory.Create(args);
            if (context == null)
            {
                return;
            }
            else
            {
                StripSccBindingsProviderBase provider = StripSccBindingsProviderFactory.CreateProvider(context);
                if (provider == null)
                {
                    throw new Exception("The provider could not be located.");
                }
                else
                {
                    provider.Start();
                }
            }
        }

        /// <summary>
        /// Writes the help information to the console.
        /// </summary>
        private static void DisplayHelp()
        {
            Console.WriteLine("Usage: stripsccbindings.exe provider [options] path");
            Console.WriteLine();

            Console.WriteLine("Providers");
            StripSccBindingsSection section = (StripSccBindingsSection)ConfigurationManager.GetSection(StripSccBindingsSection.SectionName);
            if (section != null && section.Providers != null)
            {
                foreach (ProviderSettings provider in section.Providers)
                {
                    Console.WriteLine(provider.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Options");
            Console.WriteLine("-filter: Filters the files to be checked.");
            Console.WriteLine("-recursive: Enables recursively searching the path.");
        }
    }
}