Imports System.IO
Imports System.IO.Compression

Public Class ZIPUpdater
    Dim startPath As String = "c:\example\start"
    Dim outputZip As String = "output zip file path"
    Dim inputZip As String = "C:\Users\PopTart\AppData\Roaming\JTechMe\JumpGo\StandardEd\Updates\update.zip"
    Dim inputFolder As String = "input folder path"
    Dim outputFolder As String = "C:\Program Files (x86)\JTechMe\JumpGo Browser"
    'Dim outputFolder As String = "C:\JumpGo"
    'Dim outputFolder As String = Environment.CurrentDirectory

    'Declare the shell object
    Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))

    Sub ClearThePath()
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(
    outputFolder,
    Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories, "*.*")

            My.Computer.FileSystem.DeleteFile(foundFile,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently)
        Next
    End Sub

    Sub Zip()

        'Lets create an empty Zip File .
        'The following data represents an empty zip file.
        Dim startBytes() As Byte = {80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0,
                                     0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        'Data for an empty zip file.

        FileIO.FileSystem.WriteAllBytes(outputZip, startBytes, False)
        'We have successfully created the empty zip file.

        'Declare the folder which contains the items (files/folders) that you want to zip.
        Dim input As Object = shObj.NameSpace((inputFolder))

        'Declare the created empty zip file.
        Dim output As Object = shObj.NameSpace((outputZip))

        'Compress the items into the zip file.
        output.CopyHere((input.Items), 4)

    End Sub

    Sub UnZip()

        'Create directory in which you will unzip your items.
        IO.Directory.CreateDirectory(outputFolder)
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ IO.Directory.CreateDirectory(" & outputFolder & ")"
        ProgressBar1.Value = 25

        'Declare the folder where the items will be extracted.
        Dim output As Object = shObj.NameSpace((outputFolder))
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Dim output As Object = shObj.NameSpace((" & outputFolder & "))"
        ProgressBar1.Value = 50

        'Declare the input zip file.
        Dim input As Object = shObj.NameSpace((inputZip))
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Dim input As Object = shObj.NameSpace((" & outputFolder & "))"
        ProgressBar1.Value = 75

        'Extract the items from the zip file.
        output.CopyHere((input.Items), 4)
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ output.CopyHere((input.Items))"
        ProgressBar1.Value = 100

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ClearThePath()
        UnZip()
    End Sub

    Private Sub ZIPUpdater_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class