//--------------------------------------------------------------------------
// <copyright file="RasPhoneBookDialogEventArgs.cs" company="Jeff Winn">
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

    /// <summary>
    /// Provides data for <see cref="RasPhoneBookDialog"/> events.
    /// </summary>
    public class RasPhoneBookDialogEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasPhoneBookDialogEventArgs"/> class.
        /// </summary>
        /// <param name="callbackId">The application defined value that was passed to the event.</param>
        /// <param name="text">A string whose meaning depends on the event which was raised.</param>
        /// <param name="data">A pointer to an additional buffer of data whose meaning depends on the event which was raised.</param>
        public RasPhoneBookDialogEventArgs(IntPtr callbackId, string text, IntPtr data)
        {
            CallbackId = callbackId;
            Text = text;
            Data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an application defined value that was passed to the event.
        /// </summary>
        public IntPtr CallbackId
        {
            get;
        }

        /// <summary>
        /// Gets a string whose meaning depends on the event which was raised.
        /// </summary>
        public string Text
        {
            get;
        }

        /// <summary>
        /// Gets a pointer to an additional buffer of data whose meaning depends on the event which was raised.
        /// </summary>
        public IntPtr Data
        {
            get;
        }

        #endregion
    }
}