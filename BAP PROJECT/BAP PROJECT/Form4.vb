Public Class Form4
    Dim sqlnya As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_kamar", conn)
        DS = New DataSet
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
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call panggildata()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        sqlnya = "insert into tb_kamar(kode_kamar,nama_kamar,fasilitas,fungsi,tarif,penanggung_jawab)values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "')"
        Call jalan()
        MsgBox("Data Berhasil Tersimpan")
        Call panggildata()
    End Sub
    Private Sub DataGridView1_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        TextBox1.Text = DataGridView1.Item(0, i).Value
        TextBox2.Text = DataGridView1.Item(1, i).Value
        TextBox3.Text = DataGridView1.Item(2, i).Value
        TextBox4.Text = DataGridView1.Item(3, i).Value
        TextBox5.Text = DataGridView1.Item(4, i).Value
        TextBox6.Text = DataGridView1.Item(5, i).Value
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs)
        sqlnya = "UPDATE tb_kamar set nama_kamar='" & TextBox2.Text & "', fasilitas = '" & TextBox3.Text & "', fungsi ='" & TextBox4.Text & "',tarif='" & TextBox5.Text & "',penanggung_jawab='" & TextBox6.Text & "' where kode_kamar='" & TextBox1.Text & "'"
        Call jalan()
        MsgBox("Data Berhasil Terubah")
        Call panggildata()
    End Sub
    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs)
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_kamar where nama_kamar like '%" & TextBox7.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_kamar")
        DataGridView1.DataSource = DS.Tables("tb_kamar")
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs)
        sqlnya = "delete from tb_kamar where kode_kamar='" & TextBox1.Text & "'"
        Call jalan()
        MsgBox("Data Berhasil Terhapus")
        Call panggildata()
    End Sub
End Class