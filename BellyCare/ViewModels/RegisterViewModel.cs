using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Database;
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
        string names;

        [ObservableProperty]
        string lastNames;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmPassword;

        [ObservableProperty]
        DateTime birthDate;

        [ObservableProperty]
        string culturalGroup;

        [ObservableProperty]
        string identificationNumber;

        [ObservableProperty]
        string province;

        [ObservableProperty]
        string canton;

        [ObservableProperty]
        string mainStreet;

        [ObservableProperty]
        string secondaryStreet;

        [ObservableProperty]
        bool hasInsurance;

        [ObservableProperty]
        string phoneNumber;

        [ObservableProperty]
        bool isPasswordVisible;


        [RelayCommand]
        async Task Register()
        {
            var user = new User
            {
                Role = IsPatient ? "Patient" : "Doctor",
                Names = Names,
                Lastnames = LastNames,
                Email = Email,
                Password = Password,
                Birthdate = BirthDate,
                SelectedCulturalGroup = CulturalGroup,
                IdentificationNumber = IdentificationNumber,
                Province = Province,
                Canton = Canton,
                MainStreet = MainStreet,
                SecondaryStreet = SecondaryStreet,
                HasInsurance = HasInsurance == true,
                PhoneNumber = PhoneNumber
            };

            // Save user to database
            var result = await db.Child(nameof(User)).PostAsync(user);
            
            //Display success message
            await Application.Current.MainPage.DisplayAlert("Success", $"User registered successfully. {result.Key}", "Ok");
        }

        public void OnAppearing()
        {
        }
        public void OnDisappearing()
        {
        }
    }
}