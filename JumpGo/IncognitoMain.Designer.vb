<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IncognitoMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IncognitoMain))
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1 = New MdiTabControl.TabControl()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.NewTabToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewPrivateWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.OpenFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SavePageAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.UserControlBox1 = New JumpGoStandardEdition.UserControlBox()
        Me.ToolStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.ToolStrip1.Location = New System.Drawing.Point(933, 0)
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
        'TabControl1
        '
        Me.TabControl1.AutoSize = True
        Me.TabControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TabControl1.BackColor = System.Drawing.Color.Transparent
        Me.TabControl1.BackHighColor = System.Drawing.Color.Transparent
        Me.TabControl1.BackLowColor = System.Drawing.Color.Transparent
        Me.TabControl1.BorderColor = System.Drawing.Color.DimGray
        Me.TabControl1.BorderColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.ContextMenuStrip = Me.ContextMenuStrip2
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.MenuRenderer = Nothing
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.Size = New System.Drawing.Size(880, 527)
        Me.TabControl1.TabBackHighColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabBackHighColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.TabBackLowColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.TabBorderEnhanced = True
        Me.TabControl1.TabBorderEnhanceWeight = MdiTabControl.TabControl.Weight.Soft
        Me.TabControl1.TabCloseButtonBackHighColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackHighColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.TabCloseButtonBackHighColorHot = System.Drawing.SystemColors.ControlLight
        Me.TabControl1.TabCloseButtonBackLowColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBackLowColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.TabCloseButtonBackLowColorHot = System.Drawing.SystemColors.ControlLight
        Me.TabControl1.TabCloseButtonBorderColor = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonBorderColorDisabled = System.Drawing.Color.Transparent
        Me.TabControl1.TabCloseButtonBorderColorHot = System.Drawing.SystemColors.Control
        Me.TabControl1.TabCloseButtonForeColor = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonForeColorDisabled = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonForeColorHot = System.Drawing.Color.DimGray
        Me.TabControl1.TabCloseButtonImage = Global.JumpGoStandardEdition.My.Resources.Resources.tabclose
        Me.TabControl1.TabCloseButtonImageDisabled = Nothing
        Me.TabControl1.TabCloseButtonImageHot = Global.JumpGoStandardEdition.My.Resources.Resources.tabclosehot
        Me.TabControl1.TabGlassGradient = True
        Me.TabControl1.TabIndex = 2
        Me.TabControl1.TabMinimumWidth = 200
        Me.TabControl1.TabOffset = 0
        Me.TabControl1.TabPlusButton = Me.Button1
        Me.TabControl1.TabPlusImage = Nothing
        Me.TabControl1.TabPlusVisable = True
        Me.TabControl1.TopSeparator = False
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(106, 26)
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.Image = Global.JumpGoStandardEdition.My.Resources.Resources.MenuTransparent
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.MenuToolStripMenuItem.Text = "Menu"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(536, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 47)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ToolStrip2
        '
        Me.ToolStrip2.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip2.ContextMenuStrip = Me.ContextMenuStrip2
        Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(781, 25)
        Me.ToolStrip2.TabIndex = 8
        Me.ToolStrip2.Text = "ToolStrip2"
        Me.ToolStrip2.Visible = False
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewTabToolStripMenuItem, Me.NewWindowToolStripMenuItem, Me.NewPrivateWindowToolStripMenuItem, Me.ToolStripSeparator1, Me.OpenFileToolStripMenuItem, Me.SavePageAsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.ToolStripDropDownButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ToolStripDropDownButton1.Image = Global.JumpGoStandardEdition.My.Resources.Resources.MenuTransparent
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(54, 22)
        Me.ToolStripDropDownButton1.Text = "File"
        '
        'NewTabToolStripMenuItem
        '
        Me.NewTabToolStripMenuItem.Name = "NewTabToolStripMenuItem"
        Me.NewTabToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.NewTabToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.NewTabToolStripMenuItem.Text = "New Tab"
        '
        'NewWindowToolStripMenuItem
        '
        Me.NewWindowToolStripMenuItem.Name = "NewWindowToolStripMenuItem"
        Me.NewWindowToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewWindowToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.NewWindowToolStripMenuItem.Text = "New Window"
        '
        'NewPrivateWindowToolStripMenuItem
        '
        Me.NewPrivateWindowToolStripMenuItem.Name = "NewPrivateWindowToolStripMenuItem"
        Me.NewPrivateWindowToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.NewPrivateWindowToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.NewPrivateWindowToolStripMenuItem.Text = "New Private Window"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(254, 6)
        '
        'OpenFileToolStripMenuItem
        '
        Me.OpenFileToolStripMenuItem.Enabled = False
        Me.OpenFileToolStripMenuItem.Name = "OpenFileToolStripMenuItem"
        Me.OpenFileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenFileToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.OpenFileToolStripMenuItem.Text = "Open File..."
        '
        'SavePageAsToolStripMenuItem
        '
        Me.SavePageAsToolStripMenuItem.Enabled = False
        Me.SavePageAsToolStripMenuItem.Name = "SavePageAsToolStripMenuItem"
        Me.SavePageAsToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.SavePageAsToolStripMenuItem.Text = "Save Page As..."
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(257, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.Violet
        Me.PictureBox1.BackgroundImage = Global.JumpGoStandardEdition.My.Resources.Resources.JGTabControlGradient_HC
        Me.PictureBox1.Image = Global.JumpGoStandardEdition.My.Resources.Resources.ic_incognito
        Me.PictureBox1.Location = New System.Drawing.Point(739, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 21)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'UserControlBox1
        '
        Me.UserControlBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserControlBox1.Location = New System.Drawing.Point(777, 0)
        Me.UserControlBox1.Name = "UserControlBox1"
        Me.UserControlBox1.Size = New System.Drawing.Size(100, 21)
        Me.UserControlBox1.TabIndex = 9
        '
        'IncognitoMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 527)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.UserControlBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "IncognitoMain"
        Me.ShowIcon = False
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As MdiTabControl.TabControl
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents ToolStrip2 As ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As ToolStripDropDownButton
    Friend WithEvents NewTabToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewPrivateWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents SavePageAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents MenuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UserControlBox1 As UserControlBox
    Friend WithEvents PictureBox1 As PictureBox
End Class
