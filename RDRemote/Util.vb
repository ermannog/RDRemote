Public NotInheritable Class Util
    Private Sub New()
        MyBase.New()
    End Sub

    Public Shared Function SetWaitCursor(ByVal state As Boolean) As Boolean
        Dim waitState As Boolean

        waitState = System.Windows.Forms.Cursor.Current Is System.Windows.Forms.Cursors.WaitCursor

        If state Then
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Else
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        End If

        Return waitState
    End Function

#Region "Gestione Enum"
    Public Shared Function GetEnumDescription(ByVal enumType As System.Type, ByVal value As Object) As String
        Dim description As String = GetEnumDescriptionAttributeValue(enumType, value)

        If String.IsNullOrEmpty(description) Then
            description = System.Enum.GetName(enumType, value)
        End If

        Return description
    End Function

    Public Shared Function GetEnumDescriptionAttributeValue(ByVal enumType As System.Type, ByVal value As Object) As String
        Dim name As String = System.Enum.GetName(enumType, value)

        Dim field As System.Reflection.FieldInfo = _
            enumType.GetField(System.Enum.GetName(enumType, value), _
                Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)

        Dim description As String = String.Empty
        Dim fieldDescriptions() As Object = _
            field.GetCustomAttributes( _
                GetType(System.ComponentModel.DescriptionAttribute), False)
        If fieldDescriptions.Length > 0 Then
            description = DirectCast(fieldDescriptions(0),  _
                System.ComponentModel.DescriptionAttribute).Description
        End If

        Return description
    End Function
#End Region

    Public Shared Function GetExceptionMessage(ByVal message As String, ByVal exception As System.Exception) As String
        Dim text As String = message

        Dim ex As System.Exception = exception

        While ex IsNot Nothing
            'Aggiunta Message
            If Not String.IsNullOrEmpty(ex.Message) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine & ControlChars.NewLine
                text &= ex.Message
            End If

            'If showDetailsException Then
            'Aggiunta Source
            If Not String.IsNullOrEmpty(ex.Source) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine
                text &= ex.GetType().ToString()
            End If

            'Aggiunta Error code
            Dim lastWin32Error = GetLastWin32Error(String.Empty)
            If Not String.IsNullOrEmpty(lastWin32Error) Then
                text &= ControlChars.NewLine & ControlChars.NewLine
                text &= "Last Win32 Error: " & lastWin32Error
            End If

            'Aggiunta StackTrace
            If Not String.IsNullOrEmpty(ex.StackTrace) Then
                If Not String.IsNullOrEmpty(text) Then _
                    text &= ControlChars.NewLine & ControlChars.NewLine
                text &= ex.StackTrace.Trim()
            End If

            ex = ex.InnerException
        End While

        Return text
    End Function

#Region "Method ShowErrorException"
    Public Overloads Shared Sub ShowErrorException(ByVal exception As System.Exception, ByVal abort As Boolean)
        ShowError(GetExceptionMessage(String.Empty, exception), abort)
    End Sub

    Public Overloads Shared Sub ShowErrorException(ByVal message As String, ByVal exception As System.Exception, ByVal abort As Boolean)
        ShowError(GetExceptionMessage(message, exception), abort)
    End Sub
#End Region

    Public Shared Function GetLastWin32Error(ByVal message As String) As String
        Dim text As String = message

        Dim ex As New System.ComponentModel.Win32Exception( _
            System.Runtime.InteropServices.Marshal.GetLastWin32Error())


        'Aggiunta Error code
        If ex.NativeErrorCode <> 0 Then
            If Not String.IsNullOrEmpty(text) Then _
                text &= ControlChars.NewLine & ControlChars.NewLine
            text &= "Error code: " & Hex(ex.NativeErrorCode) & " (" & ex.Message & ")"
        End If

        Return text
    End Function

