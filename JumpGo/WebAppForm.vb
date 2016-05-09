Public Class WebAppForm

    Private Sub WebAppForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FasterBrowser1.Navigate("https://www.google.com/")
    End Sub
End Class