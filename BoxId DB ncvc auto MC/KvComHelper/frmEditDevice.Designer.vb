<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditDevice
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditDevice))
        Me.btnRead = New System.Windows.Forms.Button()
        Me.btnWrite = New System.Windows.Forms.Button()
        Me.cmbDeviceNumber = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AxDBCommManager1 = New AxDATABUILDERAXLibLB.AxDBCommManager()
        Me.btnClear = New System.Windows.Forms.Button()
        CType(Me.AxDBCommManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(44, 114)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 23)
        Me.btnRead.TabIndex = 1
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = True
        '
        'btnWrite
        '
        Me.btnWrite.Location = New System.Drawing.Point(148, 114)
        Me.btnWrite.Name = "btnWrite"
        Me.btnWrite.Size = New System.Drawing.Size(75, 23)
        Me.btnWrite.TabIndex = 2
        Me.btnWrite.Text = "Write"
        Me.btnWrite.UseVisualStyleBackColor = True
        '
        'cmbDeviceNumber
        '
        Me.cmbDeviceNumber.FormattingEnabled = True
        Me.cmbDeviceNumber.Items.AddRange(New Object() {"DM10", "R15001", "R15002", "R15100", "R11309", "R6506"})
        Me.cmbDeviceNumber.Location = New System.Drawing.Point(109, 35)
        Me.cmbDeviceNumber.Name = "cmbDeviceNumber"
        Me.cmbDeviceNumber.Size = New System.Drawing.Size(121, 20)
        Me.cmbDeviceNumber.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Device"
        '
        'txtValue
        '
        Me.txtValue.Location = New System.Drawing.Point(109, 70)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.Size = New System.Drawing.Size(121, 19)
        Me.txtValue.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Value"
        '
        'AxDBCommManager1
        '
        Me.AxDBCommManager1.Enabled = True
        Me.AxDBCommManager1.Location = New System.Drawing.Point(170, 158)
        Me.AxDBCommManager1.Name = "AxDBCommManager1"
        Me.AxDBCommManager1.OcxState = CType(resources.GetObject("AxDBCommManager1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxDBCommManager1.Size = New System.Drawing.Size(24, 24)
        Me.AxDBCommManager1.TabIndex = 8
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(44, 158)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmEditDevice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 201)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.AxDBCommManager1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDeviceNumber)
        Me.Controls.Add(Me.btnWrite)
        Me.Controls.Add(Me.btnRead)
        Me.Name = "frmEditDevice"
        Me.Text = "Form1"
        CType(Me.AxDBCommManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRead As Button
    Friend WithEvents btnWrite As Button
    Friend WithEvents cmbDeviceNumber As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txtValue As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents AxDBCommManager1 As AxDATABUILDERAXLibLB.AxDBCommManager
    Friend WithEvents btnClear As Button
End Class
