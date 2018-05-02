using System.Collections.Generic;
using System.Windows.Controls;
using DotRas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Threading;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using GalaSoft.MvvmLight.Messaging;

namespace CreateAndDialVpnEntryMvvm
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string ConnectionName = "VPN Connection";

        private RasDialer dialer;
        private RasPhoneBook phonebook;
        private IEnumerable<RasEntry> connectionsDataSource;
        private bool isCreateButtonEnabled = true;
        private bool isDisconnectButtonEnabled;
        private bool isConnectionsComboBoxEnabled = true;
        private bool isDialButtonEnabled;
        private int selectedConnectionIndex = -1;

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.phonebook = new RasPhoneBook();
            this.phonebook.Changed += new EventHandler<EventArgs>(this.phonebook_Changed);
            this.phonebook.EnableFileWatcher = true;

            this.dialer = new RasDialer();
            this.dialer.DialCompleted += new System.EventHandler<DialCompletedEventArgs>(this.dialer_DialCompleted);
            this.dialer.StateChanged += new System.EventHandler<StateChangedEventArgs>(this.dialer_StateChanged);

            this.CreateEntry = new RelayCommand(() => this.OnCreateEntry());
            this.Dial = new RelayCommand<RasEntry>((item) => this.OnDial(item));
            this.Disconnect = new RelayCommand(() => this.OnDisconnect());
            this.WindowInit = new RelayCommand(() => this.OnWindowInit());
        }

        public RelayCommand<RasEntry> Dial { get; set; }
        public RelayCommand CreateEntry { get; set; }
        public RelayCommand WindowInit { get; set; }
        public RelayCommand Disconnect { get; set; }

        public IEnumerable<RasEntry> ConnectionsDataSource
        {
            get
            {
                if (this.connectionsDataSource == null)
                {
                    this.connectionsDataSource = new ObservableCollection<RasEntry>();
                }

                return this.connectionsDataSource;
            }

            set
            {
                if (this.connectionsDataSource != value)
                {
                    this.connectionsDataSource = value;
                    this.RaisePropertyChanged("ConnectionsDataSource");
                }
            }
        }

        public int SelectedConnectionIndex
        {
            get
            {
                return this.selectedConnectionIndex;
            }

            set
            {
                if (this.selectedConnectionIndex != value)
                {
                    this.selectedConnectionIndex = value;
                    this.RaisePropertyChanged("SelectedConnectionIndex");

                    this.IsDialButtonEnabled = value > 0;
                }
            }
        }

        public bool IsConnectionsComboBoxEnabled
        {
            get
            {
                return this.isConnectionsComboBoxEnabled;
            }

            set
            {
                if (this.isConnectionsComboBoxEnabled != value)
                {
                    this.isConnectionsComboBoxEnabled = value;
                    this.RaisePropertyChanged("IsConnectionsComboBoxEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the 'create entry' button is enabled.
        /// </summary>
        public bool IsCreateButtonEnabled
        {
            get
            {
                return this.isCreateButtonEnabled;
            }

            set
            {
                if (this.isCreateButtonEnabled != value)
                {
                    this.isCreateButtonEnabled = value;
                    this.RaisePropertyChanged("IsCreateButtonEnabled");
                }
            }
        }

        public bool IsDisconnectButtonEnabled
        {
            get
            {
                return this.isDisconnectButtonEnabled;
            }

            set
            {
                if (this.isDisconnectButtonEnabled != value)
                {
                    this.isDisconnectButtonEnabled = value;
                    this.RaisePropertyChanged("IsDisconnectButtonEnabled");
                }
            }
        }

        public bool IsDialButtonEnabled
        {
            get
            {
                return this.isDialButtonEnabled;
            }

            set
            {
                if (this.isDialButtonEnabled != value)
                {
                    this.isDialButtonEnabled = value;
                    this.RaisePropertyChanged("IsDialButtonEnabled");
                }
            }
        }

        public override void Cleanup()
        {
            if (this.dialer != null)
            {
                this.dialer.Dispose();
            }

            if (this.phonebook != null)
            {
                this.phonebook.Dispose();
            }

            base.Cleanup();
        }

        private void LoadAllUserEntries()
        {
            ObservableCollection<RasEntry> entries = (ObservableCollection<RasEntry>)this.ConnectionsDataSource;
            entries.Clear();

            entries.Add(new RasEntry("Select one..."));
           
            foreach (RasEntry entry in phonebook.Entries)
            {
                entries.Add(entry);
            }

            this.IsCreateButtonEnabled = !this.phonebook.Entries.Contains(ConnectionName);
            this.SelectedConnectionIndex = 0;
        }

        private void OnCreateEntry()
        {
            if (this.phonebook.Entries.Contains(ConnectionName))
            {
                this.IsCreateButtonEnabled = false;
            }
            else
            {
                RasEntry entry = RasEntry.CreateVpnEntry(ConnectionName, IPAddress.Loopback.ToString(), RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn));

                this.phonebook.Entries.Add(entry);
            }
        }

        private void AppendStatusText(string value)
        {
            Messenger.Default.Send<DialerStateChangedMessage>(new DialerStateChangedMessage(value));
        }

        private void OnDial(RasEntry entry)
        {
            if (entry == null || entry.Id == Guid.Empty)
            {
                return;
            }

            try
            {
                Messenger.Default.Send<DialerStateChangedMessage>(new DialerStateChangedMessage());

                this.dialer.EntryName = entry.Name;
                this.dialer.PhoneBookPath = entry.Owner.Path;

                this.dialer.Credentials = new System.Net.NetworkCredential("Test", "User");
                this.dialer.SynchronizingObject = new DispatcherSynchronizingObject(App.Current.Dispatcher);

                this.dialer.DialAsync();

                // Make sure the user cannot change the selected connection, and change the buttons that are enabled.
                this.IsConnectionsComboBoxEnabled = false;
                this.IsDialButtonEnabled = false;
                this.IsDisconnectButtonEnabled = true;
            }
            catch (Exception ex)
            {
                this.AppendStatusText(ex.ToString());
            }
        }

        private void OnDisconnect()
        {
            if (!this.dialer.IsBusy)
            {
                return;
            }

            this.dialer.DialAsyncCancel();
        }

        private void OnWindowInit()
        {
            this.phonebook.SynchronizingObject = new DispatcherSynchronizingObject(App.Current.Dispatcher);
            this.phonebook.Open();

            if (this.phonebook.Entries.Contains(ConnectionName))
            {
                // The entry already exists within the phonebook, disable the button.
                this.IsCreateButtonEnabled = false;
            }

            this.LoadAllUserEntries();
        }

        private void phonebook_Changed(object sender, EventArgs e)
        {
            this.LoadAllUserEntries();
        }

        private void dialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            this.AppendStatusText(string.Join(string.Empty, new string[] { e.State.ToString(), "\r\n" }));
        }

        private void dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.AppendStatusText("Cancelled!");
            }
            else if (e.Connected)
            {
                this.AppendStatusText("Connected!");
            }
            else if (e.TimedOut)
            {
                this.AppendStatusText("Connection attempt timed out!");
            }
            else if (e.Error != null)
            {
                this.AppendStatusText(e.Error.ToString());
            }

            this.IsConnectionsComboBoxEnabled = true;
            this.IsDisconnectButtonEnabled = false;
            this.IsDialButtonEnabled = true;
        }
    }
}