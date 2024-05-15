using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using BellyCare.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BellyCare.ViewModels
{
    public partial class DoctorHomeViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;

        [ObservableProperty]
        ObservableCollection<FirebasePatient> patients = [];

        #region Properties
        [ObservableProperty]
        string doctorName;

        [ObservableProperty]
        string doctorCode;

        [ObservableProperty]
        bool isListEmpty;
        #endregion

        public DoctorHomeViewModel(ISettingsService settings, INavigationService navigationService, BaseOnlineRepository<Patient> patientRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
        }

        [RelayCommand]
        void ClickLogout()
        {
            Logout();
        }

        [RelayCommand]
        async Task TapDetail(FirebasePatient patient)
        {
            await navigation.NavigateToAsync<PatientHomeView>(new()
            {
                { "PatientId", patient.Id },
                { "Patient", patient.Patient }
            });
        }

        [RelayCommand]
        async Task CopyCode()
        {
            await Clipboard.SetTextAsync(DoctorCode);

            //Display toast
            var toast = Toast.Make("Copiado al portapapeles", ToastDuration.Short);
            await toast.Show();
        }

        public async void OnAppearing()
        {
            var doctor = settings.Doctor;
            DoctorName = $"{doctor.Speciality} {doctor.Names} {doctor.Lastnames}";
            DoctorCode = doctor.Code;

            try
            {
                var patients = (await patientRepository.GetAllBy(x => x.Object.DoctorCode == settings.Doctor.Code))?
                    .Select(x => new FirebasePatient()
                    {
                        Id = x.Key,
                        Patient = x.Object
                    })
                    .ToList();

                if (patients != null)
                {
                    Patients = new ObservableCollection<FirebasePatient>(patients);
                }
            }
            catch (Exception ex)
            {
                await AppUtils.ShowAlert("No se ha podido obtener la lista de pacientes. Chequea tu conexión a internet.", AlertType.Warning);
            }

            IsListEmpty = Patients.Count == 0;
        }

        public void OnDisappearing()
        {
        }
    }
}