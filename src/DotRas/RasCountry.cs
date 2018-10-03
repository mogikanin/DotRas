//--------------------------------------------------------------------------
// <copyright file="RasCountry.cs" company="Jeff Winn">
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
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using Internal;

    /// <summary>
    /// Represents country or region specific dialing information. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to retrieve all countries from the Windows Telephony list.
    /// <code lang="C#">
    /// <![CDATA[
    /// ReadOnlyCollection<RasCountry> countries = RasCountry.GetCountries();
    /// foreach (RasCountry country in countries)
    /// {
    ///     // Do something useful.
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim countries As ReadOnlyCollection(Of RasCountry) = RasCountry.GetCountries();
    /// For Each country As RasCountry in countries
    ///     ' Do something useful.
    /// Next
    /// ]]>
    /// </code>
    /// </example>
    [Serializable]
    [DebuggerDisplay("Id = {Id}, Name = {Name}")]
    public sealed class RasCountry
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasCountry"/> class.
        /// </summary>
        /// <param name="id">The TAPI identifier.</param>
        /// <param name="code">The country or region code.</param>
        /// <param name="name">The name of the country.</param>
        internal RasCountry(int id, int code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the TAPI identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the country or region code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows Telephony list of countries/regions for a specific country id.
        /// </summary>
        /// <param name="countryId">The country id to retrieve.</param>
        /// <returns>A new <see cref="DotRas.RasCountry"/> object.</returns>
        public static RasCountry GetCountryById(int countryId)
        {
            return RasHelper.Instance.GetCountry(countryId, out _);
        }

        /// <summary>
        /// Retrieves country/region specific dialing information from the Windows Telephony list of countries/regions.
        /// </summary>
        /// <returns>A collection of <see cref="RasCountry"/> objects.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This should not be a property.")]
        public static ReadOnlyCollection<RasCountry> GetCountries()
        {
            var tempCollection = new Collection<RasCountry>();

            // The country id must be set to 1 to initiate retrieval of all countries in the list.
            var countryId = 1;

            do
            {
                var country = RasHelper.Instance.GetCountry(countryId, out countryId);
                if (country != null)
                {
                    tempCollection.Add(country);
                }
            }
            while (countryId != 0);

            return new ReadOnlyCollection<RasCountry>(tempCollection);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="DotRas.RasCountry"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="DotRas.RasCountry"/>.</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}