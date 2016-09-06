Public Class JGMWin10Tut
    Dim curpt As Integer = 0
    Private Sub JGMWin10Tut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        curpt = 0
    End Sub

    Private Sub pt1()
        Me.Location.X.Equals(JumpGoMain.Location.X + JumpGoMain.Size.Width - 350)
        Me.Location.Y.Equals(JumpGoMain.Location.Y - 76)
        PictureBox2.Visible = True
        PictureBox7.Image = My.Resources.w10t_menu
        Label1.Text = ""
        curpt = 1
    End Sub

    Private Sub pt2()
        Me.Location.X.Equals(JumpGoMain.Location.X - 179)
        Me.Location.Y.Equals(JumpGoMain.Location.Y - 76)
        PictureBox2.Visible = True
        PictureBox7.Image = My.Resources.w10t_tabs
        Label1.Text = "JumpGo Tabs are simple"
        curpt = 2
    End Sub

    Private Sub pt3()
        Me.Location.X.Equals(JumpGoMain.Location.X + JumpGoMain.Size.Width - 350)
        Me.Location.Y.Equals(JumpGoMain.Location.Y - 30)
        PictureBox2.Visible = True
        PictureBox7.Image = My.Resources.w10t_controlbox
        Label1.Text = "JG on Windows 10 uses a
custom title bar and controlbox"
        curpt = 3
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        If curpt = 0 Then
            pt1()
            curpt = 1
        Else
            If curpt = 1 Then
                pt2()
                curpt = 2
            Else
                If curpt = 2 Then
                    pt3()
                    curpt = 3
                Else
                    If curpt = 3 Then
                        Me.Close()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Me.Close()
    End Sub
End Class