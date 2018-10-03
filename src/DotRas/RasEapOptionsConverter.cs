//--------------------------------------------------------------------------
// <copyright file="RasEapOptionsConverter.cs" company="Jeff Winn">
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

namespace DotRas
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using Internal;

    /// <summary>
    /// Provides methods to convert a <see cref="RasEapOptions"/> instance from one data type to another. Access this class through the <see cref="System.ComponentModel.TypeDescriptor"/> object.
    /// </summary>
    public class RasEapOptionsConverter : TypeConverter
    {
        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Indicates whether this converter can convert an object of a given type to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
        /// <returns><b>true</b> if this converter can perform the conversion, otherwise <b>false</b>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Indicates whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
        /// <returns><b>true</b> if this converter can perform the conversion, otherwise <b>false</b>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="System.Object"/> to convert.</param>
        /// <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var data = (string)value;
            if (data == null)
            {
                return base.ConvertFrom(context, culture, value);
            }

            data = data.Trim();
            if (data.Length == 0)
            {
                return null;
            }

            var items = data.Split(culture.TextInfo.ListSeparator.ToCharArray());
            var converter = TypeDescriptor.GetConverter(typeof(bool));

            var retval = new RasEapOptions
            {
                NonInteractive = (bool) converter.ConvertFromString(context, culture, items[0]),
                LogOn = (bool) converter.ConvertFromString(context, culture, items[1]),
                Preview = (bool) converter.ConvertFromString(context, culture, items[2])
            };

            return retval;
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The <see cref="System.Object"/> to convert.</param>
        /// <param name="destinationType">The <see cref="System.Type"/> to convert the <paramref name="value"/> parameter to.</param>
        /// <returns>An <see cref="System.Object"/> that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                ThrowHelper.ThrowArgumentNullException("destinationType");
            }

            var options = value as RasEapOptions;
            if (options != null)
            {
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var constructor = typeof(RasEapOptions).GetConstructor(new Type[] { typeof(bool), typeof(bool), typeof(bool) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { options.NonInteractive, options.LogOn, options.Preview });
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Returns a collection of properties for the type of array specified by the value parameter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="value">An <see cref="System.Object"/> that specifies the type of array for which to get properties.</param>
        /// <param name="attributes">An array of attributes that is used as a filter.</param>
        /// <returns>A <see cref="PropertyDescriptorCollection"/> with the properties that are exposed for this data type, or a null reference (<b>Nothing</b> in Visual Basic) if there are no properties.</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(RasEapOptions), attributes);
        }

        /// <summary>
        /// Indicates whether this object supports properties, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <returns><b>true</b> if <see cref="GetProperties"/> should be called to find the properties of this object, otherwise <b>false</b>.</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        #endregion
    }
}