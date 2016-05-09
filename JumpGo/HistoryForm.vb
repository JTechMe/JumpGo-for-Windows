Public Class HistoryForm

    Private Sub HistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ListBox1.Items.Add(My.Settings.WebHistory)
        'Dim obj As [Object]
        'For Each item In My.Settings.WebHistory
        'Console.WriteLine("   {0}", obj)
        'ListBox1.Items.Add(obj)
        'Next obj
        'Console.WriteLine()
        'ListBox1.Items.Equals(My.Settings.WebHistory)
        If My.Settings.SavedLinks IsNot Nothing Then
            For Each Sl As String In My.Settings.SavedLinks
                Dim LinkInfo As String() = Sl.Split(",")
                Dim NewLink As New ToolStripButton(LinkInfo(0))
                'ToolStrip1.Items.Add(NewLink)
                NewLink.Tag = LinkInfo(1)
                'AddHandler NewLink.Click, AddressOf GoToLink
                Dim sep As New ToolStripSeparator
                'ToolStrip1.Items.Add(sep)
            Next
        Else
            'My.Settings.SavedLinks = New System.Collections.Specialized.StringCollection
        End If
    End Sub
End Class