using Microsoft.EntityFrameworkCore;
using WeatherApp.API.Entities;

namespace WeatherApp.API.Data.Repositories;

public class CityRepository : ICityRepository
{
    private readonly WeatherAppDbContext _context;

    public CityRepository(WeatherAppDbContext context)
    {
        _context = context;
    }


    public async Task<City?> GetCityByIdAsync(int id)
    {
        return await _context.Cities.FindAsync(id);
    }

    public async Task<List<City>> GetAllCitiesNamesAsync()
    {
        return await _context.Cities.Select(c => new City
        {
            Id = c.Id,
            Name = c.Name,
            Country = c.Country
        }).ToListAsync();
    }
}
