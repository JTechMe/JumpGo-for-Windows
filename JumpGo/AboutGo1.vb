Imports System.IO

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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        JumpGoMain.CreateNewTab("https://www.mozilla.org/en-US/about/powered-by/")
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub AboutGo1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        'Me.LabelCompanyName.Text = "JumpGo and the JumpGo logos are trademarks of " + My.Application.Info.CompanyName
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCurVer.Text = "JumpGo is up to date"
        Me.ShowIcon = False
    End Sub

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
End Class