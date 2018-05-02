using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotRas;
using System.Threading;
using System.Net;
using System.Reflection;

namespace DiagnosticTool
{
    public partial class MainForm : Form
    {
        private NetworkCredential credentials;

        public MainForm()
        {
            InitializeComponent();
        }

        private void DialButton_Click(object sender, EventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)this.EntryNamesComboBox.SelectedItem;

            this.credentials = this.GetUserCredentials((string)item.Text);
            this.Dial();
        }

        private NetworkCredential GetUserCredentials(string targetName)
        {
            NetworkCredential result = null;

            if (this.CredentialPromptCheckBox.Checked)
            {
                using (CredentialPromptDialog dialog = new CredentialPromptDialog())
                {
                    dialog.Caption = "Connect to " + targetName;
                    dialog.Message = string.Format("Please enter your credentials to connect to {0}.", targetName);
                    dialog.TargetName = targetName;
                    dialog.ShowSaveCheckBox = false;

                    if (dialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        result = new NetworkCredential(dialog.UserName, dialog.Password);
                    }
                }
            }

            return result;
        }

        private void Dialer_StateChanged(object sender, StateChangedEventArgs e)
        {
            this.StatusTextBox.AppendText(e.State.ToString() + "\r\n");
        }

        private void Dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.StatusTextBox.AppendText("Cancelled!");
            }
            else if (e.Connected)
            {
                this.StatusTextBox.AppendText("Connected!");
            }
            else if (e.Error != null)
            {
                this.AppendException(e.Error);
            }
            else if (e.TimedOut)
            {
                this.StatusTextBox.AppendText("The connection attempt has timed out!");
            }

            this.CancelButton.Enabled = false;
            this.DialButton.Enabled = true;
        }

        private void AppendException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("An error occurred while connecting:\r\n\r\n\r\n");

            AppendExceptionData(ex, sb);

            this.StatusTextBox.AppendText(sb.ToString());
        }

        private void AppendExceptionData(Exception ex, StringBuilder sb)
        {
            PropertyInfo[] props = ex.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (props != null && props.Length > 0)
            {
                foreach (PropertyInfo prop in props)
                {
                    if (prop == null)
                    {
                        continue;
                    }

                    sb.AppendFormat("Property: {0}\r\n", prop.Name);

                    object value = prop.GetValue(ex, null);
                    if (value == null)
                    {
                        sb.AppendFormat("Value: [NULL]");
                    }
                    else
                    {
                        sb.AppendFormat("Value: {0}", value.ToString());
                    }

                    sb.Append("\r\n\r\n");
                }

                sb.AppendFormat("Stack Trace:\r\n{0}\r\n", ex.StackTrace);
            }

            sb.Append("\r\n\r\n");

            if (ex.InnerException != null)
            {
                sb.Append("-------------------------------");

                AppendExceptionData(ex.InnerException, sb);
            }
        }

        private void Dialer_Error(object sender, System.IO.ErrorEventArgs e)
        {
            this.AppendException(e.GetException());
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select a phone book...";
                dialog.DefaultExt = ".pbk";
                dialog.Filter = "Dial-Up Phonebook (*.pbk)|*.pbk|All Files (*.*)|*.*";
                dialog.Multiselect = false;
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.PhoneBookTextBox.Text = dialog.FileName;
                    this.LoadEntryNamesComboBox();
                }
            }
        }

        private void LoadEntryNamesComboBox()
        {
            this.EntryNamesComboBox.Items.Clear();
            this.EntryNamesComboBox.Items.Add(new ComboBoxItem("Select one..."));

            using (RasPhoneBook pbk = new RasPhoneBook())
            {
                pbk.Open(this.PhoneBookTextBox.Text);

                foreach (RasEntry entry in pbk.Entries)
                {
                    this.EntryNamesComboBox.Items.Add(new ComboBoxItem(entry.Name, entry.Owner.Path));
                }                
            }

            this.EntryNamesComboBox.SelectedIndex = 0;
            this.EntryNamesComboBox.Enabled = true;
        }

        private void EntryNamesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DialButton.Enabled = this.EntryNamesComboBox.SelectedIndex > 0;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (this.Dialer.IsBusy)
            {
                this.Dialer.DialAsyncCancel();
            }
        }

        private void rasConnectionWatcher1_Disconnected(object sender, RasConnectionEventArgs e)
        {
            this.Dial();
        }

        private void Dial()
        {
            this.StatusTextBox.Clear();

            ComboBoxItem item = (ComboBoxItem)this.EntryNamesComboBox.SelectedItem;

            this.Dialer.PhoneBookPath = (string)item.Tag;
            this.Dialer.EntryName = item.Text;
            this.Dialer.Credentials = this.credentials;
            this.Dialer.DialAsync();

            this.CancelButton.Enabled = true;
            this.DialButton.Enabled = false;
        }
    }
}