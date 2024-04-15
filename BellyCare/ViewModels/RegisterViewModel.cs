using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
using Firebase.Database.Offline;
using Firebase.Database.Query;

namespace BellyCare.ViewModels
{
    public partial class RegisterViewModel : BaseViewModel, IEventfulViewModel
    {
        public RegisterViewModel(FirebaseClient db) : base(db)
        {
            IsPatient = true;
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

            var user = new User
            {
                Role = IsPatient ? "Patient" : "Doctor",
                Email = Email,
                Password = Password,
            };

            // Save user to database
            var result = db.Child(nameof(User)).AsRealtimeDatabase<User>().Post(user);
            
            //Display success message
            await AppUtils.ShowAlert($"Usuario registrado con éxito. {result}", AlertType.Success);
        }

        public void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}