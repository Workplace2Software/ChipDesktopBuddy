Imports System.Runtime.InteropServices
Imports System.IO

Imports IWshRuntimeLibrary

Public Class Form1
#Region "winapi consts"
    Const WM_NCLBUTTONDOWN = &HA1&
    Const HT_CAPTION = 2

    Private ReadOnly HWND_BOTTOM As New IntPtr(1)
    Private ReadOnly HWND_TOPMOST As New IntPtr(-1)

    Const SWP_NOMOVE = 2
    Const SWP_NOSIZE = 1
#End Region
    Private ReadOnly shortcutPath =
        Environment.GetFolderPath(Environment.SpecialFolder.Startup) & _
        "\ChipDesktopBuddy.lnk"

    Dim r As New Random

    Dim currentScreen As Screen
    Dim angle = 135

    Dim chipsfxs() = { _
        My.Resources.chip1, My.Resources.chip2, My.Resources.chip3,
        My.Resources.chip4, My.Resources.chip5, My.Resources.chip6 _
    }

    Private spinspeed = 0
    Dim chipimgs() = { _
        My.Resources.chipimg, My.Resources.chipimg2,
        My.Resources.chipimg3, My.Resources.chipimg4 _
    }

    Public dialogRes1, dialogRes2 As String

    <DllImport("user32.dll")> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <DllImport("user32.dll")> _
    Public Shared Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            ' random chance to scream when touched, like real people!
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

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem1.Click
        Application.Exit()
    End Sub

    Private Sub PlayCrunch(ByVal sound As System.IO.UnmanagedMemoryStream)
        ' Cough condition, covid lockdown or flu season
        If (Date.Now.Month = 3 Or Date.Now.Month = 10) And r.Next() > 0.5 Then
            My.Computer.Audio.Play(My.Resources.cough, AudioPlayMode.Background)
            Exit Sub
        End If
        sound.Seek(0, SeekOrigin.Begin)
        My.Computer.Audio.Play(sound, AudioPlayMode.Background)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim soundnum = r.Next(0, chipsfxs.Length) ' .net always starts at 0
        PlayCrunch(chipsfxs(soundnum))
        Timer1.Interval = r.Next(10000, 60000)
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Interval = r.Next(10000, 60000)
        Timer1.Enabled = True

        currentScreen = Screen.AllScreens.First(Function(s) s.Bounds.Contains(Me.Location))

        ' act on persistent settings
        AlwaysOnTopOperation()
        RubberBallOperation()
        StartWithMyComputerOperation(True)
    End Sub

    Private Sub MoreFromWorkplace2SoftwareToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoreFromWorkplace2SoftwareToolStripMenuItem1.Click
        Process.Start("iexplore", "http://www.theworkplace.tk")
    End Sub

    Private Sub GetFunkyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetFunkyToolStripMenuItem1.Click
        Process.Start("http://youtu.be/WIRK_pGdIdA")
    End Sub

    Private Sub AlwaysOnTopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AlwaysOnTopToolStripMenuItem1.Click
        My.Settings.OnTop = Not My.Settings.OnTop
        AlwaysOnTopOperation()
        My.Settings.Save()
    End Sub

    Private Sub AlwaysOnTopOperation()
        AlwaysOnTopToolStripMenuItem1.Checked = My.Settings.OnTop

        If My.Settings.OnTop Then
            SetWindowPos(Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
        Else
            SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
        End If
    End Sub

    Private Sub CheckForUpdatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckForUpdatesToolStripMenuItem1.Click
        My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
        WarningForm.Show()
        SetWindowPos(WarningForm.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    End Sub

    Private Sub AboutChipDesktopBuddyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutChipDesktopBuddyToolStripMenuItem1.Click
        AboutBox1.Show()
    End Sub

    Private Sub ChangeSpeedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeSpeedToolStripMenuItem1.Click
        Dim res = SpeedDialog.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            spinspeed += 1
            If spinspeed >= chipimgs.Length Then
                My.Computer.Audio.Play(My.Resources.WindowGlass, AudioPlayMode.WaitToComplete)
                PictureBox1.Visible = False
                MessageBox.Show(
                    "too fast",
                    "Oh shit!!"
                )
                Application.Exit()
                Exit Sub
            End If

            Timer2.Interval -= 20

            PictureBox1.Image = chipimgs(spinspeed)
        End If
        SpeedDialog.Dispose()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Dim angleRad As Single = Math.PI * (angle Mod 360) / 180
        Dim newx As Integer = Me.Location.X - Math.Sin(angleRad) * 10
        Dim newy As Integer = Me.Location.Y + Math.Cos(angleRad) * 10

        Dim location As New Point(newx, newy)
        Me.Location = location

        Dim p1 As Point = New Point(Me.Location.X - currentScreen.Bounds.Left, Me.Location.Y - currentScreen.Bounds.Top)
        Dim p2 As Point = New Point(Me.Right - currentScreen.Bounds.Left, Me.Bottom - currentScreen.Bounds.Top)
        If (p1.X < 0 Or p1.Y < 0 _
            Or p2.Y > currentScreen.Bounds.Height Or p2.X > currentScreen.Bounds.Width _
        ) Then
            angle += 90
        End If
    End Sub

    Private Sub RubberBallToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RubberBallToolStripMenuItem.Click
        My.Settings.Bouncing = Not My.Settings.Bouncing
        RubberBallOperation()
        My.Settings.Save()
    End Sub

    Private Sub RubberBallOperation()
        RubberBallToolStripMenuItem.Checked = My.Settings.Bouncing
        Timer2.Enabled = My.Settings.Bouncing
    End Sub

    Private Sub ShareMeWithFriendsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShareMeWithFriendsToolStripMenuItem.Click
        Dim res = MailDialog.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            Process.Start("mailto:" & dialogRes1 & "?subject=Check out this Cool Desktop Assistant%21&body=" & _
                Uri.EscapeDataString(dialogRes2 & vbCrLf & vbCrLf & StrDup(40, "-") & vbCrLf & _
                                     "This message was sent via Chip Desktop Buddy." & vbCrLf & _
                                     "Download me at https://www.theworkplace.tk!" _
                ) _
            )
        End If
        MailDialog.Dispose()
    End Sub

    Private Sub StartWithMyComputerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartWithMyComputerToolStripMenuItem.Click
        My.Settings.AutoStart = Not My.Settings.AutoStart
        StartWithMyComputerOperation()

        ' one time operation
        If My.Settings.AutoStart Then
            Dim wsh As New WshShell
            Dim shortcut = CType(
                wsh.CreateShortcut(shortcutPath), 
                IWshShortcut
            )
            shortcut.TargetPath = Application.ExecutablePath
            shortcut.Save()
        Else
            If IO.File.Exists(shortcutPath) Then IO.File.Delete(shortcutPath)
        End If

        My.Settings.Save()
    End Sub

    Private Sub StartWithMyComputerOperation(Optional ByVal doneAtStartup As Boolean = False)
        If doneAtStartup Then
            ' prevent checkbox desync if file is manually deleted
            My.Settings.AutoStart = IO.File.Exists(shortcutPath)
        End If
        StartWithMyComputerToolStripMenuItem.Checked = My.Settings.AutoStart
    End Sub
End Class
