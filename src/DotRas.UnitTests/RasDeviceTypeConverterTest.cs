//--------------------------------------------------------------------------
// <copyright file="RasDeviceTypeConverterTest.cs" company="Jeff Winn">
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
    using System.ComponentModel;
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasDeviceTypeConverter"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasDeviceTypeConverterTest : UnitTest
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDeviceTypeConverterTest"/> class.
        /// </summary>
        public RasDeviceTypeConverterTest()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the test instance.
        /// </summary>
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Tests the RasDeviceType enum to ensure the RasDeviceTypeConverter is the current type converter.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void RasDeviceTypeDescriptorTest()
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(RasDeviceType));
            Assert.IsTrue(converter is RasDeviceTypeConverter);
        }

        /// <summary>
        /// Tests the CanConvertFrom method to ensure false is returned if the value being converted from is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertFromObjectTest()
        {
            bool expected = false;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            bool actual = target.CanConvertFrom(typeof(object));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertFrom method to ensure true is returned if the value being converted from is a string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertFromStringTest()
        {
            bool expected = true;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            bool actual = target.CanConvertFrom(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertTo method to ensure true is returned if the value being converted to is a string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertToStringTest()
        {
            bool expected = true;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            bool actual = target.CanConvertTo(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertTo method to ensure false is returned if the value being converted to is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertToObjectTest()
        {
            bool expected = false;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            bool actual = target.CanConvertTo(typeof(object));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFrom method to ensure an exception is thrown if the value being converted is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertFromObjectTest()
        {
            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            object actual = target.ConvertFrom(new object());
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Modem"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromModemTest()
        {
            RasDeviceType expected = RasDeviceType.Modem;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Modem);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure an exception is thrown if the string could not be found.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertFromInvalidStringTest()
        {
            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            target.ConvertFromString(" ");
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Isdn"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromIsdnTest()
        {
            RasDeviceType expected = RasDeviceType.Isdn;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Isdn);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.X25"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromX25Test()
        {
            RasDeviceType expected = RasDeviceType.X25;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_X25);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Vpn"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromVpnTest()
        {
            RasDeviceType expected = RasDeviceType.Vpn;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Vpn);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Pad"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromPadTest()
        {
            RasDeviceType expected = RasDeviceType.Pad;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Pad);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Generic"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromGenericTest()
        {
            RasDeviceType expected = RasDeviceType.Generic;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Generic);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Serial"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSerialTest()
        {
            RasDeviceType expected = RasDeviceType.Serial;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Serial);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.FrameRelay"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromFrameRelayTest()
        {
            RasDeviceType expected = RasDeviceType.FrameRelay;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_FrameRelay);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Sonet"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSonetTest()
        {
            RasDeviceType expected = RasDeviceType.Sonet;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Sonet);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.SW56"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSW56Test()
        {
            RasDeviceType expected = RasDeviceType.SW56;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_SW56);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Irda"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromIrdaTest()
        {
            RasDeviceType expected = RasDeviceType.Irda;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Irda);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Parallel"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromParallelTest()
        {
            RasDeviceType expected = RasDeviceType.Parallel;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Parallel);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.PPPoE"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromPPPoETest()
        {
            RasDeviceType expected = RasDeviceType.PPPoE;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            RasDeviceType actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_PPPoE);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }
#endif

        /// <summary>
        /// Tests the ConvertTo method to ensure an exception is thrown when the destination type is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(NotSupportedException))]
        public void ConvertToObjectTest()
        {
            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            target.ConvertTo(RasDeviceType.Modem, typeof(object));
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Modem' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToModemTest()
        {
            string expected = NativeMethods.RASDT_Modem;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Modem, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Isdn' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToIsdnTest()
        {
            string expected = NativeMethods.RASDT_Isdn;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Isdn, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_X25' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToX25Test()
        {
            string expected = NativeMethods.RASDT_X25;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.X25, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Vpn' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToVpnTest()
        {
            string expected = NativeMethods.RASDT_Vpn;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Vpn, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Pad' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToPadTest()
        {
            string expected = NativeMethods.RASDT_Pad;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Pad, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Generic' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToGenericTest()
        {
            string expected = NativeMethods.RASDT_Generic;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Generic, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Serial' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSerialTest()
        {
            string expected = NativeMethods.RASDT_Serial;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Serial, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_FrameRelay' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToFrameRelayTest()
        {
            string expected = NativeMethods.RASDT_FrameRelay;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.FrameRelay, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Atm' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToAtmTest()
        {
            string expected = NativeMethods.RASDT_Atm;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Atm, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Sonet' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSonetTest()
        {
            string expected = NativeMethods.RASDT_Sonet;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Sonet, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_SW56' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSW56Test()
        {
            string expected = NativeMethods.RASDT_SW56;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.SW56, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Irda' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToIrdaTest()
        {
            string expected = NativeMethods.RASDT_Irda;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Irda, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Parallel' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToParallelTest()
        {
            string expected = NativeMethods.RASDT_Parallel;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.Parallel, typeof(string));

            Assert.AreEqual(expected, actual);
        }

#if (WINXP || WIN2K8 || WIN7 || WIN8)
        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_PPPoE' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToPPPoETest()
        {
            string expected = NativeMethods.RASDT_PPPoE;

            RasDeviceTypeConverter target = new RasDeviceTypeConverter();
            string actual = (string)target.ConvertTo(RasDeviceType.PPPoE, typeof(string));

            Assert.AreEqual(expected, actual);
        }
#endif

        #endregion
    }
}