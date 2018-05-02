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
        Me.ServerAddressTextBox = New System.Windows.Forms.TextBox
        Me.ServerAddressLabel = New System.Windows.Forms.Label
        Me.ClientAddressTextBox = New System.Windows.Forms.TextBox
        Me.ClientAddressLabel = New System.Windows.Forms.Label
        Me.ConnectionsLabel = New System.Windows.Forms.Label
        Me.ConnectionsComboBox = New System.Windows.Forms.ComboBox
        Me.GetAddressButton = New System.Windows.Forms.Button
        Me.Dialer = New DotRas.RasDialer(Me.components)
        Me.SuspendLayout()
        '
        'ServerAddressTextBox
        '
        Me.ServerAddressTextBox.Location = New System.Drawing.Point(12, 155)
        Me.ServerAddressTextBox.Name = "ServerAddressTextBox"
        Me.ServerAddressTextBox.ReadOnly = True
        Me.ServerAddressTextBox.Size = New System.Drawing.Size(260, 20)
        Me.ServerAddressTextBox.TabIndex = 13
        '
        'ServerAddressLabel
        '
        Me.ServerAddressLabel.AutoSize = True
        Me.ServerAddressLabel.Location = New System.Drawing.Point(12, 139)
        Me.ServerAddressLabel.Name = "ServerAddressLabel"
        Me.ServerAddressLabel.Size = New System.Drawing.Size(95, 13)
        Me.ServerAddressLabel.TabIndex = 12
        Me.ServerAddressLabel.Text = "Server IP Address:"
        '
        'ClientAddressTextBox
        '
        Me.ClientAddressTextBox.Location = New System.Drawing.Point(12, 109)
        Me.ClientAddressTextBox.Name = "ClientAddressTextBox"
        Me.ClientAddressTextBox.ReadOnly = True
        Me.ClientAddressTextBox.Size = New System.Drawing.Size(260, 20)
        Me.ClientAddressTextBox.TabIndex = 11
        '
        'ClientAddressLabel
        '
        Me.ClientAddressLabel.AutoSize = True
        Me.ClientAddressLabel.Location = New System.Drawing.Point(12, 93)
        Me.ClientAddressLabel.Name = "ClientAddressLabel"
        Me.ClientAddressLabel.Size = New System.Drawing.Size(90, 13)
        Me.ClientAddressLabel.TabIndex = 10
        Me.ClientAddressLabel.Text = "Client IP Address:"
        '
        'ConnectionsLabel
        '
        Me.ConnectionsLabel.AutoSize = True
        Me.ConnectionsLabel.Location = New System.Drawing.Point(12, 15)
        Me.ConnectionsLabel.Name = "ConnectionsLabel"
        Me.ConnectionsLabel.Size = New System.Drawing.Size(69, 13)
        Me.ConnectionsLabel.TabIndex = 9
        Me.ConnectionsLabel.Text = "Connections:"
        '
        'ConnectionsComboBox
        '
        Me.ConnectionsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ConnectionsComboBox.FormattingEnabled = True
        Me.ConnectionsComboBox.Location = New System.Drawing.Point(12, 31)
        Me.ConnectionsComboBox.Name = "ConnectionsComboBox"
        Me.ConnectionsComboBox.Size = New System.Drawing.Size(260, 21)
        Me.ConnectionsComboBox.TabIndex = 8
        '
        'GetAddressButton
        '
        Me.GetAddressButton.Enabled = False
        Me.GetAddressButton.Location = New System.Drawing.Point(178, 226)
        Me.GetAddressButton.Name = "GetAddressButton"
        Me.GetAddressButton.Size = New System.Drawing.Size(94, 23)
        Me.GetAddressButton.TabIndex = 7
        Me.GetAddressButton.Text = "Get Address"
        Me.GetAddressButton.UseVisualStyleBackColor = True
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.Controls.Add(Me.ServerAddressTextBox)
        Me.Controls.Add(Me.ServerAddressLabel)
        Me.Controls.Add(Me.ClientAddressTextBox)
        Me.Controls.Add(Me.ClientAddressLabel)
        Me.Controls.Add(Me.ConnectionsLabel)
        Me.Controls.Add(Me.ConnectionsComboBox)
        Me.Controls.Add(Me.GetAddressButton)
        Me.Name = "MainForm"
        Me.Text = "Get IP Addresses Example"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents ServerAddressTextBox As System.Windows.Forms.TextBox
    Private WithEvents ServerAddressLabel As System.Windows.Forms.Label
    Private WithEvents ClientAddressTextBox As System.Windows.Forms.TextBox
    Private WithEvents ClientAddressLabel As System.Windows.Forms.Label
    Private WithEvents ConnectionsLabel As System.Windows.Forms.Label
    Private WithEvents ConnectionsComboBox As System.Windows.Forms.ComboBox
    Private WithEvents GetAddressButton As System.Windows.Forms.Button
    Friend WithEvents Dialer As DotRas.RasDialer

End Class
