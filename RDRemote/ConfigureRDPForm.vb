Public Class ConfigureRDPForm

    Private Sub cmbConnectionPolicy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbConnectionPolicy.SelectedIndexChanged
        If Me.cmbConnectionPolicy.SelectedIndex = MainForm.RDPConnectionPolicyValues.ServerOverride Then
            Me.chkDefaultToClientPrinter.Enabled = True
            Me.chkConnectPrinterAtLogon.Enabled = True
            Me.chkConnectClientDrivesAtLogon.Enabled = True
        Else
            Me.chkDefaultToClientPrinter.Enabled = False
            Me.chkConnectPrinterAtLogon.Enabled = False
            Me.chkConnectClientDrivesAtLogon.Enabled = False
        End If
    End Sub


    Private Sub chkColorDepthPolicy_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkColorDepthPolicy.CheckedChanged
        Me.cmbColorDepth.Enabled = Me.chkColorDepthPolicy.Checked
    End Sub
End Class