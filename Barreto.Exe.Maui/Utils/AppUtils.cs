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
    public static async Task<bool> ShowAlert(string message, AlertType type = AlertType.Info, bool hasCancelButton = false)
    {
        string title = type switch
        {
            AlertType.Success => "✅ Éxito",
            AlertType.Error => "❌ Error",
            AlertType.Warning => "⚠️ Alerta",
            _ => "ℹ️ Información"
        };

        if(hasCancelButton)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, "Aceptar", "Cancelar");
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert(title, message, "Aceptar");
            return true;
        }
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
    public static string Md5Encrypt(this string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
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
