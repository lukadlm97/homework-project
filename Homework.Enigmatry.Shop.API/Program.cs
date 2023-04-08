using System.Text;
using Homework.Enigmatry.Application.Shared.Models;
using Homework.Enigmatry.Persistence.Shared;
using Homework.Enigmatry.Shop.API.Extensions;
using Homework.Enigmatry.Shop.Application;
using Homework.Enigmatry.Shop.Application.Models;
using Homework.Enigmatry.Shop.Infrastructure;
using Homework.Enigmatry.Shop.Presentation.Middlewares;
using Homework.Enigmatry.Shop.Presentation.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOptions<PersistenceSettings>()
    .BindConfiguration(nameof(PersistenceSettings))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.AddOptions<VendorSettings>()
    .BindConfiguration(nameof(VendorSettings))
    .ValidateDataAnnotations()
    .ValidateOnStart();
builder.Services.Configure<TokenSettings>(
    builder.Configuration.GetSection(nameof(TokenSettings)));


builder.Services.ConfigureInMemoryPersistenceServices(builder.Configuration);
builder.Services.ConfigureShopApplicationServices();
builder.Services.ConfigureVendorsHttpClient(builder.Configuration);
builder.Services.ConfigureInfrastructureServices();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["TokenSettings:JwtSecretKey"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
        ValidAudience = builder.Configuration["TokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Add services to the container.
builder.Services.ConfigureSwaggerServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.UseCors("CorsPolicy");
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json",
    $"{builder.Configuration.GetSection("ApplicationSettings:Title").Value} {builder.Configuration.GetSection("ApplicationSettings:Version").Value}"));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();







