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

Imports Gecko
'Imports System.IO
Imports System.Linq
Imports System.Environment

Public Class JumpGoMain

    Dim timedExtraction As Integer = 0

    Public WithEvents TaskButton1 As New ThumbnailToolBarButton(My.Resources.imageres_5302, "Web Source")
    Public WithEvents TaskButton2 As New ThumbnailToolBarButton(My.Resources.imageres_5306, "Source Writer")
    Public WithEvents TaskButton3 As New ThumbnailToolBarButton(My.Resources.IconPlus, "New Tab")
    Public WithEvents TaskButton4 As New ThumbnailToolBarButton(My.Resources.imageres_114, "Settings")
    Public WithEvents TaskButton5 As New ThumbnailToolBarButton(My.Resources.imageres_5322, "New Window")

    Private Sub TaskButton1Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton1.Click
        'WebSource.Visible = True
        TabControl1.SelectedForm.fasterbrowser.viewsource()
    End Sub

    Private Sub TaskButton2Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton2.Click
        SourceWriter.Visible = True
    End Sub

    Private Sub TaskButton3Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton3.Click
        CreateNewTab("")
    End Sub

    Private Sub TaskButton4Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton4.Click
        Settings.Visible = True
    End Sub

    Private Sub TaskButton5Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton5.Click
        Dim SecondForm As New JumpGoMain
        SecondForm.Show()
    End Sub

    Private browser As GeckoWebBrowser

    Sub New()

        InitializeComponent()
        'Xpcom.Initialize(Environment.CurrentDirectory + "/xulrunner")
        Dim app_dir = Path.GetDirectoryName(Application.ExecutablePath)
        Gecko.Xpcom.Initialize(Path.Combine(app_dir, "xulrunner"))
        'browser = New GeckoWebBrowser()

        'browser.Dock = DockStyle.Fill
        'Me.browser.Name = "browser"

        'Me.Controls.Add(browser)
        'Xpcom.CreateInstance(Of AppDomainManager)("@mozilla.org/login-manager;1")
    End Sub

    Private Sub exportSettings()
        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

        Dim SettingFileName As String
        'SettingFileName = appData + "\JTechMe\JumpGo\DevEd\Settings\MySettings.AppSettings"
        SettingFileName = appData + "\JTechMe\JumpGo\DevEd\Settings\MySettings.config"

        Dim sDialog As New SaveFileDialog()
        sDialog.DefaultExt = ".AppSettings"
        sDialog.Filter = "Application Settings (*.AppSettings)|*AppSettings"
        sDialog.InitialDirectory = appData + "\JTechMe\JumpGo\DevEd\Settings"
        'sDialog.FileName = "MySettings.AppSettings"
        sDialog.FileName = "MySettings.config"
        sDialog.CreatePrompt = False

        If Directory.Exists(appData + "\JTechMe\JumpGo\DevEd\Settings\") = False Then
            My.Computer.FileSystem.CreateDirectory(
                appData + "\JTechMe\JumpGo\DevEd\Settings")
        End If

        'Using sWriter As New StreamWriter(SettingFileName)
        'For Each setting As Configuration.SettingsPropertyValue In My.Settings.PropertyValues
        'sWriter.WriteLine(setting.Name & "," & setting.PropertyValue.ToString())
        'sWriter.WriteLine(setting.Name & "," & setting.PropertyValue)
        'sWriter.Close()
        'Next
        'End Using

        'My.Settings.Save()
        'MessageBox.Show("Settings has been saved to the specified file", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)


        'For Each setting As Configuration.SettingsPropertyValue In My.Settings.PropertyValues
        'sWriter.WriteLine(setting.Name & "," & setting.PropertyValue.ToString())
        'sWriter.Write("This is a demo using a StreamWriter object.")
        'sWriter.Close()
        'File.WriteAllText(SettingFileName, "This is a demo using the File class")
        'File.WriteAllText(SettingFileName, setting.Name & "," & setting.PropertyValue.ToString())
        'Next

        My.Settings.Save()
        'MessageBox.Show("Settings has been saved to the specified file", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub importSettings()

        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

        Dim oDialog As New OpenFileDialog
        oDialog.Filter = "Application Settings (*.AppSettings)|*AppSettings"
        oDialog.InitialDirectory = appData + "\JTechMe\JumpGo\DevEd\Settings"
        oDialog.FileName = "MySettings.AppSettings"
        oDialog.OpenFile()

        'If oDialog.ShowDialog() = DialogResult.OK Then

        Using sReader As New StreamReader(oDialog.FileName)

                While sReader.Peek() > 0

                    Dim input = sReader.ReadLine()

                    ' Split comma delimited data ( SettingName,SettingValue )  
                    Dim dataSplit = input.Split(CChar(","))

                    '           Setting         Value  
                    My.Settings(dataSplit(0)) = dataSplit(1)

                End While

            End Using

            My.Settings.Save()

        'MessageBox.Show("Import of settings successfull", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'End If
    End Sub

    Private Sub JumpGo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.CurVerIDAvai = My.Application.Info.Version.ToString
        If File.Exists(GetFolderPath(SpecialFolder.ApplicationData) + "\JTechMe\JumpGo\DevEd\Settings\MySettings.AppSettings") = True Then
            'importSettings()
        End If

        If My.Settings.SetIcon = "Dev" Then
            Me.Icon = My.Resources.JumpGo_Dev_Edition_Updated
        Else
            If My.Settings.SetIcon = "JG4" Then
                Me.Icon = My.Resources.JumpGo_4_2_Updated
            Else
                If My.Settings.SetIcon = "JG3" Then
                    Me.Icon = My.Resources.JumpGo_3_Icon
                Else
                    If My.Settings.SetIcon = "JG2" Then
                        Me.Icon = My.Resources.JumpGo_Icon
                    Else
                        If My.Settings.SetIcon = "JG1" Then
                            Me.Icon = My.Resources.JumpGo_1_Icon
                        End If
                    End If
                End If
            End If
        End If

        Me.Size = My.Settings.LastSize
        'Here we have the array of 3 buttons (0, 1, 2) which will be our ThumbnailToolbarButtons
        Dim array(4) As ThumbnailToolBarButton
        array(0) = TaskButton1
        array(1) = TaskButton2
        array(2) = TaskButton3
        array(3) = TaskButton4
        array(4) = TaskButton5
        'Here we make use of the API to add the buttons. It will handle the form itself, and use the array of buttons we defined earlier
        'TaskbarManager.Instance.ThumbnailToolBars.AddButtons(Me.Handle, array)

        'NewTabButton("")

        TabControl1.TabPages.Add(TabButtonNew).CloseButtonVisible = False

        'NewTabButton("")
        If My.Settings.FirstRun = True Then
            CreateNewTab(Environment.CurrentDirectory + "\Getting Started.html")
            My.Settings.FirstRun = False
            'Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Else
            CreateNewTab(My.Settings.Home)
        End If

        'NewTabButton("")

        Dim glasseffect As New rtaGlassEffectsLib.rtaGlassEffect

        glasseffect.TopBarSize = 31

        glasseffect.ShowEffect(Me)
        glasseffect.UseHandCursorOnTitle = False
        glasseffect.BottomBarSize = 0
        glasseffect.LeftBarSize = 0
        glasseffect.ShowEffect(Me)

        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        'Me.ControlBox = False
        'Me.Text = ""

        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

        If File.Exists(appData + "\JTechMe\JumpGo\StandardEd\Special\") = True Then

        Else
            My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "/Special", appData + "\JTechMe\JumpGo\StandardEd\Special", True)
        End If

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
        'Timer1.Start()
    End Sub

    Private Sub JumpGoMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        exportSettings()
    End Sub

    Function OpenHistoryTab(ByRef url As String)

        Dim t As New HistoryForm
        'FasterBrowser1.Navigate("http://www.google.com/")
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Function CreateNewTab(ByRef url As String)

        Dim t As New Tab
        t.FasterBrowser1.Navigate(url)
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Function NewInfoTab(ByRef url As String)

        Dim t As New InfoTab
        't.FasterBrowser1.Navigate("http://www.google.com/")
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Function NewTabButton(ByRef url As String)

        'Dim t As New TabButtonNew
        Dim t As New Tab
        'Dim t As New NewTab_Button
        t.FasterBrowser1.Navigate(My.Settings.NewTab)
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        CreateNewTab(My.Settings.NewTab)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Me.Text = TabControl1.SelectedForm.text
        My.Settings.LastSize = Me.Size
        'If TabControl1.SelectedForm.text = "About JumpGo Developer Edition" Then
        'If TabControl1.SelectedForm.text = " " Then
        'CreateNewTab("http://www.google.com/")
        'End If
        'If TabControl1.SelectedForm = Nothing Then
        'Me.Close()
        'Else
        'Me.Text = TabControl1.SelectedForm.text
        'End If
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs)
        'Button1.ForeColor = Color.Blue
    End Sub

    Private Sub JumpGoMain_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        Dim fore As Color = Color.FromKnownColor(KnownColor.ControlText)
        'Button1.ForeColor = fore
    End Sub

    Private Sub TabControl1_MouseHover(sender As Object, e As EventArgs) Handles TabControl1.MouseHover
        Dim fore As Color = Color.FromKnownColor(KnownColor.ControlText)
        'Button1.ForeColor = fore
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CreateNewTab(My.Settings.NewTab)
    End Sub
End Class