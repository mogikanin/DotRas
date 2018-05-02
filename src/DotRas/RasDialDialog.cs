//--------------------------------------------------------------------------
// <copyright file="RasDialDialog.cs" company="Jeff Winn">
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
    using DotRas.Design;
    using DotRas.Internal;
    using DotRas.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Prompts the user to dial a phone book entry. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to use the <b>RasDialDialog</b> component to display a user interface to dial a specific connection.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasDialDialog dialog = new RasDialDialog())
    /// {
    ///     dialog.EntryName = "VPN Connection";
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
    ///     if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    ///     {
    ///         // The entry has connected successfully.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim dialog As RasDialDialog
    /// Try
    ///     dialog = New RasDialDialog
    ///     dialog.EntryName = "VPN Connection"
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)
    ///     If (dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
    ///         ' The entry has connected successfully.
    ///     End If
    /// Finally
    ///     If (dialog IsNot Nothing) Then
    ///         dialog.Dispose()
    ///     End If
    /// End Try
    /// ]]>
    /// </code>
    /// </example>
    public sealed class RasDialDialog : RasCommonDialog
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasDialDialog"/> class.
        /// </summary>
        public RasDialDialog()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the full path (including filename) to the phone book.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("RDPhoneBookPathDesc")]
        public string PhoneBookPath
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the entry to be dialed.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDEntryNameDesc")]
        public string EntryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number to dial.
        /// </summary>
        /// <remarks>This value overrides the numbers stored in the phone book.</remarks>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("RDDPhoneNumberDesc")]
        public string PhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the zero-based index of the subentry to dial.
        /// </summary>
        [DefaultValue(0)]
        [SRCategory("CatData")]
        [SRDescription("RDSubEntryIdDesc")]
        public int SubEntryId
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all <see cref="RasDialDialog"/> properties to their default values.
        /// </summary>
        public override void Reset()
        {
            this.PhoneBookPath = null;
            this.EntryName = null;
            this.PhoneNumber = null;
            this.SubEntryId = 0;

            base.Reset();
        }

        /// <summary>
        /// Overridden. Displays the modal dialog.
        /// </summary>
        /// <param name="hwndOwner">The handle of the window that owns the dialog box.</param>
        /// <returns><b>true</b> if the user completed the entry successfully, otherwise <b>false</b>.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            NativeMethods.RASDIALDLG dlg = new NativeMethods.RASDIALDLG();
            dlg.size = Marshal.SizeOf(typeof(NativeMethods.RASDIALDLG));
            dlg.hwndOwner = hwndOwner;
            dlg.subEntryId = this.SubEntryId;

            if (this.Location != Point.Empty)
            {
                dlg.left = this.Location.X;
                dlg.top = this.Location.Y;

                dlg.flags |= NativeMethods.RASDDFLAG.PositionDlg;
            }

            bool retval = false;
            try
            {
                retval = UnsafeNativeMethods.Instance.DialDlg(this.PhoneBookPath, this.EntryName, this.PhoneNumber, ref dlg);
                if (!retval && dlg.error != NativeMethods.SUCCESS)
                {
                    RasErrorEventArgs e = new RasErrorEventArgs(dlg.error, RasHelper.Instance.GetRasErrorString(dlg.error));
                    this.OnError(e);
                }
            }
            catch (EntryPointNotFoundException)
            {
                ThrowHelper.ThrowNotSupportedException(Resources.Exception_NotSupportedOnPlatform);
            }

            return retval;
        }

        #endregion
    }
}