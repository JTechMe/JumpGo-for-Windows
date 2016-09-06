Imports System.ComponentModel

Public Class DownloadMenuItem
    Inherits System.Windows.Forms.UserControl

    Private bttnFNTxt As String = "filename.file"
    Private bttnFSTxt As String = "00.0 MB -- website.com -- 12:00 PM"
    Private bttnFLoca As String = Environment.SpecialFolder.Desktop.ToString
    Private bttnImg As Image = My.Resources.ic_file_file
    Private dlProgress As Integer = 0
    Private filetype As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Properties"
    <Category("Misc"), Description("The text used to display the name of the downloaded file")>
    Public Property FileNameText() As String
        Get
            Return bttnFNTxt
        End Get
        Set(ByVal Value As String)
            bttnFNTxt = Value
            Label1.Text = Value
        End Set
    End Property

    <Category("Misc"), Description("The text used to display the relevent data about the download")>
    Public Property FileMetadataText() As String
        Get
            Return bttnFSTxt
        End Get
        Set(ByVal Value As String)
            bttnFSTxt = Value
            Label2.Text = Value
        End Set
    End Property

    <Category("Misc"), Description("The folder location that the file was downloaded to")>
    Public Property FileFolderLocation() As String
        Get
            Return bttnFLoca
        End Get
        Set(ByVal Value As String)
            bttnFLoca = Value
            'Label2.Text = Value
        End Set
    End Property

    <Category("Misc"), Description("The folder location that the file was downloaded to")>
    Public Property DownloadProgress() As Integer
        Get
            Return dlProgress
        End Get
        Set(ByVal Value As Integer)
            dlProgress = Value
            'Label2.Text = Value
        End Set
    End Property

    <Category("Appearance"), Description("The image used to display what kind of file was downloaded")>
    Public Property Image() As Image
        Get
            Return bttnImg
        End Get
        Set(ByVal Value As Image)
            bttnImg = Value
            PictureBox1.Image = Value
        End Set
    End Property
#End Region

#Region "Display Subs"
    Private Sub GridMenuButton_MouseHover(sender As Object, e As EventArgs) Handles MyBase.MouseHover
        Me.BackgroundImage = My.Resources.DLBGH
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        Me.BackgroundImage = My.Resources.DLBGH
    End Sub

    Private Sub Label1_MouseHover(sender As Object, e As EventArgs) Handles Label1.MouseHover
        Me.BackgroundImage = My.Resources.DLBGH
    End Sub

    Private Sub Label2_MouseHover(sender As Object, e As EventArgs) Handles Label2.MouseHover
        Me.BackgroundImage = My.Resources.DLBGH
    End Sub

    Private Sub GridMenuButton_MouseDown(sender As Object, e As EventArgs) Handles MyBase.MouseDown
        Me.BackgroundImage = My.Resources.DLBGD
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        Me.BackgroundImage = My.Resources.DLBGD
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As EventArgs) Handles Label1.MouseDown
        Me.BackgroundImage = My.Resources.DLBGD
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub Label2_MouseDown(sender As Object, e As EventArgs) Handles Label2.MouseDown
        Me.BackgroundImage = My.Resources.DLBGD
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub GridMenuButton_MouseUp(sender As Object, e As EventArgs) Handles MyBase.MouseUp
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub Label1_MouseUp(sender As Object, e As EventArgs) Handles Label1.MouseUp
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub Label2_MouseUp(sender As Object, e As EventArgs) Handles Label2.MouseUp
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub GridMenuButton_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        Me.BackgroundImage = My.Resources.DLBG
    End Sub

    Private Sub Label2_MouseLeave(sender As Object, e As EventArgs) Handles Label2.MouseLeave
        Me.BackgroundImage = My.Resources.DLBG
    End Sub
#End Region

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.OnClick(EventArgs.Empty)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.OnClick(EventArgs.Empty)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Me.OnClick(EventArgs.Empty)
    End Sub

    Private Sub DownloadMenuItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
