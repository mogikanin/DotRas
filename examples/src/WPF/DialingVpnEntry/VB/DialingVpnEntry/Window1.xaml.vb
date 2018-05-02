Imports DotRas

Class Window1
    Private dialer As RasDialer
    Private handle As RasHandle

    Private Sub ConnectionComboBox_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs)
        Me.DialButton.IsEnabled = Me.ConnectionComboBox.SelectedIndex > 0
    End Sub

    Private Sub Window1_Initialized(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Initialized
        Me.ConnectionComboBox.Items.Clear()
        Me.ConnectionComboBox.Items.Add("Select one...")

        Dim pbk As New RasPhoneBook
        Try
            pbk.Open()

            For Each entry As RasEntry In pbk.Entries
                Me.ConnectionComboBox.Items.Add(entry.Name)
            Next
        Finally
            If (pbk IsNot Nothing) Then
                pbk.Dispose()
            End If
        End Try

        ' Select the initial entry.
        Me.ConnectionComboBox.SelectedIndex = 0
    End Sub

    Private Sub DialButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.StatusTextBox.Clear()

        Me.dialer = New RasDialer()
        AddHandler Me.dialer.DialCompleted, AddressOf Me.dialer_DialCompleted
        AddHandler Me.dialer.StateChanged, AddressOf Me.dialer_StateChanged

        Me.dialer.EntryName = CType(Me.ConnectionComboBox.SelectedValue, String)
        Me.dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)

        ' This allows the multi-threaded dialer to update information on the user interface without causing any threading problems.
        Me.dialer.SynchronizingObject = New DispatcherSynchronizingObject(Application.Current.Dispatcher)

        Try
            ' Set the credentials the dialer should use.
            Me.dialer.Credentials = New System.Net.NetworkCredential("Test", "User")

            Me.handle = Me.dialer.DialAsync()

            ' Disable the button used to initiate dialing while the operation is in progress.
            Me.DialButton.IsEnabled = False
            Me.DisconnectButton.IsEnabled = True
        Catch ex As Exception
            Me.StatusTextBox.AppendText(ex.ToString())
            Me.StatusTextBox.ScrollToEnd()
        End Try
    End Sub

    Private Sub dialer_DialCompleted(ByVal sender As System.Object, ByVal e As DotRas.DialCompletedEventArgs)
        If (e.Cancelled) Then
            Me.StatusTextBox.AppendText("Cancelled!")
        ElseIf (e.Connected) Then
            Me.StatusTextBox.AppendText("Connected!")
        ElseIf (e.Error IsNot Nothing) Then
            Me.StatusTextBox.AppendText(e.Error.ToString())
        ElseIf (e.TimedOut) Then
            Me.StatusTextBox.AppendText("Connection attempt timed out!")
        End If

        Me.StatusTextBox.ScrollToEnd()

        If (Not e.Connected) Then
            ' The connection was not connected, disable the disconnect button.
            Me.DisconnectButton.IsEnabled = False
        End If

        ' Re-enable the button used to dial the connection.
        Me.DialButton.IsEnabled = True
    End Sub

    Private Sub dialer_StateChanged(ByVal sender As System.Object, ByVal e As DotRas.StateChangedEventArgs)
        Me.StatusTextBox.AppendText(String.Format("{0}" + Chr(13) + Chr(10), e.State.ToString()))
        Me.StatusTextBox.ScrollToEnd()
    End Sub

    Private Sub DisconnectButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles DisconnectButton.Click
        If Me.dialer.IsBusy Then
            Me.dialer.DialAsyncCancel()
        Else
            Dim connection As RasConnection = RasConnection.GetActiveConnectionByHandle(Me.handle)
            If (connection IsNot Nothing) Then
                connection.HangUp()
            End If
        End If
    End Sub
End Class