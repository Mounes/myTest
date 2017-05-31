Imports System
Imports System.Threading.Tasks
Imports System.Threading


Public Class Form1
    Private Shared Function ExecuteSlowStuff(ByVal procc As Action) As Integer
        Dim result As Integer = 0
        For i = 0 To 5
            result += i
            Thread.Sleep(500)
            procc()
        Next

        Return result
    End Function
    Dim lolo As Integer = 0
    Private Sub Start()

        Dim slowStuff = Task(Of Integer).Factory.StartNew(Function() ExecuteSlowStuff(AddressOf Me.ShowProgress))
        slowStuff.ContinueWith(Sub()

                                   If Me.Progress.InvokeRequired Then
                                       Dim x As New Action(AddressOf Me.Progress.Dispose)
                                       Me.Invoke(x)
                                   End If
                                   MsgBox("Finished")
                               End Sub, TaskContinuationOptions.OnlyOnRanToCompletion)
       
    End Sub

    Private Sub ShowProgress()
        If Me.Progress.InvokeRequired Then
            Dim cross As New Action(AddressOf Me.ShowProgress)
            Me.Invoke(cross)
         
        Else
            If Me.Progress.Value = Me.Progress.Maximum Then
                Me.Progress.Value = Me.Progress.Minimum
            Else
                Me.Progress.Increment(10)
            End If
            Me.Progress.Refresh()

        End If


    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles button.Click
        Start()
    End Sub
End Class
