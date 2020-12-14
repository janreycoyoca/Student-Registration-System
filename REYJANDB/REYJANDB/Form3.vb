Imports System.Data.OleDb

Public Class Form3

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            testCon()
            displayData(False)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim query As String

            If isEdit Then
                query = "UPDATE Records SET Fname='" & TextBox1.Text & "',Lname='" & TextBox2.Text & "',Gender='" & ComboBox1.Text & "',Birthdate='" & DateTimePicker1.Text & "',Address='" & TextBox7.Text & "',ContactNo='" & TextBox3.Text & "',Course='" & ComboBox2.Text & "',YearLevel='" & ComboBox3.Text & "'WHERE ID =" & RecordsID & ""
            Else
                query = "INSERT INTO Records(Fname,Lname,Gender,Birthdate,Address,ContactNo,Course,YearLevel) VALUES('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & DateTimePicker1.Text & "','" & TextBox7.Text & "','" & TextBox3.Text & "','" & ComboBox2.Text & "','" & ComboBox3.Text & "')"
            End If

            If InsertUpdateDelete(query) Then
                If isEdit Then
                    MsgBox("Data Updated!")
                    isEdit = False
                    reset()
                Else
                    MsgBox("Data Saved!")
                    reset()
                End If
                TabControl1.SelectedIndex = 1
                displayData(False)
            Else
                If isEdit Then
                    MsgBox("Failed to Update!")
                    isEdit = False
                Else
                    MsgBox("Failed to Insert!")
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        reset()
        isEdit = False
    End Sub

    Sub reset()

        TextBox1.Text = ""
        TextBox2.Text = ""
        ComboBox1.Text = ""
        DateTimePicker1.Text = ""
        TextBox7.Text = ""
        TextBox3.Text = ""
        ComboBox2.Text = ""
        ComboBox3.Text = ""
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        displayData(True)
    End Sub

    Sub displayData(ByVal isSearch As Boolean)
        Try
            Dim query As String

            If isSearch Then
                query = "SELECT * FROM Records WHERE Lname LIKE '" & TextBox4.Text & "%'"
            Else
                query = "SELECT * FROM Records"
            End If

            Dim dr As OleDbDataReader

            cmd = New OleDbCommand(query, con)
            dr = cmd.ExecuteReader

            DataGridView1.Rows.Clear()

            While dr.Read
                DataGridView1.Rows.Add(dr("ID"), dr("Fname"), dr("Lname"), dr("Gender"), dr("Birthdate"), dr("Address"), dr("ContactNo"), dr("Course"), dr("YearLevel"), "Edit", "Delete")
            End While

            dr.Close()
            cmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Try
            If e.RowIndex < 0 Then
                Exit Sub
            End If

            Dim grid = DirectCast(sender, DataGridView)

            If TypeOf grid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then

                If grid.Columns(e.ColumnIndex).Name = "Column7" Then
                    TabControl1.SelectedIndex = 0

                    TextBox1.Text = CStr(grid.Rows(e.RowIndex).Cells(1).Value)
                    TextBox2.Text = CStr(grid.Rows(e.RowIndex).Cells(2).Value)
                    ComboBox1.Text = CStr(grid.Rows(e.RowIndex).Cells(3).Value)
                    DateTimePicker1.Text = CStr(grid.Rows(e.RowIndex).Cells(4).Value)
                    TextBox7.Text = CStr(grid.Rows(e.RowIndex).Cells(5).Value)
                    TextBox3.Text = CStr(grid.Rows(e.RowIndex).Cells(6).Value)
                    ComboBox2.Text = CStr(grid.Rows(e.RowIndex).Cells(7).Value)
                    ComboBox3.Text = CStr(grid.Rows(e.RowIndex).Cells(8).Value)
                    isEdit = True
                    RecordsID = CInt(grid.Rows(e.RowIndex).Cells(0).Value)

                ElseIf grid.Columns(e.ColumnIndex).Name = "Column8" Then
                    If MsgBox("Are you sure you want to delete?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                        If InsertUpdateDelete("DELETE FROM Records WHERE ID=" & grid.Rows(e.RowIndex).Cells(0).Value & "") Then
                            displayData(False)
                        Else
                            MsgBox("Failed to  Delete!")
                            displayData(False)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

  
End Class