Imports BControls

Class MainWindow

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Try
            BMessage.sbInitialize(Brushes.Purple, Brushes.White, Brushes.Purple, Brushes.White, New FontFamilyConverter().ConvertFrom("Arial"), System.AppDomain.CurrentDomain.BaseDirectory & "Log")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnOk_Click(sender As Object, e As RoutedEventArgs) Handles btnOk.Click
        Try
            If BMessage.Instance.fnMessage("Test ok message box.", "Test", MessageBoxButton.OK) = MessageBoxResult.OK Then
                BMessage.Instance.fnMessage("Return ok", "Test", MessageBoxButton.OK, BMessage.enm_MessageImages.Ok)
            Else
                BMessage.Instance.fnMessage("Return Cancel", "Test", MessageBoxButton.OK)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnOkCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnOkCancel.Click
        Try
            If BMessage.Instance.fnMessage("Test ok/cancel message box.", "Test", MessageBoxButton.OKCancel) = MessageBoxResult.OK Then
                BMessage.Instance.fnMessage("Return ok", "Test", MessageBoxButton.OK)
            Else
                BMessage.Instance.fnMessage("Return Cancel", "Test", MessageBoxButton.OK)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnYesNo_Click(sender As Object, e As RoutedEventArgs) Handles btnYesNo.Click
        Try
            If BMessage.Instance.fnMessage("Test yes/no message box.", "Test", MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                BMessage.Instance.fnMessage("Return yes", "Test", MessageBoxButton.OK)
            Else
                BMessage.Instance.fnMessage("Return no", "Test", MessageBoxButton.OK)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnYesNoCancel_Click(sender As Object, e As RoutedEventArgs) Handles btnYesNoCancel.Click

        Dim objReturn As MessageBoxResult

        Try
            objReturn = BMessage.Instance.fnMessage("Test yes/no message box.", "Test", MessageBoxButton.YesNoCancel)
            If objReturn = MessageBoxResult.Yes Then
                BMessage.Instance.fnMessage("Return yes", "Test", MessageBoxButton.OK)
            ElseIf objReturn = MessageBoxResult.No Then
                BMessage.Instance.fnMessage("Return no", "Test", MessageBoxButton.OK)
            Else
                BMessage.Instance.fnMessage("Return cancel", "Test", MessageBoxButton.OK)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnError_Click(sender As Object, e As RoutedEventArgs) Handles btnError.Click
        Try
            Call sbGiveMeError()
        Catch ex As Exception
            BMessage.Instance.fnErrorMessage(ex)
        End Try
    End Sub

    Private Sub sbGiveMeError()

        Dim inty As Integer
        Dim intx As Integer

        Try

            inty = 0
            intx = 100

            inty = intx / inty

        Catch ex As Exception
            Throw New Exception("Take your error", ex)
        End Try
    End Sub

End Class
