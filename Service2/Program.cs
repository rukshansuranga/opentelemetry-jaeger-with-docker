using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// This must be set before creating a GrpcChannel/HttpClient when calling an insecure service
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

builder.Services.AddOpenTelemetry()
        .ConfigureResource(builder => builder.AddService(serviceName: "Service2"))
        .WithTracing(builder => builder
        .AddSource("ExampleTracer2")
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter()
        .AddOtlpExporter(o =>
        {
            o.Endpoint = new Uri("http://host.docker.internal:4317");
        }
     ));

     

var app = builder.Build();

app.MapControllers();

app.Run();
