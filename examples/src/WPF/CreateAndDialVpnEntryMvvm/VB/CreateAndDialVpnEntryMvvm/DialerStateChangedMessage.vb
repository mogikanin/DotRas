Public Class DialerStateChangedMessage
    Private _message As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal message As String)
        Me.Message = message
    End Sub

    Public Property Message As String
        Get
            Return Me._message
        End Get
        Set(ByVal value As String)
            Me._message = value
        End Set
    End Property
End Class
