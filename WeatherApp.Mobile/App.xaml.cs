using System.Globalization;

namespace WeatherApp.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Change culture
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR", false);
        //Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR", false);

        MainPage = new AppShell();
    }
}
