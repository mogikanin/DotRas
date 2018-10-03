//--------------------------------------------------------------------------
// <copyright file="Luid.cs" company="Jeff Winn">
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
    using System.Runtime.InteropServices;
    using System.Text;
    using Internal;

    /// <summary>
    /// Represents a locally unique identifier (LUID).
    /// </summary>
    /// <example>
    /// This example shows how to create a new <b>Luid</b> structure.
    /// <code lang="C#">
    /// <![CDATA[
    /// Luid l = Luid.NewLuid();
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim l As Luid = Luid.NewLuid()
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct Luid : IEquatable<Luid>, IFormattable
    {
        #region Fields

        /// <summary>
        /// Represents an empty <see cref="Luid"/>. This field is read-only.
        /// </summary>
        public static readonly Luid Empty = new Luid();

        private int lowPart;
        private int highPart;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Luid"/> structure.
        /// </summary>
        /// <param name="lowPart">The low part.</param>
        /// <param name="highPart">The high part.</param>
        public Luid(int lowPart, int highPart)
        {
            this.lowPart = lowPart;
            this.highPart = highPart;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generates a new locally unique identifier.
        /// </summary>
        /// <returns>A new <see cref="DotRas.Luid"/> structure.</returns>
        /// <remarks>A <see cref="Luid"/> is guaranteed to be unique only on the system on which it was generated. Also, the uniqueness of a <see cref="Luid"/> is guaranteed only until the system is restarted.</remarks>
        public static Luid NewLuid()
        {
            return RasHelper.Instance.AllocateLocallyUniqueId();
        }

        /// <summary>
        /// Indicates whether the objects are equal.
        /// </summary>
        /// <param name="objA">The first object to check.</param>
        /// <param name="objB">The second object to check.</param>
        /// <returns><b>true</b> if the objects are equal, otherwise <b>false</b>.</returns>
        public static bool operator ==(Luid objA, Luid objB)
        {
            return objA.Equals(objB);
        }

        /// <summary>
        /// Indicates whether the objects are not equal.
        /// </summary>
        /// <param name="objA">The first object to check.</param>
        /// <param name="objB">The second object to check.</param>
        /// <returns><b>true</b> if the objects are not equal, otherwise <b>false</b>.</returns>
        public static bool operator !=(Luid objA, Luid objB)
        {
            return !objA.Equals(objB);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare the current instance to.</param>
        /// <returns><b>true</b> if the objects are equal, otherwise <b>false</b>.</returns>
        public override bool Equals(object obj)
        {
            var retval = false;

            if (obj is Luid)
            {
                retval = Equals((Luid)obj);
            }

            return retval;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other">The <see cref="DotRas.Luid"/> to compare the current instance to.</param>
        /// <returns><b>true</b> if the objects are equal, otherwise <b>false</b>.</returns>
        public bool Equals(Luid other)
        {
            return highPart == other.highPart && lowPart == other.lowPart;
        }

        /// <summary>
        /// Overridden. Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code for the instance.</returns>
        public override int GetHashCode()
        {
            var hashCode = (long)highPart + (long)lowPart;

            var retval = 0;
            if (hashCode < int.MinValue)
            {
                retval = int.MinValue;
            }
            else if (hashCode > int.MaxValue)
            {
                retval = int.MaxValue;
            }
            else if (hashCode >= int.MinValue && hashCode <= int.MaxValue)
            {
                retval = (int)hashCode;
            }
            else
            {
                retval = 0;
            }

            return retval;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representation of this locally unique identifier.
        /// </summary>
        /// <returns>A <see cref="System.String"/> representation of this locally unique identifier.</returns>
        public override string ToString()
        {
            return ToString(null, null);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representation of this locally unique identifier.
        /// </summary>
        /// <param name="format">A format indicating how to format the value of this <see cref="Luid"/>.</param>
        /// <returns>A <see cref="System.String"/> representation of this locally unique identifier.</returns>
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> representation of this locally unique identifier.
        /// </summary>
        /// <param name="format">A format indicating how to format the value of this <see cref="Luid"/>.</param>
        /// <param name="formatProvider">An <see cref="IFormatProvider"/> that supplies culture-specific formatting services.</param>
        /// <returns>A <see cref="System.String"/> representation of this locally unique identifier.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            var sb = new StringBuilder();
            sb.Append(lowPart.ToString(format, formatProvider)).Append("-").Append(highPart.ToString(format, formatProvider));

            return sb.ToString();
        }

        #endregion
    }
}