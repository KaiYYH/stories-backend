using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using MySqlConnector;
using StoriesApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/* builder.Services.AddDbContext<StoryContext>(opt =>
    opt.UseInMemoryDatabase("Stories")); */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/* builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!); */

builder.Services.AddDbContext<StoryContext>(options => 
{
    var connectionString=builder.Configuration.GetConnectionString("Default");
    options.UseMySQL(connectionString);
});

/* builder.Services.AddEntityFrameworkMySQL()
    .AddDbContext<StoryContext>(options =>
    {
        options.UseMySQL(builder.Configuration.GetConnectionString("Default"));
    }); */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();