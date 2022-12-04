Imports System.Windows.Forms

Public Class MailDialog
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then
            MessageBox.Show(
                "No E-Mail Addresses have been inputted." & vbCrLf & _
                "Please input at least one E-Mail Address or click Cancel to cancel.",
                "Input an E-Mail Address",
                MessageBoxButtons.OK, MessageBoxIcon.Information
            )
            Exit Sub
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Form1.dialogRes1 = TextBox1.Text
        Form1.dialogRes2 = TextBox2.Text
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
