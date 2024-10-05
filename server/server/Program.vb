Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection
Imports System
Imports System.Dynamic
Imports System.Data.SqlClient

Module Program
    Sub Main(args As String())
        ' call to db
        Dim SQL As New SQLControl()
        If SQL.GETquery("SELECT e.first_name + ' ' + e.last_name AS Name, d.title AS Department, d.description AS Responsibilities FROM Employees e INNER JOIN Departments d ON e.departmentId=d.id") Then
            Console.WriteLine("Successful Connection")
        Else
            Console.WriteLine("Unsuccessful Connection")
        End If
        ' call to db

        Dim builder = WebApplication.CreateBuilder(args)

        builder.Services.AddCors(Function(options)
                                     options.AddPolicy("AllowAllOrigins", Sub(policy)
                                                                              policy.AllowAnyOrigin() _
                                                                          .AllowAnyMethod() _
                                                                          .AllowAnyHeader()
                                                                          End Sub)
                                 End Function)

        Dim app = builder.Build()

        app.UseCors("AllowAllOrigins")

        app.MapGet("/", Function()
                            Dim expando As ExpandoObject = New ExpandoObject()
                            Dim expandoDict = CType(expando, IDictionary(Of String, Object))

                            Try
                                expandoDict("success") = True
                                expandoDict("message") = "Yay"
                            Catch ex As Exception
                                expandoDict("success") = False
                            End Try

                            Return expandoDict
                        End Function)
        app.Run()
    End Sub
End Module
