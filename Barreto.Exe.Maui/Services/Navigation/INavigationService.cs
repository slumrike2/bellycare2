namespace Barreto.Exe.Maui.Services.Navigation;

public interface INavigationService
{
    void RestartSession();

    Task NavigateToAsync<T>(Dictionary<string, object> routeParameters = null) where T : Page;

    Task NavigateToAsync(string route);

    Task PopAsync();

    Task GoBackAsync();
}
