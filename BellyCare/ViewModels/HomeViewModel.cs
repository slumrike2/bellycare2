using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Services;
using CommunityToolkit.Mvvm.Input;

namespace BellyCare.ViewModels
{
    public partial class HomeViewModel(ISettingsService settings, INavigationService navigationService) : BaseViewModel(settings, navigationService), IEventfulViewModel
    {
        [RelayCommand]
        async Task Logout()
        {
            bool accept = await AppUtils.ShowAlert("¿Estás seguro de que deseas cerrar sesión?");
            if (!accept) return;

            settings.User = null;
            settings.AccessToken = null;

            navigationService.RestartSession();
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}