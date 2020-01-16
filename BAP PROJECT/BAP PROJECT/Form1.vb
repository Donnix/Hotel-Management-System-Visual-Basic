Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call panggildatakelolakamar()
        Call panggiltamu()
        Call panggilpetugas()
        Call panggiltransaksi()
        Call kodeautokamar()
        Call kodeautotamu()
        Call kodeautopetugas()
        Call kodeautotransaksi()

    End Sub

    Dim sqlnya As String
    Dim jk As String
    Dim jkp As String
    Sub panggildatakelolakamar()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM Tb_Kamar", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "Tb_Kamar")
        gridkamar.DataSource = DS.Tables("Tb_Kamar")
        gridkamar.Enabled = True
    End Sub
    Sub kodeautokamar()
        konek()
        CMD = New OleDb.OleDbCommand("select * from tb_Kamar order by kd_kamar desc", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            tbkode_kamar.Text = "KK" + "001"
        Else
            tbkode_kamar.Text = Val(Microsoft.VisualBasic.Mid(RD.Item("kd_kamar").ToString, 4, 3)) + 1
            If Len(tbkode_kamar.Text) = 1 Then
                tbkode_kamar.Text = "KK00" & tbkode_kamar.Text & ""
            ElseIf Len(tbkode_kamar.Text) = 2 Then
                tbkode_kamar.Text = "KK0" & tbkode_kamar.Text & ""
            ElseIf Len(tbkode_kamar.Text) = 3 Then
                tbkode_kamar.Text = "KK" & tbkode_kamar.Text & ""
            End If
        End If
    End Sub
    Sub jalankelolakamar()
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = sqlnya
        objcmd.ExecuteNonQuery()
        objcmd.Dispose()
        tbkode_kamar.Text = ""
        tbnama_kamar.Text = ""
        cmbjenis.SelectedIndex = -1
        tbtarif.Text = ""
        cmbkelas.SelectedIndex = -1
        tbsource.Text = ""
        tbkode_petugas.Text = ""
        Cmbstatus.SelectedIndex = -1
        PictureBox4.ImageLocation = ""

    End Sub
    Public Sub Query(ByVal cmdText As String, ByVal table As String)
        konek()
        DA = New OleDb.OleDbDataAdapter(cmdText, conn)
        If DS.Tables.Contains(table) Then
            DS.Tables(table).Clear()
        End If
        DA.Fill(DS, table)
    End Sub
    Public Sub NonQuery(ByVal cmdText As String)
        konek()
        CMD.CommandText = cmdText
        CMD.Connection = conn
        CMD.ExecuteNonQuery()

    End Sub



    Private Sub simpankelolakamar_Click(sender As Object, e As EventArgs) Handles btnsimpankelolakamar.Click
        If tbkode_kamar.Text = "" Or tbnama_kamar.Text = "" Or cmbjenis.SelectedItem = "" Or tbtarif.Text = "" Or tbsource.Text = "" Or tbkode_petugas.Text = "" Or Cmbstatus.SelectedItem = "" Or Cmbstatus.SelectedItem = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "insert into Tb_Kamar(kd_kamar,nama_kamar,kelas,jenis,tarif,foto,kode_petugas,status) values('" & tbkode_kamar.Text & "','" & tbnama_kamar.Text & "','" & cmbkelas.SelectedItem & "','" & cmbjenis.SelectedItem & "'," & Val(tbtarif.Text) & ",'" & tbsource.Text & "','" & tbkode_petugas.Text & "','" & Cmbstatus.SelectedItem & "')"
            Call jalankelolakamar()
            MsgBox("Data Berhasil Tersimpan")
            Call panggildatakelolakamar()
        End If
    End Sub


    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles btnhapuskelolakamar.Click
        If tbkode_kamar.Text = "" Or tbnama_kamar.Text = "" Or cmbjenis.SelectedIndex = "" Or tbtarif.Text = "" Or tbsource.Text = "" Or tbkode_petugas.Text = "" Or Cmbstatus.SelectedItem = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "delete from Tb_Kamar where kd_kamar='" & tbkode_kamar.Text & "'"
            Call jalankelolakamar()
            MsgBox("Data Berhasil Terhapus")
            Call panggildatakelolakamar()
        End If
    End Sub



    Private Sub gridkamar_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles gridkamar.RowHeaderMouseClick
        Dim i As Integer
        i = gridkamar.CurrentRow.Index
        If i = -1 Then
            MsgBox("Data Telah Habis")
        Else
            btnhapuskelolakamar.Visible = True
            tbkode_kamar.Text = gridkamar.Item(0, i).Value
            tbnama_kamar.Text = gridkamar.Item(1, i).Value
            cmbkelas.Text = gridkamar.Item(2, i).Value
            cmbjenis.Text = gridkamar.Item(3, i).Value
            tbtarif.Text = gridkamar.Item(4, i).Value
            tbsource.Text = gridkamar.Item(5, i).Value
            tbkode_petugas.Text = gridkamar.Item(6, i).Value
            Cmbstatus.Text = gridkamar.Item(7, i).Value
            PictureBox4.ImageLocation = gridkamar.Item(5, i).Value
            PictureBox4.SizeMode = PictureBoxSizeMode.StretchImage
            btnsimpankelolakamar.Enabled = False
            btnhapuskelolakamar.Enabled = True
            btnupdatekelolakamar.Enabled = True
            tbkode_kamar.Enabled = False
            Call panggildatakelolakamar()

        End If
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles close.Click
        End
    End Sub

    Private Sub Btnupdatekelolakamar_Click(sender As Object, e As EventArgs) Handles btnupdatekelolakamar.Click
        tbkode_kamar.Enabled = True
        If tbkode_kamar.Text = "" Or tbnama_kamar.Text = "" Or cmbjenis.SelectedItem = "" Or tbtarif.Text = "" Or tbsource.Text = "" Or tbkode_petugas.Text = "" Or Cmbstatus.SelectedItem = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "UPDATE Tb_kamar set nama_kamar='" & tbnama_kamar.Text & "',kelas='" & cmbkelas.SelectedItem & "',jenis='" & cmbjenis.SelectedIndex & "',tarif=" & tbtarif.Text & ", foto='" & tbsource.Text & "',kode_petugas='" & tbkode_petugas.Text & "',status='" & Cmbstatus.SelectedItem & "' where kd_kamar='" & tbkode_kamar.Text & "'"
            Call jalankelolakamar()
            MsgBox("Data Berhasil Terubah")
            Call panggildatakelolakamar()
            btnsimpankelolakamar.Enabled = True
            btnhapuskelolakamar.Enabled = False
            btnupdatekelolakamar.Enabled = False

            PictureBox4.ImageLocation = ""
        End If
    End Sub
    Dim gam As String
    Private PathFile As String = Nothing
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog1.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|GIF Files(*.gif)|*.gif|PNG Files(*.png)|*.png|BMP Files(*.bmp)|*.bmp|TIFF Files(*.tiff)|*.tiff"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox4.SizeMode = PictureBoxSizeMode.StretchImage
            PictureBox4.Image = New Bitmap(OpenFileDialog1.FileName)
            Button5.Enabled = True
            PathFile = OpenFileDialog1.FileName
            tbsource.Text = OpenFileDialog1.FileName
            gam = OpenFileDialog1.FileName
            PictureBox4.Image = Image.FromFile(tbsource.Text)
        End If
    End Sub


    Private Sub Pencarian_TextChanged(sender As Object, e As EventArgs) Handles tbpencariankelolakamar.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM Tb_kamar where nama_kamar like '%" & tbpencariankelolakamar.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "Tb_kamar")
        gridkamar.DataSource = DS.Tables("Tb_kamar")
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Paneltamu.BringToFront()
        Query("Select*from tb_tamu", "tb_tamu")
        gridtamu.DataSource = DS.Tables("tb_tamu")
    End Sub
    Sub panggiltamu()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_tamu", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_tamu")
        gridtamu.DataSource = DS.Tables("tb_tamu")
        gridtamu.Enabled = True
    End Sub
    Sub jalantamu()
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = sqlnya
        objcmd.ExecuteNonQuery()
        objcmd.Dispose()
        tbkd_tamu.Text = ""
        tbnamatamu.Text = ""
        jklaki.Checked = False
        jkperempuan.Checked = False
        dttamu.Text = ""
        tbalamattamu.Text = ""
        tbnohptamu.Text = ""

    End Sub

    Sub kodeautotamu()
        konek()
        CMD = New OleDb.OleDbCommand("select * from tb_tamu order by kd_tamu desc", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            tbkd_tamu.Text = "KT" + "001"
        Else
            tbkd_tamu.Text = Val(Microsoft.VisualBasic.Mid(RD.Item("kd_tamu").ToString, 4, 3)) + 1
            If Len(tbkd_tamu.Text) = 1 Then
                tbkd_tamu.Text = "KT00" & tbkd_tamu.Text & ""
            ElseIf Len(tbkd_tamu.Text) = 2 Then
                tbkd_tamu.Text = "KT0" & tbkd_tamu.Text & ""
            ElseIf Len(tbkd_tamu.Text) = 3 Then
                tbkd_tamu.Text = "KT" & tbkd_tamu.Text & ""
            End If
        End If

    End Sub
    Private Sub Jklaki_CheckedChanged(sender As Object, e As EventArgs) Handles jklaki.CheckedChanged, jkperempuan.CheckedChanged
        If (jklaki.Checked) Then
            jk = "Laki Laki"
        ElseIf (jkperempuan.Checked) Then
            jk = "Perempuan"
        End If
    End Sub
    Private Sub btnsimpantamu_Click(sender As Object, e As EventArgs) Handles btnsimpantamu.Click


        If tbkd_tamu.Text = "" Or tbnamatamu.Text = "" Or jk = "" Or dttamu.Text = "" Or tbalamattamu.Text = "" Or tbnohptamu.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "insert into tb_tamu(kd_tamu,nama_tamu,jk,ttl_tamu,alamat,nohp_tamu) values('" & tbkd_tamu.Text & "','" & tbnamatamu.Text & "','" & jk & "','" & dttamu.Text & "','" & tbalamattamu.Text & "','" & tbnohptamu.Text & "')"
            Call jalantamu()
            MsgBox("Data Berhasil Tersimpan")
            Call panggiltamu()
            kodeautotamu()
        End If
    End Sub

    Private Sub Btnhapustamu_Click(sender As Object, e As EventArgs) Handles btnhapustamu.Click
        If tbkd_tamu.Text = "" Or tbnamatamu.Text = "" Or dttamu.Text = "" Or tbalamattamu.Text = "" Or tbnohptamu.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "delete from tb_tamu where kd_tamu='" & tbkd_tamu.Text & "'"
            Call jalantamu()
            MsgBox("Data Berhasil Terhapus")
            Call panggiltamu()
            btnsimpantamu.Enabled = True
            btnhapustamu.Enabled = False
            btnupdatetamu.Enabled = False
            kodeautotamu()
        End If
    End Sub

    Private Sub Btnupdatetamu_Click(sender As Object, e As EventArgs) Handles btnupdatetamu.Click

        tbkd_tamu.Enabled = True
        If tbkd_tamu.Text = "" Or tbnamatamu.Text = "" Or dttamu.Text = "" Or tbalamattamu.Text = "" Or tbnohptamu.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "UPDATE tb_tamu set nama_tamu='" & tbnamatamu.Text & "',jk='" & jk & "',ttl_tamu='" & dttamu.Text & "',alamat_tamu='" & tbalamattamu.Text & "', nohp_tamu='" & tbnohptamu.Text & "' where kd_tamu='" & tbkd_tamu.Text & "'"

            Call jalantamu()
            MsgBox("Data Berhasil Terubah")
            Call panggiltamu()
            btnsimpantamu.Enabled = True
            btnhapustamu.Enabled = False
            btnupdatetamu.Enabled = False
            
        End If
    End Sub

    Private Sub gridtamu_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles gridtamu.RowHeaderMouseClick
        Dim i As Integer
        i = gridtamu.CurrentRow.Index
        If i = -1 Then
            MsgBox("Data Telah Habis")
        Else
            btnhapustamu.Visible = True
            tbkd_tamu.Text = gridtamu.Item(0, i).Value
            tbnamatamu.Text = gridtamu.Item(1, i).Value
            Dim jk = gridtamu.Item(2, i).Value
            If jk = "Laki Laki" Then
                jklaki.Checked = True
                jkperempuan.Checked = False
            ElseIf jk = "Perempuan" Then
                jkperempuan.Checked = True
                jklaki.Checked = False
            End If
            dttamu.Text = gridtamu.Item(3, i).Value
            tbalamattamu.Text = gridtamu.Item(4, i).Value
            tbnohptamu.Text = gridtamu.Item(5, i).Value
            btnsimpantamu.Enabled = False
            btnhapustamu.Enabled = True
            btnupdatetamu.Enabled = True
            tbkd_tamu.Enabled = False
        End If



    End Sub

    Private Sub Tbpencariantamu_TextChanged(sender As Object, e As EventArgs) Handles tbpencariantamu.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_tamu where nama_tamu like '%" & tbpencariantamu.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_tamu")
        gridtamu.DataSource = DS.Tables("tb_tamu")
        tbkd_petugas.Show()

    End Sub

    Sub panggilpetugas()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_petugas", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_petugas")
        gridpetugas.DataSource = DS.Tables("tb_petugas")
        gridpetugas.Enabled = True
    End Sub
    Sub jalanpetugas()
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = sqlnya
        objcmd.ExecuteNonQuery()
        objcmd.Dispose()
        tbkd_petugas.Text = ""
        tbnama_petugas.Text = ""
        tbnohp_petugas.Text = ""

    End Sub

    Sub kodeautopetugas()
        konek()
        CMD = New OleDb.OleDbCommand("select * from tb_petugas order by kd_petugas desc", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            tbkd_petugas.Text = "KP" + "001"
        Else
            tbkd_petugas.Text = Val(Microsoft.VisualBasic.Mid(RD.Item("kd_petugas").ToString, 4, 3)) + 1
            If Len(tbkd_petugas.Text) = 1 Then
                tbkd_petugas.Text = "KP00" & tbkd_petugas.Text & ""
            ElseIf Len(tbkd_petugas.Text) = 2 Then
                tbkd_petugas.Text = "KP0" & tbkd_petugas.Text & ""
            ElseIf Len(tbkd_tamu.Text) = 3 Then
                tbkd_petugas.Text = "KP" & tbkd_petugas.Text & ""
            End If
        End If
    End Sub
    Private Sub Jklaki_petugas_CheckedChanged(sender As Object, e As EventArgs) Handles jklaki_petugas.CheckedChanged, jkperempuan_petugas.CheckedChanged
        If (jklaki_petugas.Checked) Then
            jkp = "Laki Laki"
        ElseIf (jkperempuan_petugas.Checked) Then
            jkp = "Perempuan"
        End If
    End Sub
    Private Sub Btnsimpan_petugas_Click(sender As Object, e As EventArgs) Handles btnsimpan_petugas.Click
        If tbkd_petugas.Text = "" Or tbnama_petugas.Text = "" Or jkp = "" Or tbnohp_petugas.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "insert into tb_petugas(kd_petugas,nama_petugas,jk,no_hp) values('" & tbkd_petugas.Text & "','" & tbnama_petugas.Text & "','" & jkp & "','" & tbnohp_petugas.Text & "')"
            Call jalanpetugas()
            MsgBox("Data Berhasil Tersimpan")
            Call panggilpetugas()
        End If
    End Sub

    Private Sub Btnhapus_petugas_Click(sender As Object, e As EventArgs) Handles btnhapus_petugas.Click
        If tbkd_petugas.Text = "" Or tbnama_petugas.Text = "" Or jkp = "" Or tbnohp_petugas.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "delete from tb_petugas where kd_petugas='" & tbkd_petugas.Text & "'"
            Call jalanpetugas()
            MsgBox("Data Berhasil Terhapus")
            Call panggilpetugas()
        End If
    End Sub

    Private Sub Btnupdate_petugas_Click(sender As Object, e As EventArgs) Handles btnupdate_petugas.Click
        tbkd_petugas.Enabled = True
        If tbkd_petugas.Text = "" Or tbnama_petugas.Text = "" Or jkp = "" Or tbnohp_petugas.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "UPDATE tb_petugas set nama_petugas='" & tbnama_petugas.Text & "',jk='" & jkp & "',no_hp='" & tbnohp_petugas.Text & "' where kd_petugas='" & tbkd_petugas.Text & "'"
            Call jalanpetugas()
            MsgBox("Data Berhasil Terubah")
            Call panggilpetugas()
            btnsimpan_petugas.Enabled = True
            btnhapus_petugas.Enabled = False
            btnupdate_petugas.Enabled = False
        End If
    End Sub

    Private Sub gridpetugas_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles gridpetugas.RowHeaderMouseClick
        Dim i As Integer
        i = gridpetugas.CurrentRow.Index
        If i = -1 Then
            MsgBox("Data Telah Habis")
        Else
            btnhapus_petugas.Visible = True
            tbkd_petugas.Text = gridpetugas.Item(0, i).Value
            tbnama_petugas.Text = gridpetugas.Item(1, i).Value
            Dim j = gridpetugas.Item(2, i).Value
            If j = "Laki Laki" Then
                jklaki_petugas.Checked = True
                jkperempuan_petugas.Checked = False
            ElseIf j = "Perempuan" Then
                jkperempuan_petugas.Checked = True
                jklaki_petugas.Checked = False
            End If
            tbnohp_petugas.Text = gridpetugas.Item(3, i).Value
            btnsimpan_petugas.Enabled = False
            btnhapus_petugas.Enabled = True
            btnupdate_petugas.Enabled = True
            tbkode_petugas.Enabled = False
        End If

    End Sub

    Private Sub Tbpencarian_petugas_TextChanged(sender As Object, e As EventArgs) Handles tbpencarian_petugas.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_petugas where nama_petugas like '%" & tbpencarian_petugas.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_petugas")
        gridpetugas.DataSource = DS.Tables("tb_petugas")
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
        End
    End Sub

    Private Sub PictureBox10_Click(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub PictureBox5_Click_1(sender As Object, e As EventArgs) Handles PictureBox5.Click
        End
    End Sub

    Private Sub PictureBox8_Click(sender As Object, e As EventArgs)
        End
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        panelTransaksi.BringToFront()
        Query("Select*from tb_transaksi", "tb_transaksi")
        gridtransaksi.DataSource = DS.Tables("tb_transaksi")
    End Sub
    Sub panggiltransaksi()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_transaksi", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_transaksi")
        gridtransaksi.DataSource = DS.Tables("tb_transaksi")
        gridtransaksi.Enabled = True
    End Sub
    Sub jalantransaksi()
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        Call konek()
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = sqlnya
        objcmd.ExecuteNonQuery()
        objcmd.Dispose()
        tbkode_transaksi.Text = ""
        tbkode_pelanggan1.Text = ""
        tbkamar_transaksi.Text = ""
        dttgl_checkin.ResetText()
        dttgl_checkout.ResetText()
        tblama_inap.Text = ""
        dttgl_transaksi.ResetText()
        tbtotal_bayar.Text = ""
        tbalamattamu.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""

    End Sub

    Sub kodeautotransaksi()
        konek()
        CMD = New OleDb.OleDbCommand("select * from tb_transaksi order by kd_transaksi desc", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            tbkode_transaksi.Text = "KTR" + "001"
        Else
            tbkode_transaksi.Text = Val(Microsoft.VisualBasic.Mid(RD.Item("kd_transaksi").ToString, 4, 3)) + 1
            If Len(tbkode_transaksi.Text) = 1 Then
                tbkode_transaksi.Text = "KTR00" & tbkode_transaksi.Text & ""
            ElseIf Len(tbkode_transaksi.Text) = 2 Then
                tbkode_transaksi.Text = "KTR0" & tbkode_transaksi.Text & ""
            ElseIf Len(tbkode_transaksi.Text) = 3 Then
                tbkode_transaksi.Text = "KTR" & tbkode_transaksi.Text & ""
            End If
        End If
    End Sub
    Private Sub Simpantransaksi_Click(sender As Object, e As EventArgs) Handles simpantransaksi.Click

        If tbkode_transaksi.Text = "" Or tbkode_pelanggan1.Text = "" Or tbkamar_transaksi.Text = "" Or tblama_inap.Text = "" Or tbtotal_bayar.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else



            sqlnya = "insert into tb_transaksi(`kd_transaksi`,`kd_pelanggan`,`kd_kamar`,`tgl_check_in`,`tgl_check_out`,`lama_inap`,`tgl_transaksi`,`total_bayar`) values('" & tbkode_transaksi.Text & "','" & tbkode_pelanggan1.Text & "','" & tbkamar_transaksi.Text & "','" & dttgl_checkin.Text & "','" & dttgl_checkout.Text & "','" & tblama_inap.Text & "','" & dttgl_transaksi.Text & "','" & tbtotal_bayar.Text & "')"
            Call jalantransaksi()
            MsgBox("Data Berhasil Tersimpan")


            dttgl_checkin.Text = ""
            dttgl_checkout.Text = ""
            tblama_inap.Text = ""
            dttgl_transaksi.Text = ""
            tbtotal_bayar.Text = ""
            tbalamattamu.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""

            Call panggiltransaksi()
        End If
        Dim objcmd As New System.Data.OleDb.OleDbCommand
        objcmd.Connection = conn
        objcmd.CommandType = CommandType.Text
        objcmd.CommandText = "select sum(total_bayar) as total_bayar from tb_transaksi where kd_transaksi='" & tbkode_kamar.Text & "'"
        RD = objcmd.ExecuteReader()
        RD.Read()
        If RD.HasRows > 1 Then
            tbtotal_bayar.Text = RD.Item("total_bayar")
        End If

    End Sub


    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        PopUp.Show()
    End Sub

    Private Sub Hapustransaksi_Click(sender As Object, e As EventArgs) Handles hapustransaksi.Click
        If tbkode_transaksi.Text = "" Or tbkode_pelanggan1.Text = "" Or tbkamar_transaksi.Text = "" Or dttgl_checkin.Text = "" Or dttgl_checkout.Text = "" Or tblama_inap.Text = "" Or dttgl_transaksi.Text = "" Or tbtotal_bayar.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "delete from tb_transaksi where kd_transaksi='" & tbkode_transaksi.Text & "'"
            Call jalantransaksi()
            MsgBox("Data Berhasil Terhapus")
            Call panggiltransaksi()
        End If
    End Sub

    Private Sub Updatetransaksi_Click(sender As Object, e As EventArgs) Handles updatetransaksi.Click
        tbkode_transaksi.Enabled = True
        If tbkode_transaksi.Text = "" Or tbkode_pelanggan1.Text = "" Or tbkamar_transaksi.Text = "" Or dttgl_checkin.Text = "" Or dttgl_checkout.Text = "" Or tblama_inap.Text = "" Or dttgl_transaksi.Text = "" Or tbtotal_bayar.Text = "" Then
            MsgBox("Maaf Data yang Anda masukan masih kurang lengkap")
        Else
            sqlnya = "UPDATE tb_transaksi set kd_pelanggan='" & tbkode_pelanggan1.Text & "',tgl_check_in='" & dttgl_checkin.Text & "',tgl_check_out='" & dttgl_checkout.Text & "',lama_inap='" & tblama_inap.Text & "',tgl_transaksi='" & dttgl_transaksi.Text & "' where kd_transaksi='" & tbkode_transaksi.Text & "'"
            Call jalantransaksi()
            MsgBox("Data Berhasil Terubah")
            Call panggiltransaksi()
            simpantransaksi.Enabled = True
            hapustransaksi.Enabled = False
            updatetransaksi.Enabled = False
        End If
    End Sub

    Private Sub gridtransaksi_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles gridtransaksi.RowHeaderMouseClick
        Dim i As Integer
        i = gridtransaksi.CurrentRow.Index
        If i = -1 Then
            MsgBox("Data Telah Habis")
        Else
            hapustransaksi.Visible = True
            tbkode_transaksi.Text = gridtransaksi.Item(0, i).Value
            tbkode_pelanggan1.Text = gridtransaksi.Item(1, i).Value
            tbkamar_transaksi.Text = gridtransaksi.Item(2, i).Value
            dttgl_checkin.Text = gridtransaksi.Item(3, i).Value
            dttgl_checkout.Text = gridtransaksi.Item(4, i).Value
            tblama_inap.Text = gridtransaksi.Item(5, i).Value
            dttgl_transaksi.Text = gridtransaksi.Item(6, i).Value
            tbtotal_bayar.Text = gridtransaksi.Item(7, i).Value
            simpantransaksi.Enabled = False
            hapustransaksi.Enabled = True
            updatetransaksi.Enabled = True
            tbkode_transaksi.Enabled = False
            Call panggildatakelolakamar()

        End If
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        End
    End Sub

    Private Sub Cmbkelas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbkelas.SelectedIndexChanged
        If cmbkelas.SelectedItem = "STANDARD" Then
            tbtarif.Text = "500000"
        ElseIf cmbkelas.SelectedItem = "SUPERIOR" Then
            tbtarif.Text = "700000"
        ElseIf cmbkelas.SelectedItem = "DELUXE" Then
            tbtarif.Text = "900000"
        ElseIf cmbkelas.SelectedItem = "SUITE" Then
            tbtarif.Text = "1000000"

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panelkelolakamar.BringToFront()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panelpetugas.BringToFront()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        form8.Show()
    End Sub

    Private Sub Dttgl_checkout_ValueChanged(sender As Object, e As EventArgs) Handles dttgl_checkout.ValueChanged

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs)
        form9.Show()
    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        form9.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim i As Integer = gridkamar.CurrentRow.Index
        Dim borrow As DateTime = Convert.ToDateTime(dttgl_checkout.Text)
        Dim back As DateTime = Convert.ToDateTime(dttgl_checkin.Text)
        Dim countdays As TimeSpan = back.Subtract(borrow)
        Dim totaldays = Convert.ToInt32(countdays.Days)
        If Convert.ToInt32(countdays.Days) >= 0 Then
            tblama_inap.Text = totaldays & "  Hari"
        End If
        Dim total As Integer = gridkamar.Item(4, i).Value * Val(tblama_inap.Text)
        tbtotal_bayar.Text = total
    End Sub


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If TextBox1.Text = "" Then
            MsgBox("Maaf Bayar Harus di isi",, "Informasi")
        Else
            Dim i As Integer = gridkamar.CurrentRow.Index
            Dim total As Integer = gridkamar.Item(4, i).Value * Val(tblama_inap.Text)
            TextBox2.Text = Val(TextBox1.Text) - total
            simpantransaksi.Enabled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not IsNumeric(TextBox1.Text) And Not TextBox1.Text = "" Then
            TextBox1.Text = ""
            MsgBox("maaaf harus di isi dengan angka")

        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress

    End Sub

    Private Sub Pencariantransaksi_TextChanged(sender As Object, e As EventArgs) Handles pencariantransaksi.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_transaksi where kd_transaksi like '%" & pencariantransaksi.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_transaksi")
        gridtransaksi.DataSource = DS.Tables("tb_transaksi")
    End Sub

    Private Sub Panelpetugas_Paint(sender As Object, e As PaintEventArgs) Handles Panelpetugas.Paint

    End Sub

    Private Sub Gridkamar_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridkamar.CellContentClick

    End Sub

    Private Sub Tbnohptamu_TextChanged(sender As Object, e As EventArgs) Handles tbnohptamu.TextChanged
        If Not IsNumeric(tbnohptamu.Text) And Not tbnohptamu.Text = "" Then
            tbnohptamu.Text = ""
            MsgBox("maaaf harus di isi dengan angka")
        End If
    End Sub
End Class