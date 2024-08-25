using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WeatherApp.API.Data;
using WeatherApp.API.Data.Repositories;
using WeatherApp.API.Entities;
using WeatherApp.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Db Context
builder.Services.AddDbContext<WeatherAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options => { options.EnableRetryOnFailure(5); });
});

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityService, CityService>();

var app = builder.Build();

// Apply migrations and seed the database
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<WeatherAppDbContext>();
    context.Database.Migrate();  // Applies any pending migrations

    // if Cities table is empty, parse the seed file Data/seed.json and seed the table
    if (context.Cities.Count() == 0)
    {
        var cities = System.Text.Json.JsonSerializer.Deserialize<List<City>>(File.ReadAllText("Data/seed.json"))!;
        context.Cities.AddRange(cities);
        context.SaveChanges();
    }
}

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
