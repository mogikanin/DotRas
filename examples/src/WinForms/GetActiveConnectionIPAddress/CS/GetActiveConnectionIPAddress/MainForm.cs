using System;
using System.Windows.Forms;

namespace DotRas.Samples.GetActiveConnectionIPAddress
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadConnectionsComboBox()
        {
            ConnectionsComboBox.Items.Add(new ComboBoxItem("Choose one...", null));

            foreach (var connection in RasConnection.GetActiveConnections())
            {
                ConnectionsComboBox.Items.Add(new ComboBoxItem(connection.EntryName, connection.EntryId));
            }

            ConnectionsComboBox.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadConnectionsComboBox();
        }

        private void GetAddressButton_Click(object sender, EventArgs e)
        {
            foreach (var connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryId != (Guid) ((ComboBoxItem) ConnectionsComboBox.SelectedItem).Value) continue;
                var ipAddresses = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                if (ipAddresses == null) continue;

                ClientAddressTextBox.Text = ipAddresses.IPAddress.ToString();
                ServerAddressTextBox.Text = ipAddresses.ServerIPAddress.ToString();
            }
        }

        private void ConnectionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetAddressButton.Enabled = ConnectionsComboBox.SelectedIndex > 0;
        }
    }
}