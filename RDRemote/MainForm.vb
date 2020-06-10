Public Class MainForm
    Private OSVersion As String = String.Empty
    Private RDPSettingsUInt32 As New System.Collections.Generic.Dictionary(Of String, TSClientSettingsPropertyUInt32FlagValue)
    Private Const RDPAudioMappingPropertyName As String = "AudioMapping"
    Private Const RDPClipboardMappingPropertyName As String = "ClipboardMapping"
    Private Const RDPDriveMappingPropertyName As String = "DriveMapping"
    Private Const RDPWindowsPrinterMappingPropertyName As String = "WindowsPrinterMapping"
    Private Const RDPPNPRedirectionPropertyName As String = "PNPRedirection"
    Private Const RDPLPTPortMappingPropertyName As String = "LPTPortMapping"
    Private Const RDPCOMPortMappingPropertyName As String = "COMPortMapping"
    Private Const RDPConnectPrinterAtLogonPropertyName As String = "ConnectPrinterAtLogon"
    Private Const RDPDefaultToClientPrinterPropertyName As String = "DefaultToClientPrinter"
    Private Const RDPConnectClientDrivesAtLogonPropertyName As String = "ConnectClientDrivesAtLogon"
    Private Const RDPAllowDwmPropertyName As String = "AllowDwm"

    Private RDPConnectionPolicy As RDPConnectionPolicyValues = RDPConnectionPolicyValues.Unknown
    Friend Enum RDPConnectionPolicyValues As UInt32
        <System.ComponentModel.Description("Server override")> _
        ServerOverride = 0
        <System.ComponentModel.Description("Per user")> _
        PerUser = 1
        Unknown = 2
    End Enum

    Private RDPColorDepth As RDPColorDepthValues = RDPColorDepthValues.Unknown
    Private Enum RDPColorDepthValues As UInt32
        Unknown = 0
        <System.ComponentModel.Description("8 bits per pixel")> _
        Bits8 = 1
        <System.ComponentModel.Description("15 bits per pixel")> _
        Bits15 = 2
        <System.ComponentModel.Description("16 bits per pixel")> _
        Bits16 = 3
        <System.ComponentModel.Description("24 bits per pixel")> _
        Bits24 = 4
        <System.ComponentModel.Description("32 bits per pixel")> _
        Bits32 = 5
    End Enum

    Private RDPColorDepthPolicy As RDPColorDepthPolicyValues = RDPColorDepthPolicyValues.Unknown
    Private Enum RDPColorDepthPolicyValues As UInt32
        DoNotOverrideUserPolicy = 0
        OverrideUserPolicy = 1
        Unknown = 2
    End Enum

    Private RDPMaxMonitors As UInt32 = 0
    Private RDPMaxXResolution As UInt32 = 0
    Private RDPMaxYResolution As UInt32 = 0


    Private Sub MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Inizializzazioni
        Me.Text = String.Format(Me.Text, _
            My.Application.Info.Description, _
            String.Format("{0}.{1}.{2}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build))

        Me.ResetData()
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Util.SetWaitCursor(True)

        Dim scope As System.Management.ManagementScope = Nothing
        Dim options As System.Management.ConnectionOptions = Nothing

        Dim scopeTs As System.Management.ManagementScope = Nothing

        Try
            Me.ResetData()

            Me.SetStatus("Connecting...")

            If Not String.IsNullOrEmpty(Me.txtUser.Text) Then
                options = New System.Management.ConnectionOptions()
                options.Username = Me.txtUser.Text
                options.Password = Me.txtPassword.Text

                'options.Timeout non ha effetto sul metodo Connect
                'http://msdn.microsoft.com/it-it/library/system.management.managementoptions.timeout.aspx
            End If

            scope = New System.Management.ManagementScope( _
                    "\\" & Me.txtRemoteSystem.Text & "\root\cimv2", options)
            scope.Connect()

            'Lettura info sistema operativo
            Me.LoadOS(scope)

            'Connessione WMI per TS
            scopeTs = Me.ConnectTS()

            'Lettura informazioni TS
            Me.LoadTSettings(scopeTs)
            Me.LoadTSGeneralSettings(scopeTs)
            Me.LoadTSClientSettings(scopeTs)

            Me.SetStatus("Connection successful.")
        Catch ex As Exception
            Me.SetStatus("Connection failed.")
            Util.ShowErrorException(ex, False)
        Finally
            'Rilascio risorse
            options = Nothing
            scope = Nothing
            scopeTs = Nothing
        End Try

        Util.SetWaitCursor(False)

        'http://gallery.technet.microsoft.com/scriptcenter/219216e1-16c2-4f16-8f48-8abdbffdfaf5
        'http://blogs.msdn.com/b/securitytools/archive/2009/08/21/automating-windows-firewall-settings-with-c.aspx
        'http://it.w3support.net/index.php?db=sf&id=91197

        'http://msdn.microsoft.com/en-us/library/windows/desktop/aa383799(v=VS.85).aspx#properties
    End Sub

    Private Function ConnectTS() As System.Management.ManagementScope
        Dim scopeTs As System.Management.ManagementScope = Nothing
        Dim optionsTS As System.Management.ConnectionOptions = Nothing


        If Not String.IsNullOrEmpty(Me.txtUser.Text) Then
            optionsTS = New System.Management.ConnectionOptions()
            optionsTS.Username = Me.txtUser.Text
            optionsTS.Password = Me.txtPassword.Text
            optionsTS.Authentication = Management.AuthenticationLevel.PacketPrivacy
        End If

        If Util.IsOSWS2008OrAbove(Me.OSVersion) Then
            scopeTs = New System.Management.ManagementScope( _
                "\\" & Me.txtRemoteSystem.Text & "\root\cimv2\TerminalServices", optionsTS)
        Else
            scopeTs = New System.Management.ManagementScope( _
                "\\" & Me.txtRemoteSystem.Text & "\root\cimv2", optionsTS)
        End If
        scopeTs.Connect()

        Return scopeTs
    End Function

    Public Sub ResetData()
        Me.OSVersion = String.Empty
        Me.RDPSettingsUInt32.Clear()
        Me.RDPConnectionPolicy = RDPConnectionPolicyValues.Unknown
        Me.RDPColorDepth = RDPColorDepthValues.Unknown
        Me.RDPColorDepthPolicy = RDPColorDepthPolicyValues.Unknown
        Me.RDPMaxMonitors = 0
        Me.RDPMaxXResolution = 0
        Me.RDPMaxYResolution = 0

        Me.lsvSystemInformation.Items.Clear()
        Me.lsvSystemInformation.Groups.Clear()

        Me.btnEnableRDP.Enabled = False
        Me.btnDisableRDP.Enabled = False
        Me.btnConfigureRDP.Enabled = False

        Me.lblStatus.Text = String.Empty
    End Sub

    Private Sub SetStatus(ByVal text As String)
        Me.lblStatus.Text = text
        Me.stsMain.Update()
    End Sub

    Private Sub LoadOS(ByVal scope As System.Management.ManagementScope)
        'Lettura info sistema operativo
        Dim query = "SELECT Version,Caption,CSDVersion,InstallDate,LastBootUpTime FROM Win32_OperatingSystem"
        Using os = Util.GetManagementObject(scope, query)

            If os IsNot Nothing Then
                Dim osGroup = Me.lsvSystemInformation.Groups.Add("OS", "Operating System")

                Me.OSVersion = os("Version").ToString()
                Dim osDescription = os("Caption")
                Dim csdVersion = os("CSDVersion")
                Dim osInstallDate = Util.ParseCIM(os("InstallDate").ToString())
                Dim osBootUpDate = Util.ParseCIM(os("LastBootUpTime").ToString())

                Dim item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Edition"
                item.SubItems(Me.colValue.Index).Text = String.Format("{0} {1}", _
                    osDescription, csdVersion)
                Me.lsvSystemInformation.Items.Add(item).Group = osGroup

                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Install date"
                item.SubItems(Me.colValue.Index).Text = String.Format("{0} {1}", _
                    osInstallDate.Value.ToShortDateString(), osInstallDate.Value.ToShortTimeString())
                Me.lsvSystemInformation.Items.Add(item).Group = osGroup

                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Last boot up"
                item.SubItems(Me.colValue.Index).Text = String.Format("{0} {1}", _
                    osBootUpDate.Value.ToShortDateString(), osBootUpDate.Value.ToShortTimeString())
                Me.lsvSystemInformation.Items.Add(item).Group = osGroup
            End If
        End Using
    End Sub

    Private Sub LoadTSettings(ByVal scope As System.Management.ManagementScope)
        Dim query = "SELECT AllowTSConnections, TerminalServerMode, Logons, SingleSession, LicensingType FROM Win32_TerminalServiceSetting"

        Using ts = Util.GetManagementObject(scope, query)
            If ts IsNot Nothing Then
                Dim item As ListViewItem = Nothing
                Dim tsSettingsGroup = Me.lsvSystemInformation.Groups.Add("TSSettings", "Terminal Service Settings")

                '*** AllowTSConnections
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                Select Case Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, "AllowTSConnections", "RDP connections", _
                    1, String.Empty, 0, String.Empty)
                    Case TSClientSettingsPropertyUInt32FlagValue.Enabled
                        Me.btnDisableRDP.Enabled = True
                        Me.btnConfigureRDP.Enabled = True
                    Case TSClientSettingsPropertyUInt32FlagValue.Disabled
                        Me.btnEnableRDP.Enabled = True
                End Select
                Me.lsvSystemInformation.Items.Add(item).Group = tsSettingsGroup

                '*** TerminalServerMode
                Dim tsServerMode = ts("TerminalServerMode")
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RDS Mode"

                If tsServerMode IsNot Nothing AndAlso CInt(tsServerMode) = 0 Then
                    item.SubItems(Me.colValue.Index).Text = "Remote Administration"
                ElseIf tsServerMode IsNot Nothing AndAlso CInt(tsServerMode) = 1 Then
                    item.SubItems(Me.colValue.Index).Text = "Application Server"
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsSettingsGroup

                '*** Logons
                Dim tsLogons = ts("Logons")
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RDS Sessions"
                If tsLogons IsNot Nothing AndAlso CInt(tsLogons) = 0 Then
                    item.SubItems(Me.colValue.Index).Text = "New sessions are allowed"
                ElseIf tsLogons IsNot Nothing AndAlso CInt(tsLogons) = 1 Then
                    item.SubItems(Me.colValue.Index).Text = "New sessions are not allowed"
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsSettingsGroup

                '*** SingleSession
                Dim tsSingleSession = ts("SingleSession")
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RDS Sessions per user"
                If tsSingleSession IsNot Nothing AndAlso CInt(tsSingleSession) = 0 Then
                    item.SubItems(Me.colValue.Index).Text = "More than one session is allowed per user"
                ElseIf tsSingleSession IsNot Nothing AndAlso CInt(tsSingleSession) = 1 Then
                    item.SubItems(Me.colValue.Index).Text = "Only one session is allowed per user"
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsSettingsGroup

                '*** LicensingType
                Dim tsLicensingType = ts("LicensingType")
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RD Licensing type"

                If tsLicensingType IsNot Nothing AndAlso CInt(tsLicensingType) = 0 Then
                    item.SubItems(Me.colValue.Index).Text = "Personal RD Session Host server"
                ElseIf tsLicensingType IsNot Nothing AndAlso CInt(tsLicensingType) = 1 Then
                    item.SubItems(Me.colValue.Index).Text = "Remote Desktop for Administration"
                ElseIf tsLicensingType IsNot Nothing AndAlso CInt(tsLicensingType) = 2 Then
                    item.SubItems(Me.colValue.Index).Text = "Per Device"
                ElseIf tsLicensingType IsNot Nothing AndAlso CInt(tsLicensingType) = 4 Then
                    item.SubItems(Me.colValue.Index).Text = "Per User"
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsSettingsGroup
            End If
        End Using
    End Sub

    Private Sub LoadTSGeneralSettings(ByVal scope As System.Management.ManagementScope)
        'Dim query = "SELECT UserAuthenticationRequired, SecurityLayer, Transport, TerminalProtocol, TerminalName FROM Win32_TSGeneralSetting WHERE TerminalName='RDP-Tcp'"
        'Per gestire anche le proprietà non supportate in versioni di OS precedenti occorre
        'selezionare tutte le proprietà a poi gestirne l'esistenza
        Dim query = "SELECT * FROM Win32_TSGeneralSetting WHERE TerminalName='RDP-Tcp'"

        Using ts = Util.GetManagementObject(scope, query)
            If ts IsNot Nothing Then
                Dim item As ListViewItem = Nothing
                Dim tsGeneralSettingsGroup = Me.lsvSystemInformation.Groups.Add("TSGeneralSettings", "Terminal Service General Settings")

                '*** UserAuthenticationRequired
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, "UserAuthenticationRequired", "Network Level Authentication", _
                    1, "User authentication at connection is enabled, only RDP clients 6.0 or higher are able to connect", _
                    0, "User authentication at connection is disabled")
                Me.lsvSystemInformation.Items.Add(item).Group = tsGeneralSettingsGroup

                '*** RDP Connection
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RDP Connection"
                item.SubItems(Me.colValue.Index).Text = "Transport:" & ts("Transport").ToString() '& _
                item.SubItems(Me.colValue.Index).Text &= "; Protocol:" & ts("TerminalProtocol").ToString()
                item.SubItems(Me.colValue.Index).Text &= "; Terminal:" & ts("TerminalName").ToString()
                Me.lsvSystemInformation.Items.Add(item).Group = tsGeneralSettingsGroup

                '*** SecurityLayer
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "RDP Security"

                If Me.ExistManagementObjectProperty(ts, "SecurityLayer", item) Then
                    Dim tsSecurity = ts("SecurityLayer")

                    If tsSecurity IsNot Nothing AndAlso CInt(tsSecurity) = 0 Then
                        item.SubItems(Me.colValue.Index).Text = "RDP Security Layer (native RDP encryption)"
                    ElseIf tsSecurity IsNot Nothing AndAlso CInt(tsSecurity) = 1 Then
                        item.SubItems(Me.colValue.Index).Text = "Negotiate (most secure layer supported by the client)"
                    ElseIf tsSecurity IsNot Nothing AndAlso CInt(tsSecurity) = 2 Then
                        item.SubItems(Me.colValue.Index).Text = "SSL (TLS 1.0 is used, requires the server to have an SSL-compatible certificate)"
                    ElseIf tsSecurity IsNot Nothing AndAlso CInt(tsSecurity) = 4 Then
                        item.SubItems(Me.colValue.Index).Text = "New security layer in Windows Vista"
                    End If

                    Me.lsvSystemInformation.Items.Add(item).Group = tsGeneralSettingsGroup
                End If

            End If
        End Using
    End Sub

    Private Sub LoadTSClientSettings(ByVal scope As System.Management.ManagementScope)
        'Per gestire anche le proprietà non supportate in versioni di OS precedenti occorre
        'selezionare tutte le proprietà a poi gestirne l'esistenza
        Dim query = "SELECT * FROM Win32_TSClientSetting WHERE TerminalName='RDP-Tcp'"

        Using ts = Util.GetManagementObject(scope, query)
            If ts IsNot Nothing Then
                Dim tsClientSettingsGroup = Me.lsvSystemInformation.Groups.Add("TSClientSettings", "Terminal Service Client Settings")
                Dim item As ListViewItem = Nothing
                Dim valueUInt32Flag As TSClientSettingsPropertyUInt32FlagValue = Nothing

                '*** ConnectionPolicy
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Connection policy"
                Dim tsConnectionPolicy = ts("ConnectionPolicy")

                If tsConnectionPolicy IsNot Nothing AndAlso CInt(tsConnectionPolicy) = RDPConnectionPolicyValues.ServerOverride Then
                    item.SubItems(Me.colValue.Index).Text = Util.GetEnumDescription(GetType(RDPConnectionPolicyValues), RDPConnectionPolicyValues.ServerOverride)
                    item.ToolTipText = "The user's connection settings are overridden by the server"
                    Me.RDPConnectionPolicy = RDPConnectionPolicyValues.ServerOverride
                ElseIf tsConnectionPolicy IsNot Nothing AndAlso CInt(tsConnectionPolicy) = RDPConnectionPolicyValues.PerUser Then
                    item.SubItems(Me.colValue.Index).Text = Util.GetEnumDescription(GetType(RDPConnectionPolicyValues), RDPConnectionPolicyValues.PerUser)
                    item.ToolTipText = "The user's connection settings are in effect"
                    Me.RDPConnectionPolicy = RDPConnectionPolicyValues.PerUser
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** AllowDwm - PolicySourceAllowDwm
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPAllowDwmPropertyName, "Desktop composition", _
                    1, String.Empty, 0, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceAllowDwm", _
                    New UInt32() {0, 1}, _
                    New String() {"Server", "Group policy"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPAllowDwmPropertyName, valueUInt32Flag)

                '*** ColorDepth - PolicySourceColorDepth
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Color depth"
                Dim tsColorDepth = ts("ColorDepth")

                If tsColorDepth IsNot Nothing Then
                    item.SubItems(Me.colValue.Index).Text = Util.GetEnumDescription( _
                        GetType(RDPColorDepthValues), tsColorDepth)
                    Me.RDPColorDepth = DirectCast(tsColorDepth, RDPColorDepthValues)
                End If

                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceColorDepth", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** ColorDepthPolicy
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Color depth policy"
                Dim tsColorDepthPolicy = ts("ColorDepthPolicy")

                If tsColorDepthPolicy IsNot Nothing AndAlso CInt(tsColorDepthPolicy) = 0 Then
                    item.SubItems(Me.colValue.Index).Text = "Override the user's policy"
                    Me.RDPColorDepthPolicy = RDPColorDepthPolicyValues.OverrideUserPolicy
                ElseIf tsColorDepthPolicy IsNot Nothing AndAlso CInt(tsColorDepthPolicy) = 1 Then
                    item.SubItems(Me.colValue.Index).Text = "Do not override the user's policy"
                    Me.RDPColorDepthPolicy = RDPColorDepthPolicyValues.DoNotOverrideUserPolicy
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** MaxMonitors - PolicySourceMaxMonitors
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Max monitors"
                If Me.ExistManagementObjectProperty(ts, "MaxMonitors", item) Then
                    Dim tsMaxMonitors = ts("MaxMonitors")

                    If tsMaxMonitors IsNot Nothing Then
                        item.SubItems(Me.colValue.Index).Text = tsMaxMonitors.ToString()
                        Me.RDPMaxMonitors = System.Convert.ToUInt32(tsMaxMonitors)
                    End If

                    Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                        ts, "PolicySourceMaxMonitors", _
                        New UInt32() {0, 1, 2}, _
                        New String() {"Server", "Group policy", "Default"})
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** MaxXResolution - MaxYResolution - PolicySourceMaxResolution
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                item.SubItems(Me.colElement.Index).Text = "Max resolution"
                item.ToolTipText = "The maximum X resolution and the maximum Y resolution supported by the server"
                If Me.ExistManagementObjectProperty(ts, "MaxXResolution", item) Then
                    Dim tsMaxXResolution = ts("MaxXResolution")

                    If tsMaxXResolution IsNot Nothing Then
                        item.SubItems(Me.colValue.Index).Text = "X=" & tsMaxXResolution.ToString() & " "
                        Me.RDPMaxXResolution = System.Convert.ToUInt32(tsMaxXResolution)
                    End If

                    Dim tsMaxYResolution = ts("MaxYResolution")

                    If tsMaxYResolution IsNot Nothing Then
                        item.SubItems(Me.colValue.Index).Text &= "Y=" & tsMaxYResolution.ToString()
                        Me.RDPMaxYResolution = System.Convert.ToUInt32(tsMaxYResolution)
                    End If

                    Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                        ts, "PolicySourceMaxResolution", _
                        New UInt32() {0, 1, 2}, _
                        New String() {"Server", "Group policy", "Default"})
                End If
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** AudioMapping - PolicySourceAudioMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPAudioMappingPropertyName, "Audio mapping", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceAudioMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPAudioMappingPropertyName, valueUInt32Flag)

                '*** AudioCaptureRedir - PolicySourceAudioMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, "AudioCaptureRedir", "Audio capture redirection", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceAudioCaptureRedir", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup

                '*** ClipboardMapping - PolicySourceClipboardMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPClipboardMappingPropertyName, "Clipboard mapping", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceClipboardMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPClipboardMappingPropertyName, valueUInt32Flag)

                '*** ConnectClientDrivesAtLogon
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPConnectClientDrivesAtLogonPropertyName, "Connect drives at Logon", _
                    1, "Drives will be automatically connected", _
                    0, "Drives will not be automatically connected")
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPConnectClientDrivesAtLogonPropertyName, valueUInt32Flag)

                '*** DriveMapping - PolicySourceDriveMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPDriveMappingPropertyName, "Drive mapping", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceDriveMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPDriveMappingPropertyName, valueUInt32Flag)

                '*** WindowsPrinterMapping 
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPWindowsPrinterMappingPropertyName, "Windows printer mapping", _
                    0, String.Empty, _
                    1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceWindowsPrinterMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPWindowsPrinterMappingPropertyName, valueUInt32Flag)

                '*** ConnectPrinterAtLogon
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPConnectPrinterAtLogonPropertyName, "Connect printer at logon", _
                    1, "Local printers will be automatically connected during the logon process", _
                    0, "Local printers will not be automatically connected during the logon process")
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPConnectPrinterAtLogonPropertyName, valueUInt32Flag)

                '*** DefaultToClientPrinter - PolicySourceDefaultToClientPrinter
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPDefaultToClientPrinterPropertyName, "Default to client printer", _
                    1, "Print jobs are to be automatically sent to the client's local printer", _
                    0, "Print jobs are not to be automatically sent to the client's local printer")
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceDefaultToClientPrinter", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPDefaultToClientPrinterPropertyName, valueUInt32Flag)

                '*** LPTPortMapping - PolicySourceLPTPortMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPLPTPortMappingPropertyName, "LPT port mapping", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceLPTPortMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPLPTPortMappingPropertyName, valueUInt32Flag)

                '*** COMPortMapping - PolicySourceCOMPortMapping
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPCOMPortMappingPropertyName, "COM port mapping", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceCOMPortMapping", _
                    New UInt32() {0, 1, 2}, _
                    New String() {"Server", "Group policy", "Default"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPCOMPortMappingPropertyName, valueUInt32Flag)

                '*** PNPRedirection - PolicySourcePNPRedirection
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                valueUInt32Flag = Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, RDPPNPRedirectionPropertyName, "Plug and Play redirection", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourcePNPRedirection", _
                    New UInt32() {0, 1}, _
                    New String() {"Server", "Group policy"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
                Me.RDPSettingsUInt32.Add(RDPPNPRedirectionPropertyName, valueUInt32Flag)

                '*** VideoPlaybackRedir - PolicySourceVideoPlaybackRedir
                item = Util.CreateListViewItem(Me.lsvSystemInformation)
                Me.ReadTSClientSettingsPropertyUInt32Flag(item, _
                    ts, "VideoPlaybackRedir", "Video playback redirection", _
                    0, String.Empty, 1, String.Empty)
                Me.ReadTSClientSettingsPropertyPolicySourceUInt32(item, _
                    ts, "PolicySourceVideoPlaybackRedir", _
                    New UInt32() {0, 1}, _
                    New String() {"Server", "Group policy"})
                Me.lsvSystemInformation.Items.Add(item).Group = tsClientSettingsGroup
            End If
        End Using
    End Sub

#Region "Metodo ExistManagementObjectProperty"
    Private Overloads Function ExistManagementObjectProperty(ByVal managementObject As System.Management.ManagementObject, ByVal name As String) As Boolean
        Dim returnValue As Boolean = False
        For Each p In managementObject.Properties
            If p.Name.ToLower() = name.ToLower() Then
                returnValue = True
                Exit For
            End If
        Next

        Return returnValue
    End Function

    Private Overloads Function ExistManagementObjectProperty(ByVal managementObject As System.Management.ManagementObject, ByVal name As String, ByVal item As ListViewItem) As Boolean
        Dim returnValue As Boolean = Me.ExistManagementObjectProperty(managementObject, name)

        If Not returnValue Then
            item.UseItemStyleForSubItems = False
            item.SubItems(Me.colValue.Index).ForeColor = Drawing.Color.Blue
            item.SubItems(Me.colValue.Index).Text = "Not supported"
        End If

        Return returnValue
    End Function
#End Region

    Friend Enum TSClientSettingsPropertyUInt32FlagValue As Integer
        Unsupported = -1
        Disabled = 0
        Enabled = 1
        Unknow = 2
    End Enum

    Private Function ReadTSClientSettingsPropertyUInt32Flag(ByVal item As ListViewItem, _
        ByVal tscs As System.Management.ManagementObject, ByVal name As String, ByVal description As String, _
        ByVal enableValue As UInt32, ByVal enableToolTip As String, _
        ByVal disablevalue As UInt32, ByVal disableToolTip As String) As TSClientSettingsPropertyUInt32FlagValue
        Dim returnValue As TSClientSettingsPropertyUInt32FlagValue = TSClientSettingsPropertyUInt32FlagValue.Unsupported

        item.SubItems(Me.colElement.Index).Text = description

        Dim supported As Boolean = Me.ExistManagementObjectProperty(tscs, name, item)

        If supported Then
            item.UseItemStyleForSubItems = False

            For Each p In tscs.Properties
                Dim s = p.Name
                s = s
            Next


            If tscs(name) IsNot Nothing Then
                Dim value = System.Convert.ToUInt32(tscs(name))

                If value = enableValue Then
                    item.SubItems(Me.colValue.Index).Text = "Enabled"
                    item.SubItems(Me.colValue.Index).ForeColor = Drawing.Color.Green
                    If Not String.IsNullOrEmpty(enableToolTip) Then
                        item.ToolTipText = enableToolTip
                    End If
                    returnValue = TSClientSettingsPropertyUInt32FlagValue.Enabled
                ElseIf value = disablevalue Then
                    item.SubItems(Me.colValue.Index).Text = "Disabled"
                    item.SubItems(Me.colValue.Index).ForeColor = Drawing.Color.Red
                    If Not String.IsNullOrEmpty(disableToolTip) Then
                        item.ToolTipText = disableToolTip
                    End If
                    returnValue = TSClientSettingsPropertyUInt32FlagValue.Disabled
                End If
            End If
        End If

        Return returnValue
    End Function

    Private Sub ReadTSClientSettingsPropertyPolicySourceUInt32(ByVal item As ListViewItem, _
        ByVal tscs As System.Management.ManagementObject, ByVal name As String, ByVal values() As UInt32, ByVal descriptions() As String)
        Dim supported As Boolean = Me.ExistManagementObjectProperty(tscs, name)

        If supported Then
            Dim value = System.Convert.ToUInt32(tscs(name))

            For i = 0 To values.Length - 1
                If value = values(i) Then
                    If Not String.IsNullOrEmpty(item.SubItems(Me.colValue.Index).Text) Then
                        item.SubItems(Me.colValue.Index).Text &= " "
                    End If
                    item.SubItems(Me.colValue.Index).Text &= "(Policy source: " & descriptions(i) & ")"
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnEnableRDP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnableRDP.Click
        Using frm As New EnableRDPForm
            If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Util.SetWaitCursor(True)

                Dim scopeTs As System.Management.ManagementScope = Nothing
                Dim query As String = String.Empty

                Try
                    Me.SetStatus("Enable RDP in progress...")

                    scopeTs = Me.ConnectTS()

                    'Impostazione NLA
                    query = "SELECT * FROM Win32_TSGeneralSetting WHERE TerminalName='RDP-tcp'"

                    Using ts = Util.GetManagementObject(scopeTs, query)
                        Using inParams = ts.GetMethodParameters("SetUserAuthenticationRequired")
                            inParams("UserAuthenticationRequired") = IIf(frm.chkEnableNLA.Checked, 1, 0)

                            Using outParams = ts.InvokeMethod("SetUserAuthenticationRequired", inParams, Nothing)
                                If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                                    Throw New System.Exception( _
                                        String.Format("Failed enable NLA [Error:{0}].", _
                                        outParams("ReturnValue")))
                                End If
                            End Using
                        End Using
                    End Using

                    'Abilitazione Connessioni RDP
                    query = "SELECT * FROM Win32_TerminalServiceSetting"

                    Using ts = Util.GetManagementObject(scopeTs, query)
                        Using inParams = ts.GetMethodParameters("SetAllowTSConnections")
                            inParams("AllowTSConnections") = 1
                            inParams("ModifyFirewallException") = IIf(frm.chkEnableFirewallRule.Checked, 1, 0)

                            Using outParams = ts.InvokeMethod("SetAllowTSConnections", inParams, Nothing)
                                If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                                    Throw New System.Exception( _
                                        String.Format("Failed enable RDP [Error:{0}].", _
                                        outParams("ReturnValue")))
                                End If
                            End Using
                        End Using

                    End Using

                    Util.ShowMessage("RDP enabled successfully.")

                    Me.btnConnect.PerformClick()
                Catch ex As Exception
                    Me.SetStatus("Enabling RDP failed.")
                    Util.ShowErrorException(ex, False)
                Finally
                    scopeTs = Nothing
                End Try

                Util.SetWaitCursor(False)
            End If
        End Using
    End Sub

    Private Sub btnDisableRDP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisableRDP.Click
        Using frm As New DisableRDPForm
            If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Util.SetWaitCursor(True)

                Dim scopeTs As System.Management.ManagementScope = Nothing

                Try
                    Me.SetStatus("Disable RDP in progress...")

                    scopeTs = Me.ConnectTS()

                    Dim query = "SELECT * FROM Win32_TerminalServiceSetting"

                    Using ts = Util.GetManagementObject(scopeTs, query)
                        Using inParams = ts.GetMethodParameters("SetAllowTSConnections")
                            inParams("AllowTSConnections") = 0
                            inParams("ModifyFirewallException") = IIf(frm.chkDisableFirewallRule.Checked, 1, 0)
                            'inParams("ModifyFirewallException") = 0

                            Using outParams = ts.InvokeMethod("SetAllowTSConnections", inParams, Nothing)
                                If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                                    Throw New System.Exception( _
                                        String.Format("Failed disable RDP [Error:{0}].", _
                                        outParams("ReturnValue")))
                                End If
                            End Using
                        End Using
                    End Using

                    Util.ShowMessage("RDP disabled successfully.")

                    'Refresh connessione
                    Me.btnConnect.PerformClick()
                Catch ex As Exception
                    Me.SetStatus("Disabling RDP failed.")
                    Util.ShowErrorException(ex, False)
                Finally
                    scopeTs = Nothing
                End Try

                Util.SetWaitCursor(False)
            End If
        End Using

    End Sub

    Private Sub btnConfigureRDP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfigureRDP.Click
        Using frm As New ConfigureRDPForm
            Dim scopeTs As System.Management.ManagementScope = Nothing

            'ClipboardMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkClipboardMapping, _
                RDPClipboardMappingPropertyName)
            'AudioMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkAudioMapping, _
                RDPAudioMappingPropertyName)
            'ConnectClientDrivesAtLogon
            Me.InitializeConfigurationFormCheckBox(frm.chkConnectClientDrivesAtLogon, _
               RDPConnectClientDrivesAtLogonPropertyName)
            'DriveMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkDriveMapping, _
               RDPDriveMappingPropertyName)
            'PNPRedirection
            Me.InitializeConfigurationFormCheckBox(frm.chkPNPRedirection, _
               RDPPNPRedirectionPropertyName)
            'WindowsPrinterMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkWindowsPrinterMapping, _
               RDPWindowsPrinterMappingPropertyName)
            'ConnectPrinterAtLogon
            Me.InitializeConfigurationFormCheckBox(frm.chkConnectPrinterAtLogon, _
               RDPConnectPrinterAtLogonPropertyName)
            'DefaultToClientPrinter
            Me.InitializeConfigurationFormCheckBox(frm.chkDefaultToClientPrinter, _
               RDPDefaultToClientPrinterPropertyName)
            'LPTPortMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkLPTPortMapping, _
               RDPLPTPortMappingPropertyName)
            'COMPortMapping
            Me.InitializeConfigurationFormCheckBox(frm.chkCOMPortMapping, _
               RDPCOMPortMappingPropertyName)

            'ConnectionPolicy
            For Each v As RDPConnectionPolicyValues In System.Enum.GetValues(GetType(RDPConnectionPolicyValues))
                If v <> RDPConnectionPolicyValues.Unknown Then
                    frm.cmbConnectionPolicy.Items.Add(Util.GetEnumDescription( _
                        GetType(RDPConnectionPolicyValues), v))
                End If
            Next
            If Me.RDPConnectionPolicy <> RDPConnectionPolicyValues.Unknown Then _
                frm.cmbConnectionPolicy.SelectedIndex = CInt(Me.RDPConnectionPolicy)

            'Se una delle 3 proprietà non è supportata non si consente
            'la modidifica perchè vanno impostate insieme
            frm.cmbConnectionPolicy.Enabled = _
                Me.RDPSettingsUInt32(RDPDefaultToClientPrinterPropertyName) <> TSClientSettingsPropertyUInt32FlagValue.Unsupported AndAlso _
                Me.RDPSettingsUInt32(RDPConnectPrinterAtLogonPropertyName) <> TSClientSettingsPropertyUInt32FlagValue.Unsupported AndAlso _
                Me.RDPSettingsUInt32(RDPConnectClientDrivesAtLogonPropertyName) <> TSClientSettingsPropertyUInt32FlagValue.Unsupported

            'ColorDepthPolicy
            If Me.RDPColorDepthPolicy = RDPColorDepthPolicyValues.Unknown Then
                frm.chkColorDepthPolicy.Enabled = False
            Else
                frm.chkColorDepthPolicy.Checked = _
                    Me.RDPColorDepthPolicy = RDPColorDepthPolicyValues.OverrideUserPolicy
            End If

            'ColorDepth
            For Each v As RDPColorDepthValues In System.Enum.GetValues(GetType(RDPColorDepthValues))
                If v <> RDPColorDepthValues.Unknown Then
                    frm.cmbColorDepth.Items.Add(Util.GetEnumDescription( _
                        GetType(RDPColorDepthValues), v))
                End If
            Next

            If Me.RDPColorDepth <> RDPColorDepthValues.Unknown Then _
                frm.cmbColorDepth.SelectedIndex = CInt(Me.RDPColorDepth - 1)
            frm.cmbColorDepth.Enabled = frm.chkColorDepthPolicy.Enabled AndAlso _
                frm.chkColorDepthPolicy.Checked

            'AllowDwm
            Me.InitializeConfigurationFormCheckBox(frm.chkAllowDwm, _
               RDPAllowDwmPropertyName)

            'MaxMonitors
            If Me.RDPMaxMonitors > 0 Then
                frm.nudMaxMonitors.Value = Me.RDPMaxMonitors
            Else
                frm.nudMaxMonitors.Enabled = False
            End If

            'MaxXResolution
            If Me.RDPMaxXResolution > 0 Then
                frm.nudMaxXResolution.Value = Me.RDPMaxXResolution
            Else
                frm.nudMaxXResolution.Enabled = False
            End If

            'MaxYResolution
            If Me.RDPMaxYResolution > 0 Then
                frm.nudMaxYResolution.Value = Me.RDPMaxYResolution
            Else
                frm.nudMaxYResolution.Enabled = False
            End If

            If frm.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                Util.SetWaitCursor(True)
                Me.SetStatus("RDP set configuration in progress...")

                Try
                    scopeTs = Me.ConnectTS()
                    Dim query = "SELECT * FROM Win32_TSClientSetting WHERE TerminalName='RDP-Tcp'"

                    Using ts = Util.GetManagementObject(scopeTs, query)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkClipboardMapping)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkAudioMapping)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkDriveMapping)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkPNPRedirection)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkWindowsPrinterMapping)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkLPTPortMapping)
                        Me.ConfigureTSClientSettingBySetClientProperty(ts, frm.chkCOMPortMapping)

                        Me.ConfigureTSClientSettingByConnectionSettings(ts, _
                            frm.cmbConnectionPolicy, _
                            frm.chkConnectClientDrivesAtLogon, _
                            frm.chkConnectPrinterAtLogon, _
                            frm.chkDefaultToClientPrinter)

                        Me.ConfigureTSClientSettingBySetColorDepthPolicy(ts, _
                            frm.chkColorDepthPolicy)

                        Me.ConfigureTSClientSettingBySetColorDepth(ts, _
                            frm.cmbColorDepth)

                        Me.ConfigureTSClientSettingBySetAllowDwm(ts, _
                          frm.chkAllowDwm)

                        Me.ConfigureTSClientSettingBySetMaxMonitors(ts, _
                            frm.nudMaxMonitors)

                        Me.ConfigureTSClientSettingBySetMaxXResolution(ts, _
                            frm.nudMaxXResolution)

                        Me.ConfigureTSClientSettingBySetMaxYResolution(ts, _
                            frm.nudMaxYResolution)
                    End Using

                    Util.ShowMessage("RDP set configuration successfully.")

                    'Refresh connessione
                    Me.btnConnect.PerformClick()
                Catch ex As Exception
                    Me.SetStatus("Set RDP configuration failed.")
                    Util.ShowErrorException(ex, False)
                End Try

                Util.SetWaitCursor(False)
            End If
        End Using
    End Sub

    Private Sub InitializeConfigurationFormCheckBox(ByVal checkBox As CheckBox, ByVal propertyName As String)
        checkBox.Tag = propertyName

        Select Case Me.RDPSettingsUInt32(propertyName)
            Case MainForm.TSClientSettingsPropertyUInt32FlagValue.Enabled
                checkBox.Checked = True
            Case MainForm.TSClientSettingsPropertyUInt32FlagValue.Disabled
                checkBox.Checked = False
            Case Else
                checkBox.Enabled = False
        End Select
    End Sub

    Private Sub ConfigureTSClientSettingBySetClientProperty(ByVal ts As System.Management.ManagementObject, ByVal checkBox As CheckBox)
        If checkBox.Enabled Then
            Using inParams = ts.GetMethodParameters("SetClientProperty")
                inParams("PropertyName") = checkBox.Tag
                inParams("Value") = IIf(checkBox.Checked, 0, 1)

                Using outParams = ts.InvokeMethod("SetClientProperty", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable {0} [Error:{1}].", _
                            checkBox.Text, outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingByConnectionSettings(ByVal ts As System.Management.ManagementObject, _
        ByVal comboBoxConnectionPolicy As ComboBox, _
        ByVal checkBoxConnectClientDrivesAtLogon As CheckBox, _
        ByVal checkBoxConnectPrinterAtLogon As CheckBox, _
        ByVal checkBoxDefaultToClientPrinter As CheckBox)


        If comboBoxConnectionPolicy.Enabled Then
            ts.Item("ConnectionPolicy") = comboBoxConnectionPolicy.SelectedIndex
            ts.Put()
        End If

        If comboBoxConnectionPolicy.Enabled AndAlso _
            comboBoxConnectionPolicy.SelectedIndex = RDPConnectionPolicyValues.ServerOverride AndAlso _
            checkBoxConnectClientDrivesAtLogon.Enabled AndAlso _
            checkBoxConnectPrinterAtLogon.Enabled AndAlso _
            checkBoxDefaultToClientPrinter.Enabled Then
            Using inParams = ts.GetMethodParameters("ConnectionSettings")
                inParams("ConnectClientDrivesAtLogon") = IIf(checkBoxConnectClientDrivesAtLogon.Checked, 1, 0)
                inParams("ConnectPrinterAtLogon") = IIf(checkBoxConnectPrinterAtLogon.Checked, 1, 0)
                inParams("DefaultToClientPrinter") = IIf(checkBoxDefaultToClientPrinter.Checked, 1, 0)

                Using outParams = ts.InvokeMethod("ConnectionSettings", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception(
                            String.Format("Failed enable ConnectClientDrivesAtLogon, ConnectPrinterAtLogon, DefaultToClientPrinter [Error:{0}].",
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetAllowDwm(ByVal ts As System.Management.ManagementObject, ByVal checkBox As CheckBox)
        If checkBox.Enabled Then
            Using inParams = ts.GetMethodParameters("SetAllowDwm")
                inParams("AllowDwm") = IIf(checkBox.Checked, 1, 0)

                Using outParams = ts.InvokeMethod("SetAllowDwm", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable AllowDwm [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetColorDepth(ByVal ts As System.Management.ManagementObject, ByVal comboBox As ComboBox)
        If comboBox.Enabled AndAlso comboBox.SelectedItem IsNot Nothing Then
            Using inParams = ts.GetMethodParameters("SetColorDepth")
                inParams("ColorDepth") = comboBox.SelectedIndex + 1

                Using outParams = ts.InvokeMethod("SetColorDepth", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable SetColorDepth [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetColorDepthPolicy(ByVal ts As System.Management.ManagementObject, ByVal checkBox As CheckBox)
        If checkBox.Enabled Then
            Using inParams = ts.GetMethodParameters("SetColorDepthPolicy")
                inParams("ColorDepthPolicy") = IIf(checkBox.Checked, 0, 1)

                Using outParams = ts.InvokeMethod("SetColorDepthPolicy", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable SetColorDepthPolicy [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetMaxMonitors(ByVal ts As System.Management.ManagementObject, ByVal numericUpDown As NumericUpDown)
        If numericUpDown.Enabled Then
            Using inParams = ts.GetMethodParameters("SetMaxMonitors")
                inParams("MaxMonitors") = numericUpDown.Value

                Using outParams = ts.InvokeMethod("SetMaxMonitors", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable SetMaxMonitors [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetMaxXResolution(ByVal ts As System.Management.ManagementObject, ByVal numericUpDown As NumericUpDown)
        If numericUpDown.Enabled Then
            Using inParams = ts.GetMethodParameters("SetMaxXResolution")
                inParams("MaxXResolution") = numericUpDown.Value

                Using outParams = ts.InvokeMethod("SetMaxXResolution", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable SetMaxXResolution [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub

    Private Sub ConfigureTSClientSettingBySetMaxYResolution(ByVal ts As System.Management.ManagementObject, ByVal numericUpDown As NumericUpDown)
        If numericUpDown.Enabled Then
            Using inParams = ts.GetMethodParameters("SetMaxYResolution")
                inParams("MaxYResolution") = numericUpDown.Value

                Using outParams = ts.InvokeMethod("SetMaxYResolution", inParams, Nothing)
                    If System.Convert.ToUInt32(outParams("ReturnValue")) <> 0 Then
                        Throw New System.Exception( _
                            String.Format("Failed enable SetMaxYResolution [Error:{0}].", _
                            outParams("ReturnValue")))
                    End If
                End Using
            End Using
        End If
    End Sub


    Private Sub lsvSystemInformation_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lsvSystemInformation.MouseMove
        If Me.lsvSystemInformation.Items.Count = 0 Then Return

        Dim ht = Me.lsvSystemInformation.HitTest(e.Location)

        If ht.Item IsNot Nothing AndAlso _
            Not String.IsNullOrEmpty(ht.Item.ToolTipText) AndAlso _
            ht.SubItem IsNot Nothing AndAlso _
            ht.Item.SubItems(0) IsNot ht.SubItem Then


            Me.tltListView.Show(ht.Item.ToolTipText, _
                Me.lsvSystemInformation, _
                e.Location.X, _
                ht.SubItem.Bounds.Bottom + ht.SubItem.Bounds.Height)
        Else
            Me.tltListView.Hide(Me.lsvSystemInformation)
        End If
    End Sub

End Class
