//--------------------------------------------------------------------------
// <copyright file="RasDialOptionsConverter.cs" company="Jeff Winn">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using Internal;

    /// <summary>
    /// Provides methods to convert a <see cref="RasDialOptions"/> instance from one data type to another. Access this class through the <see cref="System.ComponentModel.TypeDescriptor"/> object.
    /// </summary>
    public class RasDialOptionsConverter : TypeConverter
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

            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }

            var items = data.Split(culture.TextInfo.ListSeparator.ToCharArray());
            var converter = TypeDescriptor.GetConverter(typeof(bool));

            var retval = new RasDialOptions();
            retval.UsePrefixSuffix = (bool)converter.ConvertFromString(context, culture, items[0]);
            retval.PausedStates = (bool)converter.ConvertFromString(context, culture, items[1]);
            retval.SetModemSpeaker = (bool)converter.ConvertFromString(context, culture, items[2]);
            retval.SetSoftwareCompression = (bool)converter.ConvertFromString(context, culture, items[3]);
            retval.DisableConnectedUI = (bool)converter.ConvertFromString(context, culture, items[4]);
            retval.DisableReconnectUI = (bool)converter.ConvertFromString(context, culture, items[5]);
            retval.DisableReconnect = (bool)converter.ConvertFromString(context, culture, items[6]);
            retval.NoUser = (bool)converter.ConvertFromString(context, culture, items[7]);
            retval.Router = (bool)converter.ConvertFromString(context, culture, items[8]);
            retval.CustomDial = (bool)converter.ConvertFromString(context, culture, items[9]);

#if (WINXP || WIN2K8 || WIN7 || WIN8)

            retval.UseCustomScripting = (bool)converter.ConvertFromString(context, culture, items[10]);

#endif

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

            var options = value as RasDialOptions;
            if (options != null)
            {
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }

                if (destinationType == typeof(InstanceDescriptor))
                {
                    var types = new List<Type>(new Type[] { typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool), typeof(bool) });
                    var values = new List<object>(new object[] { options.UsePrefixSuffix, options.PausedStates, options.SetModemSpeaker, options.SetSoftwareCompression, options.DisableConnectedUI, options.DisableReconnectUI, options.DisableReconnect, options.NoUser, options.Router, options.CustomDial });

#if (WINXP || WIN2K8 || WIN7 || WIN8)

                    types.Add(typeof(bool));
                    values.Add(options.UseCustomScripting);

#endif

                    var constructor = typeof(RasDialOptions).GetConstructor(types.ToArray());
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, values);
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
            return TypeDescriptor.GetProperties(typeof(RasDialOptions), attributes);
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