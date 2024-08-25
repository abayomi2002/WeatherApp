using System.Text.Json.Serialization;

namespace WeatherApp.Shared;
public class CityNameDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;
}
