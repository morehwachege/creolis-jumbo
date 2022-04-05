Imports MySql.Data.MySqlClient
Public Class Management

    Public cmd As MySqlCommand
    Public conn As New MySqlConnection
    Public id As Integer
    Public rd As MySqlDataReader
    Public minStr As String


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        conn.ConnectionString = "server=localhost; database=vbapp; username=root; password=;"
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        displayData()

    End Sub

    Public Sub displayData()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from students"
        cmd.ExecuteNonQuery()
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Public Sub minimumScore()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select min(student_grade) from students"
        cmd.ExecuteNonQuery()
        rd = cmd.ExecuteReader()
        If rd.Read Then

            Dim idSql As Integer = rd.GetInt32(0)
            minStr = rd.GetString(0)
            Dim minInt As Integer = rd.GetInt32(0)
            txtMinScore.Text = minInt


        End If
        rd.Close()
    End Sub

    Public Sub maximumScore()
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select max(student_grade) from students"
        cmd.ExecuteNonQuery()
        rd = cmd.ExecuteReader()
        If rd.Read Then

            Dim idSql As Integer = rd.GetInt32(0)
            minStr = rd.GetString(0)
            Dim maxInt As Integer = rd.GetInt32(0)
            txtMaxScore.Text = maxInt
        End If
        rd.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "INSERT INTO students VALUES ('NULL','" + txtName.Text + "','" + txtScore.Text + "')"
        cmd.ExecuteNonQuery()
        txtName.Text = ""
        txtScore.Text = ""
        displayData()
        minimumScore()
        maximumScore()
        MessageBox.Show("Data Saved Successfully")
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        id = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString)
        cmd = conn.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "delete from students where id=" & id & ""
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex, "Error")
        End Try
        Dim dt As New DataTable
        Dim da As New MySqlDataAdapter(cmd)
        da.Fill(dt)
        Dim dr As MySqlDataReader
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        While dr.Read
            txtName.Text = dr.GetString(1).ToString
            txtScore.Text = dr.GetString(2).ToString
        End While
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()

        Try
            id = Convert.ToInt32(DataGridView1.SelectedCells.Item(0).Value.ToString)
            cmd = conn.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "delete from students where id=" & id & ""
            cmd.ExecuteNonQuery()
            txtName.Text = ""
            txtScore.Text = ""

            displayData()
            'MessageBox.Show("Deleted Successfully")

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Me.Refresh()
    End Sub

End Class
