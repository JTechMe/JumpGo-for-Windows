'Imports JumpGoStandardEdition
Imports System.IO

Public Class MainUForm
    Dim currLine As Integer
    Dim allLines As List(Of String) = New List(Of String)
    Dim allLines2 As List(Of String) = New List(Of String)
    Dim textDoc As String
    Dim textDoc2 As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As String = Path.GetTempPath()
        If File.Exists(result + "\curverid.txt") = True Then
            File.Delete(result + "curverid.txt")
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.txt",
        result + "\curverid.txt")
        Else
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.txt",
        result + "\curverid.txt")
        End If

        Dim reader As New System.IO.StreamReader(result + "\curverid.txt")
        'Dim allLines As List(Of String) = New List(Of String)
        Do While Not reader.EndOfStream
            allLines.Add(reader.ReadLine())
            textDoc = reader.ToString
        Loop
        reader.Close()

        Dim reader2 As New System.IO.StreamReader("C:\Program Files (x86)\JTechMe\JumpGo Browser\curverid.txt")
        'Dim allLines As List(Of String) = New List(Of String)
        Do While Not reader2.EndOfStream
            allLines2.Add(reader2.ReadLine())
            textDoc2 = reader2.ToString
        Loop
        reader.Close()

        Dim isTag As String
        isTag = textDoc
        Dim i As Integer = isTag.IndexOf("<curverid>{")
        Dim tagResult As String = isTag.Substring(i + 1, isTag.IndexOf("}", i + 1) - i - 1)

        Dim isTag2 As String
        isTag2 = textDoc2
        Dim i2 As Integer = isTag2.IndexOf("<curverid>{")
        Dim tagResult2 As String = isTag2.Substring(i2 + 1, isTag2.IndexOf("}", i2 + 1) - i2 - 1)

        If tagResult2 = tagResult Then

        Else
            i = isTag.IndexOf("<oldverid>{")
            tagResult = isTag.Substring(i + 1, isTag.IndexOf("}", i + 1) - i - 1)

            i2 = isTag2.IndexOf("<oldverid>{")
            tagResult2 = isTag2.Substring(i2 + 1, isTag.IndexOf("}", i2 + 1) - i2 - 1)

            If tagResult2.Contains(tagResult) Then

            Else
                'https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip
                If File.Exists(result + "\update.zip") = True Then
                    File.Delete(result + "\update.zip")
                    My.Computer.Network.DownloadFile(
            "https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip",
                result + "\update.zip")
                Else
                    My.Computer.Network.DownloadFile(
            "https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip",
                result + "\update.zip")
                    ZIPUpdater.Visible = True
                End If
            End If
        End If
    End Sub

    Public Sub freshID()
        Dim isTag As String
        isTag = textDoc
        Dim i As Integer = isTag.IndexOf("<curverid>{")
        Dim tagResult As String = isTag.Substring(i + 1, isTag.IndexOf("}", i + 1) - i - 1)
    End Sub

    Private Sub MainUForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class