Imports System.Data.OleDb

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\REYJANDB.accdb"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                MsgBox("Please enter Username and Password")
            Else
                ds = New DataSet
                da = New OleDbDataAdapter("SELECT * FROM [Users] WHERE [Username] = '" & TextBox1.Text & "' And [Password] = '" & TextBox2.Text & "'", con)
                da.Fill(ds, "Users")

                If ds.Tables("Users").Rows.Count > 0 Then
                    Form3.Show()
                    TextBox1.Clear()
                    TextBox2.Clear()
                Else
                    MsgBox("Username and Password is Incorrect")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Form2.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If TextBox2.UseSystemPasswordChar = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
        End If
    End Sub
End Class
