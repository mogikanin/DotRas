//--------------------------------------------------------------------------
// <copyright file="IPAddressConverterTest.cs" company="Jeff Winn">
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

namespace DotRas.UnitTests
{
    using System;
    using System.Net;
    using DotRas.Internal;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.Internal.IPAddressConverter"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class IPAddressConverterTest
    {
        #region Constructors

        #endregion

        [TestMethod]
        public void CanConvertFromUnsupportedType()
        {
            var expected = false;

            var target = new IPAddressConverter();
            var actual = target.CanConvertFrom(typeof(bool));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertFromRASIPADDRTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertFrom(typeof(NativeMethods.RASIPADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertFromStringTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertFrom(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToUnsupportedTypeTest()
        {
            var expected = false;

            var target = new IPAddressConverter();
            var actual = target.CanConvertTo(typeof(bool));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToRASIPADDRTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertTo(typeof(NativeMethods.RASIPADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromRASIPADDRTest()
        {
            var expected = IPAddress.Loopback.GetAddressBytes();

            var value = new NativeMethods.RASIPADDR();
            value.addr = expected;

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(value);

            var actual = result.GetAddressBytes();

            Assert.AreEqual(System.Net.Sockets.AddressFamily.InterNetwork, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromStringTest()
        {
            var expected = IPAddress.Loopback.ToString();

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(expected);

            Assert.IsNotNull(result);

            var actual = result.ToString();

            Assert.AreEqual(expected, actual, true);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertFromUnsupportedTypeTest()
        {
            var target = new IPAddressConverter();
            target.ConvertFrom(true);
        }

        [TestMethod]
        public void ConvertToRASIPADDRTest()
        {
            var expected = IPAddress.Loopback.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASIPADDR)target.ConvertTo(IPAddress.Loopback, typeof(NativeMethods.RASIPADDR));

            var actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToRASIPADDRFromIPv6Test()
        {
            var target = new IPAddressConverter();
            target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASIPADDR));
        }

        [TestMethod]
        public void ConvertToRASIPADDRFromNullTest()
        {
            var expected = IPAddress.Any.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASIPADDR)target.ConvertTo(null, typeof(NativeMethods.RASIPADDR));

            var actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToUnsupportedTypeTest()
        {
            var target = new IPAddressConverter();
            target.ConvertTo(null, typeof(bool));
        }

#if (WIN2K8 || WIN7 || WIN8)

        [TestMethod]
        public void CanConvertFromRASIPV6ADDRTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertFrom(typeof(NativeMethods.RASIPV6ADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToRASIPV6ADDRTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertTo(typeof(NativeMethods.RASIPV6ADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromRASIPV6ADDRTest()
        {
            var expected = IPAddress.IPv6Loopback.GetAddressBytes();

            var value = new NativeMethods.RASIPV6ADDR();
            value.addr = expected;

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(value);

            var actual = result.GetAddressBytes();

            Assert.AreEqual(System.Net.Sockets.AddressFamily.InterNetworkV6, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToRASIPV6ADDRTest()
        {
            var expected = IPAddress.IPv6Loopback.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASIPV6ADDR)target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASIPV6ADDR));

            var actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToRASIPV6ADDRFromNullTest()
        {
            var expected = IPAddress.IPv6Any.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASIPV6ADDR)target.ConvertTo(null, typeof(NativeMethods.RASIPV6ADDR));

            var actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

#endif

#if (WIN7 || WIN8)

        [TestMethod]
        public void CanConvertFromRASTUNNELENDPOINTTest()
        {
            var expected = true;

            var target = new IPAddressConverter();
            var actual = target.CanConvertFrom(typeof(NativeMethods.RASTUNNELENDPOINT));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromIPv4RASTUNNELENDPOINTTest()
        {
            var expected = IPAddress.Loopback.GetAddressBytes();

            var value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4;
            value.addr = expected;

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(value);

            var actual = result.GetAddressBytes();

            Assert.AreEqual(System.Net.Sockets.AddressFamily.InterNetwork, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromIPv6RASTUNNELENDPOINTTest()
        {
            var expected = IPAddress.IPv6Loopback.GetAddressBytes();

            var value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv6;
            value.addr = expected;

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(value);

            var actual = result.GetAddressBytes();

            Assert.AreEqual(System.Net.Sockets.AddressFamily.InterNetworkV6, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromUnknownRASTUNNELENDPOINTTest()
        {
            var value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.Unknown;

            var target = new IPAddressConverter();
            var result = (IPAddress)target.ConvertFrom(value);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToIPv4RASTUNNELENDPOINTTest()
        {
            var expected = IPAddress.Loopback.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASTUNNELENDPOINT)target.ConvertTo(IPAddress.Loopback, typeof(NativeMethods.RASTUNNELENDPOINT));

            var actual = result.addr;

            Assert.AreEqual(NativeMethods.RASTUNNELENDPOINTTYPE.IPv4, result.type);
            CollectionAssert.IsSubsetOf(expected, actual);
        }

        [TestMethod]
        public void ConvertToIPv6RASTUNNELENDPOINTTest()
        {
            var expected = IPAddress.IPv6Loopback.GetAddressBytes();

            var target = new IPAddressConverter();
            var result = (NativeMethods.RASTUNNELENDPOINT)target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASTUNNELENDPOINT));

            var actual = result.addr;

            Assert.AreEqual(NativeMethods.RASTUNNELENDPOINTTYPE.IPv6, result.type);
            CollectionAssert.AreEqual(expected, actual);
        }

#endif
    }
}