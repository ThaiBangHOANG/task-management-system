using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TaskManagementSystem.API.Common;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Services;
using TaskManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:4200",
                    "https://proud-mud-076774a03.7.azurestaticapps.net"
                 )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter 'Bearer' [space] and your token"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                new string[] {}
            }
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key =
            builder.Configuration[
                "JwtSettings:Key"
            ];

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration[
                        "JwtSettings:Issuer"
                    ],

                ValidAudience =
                    builder.Configuration[
                        "JwtSettings:Audience"
                    ],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key ?? string.Empty)
                    )
            };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode =
            StatusCodes.Status500InternalServerError;

        context.Response.ContentType =
            "application/json";

        var exception =
            context.Features
                .Get<IExceptionHandlerFeature>()
                ?.Error;

        var response =
            new ErrorResponse
            {
                Message =
                    "An unexpected error occurred",

                Details =
                    exception?.Message
            };

        await context.Response
            .WriteAsJsonAsync(response);
    });
});

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Task Management API Ok",
        version = "1.0",
        documentation = "/swagger",
        health = "/api/health"
    });
});

using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider
            .GetRequiredService<AppDbContext>();

    DbSeeder.Seed(context);
}

app.Run();
