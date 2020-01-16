Public Class PopUp
    Dim jk As Boolean
    Private Sub DataGridView1_RowHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        Form1.tbkode_pelanggan1.Text = Me.DataGridView1.Item("kd_tamu", i).Value
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_tamu where kd_tamu like '%" & TextBox1.Text & "%'", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_tamu")
        DataGridView1.DataSource = DS.Tables("tb_tamu")
    End Sub

    Dim sqlnya As String
    Sub panggildata()
        konek()
        DA = New OleDb.OleDbDataAdapter("SELECT * FROM tb_tamu", conn)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tb_tamu")
        DataGridView1.DataSource = DS.Tables("tb_tamu")
        DataGridView1.Enabled = True
    End Sub

    Private Sub popup2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call panggildata()
    End Sub

End Class