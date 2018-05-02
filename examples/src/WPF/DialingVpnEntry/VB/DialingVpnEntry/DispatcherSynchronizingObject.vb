Imports System.Windows.Threading

Public Class DispatcherSynchronizingObject
    Implements System.ComponentModel.ISynchronizeInvoke

    Private dispatcher As Dispatcher

    Public Sub New(ByVal dispatcher As Dispatcher)
        Me.dispatcher = dispatcher
    End Sub

    Public Function BeginInvoke(ByVal method As System.Delegate, ByVal args() As Object) As System.IAsyncResult Implements System.ComponentModel.ISynchronizeInvoke.BeginInvoke
        Return New DispatcherAsyncResult(Me.dispatcher.BeginInvoke(method, args))
    End Function

    Public Function EndInvoke(ByVal result As System.IAsyncResult) As Object Implements System.ComponentModel.ISynchronizeInvoke.EndInvoke
        Dim asyncResult = CType(result, DispatcherAsyncResult)
        If (asyncResult IsNot Nothing) Then
            Dim dispatcherOperation As DispatcherOperation = CType(asyncResult.AsyncState, DispatcherOperation)
            If (dispatcherOperation IsNot Nothing) Then
                Return dispatcherOperation.Wait()
            End If
        End If

        Return Nothing
    End Function

    Public Function Invoke(ByVal method As System.Delegate, ByVal args() As Object) As Object Implements System.ComponentModel.ISynchronizeInvoke.Invoke
        Return Me.dispatcher.Invoke(method, args)
    End Function

    Public ReadOnly Property InvokeRequired() As Boolean Implements System.ComponentModel.ISynchronizeInvoke.InvokeRequired
        Get
            Return True
        End Get
    End Property
End Class
