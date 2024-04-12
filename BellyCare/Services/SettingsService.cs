using Barreto.Exe.Maui.Services.Settings;
using BellyCare.Models;

namespace BellyCare.Services
{
    public class SettingsService : BaseSettingsService, ISettingsService
    {
        private readonly User UserDefault = null;
        public User User
        {
            get => GetValueOrDefault(nameof(User), UserDefault);
            set => SetValue(nameof(User), value);
        }
    }
}
