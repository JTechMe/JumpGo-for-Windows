Imports System.IO

Public Class AppBuilder

    Private mIcons As New Hashtable
    Private mRootPath As String = Environment.CurrentDirectory + "\AppProjects"
    Public projectAPNM As String = Environment.CurrentDirectory + "\AppProjects\AppProject1"
    Public ProjectDIR As String
    Public ProjName As String
    Public APNMName As String
    Public ProjectVersion
    Public INTERNET_ACCESS As String
    Public IconLoca As String
    Public IconFile As Icon
    Public indexFile As String
    Public cz182 As String

    Property RootPath() As String
        Get
            Return mRootPath
        End Get
        Set(ByVal value As String)
            mRootPath = value
        End Set
    End Property

    Public Sub TheLoadingBit()
        ' when our component is loaded, we initialize the TreeView by adding the root node
        Dim mRootNode As New TreeNode
        'mRootNode.Text = RootPath
        mRootNode.Text = "AppProjects"
        mRootNode.Tag = RootPath
        mRootNode.ImageKey = CacheShellIcon(RootPath)
        mRootNode.SelectedImageKey = mRootNode.ImageKey & "-open"
        mRootNode.Nodes.Add("*DUMMY*")
        TreeView1.Nodes.Add(mRootNode)
    End Sub

    Private Sub UserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' This will call TheLoadingBit of the node generating code
        TheLoadingBit()
        OpenToolStripMenuItem.PerformClick()
    End Sub

    Private Sub TreeView1_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeCollapse
        ' clear the node that is being collapsed
        e.Node.Nodes.Clear()
        ' add a dummy TreeNode to the node being collapsed so it is expandable
        e.Node.Nodes.Add("*DUMMY*")
    End Sub

    Private Sub TreeView1_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeExpand
        ' clear the argNode so we can re-populate, or else we end up with duplicate nodes
        e.Node.Nodes.Clear()
        ' get the directory representing this node
        Dim mNodeDirectory As IO.DirectoryInfo
        mNodeDirectory = New IO.DirectoryInfo(e.Node.Tag.ToString)
        ' add each directory from the file system that is a child of the argNode that was passed in
        For Each mDirectory As IO.DirectoryInfo In mNodeDirectory.GetDirectories
            ' declare a TreeNode for this directory
            Dim mDirectoryNode As New TreeNode
            ' store the full path to this directory in the directory TreeNode's Tag property
            mDirectoryNode.Tag = mDirectory.FullName
            ' set the directory TreeNodes's display text
            mDirectoryNode.Text = mDirectory.Name
            ' add a dummy TreeNode to the directory node so that it is expandable
            mDirectoryNode.Nodes.Add("*DUMMY*")
            ' get the icon/open icon for this directory
            mDirectoryNode.ImageKey = CacheShellIcon(mDirectory.FullName)
            mDirectoryNode.SelectedImageKey = mDirectoryNode.ImageKey & "-open"
            ' add this directory treenode to the treenode that is being populated
            e.Node.Nodes.Add(mDirectoryNode)
        Next

        ' add each file from the file system that is a child of the argNode that was passed in
        For Each mFile As IO.FileInfo In mNodeDirectory.GetFiles
            ' declare a TreeNode for this directory
            Dim mFileNode As New TreeNode
            ' store the full path to this directory in the directory TreeNode's Tag property
            mFileNode.Tag = mFile.FullName
            ' set the directory TreeNodes's display text
            mFileNode.Text = mFile.Name
            mFileNode.ToolTipText = mFile.FullName
            ' get the icon/open icon for this file
            mFileNode.ImageKey = CacheShellIcon(mFile.FullName)
            mFileNode.SelectedImageKey = mFileNode.ImageKey
            'mFileNode.SelectedImageKey = mFileNode.ImageKey & "-open"
            ' add this directory treenode to the treenode that is being populated
            e.Node.Nodes.Add(mFileNode)
        Next

    End Sub

    Function CacheShellIcon(ByVal argPath As String) As String
        Dim mKey As String = Nothing
        ' determine the icon key for the file/folder specified in argPath
        If IO.Directory.Exists(argPath) = True Then
            mKey = "folder"
        ElseIf IO.File.Exists(argPath) = True Then
            mKey = IO.Path.GetExtension(argPath)
        End If
        ' check if an icon for this key has already been added to the collection
        If ImageList1.Images.ContainsKey(mKey) = False Then
            ImageList1.Images.Add(mKey, GetShellIconAsImage(argPath))
            If mKey = "folder" Then ImageList1.Images.Add(mKey & "-open", GetShellOpenIconAsImage(argPath))
        End If
        Return mKey
    End Function

    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        ' only proceed if the node represents a file
        If e.Node.ImageKey = "folder" Then Exit Sub
        If e.Node.Tag = "" Then Exit Sub
        ' try to open the file
        Try
            Process.Start(e.Node.Tag)
        Catch ex As Exception
            MessageBox.Show("Error opening file: " & ex.Message)
        End Try
    End Sub

    Private Sub ViewHTMLDocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewHTMLDocToolStripMenuItem.Click
        If TreeView1.SelectedNode.ToolTipText.Contains(".html") Or TreeView1.SelectedNode.ToolTipText.Contains(".xml") Or TreeView1.SelectedNode.ToolTipText.Contains(".css") Or TreeView1.SelectedNode.ToolTipText.Contains(".js") Then
            OpenFileDialog1.FileName = TreeView1.SelectedNode.ToolTipText
            OpenFileDialog1.OpenFile()
            FastColoredTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        End If
    End Sub

    Private Sub AndroidcomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AndroidcomToolStripMenuItem.Click
        'This copies the android-dot-com material design folder into the AppProjects folder
        My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "\mdl-templates\templates\android-dot-com", Environment.CurrentDirectory + "\AppProjects", True)
    End Sub

    Private Sub ArticleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArticleToolStripMenuItem.Click
        'This copies the article material design folder into the AppProjects folder
        My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "\mdl-templates\templates\article", Environment.CurrentDirectory + "\AppProjects", True)
    End Sub

    Private Sub BlogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlogToolStripMenuItem.Click
        'This copies the blog material design folder into the AppProjects folder
        My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "\mdl-templates\templates\blog", Environment.CurrentDirectory + "\AppProjects", True)
    End Sub

    Private Sub DashboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DashboardToolStripMenuItem.Click
        'This copies the dashboard material design folder into the AppProjects folder
        My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "\mdl-templates\templates\dashboard", Environment.CurrentDirectory + "\AppProjects", True)
    End Sub

    Private Sub TextOnlyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TextOnlyToolStripMenuItem.Click
        'This copies the text-only material design folder into the AppProjects folder
        My.Computer.FileSystem.CopyDirectory(Environment.CurrentDirectory + "\mdl-templates\templates\text-only", Environment.CurrentDirectory + "\AppProjects", True)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        'FolderBrowserDialog1.RootFolder = Environment.CurrentDirectory + "\AppProjects"
        FolderBrowserDialog1.ShowDialog()
        ProjectDIR = FolderBrowserDialog1.SelectedPath
        cz182 = FolderBrowserDialog1.SelectedPath
        OpenFileDialog1.FileName = FolderBrowserDialog1.SelectedPath + "/project.apnm"
        OpenFileDialog1.OpenFile()
        FastColoredTextBox1.Text = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        ToolStripButton1.Enabled = True
        'All this does is set up the OpenFileDialog for the .apnm file type
        'OpenFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\AppProjects"
        'OpenFileDialog1.DefaultExt = "apnm"
        'OpenFileDialog1.FileName = ""
        'OpenFileDialog1.Filter = "Project Files (*.apnm)|*.apnm|All Files (*.*)|*.*"
        'OpenFileDialog1.Multiselect = False
        'OpenFileDialog1.ShowDialog()
        'This changes the mRootPath string to the root path of the selected project.apnm file (will probably be replaced)
        'mRootPath = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        projectAPNM = OpenFileDialog1.FileName
        'Dim APNMText As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)
        Dim reader As New System.IO.StreamReader(projectAPNM)
        Dim allLines As List(Of String) = New List(Of String)
        Do While Not reader.EndOfStream
            allLines.Add(reader.ReadLine())
        Loop
        reader.Close()
        Me.Text = ReadLine(5, allLines) + " - AppBuilder"
        ProjName = ReadLine(5, allLines)
        APNMName = ReadLine(6, allLines)
        ProjectVersion = ReadLine(7, allLines)
        INTERNET_ACCESS = ReadLine(12, allLines)
        IconLoca = ReadLine(26, allLines)
        indexFile = ReadLine(30, allLines)
    End Sub

    Public Function ReadLine(lineNumber As Integer, lines As List(Of String)) As String
        Return lines(lineNumber - 1)
    End Function

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        'AppBuildTester.FasterBrowser1.Navigate(ProjectDIR + "/" + indexFile)
        AppBuildTester.FasterBrowser1.Navigate(cz182 + "/index.html")
        AppBuildTester.Text = ProjName + " - AppBuildTester"
        AppBuildTester.Visible = True
    End Sub
End Class