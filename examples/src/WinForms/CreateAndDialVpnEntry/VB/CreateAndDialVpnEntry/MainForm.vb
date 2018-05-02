Imports System.Net
Imports DotRas

Public Class MainForm
    Public Const EntryName As String = "VPN Connection"
    Private connectionHandle As RasHandle

    Private Sub CreateEntryButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateEntryButton.Click
        ' This opens the phonebook so it can be used. Different overloads here will determine where the phonebook is opened/created.
        Me.AllUsersPhoneBook.Open()

        ' Create the entry that will be used by the dialer to dial the connection. Entries can be created manually, however the static methods on
        ' the RasEntry class shown below contain default information matching that what is set by Windows for each platform.
        Dim entry As RasEntry = RasEntry.CreateVpnEntry(EntryName, IPAddress.Loopback.ToString(), RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn))

        ' Add the new entry to the phone book.
        Me.AllUsersPhoneBook.Entries.Add(entry)
    End Sub

    Private Sub DialButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DialButton.Click
        Me.StatusTextBox.Clear()

        ' This button will be used to dial the connection.
        Me.Dialer.EntryName = EntryName
        Me.Dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)

        Try
            ' Set the credentials the dialer should use.
            Me.Dialer.Credentials = New NetworkCredential("Test", "User")

            ' NOTE: The entry MUST be in the phone book before the connection can be dialed.
            ' Begin dialing the connection; this will raise events from the dialer instance.
            Me.connectionHandle = Me.Dialer.DialAsync()

            ' Enable the disconnect button for use later.
            Me.DisconnectButton.Enabled = True
        Catch ex As Exception
            Me.StatusTextBox.AppendText(ex.ToString())
        End Try
    End Sub

    Private Sub Dialer_StateChanged(ByVal sender As System.Object, ByVal e As DotRas.StateChangedEventArgs) Handles Dialer.StateChanged
        Me.StatusTextBox.AppendText(e.State.ToString() + Chr(13) + Chr(10))
    End Sub

    Private Sub Dialer_DialCompleted(ByVal sender As System.Object, ByVal e As DotRas.DialCompletedEventArgs) Handles Dialer.DialCompleted
        If (e.Cancelled) Then
            Me.StatusTextBox.AppendText("Cancelled!")
        ElseIf (e.TimedOut) Then
            Me.StatusTextBox.AppendText("Connection attempt timed out!")
        ElseIf (e.Error IsNot Nothing) Then
            Me.StatusTextBox.AppendText(e.Error.ToString())
        ElseIf (e.Connected) Then
            Me.StatusTextBox.AppendText("Connection successful!")
        End If

        If (Not e.Connected) Then
            Me.DisconnectButton.Enabled = False
        End If
    End Sub

    Private Sub DisconnectButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectButton.Click
        If (Me.Dialer.IsBusy) Then
            Me.Dialer.DialAsyncCancel()
        Else
            ' The connection attempt has completed, attempt to find the connection in the active connections.
            Dim connection As RasConnection = RasConnection.GetActiveConnectionByHandle(Me.connectionHandle)
            If (connection IsNot Nothing) Then
                ' The connection has been found, disconnect it.
                connection.HangUp()
            End If
        End If
    End Sub
End Class