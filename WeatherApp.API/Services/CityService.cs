using WeatherApp.API.Data.Repositories;
using WeatherApp.Shared;

namespace WeatherApp.API.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<CityDto?> GetCityByIdAsync(int id)
    {
        var city = await _cityRepository.GetCityByIdAsync(id);

        if (city == null)
        {
            return null;
        }

        return new CityDto
        {
            Id = city.Id,
            Name = city.Name,
            Country = city.Country,
            WeatherCondition = city.WeatherCondition,
            MaximumTemperature = city.MaximumTemperature,
            MinimumTemperature = city.MinimumTemperature,
            WindDirection = city.WindDirection,
            WindSpeed = city.WindSpeed,
            OutlookForNextDay = city.OutlookForNextDay,
        };
    }

    public async Task<List<CityNameDto>> GetAllCitiesNamesAsync()
    {
        var cities = await _cityRepository.GetAllCitiesNamesAsync();

        return cities.Select(c => new CityNameDto
        {
            Id = c.Id,
            DisplayName = $"{c.Name}, {c.Country}"
        }).ToList();
    }
}
