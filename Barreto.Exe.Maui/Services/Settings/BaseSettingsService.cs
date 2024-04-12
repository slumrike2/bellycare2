using Newtonsoft.Json;
namespace Barreto.Exe.Maui.Services.Settings;
public class BaseSettingsService : IBaseSettingsService
{
    private readonly string AccessTokenDefault = string.Empty;
    public string AccessToken
    {
        get => GetValueOrDefault(nameof(AccessToken), AccessTokenDefault);
        set => SetValue(nameof(AccessToken), value);
    }
    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(AccessToken);

    public void SetValue(string key, object value)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            Preferences.Remove(key);
        }
        else
        {
            var json = JsonConvert.SerializeObject(value);
            Preferences.Set(key, json);
        }
    }
    public T GetValueOrDefault<T>(string key, T defaultValue)
    {
        var json = Preferences.Get(key, string.Empty);
        return json == string.Empty ? defaultValue : JsonConvert.DeserializeObject<T>(json);
    }
}