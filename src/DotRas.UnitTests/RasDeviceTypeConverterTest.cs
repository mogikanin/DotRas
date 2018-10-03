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
            var converter = TypeDescriptor.GetConverter(typeof(RasDeviceType));
            Assert.IsTrue(converter is RasDeviceTypeConverter);
        }

        /// <summary>
        /// Tests the CanConvertFrom method to ensure false is returned if the value being converted from is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertFromObjectTest()
        {
            var expected = false;

            var target = new RasDeviceTypeConverter();
            var actual = target.CanConvertFrom(typeof(object));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertFrom method to ensure true is returned if the value being converted from is a string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertFromStringTest()
        {
            var expected = true;

            var target = new RasDeviceTypeConverter();
            var actual = target.CanConvertFrom(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertTo method to ensure true is returned if the value being converted to is a string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertToStringTest()
        {
            var expected = true;

            var target = new RasDeviceTypeConverter();
            var actual = target.CanConvertTo(typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the CanConvertTo method to ensure false is returned if the value being converted to is an object.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CanConvertToObjectTest()
        {
            var expected = false;

            var target = new RasDeviceTypeConverter();
            var actual = target.CanConvertTo(typeof(object));

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
            var target = new RasDeviceTypeConverter();
            var actual = target.ConvertFrom(new object());
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Modem"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromModemTest()
        {
            var expected = RasDeviceType.Modem;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Modem);

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
            var target = new RasDeviceTypeConverter();
            target.ConvertFromString(" ");
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Isdn"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromIsdnTest()
        {
            var expected = RasDeviceType.Isdn;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Isdn);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.X25"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromX25Test()
        {
            var expected = RasDeviceType.X25;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_X25);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Vpn"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromVpnTest()
        {
            var expected = RasDeviceType.Vpn;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Vpn);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Pad"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromPadTest()
        {
            var expected = RasDeviceType.Pad;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Pad);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Generic"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromGenericTest()
        {
            var expected = RasDeviceType.Generic;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Generic);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Serial"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSerialTest()
        {
            var expected = RasDeviceType.Serial;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Serial);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.FrameRelay"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromFrameRelayTest()
        {
            var expected = RasDeviceType.FrameRelay;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_FrameRelay);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Sonet"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSonetTest()
        {
            var expected = RasDeviceType.Sonet;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Sonet);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.SW56"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromSW56Test()
        {
            var expected = RasDeviceType.SW56;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_SW56);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Irda"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromIrdaTest()
        {
            var expected = RasDeviceType.Irda;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Irda);

            Assert.AreEqual<RasDeviceType>(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertFromString method to ensure a <see cref="RasDeviceType.Parallel"/> is returned.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertFromParallelTest()
        {
            var expected = RasDeviceType.Parallel;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_Parallel);

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
            var expected = RasDeviceType.PPPoE;

            var target = new RasDeviceTypeConverter();
            var actual = (RasDeviceType)target.ConvertFromString(NativeMethods.RASDT_PPPoE);

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
            var target = new RasDeviceTypeConverter();
            target.ConvertTo(RasDeviceType.Modem, typeof(object));
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Modem' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToModemTest()
        {
            var expected = NativeMethods.RASDT_Modem;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Modem, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Isdn' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToIsdnTest()
        {
            var expected = NativeMethods.RASDT_Isdn;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Isdn, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_X25' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToX25Test()
        {
            var expected = NativeMethods.RASDT_X25;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.X25, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Vpn' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToVpnTest()
        {
            var expected = NativeMethods.RASDT_Vpn;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Vpn, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Pad' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToPadTest()
        {
            var expected = NativeMethods.RASDT_Pad;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Pad, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Generic' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToGenericTest()
        {
            var expected = NativeMethods.RASDT_Generic;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Generic, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Serial' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSerialTest()
        {
            var expected = NativeMethods.RASDT_Serial;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Serial, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_FrameRelay' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToFrameRelayTest()
        {
            var expected = NativeMethods.RASDT_FrameRelay;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.FrameRelay, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Atm' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToAtmTest()
        {
            var expected = NativeMethods.RASDT_Atm;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Atm, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Sonet' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSonetTest()
        {
            var expected = NativeMethods.RASDT_Sonet;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Sonet, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_SW56' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToSW56Test()
        {
            var expected = NativeMethods.RASDT_SW56;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.SW56, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Irda' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToIrdaTest()
        {
            var expected = NativeMethods.RASDT_Irda;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Irda, typeof(string));

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the ConvertTo method to ensure 'NativeMethods.RASDT_Parallel' is returned as expected.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void ConvertToParallelTest()
        {
            var expected = NativeMethods.RASDT_Parallel;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.Parallel, typeof(string));

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
            var expected = NativeMethods.RASDT_PPPoE;

            var target = new RasDeviceTypeConverter();
            var actual = (string)target.ConvertTo(RasDeviceType.PPPoE, typeof(string));

            Assert.AreEqual(expected, actual);
        }
#endif

        #endregion
    }
}