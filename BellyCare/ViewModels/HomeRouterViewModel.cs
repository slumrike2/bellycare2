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
                bool isFullRegistered = false;

                for(int i = 0; i < 3; i++)
                {
                    try
                    {
                        Patient? patient = await patientRepository.GetById(settings.AccessToken);
                        isFullRegistered = patient?.IsFullRegistered ?? false;
                        break;
                    }
                    catch (Exception ex)
                    {
                        await AppUtils.ShowAlert("Error al obtener los datos del paciente.");
                        Logout();
                    }
                }


                if(isFullRegistered)
                {
                    //await navigation.NavigateToAsync<PatientHomeView>();
                    Application.Current.MainPage = new PatientShell();
                }
                else
                {
                    await AppUtils.ShowAlert("Debe completar sus datos.");
                    await navigation.NavigateToAsync<PatientProfileView>();
                }
            }
            else if(settings.UserType == LoggedUserType.Doctor)
            {
            }
            else if(settings.UserType == LoggedUserType.Admin)
            {
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