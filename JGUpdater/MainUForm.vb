'Imports JumpGoStandardEdition
Imports System.IO

Public Class MainUForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As String = Path.GetTempPath()

        My.Computer.Network.DownloadFile(
    My.Settings.DLURL + "curverid.txt",
        result + "\curverid.txt")

        'My.Computer.Network.DownloadFile(
        'My.Settings.DLURL + "jumpgo-program-files.zip",
        'result + "\jumpgo-program-files.zip")
    End Sub

    Private Sub MainUForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class