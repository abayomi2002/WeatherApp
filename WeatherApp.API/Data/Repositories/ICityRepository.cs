using WeatherApp.API.Entities;

namespace WeatherApp.API.Data.Repositories;

public interface ICityRepository
{
    public Task<City?> GetCityByIdAsync(int id);
    public Task<List<City>> GetAllCitiesNamesAsync();
}
