using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BellyCare.ViewModels
{
    public partial class LoginViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;
        private readonly BaseOnlineRepository<Doctor> doctorRepository;
        private readonly BaseOnlineRepository<Admin> adminRepository;

        public LoginViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository, 
            BaseOnlineRepository<Doctor> doctorRepository,
            BaseOnlineRepository<Admin> adminRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.adminRepository = adminRepository;
        }

        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        [ObservableProperty]
        string? test;

        [ObservableProperty]
        bool isPasswordVisible;

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                return;
            }

            User user = new()
            {
                Email = Email,
                Password = Password
            };

            // Check if the user is a patient or a doctor
            var patientTask = patientRepository.GetAllBy(o => o.Object.Email == Email && o.Object.Password == Password);
            var doctorTask = doctorRepository.GetAllBy(o => o.Object.Email == Email && o.Object.Password == Password);
            var adminTask = adminRepository.GetAllBy(o => o.Object.Email == Email && o.Object.Password == Password);

            await Task.WhenAll(patientTask, doctorTask, adminTask);

            var patient = patientTask.Result.FirstOrDefault();
            var doctor = doctorTask.Result.FirstOrDefault();
            var admin = adminTask.Result.FirstOrDefault();

            // Check if the password is correct and assign the user to the settings
            if (patient != null)
            {
                settings.AccessToken = patient.Key;
                settings.Patient = patient.Object;
                settings.UserType = LoggedUserType.Patient;
            }
            else if (doctor != null)
            {
                settings.AccessToken = doctor.Key;
                settings.Doctor = doctor.Object;
                settings.UserType = LoggedUserType.Doctor;
            }
            else if (admin != null)
            {
                settings.AccessToken = admin.Key;
                settings.Admin = admin.Object;
                settings.UserType = LoggedUserType.Admin;
            }
            else
            {
                await AppUtils.ShowAlert("Correo o contraseña incorrectos.");
                return;
            }

            // If the password is correct, save the access token and restart the session
            navigation.RestartSession();
        }

        public async void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}
