using Microsoft.AspNetCore.Mvc;
using WeatherApp.API.Services;
using WeatherApp.Shared;

namespace WeatherApp.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CityWeatherController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityWeatherController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CityDto>> GetCityByIdAsync(int id)
    {
        var city = await _cityService.GetCityByIdAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return Ok(city);
    }

    [HttpGet("names")]
    public async Task<ActionResult<List<CityNameDto>>> GetAllCitiesNamesAsync()
    {
        var cities = await _cityService.GetAllCitiesNamesAsync();

        return Ok(cities);
    }
}