using System.Globalization;

namespace WeatherApp.Mobile;

public partial class SettingsPage : ContentPage
{
    private bool _isPageLoaded;

    public SettingsPage()
    {
        InitializeComponent();

        // Set the picker to the current language
        var currentCulture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        switch (currentCulture)
        {
            case "fr":
                LanguagePicker.SelectedIndex = 1;
                break;
            case "ga":
                LanguagePicker.SelectedIndex = 2;
                break;
            default:
                LanguagePicker.SelectedIndex = 0;
                break;
        }

        _isPageLoaded = true; // Set the flag to true after the initialization
    }

    private void OnLanguageChanged(object sender, EventArgs e)
    {
        if (!_isPageLoaded)
            return; // Exit if the page is not fully loaded
        
        var selectedLanguage = LanguagePicker.SelectedItem.ToString();
        string cultureCode = selectedLanguage switch
        {
            "French" => "fr-FR",
            "Irish" => "ga-IE",
            _ => "en-US"
        };

        // Change the culture
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode, false);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCode, false);

        // Navigate back to the home page
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}
