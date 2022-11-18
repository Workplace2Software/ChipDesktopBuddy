Imports System.Runtime.InteropServices

Public Class Form1
    Const WM_NCLBUTTONDOWN = &HA1&
    Const HT_CAPTION = 2

    Private ReadOnly HWND_BOTTOM As New IntPtr(1)
    Private ReadOnly HWND_TOPMOST As New IntPtr(-1)

    Const SWP_NOMOVE = 2
    Const SWP_NOSIZE = 1

    Dim r As New Random

    Dim ontop As Boolean

    <DllImportAttribute("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <DllImportAttribute("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim coolevent = r.Next(1, 50)
            If coolevent = 22 Then
                My.Computer.Audio.Play(My.Resources.a, AudioPlayMode.WaitToComplete)
            End If

            ' drag to move
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        ElseIf e.Button = MouseButtons.Right Then
            ' context menu
            ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub PlayCrunch(ByVal sound As System.IO.UnmanagedMemoryStream)
        ' Cough condition, covid lockdown or flu season
        If (Date.Now.Month = 3 Or Date.Now.Month = 10) And r.Next() > 0.5 Then
            My.Computer.Audio.Play(My.Resources.cough, AudioPlayMode.Background)
            Exit Sub
        End If
        My.Computer.Audio.Play(sound, AudioPlayMode.Background)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Select Case r.Next(1, 6)
            Case 1
                PlayCrunch(My.Resources.chip1)
            Case 2
                PlayCrunch(My.Resources.chip2)
            Case 3
                PlayCrunch(My.Resources.chip3)
            Case 4
                PlayCrunch(My.Resources.chip4)
            Case 5
                PlayCrunch(My.Resources.chip5)
            Case 6
                PlayCrunch(My.Resources.chip6)
        End Select
        Timer1.Interval = r.Next(10000, 60000)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Interval = r.Next(10000, 60000)
        Timer1.Enabled = True
    End Sub

    Private Sub MoreFromWorkplace2SoftwareToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoreFromWorkplace2SoftwareToolStripMenuItem.Click
        Process.Start("iexplore", "http://www.theworkplace.tk")
    End Sub

    Private Sub GetFunkyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetFunkyToolStripMenuItem.Click
        Process.Start("http://youtu.be/WIRK_pGdIdA")
    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem.Click
        ontop = Not ontop
        AlwaysOnTopToolStripMenuItem.Checked = ontop

        If ontop Then
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
        Else
            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
        End If
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem.Click
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        WarningForm.Show()
        SetWindowPos(WarningForm.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    End Sub

    Private Sub AboutChipDesktopBuddyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutChipDesktopBuddyToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub
End Class
