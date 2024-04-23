using Barreto.Exe.Maui.Services.Navigation;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Database;

namespace BellyCare.ViewModels
{
    public class BaseViewModel(ISettingsService settings, INavigationService navigationService) : ObservableObject
    {
        protected readonly ISettingsService settings = settings;
        protected readonly INavigationService navigation = navigationService;
    }
}