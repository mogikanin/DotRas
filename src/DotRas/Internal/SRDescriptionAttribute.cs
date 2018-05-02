//--------------------------------------------------------------------------
// <copyright file="SRDescriptionAttribute.cs" company="Jeff Winn">
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
    using DotRas.Properties;

    /// <summary>
    /// Specifies a description for a property event based on the string resource specified. This class cannot be inherited.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        #region Fields

        private bool replaced;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SRDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="resource">The name of the resource.</param>
        public SRDescriptionAttribute(string resource)
            : base(resource)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the description stored in this attribute.
        /// </summary>
        public override string Description
        {
            get
            {
                if (!this.replaced)
                {
                    this.replaced = true;
                    this.DescriptionValue = Resources.ResourceManager.GetString(base.Description);
                }

                return base.Description;
            }
        }

        #endregion
    }
}