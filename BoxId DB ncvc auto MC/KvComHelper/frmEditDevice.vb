Public Class frmEditDevice

    Public Device As String
    Public Value As String

    Private Sub frmEditDevice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbDeviceNumber.SelectedText = Device
        txtValue.Text = Value
        AxDBCommManager1.Connect()
    End Sub

    Public Sub ReadDivices()
        If String.IsNullOrEmpty(Device) Then Exit Sub

        Dim prefixt As String = Microsoft.VisualBasic.Left(Device, 1)
        If prefixt = "D" Then
            Dim numberD As String = Microsoft.VisualBasic.Mid(Device, 3)
            Value = AxDBCommManager1.ReadDevice(DATABUILDERAXLibLB.DBPlcDevice.DKV5000_DM, numberD)
            txtValue.Text = Value
        ElseIf prefixt = "R" Then
            Dim numberR As String = Microsoft.VisualBasic.Mid(Device, 2)
            Value = AxDBCommManager1.ReadDevice(DATABUILDERAXLibLB.DBPlcDevice.DKV5000_RLY_B, numberR)
            txtValue.Text = Value
        End If
    End Sub

    Public Sub WriteDivices()
        Dim parsedValue As Long
        If String.IsNullOrEmpty(Device) Or String.IsNullOrEmpty(Value) Or Not Long.TryParse(Value, parsedValue) Then
            Exit Sub
        End If

        Dim prefixt As String = Microsoft.VisualBasic.Left(Device, 1)
        If prefixt = "D" Then
            Dim numberD As String = Microsoft.VisualBasic.Mid(Device, 3)
            AxDBCommManager1.WriteDevice(DATABUILDERAXLibLB.DBPlcDevice.DKV5000_DM, numberD, parsedValue)
            'AxDBCommManager1.WriteDevice(DATABUILDERAXLibLB.DBPlcDevice.DKV5000_DM, numberD, CLng(txtValue.Text))
        ElseIf prefixt = "R" Then
            Dim numberR As String = Microsoft.VisualBasic.Mid(Device, 2)
            AxDBCommManager1.WriteDevice(DATABUILDERAXLibLB.DBPlcDevice.DKV5000_RLY_B, numberR, parsedValue)
        End If
    End Sub

    Public Sub ClearDiviceAndValue()
        Device = String.Empty
        Value = String.Empty
    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        Device = cmbDeviceNumber.Text
        ReadDivices()
    End Sub

    Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
        Device = cmbDeviceNumber.Text
        Value = txtValue.Text
        WriteDivices()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        cmbDeviceNumber.SelectedIndex = -1
        txtValue.Text = String.Empty
        ClearDiviceAndValue()
    End Sub

End Class
