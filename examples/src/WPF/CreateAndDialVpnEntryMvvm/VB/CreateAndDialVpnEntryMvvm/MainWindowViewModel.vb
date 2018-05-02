Imports GalaSoft.MvvmLight
Imports GalaSoft.MvvmLight.Messaging
Imports DotRas
Imports GalaSoft.MvvmLight.Command
Imports System.Collections.ObjectModel
Imports System.Net

Public Class MainWindowViewModel : Inherits ViewModelBase
    Public Const ConnectionName As String = "VPN Connection"

    Private dialer As RasDialer
    Private phoneBook As RasPhoneBook
    Private _connectionsDataSource As IEnumerable(Of RasEntry)
    Private _isCreateButtonEnabled As Boolean = True
    Private _isDisconnectButtonEnabled As Boolean
    Private _isConnectionsComboBoxEnabled As Boolean = True
    Private _isDialButtonEnabled As Boolean
    Private _selectedConnectionIndex As Integer = -1

    Public Sub New()
        Me.phoneBook = New RasPhoneBook()
        AddHandler Me.phoneBook.Changed, AddressOf Me.phoneBook_Changed
        Me.phoneBook.EnableFileWatcher = True

        Me.dialer = New RasDialer()
        AddHandler Me.dialer.StateChanged, AddressOf Me.dialer_StateChanged
        AddHandler Me.dialer.DialCompleted, AddressOf Me.dialer_DialCompleted

        Me.CreateEntry = New RelayCommand(AddressOf Me.OnCreateEntry)
        Me.Dial = New RelayCommand(Of RasEntry)(AddressOf Me.OnDial)
        Me.Disconnect = New RelayCommand(AddressOf Me.OnDisconnect)
        Me.WindowInit = New RelayCommand(AddressOf Me.OnWindowInit)
    End Sub

    Public Property Dial As RelayCommand(Of RasEntry)
    Public Property CreateEntry As RelayCommand
    Public Property WindowInit As RelayCommand
    Public Property Disconnect As RelayCommand

    Public Property ConnectionsDataSource As IEnumerable(Of RasEntry)
        Get
            If Me._connectionsDataSource Is Nothing Then
                Me._connectionsDataSource = New ObservableCollection(Of RasEntry)
            End If

            Return Me._connectionsDataSource
        End Get
        Set(ByVal value As IEnumerable(Of RasEntry))
            If value IsNot Me._connectionsDataSource Then
                Me._connectionsDataSource = value
                Me.RaisePropertyChanged("ConnectionsDataSource")
            End If
        End Set
    End Property

    Public Property SelectedConnectionIndex As Integer
        Get
            Return Me._selectedConnectionIndex
        End Get
        Set(ByVal value As Integer)
            If Me._selectedConnectionIndex <> value Then
                Me._selectedConnectionIndex = value
                Me.RaisePropertyChanged("SelectedConnectionIndex")

                Me.IsDialButtonEnabled = value > 0
            End If
        End Set
    End Property

    Public Property IsConnectionsComboBoxEnabled As Boolean
        Get
            Return Me._isConnectionsComboBoxEnabled
        End Get
        Set(ByVal value As Boolean)
            If (Me._isConnectionsComboBoxEnabled <> value) Then
                Me._isConnectionsComboBoxEnabled = value
                Me.RaisePropertyChanged("IsConnectionsComboBoxEnabled")
            End If
        End Set
    End Property

    Public Property IsCreateButtonEnabled As Boolean
        Get
            Return Me._isCreateButtonEnabled
        End Get
        Set(ByVal value As Boolean)
            If Me._isCreateButtonEnabled <> value Then
                Me._isCreateButtonEnabled = value
                Me.RaisePropertyChanged("IsCreateButtonEnabled")
            End If
        End Set
    End Property

    Public Property IsDisconnectButtonEnabled As Boolean
        Get
            Return Me._isDisconnectButtonEnabled
        End Get
        Set(ByVal value As Boolean)
            If Me._isDisconnectButtonEnabled <> value Then
                Me._isDisconnectButtonEnabled = value
                Me.RaisePropertyChanged("IsDisconnectButtonEnabled")
            End If
        End Set
    End Property

    Public Property IsDialButtonEnabled() As Boolean
        Get
            Return Me._isDialButtonEnabled
        End Get
        Set(ByVal value As Boolean)
            If Me._isDialButtonEnabled <> value Then
                Me._isDialButtonEnabled = value
                Me.RaisePropertyChanged("IsDialButtonEnabled")
            End If
        End Set
    End Property

    Public Overrides Sub Cleanup()
        If dialer IsNot Nothing Then
            dialer.Dispose()
        End If

        If phoneBook IsNot Nothing Then
            phoneBook.Dispose()
        End If

        MyBase.Cleanup()
    End Sub

    Private Sub LoadAllUserEntries()
        Dim entries As ObservableCollection(Of RasEntry) = CType(Me.ConnectionsDataSource, ObservableCollection(Of RasEntry))
        entries.Clear()

        entries.Add(New RasEntry("Select one..."))

        For Each entry As RasEntry In Me.phoneBook.Entries
            entries.Add(entry)
        Next

        Me.IsCreateButtonEnabled = Not Me.phoneBook.Entries.Contains(ConnectionName)
        Me.SelectedConnectionIndex = 0
    End Sub

    Private Sub OnCreateEntry()
        If Me.phoneBook.Entries.Contains(ConnectionName) Then
            Me.IsCreateButtonEnabled = False
        Else
            Dim entry As RasEntry = RasEntry.CreateVpnEntry(ConnectionName, IPAddress.Loopback.ToString(), RasVpnStrategy.Default, RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn))

            Me.phoneBook.Entries.Add(entry)
        End If
    End Sub

    Private Sub AppendStatusText(ByVal value As String)
        Messenger.Default.Send(Of DialerStateChangedMessage)(New DialerStateChangedMessage(value))
    End Sub

    Private Sub OnDial(ByVal entry As RasEntry)
        If entry IsNot Nothing AndAlso entry.Id = Guid.Empty Then
            Return
        End If

        Try
            Messenger.Default.Send(Of DialerStateChangedMessage)(New DialerStateChangedMessage())

            Me.dialer.EntryName = entry.Name
            Me.dialer.PhoneBookPath = entry.Owner.Path

            Me.dialer.Credentials = New NetworkCredential("Test", "User")
            Me.dialer.SynchronizingObject = New DispatcherSynchronizingObject(Application.Current.Dispatcher)

            Me.dialer.DialAsync()

            ' Make sure the user cannot change the selected connection, and change the buttons that are enabled.
            Me.IsConnectionsComboBoxEnabled = False
            Me.IsDialButtonEnabled = False
            Me.IsDisconnectButtonEnabled = True
        Catch ex As Exception
            Me.AppendStatusText(ex.ToString())
        End Try
    End Sub

    Private Sub OnDisconnect()
        If Not Me.dialer.IsBusy Then
            Return
        End If

        Me.dialer.DialAsyncCancel()
    End Sub

    Private Sub OnWindowInit()
        Me.phoneBook.SynchronizingObject = New DispatcherSynchronizingObject(Application.Current.Dispatcher)
        Me.phoneBook.Open()

        If Me.phoneBook.Entries.Contains(ConnectionName) Then
            ' The entry already exists within the phonebook, disable the button.
            Me.IsCreateButtonEnabled = False
        End If

        Me.LoadAllUserEntries()
    End Sub

    Private Sub phoneBook_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Me.LoadAllUserEntries()
    End Sub

    Private Sub dialer_StateChanged(ByVal sender As Object, ByVal e As StateChangedEventArgs)
        Me.AppendStatusText(e.State.ToString() + Chr(13) + Chr(10))
    End Sub

    Private Sub dialer_DialCompleted(ByVal sender As Object, ByVal e As DialCompletedEventArgs)
        If e.Cancelled Then
            Me.AppendStatusText("Cancelled!")
        ElseIf e.Connected Then
            Me.AppendStatusText("Connected!")
        ElseIf e.TimedOut Then
            Me.AppendStatusText("Connection attempt timed out!")
        ElseIf e.Error IsNot Nothing Then
            Me.AppendStatusText(e.Error.ToString())
        End If

        Me.IsConnectionsComboBoxEnabled = True
        Me.IsDisconnectButtonEnabled = False
        Me.IsDialButtonEnabled = True
    End Sub
End Class