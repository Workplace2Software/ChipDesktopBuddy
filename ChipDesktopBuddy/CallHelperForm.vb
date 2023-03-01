Public Class CallHelperForm
    Friend denyClose = True

    Private Sub CallHelperForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = denyClose
    End Sub
End Class