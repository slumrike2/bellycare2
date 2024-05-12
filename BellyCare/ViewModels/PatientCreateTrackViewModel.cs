using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
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
        string? weight;

        [ObservableProperty]
        string? bellySize;

        [ObservableProperty]
        string? heartRate;

        [ObservableProperty]
        string? respiratoryRate;

        [ObservableProperty]
        string? oxygenSaturation;

        [ObservableProperty]
        string bloodPressure;

        [ObservableProperty]
        string hemoglobin;

        [ObservableProperty]
        string? glucose;

        [ObservableProperty]
        string? temperature;

        [ObservableProperty]
        string? abdominalCircumference;

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
        async Task Save()
        {
            TrackEntry track = new()
            {
                Date = Date,
                Weight = double.TryParse(Weight, out double weight) ? weight : null,
                BellySize = double.TryParse(BellySize, out double bellySize) ? bellySize : null,
                HeartRate = double.TryParse(HeartRate, out double heartRate) ? heartRate : null,
                RespiratoryRate = double.TryParse(RespiratoryRate, out double respiratoryRate) ? respiratoryRate : null,
                OxygenSaturation = double.TryParse(OxygenSaturation, out double oxygenSaturation) ? oxygenSaturation : null,
                BloodPressure = double.TryParse(BloodPressure, out double bloodPressure) ? bloodPressure : null,
                Hemoglobin = double.TryParse(Hemoglobin, out double hemoglobin) ? hemoglobin : null,
                Glucose = double.TryParse(Glucose, out double glucose) ? glucose : null,
                Temperature = double.TryParse(Temperature, out double temperature) ? temperature : null,
                AbdominalCircumference = double.TryParse(AbdominalCircumference, out double abdominalCircumference) ? abdominalCircumference : null,
                LabResults = LabResults,
                VdrlTest = VdrlTest,
                VdrlResult = VdrlResult,
                VdrlDate = VdrlDate,
                Treatment = Treatment,
                Note = Note,
                IMC = weight != 0 && bellySize != 0 ? weight / (bellySize / 100) : 0
            };

            try
            {
                if (Entry is not null)
                {
                    TrackRepository.Update(Entry.Id, track);
                }
                else
                {
                    TrackRepository.Add(track);
                }

                await AppUtils.ShowAlert("Datos guardados correctamente", AlertType.Success);
                await navigation.GoBackAsync();
            }
            catch
            {
                await AppUtils.ShowAlert("Ocurrió un error al guardar los datos, por favor intenta de nuevo.", AlertType.Error);
            }
        }


        void FillForm()
        {
            Date = Entry.TrackEntry.Date;
            Weight = Entry.TrackEntry.Weight?.ToString() ?? string.Empty;
            BellySize = Entry.TrackEntry.BellySize?.ToString() ?? string.Empty;
            HeartRate = Entry.TrackEntry.HeartRate?.ToString() ?? string.Empty;
            RespiratoryRate = Entry.TrackEntry.RespiratoryRate?.ToString() ?? string.Empty;
            OxygenSaturation = Entry.TrackEntry.OxygenSaturation?.ToString() ?? string.Empty;
            BloodPressure = Entry.TrackEntry.BloodPressure?.ToString() ?? string.Empty;
            Hemoglobin = Entry.TrackEntry.Hemoglobin?.ToString() ?? string.Empty;
            Glucose = Entry.TrackEntry.Glucose?.ToString() ?? string.Empty;
            Temperature = Entry.TrackEntry.Temperature?.ToString() ?? string.Empty;
            AbdominalCircumference = Entry.TrackEntry.AbdominalCircumference?.ToString() ?? string.Empty;
            LabResults = Entry.TrackEntry.LabResults;
            VdrlTest = Entry.TrackEntry.VdrlTest ?? false;
            VdrlResult = Entry.TrackEntry.VdrlResult ?? false;
            VdrlDate = Entry.TrackEntry.VdrlDate ?? DateTime.Now;
            Treatment = Entry.TrackEntry.Treatment ?? string.Empty;
            Note = Entry.TrackEntry.Note ?? string.Empty;
        }

        public void OnAppearing()
        {
            if(Entry is not null)
            {
                FillForm();
            }
            else
            {
                Date = DateTime.Now;
                VdrlTest = false;
                VdrlResult = false;
                VdrlDate = DateTime.Now;
            }
        }

        public void OnDisappearing()
        {
        }
    }
}