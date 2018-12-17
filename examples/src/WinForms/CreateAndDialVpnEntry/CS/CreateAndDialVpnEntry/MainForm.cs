using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace DotRas.Samples.CreateAndDialVpnEntry
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Defines the name of the entry being used by the example.
        /// </summary>
        public const string EntryName = "VPN Connection";

        /// <summary>
        /// Defines the path to phonebook file.
        /// </summary>
        private const string PHONE_BOOK_PATH = "phonebook.pbk";

        /// <summary>
        /// Holds a value containing the handle used by the connection that was dialed.
        /// </summary>
        private RasHandle handle;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user clicks the Create Entry button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void CreateEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                // This opens the phonebook so it can be used. Different overloads here will determine where the phonebook is opened/created.
                AllUsersPhoneBook.Open(PHONE_BOOK_PATH);

                // Create the entry that will be used by the dialer to dial the connection. Entries can be created manually, however the static methods on
                // the RasEntry class shown below contain default information matching that what is set by Windows for each platform.
                var rasDevice = RasDevice.GetDevices().FirstOrDefault(_ => _.Name.Contains("(PPTP)") && _.DeviceType == RasDeviceType.Vpn);
                var entry = RasEntry.CreateVpnEntry(EntryName, IPAddress.Loopback.ToString(), RasVpnStrategy.Default, rasDevice);

                // Add the new entry to the phone book.
                AllUsersPhoneBook.Entries.Add(entry);
            }
            catch (Exception ex)
            {
                StatusTextBox.AppendText(ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user clicks the Dial button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void DialButton_Click(object sender, EventArgs e)
        {
            StatusTextBox.Clear();

            // This button will be used to dial the connection.
            Dialer.EntryName = EntryName;
            Dialer.PhoneBookPath = PHONE_BOOK_PATH;

            try
            {
                // Set the credentials the dialer should use.
                Dialer.Credentials = new NetworkCredential("Test", "User");

                // NOTE: The entry MUST be in the phone book before the connection can be dialed.
                // Begin dialing the connection; this will raise events from the dialer instance.
                handle = Dialer.DialAsync();

                // Enable the disconnect button for use later.
                DisconnectButton.Enabled = true;
            }
            catch (Exception ex)
            {
                StatusTextBox.AppendText(ex.ToString());
            }
        }

        /// <summary>
        /// Occurs when the dialer state changes during a connection attempt.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="DotRas.StateChangedEventArgs"/> containing event data.</param>
        private void Dialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            StatusTextBox.AppendText(e.State + "\r\n");
        }

        /// <summary>
        /// Occurs when the dialer has completed a dialing operation.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="DotRas.DialCompletedEventArgs"/> containing event data.</param>
        private void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                StatusTextBox.AppendText("Cancelled!");
            }
            else if (e.TimedOut)
            {
                StatusTextBox.AppendText("Connection attempt timed out!");
            }
            else if (e.Error != null)
            {
                StatusTextBox.AppendText(e.Error.ToString());
            }
            else if (e.Connected)
            {
                StatusTextBox.AppendText("Connection successful!");
            }

            if (!e.Connected)
            {
                // The connection was not connected, disable the disconnect button.
                DisconnectButton.Enabled = false;
            }
        }

        /// <summary>
        /// Occurs when the user clicks the Disconnect button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">An <see cref="System.EventArgs"/> containing event data.</param>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if (Dialer.IsBusy)
            {
                // The connection attempt has not been completed, cancel the attempt.
                Dialer.DialAsyncCancel();
            }
            else
            {
                // The connection attempt has completed, attempt to find the connection in the active connections.
                var connection = RasConnection.GetActiveConnections().FirstOrDefault(_ => _.Handle == handle);

                // The connection has been found, disconnect it.
                connection?.HangUp();
            }
        }
    }
}
