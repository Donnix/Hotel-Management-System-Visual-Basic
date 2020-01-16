Public Class FormLogin
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "admin" And TextBox2.Text = "password" Then
            MsgBox("selamat datang")
            Form1.Show()
            Me.Hide()
        Else
            MsgBox("username salah")


        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class