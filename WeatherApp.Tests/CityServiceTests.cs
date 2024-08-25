using Moq;
using WeatherApp.API.Data.Repositories;
using WeatherApp.API.Entities;
using WeatherApp.API.Services;

namespace WeatherApp.Tests;
public class CityServiceTests
{
    private readonly Mock<ICityRepository> _mockCityRepository;
    private readonly CityService _cityService;

    public CityServiceTests()
    {
        _mockCityRepository = new Mock<ICityRepository>();
        _cityService = new CityService(_mockCityRepository.Object);
    }

    [Fact]
    public async Task GetCityByIdAsync_ReturnsCityDto_WhenCityExists()
    {
        // Arrange
        var cityId = 1;
        var city = new City
        {
            Id = cityId,
            Name = "Dublin",
            Country = "Ireland",
            WeatherCondition = "Cloudy",
            MaximumTemperature = 18,
            MinimumTemperature = 10,
            WindDirection = "Northwest",
            WindSpeed = 15,
            OutlookForNextDay = "Rain"
        };

        _mockCityRepository.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync(city);

        // Act
        var result = await _cityService.GetCityByIdAsync(cityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cityId, result.Id);
        Assert.Equal("Dublin", result.Name);
        Assert.Equal("Ireland", result.Country);
        Assert.Equal("Cloudy", result.WeatherCondition);
        Assert.Equal(18, result.MaximumTemperature);
        Assert.Equal(10, result.MinimumTemperature);
        Assert.Equal("Northwest", result.WindDirection);
        Assert.Equal(15, result.WindSpeed);
        Assert.Equal("Rain", result.OutlookForNextDay);
    }

    [Fact]
    public async Task GetCityByIdAsync_ReturnsNull_WhenCityDoesNotExist()
    {
        // Arrange
        var cityId = 2;
        _mockCityRepository.Setup(repo => repo.GetCityByIdAsync(cityId)).ReturnsAsync((City)null);

        // Act
        var result = await _cityService.GetCityByIdAsync(cityId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllCitiesNamesAsync_ReturnsListOfCityNameDtos()
    {
        // Arrange
        var cities = new List<City>
            {
                new City { Id = 1, Name = "Dublin", Country = "Ireland" },
                new City { Id = 2, Name = "Paris", Country = "France" }
            };

        _mockCityRepository.Setup(repo => repo.GetAllCitiesNamesAsync()).ReturnsAsync(cities);

        // Act
        var result = await _cityService.GetAllCitiesNamesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(result, r => r.DisplayName == "Dublin, Ireland");
        Assert.Contains(result, r => r.DisplayName == "Paris, France");
    }

    [Fact]
    public async Task GetAllCitiesNamesAsync_ReturnsEmptyList_WhenNoCitiesExist()
    {
        // Arrange
        _mockCityRepository.Setup(repo => repo.GetAllCitiesNamesAsync()).ReturnsAsync(new List<City>());

        // Act
        var result = await _cityService.GetAllCitiesNamesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}

