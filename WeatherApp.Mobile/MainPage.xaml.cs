using WeatherApp.Mobile.Services;
using WeatherApp.Shared;
using Localizer = WeatherApp.Mobile.Resources.Language.Strings;
namespace WeatherApp.Mobile;

public partial class MainPage : ContentPage
{
    private readonly CityService _cityService;

    private List<CityNameDto> _allCities;
    public MainPage()
    {
        _cityService = new CityService();
        InitializeComponent();
        _allCities = [];
        SearchResultLayout.IsVisible = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ShowLoadingAsync(Localizer.API_LOADING_CITIES_MESSAGE);
        _allCities = await _cityService.GetAllCitiesNamesAsync();
        CityCollectionView.ItemsSource = _allCities.Select(c => c.DisplayName);
        CityCollectionView.IsVisible = true;
        SearchResultLayout.IsVisible = true;

        await HideLoadingAsync();
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        SearchResultLayout.IsVisible = true;

        var searchTerm = e.NewTextValue;

        CityCollectionView.ItemsSource = string.IsNullOrEmpty(searchTerm) ?
            _allCities.Select(city => city.DisplayName).ToList() :
            _allCities.Where(city => city.DisplayName.ToLower().Contains(searchTerm.ToLower()))
            .Select(city => city.DisplayName)
            .ToList();
        CityCollectionView.IsVisible = true;
    }

    private async void OnCitySelected(object sender, SelectionChangedEventArgs e)
    {
        if (CityCollectionView.SelectedItem == null)
            return;

        CityCollectionView.IsVisible = false;  // Hide dropdown after selection
        SearchResultLayout.IsVisible = false;

        var selectedCity = _allCities.FirstOrDefault(c => c.DisplayName == (CityCollectionView.SelectedItem as string))!;

        await ShowLoadingAsync($"{Localizer.API_WEATHER_LOADING_MESSAGE} {selectedCity.DisplayName}...");
        var weatherInfo = await _cityService.GetCityByIdAsync(selectedCity.Id);
        await HideLoadingAsync();

        DisplayWeatherInfo(weatherInfo!);


        CitySearchBar.Text = selectedCity.DisplayName;     // Set search bar text to the selected city
        CityCollectionView.IsVisible = false;  // Hide dropdown after selection
        SearchResultLayout.IsVisible = false;
    }

    private void DisplayWeatherInfo(CityDto city)
    {
        CityNameLabel.Text = city.Name;
        CountryLabel.Text = city.Country;
        WeatherConditionLabel.Text = Localizer.ResourceManager.GetString(city.WeatherCondition);
        MaxTempLabel.Text = $"{Localizer.MAX_TEMP}: {city.MaximumTemperature}°C";
        MinTempLabel.Text = $"{Localizer.MIN_TEMP}: {city.MinimumTemperature}°C";
        WindLabel.Text = $"{Localizer.wind}: {Localizer.ResourceManager.GetString(city.WindDirection)} at {city.WindSpeed} km/h";
        OutlookLabel.Text = $"{Localizer.Outlook}: {Localizer.ResourceManager.GetString(city.OutlookForNextDay)}";

        WeatherInfoLayout.IsVisible = true;
    }

    private async void OnSettingsClickedAsync(object sender, EventArgs e)
    {
        // Navigate to the settings page
        await Navigation.PushAsync(new SettingsPage());
    }

    private async Task ShowLoadingAsync(string loaderText = "")
    {
        await Dispatcher.DispatchAsync(() =>
        {
            LoaderLabel.Text = loaderText;
            LoadingOverlay.IsVisible = true;
        });
    }

    private async Task HideLoadingAsync()
    {
        await Dispatcher.DispatchAsync(() =>
        {
            LoadingOverlay.IsVisible = false;
        });
    }
}


