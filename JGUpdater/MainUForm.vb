Imports JumpGoStandardEdition
Imports System.IO

Public Class MainUForm
    Private client As Object
    Dim latestTag

    Private Sub MainUForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.JumpGoVersion = JumpGoStandardEdition.CurVerIDClass.CurJGVer

        Dim releases = client.Release.GetAll("JTechMe", "JumpGo-for-Windows")
        Dim latest = releases(0)
        Console.WriteLine("The latest release is tagged at {0} and is named {1}", latest.TagName, latest.Name)
        latestTag = releases(0)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Octokit.

        Dim releases = client.Release.GetAll("JTechMe", "JumpGo-for-Windows")
        Dim latest = releases(0)
        Console.WriteLine("The latest release is tagged at {0} and is named {1}", latest.TagName, latest.Name)
        latestTag = releases(0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As String = Path.GetTempPath()
        Dim curFile As String = result + "\update.zip"

        If File.Exists(curFile) Then
            My.Computer.FileSystem.DeleteFile(result + "\update.zip")
        End If

        'My.Computer.Network.DownloadFile(
        '"https://github.com/JTechMe/JumpGo-for-Windows/releases/download" + "/update.zip",
        'result + "\update.zip")

        My.Computer.Network.DownloadFile(
    latestTag + "/update.zip",
        result + "\update.zip")
    End Sub
End Class
