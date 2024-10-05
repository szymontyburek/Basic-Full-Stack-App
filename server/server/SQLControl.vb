Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient

Public Class SQLControl
    Private SQLCon As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function connect() As Boolean
        Try
            SQLCon.Open()
            SQLCon.Close()
            Return True
        Catch ex As Exception
            Console.WriteLine(ex)
            Return False
        End Try
    End Function
End Class
