Imports System.Windows.Forms

Public Class SpeedDialog
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub brakeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brakeButton.Click
        MessageBox.Show(
            "SOmeone cut the breaks!!!!!!!111",
            "NOOOOOO MA'M NO!!",
            MessageBoxButtons.OK, MessageBoxIcon.Error
        )
    End Sub
End Class
