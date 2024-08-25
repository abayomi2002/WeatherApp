using WeatherApp.Shared;

namespace WeatherApp.API.Services;

public interface ICityService
{
    public Task<CityDto?> GetCityByIdAsync(int id);
    public Task<List<CityNameDto>> GetAllCitiesNamesAsync();
}
