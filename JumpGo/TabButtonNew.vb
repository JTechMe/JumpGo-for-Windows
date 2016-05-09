Public Class TabButtonNew

    Private Sub TabButtonNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Focus = True Then
            JumpGoMain.CreateNewTab("")
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Me.Focus = True Then
            JumpGoMain.CreateNewTab("")
            'JumpGoMain.NewTabButton("")
            'Me.Close()
        End If
    End Sub
End Class