using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DotRas;

namespace DialingVpnEntryWithWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private RasDialer dialer;
        private RasHandle handle;

        public Window1()
        {
            InitializeComponent();
        }

        private void DialButton_Click(object sender, RoutedEventArgs e)
        {
            this.StatusTextBox.Clear();

            this.dialer = new RasDialer();
            this.dialer.DialCompleted += new EventHandler<DialCompletedEventArgs>(dialer_DialCompleted);
            this.dialer.StateChanged += new EventHandler<StateChangedEventArgs>(dialer_StateChanged);

            this.dialer.EntryName = (string)this.ConnectionComboBox.SelectedValue;
            this.dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            // This allows the multi-threaded dialer to update information on the user interface without causing any threading problems.
            this.dialer.SynchronizingObject = new DispatcherSynchronizingObject(App.Current.Dispatcher);

            try
            {
                // Set the credentials the dialer should use.
                this.dialer.Credentials = new System.Net.NetworkCredential("Test", "User");

                this.handle = this.dialer.DialAsync();

                // Disable the button used to initiate dialing while the operation is in progress.
                this.DialButton.IsEnabled = false;
                this.DisconnectButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                this.StatusTextBox.AppendText(ex.ToString());
                this.StatusTextBox.ScrollToEnd();
            }
        }

       private void dialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            this.StatusTextBox.AppendText(string.Format("{0}\r\n", e.State.ToString()));
            this.StatusTextBox.ScrollToEnd();
        }

        private void dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.StatusTextBox.AppendText("Cancelled!");
            }
            else if (e.TimedOut)
            {
                this.StatusTextBox.AppendText("Connection attempt timed out!");
            }
            else if (e.Error != null)
            {
                this.StatusTextBox.AppendText(e.Error.ToString());
            }
            else if (e.Connected)
            {
                this.StatusTextBox.AppendText("Connected!");
            }

            this.StatusTextBox.ScrollToEnd();

            if (!e.Connected)
            {
                // The connection was not connected, disable the disconnect button.
                this.DisconnectButton.IsEnabled = false;
            }

            this.DialButton.IsEnabled = true;
        }

        private void ConnectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DialButton.IsEnabled = this.ConnectionComboBox.SelectedIndex > 0;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.ConnectionComboBox.Items.Clear();
            this.ConnectionComboBox.Items.Add("Select one...");

            using (RasPhoneBook pbk = new RasPhoneBook())
            {
                pbk.Open();

                foreach (RasEntry entry in pbk.Entries)
                {
                    this.ConnectionComboBox.Items.Add(entry.Name);
                }
            }

            // Select the initial entry.
            this.ConnectionComboBox.SelectedIndex = 0;
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.dialer.IsBusy)
            {
                this.dialer.DialAsyncCancel();
            }
            else
            {
                RasConnection connection = RasConnection.GetActiveConnectionByHandle(this.handle);
                if (connection != null)
                {
                    connection.HangUp();
                }
            }
        }
    }
}
