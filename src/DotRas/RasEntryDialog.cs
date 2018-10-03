//--------------------------------------------------------------------------
// <copyright file="RasEntryDialog.cs" company="Jeff Winn">
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

using JetBrains.Annotations;

namespace DotRas
{
    using Design;
    using Internal;
    using Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Prompts the user to create or modify a phone book entry. This class cannot be inherited.
    /// </summary>
    /// <example>
    /// The following example shows how to modify an existing entry using the <b>RasEntryDialog</b> component.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasEntryDialog dialog = new RasEntryDialog())
    /// {
    ///     dialog.EntryName = "VPN Connection";
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
    ///     dialog.Style = RasDialogStyle.Edit;
    ///     if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    ///     {
    ///         ' The entry was modified successfully.
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim dialog As RasEntryDialog
    /// Try
    ///     dialog = New RasEntryDialog
    ///     dialog.EntryName = "VPN Connection"
    ///     dialog.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)
    ///     dialog.Style = RasDialogStyle.Edit
    ///     If (dialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
    ///         ' The entry was modified successfully.
    ///     End If
    /// Finally
    ///     If (dialog IsNot Nothing) Then
    ///         dialog.Dispose()
    ///     End If
    /// End Try
    /// ]]>
    /// </code>
    /// </example>
    [UsedImplicitly]
    public sealed class RasEntryDialog : RasCommonDialog
    {
        #region Constructors

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the entry.
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
        /// Gets or sets a value indicating whether entries cannot be renamed while in edit mode.
        /// </summary>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("REDNoRenameDesc")]
        public bool NoRename
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style of dialog box to display.
        /// </summary>
        [DefaultValue(typeof(RasDialogStyle), "Create")]
        [SRCategory("CatBehavior")]
        [SRDescription("REDRasDialogStyleDesc")]
        public RasDialogStyle Style
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the full path (including filename) to the phone book.
        /// </summary>
        [DefaultValue(null)]
        [SRCategory("CatData")]
        [SRDescription("REDPhoneBookPathDesc")]
        public string PhoneBookPath
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets all <see cref="RasEntryDialog"/> properties to their default values.
        /// </summary>
        public override void Reset()
        {
            EntryName = null;
            NoRename = false;
            Style = RasDialogStyle.Create;
            PhoneBookPath = null;

            base.Reset();
        }

        /// <summary>
        /// Overridden. Displays the modal dialog.
        /// </summary>
        /// <param name="hwndOwner">The handle of the window that owns the dialog box.</param>
        /// <returns><b>true</b> if the user completed the entry successfully, otherwise <b>false</b>.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            if (string.IsNullOrEmpty(PhoneBookPath))
            {
                ThrowHelper.ThrowArgumentException("PhoneBookPath", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            var dlg = new NativeMethods.RASENTRYDLG
            {
                size = Marshal.SizeOf(typeof(NativeMethods.RASENTRYDLG)), hwndOwner = hwndOwner
            };

            switch (Style)
            {
                case RasDialogStyle.Edit:
                    if (NoRename)
                    {
                        dlg.flags |= NativeMethods.RASEDFLAG.NoRename;
                    }

                    break;

                default:
                    dlg.flags |= NativeMethods.RASEDFLAG.NewEntry;
                    break;
            }

            if (Location != Point.Empty)
            {
                dlg.left = Location.X;
                dlg.top = Location.Y;

                dlg.flags |= NativeMethods.RASEDFLAG.PositionDlg;
            }

            var retval = false;

            try
            {
                string entryName = null;
                if (!string.IsNullOrEmpty(EntryName))
                {
                    entryName = EntryName;
                }

                retval = UnsafeNativeMethods.Instance.EntryDlg(PhoneBookPath, entryName, ref dlg);
                if (retval)
                {
                    EntryName = dlg.entryName;
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