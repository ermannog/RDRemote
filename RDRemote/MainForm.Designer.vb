<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.lblRemoteSystem = New System.Windows.Forms.Label
        Me.txtRemoteSystem = New System.Windows.Forms.TextBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.lblUser = New System.Windows.Forms.Label
        Me.lblConnectionNotes = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.lblPassword = New System.Windows.Forms.Label
        Me.grpSystemInformation = New System.Windows.Forms.GroupBox
        Me.lsvSystemInformation = New System.Windows.Forms.ListView
        Me.colElement = New System.Windows.Forms.ColumnHeader
        Me.colValue = New System.Windows.Forms.ColumnHeader
        Me.stsMain = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.btnEnableRDP = New System.Windows.Forms.Button
        Me.btnDisableRDP = New System.Windows.Forms.Button
        Me.tltListView = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnConfigureRDP = New System.Windows.Forms.Button
        Me.grpSystemInformation.SuspendLayout()
        Me.stsMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblRemoteSystem
        '
        Me.lblRemoteSystem.AutoSize = True
        Me.lblRemoteSystem.Location = New System.Drawing.Point(12, 9)
        Me.lblRemoteSystem.Name = "lblRemoteSystem"
        Me.lblRemoteSystem.Size = New System.Drawing.Size(83, 13)
        Me.lblRemoteSystem.TabIndex = 0
        Me.lblRemoteSystem.Text = "Hostname or IP:"
        '
        'txtRemoteSystem
        '
        Me.txtRemoteSystem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemoteSystem.Location = New System.Drawing.Point(15, 25)
        Me.txtRemoteSystem.Name = "txtRemoteSystem"
        Me.txtRemoteSystem.Size = New System.Drawing.Size(223, 20)
        Me.txtRemoteSystem.TabIndex = 3
        '
        'btnConnect
        '
        Me.btnConnect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConnect.Location = New System.Drawing.Point(455, 268)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(75, 23)
        Me.btnConnect.TabIndex = 7
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'lblUser
        '
        Me.lblUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(241, 9)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(32, 13)
        Me.lblUser.TabIndex = 1
        Me.lblUser.Text = "User:"
        '
        'lblConnectionNotes
        '
        Me.lblConnectionNotes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConnectionNotes.Location = New System.Drawing.Point(12, 48)
        Me.lblConnectionNotes.Name = "lblConnectionNotes"
        Me.lblConnectionNotes.Size = New System.Drawing.Size(518, 26)
        Me.lblConnectionNotes.TabIndex = 6
        Me.lblConnectionNotes.Text = "If credentials are not provided the connection will be opened with the credential" & _
            "s of the session or any specified network password."
        '
        'txtUser
        '
        Me.txtUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUser.Location = New System.Drawing.Point(244, 25)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(140, 20)
        Me.txtUser.TabIndex = 4
        '
        'txtPassword
        '
        Me.txtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPassword.Location = New System.Drawing.Point(390, 25)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9679)
        Me.txtPassword.Size = New System.Drawing.Size(140, 20)
        Me.txtPassword.TabIndex = 5
        '
        'lblPassword
        '
        Me.lblPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(387, 9)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(56, 13)
        Me.lblPassword.TabIndex = 2
        Me.lblPassword.Text = "Password:"
        '
        'grpSystemInformation
        '
        Me.grpSystemInformation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSystemInformation.Controls.Add(Me.lsvSystemInformation)
        Me.grpSystemInformation.Location = New System.Drawing.Point(12, 77)
        Me.grpSystemInformation.Name = "grpSystemInformation"
        Me.grpSystemInformation.Size = New System.Drawing.Size(518, 185)
        Me.grpSystemInformation.TabIndex = 8
        Me.grpSystemInformation.TabStop = False
        Me.grpSystemInformation.Text = "System informations"
        '
        'lsvSystemInformation
        '
        Me.lsvSystemInformation.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colElement, Me.colValue})
        Me.lsvSystemInformation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lsvSystemInformation.FullRowSelect = True
        Me.lsvSystemInformation.Location = New System.Drawing.Point(3, 16)
        Me.lsvSystemInformation.Name = "lsvSystemInformation"
        Me.lsvSystemInformation.ShowItemToolTips = True
        Me.lsvSystemInformation.Size = New System.Drawing.Size(512, 166)
        Me.lsvSystemInformation.TabIndex = 0
        Me.lsvSystemInformation.UseCompatibleStateImageBehavior = False
        Me.lsvSystemInformation.View = System.Windows.Forms.View.Details
        '
        'colElement
        '
        Me.colElement.Text = "Element"
        Me.colElement.Width = 175
        '
        'colValue
        '
        Me.colValue.Text = "Value"
        Me.colValue.Width = 315
        '
        'stsMain
        '
        Me.stsMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus})
        Me.stsMain.Location = New System.Drawing.Point(0, 294)
        Me.stsMain.Name = "stsMain"
        Me.stsMain.Size = New System.Drawing.Size(542, 22)
        Me.stsMain.SizingGrip = False
        Me.stsMain.TabIndex = 11
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(29, 17)
        Me.lblStatus.Text = "Text"
        '
        'btnEnableRDP
        '
        Me.btnEnableRDP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnEnableRDP.Enabled = False
        Me.btnEnableRDP.Location = New System.Drawing.Point(98, 268)
        Me.btnEnableRDP.Name = "btnEnableRDP"
        Me.btnEnableRDP.Size = New System.Drawing.Size(80, 23)
        Me.btnEnableRDP.TabIndex = 10
        Me.btnEnableRDP.Text = "Enable RDP"
        Me.btnEnableRDP.UseVisualStyleBackColor = True
        '
        'btnDisableRDP
        '
        Me.btnDisableRDP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDisableRDP.Enabled = False
        Me.btnDisableRDP.Location = New System.Drawing.Point(12, 268)
        Me.btnDisableRDP.Name = "btnDisableRDP"
        Me.btnDisableRDP.Size = New System.Drawing.Size(80, 23)
        Me.btnDisableRDP.TabIndex = 9
        Me.btnDisableRDP.Text = "Disable RDP"
        Me.btnDisableRDP.UseVisualStyleBackColor = True
        '
        'tltListView
        '
        Me.tltListView.AutoPopDelay = 750
        Me.tltListView.InitialDelay = 500
        Me.tltListView.ReshowDelay = 100
        '
        'btnConfigureRDP
        '
        Me.btnConfigureRDP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConfigureRDP.Enabled = False
        Me.btnConfigureRDP.Location = New System.Drawing.Point(184, 268)
        Me.btnConfigureRDP.Name = "btnConfigureRDP"
        Me.btnConfigureRDP.Size = New System.Drawing.Size(75, 23)
        Me.btnConfigureRDP.TabIndex = 12
        Me.btnConfigureRDP.Text = "Configure"
        Me.btnConfigureRDP.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 316)
        Me.Controls.Add(Me.btnConfigureRDP)
        Me.Controls.Add(Me.stsMain)
        Me.Controls.Add(Me.grpSystemInformation)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.btnEnableRDP)
        Me.Controls.Add(Me.btnDisableRDP)
        Me.Controls.Add(Me.lblConnectionNotes)
        Me.Controls.Add(Me.lblUser)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtRemoteSystem)
        Me.Controls.Add(Me.lblRemoteSystem)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MainForm"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "{0} {1}"
        Me.grpSystemInformation.ResumeLayout(False)
        Me.stsMain.ResumeLayout(False)
        Me.stsMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblRemoteSystem As System.Windows.Forms.Label
    Friend WithEvents txtRemoteSystem As System.Windows.Forms.TextBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents lblConnectionNotes As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents grpSystemInformation As System.Windows.Forms.GroupBox
    Friend WithEvents stsMain As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnDisableRDP As System.Windows.Forms.Button
    Friend WithEvents btnEnableRDP As System.Windows.Forms.Button
    Friend WithEvents lsvSystemInformation As System.Windows.Forms.ListView
    Friend WithEvents colElement As System.Windows.Forms.ColumnHeader
    Friend WithEvents colValue As System.Windows.Forms.ColumnHeader
    Friend WithEvents tltListView As System.Windows.Forms.ToolTip
    Friend WithEvents btnConfigureRDP As System.Windows.Forms.Button

End Class
