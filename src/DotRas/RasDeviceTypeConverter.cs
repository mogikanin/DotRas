//--------------------------------------------------------------------------
// <copyright file="RasDeviceTypeConverter.cs" company="Jeff Winn">
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
    using System.Globalization;
    using Internal;
    using Properties;

    /// <summary>
    /// Provides a converter for <see cref="RasDeviceType"/>. This class cannot be inherited.
    /// </summary>
    public sealed class RasDeviceTypeConverter : TypeConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDeviceTypeConverter"/> class.
        /// </summary>
        public RasDeviceTypeConverter()
        {
        }

        #endregion

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A <see cref="Type"/> that represents the type you want to convert from.</param>
        /// <returns><b>true</b> if this converter can perform the conversion, otherwise <b>false</b>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
        /// <returns><b>true</b> if this converter can perform the conversion, otherwise <b>false</b>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given value to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <returns>An object that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var v = value as string;
            if (v != null)
            {
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }

                v = v.ToUpper(culture);

                if (string.Equals(v, NativeMethods.RASDT_Modem, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Modem;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Isdn, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Isdn;
                }
                else if (string.Equals(v, NativeMethods.RASDT_X25, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.X25;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Vpn, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Vpn;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Pad, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Pad;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Generic, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Generic;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Serial, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Serial;
                }
                else if (string.Equals(v, NativeMethods.RASDT_FrameRelay, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.FrameRelay;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Sonet, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Sonet;
                }
                else if (string.Equals(v, NativeMethods.RASDT_SW56, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.SW56;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Irda, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Irda;
                }
                else if (string.Equals(v, NativeMethods.RASDT_Parallel, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.Parallel;
                }
                else if (string.Equals(v, NativeMethods.RASDT_PPPoE, StringComparison.CurrentCultureIgnoreCase))
                {
                    return RasDeviceType.PPPoE;
                }
                else
                {
                    ThrowHelper.ThrowNotSupportedException(Resources.Exception_ConversionNotSupported, value);
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the arguments.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The type to convert the <paramref name="value"/> parameter to.</param>
        /// <returns>An object that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var deviceType = (RasDeviceType)value;

                switch (deviceType)
                {
                    case RasDeviceType.Modem:
                        return NativeMethods.RASDT_Modem;

                    case RasDeviceType.Isdn:
                        return NativeMethods.RASDT_Isdn;

                    case RasDeviceType.X25:
                        return NativeMethods.RASDT_X25;

                    case RasDeviceType.Vpn:
                        return NativeMethods.RASDT_Vpn;

                    case RasDeviceType.Pad:
                        return NativeMethods.RASDT_Pad;

                    case RasDeviceType.Generic:
                        return NativeMethods.RASDT_Generic;

                    case RasDeviceType.Serial:
                        return NativeMethods.RASDT_Serial;

                    case RasDeviceType.FrameRelay:
                        return NativeMethods.RASDT_FrameRelay;

                    case RasDeviceType.Atm:
                        return NativeMethods.RASDT_Atm;

                    case RasDeviceType.Sonet:
                        return NativeMethods.RASDT_Sonet;

                    case RasDeviceType.SW56:
                        return NativeMethods.RASDT_SW56;

                    case RasDeviceType.Irda:
                        return NativeMethods.RASDT_Irda;

                    case RasDeviceType.Parallel:
                        return NativeMethods.RASDT_Parallel;

                    case RasDeviceType.PPPoE:
                        return NativeMethods.RASDT_PPPoE;
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}