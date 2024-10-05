Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Microsoft.Data.SqlClient

Public Class SQLControl
    Private SQLCon As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function GETquery(Query As String) As Boolean
        Try
            SQLCon.Open()

            SQLCmd = New SqlCommand(Query, SQLCon)
            Dim R As SqlDataReader = SQLCmd.ExecuteReader

            SQLCon.Close()

            Return True
        Catch ex As Exception
            Console.WriteLine(ex)
            Return False
        End Try
    End Function
    Public Function POSTquery() As Boolean
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
