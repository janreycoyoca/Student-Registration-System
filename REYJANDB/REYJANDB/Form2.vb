Imports System.Data.OleDb

Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\REYJANDB.accdb"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Please Fill all Fields")

            ElseIf TextBox2.Text = TextBox3.Text = False Then
                MsgBox("Password did not match")
            Else
                ds = New DataSet
                da = New OleDbDataAdapter("INSERT INTO [Users] ([Username],[Password]) VALUES " &
                                          "('" & TextBox1.Text & "','" & TextBox2.Text & "')", con)
                da.Fill(ds, "Users")
                MsgBox("Registration Successful")
                TextBox3.Clear()
                TextBox2.Clear()
                TextBox1.Clear()
            End If
        Catch ex As Exception
            MsgBox("Username already exist")
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub

    Private Sub CheckBox1_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar And TextBox3.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
            TextBox3.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox3.UseSystemPasswordChar = True
        End If
    End Sub

End Class