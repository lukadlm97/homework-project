using Homework.Enigmatry.Shop.Application;
using Homework.Enigmatry.Shop.Persistence;
using Homework.Enigmatry.Shop.Presentation.Middlewares;
using Homework.Enigmatry.Shop.Presentation.Swagger;
using Homework.Enigmatry.Vendor.Application;

var builder = WebApplication.CreateBuilder(args);


/*
builder.Services.AddOptions<PersistenceSettings>()
    .BindConfiguration(nameof(PersistenceSettings))
    .ValidateDataAnnotations()
    .ValidateOnStart();*/

builder.Services.ConfigureInMemoryVendorPersistenceServices(builder.Configuration);
builder.Services.ConfigureVendorApplicationServices();


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
