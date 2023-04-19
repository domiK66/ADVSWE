using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Services;
using Services.Auth;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<GlobalService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( x => x.TokenValidationParameters = Authentication.ValidationParams);

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
