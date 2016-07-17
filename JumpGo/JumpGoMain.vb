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
Imports System.Runtime.InteropServices

Public Class JumpGoMain

    Dim timedExtraction As Integer = 0

#Region "Buttons in Dev Ed"
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

    Private Sub TaskButton5Click(ByVal sender As System.Object,
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton5.Click
        Dim SecondForm As New JumpGoMain
        SecondForm.Show()
    End Sub
#End Region

    Private browser As GeckoWebBrowser

#Region "New Aero pt 1"
    'begining of the new aero extender function
    <Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)> Public Structure Side
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure
    <Runtime.InteropServices.DllImport("dwmapi.dll")> Public Shared Function DwmExtendFrameIntoClientArea(ByVal hWnd As IntPtr, ByRef pMarinset As Side) As Integer
    End Function
    'end of the aero extender
#End Region

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

#Region "Old Import Export Settings Subs"
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
#End Region

    Private Sub JumpGo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim jgAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)

        'Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))

#Region "New Aero pt 2"
        'aero extending try
        Try
            Me.BackColor = Color.Black 'It must be set to black...
            Dim Side As Side = New Side
            Side.Left = 0
            Side.Right = 0
            Side.Top = 31
            Side.Bottom = 0
            Dim result As Integer = DwmExtendFrameIntoClientArea(Me.Handle, Side)
        Catch ex As Exception
        End Try
        'end of aero extending try
#End Region

        My.Settings.CurVerIDAvai = My.Application.Info.Version.ToString
        If File.Exists(GetFolderPath(SpecialFolder.ApplicationData) + "\JTechMe\JumpGo\StandardEd\Settings\MySettings.AppSettings") = True Then
            'importSettings()
        End If

#Region "Theming"
        'This will set the JumpGo icon to whatever current or legacy icon the user chooses
        If My.Settings.SetIcon = "Dev" Then 'This option is only available in JG Dev Ed
            Me.Icon = My.Resources.JumpGo_Dev_Edition_Updated
        Else
            If My.Settings.SetIcon = "JG4" Then 'This is the most up-to-date icon as of 6/15/16
                Me.Icon = My.Resources.JumpGo_4_2_Updated
            Else
                If My.Settings.SetIcon = "JG3" Then 'This is the JumpGo 3 icon
                    Me.Icon = My.Resources.JumpGo_3_Icon
                Else
                    If My.Settings.SetIcon = "JG2" Then 'This is the JumpGo 2 icon
                        Me.Icon = My.Resources.JumpGo_Icon
                    Else
                        If My.Settings.SetIcon = "JG1" Then 'This is the first JumpGo icon
                            Me.Icon = My.Resources.JumpGo_1_Icon
                        End If
                    End If
                End If
            End If
        End If

        'This will set the aero transparency for the MDI Tab Control
        If My.Settings.AeroTabs = True Then
            TabControl1.TabGlassGradient = True
        Else
            TabControl1.TabGlassGradient = False
        End If

        ''This is where the rtaGlassEffect begins
        'Dim glasseffect As New rtaGlassEffectsLib.rtaGlassEffect

        'glasseffect.TopBarSize = 31

        'glasseffect.ShowEffect(Me)
        'glasseffect.UseHandCursorOnTitle = False
        'glasseffect.BottomBarSize = 0
        'glasseffect.LeftBarSize = 0
        'glasseffect.ShowEffect(Me)
        ''This is where it ends
#End Region

        'This will set the size and window state of the JumpGoMain.vb form
        If My.Settings.LastWinState = "normal" Then 'This first checks if the last window state is normal
            Me.Size = My.Settings.LastSize 'If so this will set the form size to the last saved size
        Else 'This will run if the last saved window state was not normal
            If My.Settings.LastWinState = "maximized" Then 'This checks if the last saved window state is maximized
                Me.WindowState = FormWindowState.Maximized 'If so this will set the current window state to maximized
            End If
        End If

