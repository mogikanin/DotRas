//--------------------------------------------------------------------------
// <copyright file="MarshalStructTraceEvent.cs" company="Jeff Winn">
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

namespace DotRas.Diagnostics
{
    using System;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Represents the trace event for marshaling an unmanaged memory block to a structure. This class cannot be inherited.
    /// </summary>
    internal sealed class MarshalStructTraceEvent : TraceEvent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MarshalStructTraceEvent"/> class.
        /// </summary>
        /// <param name="result">The result of the marshaling call.</param>
        public MarshalStructTraceEvent(object result)
        {
            Result = result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the result of the marshaling call.
        /// </summary>
        public object Result
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the event.
        /// </summary>
        /// <returns>The serialized event data.</returns>
        public override string Serialize()
        {
            if (Result == null)
            {
                return "Result is [NULL].";
            }

            var result = SerializeObject(Result);
            if (result == null)
            {
                return "Result could not be serialized.";
            }

            return result.ToString();
        }

        private static bool ShouldSerialize(Type t)
        {
            return !t.IsEnum && !t.IsPrimitive && !t.IsArray;
        }

        private static object SerializeObject(object obj)
        {
            var t = obj.GetType();

            var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fields != null && fields.Length > 0)
            {
                var sb = new StringBuilder();

                foreach (var field in fields)
                {
                    var value = ShouldSerialize(field.FieldType) ? SerializeObject(field.GetValue(obj)) : field.GetValue(obj);

                    var arr = value as Array;
                    if (arr != null)
                    {
                        var sb2 = new StringBuilder();

                        sb2.Append("{ ");

                        for (var index = 0; index < arr.Length; index++)
                        {
                            var current = arr.GetValue(index);

                            if (ShouldSerialize(current.GetType()))
                            {
                                current = SerializeObject(current);
                            }

                            sb2.Append(current);

                            if (index < arr.Length - 1)
                            {
                                sb2.Append(", ");
                            }
                        }

                        sb2.Append(" }");

                        value = sb2.ToString();
                    }

                    sb.AppendFormat("{0}: '{1}'", field.Name, value).AppendLine();
                }

                return sb.ToString();
            }

            return obj;
        }

        #endregion
    }
}