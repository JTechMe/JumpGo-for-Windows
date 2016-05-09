Imports System.IO
Imports System.IO.Compression
Public Class Settings
    'The region below is required for the template form to function correctly.
    'The rest of the template iis completely customizable.

#Region "Common"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please enter a valid search url")
        Else
            If RadioButton4.Checked = True Then
                My.Settings.UserSearch = TextBox1.Text
                My.Settings.Search = My.Settings.UserSearch
            End If
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            My.Settings.Search = My.Settings.Google
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            My.Settings.Search = My.Settings.Yahoo
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            My.Settings.Search = My.Settings.Bing
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        PictureBox1.Image = My.Resources.JumpGo_4_2
        AboutBox1.PictureBox1.Image = My.Resources.JumpGo_4_2
        JumpGoMain.Icon = My.Resources.JumpGo_4_2_Updated
        My.Settings.SetIcon = "JG4"
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        PictureBox1.Image = My.Resources.JumpGo_3
        AboutBox1.PictureBox1.Image = My.Resources.JumpGo_3
        JumpGoMain.Icon = My.Resources.JumpGo_3_Icon
        My.Settings.SetIcon = "JG3"
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        PictureBox1.Image = My.Resources.JumpGo___Copy
        AboutBox1.PictureBox1.Image = My.Resources.JumpGo___Copy
        JumpGoMain.Icon = My.Resources.JumpGo_Icon
        My.Settings.SetIcon = "JG2"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Then
            MessageBox.Show("Please enter a valid url")
        Else
            My.Settings.Home = TextBox2.Text
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox3.Text = "" Then
            MessageBox.Show("Please enter a valid url")
        Else
            My.Settings.NewTab = TextBox3.Text
        End If
    End Sub

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.SetIcon = "Dev" Then
            RadioButton9.Checked = True
        Else
            If My.Settings.SetIcon = "JG4" Then
                RadioButton5.Checked = True
            Else
                If My.Settings.SetIcon = "JG3" Then
                    RadioButton6.Checked = True
                Else
                    If My.Settings.SetIcon = "JG2" Then
                        RadioButton7.Checked = True
                    Else
                        If My.Settings.SetIcon = "JG1" Then
                            RadioButton8.Checked = True
                        End If
                    End If
                End If
            End If
        End If

        TextBox1.Text = My.Settings.UserSearch
        TextBox2.Text = My.Settings.Home
        TextBox3.Text = My.Settings.NewTab
        If My.Settings.Search = My.Settings.Google Then
            RadioButton1.Checked = True
        Else
            RadioButton1.Checked = False
        End If
        If My.Settings.Search = My.Settings.Yahoo Then
            RadioButton2.Checked = True
        Else
            RadioButton2.Checked = False
        End If
        If My.Settings.Search = My.Settings.Bing Then
            RadioButton3.Checked = True
        Else
            RadioButton3.Checked = False
        End If
        If My.Settings.Search = My.Settings.UserSearch Then
            RadioButton4.Checked = True
        Else
            RadioButton4.Checked = False
        End If
        If My.Settings.Search = My.Settings.GoogleDevelopersSearch Then
            RadioButton12.Checked = True
        Else
            RadioButton12.Checked = False
        End If
        If My.Settings.Search = My.Settings.GitHubSearch Then
            RadioButton13.Checked = True
        Else
            RadioButton13.Checked = False
        End If
        If My.Settings.Search = My.Settings.DuckDuckGoSearch Then
            RadioButton14.Checked = True
        Else
            RadioButton14.Checked = False
        End If
        If My.Settings.Search = My.Settings.WikipediaSearch Then
            RadioButton15.Checked = True
        Else
            RadioButton15.Checked = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ThemeDesign.Visible = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox4.Text = "" Then
            MessageBox.Show("Please enter the name of the theme before loading it.")
        Else
            OpenFileDialog1.ShowDialog()
            'Dim startPath As String = "C:\Users\PopTart\Pictures\JumpGo\Test Theme"
            Dim zipPath As String = OpenFileDialog1.FileName
            Dim extractPath As String = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox4.Text
            'ZipFile.CreateFromDirectory(startPath, zipPath)
            ZipFile.ExtractToDirectory(zipPath, extractPath)
            'panel1.BackgroundImage = System.Drawing.Image.FromFile("C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\NavPanel.png")
            My.Settings.NavPanelBG = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox4.Text & "\NavPanel.png"
            'Panel4.BackgroundImage = System.Drawing.Image.FromFile("C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox7.Text & "\MenuPanel.png")
            My.Settings.MenuPanelBG = "C:\Program Files (x86)\JTechMe\JumpGo\Themes\" & TextBox4.Text & "\MenuPanel.png"
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FolderBrowserDialog1.ShowDialog()
        'panel1.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog1.SelectedPath & "\NavPanel.png")
        My.Settings.NavPanelBG = FolderBrowserDialog1.SelectedPath & "\NavPanel.png"
        'Panel4.BackgroundImage = System.Drawing.Image.FromFile(FolderBrowserDialog1.SelectedPath & "\MenuPanel.png")
        My.Settings.MenuPanelBG = FolderBrowserDialog1.SelectedPath & "\MenuPanel.png"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Settings.NavPanelBG = ""
        My.Settings.MenuPanelBG = ""
    End Sub
#End Region

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        PictureBox1.Image = My.Resources.JumpGo_1
        AboutBox1.PictureBox1.Image = My.Resources.JumpGo_1
        JumpGoMain.Icon = My.Resources.JumpGo_1_Icon
        My.Settings.SetIcon = "JG1"
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        PictureBox1.Image = My.Resources.JumpGo_Dev_Edition
        AboutBox1.PictureBox1.Image = My.Resources.JumpGo_Dev_Edition
        JumpGoMain.Icon = My.Resources.JumpGo_Dev_Edition_Updated
        My.Settings.SetIcon = "Dev"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        AddOnSettings.Visible = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        AppBuilder.Visible = True
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        If RadioButton12.Checked = True Then
            My.Settings.Search = My.Settings.GoogleDevelopersSearch
        End If
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        If RadioButton13.Checked = True Then
            My.Settings.Search = My.Settings.GitHubSearch
        End If
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        If RadioButton14.Checked = True Then
            My.Settings.Search = My.Settings.DuckDuckGoSearch
        End If
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        If RadioButton15.Checked = True Then
            My.Settings.Search = My.Settings.WikipediaSearch
        End If
    End Sub
End Class
