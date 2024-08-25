using Microsoft.EntityFrameworkCore;
using WeatherApp.API.Entities;

namespace WeatherApp.API.Data;

public class WeatherAppDbContext : DbContext
{
    public WeatherAppDbContext(DbContextOptions<WeatherAppDbContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; }
}
