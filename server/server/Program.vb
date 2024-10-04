Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection

Module Program
    Sub Main(args As String())
        ' Create a builder for the application
        Dim builder = WebApplication.CreateBuilder(args)

        ' Add CORS services with a specific policy
        builder.Services.AddCors(Function(options)
                                     options.AddPolicy("AllowAllOrigins", Sub(policy)
                                                                              policy.AllowAnyOrigin() _
                                                                          .AllowAnyMethod() _
                                                                          .AllowAnyHeader()
                                                                          End Sub)
                                 End Function)

        ' Build the application
        Dim app = builder.Build()

        ' Use the CORS policy
        app.UseCors("AllowAllOrigins")

        ' Define a simple GET endpoint at the root
        app.MapGet("/", Function()
                            Dim x = 5 + 2
                            Return $"The result is: {x}"
                        End Function)

        ' Define an example endpoint for testing CORS
        app.MapGet("/api/test", Function()
                                    Return "CORS is working!"
                                End Function)

        ' Run the application
        app.Run()
    End Sub
End Module
