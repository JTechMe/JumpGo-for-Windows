<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WebAppForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WebAppForm))
        Me.FasterBrowser1 = New JumpGoStandardEdition.FasterBrowser()
        Me.SuspendLayout()
        '
        'FasterBrowser1
        '
        Me.FasterBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FasterBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.FasterBrowser1.Name = "FasterBrowser1"
        Me.FasterBrowser1.Size = New System.Drawing.Size(699, 507)
        Me.FasterBrowser1.TabIndex = 0
        Me.FasterBrowser1.UseHttpActivityObserver = False
        '
        'WebAppForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(699, 507)
        Me.Controls.Add(Me.FasterBrowser1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WebAppForm"
        Me.Text = "WebAppForm"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FasterBrowser1 As JumpGoStandardEdition.FasterBrowser
End Class
