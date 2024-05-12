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

    public Task NavigateToAsync<T>(Dictionary<string, object> routeParameters = null, bool isAbsolute = false) where T : Page
    {
        string route = typeof(T).Name;

        if(isAbsolute)
        {
            route = $"//{route}";
        }

        return
            routeParameters != null
                ? Shell.Current.GoToAsync(route, routeParameters)
                : Shell.Current.GoToAsync(route);
    }

    public Task NavigateToAsync(string route, bool isAbsolute = false)
    {
        if(isAbsolute)
        {
            return Shell.Current.GoToAsync($"//{route}");
        }

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
