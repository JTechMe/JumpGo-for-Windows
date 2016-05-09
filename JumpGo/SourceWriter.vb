Public Class SourceWriter
    Dim OpenedDoc As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RichTextBox1.Text = FastColoredTextBox1.Text

        SaveFileDialog1.Title = "PageBox - Save File"
        SaveFileDialog1.DefaultExt = "html"
        SaveFileDialog1.Filter = "HTML Files|*.html|All Files|*.*"
        SaveFileDialog1.FilterIndex = 1
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName = "" Then Exit Sub

        Dim strExt As String
        strExt = System.IO.Path.GetExtension(SaveFileDialog1.FileName)
        strExt = strExt.ToUpper()

        Select Case strExt
            Case ".HTML"
                RichTextBox1.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText)
            Case Else
                Dim txtWriter As System.IO.StreamWriter
                txtWriter = New System.IO.StreamWriter(SaveFileDialog1.FileName)
                txtWriter.Write(RichTextBox1.Text)
                txtWriter.Close()
                txtWriter = Nothing
                RichTextBox1.SelectionStart = 0
                RichTextBox1.SelectionLength = 0
        End Select
        'FasterBrowser1.Navigate(SaveFileDialog1.FileName)
        OpenedDoc = SaveFileDialog1.FileName
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FasterBrowser1.Navigate(SaveFileDialog1.FileName)
    End Sub
End Class