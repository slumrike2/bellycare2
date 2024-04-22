using Barreto.Exe.Maui.Services.Settings;
using BellyCare.Models;

namespace BellyCare.Services
{
    public interface ISettingsService : IBaseSettingsService
    {
        Patient Patient { get; set; }
        Doctor Doctor { get; set; }
        Admin Admin { get; set; }
        LoggedUserType? UserType { get; set; }
    }
    public enum LoggedUserType
    {
        Patient,
        Doctor,
        Admin
    }
}
