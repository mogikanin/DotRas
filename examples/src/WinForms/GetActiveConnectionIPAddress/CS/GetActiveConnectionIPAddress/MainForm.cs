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
            this.ConnectionsComboBox.Items.Add(new ComboBoxItem("Choose one...", null));

            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                this.ConnectionsComboBox.Items.Add(new ComboBoxItem(connection.EntryName, connection.EntryId));
            }

            this.ConnectionsComboBox.SelectedIndex = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.LoadConnectionsComboBox();
        }

        private void GetAddressButton_Click(object sender, EventArgs e)
        {
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                if (connection.EntryId == (Guid)((ComboBoxItem)this.ConnectionsComboBox.SelectedItem).Value)
                {
                    RasIPInfo ipAddresses = (RasIPInfo)connection.GetProjectionInfo(RasProjectionType.IP);
                    if (ipAddresses != null)
                    {
                        this.ClientAddressTextBox.Text = ipAddresses.IPAddress.ToString();
                        this.ServerAddressTextBox.Text = ipAddresses.ServerIPAddress.ToString();
                    }
                }
            }
        }

        private void ConnectionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetAddressButton.Enabled = this.ConnectionsComboBox.SelectedIndex > 0;
        }
    }
}