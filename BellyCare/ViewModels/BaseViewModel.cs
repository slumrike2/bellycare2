using Barreto.Exe.Maui.Services.Navigation;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.ViewModels
{
    public class BaseViewModel(ISettingsService settings, INavigationService navigationService) : ObservableObject
    {
        protected readonly ISettingsService settings = settings;
        protected readonly INavigationService navigation = navigationService;

        protected void Logout()
        {
            settings.Patient = null;
            settings.Doctor = null;
            settings.Admin = null;
            settings.AccessToken = null;

            navigation.RestartSession();
        }
    }
}