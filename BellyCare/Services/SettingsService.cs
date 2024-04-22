using Barreto.Exe.Maui.Services.Settings;
using BellyCare.Models;

namespace BellyCare.Services
{
    public class SettingsService : BaseSettingsService, ISettingsService
    {
        private readonly Patient PatientDefault = null;
        public Patient Patient
        {
            get => GetValueOrDefault(nameof(Patient), PatientDefault);
            set => SetValue(nameof(Patient), value);
        }

        private readonly Doctor DoctorDefault = null;
        public Doctor Doctor
        {
            get => GetValueOrDefault(nameof(Doctor), DoctorDefault);
            set => SetValue(nameof(Doctor), value);
        }

        private readonly Admin AdminDefault = null;
        public Admin Admin
        {
            get => GetValueOrDefault(nameof(Admin), AdminDefault);
            set => SetValue(nameof(Admin), value);
        }

        private readonly LoggedUserType? TypeDefault = null;
        public LoggedUserType? UserType
        {
            get => GetValueOrDefault(nameof(UserType), TypeDefault);
            set => SetValue(nameof(UserType), value);
        }
    }
}
