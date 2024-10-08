Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Dynamic
Imports System.Reflection.Metadata.Ecma335
Imports Microsoft.Data.SqlClient

Public Class Employee
    Private Property id As Integer
    Public Property firstName As String
    Public Property lastName As String
    Private Property deparmentId As Integer
    Public Property department As String
    Public Property description As String
End Class

Public Class SQLControl
    Private connection As New SqlConnection With {.ConnectionString = "Server=(localdb)\MSSQLLocalDB; Database=Pure Wafer; Integrated Security=SSPI"}
    Private SQLCmd As SqlCommand

    Public Function GETquery(idAsStr As String) As Object
        Dim rtn As Object = New ExpandoObject()
        Dim employees As New List(Of Employee)()
        rtn.message = ""

        Try
            Dim id As Integer = CInt(idAsStr)
            connection.Open()

            Using command As New SqlCommand("SELECT e.id, e.firstName, e.lastName, d.id AS deparmentId, d.title, d.description FROM Employees e INNER JOIN Departments d ON e.departmentId=d.id", connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim employee As New Employee() With {
                            .firstName = reader.GetString(1),
                            .lastName = reader.GetString(2),
                            .department = reader.GetString(4),
                        .description = reader.GetString(5)
                        }
                        employees.Add(employee)
                    End While
                End Using
            End Using

            connection.Close()

            rtn.data = employees
            rtn.success = True

        Catch ex As InvalidCastException
            rtn.success = False
            rtn.message = "Invalid Employee ID"

        Catch ex As Exception
            rtn.success = False
            rtn.message = ex

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
