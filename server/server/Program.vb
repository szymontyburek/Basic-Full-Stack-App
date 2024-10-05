Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection
Imports System
Imports System.Dynamic
Imports System.Data.SqlClient

Module Program
    Sub Main(args As String())
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

        app.MapGet("/getEmpInfo", Async Function()
                                      Dim SQL As New SQLControl()
                                      Dim res = CType(Await SQL.GETquery("SELECT e.first_name + ' ' + e.last_name AS Name, d.title AS Department, d.description AS Responsibilities FROM Employees e INNER JOIN Departments d ON e.departmentId=d.id"), IDictionary(Of String, Object))
                                      SQL.connection.Close() 'data is NOT visible if connection to DB has been closed. That is why it is closed AFTER receiving data from SQLControl class

                                      If res("success") Then
                                          Console.WriteLine("Successful query")
                                      Else
                                          Console.WriteLine("Unsuccessful query")
                                      End If

                                      Return res
                                  End Function)
        app.Run()
    End Sub
End Module
