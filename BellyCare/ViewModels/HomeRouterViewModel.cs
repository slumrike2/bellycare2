using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.ViewModels
{
    public partial class HomeRouterViewModel(ISettingsService settings, INavigationService navigationService) : 
                         BaseViewModel(settings, navigationService), IEventfulViewModel
    {
        public void OnAppearing()
        {
            //Check if the user is a patient, doctor or admin and string Test
            if(settings.UserType == LoggedUserType.Patient)
            {
            }
            else if(settings.UserType == LoggedUserType.Doctor)
            {
            }
            else if(settings.UserType == LoggedUserType.Admin)
            {
            }
        }

        public void OnDisappearing()
        {
        }
    }
}