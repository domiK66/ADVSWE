using System.Text.Json.Serialization;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Converters;
using Services;
using Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<GlobalService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHealthChecks();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(o =>
    {
        o.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => x.TokenValidationParameters = Authentication.ValidationParams);

builder.Services.AddCors(
    options =>
        options.AddPolicy(
            "Cors",
            builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
        )
);

builder.Services.AddOpenApiDocument(doc =>
{
    doc.Title = "Aquarium Management API";
    doc.Description = "Aquarium Management API";
    doc.AddSecurity(
        "JWT",
        Enumerable.Empty<String>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Description = "Type into textbox: Bearer ..... "
        }
    );
});

var app = builder.Build();

app.UseCors("Cors");

app.MapHealthChecks("/health");

/*
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseOpenApi();

app.UseSwaggerUi3();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
