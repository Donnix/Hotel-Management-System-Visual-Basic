Public Class Form3
    Dim sqlnya As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_kamar", conn)
        DS.Clear()
        DA.Fill(DS, "tb_kamar")
        DataGridView1.DataSource = DS.Tables("tb_kamar")
        DataGridView1.Enabled = True
    End Sub



    Sub jalan()
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = sqlnya
        objcmd.ExecuteNonQuery()
        objcmd.Dispose()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""

    End Sub

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        panggildata()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "insert into Tb_Kamar(kd_kamar,nama_kamar,kelas,jenis,tarif,kode_petugas,status) values(" & TextBox1.Text & ",'" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "'," & TextBox5.Text & "," & TextBox6.Text & ",'" & TextBox7.Text & "')"
            Call jalan()
            MsgBox("Data Berhasil Tersimpan")
            Call panggildata()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class