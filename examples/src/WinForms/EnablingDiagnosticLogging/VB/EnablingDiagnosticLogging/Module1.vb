Imports DotRas

Module Module1

    Sub Main()
        Dim phoneBookPath As String = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers)

        Dim pbk As RasPhoneBook = Nothing
        Try
            pbk = New RasPhoneBook()
            pbk.Open(phoneBookPath)

            For Each entry As RasEntry In pbk.Entries
                Console.WriteLine(entry.Name)
            Next
        Finally
            If (pbk IsNot Nothing) Then
                pbk.Dispose()
            End If
        End Try

        Console.WriteLine("Press any key to continue.")
        Console.ReadKey(True)
    End Sub

End Module
