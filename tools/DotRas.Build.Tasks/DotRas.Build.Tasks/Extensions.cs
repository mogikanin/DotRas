//--------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Jeff Winn">
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

namespace DotRas.Build.Tasks
{
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Provides extension methods.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Appends the switch if <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="builder">The <see cref="CommandLineBuilder"/> to which the switch should be appended.</param>
        /// <param name="condition">The condition which must be met.</param>
        /// <param name="switchName">The switch name to append.</param>
        public static void AppendSwitchIfTrue(this CommandLineBuilder builder, bool condition, string switchName)
        {
            if (condition && !string.IsNullOrEmpty(switchName))
            {
                builder.AppendSwitch(switchName);
            }
        }
    }
}