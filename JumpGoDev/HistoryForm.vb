Public Class HistoryForm

    Private Sub HistoryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ListBox1.Items.Add(My.Settings.WebHistory.Item)
        'Dim obj As [Object]
        'For Each item In My.Settings.WebHistory
        'Console.WriteLine("   {0}", obj)
        'ListBox1.Items.Add(obj)
        'Next obj
        'Console.WriteLine()
        ListBox1.Equals(My.Settings.WebHistory)
    End Sub
End Class