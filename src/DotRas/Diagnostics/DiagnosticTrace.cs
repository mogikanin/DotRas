//--------------------------------------------------------------------------
// <copyright file="DiagnosticTrace.cs" company="Jeff Winn">
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
    using System.Diagnostics;
    using DotRas.Internal;

    /// <summary>
    /// Provides diagnostic trace logging capabilities. This class cannot be inherited.
    /// </summary>
    internal sealed class DiagnosticTrace
    {
        #region Fields

        public const string SourceName = "DotRas";

        private static DiagnosticTrace _default;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticTrace"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public DiagnosticTrace(DiagnosticSource source)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException("source");
            }

            this.Source = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default trace.
        /// </summary>
        public static DiagnosticTrace Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new DiagnosticTrace(new DiagnosticSource(new TraceSource(SourceName, SourceLevels.Off)));
                }

                return _default;
            }
        }

        /// <summary>
        /// Gets or sets the trace source.
        /// </summary>
        private DiagnosticSource Source
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="evt">The trace event data.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="evt"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public void TraceEvent(TraceEventType eventType, TraceEvent evt)
        {
            this.TraceEvent(eventType, evt, 0);
        }

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="evt">The trace event data.</param>
        /// <param name="eventId">The event id.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="evt"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public void TraceEvent(TraceEventType eventType, TraceEvent evt, int eventId)
        {
            if (evt == null)
            {
                ThrowHelper.ThrowArgumentNullException("evt");
            }

            this.Source.TraceEvent(eventType, eventId, evt);
        }

        #endregion
    }
}