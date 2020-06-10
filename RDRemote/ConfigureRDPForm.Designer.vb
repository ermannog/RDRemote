<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigureRDPForm
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
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.chkAudioMapping = New System.Windows.Forms.CheckBox
        Me.chkDriveMapping = New System.Windows.Forms.CheckBox
        Me.chkClipboardMapping = New System.Windows.Forms.CheckBox
        Me.chkLPTPortMapping = New System.Windows.Forms.CheckBox
        Me.chkCOMPortMapping = New System.Windows.Forms.CheckBox
        Me.chkWindowsPrinterMapping = New System.Windows.Forms.CheckBox
        Me.chkPNPRedirection = New System.Windows.Forms.CheckBox
        Me.chkConnectPrinterAtLogon = New System.Windows.Forms.CheckBox
        Me.chkDefaultToClientPrinter = New System.Windows.Forms.CheckBox
        Me.chkConnectClientDrivesAtLogon = New System.Windows.Forms.CheckBox
        Me.lblConnectionPolicy = New System.Windows.Forms.Label
        Me.cmbConnectionPolicy = New System.Windows.Forms.ComboBox
        Me.grpConnectionSettings = New System.Windows.Forms.GroupBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.grpUserExperience = New System.Windows.Forms.GroupBox
        Me.nudMaxYResolution = New System.Windows.Forms.NumericUpDown
        Me.nudMaxXResolution = New System.Windows.Forms.NumericUpDown
        Me.lblMaxResolution = New System.Windows.Forms.Label
        Me.nudMaxMonitors = New System.Windows.Forms.NumericUpDown
        Me.lblMaxMonitors = New System.Windows.Forms.Label
        Me.chkColorDepthPolicy = New System.Windows.Forms.CheckBox
        Me.cmbColorDepth = New System.Windows.Forms.ComboBox
        Me.lblColorDepth = New System.Windows.Forms.Label
        Me.chkAllowDwm = New System.Windows.Forms.CheckBox
        Me.grpConnectionSettings.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grpUserExperience.SuspendLayout()
        CType(Me.nudMaxYResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMaxXResolution, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudMaxMonitors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(405, 231)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnOK.Location = New System.Drawing.Point(324, 231)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'chkAudioMapping
        '
        Me.chkAudioMapping.AutoSize = True
        Me.chkAudioMapping.Location = New System.Drawing.Point(6, 88)
        Me.chkAudioMapping.Name = "chkAudioMapping"
        Me.chkAudioMapping.Size = New System.Drawing.Size(96, 17)
        Me.chkAudioMapping.TabIndex = 2
        Me.chkAudioMapping.Text = "Audio mapping"
        Me.chkAudioMapping.UseVisualStyleBackColor = True
        '
        'chkDriveMapping
        '
        Me.chkDriveMapping.AutoSize = True
        Me.chkDriveMapping.Location = New System.Drawing.Point(6, 65)
        Me.chkDriveMapping.Name = "chkDriveMapping"
        Me.chkDriveMapping.Size = New System.Drawing.Size(94, 17)
        Me.chkDriveMapping.TabIndex = 3
        Me.chkDriveMapping.Text = "Drive mapping"
        Me.chkDriveMapping.UseVisualStyleBackColor = True
        '
        'chkClipboardMapping
        '
        Me.chkClipboardMapping.AutoSize = True
        Me.chkClipboardMapping.Location = New System.Drawing.Point(6, 19)
        Me.chkClipboardMapping.Name = "chkClipboardMapping"
        Me.chkClipboardMapping.Size = New System.Drawing.Size(113, 17)
        Me.chkClipboardMapping.TabIndex = 4
        Me.chkClipboardMapping.Text = "Clipboard mapping"
        Me.chkClipboardMapping.UseVisualStyleBackColor = True
        '
        'chkLPTPortMapping
        '
        Me.chkLPTPortMapping.AutoSize = True
        Me.chkLPTPortMapping.Location = New System.Drawing.Point(6, 111)
        Me.chkLPTPortMapping.Name = "chkLPTPortMapping"
        Me.chkLPTPortMapping.Size = New System.Drawing.Size(110, 17)
        Me.chkLPTPortMapping.TabIndex = 5
        Me.chkLPTPortMapping.Text = "LPT port mapping"
        Me.chkLPTPortMapping.UseVisualStyleBackColor = True
        '
        'chkCOMPortMapping
        '
        Me.chkCOMPortMapping.AutoSize = True
        Me.chkCOMPortMapping.Location = New System.Drawing.Point(6, 134)
        Me.chkCOMPortMapping.Name = "chkCOMPortMapping"
        Me.chkCOMPortMapping.Size = New System.Drawing.Size(114, 17)
        Me.chkCOMPortMapping.TabIndex = 6
        Me.chkCOMPortMapping.Text = "COM port mapping"
        Me.chkCOMPortMapping.UseVisualStyleBackColor = True
        '
        'chkWindowsPrinterMapping
        '
        Me.chkWindowsPrinterMapping.AutoSize = True
        Me.chkWindowsPrinterMapping.Location = New System.Drawing.Point(6, 42)
        Me.chkWindowsPrinterMapping.Name = "chkWindowsPrinterMapping"
        Me.chkWindowsPrinterMapping.Size = New System.Drawing.Size(99, 17)
        Me.chkWindowsPrinterMapping.TabIndex = 7
        Me.chkWindowsPrinterMapping.Text = "Printer mapping"
        Me.chkWindowsPrinterMapping.UseVisualStyleBackColor = True
        '
        'chkPNPRedirection
        '
        Me.chkPNPRedirection.AutoSize = True
        Me.chkPNPRedirection.Location = New System.Drawing.Point(6, 157)
        Me.chkPNPRedirection.Name = "chkPNPRedirection"
        Me.chkPNPRedirection.Size = New System.Drawing.Size(100, 17)
        Me.chkPNPRedirection.TabIndex = 8
        Me.chkPNPRedirection.Text = "PNP redirection"
        Me.chkPNPRedirection.UseVisualStyleBackColor = True
        '
        'chkConnectPrinterAtLogon
        '
        Me.chkConnectPrinterAtLogon.AutoSize = True
        Me.chkConnectPrinterAtLogon.Enabled = False
        Me.chkConnectPrinterAtLogon.Location = New System.Drawing.Point(6, 65)
        Me.chkConnectPrinterAtLogon.Name = "chkConnectPrinterAtLogon"
        Me.chkConnectPrinterAtLogon.Size = New System.Drawing.Size(139, 17)
        Me.chkConnectPrinterAtLogon.TabIndex = 9
        Me.chkConnectPrinterAtLogon.Text = "Connect printer at logon"
        Me.chkConnectPrinterAtLogon.UseVisualStyleBackColor = True
        '
        'chkDefaultToClientPrinter
        '
        Me.chkDefaultToClientPrinter.AutoSize = True
        Me.chkDefaultToClientPrinter.Enabled = False
        Me.chkDefaultToClientPrinter.Location = New System.Drawing.Point(6, 42)
        Me.chkDefaultToClientPrinter.Name = "chkDefaultToClientPrinter"
        Me.chkDefaultToClientPrinter.Size = New System.Drawing.Size(132, 17)
        Me.chkDefaultToClientPrinter.TabIndex = 10
        Me.chkDefaultToClientPrinter.Text = "Default to client printer"
        Me.chkDefaultToClientPrinter.UseVisualStyleBackColor = True
        '
        'chkConnectClientDrivesAtLogon
        '
        Me.chkConnectClientDrivesAtLogon.AutoSize = True
        Me.chkConnectClientDrivesAtLogon.Enabled = False
        Me.chkConnectClientDrivesAtLogon.Location = New System.Drawing.Point(197, 42)
        Me.chkConnectClientDrivesAtLogon.Name = "chkConnectClientDrivesAtLogon"
        Me.chkConnectClientDrivesAtLogon.Size = New System.Drawing.Size(138, 17)
        Me.chkConnectClientDrivesAtLogon.TabIndex = 11
        Me.chkConnectClientDrivesAtLogon.Text = "Connect drives at logon"
        Me.chkConnectClientDrivesAtLogon.UseVisualStyleBackColor = True
        '
        'lblConnectionPolicy
        '
        Me.lblConnectionPolicy.AutoSize = True
        Me.lblConnectionPolicy.Location = New System.Drawing.Point(3, 20)
        Me.lblConnectionPolicy.Name = "lblConnectionPolicy"
        Me.lblConnectionPolicy.Size = New System.Drawing.Size(94, 13)
        Me.lblConnectionPolicy.TabIndex = 12
        Me.lblConnectionPolicy.Text = "Connection policy:"
        '
        'cmbConnectionPolicy
        '
        Me.cmbConnectionPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbConnectionPolicy.FormattingEnabled = True
        Me.cmbConnectionPolicy.Location = New System.Drawing.Point(103, 17)
        Me.cmbConnectionPolicy.Name = "cmbConnectionPolicy"
        Me.cmbConnectionPolicy.Size = New System.Drawing.Size(232, 21)
        Me.cmbConnectionPolicy.TabIndex = 13
        '
        'grpConnectionSettings
        '
        Me.grpConnectionSettings.Controls.Add(Me.chkDefaultToClientPrinter)
        Me.grpConnectionSettings.Controls.Add(Me.lblConnectionPolicy)
        Me.grpConnectionSettings.Controls.Add(Me.cmbConnectionPolicy)
        Me.grpConnectionSettings.Controls.Add(Me.chkConnectPrinterAtLogon)
        Me.grpConnectionSettings.Controls.Add(Me.chkConnectClientDrivesAtLogon)
        Me.grpConnectionSettings.Location = New System.Drawing.Point(139, 12)
        Me.grpConnectionSettings.Name = "grpConnectionSettings"
        Me.grpConnectionSettings.Size = New System.Drawing.Size(341, 86)
        Me.grpConnectionSettings.TabIndex = 14
        Me.grpConnectionSettings.TabStop = False
        Me.grpConnectionSettings.Text = "Connection settings"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkCOMPortMapping)
        Me.GroupBox1.Controls.Add(Me.chkPNPRedirection)
        Me.GroupBox1.Controls.Add(Me.chkAudioMapping)
        Me.GroupBox1.Controls.Add(Me.chkWindowsPrinterMapping)
        Me.GroupBox1.Controls.Add(Me.chkLPTPortMapping)
        Me.GroupBox1.Controls.Add(Me.chkDriveMapping)
        Me.GroupBox1.Controls.Add(Me.chkClipboardMapping)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(124, 185)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Client properties"
        '
        'grpUserExperience
        '
        Me.grpUserExperience.Controls.Add(Me.nudMaxYResolution)
        Me.grpUserExperience.Controls.Add(Me.nudMaxXResolution)
        Me.grpUserExperience.Controls.Add(Me.lblMaxResolution)
        Me.grpUserExperience.Controls.Add(Me.nudMaxMonitors)
        Me.grpUserExperience.Controls.Add(Me.lblMaxMonitors)
        Me.grpUserExperience.Controls.Add(Me.chkColorDepthPolicy)
        Me.grpUserExperience.Controls.Add(Me.cmbColorDepth)
        Me.grpUserExperience.Controls.Add(Me.lblColorDepth)
        Me.grpUserExperience.Controls.Add(Me.chkAllowDwm)
        Me.grpUserExperience.Location = New System.Drawing.Point(139, 104)
        Me.grpUserExperience.Name = "grpUserExperience"
        Me.grpUserExperience.Size = New System.Drawing.Size(341, 93)
        Me.grpUserExperience.TabIndex = 16
        Me.grpUserExperience.TabStop = False
        Me.grpUserExperience.Text = "User experience"
        '
        'nudMaxYResolution
        '
        Me.nudMaxYResolution.Location = New System.Drawing.Point(285, 64)
        Me.nudMaxYResolution.Maximum = New Decimal(New Integer() {8192, 0, 0, 0})
        Me.nudMaxYResolution.Minimum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.nudMaxYResolution.Name = "nudMaxYResolution"
        Me.nudMaxYResolution.Size = New System.Drawing.Size(50, 20)
        Me.nudMaxYResolution.TabIndex = 20
        Me.nudMaxYResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudMaxYResolution.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'nudMaxXResolution
        '
        Me.nudMaxXResolution.Location = New System.Drawing.Point(229, 64)
        Me.nudMaxXResolution.Maximum = New Decimal(New Integer() {8192, 0, 0, 0})
        Me.nudMaxXResolution.Minimum = New Decimal(New Integer() {200, 0, 0, 0})
        Me.nudMaxXResolution.Name = "nudMaxXResolution"
        Me.nudMaxXResolution.Size = New System.Drawing.Size(50, 20)
        Me.nudMaxXResolution.TabIndex = 19
        Me.nudMaxXResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudMaxXResolution.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'lblMaxResolution
        '
        Me.lblMaxResolution.AutoSize = True
        Me.lblMaxResolution.Location = New System.Drawing.Point(121, 66)
        Me.lblMaxResolution.Name = "lblMaxResolution"
        Me.lblMaxResolution.Size = New System.Drawing.Size(106, 13)
        Me.lblMaxResolution.TabIndex = 18
        Me.lblMaxResolution.Text = "Max resolution (XxY):"
        '
        'nudMaxMonitors
        '
        Me.nudMaxMonitors.Location = New System.Drawing.Point(75, 64)
        Me.nudMaxMonitors.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.nudMaxMonitors.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudMaxMonitors.Name = "nudMaxMonitors"
        Me.nudMaxMonitors.Size = New System.Drawing.Size(40, 20)
        Me.nudMaxMonitors.TabIndex = 17
        Me.nudMaxMonitors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nudMaxMonitors.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblMaxMonitors
        '
        Me.lblMaxMonitors.AutoSize = True
        Me.lblMaxMonitors.Location = New System.Drawing.Point(3, 66)
        Me.lblMaxMonitors.Name = "lblMaxMonitors"
        Me.lblMaxMonitors.Size = New System.Drawing.Size(72, 13)
        Me.lblMaxMonitors.TabIndex = 16
        Me.lblMaxMonitors.Text = "Max monitors:"
        '
        'chkColorDepthPolicy
        '
        Me.chkColorDepthPolicy.AutoSize = True
        Me.chkColorDepthPolicy.Location = New System.Drawing.Point(6, 19)
        Me.chkColorDepthPolicy.Name = "chkColorDepthPolicy"
        Me.chkColorDepthPolicy.Size = New System.Drawing.Size(130, 17)
        Me.chkColorDepthPolicy.TabIndex = 15
        Me.chkColorDepthPolicy.Text = "Override user's setting"
        Me.chkColorDepthPolicy.UseVisualStyleBackColor = True
        '
        'cmbColorDepth
        '
        Me.cmbColorDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbColorDepth.FormattingEnabled = True
        Me.cmbColorDepth.Location = New System.Drawing.Point(212, 17)
        Me.cmbColorDepth.Name = "cmbColorDepth"
        Me.cmbColorDepth.Size = New System.Drawing.Size(123, 21)
        Me.cmbColorDepth.TabIndex = 14
        '
        'lblColorDepth
        '
        Me.lblColorDepth.AutoSize = True
        Me.lblColorDepth.Location = New System.Drawing.Point(142, 20)
        Me.lblColorDepth.Name = "lblColorDepth"
        Me.lblColorDepth.Size = New System.Drawing.Size(64, 13)
        Me.lblColorDepth.TabIndex = 14
        Me.lblColorDepth.Text = "Color depth:"
        '
        'chkAllowDwm
        '
        Me.chkAllowDwm.AutoSize = True
        Me.chkAllowDwm.Location = New System.Drawing.Point(6, 42)
        Me.chkAllowDwm.Name = "chkAllowDwm"
        Me.chkAllowDwm.Size = New System.Drawing.Size(125, 17)
        Me.chkAllowDwm.TabIndex = 9
        Me.chkAllowDwm.Text = "Desktop composition"
        Me.chkAllowDwm.UseVisualStyleBackColor = True
        '
        'ConfigureRDPForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(492, 266)
        Me.Controls.Add(Me.grpUserExperience)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpConnectionSettings)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigureRDPForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configure RDP"
        Me.grpConnectionSettings.ResumeLayout(False)
        Me.grpConnectionSettings.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grpUserExperience.ResumeLayout(False)
        Me.grpUserExperience.PerformLayout()
        CType(Me.nudMaxYResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMaxXResolution, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudMaxMonitors, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents chkAudioMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkDriveMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkClipboardMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkLPTPortMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkCOMPortMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkWindowsPrinterMapping As System.Windows.Forms.CheckBox
    Friend WithEvents chkPNPRedirection As System.Windows.Forms.CheckBox
    Friend WithEvents chkConnectPrinterAtLogon As System.Windows.Forms.CheckBox
    Friend WithEvents chkDefaultToClientPrinter As System.Windows.Forms.CheckBox
    Friend WithEvents chkConnectClientDrivesAtLogon As System.Windows.Forms.CheckBox
    Friend WithEvents lblConnectionPolicy As System.Windows.Forms.Label
    Friend WithEvents cmbConnectionPolicy As System.Windows.Forms.ComboBox
    Friend WithEvents grpConnectionSettings As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpUserExperience As System.Windows.Forms.GroupBox
    Friend WithEvents chkAllowDwm As System.Windows.Forms.CheckBox
    Friend WithEvents lblColorDepth As System.Windows.Forms.Label
    Friend WithEvents cmbColorDepth As System.Windows.Forms.ComboBox
    Friend WithEvents chkColorDepthPolicy As System.Windows.Forms.CheckBox
    Friend WithEvents nudMaxMonitors As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxMonitors As System.Windows.Forms.Label
    Friend WithEvents lblMaxResolution As System.Windows.Forms.Label
    Friend WithEvents nudMaxYResolution As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudMaxXResolution As System.Windows.Forms.NumericUpDown
End Class
