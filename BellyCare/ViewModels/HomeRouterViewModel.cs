using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using BellyCare.Shells;
using BellyCare.Views;
using CommunityToolkit.Mvvm.Input;

namespace BellyCare.ViewModels
{
    public partial class HomeRouterViewModel : 
                         BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;
        private readonly BaseOnlineRepository<Doctor> doctorRepository;
        private readonly BaseOnlineRepository<Admin> adminRepository;

        public HomeRouterViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository, 
            BaseOnlineRepository<Doctor> doctorRepository,
            BaseOnlineRepository<Admin> adminRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.adminRepository = adminRepository;
        }

        public async void OnAppearing()
        {
            //Check if the user is a patient, doctor or admin and string Test
            if(settings.UserType == LoggedUserType.Patient)
            {
                await GoToPatient();
            }
            else if(settings.UserType == LoggedUserType.Doctor)
            {
            }
            else if(settings.UserType == LoggedUserType.Admin)
            {
                await GoToAdmin();
            }
        }

        private async Task GoToAdmin()
        {
            Admin admin;

            try
            {
                admin = await adminRepository.GetById(settings.AccessToken);
            }
            catch (Exception)
            {
                //Offline mode
                //admin = settings.Admin;
            }

            Application.Current.MainPage = new AdminShell();
        }

        private async Task GoToPatient()
        {
            Patient patient;

            try
            {
                patient = await patientRepository.GetById(settings.AccessToken);
            }
            catch (Exception)
            {
                //Offline mode
                patient = settings.Patient;
            }

            bool isFullRegistered = patient?.IsFullRegistered ?? false;
            if (isFullRegistered)
            {
                Application.Current.MainPage = new PatientShell();
            }
            else
            {
                await AppUtils.ShowAlert("Debe completar sus datos.");
                await navigation.NavigateToAsync<PatientProfileView>();
            }
        }

        [RelayCommand]
        void ClickLogout()
        {
            Logout();
        }

        public void OnDisappearing()
        {
        }
    }
}