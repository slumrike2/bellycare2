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
        string bloodPressureMin;

        [ObservableProperty]
        string bloodPressureMax;

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
                BloodPressureMin = double.TryParse(BloodPressureMin, out double bloodPressureMin) ? bloodPressureMin : null,
                BloodPressureMax = double.TryParse(BloodPressureMax, out double bloodPressureMax) ? bloodPressureMax : null,
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
                IMC = weight != 0 && bellySize != 0 ? weight / Math.Pow(bellySize / 100, 2) : 0
            };

            bool isValid = await ValidateForm(track);
            if (!isValid) return;

            try
            {
                string entryId;
                if (Entry is not null)
                {
                    await TrackRepository.Update(Entry.Id, track);
                    entryId = Entry.Id;
                }
                else
                {
                    entryId = await TrackRepository.Add(track);
                }

                //If is patient, update track to offline data
                if (settings.UserType == LoggedUserType.Patient)
                {
                    var patient = settings.Patient;

                    if(patient.TrackEntries is null)
                    {
                        patient.TrackEntries = [];
                    }

                    bool exist = patient.TrackEntries.Any(x => x.Key == entryId);

                    if (exist)
                    {
                        patient.TrackEntries[entryId] = track;
                    }
                    else
                    {
                        patient.TrackEntries.Add(entryId, track);
                    }

                    settings.Patient = patient;
                }

                await AppUtils.ShowAlert("Datos guardados correctamente.", AlertType.Success);
                await navigation.GoBackAsync();
            }
            catch(Exception ex)
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
            BloodPressureMin = Entry.TrackEntry.BloodPressureMin?.ToString() ?? string.Empty;
            BloodPressureMax = Entry.TrackEntry.BloodPressureMax?.ToString() ?? string.Empty;
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

        void ResetForm()
        {
            Date = DateTime.Now;
            Weight = string.Empty;
            BellySize = string.Empty;
            HeartRate = string.Empty;
            RespiratoryRate = string.Empty;
            OxygenSaturation = string.Empty;
            BloodPressureMin = string.Empty;
            BloodPressureMax = string.Empty;
            Hemoglobin = string.Empty;
            Glucose = string.Empty;
            Temperature = string.Empty;
            AbdominalCircumference = string.Empty;
            LabResults = string.Empty;
            VdrlTest = false;
            VdrlResult = false;
            VdrlDate = DateTime.Now;
            Treatment = string.Empty;
            Note = string.Empty;
        }

        async Task<bool> ValidateForm(TrackEntry track)
        {
            if (track == null) return false;

            bool isValid = true;
            string message = string.Empty;

            if (track.Weight.HasValue && (track.Weight < 30 || track.Weight > 160))
            {
                isValid = false;
                message += "El peso debe estar entre 30 y 160 kg.\n";
            }

            if (track.BellySize.HasValue && (track.BellySize < 130 || track.BellySize > 220))
            {
                isValid = false;
                message += "La altura debe estar entre 130 y 220 cm.\n";
            }

            if (track.HeartRate.HasValue && (track.HeartRate < 40 || track.HeartRate > 130))
            {
                isValid = false;
                message += "La frecuencia cardiaca debe estar entre 40 y 130 ppm.\n";
            }

            if (track.RespiratoryRate.HasValue && (track.RespiratoryRate < 11 || track.RespiratoryRate > 30))
            {
                isValid = false;
                message += "La frecuencia respiratoria debe estar entre 11 y 30 rpm.\n";
            }

            if (track.OxygenSaturation.HasValue && (track.OxygenSaturation < 85 || track.OxygenSaturation > 100))
            {
                isValid = false;
                message += "La saturación de oxígeno debe estar entre 85 y 100%.\n";
            }

            if (track.BloodPressureMin.HasValue && (track.BloodPressureMin < 50 || track.BloodPressureMin > 120))
            {
                isValid = false;
                message += "La presión arterial mínima debe estar entre 50 y 120 mmHg.\n";
            }

            if (track.BloodPressureMax.HasValue && (track.BloodPressureMax < 70 || track.BloodPressureMax > 180))
            {
                isValid = false;
                message += "La presión arterial máxima debe estar entre 80 y 180 mmHg.\n";
            }

            if (track.Hemoglobin.HasValue && (track.Hemoglobin < 10 || track.Hemoglobin > 20))
            {
                isValid = false;
                message += "La hemoglobina debe estar entre 10 y 20 g/dL.\n";
            }

            if (track.Glucose.HasValue && (track.Glucose < 70 || track.Glucose > 140))
            {
                isValid = false;
                message += "La glucosa debe estar entre 70 y 140 mg/dL.\n";
            }

            if (track.Temperature.HasValue && (track.Temperature < 35 || track.Temperature > 40))
            {
                isValid = false;
                message += "La temperatura debe estar entre 35 y 40 °C.\n";
            }

            if (track.AbdominalCircumference.HasValue && (track.AbdominalCircumference < 50 || track.AbdominalCircumference > 150))
            {
                isValid = false;
                message += "La circunferencia abdominal debe estar entre 50 y 150 cm.\n";
            }

            if (!isValid)
            {
                await AppUtils.ShowAlert(message, AlertType.Error);
            }

            return isValid;
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
            ResetForm();
        }
    }
}