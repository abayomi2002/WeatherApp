using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.API.Controllers;
using WeatherApp.API.Services;
using WeatherApp.Shared;

namespace WeatherApp.Tests;
public class CityControllerTests
{
    private readonly Mock<ICityService> _mockCityService;
    private readonly CityWeatherController _controller;

    public CityControllerTests()
    {
        _mockCityService = new Mock<ICityService>();
        _controller = new CityWeatherController(_mockCityService.Object);
    }

    [Fact]
    public async Task GetCityByIdAsync_ReturnsOkObjectResult_WithCityDto_WhenCityExists()
    {
        // Arrange
        var cityId = 1;
        var cityDto = new CityDto
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

        _mockCityService.Setup(service => service.GetCityByIdAsync(cityId))
                        .ReturnsAsync(cityDto);

        // Act
        var result = await _controller.GetCityByIdAsync(cityId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCity = Assert.IsType<CityDto>(okResult.Value);
        Assert.Equal(cityId, returnedCity.Id);
        Assert.Equal("Dublin", returnedCity.Name);
    }

    [Fact]
    public async Task GetCityByIdAsync_ReturnsNotFound_WhenCityDoesNotExist()
    {
        // Arrange
        var cityId = 2;
        _mockCityService.Setup(service => service.GetCityByIdAsync(cityId))
                        .ReturnsAsync((CityDto)null);

        // Act
        var result = await _controller.GetCityByIdAsync(cityId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetAllCitiesNamesAsync_ReturnsOkObjectResult_WithListOfCityNameDto()
    {
        // Arrange
        var cities = new List<CityNameDto>
            {
                new CityNameDto { Id = 1, DisplayName = "Dublin, Ireland" },
                new CityNameDto { Id = 2, DisplayName = "Paris, France" }
            };

        _mockCityService.Setup(service => service.GetAllCitiesNamesAsync())
                        .ReturnsAsync(cities);

        // Act
        var result = await _controller.GetAllCitiesNamesAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCities = Assert.IsType<List<CityNameDto>>(okResult.Value);
        Assert.Equal(2, returnedCities.Count);
        Assert.Contains(returnedCities, c => c.DisplayName == "Dublin, Ireland");
        Assert.Contains(returnedCities, c => c.DisplayName == "Paris, France");
    }

    [Fact]
    public async Task GetAllCitiesNamesAsync_ReturnsOkObjectResult_WithEmptyList_WhenNoCitiesExist()
    {
        // Arrange
        _mockCityService.Setup(service => service.GetAllCitiesNamesAsync())
                        .ReturnsAsync(new List<CityNameDto>());

        // Act
        var result = await _controller.GetAllCitiesNamesAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCities = Assert.IsType<List<CityNameDto>>(okResult.Value);
        Assert.Empty(returnedCities);
    }
}
