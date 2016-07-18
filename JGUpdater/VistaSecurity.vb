Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Security.Principal

Namespace hackman3vilGuy.CodeProject.VistaSecurity.ElevateWithButton
    Public NotInheritable Class VistaSecurity
        Private Sub New()
        End Sub
        <DllImport("user32")>
        Public Shared Function SendMessage(hWnd As IntPtr, msg As UInt32, wParam As UInt32, lParam As UInt32) As UInt32
        End Function

        Friend Const BCM_FIRST As Integer = &H1600
        Friend Const BCM_SETSHIELD As Integer = (BCM_FIRST + &HC)

        Friend Shared Function IsVistaOrHigher() As Boolean
            Return Environment.OSVersion.Version.Major < 6
        End Function

        ''' <summary>
        ''' Checks if the process is elevated
        ''' </summary>
        ''' <returns>If is elevated</returns>
        Friend Shared Function IsAdmin() As Boolean
            Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim p As New WindowsPrincipal(id)
            Return p.IsInRole(WindowsBuiltInRole.Administrator)
        End Function

        ''' <summary>
        ''' Add a shield icon to a button
        ''' </summary>
        ''' <param name="b">The button</param>
        Friend Shared Sub AddShieldToButton(b As Button)
            b.FlatStyle = FlatStyle.System
            SendMessage(b.Handle, BCM_SETSHIELD, 0, &HFFFFFFFFUI)
        End Sub

        ''' <summary>
        ''' Restart the current process with administrator credentials
        ''' </summary>
        Friend Shared Sub RestartElevated()
            Dim startInfo As New ProcessStartInfo()
            startInfo.UseShellExecute = True
            startInfo.WorkingDirectory = Environment.CurrentDirectory
            startInfo.FileName = Application.ExecutablePath
            startInfo.Verb = "runas"
            Try
                Dim p As Process = Process.Start(startInfo)
            Catch ex As System.ComponentModel.Win32Exception
                'If cancelled, do nothing
                Return
            End Try

            Application.[Exit]()
        End Sub
    End Class
End Namespace