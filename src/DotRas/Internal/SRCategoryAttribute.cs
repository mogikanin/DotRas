//--------------------------------------------------------------------------
// <copyright file="SRCategoryAttribute.cs" company="Jeff Winn">
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
    using Properties;

    /// <summary>
    /// Specifies the name of the category in which to group the property or event based on the string resource specified. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRCategoryAttribute : CategoryAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRCategoryAttribute"/> class.
        /// </summary>
        /// <param name="resource">The name of the resource.</param>
        public SRCategoryAttribute(string resource)
            : base(resource)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Overridden. Looks up the name of the category from the resource manager.
        /// </summary>
        /// <param name="value">The string resource containing the category name.</param>
        /// <returns>The category name.</returns>
        protected override string GetLocalizedString(string value)
        {
            string retval = Resources.ResourceManager.GetString(value);
            if (string.IsNullOrEmpty(retval))
            {
                retval = value;
            }

            return retval;
        }

        #endregion
    }
}