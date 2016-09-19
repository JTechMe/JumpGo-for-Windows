Imports Microsoft.WindowsAPICodePack
Imports Microsoft.WindowsAPICodePack.Taskbar
Imports Microsoft.WindowsAPICodePack.Shell
Public Class IncognitoMain

    Public WithEvents TaskButton1 As New ThumbnailToolBarButton(My.Resources.imageres_5302, "Web Source")
    Public WithEvents TaskButton2 As New ThumbnailToolBarButton(My.Resources.IconPlus, "New Tab")
    Public WithEvents TaskButton3 As New ThumbnailToolBarButton(My.Resources.imageres_5306, "Source Writer")
    Public WithEvents TaskButton4 As New ThumbnailToolBarButton(My.Resources.imageres_5369, "New Incognito Window")

    Private Sub TaskButton1Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton1.Click
        WebSource.Visible = True
    End Sub

    Private Sub TaskButton2Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton2.Click
        CreateNewTab("")
    End Sub

    Private Sub TaskButton3Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton3.Click
        SourceWriter.Visible = True
    End Sub

    Private Sub TaskButton4Click(ByVal sender As System.Object, _
                           ByVal e As Microsoft.WindowsAPICodePack.Taskbar.ThumbnailButtonClickedEventArgs) _
                           Handles TaskButton4.Click
        Dim SecondForm As New IncognitoMain
        SecondForm.Show()
    End Sub

    Private Sub JumpGo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Here we have the array of 3 buttons (0, 1, 2) which will be our ThumbnailToolbarButtons
        Dim array(3) As ThumbnailToolBarButton
        array(0) = TaskButton1
        array(1) = TaskButton2
        array(2) = TaskButton3
        array(3) = TaskButton4
        'Here we make use of the API to add the buttons. It will handle the form itself, and use the array of buttons we defined earlier
        TaskbarManager.Instance.ThumbnailToolBars.AddButtons(Me.Handle, array)

        'TabControl1.TabPages.Add(IncognitoTabButtonNew)

        'NewTabButton("")
        If My.Settings.FirstRun = True Then
            CreateNewTab(Environment.CurrentDirectory + "\Getting Started.html")
            My.Settings.FirstRun = False
            Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Else
            CreateNewTab(My.Settings.Home)
        End If
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
    End Sub



    Function CreateNewTab(ByRef url As String)

        Dim t As New IncognitoTab
        't.FasterBrowser1.Navigate("http://www.google.com/")
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    Function NewTabButton(ByRef url As String)

        Dim t As New TabButtonNew
        'Dim t As New NewTab_Button
        't.FasterBrowser1.Navigate("http://www.google.com/")
        Dim NewTab = TabControl1.TabPages.Add(t)

        'Dim TabButton = TabControl1.TabPages.Add(t)
        Return 0
    End Function

    'Function NewTabButton(ByRef url As String)

    'Dim t As New AboutBox1
    'Dim t As New NewTab_Button
    't.FasterBrowser1.Navigate("http://www.google.com/")
    'Dim NewTab = TabControl1.TabPages.Add(t)

    'Dim TabButton = TabControl1.TabPages.Add(t)
    'Return 0
    'End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        CreateNewTab(My.Settings.NewTab)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Text = TabControl1.SelectedForm.text
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)
        CreateNewTab(My.Settings.NewTab)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CreateNewTab(My.Settings.NewTab)
    End Sub
End Class