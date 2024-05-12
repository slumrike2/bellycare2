using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BellyCare.ViewModels
{
    [QueryProperty(nameof(TrackRepository), nameof(TrackRepository))]
    [QueryProperty(nameof(Entry), nameof(Entry))]
    public partial class PatientCreateTrackViewModel : BaseViewModel, IEventfulViewModel
    {
        [ObservableProperty]
        private BaseOnlineRepository<TrackEntry> trackRepository;

        [ObservableProperty]
        private FirebaseTrackEntry entry;

        public PatientCreateTrackViewModel(ISettingsService settings, INavigationService navigationService) : base(settings, navigationService)
        {
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}