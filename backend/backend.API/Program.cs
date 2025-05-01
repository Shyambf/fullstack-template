using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using backend.Domain;
using backend.Infrastructure.Data;
using backend.Domain.Entities;
var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

builder.Services.AddDbContext<myDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (myDbContext dbContext) =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();

    dbContext.WeatherForecasts.AddRange(forecast);
    dbContext.SaveChanges();

    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();
