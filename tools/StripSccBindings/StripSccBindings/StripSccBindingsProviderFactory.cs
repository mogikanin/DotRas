//--------------------------------------------------------------------------
// <copyright file="StripSccBindingsProviderFactory.cs" company="Jeff Winn">
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
    using System.Reflection;
    using StripSccBindings.Configuration;
    using StripSccBindings.Providers;

    /// <summary>
    /// Contains factory methods to create providers used to strip source control bindings.
    /// </summary>
    internal static class StripSccBindingsProviderFactory
    {
        /// <summary>
        /// Creates a new provider used to strip source control bindings.
        /// </summary>
        /// <param name="context">An <see cref="ApplicationContext"/> object containing contextual information.</param>
        /// <returns>A new <see cref="StripSccBindingsProvider"/> object.</returns>
        public static StripSccBindingsProviderBase CreateProvider(ApplicationContext context)
        {
            StripSccBindingsProviderBase provider = null;

            StripSccBindingsSection section = (StripSccBindingsSection)ConfigurationManager.GetSection(StripSccBindingsSection.SectionName);
            if (section == null)
            {
                throw new Exception("Could not find the provider section configuration in the application configuration file.");
            }
            else
            {
                ProviderSettings settings = section.Providers[context.Provider];
                if (settings == null)
                {
                    throw new Exception(string.Format("Could not find the provider '{0}' specified in the application configuration file.", context.Provider));
                }

                AssemblyQualifiedTypeNameConverter converter = new AssemblyQualifiedTypeNameConverter();
                Type type = (Type)converter.ConvertFrom(settings.Type);
                if (type != null)
                {
                    provider = (StripSccBindingsProviderBase)Activator.CreateInstance(type);
                    provider.Context = context;

                    // Initialize the provider based on the configuration file.
                    provider.Initialize(settings.Name, settings.Parameters);
                }
            }

            return provider;
        }
    }
}