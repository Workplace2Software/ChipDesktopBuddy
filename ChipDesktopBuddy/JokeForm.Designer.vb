<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JokeForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JokeForm))
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.AnotherButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(17, 18)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(382, 244)
        Me.WebBrowser1.TabIndex = 0
        '
        'CloseButton
        '
        Me.CloseButton.Location = New System.Drawing.Point(319, 276)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(75, 23)
        Me.CloseButton.TabIndex = 1
        Me.CloseButton.Text = "Close"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'AnotherButton
        '
        Me.AnotherButton.Location = New System.Drawing.Point(238, 276)
        Me.AnotherButton.Name = "AnotherButton"
        Me.AnotherButton.Size = New System.Drawing.Size(75, 23)
        Me.AnotherButton.TabIndex = 2
        Me.AnotherButton.Text = "Another!"
        Me.AnotherButton.UseVisualStyleBackColor = True
        '
        'JokeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.ChipDesktopBuddy.My.Resources.Resources.stage
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(416, 308)
        Me.Controls.Add(Me.AnotherButton)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "JokeForm"
        Me.Text = "Your a Joke..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents AnotherButton As System.Windows.Forms.Button
End Class
