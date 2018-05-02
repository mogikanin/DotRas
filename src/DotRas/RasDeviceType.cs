//--------------------------------------------------------------------------
// <copyright file="RasDeviceType.cs" company="Jeff Winn">
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

    /// <summary>
    /// Defines the remote access service (RAS) device types.
    /// </summary>
    [TypeConverter(typeof(RasDeviceTypeConverter))]
    public enum RasDeviceType
    {
        /// <summary>
        /// A modem accessed through a COM port.
        /// </summary>
        Modem,

        /// <summary>
        /// An ISDN card with a corresponding NDISWAN driver installed.
        /// </summary>
        Isdn,

        /// <summary>
        /// An X.25 card with a corresponding NDISWAN driver installed.
        /// </summary>
        X25,

        /// <summary>
        /// A virtual private network connection.
        /// </summary>
        Vpn,

        /// <summary>
        /// A packet assembler/disassembler.
        /// </summary>
        Pad,

        /// <summary>
        /// Generic device type.
        /// </summary>
        Generic,

        /// <summary>
        /// Direct serial connection through a serial port.
        /// </summary>
        Serial,

        /// <summary>
        /// Frame Relay.
        /// </summary>
        FrameRelay,

        /// <summary>
        /// Asynchronous Transfer Mode (ATM).
        /// </summary>
        Atm,

        /// <summary>
        /// Sonet device type.
        /// </summary>
        Sonet,

        /// <summary>
        /// Switched 56K access.
        /// </summary>
        SW56,

        /// <summary>
        /// An Infrared Data Association (IrDA) compliant device.
        /// </summary>
        Irda,

        /// <summary>
        /// Direct parallel connection through a parallel port.
        /// </summary>
        Parallel,

        /// <summary>
        /// Point-to-Point Protocol over Ethernet.
        /// <para>
        /// <b>Windows XP or later:</b> This value is supported.
        /// </para>
        /// </summary>
        PPPoE
    }
}