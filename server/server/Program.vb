Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection
Imports System
Imports System.Dynamic
Imports System.Data.SqlClient

Module Program
    Sub Main(args As String())
        ' call to db
        Dim SQL As New SQLControl()
        Dim res = CType(SQL.GETquery("SELECT e.first_name + ' ' + e.last_name AS Name, d.title AS Department, d.description AS Responsibilities FROM Employees e INNER JOIN Departments d ON e.departmentId=d.id"), IDictionary(Of String, Object))

        If res("success") Then
            Console.WriteLine("Successful query")
        Else
            Console.WriteLine("Unsuccessful query")
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
                            Dim rtn = CType(New ExpandoObject(), IDictionary(Of String, Object))

                            Try
                                rtn("success") = True
                                rtn("message") = "Yay"
                            Catch ex As Exception
                                rtn("success") = False
                            End Try

                            Return rtn
                        End Function)
        app.Run()
    End Sub
End Module
