' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Public Sub New()
        Me.InitializeComponent()
        WebView1.Navigate(URLvar)
    End Sub

    Private Sub HamburgerButton_Click(sender As [Object], e As RoutedEventArgs) 'Handles HamburgerButton.Click
        MySplitView.IsPaneOpen = Not MySplitView.IsPaneOpen
        'WebView1.Navigate(New Uri("http://jtechme.github.io/jg/jumpgo"))
    End Sub

    Private URLvar As New Uri("http://google.com/")

    Private Sub MenuButton1_Click(sender As [Object], e As RoutedEventArgs) Handles MenuButton1.Click
        WebView1.Navigate(URLvar)
    End Sub
End Class
