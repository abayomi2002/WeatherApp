using System.Text.Json.Serialization;

namespace WeatherApp.Shared;
public class CityDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("weatherCondition")]
    public string WeatherCondition { get; set; } = string.Empty;

    [JsonPropertyName("maximumTemperature")]
    public int MaximumTemperature { get; set; }

    [JsonPropertyName("minimumTemperature")]
    public int MinimumTemperature { get; set; }

    [JsonPropertyName("windDirection")]
    public string WindDirection { get; set; } = string.Empty;

    [JsonPropertyName("windSpeed")]
    public int WindSpeed { get; set; }

    [JsonPropertyName("outlookForNextDay")]
    public string OutlookForNextDay { get; set; } = string.Empty;
}
