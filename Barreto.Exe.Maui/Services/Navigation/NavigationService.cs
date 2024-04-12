using Barreto.Exe.Maui.Services.Settings;
using Barreto.Exe.Maui.Shells;

namespace Barreto.Exe.Maui.Services.Navigation;
public class NavigationService : INavigationService
{
    private readonly IBaseSettingsService settings;
    public NavigationService(IBaseSettingsService settings)
    {
        this.settings = settings;
    }

    public async Task PopAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public Task NavigateToAsync<T>(Dictionary<string, object> routeParameters = null) where T : Page
    {
        return
            routeParameters != null
                ? Shell.Current.GoToAsync(typeof(T).Name, routeParameters)
                : Shell.Current.GoToAsync(typeof(T).Name);
    }

    public Task NavigateToAsync(string route)
    {
        return Shell.Current.GoToAsync(route);
    }

    public void RestartSession()
    {
        bool hasToken = !string.IsNullOrEmpty(settings.AccessToken);

        if (Application.Current is IMultishellApp multishellApp)
        {
            if (hasToken)
            {
                multishellApp.GoToAppShell();
            }
            else
            {
                multishellApp.GoToSessionShell();
            }
        }
        else
        {
            //Throw exception
            throw new Exception("Application must implement IMultishellApp");
        }
    }
}
