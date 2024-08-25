using System.ComponentModel.DataAnnotations;

namespace WeatherApp.API.Entities;

public class City
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Country { get; set; } = string.Empty;
    
    [Required]
    [AllowedValues("sunny", "cloudy", "overcast", "rain", "drizzle", "fog", "snow")]
    public string WeatherCondition { get; set; } = string.Empty;

    [Required]
    [Range(-40, 40)]
    public int MaximumTemperature { get; set; }

    [Required]
    [Range(-40, 40)]
    public int MinimumTemperature { get; set; }
    
    [Required]
    [AllowedValues("north", "south", "east", "west", "northeast", "southeast", "northwest", "southwest")]
    public string WindDirection { get; set; } = string.Empty;

    [Required]
    [Range(0, 200)]
    public int WindSpeed { get; set; }

    [Required]
    [AllowedValues("sunny", "cloudy", "overcast", "rain", "drizzle", "fog", "snow")]
    public string OutlookForNextDay { get; set; } = string.Empty;
}
