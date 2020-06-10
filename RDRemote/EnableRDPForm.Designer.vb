<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EnableRDPForm
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
        Me.chkEnableNLA = New System.Windows.Forms.CheckBox
        Me.chkEnableFirewallRule = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(205, 131)
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
        Me.btnOK.Location = New System.Drawing.Point(124, 131)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'chkEnableNLA
        '
        Me.chkEnableNLA.Checked = True
        Me.chkEnableNLA.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnableNLA.Location = New System.Drawing.Point(12, 12)
        Me.chkEnableNLA.Name = "chkEnableNLA"
        Me.chkEnableNLA.Size = New System.Drawing.Size(268, 51)
        Me.chkEnableNLA.TabIndex = 2
        Me.chkEnableNLA.Text = "Allow connections only from computers running Remote Desktop with Network Level A" & _
            "uthentication"
        Me.chkEnableNLA.UseVisualStyleBackColor = True
        '
        'chkEnableFirewallRule
        '
        Me.chkEnableFirewallRule.AutoSize = True
        Me.chkEnableFirewallRule.Checked = True
        Me.chkEnableFirewallRule.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnableFirewallRule.Location = New System.Drawing.Point(12, 69)
        Me.chkEnableFirewallRule.Name = "chkEnableFirewallRule"
        Me.chkEnableFirewallRule.Size = New System.Drawing.Size(184, 17)
        Me.chkEnableFirewallRule.TabIndex = 3
        Me.chkEnableFirewallRule.Text = "Enable firewall rule for RDP traffic"
        Me.chkEnableFirewallRule.UseVisualStyleBackColor = True
        '
        'EnableRDPForm
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(292, 166)
        Me.Controls.Add(Me.chkEnableFirewallRule)
        Me.Controls.Add(Me.chkEnableNLA)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "EnableRDPForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Enable RDP"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents chkEnableNLA As System.Windows.Forms.CheckBox
    Friend WithEvents chkEnableFirewallRule As System.Windows.Forms.CheckBox
End Class
