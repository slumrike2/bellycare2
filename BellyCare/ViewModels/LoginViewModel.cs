using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;

namespace BellyCare.ViewModels
{
    public partial class LoginViewModel(FirebaseClient db) : BaseViewModel(db), IEventfulViewModel
    {
        [ObservableProperty]
        string? email;

        [ObservableProperty]
        string? password;

        [ObservableProperty]
        string? test;

        [ObservableProperty]
        bool isPasswordVisible;

        [RelayCommand]
        void Login()
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
        }

        public async void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}
