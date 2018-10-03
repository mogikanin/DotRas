//--------------------------------------------------------------------------
// <copyright file="DiagnosticSource.cs" company="Jeff Winn">
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
    using System.Diagnostics;
    using Internal;

    /// <summary>
    /// Represents a diagnostic source. This class cannot be inherited.
    /// </summary>
    internal sealed class DiagnosticSource
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagnosticSource"/> class.
        /// </summary>
        /// <param name="source">The trace source.</param>
        public DiagnosticSource(TraceSource source)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException("source");
            }

            InnerSource = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the inner source.
        /// </summary>
        private TraceSource InnerSource
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="eventId">The event id.</param>
        /// <param name="evt">The trace event data.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="evt"/> is a null reference (<b>Nothing</b> in Visual Basic).</exception>
        public void TraceEvent(TraceEventType eventType, int eventId, TraceEvent evt)
        {
            if (evt == null)
            {
                ThrowHelper.ThrowArgumentNullException("evt");
            }

            if (!InnerSource.Switch.ShouldTrace(eventType))
            {
                return;
            }

            InnerSource.TraceEvent(eventType, eventId, evt.Serialize());
        }

        #endregion
    }
}