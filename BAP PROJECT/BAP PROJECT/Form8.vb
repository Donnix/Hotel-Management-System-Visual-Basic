﻿Public Class form8
    Dim jk As Boolean
    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        Form1.tbkamar_transaksi.Text = Me.DataGridView1.Item(0, i).Value
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM Tb_Kamar where kd_kamar like '%" & TextBox1.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "Tb_Kamar")
        DataGridView1.DataSource = DS.Tables("Tb_Kamar")
    End Sub

    Dim sqlnya As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM Tb_Kamar", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "Tb_Kamar")
        DataGridView1.DataSource = DS.Tables("Tb_Kamar")
        DataGridView1.Enabled = True
    End Sub

    Private Sub popup2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call panggildata()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class