Imports System.Data.OleDb

Module Module1
    
    Public con As New OleDbConnection
    Public cmd As New OleDbCommand
    Public da As OleDbDataAdapter
    Public ds As DataSet

    Sub testCon()
        Try
            con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\REYJANDB.accdb"
            con.Open()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

End Module
