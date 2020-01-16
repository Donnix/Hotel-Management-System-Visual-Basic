Public Class Transaksi
    Dim sqlnya As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT kd_transaksi,kd_pelanggan,kd_kamar,nama_kamar,kelas,jenis,tarif,lama_inap,total_bayar FROM Query2 where kd_transaksi='" & TextBox13.Text & "'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "Query2")
        DataGridView1.DataSource = DS.Tables("Query2")
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
        'TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox11.Text = ""
        PictureBox1.ImageLocation = ""
    End Sub
    Private Sub Transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call panggildata()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim a As String
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = "select * from tb_transaksi where kd_kamar='" & TextBox13.Text & "'"
        RD = objcmd.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            TextBox3.Text = RD.Item("nama_kamar")
            TextBox4.Text = RD.Item("kelas")
            TextBox5.Text = RD.Item("status")
            TextBox11.Text = RD.Item("tarif")
            TextBox13.Text = RD.Item("kd_kamar")
            PictureBox1.ImageLocation = RD.Item("foto")
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Else
            a = MsgBox("Maaf kode kamar yang anda masukan tidak tersedia, apakah anda ingin melihat daftar kamar tersedia?", vbYesNo, "Hotel Says")
            If a = vbYes Then
                popup.Show()
            Else
                MsgBox("Kode Tidak tersedia")
            End If
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        TextBox7.Text = Val(TextBox6.Text) * Val(TextBox11.Text)
        TextBox8.Text = TextBox7.Text
        If Not IsNumeric(TextBox6.Text) And Not TextBox6.Text = "" Then
            TextBox6.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim x As Integer
        Dim a As Integer
        Dim b As Integer
        If TextBox13.Text = "" Or TextBox2.Text = "" Or TextBox6.Text = "" Or TextBox7.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            x = Val(TextBox1.Text) + 1
            b = Val(TextBox2.Text) + 1
            a = Val(TextBox13.Text) + 1

            sqlnya = "insert into tb_transaksi(`kd_transaksi`,`kd_kamar`,`lama_inap`,`total_bayar`) values('" & TextBox2.Text & "','" & TextBox13.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')"
            Call jalan()
            MsgBox("Data Berhasil Tersimpan")
            TextBox1.Text = x
            TextBox2.Text = b
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""
            TextBox12.Text = ""
            TextBox13.Text = a
            Call panggildata()
        End If
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = "select sum(total_bayar) as total_bayar from tb_transaksi where kd_transaksi='" & TextBox2.Text & "'"
        RD = objcmd.ExecuteReader()
        RD.Read()
        If RD.HasRows > 1 Then
            TextBox8.Text = RD.Item("total_bayar")
        End If
        MDIParent1.DataKamarKosongToolStripMenuItem.PerformClick()
        print.DataGridView1.DataSource = Me.DataGridView1.DataSource
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox10.Text = Val(TextBox9.Text) - Val(TextBox8.Text)
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Dim x, y, m As String
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        If e.ColumnIndex = 0 Then
            x = DataGridView1.Item("kd_kamar", i).Value
            y = DataGridView1.Item("kd_transaksi", i).Value
            sqlnya = "delete from tb_transaksi where kd_kamar='" & x & "' and kd_transaksi='" & y & "'"
            Call jalan()
            m = MsgBox("Apakah Anda yakin ?", vbYesNo, "Hotel Says")
            If m = vbYes Then
                Dim objcmd As New System.Data.OleDb.OleDbCommand
                objcmd.Connection = conn
                objcmd.CommandType = CommandType.Text
                objcmd.CommandText = "select sum(total_bayar) as total_bayar from tb_transaksi where kd_transaksi='" & TextBox2.Text & "'"
                RD = objcmd.ExecuteReader()
                RD.Read()
                If RD.HasRows Then
                    TextBox8.Text = RD.Item("total_bayar")
                Else
                    MsgBox("Data Habis")
                End If
                Call panggildata()
            End If
        End If
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        If Not IsNumeric(TextBox11.Text) And Not TextBox11.Text = "" Then
            TextBox11.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If Not IsNumeric(TextBox2.Text) And Not TextBox2.Text = "" Then
            TextBox2.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        If Not IsNumeric(TextBox7.Text) And Not TextBox7.Text = "" Then
            TextBox7.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        If Not IsNumeric(TextBox8.Text) And Not TextBox8.Text = "" Then
            TextBox8.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged
        If Not IsNumeric(TextBox9.Text) And Not TextBox9.Text = "" Then
            TextBox9.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

    Private Sub TextBox10_TextChanged(sender As Object, e As EventArgs) Handles TextBox10.TextChanged
        If Not IsNumeric(TextBox10.Text) And Not TextBox10.Text = "" Then
            TextBox10.Clear()
            MsgBox("Maaf Harus Diisi Dengan Angka")
        End If
    End Sub

End Class
