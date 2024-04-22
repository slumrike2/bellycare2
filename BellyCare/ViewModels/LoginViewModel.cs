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

        public LoginViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository, 
            BaseOnlineRepository<Doctor> doctorRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
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
            var patientTask = patientRepository.GetAllBy(o => o.Email == Email);
            var doctorTask = doctorRepository.GetAllBy(o => o.Email == Email);

            await Task.WhenAll(patientTask, doctorTask);

            Patient? patient = patientTask.Result.FirstOrDefault();
            Doctor? doctor = doctorTask.Result.FirstOrDefault();

            // Check if the password is correct
            bool isPasswordCorrect = false;
            if (patient != null)
            {
                if (patient.Password == Password)
                {
                    isPasswordCorrect = true;
                }
            }
            else if (doctor != null)
            {
                if (doctor.Password == Password)
                {
                    isPasswordCorrect = true;
                }
            }

            // If the password is correct, save the access token and restart the session
            if (!isPasswordCorrect)
            {
                await AppUtils.ShowAlert("Correo o contraseña incorrectos.");
            }
            else
            {
                settings.AccessToken = user.Email;
                settings.User = patient is not null ? patient : doctor;
                navigationService.RestartSession();
            }
        }

        public async void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}
