//--------------------------------------------------------------------------
// <copyright file="RasPhoneBook.cs" company="Jeff Winn">
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
    using System.ComponentModel;
    using System.IO;
    using Design;
    using Internal;
    using Properties;

    /// <summary>
    /// Represents a remote access service (RAS) phone book. This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When using <b>RasPhoneBook</b> to monitor a phone book for external changes, ensure the SynchronizingObject property is set if thread synchronization is If this is not done, you may get cross-thread exceptions thrown from the component. This is typically needed with applications that have an interface; for example, Windows Forms or Windows Presentation Foundation (WPF).
    /// </para>
    /// <para>
    /// There are multiple phone books in use by Windows at any given point in time and this class can only manage one phone book per instance. If you add an entry to the all user's profile phone book, attempting to manipulate it with the current user's profile phone book opened will result in failure. Entries will not be located, and changes made to the phone book will not be recognized by the instance.
    /// </para>
    /// <para>
    /// When attempting to open a phone book that does not yet exist, the <b>RasPhoneBook</b> class will automatically generate the file to be used. Also, setting the <see cref="EnableFileWatcher"/> property to <b>true</b> will allow the class to monitor for any external changes made to the file.
    /// </para>
    /// <para><b>Known Limitations</b>
    /// <list type="bullet">
    /// <item>For phone books which are not located in the all users profile directory (including those in custom locations) any stored credentials for the entries must be stored per user.</item>
    /// <item>The <b>RasPhoneBook</b> component may not be able to modify entries in the All User's profile without elevated application privileges. If your application cannot request elevated privileges you can either use the current user profile phone book, or use a custom phone book in a path that will not require elevated permissions.</item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// The following example shows how to open a phone book in a custom location using a <b>RasPhoneBook</b> class.
    /// <code lang="C#">
    /// <![CDATA[
    /// using (RasPhoneBook pbk = new RasPhoneBook())
    /// {
    ///     pbk.Open("C:\\Test.pbk");
    /// }
    /// ]]>
    /// </code>
    /// <code lang="VB.NET">
    /// <![CDATA[
    /// Dim pbk As RasPhoneBook
    /// Try
    ///     pbk = New RasPhoneBook
    ///     pbk.Open("C:\Test.pbk")
    /// Finally
    ///     If (pbk IsNot Nothing) Then
    ///         pbk.Dispose()
    ///     End If
    /// End Try
    /// ]]>
    /// </code>
    /// </example>
    public sealed partial class RasPhoneBook : RasComponent
    {
        #region Fields

        /// <summary>
        /// Defines the partial path (including filename) for a default phonebook file.
        /// </summary>
        private const string PhoneBookFilePath = @"Microsoft\Network\Connections\Pbk\rasphone.pbk";

        /// <summary>
        /// Contains the collection of entries in the phone book.
        /// </summary>
        private RasEntryCollection entries;

        /// <summary>
        /// Indicates whether the internal watcher will be enabled.
        /// </summary>
        private bool enableFileWatcher;

        /// <summary>
        /// Contains the internal <see cref="FileSystemWatcher"/> used to monitor the phone book for changes.
        /// </summary>
        private FileSystemWatcher watcher;

        /// <summary>
        /// Indicates whether the phone book has already been opened.
        /// </summary>
        private bool opened;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasPhoneBook"/> class.
        /// </summary>
        public RasPhoneBook()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DotRas.RasPhoneBook"/> class.
        /// </summary>
        /// <param name="container">An <see cref="System.ComponentModel.IContainer"/> that will contain the component.</param>
        public RasPhoneBook(IContainer container)
            : base(container)
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the phone book has changed.
        /// </summary>
        /// <remarks>This event may be raised multiple times depending on how the file was changed.</remarks>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBChangedDesc")]
        public event EventHandler<EventArgs> Changed;

        /// <summary>
        /// Occurs when the phone book has been deleted.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBDeletedDesc")]
        public event EventHandler<EventArgs> Deleted;

        /// <summary>
        /// Occurs when the phone book has been renamed.
        /// </summary>
        [SRCategory("CatBehavior")]
        [SRDescription("RPBRenamedDesc")]
        public event EventHandler<RenamedEventArgs> Renamed;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the full path (including filename) of the phone book.
        /// </summary>
        [Browsable(false)]
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of phone book.
        /// </summary>
        [Browsable(false)]
        public RasPhoneBookType PhoneBookType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the collection of entries within the phone book.
        /// </summary>
        [Browsable(false)]
        public RasEntryCollection Entries
        {
            get
            {
                if (entries == null)
                {
                    entries = new RasEntryCollection(this);
                }

                return entries;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the phone book file will be monitored for external changes.
        /// </summary>
        [DefaultValue(false)]
        [SRCategory("CatBehavior")]
        [SRDescription("RPBEnableFileWatcherDesc")]
        public bool EnableFileWatcher
        {
            get => enableFileWatcher;

            set
            {
                enableFileWatcher = value;

                if (opened)
                {
                    // The phone book has already been opened, update the setting on the watcher.
                    watcher.EnableRaisingEvents = value;
                }
            }
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Determines the full path (including filename) of the phone book.
        /// </summary>
        /// <param name="phoneBookType">The type of phone book to locate.</param>
        /// <returns>The full path (including filename) of the phone book.</returns>
        /// <remarks><see cref="RasPhoneBookType.Custom"/> will always return a null reference (<b>Nothing</b> in Visual Basic).</remarks>
        public static string GetPhoneBookPath(RasPhoneBookType phoneBookType)
        {
            string retval = null;

            if (phoneBookType != RasPhoneBookType.Custom)
            {
                Environment.SpecialFolder folder = Environment.SpecialFolder.CommonApplicationData;
                if (phoneBookType == RasPhoneBookType.User)
                {
                    folder = Environment.SpecialFolder.ApplicationData;
                }

                retval = System.IO.Path.Combine(Environment.GetFolderPath(folder), PhoneBookFilePath);
            }

            return retval;
        }

        /////// <summary>
        /////// Opens the phone book.
        /////// </summary>
        /////// <remarks>This method opens the existing default phone book in the All Users profile, or creates a new phone book if the file does not already exist.</remarks>
        /////// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        ////[Obsolete("This method will be removed in a future version, please use the Open(string) overload to open the phone book.", false)]
        ////public void Open()
        ////{
        ////    this.Open(false);
        ////}

        /////// <summary>
        /////// Opens the phone book.
        /////// </summary>
        /////// <param name="openUserPhoneBook"><b>true</b> to open the phone book in the user's profile; otherwise, <b>false</b> to open the system phone book in the All Users profile.</param>
        /////// <remarks>This method opens an existing phone book or creates a new phone book if the file does not already exist.</remarks>
        /////// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        ////[Obsolete("This method will be removed in a future version, please use the Open(string) overload to open the phone book.", false)]
        ////public void Open(bool openUserPhoneBook)
        ////{
        ////    RasPhoneBookType phoneBookType = RasPhoneBookType.AllUsers;
        ////    if (openUserPhoneBook)
        ////    {
        ////        phoneBookType = RasPhoneBookType.User;
        ////    }

        ////    this.Open(RasPhoneBook.GetPhoneBookPath(phoneBookType));
        ////    this.PhoneBookType = phoneBookType;
        ////}

        /// <summary>
        /// Opens the phone book.
        /// </summary>
        /// <param name="phoneBookPath">The path (including filename) of a phone book.</param>
        /// <remarks>This method opens an existing phone book or creates a new phone book if the file does not already exist.</remarks>
        /// <exception cref="System.ArgumentException"><paramref name="phoneBookPath"/> is an empty string or null reference (<b>Nothing</b> in Visual Basic).</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission to perform the action requested.</exception>
        public void Open(string phoneBookPath)
        {
            if (string.IsNullOrEmpty(phoneBookPath))
            {
                ThrowHelper.ThrowArgumentException("phoneBookPath", Resources.Argument_StringCannotBeNullOrEmpty);
            }

            FileInfo file = new FileInfo(phoneBookPath);
            if (string.IsNullOrEmpty(file.Name))
            {
                ThrowHelper.ThrowArgumentException("phoneBookPath", Resources.Argument_InvalidFileName);
            }

            RasPhoneBookType phoneBookType = RasPhoneBookType.Custom;
            if (string.Equals(phoneBookPath, GetPhoneBookPath(RasPhoneBookType.AllUsers), StringComparison.CurrentCultureIgnoreCase))
            {
                phoneBookType = RasPhoneBookType.AllUsers;
            }
            else if (string.Equals(phoneBookPath, GetPhoneBookPath(RasPhoneBookType.User), StringComparison.CurrentCultureIgnoreCase))
            {
                phoneBookType = RasPhoneBookType.User;
            }

            Path = file.FullName;
            PhoneBookType = phoneBookType;

            // Setup the watcher used to monitor the file for changes, and attempt to load the entries.
            SetupFileWatcher(file);
            Entries.Load();

            opened = true;
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        protected override void InitializeComponent()
        {
            watcher = new FileSystemWatcher();
            watcher.BeginInit();

            watcher.Renamed += new RenamedEventHandler(WatcherRenamed);
            watcher.Deleted += new FileSystemEventHandler(WatcherDeleted);
            watcher.Changed += new FileSystemEventHandler(WatcherChanged);

            watcher.EndInit();

            base.InitializeComponent();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="DotRas.RasPhoneBook"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><b>true</b> to release both managed and unmanaged resources; <b>false</b> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (watcher != null)
                {
                    watcher.Dispose();
                }

                Path = null;
                entries = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="RasPhoneBook.Changed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void OnChanged(EventArgs e)
        {
            RaiseEvent<EventArgs>(Changed, e);
        }

        /// <summary>
        /// Raises the <see cref="RasPhoneBook.Deleted"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void OnDeleted(EventArgs e)
        {
            RaiseEvent<EventArgs>(Deleted, e);
        }

        /// <summary>
        /// Raises the <see cref="RasPhoneBook.Renamed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void OnRenamed(RenamedEventArgs e)
        {
            RaiseEvent<RenamedEventArgs>(Renamed, e);
        }

        /// <summary>
        /// Occurs when the internal watcher notices the file has changed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="FileSystemEventArgs"/> containing event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The design is ok, the exception is being raised in an event.")]
        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                Entries.Load();

                OnChanged(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        /// <summary>
        /// Occurs when the internal watcher notices the file has been deleted.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="FileSystemEventArgs"/> containing event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The design is ok, the exception is being raised in an event.")]
        private void WatcherDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                Entries.Load();

                OnDeleted(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        /// <summary>
        /// Occurs when the internal watcher notices the file has been renamed.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="RenamedEventArgs"/> containing event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "The design is ok, the exception is being raised in an event.")]
        private void WatcherRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                if (e.ChangeType == WatcherChangeTypes.Renamed)
                {
                    Path = e.FullPath;

                    // Force the file watcher to disable temporarily while the file being monitored is updated.
                    watcher.EnableRaisingEvents = false;
                    watcher.Filter = e.Name;
                    watcher.EnableRaisingEvents = EnableFileWatcher;
                }

                OnRenamed(e);
            }
            catch (Exception ex)
            {
                OnError(new ErrorEventArgs(ex));
            }
        }

        /// <summary>
        /// Setup the internal <see cref="FileSystemWatcher"/> used to monitor the phonebook for external changes.
        /// </summary>
        /// <param name="file">The full path (including filename) of the file.</param>
        private void SetupFileWatcher(FileInfo file)
        {
            if (!file.Exists)
            {
                // The file being opened does not exist, create the file so it can be monitored.
                Utilities.CreateFile(file);
            }

            watcher.Path = file.DirectoryName;
            watcher.Filter = file.Name;
            watcher.EnableRaisingEvents = EnableFileWatcher;
        }

        #endregion
    }
}