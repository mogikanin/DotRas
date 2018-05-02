//--------------------------------------------------------------------------
// <copyright file="StripSccBindingsSection.cs" company="Jeff Winn">
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

namespace StripSccBindings.Configuration
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Represents a configuration section for the application within a configuration file. This class cannot be inherited.
    /// </summary>
    internal sealed class StripSccBindingsSection : ConfigurationSection
    {
        #region Fields

        /// <summary>
        /// Defines the name of the section.
        /// </summary>
        public const string SectionName = "stripSccBindings";

        /// <summary>
        /// Defines the name of the providers property.
        /// </summary>
        public const string ProvidersProperty = "providers";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Configuration.StripSccBindingsSection"/> class.
        /// </summary>
        public StripSccBindingsSection()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a collection of providers available to the application.
        /// </summary>
        [ConfigurationProperty(ProvidersProperty, IsRequired = true)]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)this[ProvidersProperty]; }
            set { this[ProvidersProperty] = value; }
        }

        #endregion
    }
}