Imports System.Threading
Imports System.Threading.Tasks

Imports ChipDesktopBuddy.Win32
Imports ChipDesktopBuddy.Win32.SoundFlags

Imports System.IO

Public Class CallForm
    Dim phone_outbound_cut As Byte()
    Dim phone_call_opt As Byte()

    Dim t As Thread

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CallHelperForm.Show()

        Dim ms As New MemoryStream
        My.Resources.phone_outbound_cut.CopyTo(ms)
        phone_outbound_cut = ms.ToArray
        My.Resources.phone_call_opt.CopyTo(ms)
        phone_call_opt = ms.ToArray
        ms.Close()

        t = New Thread(AddressOf DoSoundTask)
        t.Start()

        Timer1.Stop()
    End Sub

    Private Sub DoSoundTask()
        Dim task As Task = _
        task.Factory.StartNew(
            Sub()
                'PlaySound(phone_outbound_cut, UNULL, SND_MEMORY Or SND_SYNC)
                My.Computer.Audio.Play(My.Resources.phone_outbound_cut, AudioPlayMode.WaitToComplete)
            End Sub) _
        .ContinueWith(
                    Sub()
                        Invoke(
                            Sub()
                                StatusLabel.Text = "Call Active"
                                Text = "Workplace2Software - Call Active"
                            End Sub)
                        'PlaySound(phone_call_opt, UNULL, SND_MEMORY Or SND_SYNC)
                        My.Computer.Audio.Play(My.Resources.phone_call_opt, AudioPlayMode.WaitToComplete)
                    End Sub
                ) _
        .ContinueWith(
               Sub()
                   ' TODO: show no more chip minutes
               End Sub
        )
    End Sub

    Private Sub CallForm_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'PlaySound("", UNULL, SND_ASYNC Or SND_NODEFAULT)
        t.Abort()

        CallHelperForm.denyClose = False
        CallHelperForm.Close()
    End Sub

    Private Sub EndCallButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndCallButton.Click
        Me.Close()
    End Sub
End Class