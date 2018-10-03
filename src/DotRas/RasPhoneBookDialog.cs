//--------------------------------------------------------------------------
// <copyright file="RasPhoneBookDialog.cs" company="Jeff Winn">
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
    using Design;
    using Internal;
    using Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// Displays the primary Dial-Up Networking dialog box. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <b>RasPhoneBookDialog</b> component to display the main dial-up networking dialog box.
    /// <code lang="C#">
    /// <![CDATA[
    /// RasPhoneBookDialog dialog = new RasPhoneBookDialog();
    /// public void Begin()
    /// {
    ///     dialog.AddedEntry += new EventHandler<RasPhoneBookDialogEventArgs>(this.dialog_AddedEntry);
    ///     dialog.ChangedEntry += new EventHandler<RasPhoneBookDialogEventArgs>(this.dialog_ChangedEntry);
    ///     dialog.DialedEntry += new EventHandler<RasPhoneBookDialogEventArgs>(this.dialog_DialedEntry);
    ///     dialog.RemovedEntry += new EventHandler<RasPhoneBookDialogEventArgs>(this.dialog_RemovedEntry);
    ///     if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    ///     {
    ///         // The dialog has closed successfully.
    ///     }
    /// }
    /// private void dialog_AddedEntry(object sender, RasPhoneBookDialogEventArgs e)
    /// {
    ///     // The dialog has added a new entry.
    /// }
    /// private void dialog_ChangedEntry(object sender, RasPhoneBookDialogEventArgs e)
    /// {
    ///     // The dialog has changed an entry.
    /// }
    /// private void dialog_DialedEntry(object sender, RasPhoneBookDialogEventArgs e)
    /// {
    ///     // The dialog has dialed an entry.
    /// }
    /// private void dialog_RemovedEntry(object sender, RasPhoneBookDialogEventArgs e)
    /// {
    ///     // The dialog removed an entry.
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim dialog As New RasPhoneBookDialog
    /// Public Sub Begin()
    ///     AddHandler dialog.AddedEntry, Me.dialog_AddedEntry
    ///     AddHandler dialog.ChangedEntry, Me.dialog_ChangedEntry
    ///     AddHandler dialog.DialedEntry, Me.dialog_DialedEntry
    ///     AddHandler dialog.RemovedEntry, Me.dialog_RemovedEntry
    ///     If (dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
    ///         ' The dialog has closed successfully.
    ///     End If
    /// End Sub
    /// Private Sub dialog_AddedEntry(ByVal sender As Object, ByVal e As RasPhoneBookDialogEventArgs)
    ///     ' The dialog has added a new entry.
    /// End Sub
    /// Private Sub dialog_ChangedEntry(ByVal sender As Object, ByVal e As RasPhoneBookDialogEventArgs)
    ///     ' The dialog has changed an entry.
    /// End Sub
    /// Private Sub dialog_DialedEntry(ByVal sender As Object, ByVal e As RasPhoneBookDialogEventArgs)
    ///     ' The dialog has dialed an entry.
    /// End Sub
    /// Private Sub dialog_RemovedEntry(ByVal sender As Object, ByVal e As RasPhoneBookDialogEventArgs)
    ///     ' The dialog removed an entry.
    /// End Sub
    /// ]]>
    /// </code>
    /// </example>
    public sealed class RasPhoneBookDialog : RasCommonDialog
    {
        #region Fields

        private NativeMethods.RasPBDlgFunc rasPhonebookDlgCallback;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasPhoneBookDialog"/> class.
        /// </summary>
        public RasPhoneBookDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the user creates a new entry or copies an existing entry.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBDAddedEntryDesc")]
        public event EventHandler<RasPhoneBookDialogEventArgs> AddedEntry;
        
        /// <summary>
        /// Occurs when the user changes an existing phone book entry.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBDChangedEntryDesc")]
        public event EventHandler<RasPhoneBookDialogEventArgs> ChangedEntry;

        /// <summary>
        /// Occurs when the user successfully dials an entry.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBDDialedEntryDesc")]
        public event EventHandler<RasPhoneBookDialogEventArgs> DialedEntry;

        /// <summary>
        /// Occurs when the user removes a phone book entry.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBDRemovedEntryDesc")]
        public event EventHandler<RasPhoneBookDialogEventArgs> RemovedEntry;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the application defined value to be passed to the events when raised.
        /// </summary>
        [Browsable(false)]
        public IntPtr CallbackId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the full path (including file name) to the phone book.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDPhoneBookPathDesc")]
        public string PhoneBookPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the entry to initially highlight.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("RPBDEntryNameDesc")]
        public string EntryName
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all <see cref="RasPhoneBookDialog"/> properties to their default values.
        /// </summary>
        public override void Reset()
        {
            PhoneBookPath = null;
            EntryName = null;
            CallbackId = IntPtr.Zero;

            base.Reset();
        }

        /// <summary>
        /// Overridden. Displays the modal dialog.
        /// </summary>
        /// <param name="hwndOwner">The handle of the window that owns the dialog box.</param>
        /// <returns><b>true</b> if the user completed the entry successfully, otherwise <b>false</b>.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            var dlg = new NativeMethods.RASPBDLG
            {
                size = Marshal.SizeOf(typeof(NativeMethods.RASPBDLG)),
                hwndOwner = hwndOwner,
                callback = rasPhonebookDlgCallback,
                callbackId = CallbackId,
                reserved = IntPtr.Zero,
                reserved2 = IntPtr.Zero
            };

            if (Location != Point.Empty)
            {
                dlg.left = Location.X;
                dlg.top = Location.Y;

                dlg.flags |= NativeMethods.RASPBDFLAG.PositionDlg;
            }

            var retval = false;
            try
            {
                retval = UnsafeNativeMethods.Instance.PhonebookDlg(PhoneBookPath, EntryName, ref dlg);
                if (!retval && dlg.error != NativeMethods.SUCCESS)
                {
                    OnError(new RasErrorEventArgs(dlg.error, RasHelper.Instance.GetRasErrorString(dlg.error)));
                }
            }
            catch (EntryPointNotFoundException)
            {
                ThrowHelper.ThrowNotSupportedException(Resources.Exception_NotSupportedOnPlatform);
            }
            catch (SecurityException)
            {
                ThrowHelper.ThrowUnauthorizedAccessException(Resources.Exception_AccessDeniedBySecurity);
            }

            return retval;
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DotRas.RasPhoneBookDialog"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rasPhonebookDlgCallback = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            rasPhonebookDlgCallback = new NativeMethods.RasPBDlgFunc(RasPhonebookDlgCallback);
        }

        /// <summary>
        /// Raises the <see cref="AddedEntry"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasPhoneBookDialogEventArgs"/> containing event data.</param>
        private void OnAddedEntry(RasPhoneBookDialogEventArgs e)
        {
            AddedEntry?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="DialedEntry"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasPhoneBookDialogEventArgs"/> containing event data.</param>
        private void OnDialedEntry(RasPhoneBookDialogEventArgs e)
        {
            DialedEntry?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="ChangedEntry"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasPhoneBookDialogEventArgs"/> containing event data.</param>
        private void OnChangedEntry(RasPhoneBookDialogEventArgs e)
        {
            ChangedEntry?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="RemovedEntry"/> event.
        /// </summary>
        /// <param name="e">An <see cref="DotRas.RasPhoneBookDialogEventArgs"/> containing event data.</param>
        private void OnRemovedEntry(RasPhoneBookDialogEventArgs e)
        {
            RemovedEntry?.Invoke(this, e);
        }

        /// <summary>
        /// Signaled by the remote access service of user activity while the dialog box is open.
        /// </summary>
        /// <param name="callbackId">An application defined value that was passed to the RasPhonebookDlg function.</param>
        /// <param name="eventType">The event that occurred.</param>
        /// <param name="text">A string whose value depends on the <paramref name="eventType"/> parameter.</param>
        /// <param name="data">Pointer to an additional buffer argument whose value depends on the <paramref name="eventType"/> parameter.</param>
        private void RasPhonebookDlgCallback(IntPtr callbackId, NativeMethods.RASPBDEVENT eventType, string text, IntPtr data)
        {
            var e = new RasPhoneBookDialogEventArgs(callbackId, text, data);

            switch (eventType)
            {
                case NativeMethods.RASPBDEVENT.AddEntry:
                    OnAddedEntry(e);
                    break;

                case NativeMethods.RASPBDEVENT.DialEntry:
                    OnDialedEntry(e);
                    break;

                case NativeMethods.RASPBDEVENT.EditEntry:
                    OnChangedEntry(e);
                    break;

                case NativeMethods.RASPBDEVENT.RemoveEntry:
                    OnRemovedEntry(e);
                    break;
            }
        }

        #endregion
    }
}