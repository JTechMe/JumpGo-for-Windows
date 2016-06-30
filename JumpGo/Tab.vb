'Imports Skybound.Gecko
Imports System.Net
Imports System.IO

Imports Gecko
'Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Environment

Public Class Tab

    Dim MenuOpen As Boolean = False
    Private browser As GeckoWebBrowser

    Sub New()
        'Xpcom.Initialize("Firefox")
        InitializeComponent()
        'Xpcom.Initialize(Environment.CurrentDirectory + "/xulrunner")
        Dim app_dir = Path.GetDirectoryName(Application.ExecutablePath)
        Gecko.Xpcom.Initialize(Path.Combine(app_dir, "xulrunner"))
        'browser = New GeckoWebBrowser()

        'browser.Dock = DockStyle.Fill
        'Me.browser.Name = "browser"

        'Me.Controls.Add(browser)
        'Xpcom.CreateInstance(Of AppDomainManager)("@mozilla.org/login-manager;1")
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If FasterBrowser1.CanGoBack Then
            FasterBrowser1.GoBack()
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If FasterBrowser1.CanGoBack Then
            FasterBrowser1.GoForward()
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button3()
        If TextBox1.Text = "JumpGo://Welcome" Then
            FasterBrowser1.Navigate(Environment.CurrentDirectory + "\Welcome\index.html")
            TextBox1.Text = "JumpGo://Welcome"
        Else
            FasterBrowser1.Navigate(TextBox1.Text)
            'AxWebBrowser1.Navigate(TextBox1.Text)
            Dim uri As Uri

            If System.Uri.TryCreate(TextBox1.Text, UriKind.RelativeOrAbsolute, uri) Then
                ' Navigate to it
                FasterBrowser1.Navigate(TextBox1.Text)
            Else
                ' Treat it as a search
                If TextBox1.Text.Contains(" ") Then
                    TextBox1.Text.Replace(" "c, "+"c)
                Else
                    FasterBrowser1.Navigate(My.Settings.Search + TextBox1.Text)
                End If
            End If
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If TextBox1.Text = "JumpGo://Welcome" Then
            FasterBrowser1.Navigate(Environment.CurrentDirectory + "\Welcome\index.html")
            TextBox1.Text = "JumpGo://Welcome"
        Else
            FasterBrowser1.Navigate(TextBox1.Text)
            'AxWebBrowser1.Navigate(TextBox1.Text)
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        FasterBrowser1.Reload()
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        FasterBrowser1.Navigate(My.Settings.Home)
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_Click(sender As Object, e As EventArgs) Handles FasterBrowser1.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_MouseClick(sender As Object, e As MouseEventArgs) Handles FasterBrowser1.MouseClick
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_MouseDown(sender As Object, e As MouseEventArgs) Handles FasterBrowser1.MouseDown
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_MouseHover(sender As Object, e As EventArgs) Handles FasterBrowser1.MouseHover
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_DomFocus(sender As Object, e As EventArgs) Handles FasterBrowser1.DomFocus
        'm_strInnerHtml = FasterBrowser1.Document.ActiveElement.GetAttribute("href")
    End Sub

    Private Sub FasterBrowser1_Navigating(sender As Object, e As EventArgs) Handles FasterBrowser1.Navigating
        Me.Text = "Connecting..."
    End Sub

    Private Sub FasterBrowser1_Navigated(sender As Object, e As GeckoNavigatedEventArgs) Handles FasterBrowser1.Navigated
        FasterBrowser1.ContextMenuStrip = ContextMenuStrip1
        FasterBrowser1.ContextMenu = ContextMenuStrip1.ContextMenu

        'AxWebBrowser1.Navigate(FasterBrowser1.Url)
        'FasterBrowser1.Url.ToString()
        TextBox1.Text = FasterBrowser1.Url.ToString
        Dim cz119 As String = FasterBrowser1.Url.ToString
        'Me.Icon
        'Me.Text = FasterBrowser1.Document.Title.ToString
        'Me.Text = FasterBrowser1.Text.ToString
        'Me.Text = FasterBrowser1.DocumentTitle.ToString
        'Me.Text = FasterBrowser1.Url.AbsoluteUri
        If cz119 = Environment.CurrentDirectory + "/Welcome/index.html" Then
            TextBox1.Text = "JumpGo://Welcome"
        End If
        If cz119.Contains(Environment.CurrentDirectory + "/Welcome/index.html") Then
            TextBox1.Text = "JumpGo://Welcome"
        End If
        If cz119.Contains("Welcome/index.html") Then
            TextBox1.Text = "JumpGo://Welcome"
        End If
        If cz119.Contains("https://") Then
            Button20.BackgroundImage = My.Resources.secured
        Else
            Button20.BackgroundImage = My.Resources.unsecured
        End If
        FavIconChange()
        geticon()
        Dim obj As [Object]
        obj = FasterBrowser1.Url.AbsoluteUri
        'My.Settings.WebHistory.Items.Add(obj)

        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquare
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If

        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircle
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If

        Me.ToolTip1.ToolTipTitle = FasterBrowser1.DocumentTitle
    End Sub

    Private Sub FasterBrowser1_DocumentCompleted(sender As Object, e As EventArgs) Handles FasterBrowser1.DocumentCompleted
        FasterBrowser1.ContextMenuStrip = ContextMenuStrip1
        FasterBrowser1.ContextMenu = ContextMenuStrip1.ContextMenu

        'AxWebBrowser1.Navigate(FasterBrowser1.Url)
        'FasterBrowser1.Url.ToString()
        'TextBox1.Text = FasterBrowser1.Url.ToString
        Dim cz119 As String = FasterBrowser1.Url.ToString
        'Me.Icon
        'Me.Text = FasterBrowser1.Document.Title.ToString
        'Me.Text = FasterBrowser1.Text.ToString
        Me.Text = FasterBrowser1.DocumentTitle.ToString
        'Me.Text = FasterBrowser1.Url.AbsoluteUri
        If FasterBrowser1.Url.AbsolutePath = Environment.CurrentDirectory + "/Welcome/index.html" Then
            TextBox1.Text = "JumpGo: //Welcome"
        End If
        If cz119 = Environment.CurrentDirectory + "/Welcome/index.html" Then
            TextBox1.Text = "JumpGo://Welcome"
        End If
        If cz119.Contains(Environment.CurrentDirectory + "/Welcome/index.html") Then
            TextBox1.Text = "JumpGo://Welcome"
        End If
        If cz119.Contains("https://") Then
            Button20.BackgroundImage = My.Resources.secured
        Else
            Button20.BackgroundImage = My.Resources.unsecured
        End If
        FavIconChange()
        geticon()
        Dim obj As [Object]
        obj = FasterBrowser1.Url
        'My.Settings.WebHistory.Items.Add(FasterBrowser1.Url.ToString)

        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        'JumpGoMain.CreateNewTab(appData + "\JTechMe\JumpGo\StandardEd\Special")

        'Dim reader As New StreamReader(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index.html")
        'My.Computer.FileSystem.CopyFile(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index.html", appData + "\JTechMe\JumpGo\StandardEd\Special\History\index2.html", True)
        'Dim writer As New StreamWriter(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index2.html")

        'Dim s = reader.ReadToEnd().Replace("<!-- This Line is Altered for History Record -->", "<!-- This Line is Altered for History Record -->" +
        'FasterBrowser1.Url.AbsoluteUri.ToString)

        'Dim allLines As List(Of String) = New List(Of String)
        'Do While Not reader.EndOfStream
        'allLines.Add(reader.ReadLine())
        'Loop
        'Dim s0 = reader.ReadLine
        'Dim s1 = reader.ReadToEnd().Replace("<!-- RANDOMCHARSHERE", FasterBrowser1.DocumentTitle.ToString)
        'Dim s2 = reader.ReadToEnd().Replace("RANDOMURLHERE1", FasterBrowser1.Url.AbsoluteUri.ToString)
        'Dim s3 = reader.ReadToEnd().Replace("RANDOMURLHERE2", FasterBrowser1.Url.AbsoluteUri.ToString)
        'Dim s4 = reader.ReadToEnd().Replace(" RANDOMENDPOINHERE", " ")
        'Dim s5 = reader.ReadToEnd().Replace(" -->", Environment.NewLine)
        'reader.Close()

        'writer.Write(s)

        'writer.Write(s1, s2, s3, s4, s5)
        'writer.Write(s2)
        'writer.Write(s3)
        'writer.Write(s4)
        'writer.Write(s5)

        'reader.Close()
        'writer.Close()
        'My.Computer.FileSystem.DeleteFile(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index.html")
        'My.Computer.FileSystem.CopyFile(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index2.html", appData + "\JTechMe\JumpGo\StandardEd\Special\History\index.html", True)
        'My.Computer.FileSystem.DeleteFile(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index2.html")

        'My.Settings.WebHistory.Add(FasterBrowser1.Url.AbsoluteUri.ToString)

        'My.Settings.WebHistory.Add(FasterBrowser1.DocumentTitle.ToString & "," & FasterBrowser1.Url.ToString)

        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquare
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If

        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircle
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If

        Me.ToolTip1.ToolTipTitle = FasterBrowser1.DocumentTitle
    End Sub

    Private Sub geticon()
        Try
            Dim url As Uri = New Uri(FasterBrowser1.Url.ToString)

            If url.HostNameType = UriHostNameType.Dns Then

                ' Get the URL of the favicon
                ' url.Host will return such string as www.google.com
                Dim iconURL = "http://" & url.Host & "/favicon.ico"

                Dim newFileName As String
                newFileName = "favicon.ico"

                ' Download the favicon
                Dim request As System.Net.WebRequest = System.Net.HttpWebRequest.Create(iconURL)
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Dim stream As System.IO.Stream = response.GetResponseStream()
                Dim favicon = Image.FromStream(stream)
                'Dim bmp As System.Drawing.Image = System.Drawing.Image.FromFile(favicon, True)
                Dim bmp As System.Drawing.Bitmap = favicon
                Dim ico As System.Drawing.Icon = System.Drawing.Icon.FromHandle(bmp.GetHicon())
                'Dim icofs As Stream = File.Create(newFileName)
                'ico.Save(icofs)
                'icofs.Close()

                ' Display the favicon on ToolStripLabel1
                'Me.favicon.Image = favicon
                Me.Icon = ico
            End If
        Catch ex As Exception
            'Me.favicon.Image = Nothing
        End Try
    End Sub

    Function FavIconSwtich()

        Return 0
    End Function

    Function FavIconChange()


        Return 0
    End Function

    Private Sub SaveHistory()
        Dim obj As [Object]
        obj = FasterBrowser1.Url
        'My.Settings.WebHistory.Items.Add(FasterBrowser1.Url.ToString)
    End Sub

    Private Sub AboutGoToolStripMenuItem_Click(sender As Object, e As EventArgs)
        AboutBox1.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If My.Settings.Search = My.Settings.WikipediaSearch Then
            FasterBrowser1.Navigate(My.Settings.Search & TextBox2.Text & My.Settings.WikipediaSearchPt2)
            Panel4.Visible = False
            MenuOpen = False
        Else
            FasterBrowser1.Navigate(My.Settings.Search & TextBox2.Text)
            Panel4.Visible = False
            MenuOpen = False
        End If
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Button3()
            Panel4.Visible = False
            MenuOpen = False
        End If
    End Sub

    Private Sub TextBox2_Click(sender As Object, e As EventArgs) Handles TextBox2.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Button6.PerformClick()
            Panel4.Visible = False
            MenuOpen = False
        End If

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub BToolStripMenuItem_Click(sender As Object, e As EventArgs)
        FasterBrowser1.Stop()
    End Sub

    Private Sub Tab_Click(sender As Object, e As EventArgs) Handles Me.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Tab_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GeckoPreferences.Default("extensions.blocklist.enabled") = False
        'GeckoPreferences.Default()
        FasterBrowser1.ContextMenuStrip = ContextMenuStrip1
        FasterBrowser1.ContextMenu = ContextMenuStrip1.ContextMenu
        If My.Settings.FirstRun = True Then
            FasterBrowser1.Navigate(Environment.CurrentDirectory + "\Welcome\index.html")
            My.Settings.FirstRun = False
            'JumpGoMain.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Else
            'FasterBrowser1.Navigate(My.Settings.NewTab)
            'FasterBrowser1.Navigate(Environment.CurrentDirectory + "\Getting Started.html")
        End If
        If My.Settings.NavPanelBG = "" Then

        Else
            Panel1.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.NavPanelBG)
        End If

        If My.Settings.MenuPanelBG = "" Then

        Else
            Panel4.BackgroundImage = System.Drawing.Image.FromFile(My.Settings.MenuPanelBG)
        End If

        If My.Settings.ThemeColor = "Dark" Then
            'Button1.BackgroundImage = My.Resources.BackCircle
            'PictureBox4.Image = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            'Button3.BackgroundImage = My.Resources.Forward_Icon1
            Button4.BackgroundImage = My.Resources.Refresh
            'Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            'Button7.BackgroundImage = My.Resources.MenuTransparent
        Else
            'PictureBox4.Image = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            'Button3.BackgroundImage = My.Resources.ForwardSquare
            Button4.BackgroundImage = My.Resources.Refresh
            'Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            'Button7.BackgroundImage = My.Resources.MenuTransparent
        End If

        Dim WinVerID As String = Environment.OSVersion.ToString()

        'Dim sUserAgent As String = "Mozilla/5.0 (Windows; U; Windows NT 6.1; pl; rv:1.9.1) Gecko/20090624 Firefox/3.5 (.NET CLR 3.5.30729)"
        'Dim sUserAgent As String = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:43.0) Gecko/20100101 Firefox/43.0"
        Dim sUserAgent As String = "Mozilla/5.0 (Windows NT " + WinVerID + "; WOW64; rv:43.0) Gecko/20100101 Firefox/43.0"
        'Dim sUserAgent As String = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:43.0) Gecko/20100101 JumpGo/43.0"
        'Dim sUserAgent As String = "Mozilla/5.0 (Mozilla/5.0 (Windows NT 6.1; WOW64; rv:46.0) Gecko/20100101 JumpGo/4.3"
        'Dim sUserAgent As String = "JTechMe/5.0 (Windows; U; Windows NT 6.1; pl; rv:1.9.1) Gecko/20090624 JumpGo/4.3 (.NET CLR 3.5.30729)"
        GeckoPreferences.User("general.useragent.override") = sUserAgent

        If My.Settings.SavedLinks IsNot Nothing Then
            For Each Sl As String In My.Settings.SavedLinks
                Dim LinkInfo As String() = Sl.Split(",")
                Dim NewLink As New ToolStripButton(LinkInfo(0))
                ToolStrip1.Items.Add(NewLink)
                NewLink.Tag = LinkInfo(1)
                AddHandler NewLink.Click, AddressOf GoToLink
                Dim sep As New ToolStripSeparator
                ToolStrip1.Items.Add(sep)
                'ToolStripDropDownButton1.DropDownItems.Add(sep)
            Next
        Else
            'My.Settings.SavedLinks = New System.Collections.Specialized.StringCollection
        End If
    End Sub

    Private Sub GoToLink(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Link As Uri = New Uri(sender.tag)
        FasterBrowser1.Navigate(Link.ToString)
    End Sub

    Private Sub AddFavoriteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddFavoriteToolStripMenuItem.Click
        If Not My.Settings.SavedLinks.Contains(FasterBrowser1.DocumentTitle & "," & FasterBrowser1.Url.ToString) Then
            Dim I As New ToolStripButton
            I.Text = FasterBrowser1.DocumentTitle
            I.Tag = FasterBrowser1.Url.ToString
            ToolStrip1.Items.Add(I)
            AddHandler I.Click, AddressOf GoToLink
            Dim s As New ToolStripSeparator
            ToolStrip1.Items.Add(s)
            My.Settings.SavedLinks.Add(FasterBrowser1.DocumentTitle & "," & FasterBrowser1.Url.ToString)
        End If
    End Sub

    Private Sub RemoveFavoriteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveFavoriteToolStripMenuItem.Click
        If My.Settings.SavedLinks IsNot Nothing Then
            For Each Savedlink As String In My.Settings.SavedLinks
                If Savedlink = FasterBrowser1.DocumentTitle & "," & FasterBrowser1.Url.ToString Then
                    My.Settings.SavedLinks.Remove(Savedlink)
                    My.Settings.Save()
                    For x As Integer = 0 To ToolStrip1.Items.Count - 2
                        If ToolStrip1.Items(x).Tag = FasterBrowser1.Url.ToString Then
                            ToolStrip1.Items.Remove(ToolStrip1.Items(x + 1))
                            ToolStrip1.Items.Remove(ToolStrip1.Items(x))
                            Exit Sub
                        End If
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        If MenuOpen = False Then
            Panel4.Visible = True
            MenuOpen = True
        Else
            Panel4.Visible = False
            MenuOpen = False
        End If
    End Sub

    Private Sub Panel1_Click(sender As Object, e As EventArgs) Handles Panel1.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Panel3_Click(sender As Object, e As EventArgs) Handles Panel3.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        JumpGoMain.CreateNewTab(My.Settings.NewTab)
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim SecondForm As New JumpGoMain
        SecondForm.Show()
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        FasterBrowser1.SaveDocument(SaveFileDialog1.ShowDialog)
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'OpenFileDialog1.ShowDialog()
        'FasterBrowser1.Navigate(OpenFileDialog1.FileName)
        FastColoredTextBox1.Text = FasterBrowser1.Document.GetElementsByTagName("html")(0).InnerHtml
        Panel5.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        AboutBox1.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Settings.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        JumpGoMain.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'geticon()
        'FasterBrowser1.Refresh()
        If FasterBrowser1.CanGoBack Then
            If PictureBox4.Image.Equals(My.Resources.BackCircleDis) Then
                'PictureBox4.Image = My.Resources.BackCircle
            End If
        End If
    End Sub

    Function themetimer()
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
            'Button1.BackgroundImage = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            'Button3.BackgroundImage = My.Resources.Forward_Icon1
            Button4.BackgroundImage = My.Resources.Refresh
            'Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            'Button7.BackgroundImage = My.Resources.MenuTransparent
        Else
            'Button1.BackgroundImage = My.Resources.BackCircle
            Button2.BackgroundImage = My.Resources.ForwardSquare
            'Button3.BackgroundImage = My.Resources.ForwardSquare
            Button4.BackgroundImage = My.Resources.Refresh
            'Button5.BackgroundImage = My.Resources.homeTransparent
            Button6.BackgroundImage = My.Resources.SearchButton
            'Button7.BackgroundImage = My.Resources.MenuTransparent
        End If
        Return 0
    End Function

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        ThemeDesign.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Dim HTMLSource As New WebSource
        HTMLSource.FastColoredTextBox1.Text = FasterBrowser1.Document.GetElementsByTagName("html")(0).InnerHtml
        HTMLSource.FileLoacation = FasterBrowser1.Url.ToString
        HTMLSource.Visible = True
        'WebSource.Visible = True
        'WebSource.FastColoredTextBox1.Text = FasterBrowser1.DocumentTitle
        'FasterBrowser1.ViewSource()
        'FastColoredTextBox1.Text = FasterBrowser1.Document.GetElementsByTagName("html")(0).InnerHtml
        'Panel5.Visible = True
        Panel4.Visible = False
        MenuOpen = False

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        SourceWriter.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        Dim NewIncognito As New IncognitoMain
        NewIncognito.Show()
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        AppBuilder.Visible = True
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        'HistoryForm.ListBox1.Items.Add(FasterBrowser1.History.Index)
        'HistoryForm.RichTextBox1.Text = FasterBrowser1.History.Index

        ' Get the path to the Application Data folder
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)

        ' Loops through all the history entries
        For Each entry As GeckoHistoryEntry In FasterBrowser1.History
            ' Creates the string that will contain our data
            Dim result As String = ""

            Try
                ' Attempts to get the title and add it to the result, which in my test cases always fails
                result = entry.Title + " | "
            Catch
            End Try

            ' Gets the URL and adds it to the result
            result += entry.Url.ToString

            ' Sends the result to the console
            Console.WriteLine(result)

            'HistoryForm.ListBox1.Items.Add(result)
        Next

        JumpGoMain.OpenHistoryTab(My.Settings.NewTab)

        'JumpGoMain.CreateNewTab(appData + "\JTechMe\JumpGo\StandardEd\Special\History\index.html")
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FasterBrowser1_DocumentCompleted(sender As Object, e As Gecko.Events.GeckoDocumentCompletedEventArgs) Handles FasterBrowser1.DocumentCompleted

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Panel5.Visible = False
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Dim HTMLSource As New WebSource
        HTMLSource.FastColoredTextBox1.Text = FasterBrowser1.Document.GetElementsByTagName("html")(0).InnerHtml
        HTMLSource.FileLoacation = FasterBrowser1.Url.ToString
        HTMLSource.Visible = True
        'WebSource.Visible = True
        'WebSource.FastColoredTextBox1.Text = FasterBrowser1.DocumentTitle
        'FasterBrowser1.ViewSource()
        'FastColoredTextBox1.Text = FasterBrowser1.Document.GetElementsByTagName("html")(0).InnerHtml
        'Panel5.Visible = True
        Panel5.Visible = False
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub FastColoredTextBox1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles FastColoredTextBox1.Click
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub ToolStrip2_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs)
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub BackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackToolStripMenuItem.Click
        'Button1.PerformClick()
    End Sub

    Private Sub ForwardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForwardToolStripMenuItem.Click
        Button2.PerformClick()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        If MenuOpen = False Then
            Panel4.Visible = True
            MenuOpen = True
        Else
            Panel4.Visible = False
            MenuOpen = False
        End If
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        FasterBrowser1.Navigate(My.Settings.Home)
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        If FasterBrowser1.CanGoBack Then
            FasterBrowser1.GoBack()
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub PictureBox4_MouseHover(sender As Object, e As EventArgs) Handles PictureBox4.MouseHover
        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircleOver
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If
    End Sub

    Private Sub PictureBox4_MouseDown(sender As Object, e As EventArgs) Handles PictureBox4.MouseDown
        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircleDown
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If
    End Sub

    Private Sub PictureBox4_MouseUp(sender As Object, e As EventArgs) Handles PictureBox4.MouseUp
        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircle
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If
    End Sub

    Private Sub PictureBox4_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox4.MouseLeave
        If FasterBrowser1.CanGoBack Then
            PictureBox4.Image = My.Resources.BackCircle
        Else
            PictureBox4.Image = My.Resources.BackCircleDis
        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
        If FasterBrowser1.CanGoForward Then
            FasterBrowser1.GoForward()
        End If
        Panel4.Visible = False
        MenuOpen = False
    End Sub

    Private Sub PictureBox5_MouseHover(sender As Object, e As EventArgs) Handles PictureBox5.MouseHover
        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquareOver
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If
    End Sub

    Private Sub PictureBox5_MouseDown(sender As Object, e As EventArgs) Handles PictureBox5.MouseDown
        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquareDown
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If
    End Sub

    Private Sub PictureBox5_MouseUp(sender As Object, e As EventArgs) Handles PictureBox5.MouseUp
        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquare
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If
    End Sub

    Private Sub PictureBox5_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox5.MouseLeave
        If FasterBrowser1.CanGoForward Then
            PictureBox5.Image = My.Resources.ForwardSquare
        Else
            PictureBox5.Image = My.Resources.ForwardSquareDis
        End If
    End Sub

    Private Sub CopyImageLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyImageLocationToolStripMenuItem.Click
        FasterBrowser1.CopyLinkLocation()
    End Sub

    Private Sub OpenInNewTabToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenInNewTabToolStripMenuItem.Click
        'FasterBrowser1.Document.ActiveElement.
        'JumpGoMain.CreateNewTab("")
    End Sub

    Private Sub FasterBrowser1_Navigating(sender As Object, e As Events.GeckoNavigatingEventArgs) Handles FasterBrowser1.Navigating

    End Sub

    Private Sub FasterBrowser1_DomFocus(sender As Object, e As DomEventArgs) Handles FasterBrowser1.DomFocus

    End Sub
End Class
