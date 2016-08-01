Imports System.IO

Public Class WebSource
    Public FileLoacation As String

    Private Sub WebSource_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'RichTextBox1.Text = JumpGoMain.TabControl1.SelectedForm.fasterbrowser1.url.absoluteuri
        ToolStripStatusLabel1.Text = "Status: Loaded"
        ToolStripStatusLabel2.Text = "File: " + FileLoacation
    End Sub

    Private Sub CutToolStripButton_Click(sender As Object, e As EventArgs) Handles CutToolStripButton.Click
        Clipboard.SetText(FastColoredTextBox1.SelectedText)
        FastColoredTextBox1.SelectedText = ""
    End Sub

    Private Sub CopyToolStripButton_Click(sender As Object, e As EventArgs) Handles CopyToolStripButton.Click
        Clipboard.SetText(FastColoredTextBox1.SelectedText)
    End Sub

    Private Sub PasteToolStripButton_Click(sender As Object, e As EventArgs) Handles PasteToolStripButton.Click
        FastColoredTextBox1.SelectedText = Clipboard.GetText
    End Sub

    Private Sub PrintToolStripButton_Click(sender As Object, e As EventArgs) Handles PrintToolStripButton.Click
        PrintDialog1.ShowDialog()
        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim TextBox1 As New TextBox
        TextBox1.Text = FastColoredTextBox1.Text

        Static currentChar As Integer
        Static currentLine As Integer
        Dim textfont As Font = TextBox1.Font
        Dim h, w As Integer
        Dim left, top As Integer
        With PrintDocument1.DefaultPageSettings
            h = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            w = .PaperSize.Width - .Margins.Left - .Margins.Right
            left = PrintDocument1.DefaultPageSettings.Margins.Left
            top = PrintDocument1.DefaultPageSettings.Margins.Top
        End With
        e.Graphics.DrawRectangle(Pens.Blue, New Rectangle(left, top, w, h))
        If PrintDocument1.DefaultPageSettings.Landscape Then
            Dim a As Integer
            a = h
            h = w
            w = a
        End If
        Dim lines As Integer = CInt(Math.Round(h / textfont.Height))
        Dim b As New Rectangle(left, top, w, h)
        Dim format As StringFormat
        If Not TextBox1.WordWrap Then
            format = New StringFormat(StringFormatFlags.NoWrap)
            format.Trimming = StringTrimming.EllipsisWord
            Dim i As Integer
            For i = currentLine To Math.Min(currentLine + lines, TextBox1.Lines.Length - 1)
                e.Graphics.DrawString(TextBox1.Lines(i), textfont, Brushes.Black, New RectangleF(left, top + textfont.Height * (i - currentLine), w, textfont.Height), format)
            Next
            currentLine += lines
            If currentLine >= TextBox1.Lines.Length Then
                e.HasMorePages = False
                currentLine = 0
            Else
                e.HasMorePages = True
            End If
            Exit Sub
        End If
        format = New StringFormat(StringFormatFlags.LineLimit)
        Dim line, chars As Integer
        e.Graphics.MeasureString(Mid(TextBox1.Text, currentChar + 1), textfont, New SizeF(w, h), format, chars, line)
        If currentChar + chars < TextBox1.Text.Length Then
            If TextBox1.Text.Substring(currentChar + chars, 1) <> " " And TextBox1.Text.Substring(currentChar + chars, 1) <> vbLf Then
                While chars > 0
                    TextBox1.Text.Substring(currentChar + chars, 1)
                    TextBox1.Text.Substring(currentChar + chars, 1)
                    chars -= 1
                End While
                chars += 1
            End If
        End If
        e.Graphics.DrawString(TextBox1.Text.Substring(currentChar, chars), textfont, Brushes.Black, b, format)
        currentChar = currentChar + chars
        If currentChar < TextBox1.Text.Length Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            currentChar = 0
        End If
    End Sub

    Private Sub OpenToolStripButton_Click(sender As Object, e As EventArgs) Handles OpenToolStripButton.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try
                ' Open the file using a stream reader.
                Using sr As New StreamReader(OpenFileDialog1.FileName)
                    Dim line As String
                    ' Read the stream to a string and write the string to the console.
                    line = sr.ReadToEnd()
                    Console.WriteLine(line)
                    FastColoredTextBox1.Text = sr.ReadToEnd
                End Using
            Catch 'e As Exception
                'Console.WriteLine("The file could not be read:")
                'Console.WriteLine(e.Message)
            End Try
            ToolStripStatusLabel1.Text = "Status: Loaded"
            ToolStripStatusLabel2.Text = "File: " + OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        FastColoredTextBox1.Text = ""
        ToolStripStatusLabel1.Text = "Status: New File Created"
        ToolStripStatusLabel2.Text = "File: New File"
    End Sub

    Private Sub SaveToolStripButton_Click(sender As Object, e As EventArgs) Handles SaveToolStripButton.Click
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim objWriter As New System.IO.StreamWriter(SaveFileDialog1.FileName)

            objWriter.Write(FastColoredTextBox1.Text)
            objWriter.Close()
            'MessageBox.Show("Text written to file")
            ToolStripStatusLabel1.Text = "Status: File Saved"
            ToolStripStatusLabel2.Text = "File: " + SaveFileDialog1.FileName
        End If
    End Sub
End Class