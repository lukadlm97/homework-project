using Homework.Enigmatry.Logging.Shared;
using Homework.Enigmatry.Persistence.Shared;
using Homework.Enigmatry.Vendor.Application;
using Homework.Enigmatry.Vendor.GrpcAPI.Services;
using Homework.Enigmatry.Vendor.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration
        .WriteTo.File($"log-.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
        .WriteTo.Console();
});
builder.Services.ConfigureLoggingServices();

builder.Services.ConfigureInMemoryVendorPersistenceServices(builder.Configuration);
builder.Services.ConfigureVendorApplicationServices();
builder.Services.ConfigureVendorInfrastructureServices();
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<VendorService>();
app.MapGet("/",
    () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
