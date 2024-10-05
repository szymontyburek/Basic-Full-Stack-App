Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Reflection.Metadata.Ecma335
Imports Microsoft.Data.SqlClient

Public Class SQLControl
    Public connection As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function GETquery(Query As String) As Task(Of ExpandoObject)
        Dim rtn = CType(New ExpandoObject(), IDictionary(Of String, Object))
        rtn("message") = ""

        Try
            connection.Open()

            SQLCmd = New SqlCommand(Query, connection)

            rtn("data") = SQLCmd.ExecuteReader
            rtn("success") = True

        Catch ex As Exception
            rtn("success") = False
            rtn("message") = ex
        End Try

        Return rtn
    End Function
    Public Function POSTquery() As Boolean
        Try
            connection.Open()
            connection.Close()
            Return True
        Catch ex As Exception
            Console.WriteLine(ex)
            Return False
        End Try
    End Function
End Class
