'Imports JumpGoStandardEdition
Imports System.IO
Imports System.Xml

Public Class MainUForm
    Dim currLine As Integer
    Dim allLines As List(Of String) = New List(Of String)
    Dim allLines2 As List(Of String) = New List(Of String)
    Dim textDoc As String
    Dim textDoc2 As String
    Dim mosupdid As String
    Dim curupdid As String
    Dim installloc As String = "C:\Program Files (x86)\JTechMe\JumpGo Browser"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As String = Path.GetTempPath()
        If File.Exists(result + "\curverid.xml") = True Then
            File.Delete(result + "\curverid.xml")
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.txt")
        Else
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.txt")
        End If

        Dim doc As XmlDocument = New XmlDocument()
        doc.Load(result + "\curverid.xml")
        Dim dldcurver As String = doc.SelectSingleNode("application/stable/curverid").InnerText
        mosupdid = doc.SelectSingleNode("application/stable/curverid").InnerText

        Dim doc2 As XmlDocument = New XmlDocument()
        doc2.Load(result + "\curverid.loc.xml")
        Dim lclcurver As String = doc2.SelectSingleNode("application/stable/curverid").InnerText
        curupdid = doc.SelectSingleNode("application/stable/curverid").InnerText

        If curupdid = mosupdid Then

        Else
            doc.Load(result + "\curverid.xml")
            Dim dlurl As String = doc.SelectSingleNode("application/stable/dlurl").InnerText

            '"https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip"
            If File.Exists(result + "\update.zip") = True Then
                File.Delete(result + "\update.zip")
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\update.zip")
            Else
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\update.zip")
                'ZIPUpdater.Visible = True
                Process.Start(Environment.CurrentDirectory + "JGUpdaterElevated.exe")
            End If
        End If
    End Sub

    Public Sub freshID()
        Dim isTag As String
        isTag = textDoc
        Dim i As Integer = isTag.IndexOf("<curverid>{")
        Dim tagResult As String = isTag.Substring(i + 1, isTag.IndexOf("}", i + 1) - i - 1)
    End Sub

    Private Sub MainUForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As String = Path.GetTempPath()
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Grabbing Temp Path..."
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ TempPath result = " + result
        ProgressBar1.Value = 15
        If File.Exists(result + "\curverid.xml") = True Then
            File.Delete(result + "\curverid.xml")
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Removing old curverid.xml file"
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.xml")
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ dl http:jtechme.github.io/jgwin/standard/curverid.xml " + result
            ProgressBar1.Value = 25
        Else
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.xml")
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ dl http:jtechme.github.io/jgwin/standard/curverid.xml " + result
            ProgressBar1.Value = 25
        End If

        Dim doc As XmlDocument = New XmlDocument()
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ doc As New XmlDocument"
        ProgressBar1.Value = 30
        doc.Load(result + "\curverid.xml")
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ doc.Load(" + result + "\curverid.xml"
        ProgressBar1.Value = 35
        Dim dldcurver As String = doc.SelectSingleNode("application/stable/curverid").InnerText
        mosupdid = doc.SelectSingleNode("application/stable/curverid").InnerText
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ xmlTag,data%<application><stable><curverid>%"
        ProgressBar1.Value = 40

        Dim doc2 As XmlDocument = New XmlDocument()
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ doc2 As New XmlDocument"
        ProgressBar1.Value = 45
        doc2.Load(result + "\curverid.loc.xml")
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ doc.Load(" + result + "\curverid.loc.xml"
        ProgressBar1.Value = 47
        Dim lclcurver As String = doc2.SelectSingleNode("application/stable/curverid").InnerText
        curupdid = doc.SelectSingleNode("application/stable/curverid").InnerText
        RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ xmlTag,data%<application><stable><curverid>%"
        ProgressBar1.Value = 50

        If curupdid = mosupdid Then
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ JumpGo and it's components are up to date!"
            ProgressBar1.Value = 100
        Else
            doc.Load(result + "\curverid.xml")
            Dim dlurl As String = doc.SelectSingleNode("application/stable/dlurl").InnerText
            RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ xmlTag,data%<application><stable><dlurl>%"
            ProgressBar1.Value = 55

            '"https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip"
            If File.Exists(result + "\update.zip") = True Then
                File.Delete(result + "\update.zip")
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Removing old update packages..."
                ProgressBar1.Value = 75
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\update.zip")
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ dl dlurl " + result
                ProgressBar1.Value = 90
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ JumpGo Update Package downloaded!"
                ProgressBar1.Value = 100
                Process.Start(Environment.CurrentDirectory + "JGUpdaterElevated.exe")
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Launching Update Package Installer"
                ProgressBar1.Value = 0
            Else
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\update.zip")
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ dl dlurl"
                ProgressBar1.Value = 90
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ JumpGo Update Package downloaded!"
                ProgressBar1.Value = 100
                'ZIPUpdater.Visible = True
                Process.Start(Environment.CurrentDirectory + "JGUpdaterElevated.exe")
                RichTextBox1.Text = RichTextBox1.Text & vbNewLine & "$ Launching Update Package Installer"
                ProgressBar1.Value = 0
            End If
        End If
    End Sub
End Class