#Region "Method ShowLastWin32Error"
    Public Overloads Shared Sub ShowLastWin32Error(ByVal abort As Boolean)
        ShowLastWin32Error(String.Empty, abort)
    End Sub

    Public Overloads Shared Sub ShowLastWin32Error(ByVal message As String, ByVal abort As Boolean)

        ShowMessage(GetLastWin32Error(message), _
            "Error", _
            System.Windows.Forms.MessageBoxIcon.Stop)

        If abort Then
            System.Environment.Exit(0)
        End If
    End Sub
#End Region

#Region "Method ShowMessage"
    Public Overloads Shared Function ShowMessage(ByVal text As String) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, ByVal title As String) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, title, System.Windows.Forms.MessageBoxIcon.None)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                ByVal icon As System.Windows.Forms.MessageBoxIcon) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty, icon)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                                  ByVal title As String, _
                                  ByVal icon As System.Windows.Forms.MessageBoxIcon) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, title, System.Windows.Forms.MessageBoxButtons.OK, icon, System.Windows.Forms.MessageBoxDefaultButton.Button1)
    End Function

    Public Overloads Shared Function ShowMessage(ByVal text As String, _
                                  ByVal title As String, _
                                  ByVal buttons As System.Windows.Forms.MessageBoxButtons, _
                                  ByVal icon As System.Windows.Forms.MessageBoxIcon, _
                                  ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        ShowMessage = System.Windows.Forms.MessageBox.Show(text, _
            My.Application.Info.Title & " " & title, _
            buttons, icon, defaultButton)
    End Function
#End Region

#Region "Metodo Question"
    Public Overloads Shared Function ShowQuestion(ByVal text As String) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, System.Windows.Forms.MessageBoxButtons.YesNo)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, System.Windows.Forms.MessageBoxButtons.YesNo, defaultButton)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, _
                                    ByVal buttons As System.Windows.Forms.MessageBoxButtons) As System.Windows.Forms.DialogResult
        Return ShowQuestion(text, buttons, System.Windows.Forms.MessageBoxDefaultButton.Button2)
    End Function

    Public Overloads Shared Function ShowQuestion(ByVal text As String, _
                                              ByVal buttons As System.Windows.Forms.MessageBoxButtons, _
                                              ByVal defaultButton As System.Windows.Forms.MessageBoxDefaultButton) As System.Windows.Forms.DialogResult
        Return ShowMessage(text, String.Empty, buttons, System.Windows.Forms.MessageBoxIcon.Question, defaultButton)
    End Function
