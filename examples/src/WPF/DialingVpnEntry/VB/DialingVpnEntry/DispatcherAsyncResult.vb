Imports System.Windows.Threading

Public Class DispatcherAsyncResult
    Implements System.IAsyncResult

    Private dispatcherOperation As DispatcherOperation

    Public Sub New(Byval dispatcherOperation As DispatcherOperation)
        Me.dispatcherOperation = dispatcherOperation
    End Sub


    Public ReadOnly Property AsyncState() As Object Implements System.IAsyncResult.AsyncState
        Get
            Return Me.dispatcherOperation
        End Get
    End Property

    Public ReadOnly Property AsyncWaitHandle() As System.Threading.WaitHandle Implements System.IAsyncResult.AsyncWaitHandle
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property CompletedSynchronously() As Boolean Implements System.IAsyncResult.CompletedSynchronously
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property IsCompleted() As Boolean Implements System.IAsyncResult.IsCompleted
        Get
            Return Me.dispatcherOperation.Status = DispatcherOperationStatus.Completed
        End Get
    End Property
End Class
