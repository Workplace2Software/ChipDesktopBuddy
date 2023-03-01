Imports System.Threading
Imports System.Threading.Tasks

Imports ChipDesktopBuddy.Win32
Imports ChipDesktopBuddy.Win32.SoundFlags

Imports System.IO

Public Class CallForm
    Dim t As Thread

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CallHelperForm.Show()

        Dim ms As New MemoryStream

        My.Resources.phone_outbound_cut.CopyTo(ms)
        Dim ringSnd = ms.ToArray

        ms.SetLength(0)

        My.Resources.phone_call_opt.CopyTo(ms)
        Dim callSnd = ms.ToArray

        ms.Close()

        Dim ringSndByteRate = BitConverter.ToInt32(New Byte() {ringSnd(28), ringSnd(29), ringSnd(30), ringSnd(31)}, 0)
        Dim callSndByteRate = BitConverter.ToInt32(New Byte() {callSnd(28), callSnd(29), callSnd(30), callSnd(31)}, 0)

        Dim ringSndLen As Integer = (ringSnd.Length / ringSndByteRate) * 1000
        Dim callSndLen As Integer = (callSnd.Length / callSndByteRate) * 1000

        t = New Thread(
            Sub()
                My.Computer.Audio.Play(My.Resources.phone_outbound_cut, AudioPlayMode.Background)
                Thread.Sleep(ringSndLen)
                Invoke(
                    Sub()
                        StatusLabel.Text = "Call In Progress"
                        Me.Text = "Workplace2Software - Call In Progress"
                    End Sub
                )
                My.Computer.Audio.Play(My.Resources.phone_call_opt, AudioPlayMode.Background)
                Thread.Sleep(callSndLen)
                Invoke(
                    Sub()
                        StatusLabel.Text = "Call Ended"
                        Me.Text = "Workplace2Software - Call Ended"
                    End Sub
                )
                ChipMinutesDialog.ShowDialog()
                ChipMinutesDialog.Dispose()
                Invoke(Sub() Me.Close())
            End Sub
        )
        t.Start()

        Timer1.Stop()
    End Sub

    Private Sub CallForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        My.Computer.Audio.Stop()
        t.Abort()

        CallHelperForm.denyClose = False
        CallHelperForm.Close()
    End Sub

    Private Sub EndCallButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndCallButton.Click
        Me.Close()
    End Sub
End Class