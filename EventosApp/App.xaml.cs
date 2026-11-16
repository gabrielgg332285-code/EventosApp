using System.Globalization;
using EventosApp.Views;

namespace EventosApp;

public partial class App : Application
{
    public App()
    {
        var cultura = new CultureInfo("pt-BR");
        CultureInfo.DefaultThreadCurrentCulture = cultura;
        CultureInfo.DefaultThreadCurrentUICulture = cultura;
        CultureInfo.CurrentCulture = cultura;

        InitializeComponent();
        UserAppTheme = AppTheme.Light;
    }

    protected override Window CreateWindow(IActivationState? activationState)
        => new Window(new NavigationPage(new CadastroEventoPage()));
}