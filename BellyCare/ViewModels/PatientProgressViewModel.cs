using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BellyCare.ViewModels
{
    [QueryProperty(nameof(PatientId), nameof(PatientId))]
    public partial class PatientProgressViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;

        [ObservableProperty]
        ObservableCollection<TrackEntry> trackEntries;

        [ObservableProperty]
        string patientId;

        [ObservableProperty]
        bool isLoading;

        public PatientProgressViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
        }

        public async void OnAppearing()
        {
            IsLoading = true;

            string patientId = PatientId ?? settings.AccessToken;

            var entries =
                (await patientRepository.GetAllBy(x => x.Key == patientId))
                .FirstOrDefault()?
                .Object
                .TrackEntries;

            if (entries is null)
            {
                //TODO - Show message
                IsLoading = false;
                return;
            }

            TrackEntries = new ObservableCollection<TrackEntry>(entries);

            IsLoading = false;
        }

        public void OnDisappearing()
        {
        }
    }
}