#End Region

    Public Shared Sub ShowError(ByVal message As String, ByVal abort As Boolean)
        ShowMessage(message, "Error", System.Windows.Forms.MessageBoxIcon.Stop)

        If abort Then
            System.Environment.Exit(0)
        End If
    End Sub

    Public Shared Function GetManagementObject(ByVal scope As System.Management.ManagementScope, ByVal queryText As String) As System.Management.ManagementObject
        Dim obj As System.Management.ManagementObject = Nothing

        Using collection = Util.GetManagementObjectCollection(scope, queryText)

            '*** Per test ***
            'Dim h = collection.Count
            'For Each k In collection
            '    Dim t As String = String.Empty

            '    For Each p In k.Properties
            '        If p.Value IsNot Nothing Then
            '            t &= p.Name & "=" & p.Value.ToString() & ControlChars.NewLine
            '        End If
            '    Next
            'Next


            If collection IsNot Nothing AndAlso _
                collection.Count > 0 Then

                For Each i In collection
                    obj = TryCast(i, System.Management.ManagementObject)
                    Exit For
                Next
            End If
        End Using

        Return obj
    End Function

    Public Shared Function GetManagementObjectCollection(ByVal scope As System.Management.ManagementScope, ByVal queryText As String) As System.Management.ManagementObjectCollection
        'How to Perform a Synchronous Query by Using System.Management
        'http://msdn.microsoft.com/en-us/library/cc146429.aspx

        Dim collection As System.Management.ManagementObjectCollection = Nothing

        Dim query As New System.Management.ObjectQuery(queryText)
        Using searcher As New System.Management.ManagementObjectSearcher(scope, query)
            collection = TryCast(searcher.Get(), System.Management.ManagementObjectCollection)
        End Using
        query = Nothing

        Return collection
    End Function

    Public Shared Function ParseCIM(ByVal [date] As String) As System.Nullable(Of DateTime)
        'datetime object to store the return value
        Dim parsed As System.Nullable(Of DateTime) = Nothing

        'check date integrity
        If [date] IsNot Nothing AndAlso [date].IndexOf("."c) <> -1 Then
            'obtain the date with miliseconds
            Dim newDate As String = [date].Substring(0, [date].IndexOf("."c) + 4)

            'check the lenght
            If newDate.Length = 18 Then
                'extract each date component
                Dim y As Integer = Convert.ToInt32(newDate.Substring(0, 4))
                Dim m As Integer = Convert.ToInt32(newDate.Substring(4, 2))
                Dim d As Integer = Convert.ToInt32(newDate.Substring(6, 2))
                Dim h As Integer = Convert.ToInt32(newDate.Substring(8, 2))
                Dim mm As Integer = Convert.ToInt32(newDate.Substring(10, 2))
                Dim s As Integer = Convert.ToInt32(newDate.Substring(12, 2))
                Dim ms As Integer = Convert.ToInt32(newDate.Substring(15, 3))

                'compose the new datetime object
                parsed = New DateTime(y, m, d, h, mm, s, ms)
            End If
        End If

        Return parsed
    End Function

    Public Shared Function CreateListViewItem(ByVal listView As System.Windows.Forms.ListView) As System.Windows.Forms.ListViewItem
        Dim item As New System.Windows.Forms.ListViewItem

        For Each column As System.Windows.Forms.ColumnHeader In listView.Columns
            item.SubItems.Add(String.Empty)
        Next

        Return item
    End Function

    Public Shared Function IsOSWS2008R2SP1OrAbove(ByVal osVersion As String) As Boolean
        If String.IsNullOrEmpty(osVersion) Then Return False

        Return New System.Version(osVersion).CompareTo( _
            New System.Version("6.1.7601")) >= 0
    End Function

    Public Shared Function IsOSWS2008OrAbove(ByVal osVersion As String) As Boolean
        If String.IsNullOrEmpty(osVersion) Then Return False

        Return New System.Version(osVersion).CompareTo( _
            New System.Version("6.0.6001")) >= 0
    End Function

#Region "Gestione Licenza"
    Public Shared Function CheckEulaAccepted(ByVal companyName As String, ByVal productName As String) As Boolean
        Const EulaAcceptedValueName As String = "EulaAccepted"
        Dim registryKey As String = String.Format("Software\{0}\{1}", _
            companyName, productName)

        Dim key = My.Computer.Registry.CurrentUser.OpenSubKey(registryKey, True)
        Dim value As Object = Nothing

        If key IsNot Nothing Then
            value = key.GetValue(EulaAcceptedValueName)
        End If

        If key Is Nothing OrElse _
            value Is Nothing OrElse _
            String.IsNullOrEmpty(value.ToString()) OrElse _
            value.ToString <> "1" Then

            'Visualizzazione Dialog
            Using frm As New LicenseForm
                If frm.ShowDialog() <> DialogResult.OK Then
                    Return False
                End If
            End Using

            'Creazione Key
            If key Is Nothing Then
                key = My.Computer.Registry.CurrentUser.CreateSubKey(registryKey)
            End If

            'Impostazione Valore
            If value Is Nothing OrElse _
                String.IsNullOrEmpty(value.ToString()) OrElse _
                value.ToString <> "1" Then
                key.SetValue(EulaAcceptedValueName, 1, Microsoft.Win32.RegistryValueKind.DWord)
            End If
        End If

        Return True
    End Function
#End Region
End Class
