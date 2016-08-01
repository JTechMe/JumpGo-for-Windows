Public Class UserControlBox
#Region "close"
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.ParentForm.Close()
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        PictureBox1.Image = My.Resources.ctrl_box_red_bttn2
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        PictureBox1.Image = My.Resources.ctrl_box_red_bttn3
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        PictureBox1.Image = My.Resources.ctrl_box_red_bttn11
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        PictureBox1.Image = My.Resources.ctrl_box_red_bttn11
    End Sub
#End Region

#Region "max"
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If Me.ParentForm.WindowState = FormWindowState.Normal Then
            Me.ParentForm.WindowState = FormWindowState.Maximized
        Else
            Me.ParentForm.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub PictureBox2_MouseHover(sender As Object, e As EventArgs) Handles PictureBox2.MouseHover
        PictureBox2.Image = My.Resources.ctrl_box_right_bttn2
    End Sub

    Private Sub PictureBox2_MouseDown(sender As Object, e As EventArgs) Handles PictureBox2.MouseDown
        PictureBox2.Image = My.Resources.ctrl_box_right_bttn3
    End Sub

    Private Sub PictureBox2_MouseUp(sender As Object, e As EventArgs) Handles PictureBox2.MouseUp
        PictureBox2.Image = My.Resources.ctrl_box_right_bttn1
    End Sub

    Private Sub PictureBox2_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox2.MouseLeave
        PictureBox2.Image = My.Resources.ctrl_box_right_bttn1
    End Sub
#End Region

#Region "min"
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        If Me.ParentForm.WindowState = FormWindowState.Minimized Then
            Me.ParentForm.WindowState = FormWindowState.Normal
        Else
            Me.ParentForm.WindowState = FormWindowState.Minimized
        End If
    End Sub

    Private Sub PictureBox3_MouseHover(sender As Object, e As EventArgs) Handles PictureBox3.MouseHover
        PictureBox3.Image = My.Resources.ctrl_box_left_bttn2
    End Sub

    Private Sub PictureBox3_MouseDown(sender As Object, e As EventArgs) Handles PictureBox3.MouseDown
        PictureBox3.Image = My.Resources.ctrl_box_left_bttn3
    End Sub

    Private Sub PictureBox3_MouseUp(sender As Object, e As EventArgs) Handles PictureBox3.MouseUp
        PictureBox3.Image = My.Resources.ctrl_box_left_bttn1
    End Sub

    Private Sub PictureBox3_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox3.MouseLeave
        PictureBox3.Image = My.Resources.ctrl_box_left_bttn1
    End Sub

    Private Sub UserControlBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
#End Region
End Class
