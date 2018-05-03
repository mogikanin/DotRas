using System.Collections.Generic;
using DotRas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CreateAndDialVpnEntryMvvm
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly RasDialer _dialer;
        private readonly RasPhoneBook _phonebook;
        private IEnumerable<RasEntry> _connectionsDataSource;
        private bool _isDialButtonEnabled;
        private int _selectedConnectionIndex = -1;
        private bool _isBusy;
        private bool _isDisconnectButtonEnabled;
        private RasHandle _handle;
        private const string ADDRESS = "127.0.0.1";
        private const string USERNAME = "user";
        private const string PASSWORD = "pwd";


        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel()
        {
            _phonebook = new RasPhoneBook();
            _dialer = new RasDialer();
            _dialer.DialCompleted += dialer_DialCompleted;
            _dialer.StateChanged += dialer_StateChanged;

            CreateEntry = new RelayCommand(OnCreateEntry);
            Dial = new RelayCommand<RasEntry>(OnDial);
            Disconnect = new RelayCommand(OnDisconnect);
            WindowInit = new RelayCommand(OnWindowInit);
        }

        public RelayCommand<RasEntry> Dial { get; }
        public RelayCommand CreateEntry { get; }
        public RelayCommand WindowInit { get; }
        public RelayCommand Disconnect { get; }

        public IEnumerable<RasEntry> ConnectionsDataSource
        {
            get => _connectionsDataSource ?? (_connectionsDataSource = new ObservableCollection<RasEntry>());
            private set
            {
                if (_connectionsDataSource == value) return;
                _connectionsDataSource = value;
                RaisePropertyChanged(nameof(ConnectionsDataSource));
            }
        }

        public int SelectedConnectionIndex
        {
            get => _selectedConnectionIndex;
            set
            {
                if (_selectedConnectionIndex == value) return;
                _selectedConnectionIndex = value;
                RaisePropertyChanged(nameof(SelectedConnectionIndex));
                IsDialButtonEnabled = value > 0;
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (value == _isBusy) return;
                _isBusy = value;
                RaisePropertyChanged(nameof(IsBusy));
                RaisePropertyChanged(nameof(IsConnectionsComboBoxEnabled));
                RaisePropertyChanged(nameof(IsCreateButtonEnabled));
            }
        }

        public bool IsConnectionsComboBoxEnabled => !IsBusy;
        public bool IsCreateButtonEnabled => !IsBusy;

        public bool IsDisconnectButtonEnabled
        {
            get => _isDisconnectButtonEnabled;
            private set
            {
                if (value == _isDisconnectButtonEnabled) return;
                _isDisconnectButtonEnabled = value;
                RaisePropertyChanged(nameof(IsDisconnectButtonEnabled));
            }
        }

        public bool IsDialButtonEnabled
        {
            get => _isDialButtonEnabled;
            set
            {
                if (_isDialButtonEnabled == value) return;
                _isDialButtonEnabled = value;
                RaisePropertyChanged(nameof(IsDialButtonEnabled));
            }
        }

        public override void Cleanup()
        {
            _dialer?.Dispose();
            _phonebook?.Dispose();

            base.Cleanup();
        }

        private void LoadAllUserEntries()
        {
            var entries = (ObservableCollection<RasEntry>)ConnectionsDataSource;
            entries.Clear();

            entries.Add(new RasEntry("Select one..."));
            foreach (var entry in _phonebook.Entries)
            {
                entries.Add(entry);
            }
            AppendStatusText($"Loaded {_phonebook.Entries.Count} entries.");

            SelectedConnectionIndex = 0;
        }

        private void OnCreateEntry()
        {
            _phonebook.Entries.Clear();

            foreach (VpnProtocol value in Enum.GetValues(typeof(VpnProtocol)))
            {
                var device = GetDevice(value);
                if (device != null)
                {
                    var entry = RasEntry.CreateVpnEntry("Connection " + value, ADDRESS, GetStrategy(value), device);
                    _phonebook.Entries.Add(entry);
                    AppendStatusText($"Created {entry.Name}.");

                }
            }

            LoadAllUserEntries();
        }

        private static RasVpnStrategy GetStrategy(VpnProtocol protocol)
        {
            switch (protocol)
            {
                case VpnProtocol.IKEv2:
                    return RasVpnStrategy.IkeV2Only;
                case VpnProtocol.PPTP:
                    return RasVpnStrategy.PptpOnly;
                case VpnProtocol.L2TP:
                    return RasVpnStrategy.L2tpOnly;
                case VpnProtocol.SSTP:
                    return RasVpnStrategy.SstpOnly;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Returns the RasDevice for the given protocol
        /// </summary>
        private static RasDevice GetDevice(VpnProtocol protocol)
        {
            string name;
            switch (protocol)
            {
                case VpnProtocol.IKEv2:
                    name = "(IKEV2)".ToLower();
                    break;
                case VpnProtocol.PPTP:
                    name = "(PPTP)".ToLower();
                    break;
                case VpnProtocol.L2TP:
                    name = "(L2TP)".ToLower();
                    break;
                case VpnProtocol.SSTP:
                    name = "(SSTP)".ToLower();
                    break;
                default:
                    throw new NotSupportedException();
            }
            return RasDevice.GetDevices().FirstOrDefault(d => d.DeviceType == RasDeviceType.Vpn && d.Name.ToLower().Contains(name));
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private enum VpnProtocol
        {
            IKEv2,
            PPTP,
            L2TP,
            SSTP,
        }

        private static void AppendStatusText(string value)
        {
            Messenger.Default.Send(new DialerStateChangedMessage(value));
        }

        private void OnDial(RasEntry entry)
        {
            if (entry == null || entry.Id == Guid.Empty)
            {
                return;
            }

            try
            {
                Messenger.Default.Send(new DialerStateChangedMessage());

                _dialer.EntryName = entry.Name;
                _dialer.PhoneBookPath = entry.Owner.Path;

                _dialer.Credentials = new NetworkCredential(USERNAME, PASSWORD);
                _dialer.SynchronizingObject = new DispatcherSynchronizingObject(Application.Current.Dispatcher);

                _dialer.DialAsync();

                // Make sure the user cannot change the selected connection, and change the buttons that are enabled.
                IsBusy = true;
                IsDialButtonEnabled = false;
                IsDisconnectButtonEnabled = true;
            }
            catch (Exception ex)
            {
                AppendStatusText(ex.ToString());
            }
        }

        private void OnDisconnect()
        {
            if (!_dialer.IsBusy)
            {
                AppendStatusText("Disconnecting...");
                var conn = RasConnection.GetActiveConnections().FirstOrDefault(_ => _.Handle == _handle);
                if (conn != null)
                {
                    conn.HangUp();
                    AppendStatusText("Disconnected!");
                }
                else
                {
                    AppendStatusText("Connection handle not found.");
                }
                IsDisconnectButtonEnabled = false;
                IsBusy = false;
                IsDialButtonEnabled = true;
                return;
            }

            _dialer.DialAsyncCancel();
        }

        private void OnWindowInit()
        {
            _phonebook.SynchronizingObject = new DispatcherSynchronizingObject(Application.Current.Dispatcher);
            _phonebook.Open("phonebook.pbk");

            LoadAllUserEntries();
        }

        private static void dialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            AppendStatusText("State changed: " + e.State);
            if (e.ErrorCode > 0)
            {
                AppendStatusText($"Error code: {e.ErrorCode}. Message: {e.ErrorMessage}");
            }
        }

        private void dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AppendStatusText("Cancelled!");
            }
            else if (e.Connected)
            {
                _handle = e.Handle;
                AppendStatusText("Connected!");
            }
            else if (e.TimedOut)
            {
                AppendStatusText("Connection attempt timed out!");
            }
            else if (e.Error != null)
            { 
                AppendStatusText(e.Error.ToString());
            }

            IsBusy = e.Connected;
            IsDialButtonEnabled = !e.Connected;
            IsDisconnectButtonEnabled = e.Connected;
        }
    }
}