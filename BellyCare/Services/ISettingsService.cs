using Barreto.Exe.Maui.Services.Settings;
using BellyCare.Models;

namespace BellyCare.Services
{
    public interface ISettingsService : IBaseSettingsService
    {
        User User { get; set; }
    }
}
