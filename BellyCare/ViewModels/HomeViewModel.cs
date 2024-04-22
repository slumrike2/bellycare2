using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Services;

namespace BellyCare.ViewModels
{
    public class HomeViewModel(ISettingsService settings, INavigationService navigationService) : BaseViewModel(settings, navigationService), IEventfulViewModel
    {
        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}