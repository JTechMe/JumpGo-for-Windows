Imports System.ComponentModel

Public Class GridMenuButton
    Inherits System.Windows.Forms.UserControl

    Private bttnTxt As String = "Button Text"
    Private bttnImg As Image = My.Resources.ic_action_home

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

#Region "Properties"
    <Category("Appearance"), Description("The text displayed at the bottom of the button control")>
    Public Property ButtonText() As String
        Get
            Return bttnTxt
        End Get
        Set(ByVal Value As String)
            bttnTxt = Value
            Label1.Text = Value
        End Set
    End Property

    <Category("Appearance"), Description("The image used in the button control")>
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
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub PictureBox1_MouseHover(sender As Object, e As EventArgs) Handles PictureBox1.MouseHover
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub Label1_MouseHover(sender As Object, e As EventArgs) Handles Label1.MouseHover
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub GridMenuButton_MouseDown(sender As Object, e As EventArgs) Handles MyBase.MouseDown
        Me.BackgroundImage = My.Resources.GridMenuButtonBGDown
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub PictureBox1_MouseDown(sender As Object, e As EventArgs) Handles PictureBox1.MouseDown
        Me.BackgroundImage = My.Resources.GridMenuButtonBGDown
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As EventArgs) Handles Label1.MouseDown
        Me.BackgroundImage = My.Resources.GridMenuButtonBGDown
        'Me.BackColor = Color.Gray
    End Sub

    Private Sub GridMenuButton_MouseUp(sender As Object, e As EventArgs) Handles MyBase.MouseUp
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As EventArgs) Handles PictureBox1.MouseUp
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub Label1_MouseUp(sender As Object, e As EventArgs) Handles Label1.MouseUp
        Me.BackgroundImage = My.Resources.GridMenuButtonBG
    End Sub

    Private Sub GridMenuButton_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        Me.BackgroundImage = Nothing
        Me.BackColor = Color.White
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        Me.BackgroundImage = Nothing
        Me.BackColor = Color.White
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        Me.BackgroundImage = Nothing
        Me.BackColor = Color.White
    End Sub
#End Region

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.OnClick(EventArgs.Empty)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Me.OnClick(EventArgs.Empty)
    End Sub

    Private Sub GridMenuButton_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
