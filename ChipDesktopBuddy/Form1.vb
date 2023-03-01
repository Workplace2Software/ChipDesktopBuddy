Imports System.IO

Imports IWshRuntimeLibrary

Imports ChipDesktopBuddy.Win32

Public Class Form1
    Private ReadOnly shortcutPath =
        Environment.GetFolderPath(Environment.SpecialFolder.Startup) & _
        "\ChipDesktopBuddy.lnk"

    Dim r As New Random

    Private currentScreen As Screen
    Public ReadOnly Property Screen As Screen
        Get
            Return currentScreen
        End Get
    End Property

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

#Region "click events"
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

    Private Sub PictureBox1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        If e.Button = MouseButtons.Left Then
            ' finished dragging
            currentScreen = Screen.AllScreens.First(Function(s) s.Bounds.Contains(Me.Location))
        End If
    End Sub
#End Region

#Region "crunching corner"
    Private Sub PlayCrunch(ByVal sound As System.IO.UnmanagedMemoryStream)
        ' Cough condition, covid lockdown or flu season
        If (Date.Now.Month = 3 Or Date.Now.Month = 10) And r.NextDouble() < 0.5 Then
            My.Computer.Audio.Play(My.Resources.cough, AudioPlayMode.Background)
            Exit Sub
        End If
        sound.Seek(0, SeekOrigin.Begin)
        My.Computer.Audio.Play(sound, AudioPlayMode.Background)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CrunchOperation()
    End Sub

    Private Sub CrunchOperation()
        Dim soundnum = r.Next(0, chipsfxs.Length) ' .net always starts at 0
        PlayCrunch(chipsfxs(soundnum))

        If r.NextDouble() < 0.2 Then
            TrackedForm.Show()
        End If

        FlashWindow(Handle, False)

        Timer1.Interval = r.Next(10000, 60000)
    End Sub
#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = False

        currentScreen = Screen.AllScreens.First(Function(s) s.Bounds.Contains(Me.Location))

        ' persist settings thru upgrades
        If New Version(My.Settings.Version) < My.Application.Info.Version Then
            My.Settings.Upgrade()
            My.Settings.Version = My.Application.Info.Version.ToString
            My.Settings.Save()
        End If

        ' act on persistent settings
        AlwaysOnTopOperation()
        RubberBallOperation()
        StartWithMyComputerOperation(True)

        PictureBox2.Visible = (Date.Now.Month = 12)

        RegisterNag.ShowDialog()
        RegisterNag.Dispose()

        Timer1.Interval = r.Next(10000, 60000)
        Timer1.Enabled = True
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F9
                QuietModeF9ToolStripMenuItem.PerformClick()
                'Case Keys.T ' debug
                '    TrackedForm.Show()
        End Select
    End Sub

#Region "tool strip menu items"
    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem1.Click
        Application.Exit()
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
                    "Oh crap!!"
                )
                Application.Exit()
                Exit Sub
            End If

            Timer2.Interval -= 20

            PictureBox1.Image = chipimgs(spinspeed)
        End If
        SpeedDialog.Dispose()
    End Sub

#Region "rubber ball"
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
#End Region

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

    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        My.Settings.OnTop = False
        AlwaysOnTopOperation()
        My.Settings.Save()
        ' second time to hide it behind all other windows
        SetWindowPos(Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE Or SWP_NOSIZE)
    End Sub

    Private Sub QuietModeF9ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuietModeF9ToolStripMenuItem.Click
        For i = 0 To 100
            keybd_event(CByte(Keys.VolumeUp), 0, 0, 0)
            keybd_event(CByte(Keys.VolumeUp), 0, 2, 0) ' keyup
        Next
    End Sub

    Private Sub SingASongToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SingASongToolStripMenuItem.Click
        ConquerToolStripMenuItem.PerformClick()
    End Sub

    Private Sub TellAJokeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TellAJokeToolStripMenuItem.Click
        JokeForm.Show()
    End Sub

#Region "let me help you"
    Private Sub TypeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TypeToolStripMenuItem.Click
        TypeDialog.Show()
    End Sub

    Private Sub ConquerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConquerToolStripMenuItem.Click
        Process.Start("https://youtu.be/0iI4ap-QA38")
    End Sub
#End Region

#Region "game launchers"
    Private Sub DoCmdHack()
        ' Stupid hack if stupid cmd stupidly stays put, stupid!
        ' Known to happen at least once
        ' {Alt down}{Space}{Alt up}n
        keybd_event(12, 0, 0, 0)
        keybd_event(CByte(Keys.Space), 0, 0, 0)
        keybd_event(CByte(Keys.Space), 0, 2, 0)
        keybd_event(12, 0, 2, 0)
        keybd_event(CByte(Keys.N), 0, 0, 0)
        keybd_event(CByte(Keys.N), 0, 2, 0)
    End Sub

    Private Sub SuperDipChipToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SuperDipChipToolStripMenuItem.Click
        Process.Start("cmd", "/c ""echo Launching game... & cd games\dipchip & start dipchip""")
        DoCmdHack()
    End Sub

    Private Sub Chip97ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Chip97ToolStripMenuItem.Click
        If Not IO.Directory.Exists("games/chipgameninetyseven") Then
            MessageBox.Show(
                "Chip '97 has been compressed to save space and will be decompressed." & vbCrLf & vbCrLf & _
                "Choose ""[Chip's install directory]/games/chipgameninetyseven"" as the folder to extract to " & _
                "(this should be the default), then choose Play Games -> Chip '97 again.",
                "Compression Advisory",
                MessageBoxButtons.OK, MessageBoxIcon.Information
            )
            Process.Start("cmd", "/c ""cd games & start cg97extract""")
            DoCmdHack()
            Exit Sub
        End If

        Dim res = MessageBox.Show(
            "This game is intended for mature audiences only. To play, you must answer the following question:" & vbCrLf & vbCrLf & _
            "Have you passed (or at least reached) pubescence?" & vbCrLf & _
            "(Choose No if you don't know what that means.)",
            "Mature Audiences Warning",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning
        )
        If res = DialogResult.No Then Exit Sub

        Process.Start("cmd", "/c ""echo Lauching game... & cd games\chipgameninetyseven & start chipgameninetyseven""")
        DoCmdHack()
    End Sub

    Private Sub ChipRacerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChipRacerToolStripMenuItem.Click
        Process.Start("cmd", "/c ""echo Launching game... & cd games\racer & start racer""")
        DoCmdHack()
    End Sub

#End Region
#End Region

    Private Sub RegisterForSaltnVinegarAccessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegisterForSaltnVinegarAccessToolStripMenuItem.Click
        Me.Hide()
        RegisterNag.ShowDialog()
        RegisterNag.Dispose()
        Me.Show()
    End Sub
End Class
