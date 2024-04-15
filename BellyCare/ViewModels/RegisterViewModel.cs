using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;

namespace BellyCare.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Doctor> doctorRepository;
        private readonly BaseOnlineRepository<Patient> patientRepository;

        public RegisterViewModel(BaseOnlineRepository<Doctor> doctorRepository, BaseOnlineRepository<Patient> patientRepository)
        {
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;

            IsPatient = true;

#if DEBUG
            Email = "luis@gmail.com";
            Password = "Luis2024*";
            ConfirmPassword = "Luis2024*";
#endif
        }

        [ObservableProperty]
        bool isPatient;

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

            dynamic repository = IsPatient ? patientRepository : doctorRepository;

            if(IsPatient)
            {
                await RegisterPatient();
            }
            else
            {
                await RegisterDoctor();
            }

            async Task RegisterPatient()
            {
                //Verify if user already exists
                var userExists = (await patientRepository.GetAllBy(o => o.Email == Email)).Any();
                if (userExists)
                {
                    await AppUtils.ShowAlert("El usuario con ese correo electrónico ya está registrado como paciente.");
                    return;
                }

                var user = new Patient
                {
                    Email = Email,
                    Password = Password,
                };

                // Save user to database
                string key = await patientRepository.Add(user);

                //Display success message
                await AppUtils.ShowAlert($"Usuario registrado con éxito. {key}", AlertType.Success);
            }
            async Task RegisterDoctor()
            {
                //Verify if user already exists
                var userExists = (await doctorRepository.GetAllBy(o => o.Email == Email)).Any();
                if (userExists)
                {
                    await AppUtils.ShowAlert("El usuario con ese correo electrónico ya está registrado como profesional de la salud.");
                    return;
                }

                var user = new Doctor
                {
                    Email = Email,
                    Password = Password,
                };

                // Save user to database
                string key = await doctorRepository.Add(user);

                //Display success message
                await AppUtils.ShowAlert($"Usuario registrado con éxito. {key}", AlertType.Success);
            }
        }

        public void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}