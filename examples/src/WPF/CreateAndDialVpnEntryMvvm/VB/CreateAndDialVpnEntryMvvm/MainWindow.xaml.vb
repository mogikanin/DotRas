Imports GalaSoft.MvvmLight.Messaging

Class MainWindow
    Public Sub New()
        Me.InitializeComponent()

        Messenger.Default.Register(Of DialerStateChangedMessage)(Me, New Action(Of DialerStateChangedMessage)(AddressOf OnDialerStateChanged))
    End Sub

    Private Sub OnDialerStateChanged(ByVal message As DialerStateChangedMessage)
        If String.IsNullOrEmpty(message.Message) Then
            Me.StatusTextBox.Clear()
        Else
            Me.StatusTextBox.AppendText(message.Message)
            Me.StatusTextBox.ScrollToEnd()
        End If
    End Sub
End Class
