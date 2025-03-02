using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StoriesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoryContext>(options => 
{
    var connectionString=builder.Configuration.GetConnectionString("Default");
    options.UseMySQL(connectionString);
});

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