#Region "More Buttons in Dev Ed"
        'Here we have the array of 3 buttons (0, 1, 2) which will be our ThumbnailToolbarButtons
        Dim array(4) As ThumbnailToolBarButton
        array(0) = TaskButton1
        array(1) = TaskButton2
        array(2) = TaskButton3
        array(3) = TaskButton4
        array(4) = TaskButton5
        'Here we make use of the API to add the buttons. It will handle the form itself, and use the array of buttons we defined earlier
        'TaskbarManager.Instance.ThumbnailToolBars.AddButtons(Me.Handle, array)
#End Region

        'NewTabButton("")

        'TabControl1.TabPages.Add(TabButtonNew).CloseButtonVisible = False

        'NewTabButton("")
        If My.Settings.FirstRun = True Then
            CreateNewTab(Environment.CurrentDirectory + "\Getting Started.html")
            My.Settings.Upgrade()
            My.Settings.FirstRun = False
            'This will set the size and window state of the JumpGoMain.vb form
            If My.Settings.LastWinState = "normal" Then 'This first checks if the last window state is normal
                Me.Size = My.Settings.LastSize 'If so this will set the form size to the last saved size
            Else 'This will run if the last saved window state was not normal
                If My.Settings.LastWinState = "maximized" Then 'This checks if the last saved window state is maximized
                    Me.WindowState = FormWindowState.Maximized 'If so this will set the current window state to maximized
                End If
            End If
        Else
            CreateNewTab(My.Settings.Home)
        End If

        'NewTabButton("")

        'Me.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        'Me.ControlBox = False
        'Me.Text = ""

        Timer2.Start()
    End Sub

    Private Sub JumpGoMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        exportSettings()
    End Sub

#Region "Tab Page Creators"
    Function OpenHistoryTab(ByRef url As String)

        Dim t As New HistoryForm
        'FasterBrowser1.Navigate("http://www.google.com/")
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Function CreateNewTab(ByRef url As String)
        'url = My.Settings.NewTab
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

    Function TabPlusBttn()

        TabControl1.TabPages.Add(TabButtonNew).CloseButtonVisible = False
        'TabControl1.TabPages.Add(TabButtonNew).Name = "meow123"

        Return 0
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        CreateNewTab(My.Settings.NewTab)
    End Sub
#End Region

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If ToolStrip2.Visible = True Then
            'aero extending try
            Try
                Me.BackColor = Color.Black 'It must be set to black...
                Dim Side As Side = New Side
                Side.Left = 0
                Side.Right = 0
                Side.Top = 56
                Side.Bottom = 0
                Dim result As Integer = DwmExtendFrameIntoClientArea(Me.Handle, Side)
            Catch ex As Exception
            End Try
            'end of aero extending try
        Else
            'aero extending try
            Try
                Me.BackColor = Color.Black 'It must be set to black...
                Dim Side As Side = New Side
                Side.Left = 0
                Side.Right = 0
                Side.Top = 31
                Side.Bottom = 0
                Dim result As Integer = DwmExtendFrameIntoClientArea(Me.Handle, Side)
            Catch ex As Exception
            End Try
            'end of aero extending try
        End If

        'Me.Text = TabControl1.SelectedForm.text
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.LastSize = Me.Size
            My.Settings.LastWinState = "normal"
        Else
            If Me.WindowState = FormWindowState.Maximized Then
                My.Settings.LastWinState = "maximized"
            Else
                If Me.WindowState = FormWindowState.Minimized Then
                    My.Settings.LastWinState = "normal"
                End If
            End If
        End If
        'If TabControl1.TabPages.Count = 0 Then
        'Me.Close()
        'End If
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

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        CreateNewTab(My.Settings.NewTab)
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If TabControl1.TabPages.Count = 0 Then
            Me.Close()
        End If
    End Sub

    Private Sub MenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.Click
        If ToolStrip2.Visible = False Then
            ToolStrip2.Visible = True
        Else
            ToolStrip2.Visible = False
        End If
    End Sub

    Private Sub NewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewTabToolStripMenuItem.Click
        CreateNewTab(My.Settings.NewTab)
    End Sub

    Private Sub NewWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewWindowToolStripMenuItem.Click
        Dim SecondForm As New JumpGoMain
        SecondForm.Show()
    End Sub

    Private Sub NewPrivateWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewPrivateWindowToolStripMenuItem.Click
        Dim NewIncognito As New IncognitoMain
        NewIncognito.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class