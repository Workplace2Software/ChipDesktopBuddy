Imports System.Windows.Forms

Public Class ChipMinutesDialog

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        OK_Button.Enabled = False
        Threading.Thread.Sleep(2000)
        MessageBox.Show(
            "Could not purchase more minutes; the server timed out (504).",
            "Purchase Failure",
            MessageBoxButtons.OK, MessageBoxIcon.Error
        )
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class
