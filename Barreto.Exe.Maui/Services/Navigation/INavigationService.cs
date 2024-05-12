namespace Barreto.Exe.Maui.Services.Navigation;

public interface INavigationService
{
    void RestartSession();

    Task NavigateToAsync<T>(Dictionary<string, object> routeParameters = null, bool isAbsolute = false) where T : Page;

    Task NavigateToAsync(string route, bool isAbsolute = false);

    Task PopAsync();

    Task GoBackAsync();
}
