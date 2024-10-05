Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Reflection.Metadata.Ecma335
Imports Microsoft.Data.SqlClient

Public Class SQLControl
    Private SQLCon As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function GETquery(Query As String) As ExpandoObject
        Dim rtn = CType(New ExpandoObject(), IDictionary(Of String, Object))
        rtn("message") = ""

        Try
            SQLCon.Open()

            SQLCmd = New SqlCommand(Query, SQLCon)

            rtn("data") = SQLCmd.ExecuteReader
            rtn("success") = True

            SQLCon.Close()
        Catch ex As Exception
            rtn("success") = False
            rtn("message") = ex
        End Try

        Return rtn
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
