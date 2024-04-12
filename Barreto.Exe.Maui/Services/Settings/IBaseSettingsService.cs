namespace Barreto.Exe.Maui.Services.Settings;

public interface IBaseSettingsService
{
    string AccessToken { get; set; }
    bool IsLoggedIn { get; }
    protected T GetValueOrDefault<T>(string key, T defaultValue);
    protected void SetValue(string key, object value);
}
