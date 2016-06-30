Imports System.IO
Imports System.IO.Compression
Public Class ThemeDesign

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Button1.BackgroundImage = My.Resources.BackCircle
        Button2.BackgroundImage = My.Resources.ForwardSquare
        Button3.BackgroundImage = My.Resources.ForwardSquare
        Button4.BackgroundImage = My.Resources.Refresh
        Button5.BackgroundImage = My.Resources.homeTransparent
        Button6.BackgroundImage = My.Resources.SearchButton
        Button7.BackgroundImage = My.Resources.MenuTransparent

        Tab.Button1.BackgroundImage = My.Resources.BackCircle
        Tab.Button2.BackgroundImage = My.Resources.ForwardSquare
        'Tab.Button3.BackgroundImage = My.Resources.ForwardSquare
        Tab.Button4.BackgroundImage = My.Resources.Refresh
        Tab.Button5.BackgroundImage = My.Resources.homeTransparent
        Tab.Button6.BackgroundImage = My.Resources.SearchButton
        Tab.Button7.BackgroundImage = My.Resources.MenuTransparent
        My.Settings.ThemeColor = "Light"
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        Button1.BackgroundImage = My.Resources.BackCircle
        Button2.BackgroundImage = My.Resources.ForwardSquare
        Button3.BackgroundImage = My.Resources.ForwardSquare
        Button4.BackgroundImage = My.Resources.Refresh
        Button5.BackgroundImage = My.Resources.homeTransparent
        Button6.BackgroundImage = My.Resources.SearchButton
        Button7.BackgroundImage = My.Resources.MenuTransparent

        Tab.Button1.BackgroundImage = My.Resources.BackCircle
        Tab.Button2.BackgroundImage = My.Resources.ForwardSquare
        'Tab.Button3.BackgroundImage = My.Resources.ForwardSquare
        Tab.Button4.BackgroundImage = My.Resources.Refresh
        Tab.Button5.BackgroundImage = My.Resources.homeTransparent
        Tab.Button6.BackgroundImage = My.Resources.SearchButton
        Tab.Button7.BackgroundImage = My.Resources.MenuTransparent
        My.Settings.ThemeColor = "Dark"
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        OpenFileDialog1.ShowDialog()
        Panel1.BackgroundImage = System.Drawing.Image.FromFile(OpenFileDialog1.FileName)
        'Panel4.BackgroundImage = System.Drawing.Image.FromFile(OpenFileDialog1.FileName)
        TextBox3.Text = OpenFileDialog1.FileName
        My.Settings.NavPanelBG = OpenFileDialog1.FileName
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        ColorDialog1.ShowDialog()
        Panel1.BackColor = ColorDialog1.Color
        TextBox4.Text = ColorDialog1.Color.ToString
    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        Dim back As Color = Color.FromKnownColor(KnownColor.Control)
        Panel1.BackColor = back
        TextBox4.Text = ""
        My.Settings.NavPanelBG = ""
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        OpenFileDialog1.ShowDialog()
        'Panel1.BackgroundImage = System.Drawing.Image.FromFile(OpenFileDialog1.FileName)
        Panel4.BackgroundImage = System.Drawing.Image.FromFile(OpenFileDialog1.FileName)
        TextBox5.Text = OpenFileDialog1.FileName
        My.Settings.NavPanelBG = OpenFileDialog1.FileName
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        ColorDialog1.ShowDialog()
        Panel4.BackColor = ColorDialog1.Color
        TextBox6.Text = ColorDialog1.Color.ToString
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        Dim back As Color = Color.FromKnownColor(KnownColor.Control)
        Panel4.BackColor = back
        TextBox6.Text = ""
        My.Settings.MenuPanelBG = ""
    End Sub

    Private Sub ThemeDesign_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'My.Computer.FileSystem.RenameFile("C:\Users\PopTart\Pictures\JumpGo\Test Theme\Aqua.Blue.MenuPanel", "Blue.MenuPanel")
        'Dim startPath As String = "C:\Users\PopTart\Pictures\JumpGo\Test Theme"
        'Dim zipPath As String = "C:\Program Files (x86)\JTechMe\JumpGo\result.zip"
        'Dim extractPath As String = "C:\Program Files (x86)\JTechMe\JumpGo\extract"
        'ZipFile.CreateFromDirectory(startPath, zipPath)
        'ZipFile.ExtractToDirectory(zipPath, extractPath)
        If My.Settings.NavPanelBG = "" Then
            Panel1.BackgroundImage = My.Resources.Transparent
        Else
            Panel1.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.NavPanelBG)
        End If

        If My.Settings.MenuPanelBG = "" Then
            Panel4.BackgroundImage = My.Resources.Transparent
        Else
            Panel4.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.MenuPanelBG)
        End If

        If My.Settings.ThemeColor = "Dark" Then
            Button1.BackgroundImage = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            Button3.BackgroundImage = My.Resources.ForwardSquare
            Button4.BackgroundImage = My.Resources.Refresh
            Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            Button7.BackgroundImage = My.Resources.MenuTransparent
        Else
            Button1.BackgroundImage = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            Button3.BackgroundImage = My.Resources.ForwardSquare
            Button4.BackgroundImage = My.Resources.Refresh
            Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            Button7.BackgroundImage = My.Resources.MenuTransparent
        End If

        If My.Settings.HighContrast = True Then
            Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            Button27.Text = "High Contrast Mode - Enabled"
            My.Settings.HighContrast = True
        Else
            Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            Button27.Text = "High Contrast Mode - Disabled"
        End If
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If TextBox7.Text = "" Then
            MessageBox.Show("Please enter the name of the theme before loading it.")
        Else
            OpenFileDialog2.ShowDialog()
            'Dim startPath As String = "C:\Users\PopTart\Pictures\JumpGo\Test Theme"
            Dim zipPath As String = OpenFileDialog2.FileName
            Dim extractPath As String = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text
            'ZipFile.CreateFromDirectory(startPath, zipPath)
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            Panel1.BackgroundImage = System.Drawing.Image.FromFile("C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\NavPanel.png")
            My.Settings.NavPanelBG = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\NavPanel.png"
            Panel4.BackgroundImage = System.Drawing.Image.FromFile("C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\MenuPanel.png")
            My.Settings.MenuPanelBG = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\MenuPanel.png"
        End If
    End Sub

    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        FolderBrowserDialog1.ShowDialog()
        Panel1.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog1.SelectedPath & "\NavPanel.png")
        My.Settings.NavPanelBG = FolderBrowserDialog1.SelectedPath & "\NavPanel.png"
        Panel4.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog1.SelectedPath & "\MenuPanel.png")
        My.Settings.MenuPanelBG = FolderBrowserDialog1.SelectedPath & "\MenuPanel.png"
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If My.Settings.NavPanelBG = "" Then

        Else
            Panel1.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.NavPanelBG)
        End If

        If My.Settings.MenuPanelBG = "" Then

        Else
            Panel4.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.MenuPanelBG)
        End If
    End Sub

    Private Sub Button26_Click(sender As Object, e As EventArgs) Handles Button26.Click
        FolderBrowserDialog2.ShowDialog()
        Panel1.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog2.SelectedPath & "\NavPanel.png")
        My.Settings.NavPanelBG = FolderBrowserDialog2.SelectedPath & "\NavPanel.png"
        Panel4.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog2.SelectedPath & "\MenuPanel.png")
        My.Settings.MenuPanelBG = FolderBrowserDialog2.SelectedPath & "\MenuPanel.png"
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        If My.Settings.HighContrast = False Then
            Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
            Button27.Text = "High Contrast Mode - Enabled"
            My.Settings.HighContrast = True
        Else
            Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
            Button27.Text = "High Contrast Mode - Disabled"
            My.Settings.HighContrast = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            If My.Settings.HighContrast = True Then
                Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
                Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
                InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
                HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient_HC
                Button27.Text = "High Contrast Mode - Enabled"
            Else
                Panel1.BackgroundImage = My.Resources.JGTabControlGradient
                Tab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
                InfoTab.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
                HistoryForm.Panel1.BackgroundImage = My.Resources.JGTabControlGradient
                Button27.Text = "High Contrast Mode - Disabled"
            End If
            GroupBox1.Enabled = True
            My.Settings.NavPnlBGEnabled = True
        Else
            Tab.Panel1.BackgroundImage = Nothing
            My.Settings.NavPnlBGEnabled = False
        End If
    End Sub
End Class