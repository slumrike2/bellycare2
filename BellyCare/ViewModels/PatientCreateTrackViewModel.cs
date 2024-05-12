using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        #region Properties
        [ObservableProperty]
        DateTime date;

        [ObservableProperty]
        double? weight;

        [ObservableProperty]
        double? bellySize;

        [ObservableProperty]
        double? heartRate;

        [ObservableProperty]
        double? respiratoryRate;

        [ObservableProperty]
        double? oxygenSaturation;

        [ObservableProperty]
        string bloodPressure;

        [ObservableProperty]
        string hemoglobin;

        [ObservableProperty]
        double? glucose;

        [ObservableProperty]
        double? temperature;

        [ObservableProperty]
        double? abdominalCircumference;

        [ObservableProperty]
        string labResults;


        [ObservableProperty]
        bool? vdrlTest;

        [ObservableProperty]
        bool? vdrlResult;
        
        [ObservableProperty]
        DateTime? vdrlDate;

        [ObservableProperty]
        string treatment;

        [ObservableProperty]
        string note;
        #endregion

        public PatientCreateTrackViewModel(ISettingsService settings, INavigationService navigationService) : base(settings, navigationService)
        {
        }

        [RelayCommand]
        void Save()
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