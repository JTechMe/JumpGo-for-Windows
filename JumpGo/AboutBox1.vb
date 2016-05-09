'Option Explicit On
'Option Strict On
Imports System
Imports System.Reflection

Imports Microsoft.WindowsAPICodePack
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports Microsoft.WindowsAPICodePack.Shell

Imports System.Globalization
Imports System.Windows.Forms
Imports AutoUpdaterDotNET
Imports System.Net

Imports System.IO
Imports System.IO.Compression
Imports System.IO.Directory
Imports System.IO.DirectoryNotFoundException
Imports System.IO.FileAccess
Imports System.Environment

Public NotInheritable Class AboutBox1

    Inherits Form
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FormMain_Load(sender As Object, e As EventArgs)
        'Uncomment below line to see Russian version

        'AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");

        'If you want to open download page when user click on download button uncomment below line.

        'AutoUpdater.OpenDownloadPage = true;

        'Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

        'AutoUpdater.LetUserSelectRemindLater = false;
        'AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
        'AutoUpdater.RemindLaterAt = 2;

        AutoUpdater.Start("https://raw.githubusercontent.com/JTechMe/DotNET-Updater-Files/master/JGDEB.xml")
    End Sub

    Dim Y, X As Integer
    Dim Newpoint As System.Drawing.Point

    Private Sub AboutBox1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub TableLayoutPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles TableLayoutPanel.MouseDown
        X = Control.MousePosition.X - Me.Location.X
        Y = Control.MousePosition.Y - Me.Location.Y
    End Sub

    Private Sub TableLayoutPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles TableLayoutPanel.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Newpoint = Control.MousePosition
            Newpoint.X -= (X)
            Newpoint.Y -= (Y)
            Me.Location = Newpoint
        End If
    End Sub

    Private Sub TableLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'JumpGoMain.CreateNewTab("http://www.mozilla.org/poweredby")
        Process.Start("http://www.mozilla.org/poweredby")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        JumpGoMain.NewInfoTab("http://joehorton990.wix.com/jumpgo")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

        If File.Exists(appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master.zip") = False Then
            If Directory.Exists(appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master") = True Then
                My.Computer.FileSystem.DeleteDirectory(
  appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master",
  FileIO.DeleteDirectoryOption.DeleteAllContents)
                'My.Computer.FileSystem.DeleteDirectory(Environment.CurrentDirectory + "\UpdaterDL\DotNET-Updater-Files-master")
            End If
        Else
            My.Computer.FileSystem.DeleteFile(appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master.zip")

            If Directory.Exists(appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master") = True Then
                My.Computer.FileSystem.DeleteDirectory(
  appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master",
  FileIO.DeleteDirectoryOption.DeleteAllContents)
                'My.Computer.FileSystem.DeleteDirectory(Environment.CurrentDirectory + "\UpdaterDL\DotNET-Updater-Files-master")
            End If
        End If

        My.Computer.Network.DownloadFile(
                "https://codeload.github.com/JTechMe/DotNET-Updater-Files/zip/master",
                appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master.zip")

        'Dim wc As WebClient = New WebClient()
        'wc.DownloadFile("http://codeload.github.com/JTechMe/DotNET-Updater-Files/zip/master", Environment.CurrentDirectory + "\UpdaterDL\DotNET-Updater-Files-master.zip")

        'Dim startPath As String = "c:\example\start"
        Dim zipPath As String = appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master.zip"
        Dim extractPath As String = appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master"

        'ZipFile.CreateFromDirectory(startPath, zipPath)

        ZipFile.ExtractToDirectory(zipPath, extractPath)

        'Uncomment below line to see Russian version

        'AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");

        'If you want to open download page when user click on download button uncomment below line.

        'AutoUpdater.OpenDownloadPage = true;

        'Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

        'AutoUpdater.LetUserSelectRemindLater = false;
        'AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
        'AutoUpdater.RemindLaterAt = 2;

        'AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml")
        'AutoUpdater.Start("https://github.com/JTechMe/DotNET-Updater-Files/blob/master/JGDEB.xml")
        AutoUpdater.Start(appData + "\JTechMe\JumpGo\DevEd\UpdaterDL\DotNET-Updater-Files-master\DotNET-Updater-Files-master\JGDEB.xml")
    End Sub
End Class
