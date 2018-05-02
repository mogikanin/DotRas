//--------------------------------------------------------------------------
// <copyright file="AssemblyQualifiedTypeNameConverter.cs" company="Jeff Winn">
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
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Provides a way of converting an assembly qualified type name to a <see cref="System.Type"/> instance.
    /// </summary>
    internal sealed class AssemblyQualifiedTypeNameConverter : TypeConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StripSccBindings.Configuration.AssemblyQualifiedTypeNameConverter"/> class.
        /// </summary>
        public AssemblyQualifiedTypeNameConverter()
        {
        }

        #endregion

        /// <summary>
        /// Converts the given value to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="Object"/> to convert.</param>
        /// <returns>An <see cref="Object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            Type providerType = null;

            string typeName = ((string)value).Substring(0, ((string)value).IndexOf(','));
            string assemblyName = ((string)value).Substring(typeName.Length + 1);

            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                throw new Exception("Could not load the assembly for the provider.");
            }
            else
            {
                providerType = assembly.GetType(typeName);
                if (providerType == null)
                {
                    throw new Exception("Could not locate the type requested in the assembly.");
                }
            }

            return providerType;
        }
    }
}