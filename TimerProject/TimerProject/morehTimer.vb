Imports System.Threading
Public Class morehTimer
    Dim newStopwatch As New System.Diagnostics.Stopwatch

    Dim results As String = Nothing

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        newStopwatch.Start()

        Dim ts As TimeSpan = newStopwatch.Elapsed
        While ts.Milliseconds > 0
            txtTimer.Text = String.Format("{0} minute(s)" & " {1} second(s)" & " {2} milesconds(s)", ts.Minutes, ts.Seconds, ts.Milliseconds)


        End While
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class
