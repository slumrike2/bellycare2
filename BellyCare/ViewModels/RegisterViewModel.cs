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
    public partial class RegisterViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;

        public RegisterViewModel(
            ISettingsService settings,
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;

#if DEBUG
            Email = "luis@gmail.com";
            Password = "Luis2024*";
            ConfirmPassword = "Luis2024*";
#endif
        }

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmPassword;

        [ObservableProperty]
        bool isPasswordVisible;


        [RelayCommand]
        async Task Register()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await AppUtils.ShowAlert("Por favor, llene todos los campos.");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await AppUtils.ShowAlert("Las contraseñas no coinciden.");
                return;
            }

            if (!AppUtils.IsValidEmail(Email))
            {
                await AppUtils.ShowAlert("Por favor, ingrese un correo electrónico válido.");
                return;
            }

            if (!AppUtils.IsValidPassword(Password))
            {
                await AppUtils.ShowAlert("La contraseña debe tener entre 8 y 15 caracteres, al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.");
                return;
            }

            //Verify if user already exists
            var userExists = (await patientRepository.GetAllBy(o => o.Object.Email == Email)).Count != 0;
            if (userExists)
            {
                await AppUtils.ShowAlert("El usuario con ese correo electrónico ya está registrado como paciente.");
                return;
            }

            var user = new Patient
            {
                Email = Email,
                Password = Password.Md5Encrypt()
            };

            // Save user to database
            string key = await patientRepository.Add(user);

            //Display success message
            await AppUtils.ShowAlert($"Usuario registrado con éxito. {key}", AlertType.Success);
        }

        public void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
#if !DEBUG
            //Clear fields
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            IsPasswordVisible = false;
#endif
        }
    }
}