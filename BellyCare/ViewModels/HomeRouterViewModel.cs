using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Services;
using BellyCare.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BellyCare.ViewModels
{
    public partial class HomeRouterViewModel(ISettingsService settings, INavigationService navigationService) : 
                         BaseViewModel(settings, navigationService), IEventfulViewModel
    {
        public async void OnAppearing()
        {
            //Check if the user is a patient, doctor or admin and string Test
            if(settings.UserType == LoggedUserType.Patient)
            {
                if(settings.Patient.IsFullRegistered)
                {

                }
                else
                {
                    //Mensaje: Debe completar sus datos.
                    //await AppUtils.ShowAlert("Debe completar sus datos.");
                    //await navigation.NavigateToAsync<PatientProfileView>();
                }
            }
            else if(settings.UserType == LoggedUserType.Doctor)
            {
            }
            else if(settings.UserType == LoggedUserType.Admin)
            {
            }
        }

        [RelayCommand]
        void Logout()
        {
            settings.Patient = null;
            settings.Doctor = null;
            settings.Admin = null;
            settings.AccessToken = null;

            navigation.RestartSession();
        }

        public void OnDisappearing()
        {
        }
    }
}