Imports ChipDesktopBuddy.Win32

Public Class TrackedForm
    Dim b As Rectangle
    Dim xOffset = 0

    Private Sub TrackedForm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click
        Process.Start("iexplore", "https://google.com/search?q=How+2+sOP+Getting+Trackde%3f%3f%3f%3f")
        Me.Close()
    End Sub

    Private Sub TrackedForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        b = Form1.Screen.WorkingArea
        SetWindowPos(Handle, HWND_TOPMOST, b.Width, b.Height - Me.Height, 0, 0, SWP_NOSIZE)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        xOffset += 3
        If xOffset > Me.Width Then Timer1.Stop() : Exit Sub
        SetWindowPos(Handle, 0, b.Width - xOffset, b.Height - Me.Height, 0, 0, SWP_NOZORDER Or SWP_NOSIZE)
    End Sub
End Class