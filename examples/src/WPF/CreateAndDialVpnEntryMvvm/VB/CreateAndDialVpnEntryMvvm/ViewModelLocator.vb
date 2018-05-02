Public Class ViewModelLocator
    Private Shared _mainWindowViewModel As MainWindowViewModel


    Public Property MainWindowViewModel() As MainWindowViewModel
        Get
            If (_mainWindowViewModel Is Nothing) Then
                _mainWindowViewModel = New MainWindowViewModel()
            End If

            Return _mainWindowViewModel
        End Get
        Set(ByVal value As MainWindowViewModel)
            _mainWindowViewModel = value
        End Set
    End Property

    Public Shared Sub Cleanup()
        _mainWindowViewModel.Cleanup()
    End Sub

End Class