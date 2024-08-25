using System.Text.Json;
using WeatherApp.Shared;

namespace WeatherApp.Mobile.Services;
public class CityService
{
    private readonly HttpClient _httpClient;
    private const string BASE_URL = "https://weatherapp-api.azurewebsites.net/api/";
    
    public CityService()
    {
        if (string.IsNullOrEmpty(BASE_URL))
            throw new InvalidOperationException("API URL is not configured.");

        _httpClient = new HttpClient();
    }

    public async Task<CityDto?> GetCityByIdAsync(int id)
    {
        var url = $"{BASE_URL}/CityWeather/{id}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var city = JsonSerializer.Deserialize<CityDto>(jsonResponse);
            return city;
        }
        return null;
    }

    public async Task<List<CityNameDto>> GetAllCitiesNamesAsync()
    {
        var url = $"{BASE_URL}/CityWeather/names";
        var response = await _httpClient.GetAsync(url);

        if(response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var cities = JsonSerializer.Deserialize<List<CityNameDto>>(jsonResponse);
            return cities;
        }
        return [];
    }
}

