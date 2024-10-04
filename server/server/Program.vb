Imports Microsoft.AspNetCore.Builder
Imports Microsoft.Extensions.DependencyInjection

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

        app.MapGet("/", Function()
                            Dim x = 5 + 2
                            Return $"The result is: {x}"
                        End Function)
        app.Run()
    End Sub
End Module
