Public Class RegisterNag

    Dim colorSwap As Boolean = True
    Dim colorSwap2 As Boolean = True
    Dim soundExit = True

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If colorSwap Then
            OrderLabel.ForeColor = Color.Blue
        Else
            OrderLabel.ForeColor = Color.Red
        End If
        colorSwap = Not colorSwap
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If colorSwap2 Then
            NotFreeLabel.ForeColor = Color.SaddleBrown
        Else
            NotFreeLabel.ForeColor = Color.FromArgb(&HFF00FF00)
        End If
        colorSwap2 = Not colorSwap2
    End Sub

    Private Sub RegisterDialog_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If soundExit Then My.Computer.Audio.Play(My.Resources.cry3_micah, AudioPlayMode.WaitToComplete)
    End Sub

    Private Sub OrderButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OrderButton.Click
        Process.Start("iexplore", "http://www.theworkplace.tk")
    End Sub

    Private Sub RegisterButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterButton.Click
        If Not MaskedTextBox1.MaskCompleted Then
            MessageBox.Show(
                "Enter all numbers of License Key and click Register.",
                "Eror",
                MessageBoxButtons.OK, MessageBoxIcon.Error
            )
            Exit Sub
        End If

        RegisterButton.Text = "Registering..."
        RegisterButton.Enabled = False

        Threading.Thread.Sleep(10000)

        Me.Hide()
        WarningForm.ShowDialog()
        WarningForm.Dispose()

        MessageBox.Show(
            "Registration server is having Issues.... Try Phone Method",
            "Registration Eror",
            MessageBoxButtons.OK, MessageBoxIcon.Error
        )

        RegisterButton.Text = "Register"
        Me.Show()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CallForm.ShowDialog()
        CallForm.Dispose()
    End Sub
End Class