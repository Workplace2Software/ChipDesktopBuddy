﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpeedDialog
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
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.brakeButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.BackColor = System.Drawing.Color.Transparent
        Me.OK_Button.FlatAppearance.BorderSize = 0
        Me.OK_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.OK_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.OK_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OK_Button.Location = New System.Drawing.Point(217, 99)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(66, 59)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.UseVisualStyleBackColor = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(320, 192)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(68, 28)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'brakeButton
        '
        Me.brakeButton.BackColor = System.Drawing.Color.Transparent
        Me.brakeButton.FlatAppearance.BorderSize = 0
        Me.brakeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.brakeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.brakeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.brakeButton.Location = New System.Drawing.Point(149, 99)
        Me.brakeButton.Name = "brakeButton"
        Me.brakeButton.Size = New System.Drawing.Size(62, 59)
        Me.brakeButton.TabIndex = 2
        Me.brakeButton.UseVisualStyleBackColor = False
        '
        'SpeedDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.ChipDesktopBuddy.My.Resources.Resources.Car_pedals_20180814_small
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(388, 221)
        Me.Controls.Add(Me.brakeButton)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SpeedDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change Speed..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents brakeButton As System.Windows.Forms.Button

End Class
