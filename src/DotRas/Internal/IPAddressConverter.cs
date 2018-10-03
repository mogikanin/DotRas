//--------------------------------------------------------------------------
// <copyright file="IPAddressConverter.cs" company="Jeff Winn">
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

namespace DotRas.Internal
{
    using System;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Provides a type converter used for converting <see cref="System.Net.IPAddress"/> values.
    /// </summary>
    internal class IPAddressConverter : TypeConverter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressConverter"/> class.
        /// </summary>
        public IPAddressConverter()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">The source type.</param>
        /// <returns><b>true</b> if the conversion can be performed, otherwise <b>false</b>.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(NativeMethods.RASIPADDR) || sourceType == typeof(string))
            {
                return true;
            }
#if (WIN2K8 || WIN7 || WIN8)
            else if (sourceType == typeof(NativeMethods.RASIPV6ADDR))
            {
                return true;
            }
#endif
#if (WIN7 || WIN8)
            else if (sourceType == typeof(NativeMethods.RASTUNNELENDPOINT))
            {
                return true;
            }
#endif

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert an object to the destination type.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns><b>true</b> if the conversion can be performed, otherwise <b>false</b>.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(NativeMethods.RASIPADDR))
            {
                return true;
            }
#if (WIN2K8 || WIN7 || WIN8)
            else if (destinationType == typeof(NativeMethods.RASIPV6ADDR))
            {
                return true;
            }
#endif

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object from the type of this converter. 
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is presumed.</param>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return IPAddress.Parse((string)value);
            }
            else if (value is NativeMethods.RASIPADDR)
            {
                return new IPAddress(((NativeMethods.RASIPADDR)value).addr);
            }
#if (WIN2K8 || WIN7 || WIN8)
            else if (value is NativeMethods.RASIPV6ADDR)
            {
                return new IPAddress(((NativeMethods.RASIPV6ADDR)value).addr);
            }
#endif
#if (WIN7 || WIN8)
            else if (value is NativeMethods.RASTUNNELENDPOINT)
            {
                var endpoint = (NativeMethods.RASTUNNELENDPOINT)value;
                if (endpoint.type != NativeMethods.RASTUNNELENDPOINTTYPE.Unknown)
                {
                    switch (endpoint.type)
                    {
                        case NativeMethods.RASTUNNELENDPOINTTYPE.IPv4:
                            var addr = new byte[4];
                            Array.Copy(endpoint.addr, 0, addr, 0, 4);

                            return new IPAddress(addr);

                        case NativeMethods.RASTUNNELENDPOINTTYPE.IPv6:
                            return new IPAddress(endpoint.addr);
                    }
                }

                return null;
            }
#endif

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given object to the type of this converter.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="System.Globalization.CultureInfo"/>. If null is passed, the current culture is presumed.</param>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The destination type.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            var addr = (IPAddress)value;

            if (destinationType == typeof(NativeMethods.RASIPADDR) && (addr == null || (addr != null && addr.AddressFamily == AddressFamily.InterNetwork)))
            {
                var ipv4 = new NativeMethods.RASIPADDR();

                ipv4.addr = value == null ? IPAddress.Any.GetAddressBytes() : addr.GetAddressBytes();

                return ipv4;
            }
#if (WIN2K8 || WIN7 || WIN8)
            else if (destinationType == typeof(NativeMethods.RASIPV6ADDR) && (addr == null || (addr != null && addr.AddressFamily == AddressFamily.InterNetworkV6)))
            {
                var ipv6 = new NativeMethods.RASIPV6ADDR
                {
                    addr = addr == null ? IPAddress.IPv6Any.GetAddressBytes() : addr.GetAddressBytes()
                };
                return ipv6;
            }
#endif

#if (WIN7 || WIN8)
            else if (destinationType == typeof(NativeMethods.RASTUNNELENDPOINT))
            {
                var endpoint = new NativeMethods.RASTUNNELENDPOINT();

                if (addr != null)
                {
                    var bytes = new byte[16];
                    var actual = addr.GetAddressBytes();

                    // Transfer the bytes to the 
                    Array.Copy(actual, bytes, actual.Length);

                    switch (addr.AddressFamily)
                    {
                        case AddressFamily.InterNetwork:
                            endpoint.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4;
                            break;

                        case AddressFamily.InterNetworkV6:
                            endpoint.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv6;
                            break;

                        default:
                            endpoint.type = NativeMethods.RASTUNNELENDPOINTTYPE.Unknown;
                            break;
                    }

                    endpoint.addr = bytes;
                }

                return endpoint;
            }

#endif

            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}