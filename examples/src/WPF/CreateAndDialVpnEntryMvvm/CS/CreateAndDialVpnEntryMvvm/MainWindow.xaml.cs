using System;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace CreateAndDialVpnEntryMvvm
{
    /// <summary>
    /// Contains the code behind for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            Messenger.Default.Register<DialerStateChangedMessage>(this, (message) => this.OnDialerStateChanged(message));
        }

        private void OnDialerStateChanged(DialerStateChangedMessage message)
        {
            if (string.IsNullOrEmpty(message.Message))
            {
                this.StatusTextBox.Clear();
            }
            else
            {
                this.StatusTextBox.AppendText(message.Message);
                this.StatusTextBox.ScrollToEnd();
            }
        }
    }
}
