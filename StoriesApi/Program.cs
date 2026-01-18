using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StoriesApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1️⃣ Build a manual NpgsqlConnection with AddressFamily
var connectionString = "Host=db.ewvzykrtjcikjfagovcv.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=[1l1k32Wr1t3!];SslMode=Require;Trust Server Certificate=true;";
var npgsqlConnection = new NpgsqlConnection(connectionString);

// 2️⃣ Pass the NpgsqlConnection directly to UseNpgsql
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseNpgsql(npgsqlConnection);
});

// 3️⃣ Build and run your app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var allowedOrigins = app.Configuration.GetSection("AllowedCorsOrigins") != null
            ? app.Configuration.GetSection("AllowedCorsOrigins").GetChildren().Select(x => x.Value).ToArray()
            : Array.Empty<string>();
        Console.WriteLine("allowed origins:" + string.Join(',', allowedOrigins));
        app.UseCors(x => x
            .WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();