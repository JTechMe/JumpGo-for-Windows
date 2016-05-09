<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JumpGoMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JumpGoMain))
        Me.TabControl1 = New MdiTabControl.TabControl()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.AllowTabReorder = False
        Me.TabControl1.AutoSize = True
        Me.TabControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TabControl1.BackColor = System.Drawing.Color.Transparent
        Me.TabControl1.BackHighColor = System.Drawing.Color.Transparent
        Me.TabControl1.BackLowColor = System.Drawing.Color.Transparent
        Me.TabControl1.BorderColor = System.Drawing.Color.Transparent
        Me.TabControl1.BorderColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.MenuRenderer = Nothing
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Size = New System.Drawing.Size(781, 472)
        Me.TabControl1.TabBackHighColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabBackLowColorDisabled = System.Drawing.SystemColors.Control
        Me.TabControl1.TabBorderEnhanced = True
        Me.TabControl1.TabBorderEnhanceWeight = MdiTabControl.TabControl.Weight.Strongest
        Me.TabControl1.TabCloseButtonBackHighColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackHighColorDisabled = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackHighColorHot = System.Drawing.SystemColors.ControlLight
        Me.TabControl1.TabCloseButtonBackLowColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackLowColorDisabled = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackLowColorHot = System.Drawing.SystemColors.ControlLight
        Me.TabControl1.TabCloseButtonBorderColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBorderColorDisabled = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBorderColorHot = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonForeColor = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonForeColorDisabled = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonForeColorHot = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonImage = Nothing
        Me.TabControl1.TabCloseButtonImageDisabled = Nothing
        Me.TabControl1.TabCloseButtonImageHot = Nothing
        Me.TabControl1.TabGlassGradient = True
        Me.TabControl1.TabIndex = 2
        Me.TabControl1.TabMinimumWidth = 16
        Me.TabControl1.TopSeparator = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.BackColor = System.Drawing.Color.DimGray
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(757, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(24, 31)
        Me.ToolStrip1.Stretch = True
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.Visible = False
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.JumpGoStandardEdition.My.Resources.Resources.Add
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 28)
        Me.ToolStripButton1.Text = "New Tab"
        '
        'Timer2
        '
        Me.Timer2.Interval = 1
        '
        'JumpGoMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 472)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "JumpGoMain"
        Me.ShowIcon = False
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As MdiTabControl.TabControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
End Class
