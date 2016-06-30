'Imports JumpGoStandardEdition
Imports System.IO
Imports VB = Microsoft.VisualBasic
Imports Octokit

Public Class IEDLTF1
    Private client As Object
    'Dim latestTag
    Dim latestTag

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'https://github.com/JTechMe/JumpGo-for-Windows/releases/download/4.3.5.9/update.zip
        'Octokit.

        'Dim releases = client.Release.GetAll("JTechMe", "JumpGo-for-Windows")
        Dim releases = client.Release.GetAll("JTechMe", "JumpGo-for-Windows")
        Dim latest = releases(0)
        Console.WriteLine("The latest release is tagged at {0} and is named {1}", latest.TagName, latest.Name)
        latestTag = releases(0)

        Dim result As String = Path.GetTempPath()
        Dim curFile As String = result + "\update.zip"

        If File.Exists(curFile) Then
            My.Computer.FileSystem.DeleteFile(result + "\update.zip")
        End If

        'WebBrowser1.Navigate("https://github.com/JTechMe/JumpGo-for-Windows/releases/download/4.3.5.9/update.zip")

        WebBrowser1.Navigate("https://github.com/JTechMe/JumpGo-for-Windows/releases/download/" + latestTag + "/update.zip")
    End Sub

    Private Sub IEDLTF1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'My.Settings.JumpGoVersion = JumpGoStandardEdition.CurVerIDClass.CurJGVer

        Dim releases = client.Release.GetAll("JTechMe", "JumpGo-for-Windows")
        Dim latest = releases(0)
        Console.WriteLine("The latest release is tagged at {0} and is named {1}", latest.TagName, latest.Name)
        latestTag = releases(0)
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted


    End Sub

    Public Sub wait(ByVal seconds As Single)
        Static start As Single
        start = VB.Timer()
        Do While VB.Timer() < start + seconds
            System.Windows.Forms.Application.DoEvents()
        Loop
    End Sub

    Private Sub lllfgf()
        Dim curLoca As String = WebBrowser1.Url.AbsoluteUri.ToString
        If curLoca.Contains("latest") Then

        Else
            If curLoca.Contains("download") Then

            Else
                If curLoca.Contains("tag") Then
                    latestTag = WebBrowser1.Url.AbsoluteUri.ToString
                    'latestTag.Replace("https://github.com/JTechMe/JumpGo-for-Windows/releases/tag/", "")
                    Timer1.Start()
                    Label2.Text = TimeOfDay
                    wait(2)
                    latestTag.ToString()
                    latestTag.Remove(0, 59)

                    'WebBrowser1.Navigate("https://github.com/JTechMe/JumpGo-for-Windows/releases/download/" + latestTag + "/update.zip")
                    Label1.Text = latestTag
                End If
            End If
        End If
    End Sub
End Class
