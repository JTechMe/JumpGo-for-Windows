<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AboutGo1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutGo1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LabelPrivacy = New System.Windows.Forms.LinkLabel()
        Me.LabelEUR = New System.Windows.Forms.LinkLabel()
        Me.LabelLicensing = New System.Windows.Forms.LinkLabel()
        Me.LabelCompanyName = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.LabelWhatsNew = New System.Windows.Forms.LinkLabel()
        Me.LabelCurVer = New System.Windows.Forms.Label()
        Me.LabelDesc = New System.Windows.Forms.Label()
        Me.LabelCheckUsOut = New System.Windows.Forms.LinkLabel()
        Me.LabelCopyright = New System.Windows.Forms.Label()
        Me.LinkDoUpdate = New System.Windows.Forms.LinkLabel()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.BackgroundImage = Global.JumpGoStandardEdition.My.Resources.Resources.poweredby_400_6d69147aa379___Copy
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(12, 47)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.JumpGoStandardEdition.My.Resources.Resources.JumpGo_4_2
        Me.PictureBox1.Location = New System.Drawing.Point(62, 27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(175, 175)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.LabelPrivacy)
        Me.Panel1.Controls.Add(Me.LabelEUR)
        Me.Panel1.Controls.Add(Me.LabelLicensing)
        Me.Panel1.Controls.Add(Me.LabelCompanyName)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 222)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(644, 82)
        Me.Panel1.TabIndex = 5
        '
        'LabelPrivacy
        '
        Me.LabelPrivacy.AutoSize = True
        Me.LabelPrivacy.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LabelPrivacy.Location = New System.Drawing.Point(437, 29)
        Me.LabelPrivacy.Name = "LabelPrivacy"
        Me.LabelPrivacy.Size = New System.Drawing.Size(73, 13)
        Me.LabelPrivacy.TabIndex = 10
        Me.LabelPrivacy.TabStop = True
        Me.LabelPrivacy.Text = "Privacy Policy"
        Me.LabelPrivacy.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'LabelEUR
        '
        Me.LabelEUR.AutoSize = True
        Me.LabelEUR.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LabelEUR.Location = New System.Drawing.Point(278, 29)
        Me.LabelEUR.Name = "LabelEUR"
        Me.LabelEUR.Size = New System.Drawing.Size(84, 13)
        Me.LabelEUR.TabIndex = 10
        Me.LabelEUR.TabStop = True
        Me.LabelEUR.Text = "End-User Rights"
        Me.LabelEUR.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'LabelLicensing
        '
        Me.LabelLicensing.AutoSize = True
        Me.LabelLicensing.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LabelLicensing.Location = New System.Drawing.Point(133, 29)
        Me.LabelLicensing.Name = "LabelLicensing"
        Me.LabelLicensing.Size = New System.Drawing.Size(107, 13)
        Me.LabelLicensing.TabIndex = 10
        Me.LabelLicensing.TabStop = True
        Me.LabelLicensing.Text = "Licensing Information"
        Me.LabelLicensing.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'LabelCompanyName
        '
        Me.LabelCompanyName.AutoSize = True
        Me.LabelCompanyName.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LabelCompanyName.Location = New System.Drawing.Point(278, 60)
        Me.LabelCompanyName.Name = "LabelCompanyName"
        Me.LabelCompanyName.Size = New System.Drawing.Size(82, 13)
        Me.LabelCompanyName.TabIndex = 4
        Me.LabelCompanyName.Text = "Company Name"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.JumpGoStandardEdition.My.Resources.Resources.JumpGoAboutLogo
        Me.PictureBox2.Location = New System.Drawing.Point(243, 27)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(163, 63)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(243, 93)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(42, 13)
        Me.LabelVersion.TabIndex = 7
        Me.LabelVersion.Text = "Version"
        '
        'LabelWhatsNew
        '
        Me.LabelWhatsNew.AutoSize = True
        Me.LabelWhatsNew.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LabelWhatsNew.Location = New System.Drawing.Point(347, 93)
        Me.LabelWhatsNew.Name = "LabelWhatsNew"
        Me.LabelWhatsNew.Size = New System.Drawing.Size(65, 13)
        Me.LabelWhatsNew.TabIndex = 8
        Me.LabelWhatsNew.TabStop = True
        Me.LabelWhatsNew.Text = "What's New"
        Me.LabelWhatsNew.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'LabelCurVer
        '
        Me.LabelCurVer.AutoSize = True
        Me.LabelCurVer.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LabelCurVer.Location = New System.Drawing.Point(243, 117)
        Me.LabelCurVer.Name = "LabelCurVer"
        Me.LabelCurVer.Size = New System.Drawing.Size(39, 13)
        Me.LabelCurVer.TabIndex = 9
        Me.LabelCurVer.Text = "CurVer"
        '
        'LabelDesc
        '
        Me.LabelDesc.AutoSize = True
        Me.LabelDesc.Location = New System.Drawing.Point(243, 137)
        Me.LabelDesc.Name = "LabelDesc"
        Me.LabelDesc.Size = New System.Drawing.Size(329, 65)
        Me.LabelDesc.TabIndex = 10
        Me.LabelDesc.Text = "JumpGo for Windows is a browser designed by JTechMe for the free" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and public use " &
    "of the internet. The JumpGo Project is open-source" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "and free-for-all." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Want to" &
    " help?"
        '
        'LabelCheckUsOut
        '
        Me.LabelCheckUsOut.AutoSize = True
        Me.LabelCheckUsOut.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.LabelCheckUsOut.Location = New System.Drawing.Point(315, 189)
        Me.LabelCheckUsOut.Name = "LabelCheckUsOut"
        Me.LabelCheckUsOut.Size = New System.Drawing.Size(121, 13)
        Me.LabelCheckUsOut.TabIndex = 11
        Me.LabelCheckUsOut.TabStop = True
        Me.LabelCheckUsOut.Text = "Check us out on GitHub"
        Me.LabelCheckUsOut.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'LabelCopyright
        '
        Me.LabelCopyright.AutoSize = True
        Me.LabelCopyright.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LabelCopyright.Location = New System.Drawing.Point(243, 206)
        Me.LabelCopyright.Name = "LabelCopyright"
        Me.LabelCopyright.Size = New System.Drawing.Size(51, 13)
        Me.LabelCopyright.TabIndex = 11
        Me.LabelCopyright.Text = "Copyright"
        '
        'LinkDoUpdate
        '
        Me.LinkDoUpdate.AutoSize = True
        Me.LinkDoUpdate.Location = New System.Drawing.Point(415, 117)
        Me.LinkDoUpdate.Name = "LinkDoUpdate"
        Me.LinkDoUpdate.Size = New System.Drawing.Size(42, 13)
        Me.LinkDoUpdate.TabIndex = 12
        Me.LinkDoUpdate.TabStop = True
        Me.LinkDoUpdate.Text = "Update"
        Me.LinkDoUpdate.Visible = False
        '
        'AboutGo1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(644, 304)
        Me.Controls.Add(Me.LinkDoUpdate)
        Me.Controls.Add(Me.LabelCopyright)
        Me.Controls.Add(Me.LabelCheckUsOut)
        Me.Controls.Add(Me.LabelDesc)
        Me.Controls.Add(Me.LabelCurVer)
        Me.Controls.Add(Me.LabelWhatsNew)
        Me.Controls.Add(Me.LabelVersion)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AboutGo1"
        Me.ShowIcon = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "About JumpGo"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents LabelVersion As Label
    Friend WithEvents LabelCompanyName As Label
    Friend WithEvents LabelWhatsNew As LinkLabel
    Friend WithEvents LabelCurVer As Label
    Friend WithEvents LabelEUR As LinkLabel
    Friend WithEvents LabelLicensing As LinkLabel
    Friend WithEvents LabelPrivacy As LinkLabel
    Friend WithEvents LabelDesc As Label
    Friend WithEvents LabelCheckUsOut As LinkLabel
    Friend WithEvents LabelCopyright As Label
    Friend WithEvents LinkDoUpdate As LinkLabel
End Class
