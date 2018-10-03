//--------------------------------------------------------------------------
// <copyright file="RasDeviceTest.cs" company="Jeff Winn">
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
    using System.Collections;
    using System.Collections.ObjectModel;
    using DotRas;
    using DotRas.Internal;
    using DotRas.UnitTests.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for <see cref="DotRas.RasDevice"/> and is intended to contain all associated unit tests.
    /// </summary>
    [TestClass]
    public class RasDeviceTest : UnitTest
    {
        #region Fields

        public static readonly string ValidDeviceName = "WAN Miniport (PPTP)";
        public static readonly string InvalidDeviceName = ValidDeviceName.ToLower();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RasDeviceTest"/> class.
        /// </summary>
        public RasDeviceTest()
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
        /// Tests the Create method to ensure an empty device name can create a device.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateDeviceWithEmptyNameTest()
        {
            RasDevice target = RasDevice.Create(string.Empty, RasDeviceType.Modem);

            Assert.AreEqual(string.Empty, target.Name);
            Assert.AreEqual(RasDeviceType.Modem, target.DeviceType);
        }

        /// <summary>
        /// Tests the Create method to ensure an exception is thrown when the device type is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDeviceWithNullDeviceTypeTest()
        {
            RasDevice target = RasDevice.Create(string.Empty, null);
        }

        /// <summary>
        /// Tests the Create method to ensure an exception is thrown with the device type is an empty string.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDeviceWithEmptyDeviceTypeTest()
        {
            RasDevice target = RasDevice.Create(string.Empty, string.Empty);
        }

        /// <summary>
        /// Tests the Name property to ensure the property returns the same value as what was passed to the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void NameTest()
        {
            string expected = "Test Device";

            RasDevice target = RasDevice.Create(expected, RasDeviceType.Generic);

            string actual;
            actual = target.Name;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the DeviceType property to ensure the property returns the same value as what was passed to the constructor.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void DeviceTypeTest()
        {
            string name = "Test Device";
            RasDeviceType expected = RasDeviceType.Generic;

            RasDevice target = RasDevice.Create(name, expected);

            RasDeviceType actual;
            actual = target.DeviceType;

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the GetDevices method to ensure it returns all of the devices from the RasHelper.GetDevices method.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void GetDevicesTest()
        {
            ReadOnlyCollection<RasDevice> expected = RasHelper.Instance.GetDevices();

            ReadOnlyCollection<RasDevice> actual;
            actual = RasDevice.GetDevices();

            CollectionAssert.AreEqual(expected, actual, new RasDeviceComparer());
        }

        /////// <summary>
        /////// Tests the GetDeviceByName method to ensure an exception is thrown when the name argument is a null reference.
        /////// </summary>
        ////[TestMethod]
        ////[TestCategory(CategoryConstants.Unit)]
        ////[ExpectedException(typeof(ArgumentNullException))]
        ////public void GetDeviceByNameNullNameExceptionTest()
        ////{
        ////    RasDevice.GetDeviceByName(null, RasDeviceType.Vpn);
        ////}

        /////// <summary>
        /////// Tests the GetDeviceByName method to ensure an exception is thrown when the device type is a null reference.
        /////// </summary>
        ////[TestMethod]
        ////[ExpectedException(typeof(ArgumentException))]
        ////public void GetDeviceByNameNullDeviceTypeExceptionTest()
        ////{
        ////    RasDevice.GetDeviceByName(RasDeviceTest.ValidDeviceName, null);
        ////}

        /////// <summary>
        /////// Tests the GetDeviceByName method to ensure an exception is thrown when the device type is an empty string.
        /////// </summary>
        ////[TestMethod]
        ////[ExpectedException(typeof(ArgumentException))]
        ////public void GetDeviceByNameEmptyDeviceTypeExceptionTest()
        ////{
        ////    RasDevice.GetDeviceByName(RasDeviceTest.ValidDeviceName, string.Empty);
        ////}

        /// <summary>
        /// Tests the Create method to ensure an exception is thrown when the device name is a null reference.
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateDeviceWithNullNameTest()
        {
            RasDevice.Create(null, RasDeviceType.Modem);
        }

        /// <summary>
        /// Tests the Create method to ensure a 
        /// </summary>
        [TestMethod]
        [TestCategory(CategoryConstants.Unit)]
        public void CreateDeviceTest()
        {
            string name = "Test Device";
            RasDeviceType deviceType = RasDeviceType.Modem;

            RasDevice target = RasDevice.Create(name, deviceType);

            Assert.AreEqual(name, target.Name);
            Assert.AreEqual<RasDeviceType>(deviceType, target.DeviceType);
        }

        /////// <summary>
        /////// Tests the GetDeviceByName method for a null reference returned because of a bad device type.
        /////// </summary>
        ////[TestMethod]
        ////public void GetDeviceByNameTestWithBadDeviceType()
        ////{
        ////    string name = RasDeviceTest.ValidDeviceName;
        ////    string deviceType = " ";

        ////    RasDevice expected = null;

        ////    RasDevice actual;
        ////    actual = RasDevice.GetDeviceByName(name, deviceType);

        ////    RasDeviceComparer comparer = new RasDeviceComparer();
        ////    bool target = comparer.Compare(expected, actual) == 0;

        ////    Assert.IsTrue(target);
        ////}

        /////// <summary>
        /////// Tests the GetDeviceByName method to ensure the same object is returned with exact matching enabled.
        /////// </summary>
        ////[TestMethod]
        ////public void GetDeviceByName1TestWithBadDeviceType()
        ////{
        ////    string name = RasDeviceTest.ValidDeviceName;
        ////    string deviceType = "frumpy";
        ////    bool exactMatchOnly = true;

        ////    RasDevice expected = null;

        ////    RasDevice actual;
        ////    actual = RasDevice.GetDeviceByName(name, deviceType, exactMatchOnly);

        ////    Assert.AreEqual(expected, actual);
        ////}

        #endregion

        #region RasDeviceComparer Class

        /// <summary>
        /// Compares two <see cref="DotRas.RasDevice"/> objects for equivalence.
        /// </summary>
        private class RasDeviceComparer : IComparer
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the <see cref="RasDeviceComparer"/> class.
            /// </summary>
            public RasDeviceComparer()
            {
            }

            #endregion

            #region Methods

            /// <summary>
            /// Performs the comparison of two <see cref="DotRas.RasDevice"/> objects for equivalence.
            /// </summary>
            /// <param name="objA">The first <see cref="DotRas.RasDevice"/> object to compare.</param>
            /// <param name="objB">The second <see cref="DotRas.RasDevice"/> object to compare.</param>
            /// <returns>The relative value when comparing <paramref name="objA"/> to <paramref name="objB"/>.</returns>
            public int Compare(object objA, object objB)
            {
                RasDevice deviceA = (RasDevice)objA;
                RasDevice deviceB = (RasDevice)objB;

                if (deviceA == null && deviceB == null)
                {
                    return 0;
                }
                else if (deviceA != null && deviceB == null)
                {
                    return -1;
                }
                else if (deviceA == null && deviceB != null)
                {
                    return 1;
                }

                int retval = string.Compare(deviceA.Name, deviceB.Name, false);
                if (retval == 0)
                {
                    if ((int)deviceA.DeviceType < (int)deviceB.DeviceType)
                    {
                        return -1;
                    }
                    else if ((int)deviceA.DeviceType > (int)deviceB.DeviceType)
                    {
                        return 1;
                    }
                    else if ((int)deviceA.DeviceType == (int)deviceB.DeviceType)
                    {
                        return 0;
                    }
                }

                return retval;
            }

            #endregion
        }

        #endregion
    }
}