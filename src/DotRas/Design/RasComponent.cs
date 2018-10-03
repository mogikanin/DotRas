//--------------------------------------------------------------------------
// <copyright file="RasComponent.cs" company="Jeff Winn">
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

namespace DotRas.Design
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using Internal;

    /// <summary>
    /// Provides the base implementation for remote access service (RAS) components. This class must be inherited. 
    /// </summary>
    public abstract class RasComponent : Component
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasComponent"/> class.
        /// </summary>
        protected RasComponent()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.Design.RasComponent"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain this component.</param>
        protected RasComponent(IContainer container)
        {
            if (container != null)
            {
                container.Add(this);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the component has encountered an error.
        /// </summary>
        [SRDescription("RCErrorDesc")]
        public event EventHandler<ErrorEventArgs> Error;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the object used to marshal event-handler calls that are issued by the component.
        /// </summary>
        /// <remarks>This property is only required if you need to marshal events raised by the component back to another thread. Typically this is only needed if you're using a user interface, applications like Windows services do not require any thread marshaling.</remarks>
        [DefaultValue(null)]
        [SRDescription("RCSyncObjectDesc")]
        public ISynchronizeInvoke SynchronizingObject
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected virtual void InitializeComponent()
        {
        }

        /// <summary>
        /// Raises the <see cref="Error"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.IO.ErrorEventArgs"/> containing event data.</param>
        protected void OnError(ErrorEventArgs e)
        {
            RaiseEvent<ErrorEventArgs>(Error, e);
        }

        /// <summary>
        /// Raises the event specified by <paramref name="method"/> with the event data provided. 
        /// </summary>
        /// <typeparam name="TEventArgs">The <see cref="System.EventArgs"/> used by the event delegate.</typeparam>
        /// <param name="method">The event delegate being raised.</param>
        /// <param name="e">An <typeparamref name="TEventArgs"/> containing event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "The design is ok. This method is used to raise events on multi-threaded components.")]
        protected void RaiseEvent<TEventArgs>(EventHandler<TEventArgs> method, TEventArgs e) where TEventArgs : EventArgs
        {
            if (method != null && CanRaiseEvents)
            {
                lock (method)
                {
                    if (SynchronizingObject != null && SynchronizingObject.InvokeRequired)
                    {
                        SynchronizingObject.Invoke(method, new object[] { this, e });
                    }
                    else
                    {
                        method(this, e);
                    }
                }
            }
        }

        #endregion
    }
}