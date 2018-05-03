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
            InitializeComponent();

            Messenger.Default.Register<DialerStateChangedMessage>(this, OnDialerStateChanged);
        }

        private void OnDialerStateChanged(DialerStateChangedMessage message)
        {
            StatusTextBox.AppendText(message.Message + Environment.NewLine);
            StatusTextBox.ScrollToEnd();
        }
    }
}
