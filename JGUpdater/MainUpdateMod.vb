Imports System.IO
Imports System.Xml

Module MainUpdateMod
    Dim currLine As Integer
    Dim allLines As List(Of String) = New List(Of String)
    Dim allLines2 As List(Of String) = New List(Of String)
    Dim textDoc As String
    Dim textDoc2 As String
    'Dim mosupdid As String
    'Dim curupdid As String
    Dim mosupdid As Decimal
    Dim curupdid As Decimal
    Dim installlocS As String = "C:\Program Files (x86)\JTechMe\JumpGo Browser"
    Dim installlocD As String = "C:\Program Files (x86)\JTechMe\JumpGo Dev"
    Friend WithEvents Timer1 As New Timer()

    Public Function IsOnNetwork() As Boolean
        'This Tests your network connection
        Try
            Return My.Computer.Network.IsAvailable
        Catch ex As Exception
            Return False
        End Try
    End Function

    Sub Main()
        'This will execute when your application
        'starts up. This is the equivilent of a
        'Form_Load event in a form application.
        'Put whatever code you want in this sub,
        'but make sure you end it with this statement:
        xmlDLandR()
        Application.Run()
    End Sub

    Sub xmlDLandR()
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
        Dim dldcurver As String = doc.SelectSingleNode("application/jgstandard/stable/curverid").InnerText
        mosupdid = doc.SelectSingleNode("application/jgstandard/stable/curverid").InnerText
        'Dim srthsgf As Decimal = doc.SelectSingleNode("application/jgstandard/stable/curverid").InnerText

        Dim doc2 As XmlDocument = New XmlDocument()
        doc2.Load(result + "\curverid.loc.xml")
        Dim lclcurver As String = doc2.SelectSingleNode("application/jgstandard/stable/curverid").InnerText
        curupdid = doc.SelectSingleNode("application/jgstandard/stable/curverid").InnerText

        If curupdid >= mosupdid Then

        Else
            doc.Load(result + "\curverid.xml")
            Dim dlurl As String = doc.SelectSingleNode("application/stable/dlurl").InnerText

            '"https://github.com/JTechMe/JumpGo-for-Windows/releases/download/v4.3.6/update.zip"
            If File.Exists(result + "\JGSetup.msi") = True Then
                File.Delete(result + "\JGSetup.msi")
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\JGSetup.msi")
            Else
                My.Computer.Network.DownloadFile(
                dlurl,
                    result + "\JGSetup.msi")
                'ZIPUpdater.Visible = True
                'Process.Start(Environment.CurrentDirectory + "JGUpdaterElevated.exe")

                Dim p As New Process()
                p.StartInfo.FileName = "msiexec"
                p.StartInfo.Arguments = "/i " + result + "\JGSetup.msi"
                p.Start()
            End If
        End If
    End Sub

    Sub msiStandardDL()
        Dim result As String = Path.GetTempPath()
        If File.Exists(result + "\curverid.xml") = True Then
            File.Delete(result + "\curverid.xml")
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.xml")
        Else
            My.Computer.Network.DownloadFile(
    "http://jtechme.github.io/jgwin/standard/curverid.xml",
        result + "\curverid.xml")
        End If

        Dim doc As XmlDocument = New XmlDocument()
        doc.Load(result + "\curverid.xml")
        Dim dldcurver As String = doc.SelectSingleNode("application/stable/curverid").InnerText
        mosupdid = doc.SelectSingleNode("application/stable/curverid").InnerText

        Dim doc2 As XmlDocument = New XmlDocument()
        doc2.Load(result + "\curverid.loc.xml")
        Dim lclcurver As String = doc2.SelectSingleNode("application/stable/curverid").InnerText
        curupdid = doc.SelectSingleNode("application/stable/curverid").InnerText
    End Sub
End Module
