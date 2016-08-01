Public Class IncognitoTabButtonNew

    Private Sub IncognitoTabButtonNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Focus = True Then
            'IncognitoMain.CreateNewTab(My.Settings.NewTab)
            IncognitoMain.NewTabButton("")
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Me.Focus = True Then
            'IncognitoMain.CreateNewTab(My.Settings.NewTab)
            IncognitoMain.CreateNewTab("")
            'JumpGoMain.NewTabButton("")
            'Me.Close()
        End If
    End Sub
End Class