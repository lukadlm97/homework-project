using Homework.Enigmatry.Logging.Shared;
using Homework.Enigmatry.Persistence.Shared;
using Homework.Enigmatry.Shop.Presentation.Middlewares;
using Homework.Enigmatry.Shop.Presentation.Swagger;
using Homework.Enigmatry.Vendor.Application;
using Homework.Enigmatry.Vendor.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Logging.ClearProviders();
builder.Host.UseSerilog((hostContext, services, configuration) => {
    configuration
        .WriteTo.File($"log-.txt", outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
        .WriteTo.Console();
});
builder.Services.ConfigureLoggingServices();
builder.Services.AddScoped<ExceptionMiddleware>();

builder.Services.ConfigureInMemoryVendorPersistenceServices(builder.Configuration);
builder.Services.ConfigureVendorApplicationServices();
builder.Services.ConfigureVendorInfrastructureServices();


builder.Services.AddHttpContextAccessor();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(Homework.Enigmatry.Shop.Vendor.Presentation.AssemblyReference).Assembly);


builder.Services.ConfigureSwaggerServices(builder.Configuration);
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
    $"{builder.Configuration.GetSection("ApplicationSettings:Title").Value} {builder.Configuration.GetSection("ApplicationSettings:Version").Value}"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
