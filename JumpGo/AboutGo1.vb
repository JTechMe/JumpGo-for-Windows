Imports System.IO
Imports System.Xml

'                   ___           ___                         ___           ___     
'       ___        /  /\         /  /\ ___ /  /\         /  /\
'      / __ /\      /  /: /        /  /:|        /  /\        /  /:\       /  /:\   
'      \__\:\    /  /:/        /  /:|:|       /  /:\      /  /:/\:\     /  /:/\:\  
'  ___ /  /: \  /  /:/        /  /:/|:|__    /  /:/\:\    /  /:/  \:\   /  /:/  \:\ 
' /__/\  /:/\/ /__/:/     /\ /__/:/_|:\  /  /:\ \:\  /__/:/_\_ \:\ /__/:/ \__\:\
' \  \:\/:/~~  \  \:\    /:/ \__\/  /~~/:/ /__/:/\:\_\:\ \  \:\__/\_\/ \  \:\ /  /:/
'  \  \:/      \  \:\  /:/        /  /:/  \__\/  \:\/:/  \  \:\ \:\    \  \:\  /:/ 
'   \__\/        \  \:\/:/        /  /:/        \  \:/    \  \:\/:/     \  \:\/:/  
'                 \  \:/        /__/:/          \__\/      \  \:/       \  \:/   
'                  \__\/         \__\/                       \__\/         \__\/    

Public Class AboutGo1

    Dim dldurl As String
    Dim dldxml As String = "http://jtechme.github.io/jgwin/standard/curverid.xml"
    Dim result As String = Path.GetTempPath()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        JumpGoMain.CreateNewTab("https://www.mozilla.org/en-US/about/powered-by/")
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Public Function IsOnNetwork() As Boolean
        'This Tests your network connection
        Try
            Return My.Computer.Network.IsAvailable
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub AboutGo1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.LabelCopyright.Text = My.Application.Info.Copyright
#Region "Update Checking"
        If IsOnNetwork() = True Then
            Me.LabelCurVer.Text = "Checking for update"
            If File.Exists(result + "\curverid.xml") = True Then
                File.Delete(result + "\curverid.xml")
                My.Computer.Network.DownloadFile(
            dldxml,
                result + "\curverid.xml")
            Else
                My.Computer.Network.DownloadFile(
            dldxml,
                result + "\curverid.xml")
            End If
            'Const URLString As String = "http://localhost/books.xml"
            'Dim reader As XmlTextReader = New XmlTextReader(URLString)
            Dim doc As XmlDocument = New XmlDocument()
            doc.Load(result + "/curverid.xml")
            Dim dldcurver As String = doc.SelectSingleNode("application/jgstandard/stable/curverid").InnerText

            If dldcurver > My.Application.Info.Version.ToString Then
                dldurl = doc.SelectSingleNode("application/jgstandard/stable/curlink").InnerText
                Me.LabelCurVer.Text = "JumpGo has an update available"
                LinkDoUpdate.Visible = True
            ElseIf dldcurver = My.Application.Info.Version.ToString Then
                Me.LabelCurVer.Text = "JumpGo is up to date"
            ElseIf dldcurver < My.Application.Info.Version.ToString Then
                Me.LabelCurVer.Text = "JumpGo is up to date"
            End If
        Else
            LinkDoUpdate.Visible = False
            Me.LabelCurVer.Text = "JumpGo is up to date"
        End If
#End Region
        Me.ShowIcon = False
    End Sub

#Region "Label Links"
    Private Sub LabelWhatsNew_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelWhatsNew.LinkClicked
        JumpGoMain.CreateNewTab("https://github.com/JTechMe/JumpGo-for-Windows/releases")
    End Sub

    Private Sub LabelCheckUsOut_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelCheckUsOut.LinkClicked
        JumpGoMain.CreateNewTab("https://github.com/JTechMe/JumpGo-for-Windows")
    End Sub

    Private Sub LabelLicensing_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelLicensing.LinkClicked
        JumpGoMain.CreateNewTab(Environment.CurrentDirectory + "\Welcome\license.html")
    End Sub

    Private Sub LabelEUR_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelEUR.LinkClicked
        JumpGoMain.CreateNewTab(Environment.CurrentDirectory + "\Welcome\rights.html")
    End Sub

    Private Sub LabelPrivacy_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelPrivacy.LinkClicked
        JumpGoMain.CreateNewTab("http://jtechme.github.io/jumpgo/privacy")
    End Sub

    Private Sub LinkDoUpdate_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkDoUpdate.LinkClicked
        If File.Exists(result + "\JGSetup.msi") = True Then
            File.Delete(result + "\JGSetup.msi")
            My.Computer.Network.DownloadFile(
            dldurl,
                result + "\JGSetup.msi")
        Else
            My.Computer.Network.DownloadFile(
            dldurl,
                result + "\JGSetup.msi")
            'ZIPUpdater.Visible = True
            'Process.Start(Environment.CurrentDirectory + "JGUpdaterElevated.exe")

            Dim p As New Process()
            p.StartInfo.FileName = "msiexec"
            p.StartInfo.Arguments = "/i " + result + "\JGSetup.msi"
            p.Start()
        End If
    End Sub
#End Region
End Class