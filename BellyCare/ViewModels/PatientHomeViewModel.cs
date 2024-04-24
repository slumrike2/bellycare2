using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Services;
using CommunityToolkit.Mvvm.Input;

namespace BellyCare.ViewModels
{
    public partial class PatientHomeViewModel : BaseViewModel, IEventfulViewModel
    {
        public PatientHomeViewModel(ISettingsService settings, INavigationService navigationService) : base(settings, navigationService)
        {
        }

        [RelayCommand]
        void ClickLogout()
        {
            Logout();
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}