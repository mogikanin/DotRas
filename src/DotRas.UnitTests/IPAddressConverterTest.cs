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

        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressConverterTest"/> class.
        /// </summary>
        public IPAddressConverterTest()
        {
        }

        #endregion

        [TestMethod]
        public void CanConvertFromUnsupportedType()
        {
            bool expected = false;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertFrom(typeof(bool));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertFromRASIPADDRTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertFrom(typeof(NativeMethods.RASIPADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertFromStringTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertFrom(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToUnsupportedTypeTest()
        {
            bool expected = false;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertTo(typeof(bool));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToRASIPADDRTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertTo(typeof(NativeMethods.RASIPADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromRASIPADDRTest()
        {
            byte[] expected = IPAddress.Loopback.GetAddressBytes();

            NativeMethods.RASIPADDR value = new NativeMethods.RASIPADDR();
            value.addr = expected;

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(value);

            byte[] actual = result.GetAddressBytes();

            Assert.AreEqual<System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily.InterNetwork, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromStringTest()
        {
            string expected = IPAddress.Loopback.ToString();

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(expected);

            Assert.IsNotNull(result);

            string actual = result.ToString();

            Assert.AreEqual(expected, actual, true);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertFromUnsupportedTypeTest()
        {
            IPAddressConverter target = new IPAddressConverter();
            target.ConvertFrom(true);
        }

        [TestMethod]
        public void ConvertToRASIPADDRTest()
        {
            byte[] expected = IPAddress.Loopback.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASIPADDR result = (NativeMethods.RASIPADDR)target.ConvertTo(IPAddress.Loopback, typeof(NativeMethods.RASIPADDR));

            byte[] actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToRASIPADDRFromIPv6Test()
        {
            IPAddressConverter target = new IPAddressConverter();
            target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASIPADDR));
        }

        [TestMethod]
        public void ConvertToRASIPADDRFromNullTest()
        {
            byte[] expected = IPAddress.Any.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASIPADDR result = (NativeMethods.RASIPADDR)target.ConvertTo(null, typeof(NativeMethods.RASIPADDR));

            byte[] actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToUnsupportedTypeTest()
        {
            IPAddressConverter target = new IPAddressConverter();
            target.ConvertTo(null, typeof(bool));
        }

#if (WIN2K8 || WIN7 || WIN8)

        [TestMethod]
        public void CanConvertFromRASIPV6ADDRTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertFrom(typeof(NativeMethods.RASIPV6ADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CanConvertToRASIPV6ADDRTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertTo(typeof(NativeMethods.RASIPV6ADDR));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromRASIPV6ADDRTest()
        {
            byte[] expected = IPAddress.IPv6Loopback.GetAddressBytes();

            NativeMethods.RASIPV6ADDR value = new NativeMethods.RASIPV6ADDR();
            value.addr = expected;

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(value);

            byte[] actual = result.GetAddressBytes();

            Assert.AreEqual<System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily.InterNetworkV6, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToRASIPV6ADDRTest()
        {
            byte[] expected = IPAddress.IPv6Loopback.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASIPV6ADDR result = (NativeMethods.RASIPV6ADDR)target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASIPV6ADDR));

            byte[] actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertToRASIPV6ADDRFromNullTest()
        {
            byte[] expected = IPAddress.IPv6Any.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASIPV6ADDR result = (NativeMethods.RASIPV6ADDR)target.ConvertTo(null, typeof(NativeMethods.RASIPV6ADDR));

            byte[] actual = result.addr;

            CollectionAssert.AreEqual(expected, actual);
        }

#endif

#if (WIN7 || WIN8)

        [TestMethod]
        public void CanConvertFromRASTUNNELENDPOINTTest()
        {
            bool expected = true;

            IPAddressConverter target = new IPAddressConverter();
            bool actual = target.CanConvertFrom(typeof(NativeMethods.RASTUNNELENDPOINT));

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromIPv4RASTUNNELENDPOINTTest()
        {
            byte[] expected = IPAddress.Loopback.GetAddressBytes();

            NativeMethods.RASTUNNELENDPOINT value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv4;
            value.addr = expected;

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(value);

            byte[] actual = result.GetAddressBytes();

            Assert.AreEqual<System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily.InterNetwork, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromIPv6RASTUNNELENDPOINTTest()
        {
            byte[] expected = IPAddress.IPv6Loopback.GetAddressBytes();

            NativeMethods.RASTUNNELENDPOINT value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.IPv6;
            value.addr = expected;

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(value);

            byte[] actual = result.GetAddressBytes();

            Assert.AreEqual<System.Net.Sockets.AddressFamily>(System.Net.Sockets.AddressFamily.InterNetworkV6, result.AddressFamily);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ConvertFromUnknownRASTUNNELENDPOINTTest()
        {
            NativeMethods.RASTUNNELENDPOINT value = new NativeMethods.RASTUNNELENDPOINT();
            value.type = NativeMethods.RASTUNNELENDPOINTTYPE.Unknown;

            IPAddressConverter target = new IPAddressConverter();
            IPAddress result = (IPAddress)target.ConvertFrom(value);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertToIPv4RASTUNNELENDPOINTTest()
        {
            byte[] expected = IPAddress.Loopback.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASTUNNELENDPOINT result = (NativeMethods.RASTUNNELENDPOINT)target.ConvertTo(IPAddress.Loopback, typeof(NativeMethods.RASTUNNELENDPOINT));

            byte[] actual = result.addr;

            Assert.AreEqual<NativeMethods.RASTUNNELENDPOINTTYPE>(NativeMethods.RASTUNNELENDPOINTTYPE.IPv4, result.type);
            CollectionAssert.IsSubsetOf(expected, actual);
        }

        [TestMethod]
        public void ConvertToIPv6RASTUNNELENDPOINTTest()
        {
            byte[] expected = IPAddress.IPv6Loopback.GetAddressBytes();

            IPAddressConverter target = new IPAddressConverter();
            NativeMethods.RASTUNNELENDPOINT result = (NativeMethods.RASTUNNELENDPOINT)target.ConvertTo(IPAddress.IPv6Loopback, typeof(NativeMethods.RASTUNNELENDPOINT));

            byte[] actual = result.addr;

            Assert.AreEqual<NativeMethods.RASTUNNELENDPOINTTYPE>(NativeMethods.RASTUNNELENDPOINTTYPE.IPv6, result.type);
            CollectionAssert.AreEqual(expected, actual);
        }

#endif
    }
}