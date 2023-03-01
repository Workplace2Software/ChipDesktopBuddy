Imports System.IO
Imports System.Net
Imports System.Threading

Public Structure JokeProvider
    Public ApiUrl As String
    Public LandingUrl As String

    Public Sub New(ByVal url As String)
        ApiUrl = url
        LandingUrl = url
    End Sub

    Public Sub New(ByVal apiUrl As String, ByVal landingUrl As String)
        Me.ApiUrl = apiUrl
        Me.LandingUrl = landingUrl
    End Sub
End Structure

Public Class JokeForm
    Dim r As New Random

    Private style As String

    Private ReadOnly JokeProviders() As JokeProvider = {
        New JokeProvider("https://icanhazdadjoke.com"),
        New JokeProvider("https://api.chucknorris.io/jokes/random", "https://chucknorris.io")
    }

    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub

    Private Sub JokeForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        style = GetSmileyStyle()

        WebBrowser1.Navigate("about:blank")
    End Sub

    Private Sub InitializeDocument(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        With WebBrowser1.Document
            .OpenNew(True)
            .Write(style)
            .Write("<h1>Loading Joke...</h1>")
        End With

        Dim t As New Thread(Sub() GetJoke(JokeProviders(r.Next(0, JokeProviders.Length))))
        t.Start()

        RemoveHandler WebBrowser1.DocumentCompleted, AddressOf InitializeDocument
    End Sub

    Private Sub AnotherButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AnotherButton.Click
        AddHandler WebBrowser1.DocumentCompleted, AddressOf InitializeDocument
        WebBrowser1.Navigate("about:blank")
    End Sub

    Private Function GetSmileyStyle() As String
        Dim ms As New MemoryStream
        ms.Seek(0, SeekOrigin.Begin)
        My.Resources.smiley.Save(ms, Imaging.ImageFormat.Png)
        Dim arr As Byte() = ms.ToArray()
        ms.Close()
        Dim smileyStr = Convert.ToBase64String(arr)

        Return "<style>html { background-image: url('data:image/png;base64," & _
            smileyStr & _
            "'); color: red; background-color: yellow; }</style>"
    End Function

    Private Sub SetJoke(ByVal joke As String, ByVal url As String)
        With WebBrowser1.Document
            .OpenNew(True)
            .Write(style)
            .Write(String.Format(
                "<p><b>{0}</b></p><br><p>Joke from <a href='{1}'>{1}</a>",
                joke, url
            ))
        End With
    End Sub

    Private Sub GetJoke(ByVal provider As JokeProvider)
        Dim wrq As HttpWebRequest = WebRequest.Create(provider.ApiUrl)
        wrq.Accept = "text/plain"

        ' Dad joke people like to know who wants them
        wrq.UserAgent = "ChipDesktopBuddy/" & _
            My.Application.Info.Version.ToString & _
            " (http://github.com/Workplace2Software/ChipDesktopBuddy)"

        Dim response As HttpWebResponse = wrq.GetResponse()
        Dim resStream = response.GetResponseStream()
        Dim reader As New StreamReader(resStream)
        Dim strResponse = reader.ReadToEnd()

        Invoke(Sub() SetJoke(strResponse, provider.LandingUrl))

        reader.Close()
        resStream.Close()
        response.Close()
    End Sub
End Class