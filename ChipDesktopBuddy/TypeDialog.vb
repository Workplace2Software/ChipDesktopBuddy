Imports System.Windows.Forms

Public Class TypeDialog
    Dim showbkg = False

    Private Sub Delay(ByVal centis As Integer)
        For i = 0 To centis
            Threading.Thread.Sleep(10)
            Application.DoEvents()
        Next
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        statusLabel.Text = "Typing in 3..."
        Delay(100)
        statusLabel.Text = "Typing in 2..."
        Delay(100)
        statusLabel.Text = "Typing in 1..."
        Delay(100)
        statusLabel.Text = "Typing..."
        SendKeys.Send(TextBox1.Text)
        statusLabel.Text = "Ready to type..."
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        showbkg = Not showbkg
        If showbkg Then
            Button1.Text = "Show textbox"
            TextBox1.Visible = False
        Else
            Button1.Text = "Show background"
            TextBox1.Visible = True
        End If
    End Sub
End Class
