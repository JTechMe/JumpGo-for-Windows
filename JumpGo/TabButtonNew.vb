Public Class TabButtonNew

    Private Sub TabButtonNew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.TabIndex = 9000
        If Me.Focus = True Then
            JumpGoMain.CreateNewTab("")
            'Me.Close()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Me.TabIndex = 9000
        If Me.Focus = True Then
            JumpGoMain.CreateNewTab("")
            'JumpGoMain.NewTabButton("")
            'Me.Close()
        End If
    End Sub
End Class