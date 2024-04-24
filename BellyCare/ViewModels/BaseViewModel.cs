using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.ViewModels
{
    public class BaseViewModel(ISettingsService settings, INavigationService navigationService) : ObservableObject
    {
        protected readonly ISettingsService settings = settings;
        protected readonly INavigationService navigation = navigationService;

        protected async void Logout()
        {
            bool confirm = await AppUtils.ShowAlert("¿Está seguro que desea cerrar sesión?", hasCancelButton: true);
            if (!confirm)
            {
                return;
            }

            settings.Patient = null;
            settings.Doctor = null;
            settings.Admin = null;
            settings.AccessToken = null;

            navigation.RestartSession();
        }
    }
}