//--------------------------------------------------------------------------
// <copyright file="PInvokeCallTraceEvent.cs" company="Jeff Winn">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Internal;
    using Properties;

    /// <summary>
    /// Represents the trace event for a p/invoke call. This class cannot be inherited.
    /// </summary>
    [DebuggerDisplay("DllName = {DllName}, EntryPoint = {EntryPoint}")]
    internal sealed class PInvokeCallTraceEvent : TraceEvent
    {
        #region Fields

        private Dictionary<string, object> _data;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PInvokeCallTraceEvent"/> class.
        /// </summary>
        /// <param name="dllName">The DLL name.</param>
        /// <param name="entryPoint">The entry point name.</param>
        public PInvokeCallTraceEvent(string dllName, string entryPoint)
        {
            if (string.IsNullOrEmpty(dllName))
            {
                ThrowHelper.ThrowArgumentException("dllName", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            if (string.IsNullOrEmpty(entryPoint))
            {
                ThrowHelper.ThrowArgumentException("entryPoint", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            DllName = dllName;
            EntryPoint = entryPoint;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data associated with the trace event.
        /// </summary>
        public Dictionary<string, object> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new Dictionary<string, object>();
                }

                return _data;
            }
        }

        /// <summary>
        /// Gets the DLL name.
        /// </summary>
        public string DllName
        {
            get;
        }

        /// <summary>
        /// Gets the entry point name.
        /// </summary>
        public string EntryPoint
        {
            get;
        }

        /// <summary>
        /// Gets or sets the result code, if applicable.
        /// </summary>
        public int ResultCode
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serializes the event.
        /// </summary>
        /// <returns>The serialized event data.</returns>
        public override string Serialize()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("DllName: {0}", DllName).AppendLine();
            sb.AppendFormat("EntryPoint: {0}", EntryPoint).AppendLine();
            sb.AppendFormat("ResultCode: {0}", ResultCode).AppendLine();

            foreach (KeyValuePair<string, object> pair in Data)
            {
                object value = pair.Value;
                sb.AppendFormat("{0}: '{1}'", pair.Key, value == null ? "[NULL]" : value).AppendLine();
            }

            return sb.ToString();
        }

        #endregion
    }
}