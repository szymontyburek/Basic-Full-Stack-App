Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Reflection.Metadata.Ecma335
Imports Microsoft.Data.SqlClient

Public Class Employee
    Public Property name As String
    Public Property department As String
    Public Property responsibilities As String
End Class

Public Class SQLControl
    Public connection As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function GETquery() As List(Of Employee)
        Dim employees As New List(Of Employee)()

        connection.Open()

        Using command As New SqlCommand("SELECT e.firstName + ' ' + e.lastName AS Name, d.title AS Department, d.description AS Responsibilities FROM Employees e INNER JOIN Departments d ON e.departmentId=d.id", connection)
            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim employee As New Employee() With {
                        .name = reader.GetString(0),
                        .department = reader.GetString(1),
                    .responsibilities = reader.GetString(2)
                    }
                    employees.Add(employee)
                End While
            End Using
        End Using

        Return employees

        'Dim rtn As Object = New ExpandoObject()
        'rtn.message = ""

        'Try
        'connection.Open()

        'SQLCmd = New SqlCommand(Query, connection)

        'rtn.data = SQLCmd.ExecuteReader
        'rtn.success = True

        'Catch ex As Exception
        'rtn.success = False
        'rtn.message = ex
        'End Try

        'Return rtn
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
