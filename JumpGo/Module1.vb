Module Module1

    Public Declare Auto Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef psfi As SHFILEINFO, ByVal cbFileInfo As Integer, ByVal uFlags As Integer) As IntPtr

    Public Const SHGFI_ICON As Integer = &H100
    Public Const SHGFI_SMALLICON As Integer = &H1
    Public Const SHGFI_LARGEICON As Integer = &H0
    Public Const SHGFI_OPENICON As Integer = &H2
    Public nIndex As Integer = 0

    Structure SHFILEINFO
        Public hIcon As IntPtr            ' will contain the icon handle
        Public iIcon As Integer           ' will contain the icon index
        Public dwAttributes As Integer    ' will contain attributes
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    ' GetShellIconAsImage
    ' Gets a real icon from the Windows shell associated with the specified path.
    Function GetShellIconAsImage(ByVal argPath As String) As Image
        Dim mShellFileInfo As New SHFILEINFO
        ' get the shell file info for lpPath
        mShellFileInfo = New SHFILEINFO
        mShellFileInfo.szDisplayName = New String(Chr(0), 260)
        mShellFileInfo.szTypeName = New String(Chr(0), 80)
        SHGetFileInfo(argPath, 0, mShellFileInfo, System.Runtime.InteropServices.Marshal.SizeOf(mShellFileInfo), SHGFI_ICON Or SHGFI_SMALLICON)
        ' create the icon from the icon handle
        Dim mIcon As System.Drawing.Icon
        Dim mImage As System.Drawing.Image
        Try
            mIcon = System.Drawing.Icon.FromHandle(mShellFileInfo.hIcon)
            mImage = mIcon.ToBitmap
        Catch ex As Exception
            ' create a blank black bitmap to return
            mImage = New System.Drawing.Bitmap(16, 16)
        End Try
        ' return the composited image
        Return mImage
    End Function

    ' GetShellOpenIconAsImage
    ' Gets the open icon from the Windows shell associated with the specified path.
    Function GetShellOpenIconAsImage(ByVal argPath As String) As System.Drawing.Image
        Dim mShellFileInfo As New SHFILEINFO
        ' get the shell file info for lpPath
        mShellFileInfo = New SHFILEINFO
        mShellFileInfo.szDisplayName = New String(Chr(0), 260)
        mShellFileInfo.szTypeName = New String(Chr(0), 80)
        SHGetFileInfo(IO.Path.GetTempPath, 0, mShellFileInfo, System.Runtime.InteropServices.Marshal.SizeOf(mShellFileInfo), SHGFI_ICON Or SHGFI_SMALLICON Or SHGFI_OPENICON)
        ' create the icon from the icon handle
        Dim mIcon As System.Drawing.Icon
        Dim mImage As System.Drawing.Image
        Try
            mIcon = System.Drawing.Icon.FromHandle(mShellFileInfo.hIcon)
            mImage = mIcon.ToBitmap
        Catch ex As Exception
            ' create a blank black bitmap to return
            mImage = New System.Drawing.Bitmap(16, 16)
        End Try
        ' return the composited image
        Return mImage
    End Function

End Module
