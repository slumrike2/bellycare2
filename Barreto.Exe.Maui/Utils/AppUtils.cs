using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Barreto.Exe.Maui.Utils;

public static class AppUtils
{
    public static string GetConfigValue(string key)
    {
        using var stream = FileSystem.OpenAppPackageFileAsync("data.json").Result;
        using var reader = new StreamReader(stream);
        var jsonVariables = reader.ReadToEnd();

        //Convert the json into a dictionary
        var variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVariables);

        //Return the value of the key if it exists, otherwise return null
        return variables.TryGetValue(key, out string value) ? value : null;
    }

    public static async Task ShowAlert(string title, string message)
    {
        await Application.Current.MainPage.DisplayAlert(title, message, "Aceptar");
    }
    public static async Task ShowAlert(string message, AlertType type = AlertType.Info)
    {
        string title = type switch
        {
            AlertType.Success => "✅ Éxito",
            AlertType.Error => "❌ Error",
            AlertType.Warning => "⚠️ Alerta",
            _ => "ℹ️ Información"
        };

        await Application.Current.MainPage.DisplayAlert(title, message, "Aceptar");
    }
    public static async Task<bool> ShowAlert(string message)
    {
        return await Application.Current.MainPage.DisplayAlert("ℹ️ Información", message, "Aceptar", "Cancelar");
    }

    public static async Task CheckConnection()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            await ShowAlert("No hay conexión a internet.", AlertType.Error);
            Application.Current.Quit();
        }
    }
    public static bool IsValidEmail(string email)
    {
        try
        {
            return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
        catch
        {
            return false;
        }
    }
    public static bool IsValidPassword(string password)
    {
        try
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
        }
        catch
        {
            return false;
        }
    }

}
public enum AlertType
{
    Success,
    Error,
    Warning,
    Info
}
