Imports System.Runtime.InteropServices

Module Win32
    Friend Const WM_NCLBUTTONDOWN = &HA1&
    Friend Const HT_CAPTION = 2

    Friend ReadOnly HWND_BOTTOM As New IntPtr(1)
    Friend ReadOnly HWND_TOPMOST As New IntPtr(-1)

    Friend Const SWP_NOMOVE = 2
    Friend Const SWP_NOSIZE = 1
    Friend Const SWP_NOZORDER = 4

    Friend ReadOnly NULL = IntPtr.Zero
    Friend ReadOnly UNULL = UIntPtr.Zero

    <DllImport("user32.dll")> _
    Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    <DllImport("user32.dll")> _
    Function ReleaseCapture() As Boolean
    End Function
    <DllImport("user32.dll", SetLastError:=True)> _
    Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInt32) As Boolean
    End Function
    <DllImport("user32.dll")> _
    Sub keybd_event(ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As UInteger, ByVal dwExtraInfo As Integer)
    End Sub
    <DllImport("user32.dll")> _
    Function FlashWindow(ByVal hwnd As IntPtr, ByVal bInvert As Boolean) As Boolean
    End Function


    <Flags()> _
    Public Enum SoundFlags As Integer
        ''' <summary>
        ''' The sound is played synchronously, and PlaySound returns after
        ''' the sound event completes. This is the default behavior.
        ''' </summary>
        SND_SYNC = &H0

        ''' <summary>
        ''' The sound is played asynchronously and PlaySound returns
        ''' immediately after beginning the sound. To terminate an
        ''' asynchronously played waveform sound, call PlaySound with
        ''' pszSound set to NULL.
        ''' </summary>
        SND_ASYNC = &H1

        ''' <summary>
        ''' No default sound event is used. If the sound cannot be found,
        ''' PlaySound returns silently without playing the default sound.
        ''' </summary>
        SND_NODEFAULT = &H2

        ''' <summary>
        ''' The pszSound parameter points to a sound loaded in memory.
        ''' </summary>
        SND_MEMORY = &H4

        ''' <summary>
        ''' The sound plays repeatedly until PlaySound is called again
        ''' with the pszSound parameter set to NULL. If this flag is
        ''' set, you must also set the SND_ASYNC flag.
        ''' </summary>
        SND_LOOP = &H8

        ''' <summary>
        ''' The specified sound event will yield to another sound event
        ''' that is already playing. If a sound cannot be played because
        ''' the resource needed to generate that sound is busy playing
        ''' another sound, the function immediately returns False without
        ''' playing the requested sound.
        ''' </summary>
        ''' <remarks>If this flag is not specified, PlaySound attempts
        ''' to stop the currently playing sound so that the device can
        ''' be used to play the new sound.
        ''' </remarks>
        SND_NOSTOP = &H10

        ''' <summary>
        ''' Stop playing wave
        ''' </summary>
        SND_PURGE = &H40

        ''' <summary>
        ''' The pszSound parameter is an application-specific alias in
        ''' the registry. You can combine this flag with the SND_ALIAS
        ''' or SND_ALIAS_ID flag to specify an application-defined sound
        ''' alias.
        ''' </summary>
        SND_APPLICATION = &H80

        ''' <summary>
        ''' If the driver is busy, return immediately without playing
        ''' the sound.
        ''' </summary>
        SND_NOWAIT = &H2000

        ''' <summary>
        ''' The pszSound parameter is a system-event alias in the
        ''' registry or the WIN.INI file. Do not use with either
        ''' SND_FILENAME or SND_RESOURCE.
        ''' </summary>
        SND_ALIAS = &H10000

        ''' <summary>
        ''' The pszSound parameter is a file name. If the file cannot be
        ''' found, the function plays the default sound unless the
        ''' SND_NODEFAULT flag is set.
        ''' </summary>
        SND_FILENAME = &H20000

        ''' <summary>
        ''' The pszSound parameter is a resource identifier; hmod must
        ''' identify the instance that contains the resource.
        ''' </summary>
        SND_RESOURCE = &H40004
    End Enum

    <DllImport("winmm.dll")> _
    Function PlaySound( _
        ByVal szSound As String, _
        ByVal hModule As UIntPtr, _
        ByVal fdwSound As Integer) As Integer
    End Function
    <DllImport("winmm.dll")> _
    Function PlaySound( _
        ByVal szSound As Byte(), _
        ByVal hModule As UIntPtr, _
        ByVal fdwSound As Integer) As Integer
    End Function
End Module
