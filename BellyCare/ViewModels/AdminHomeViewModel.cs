using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using BellyCare.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BellyCare.ViewModels
{
    public partial class AdminHomeViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Doctor> doctorRepository;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        ObservableCollection<FirebaseDoctor> doctors = [];

        [ObservableProperty]
        bool isDoctorsListEmpty;

        public AdminHomeViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Doctor> doctorRepository) : base(settings, navigationService)
        {
            this.doctorRepository = doctorRepository;
        }

        [RelayCommand]
        void ClickLogout()
        {
            Logout();
        }

        [RelayCommand]
        async Task ClickCreateDoctor()
        {
            await navigation.NavigateToAsync<AdminCreateDoctorView>();
        }

        [RelayCommand]
        void TapDetail(FirebaseDoctor doctor)
        {
            navigation.NavigateToAsync<AdminCreateDoctorView>(new()
            {
                { "DoctorId", doctor.Id },
                { "Doctor", doctor.Doctor }
            });
        }

        public async void OnAppearing()
        {
            IsLoading = true;

            try
            {
                var doctors = (await doctorRepository.GetAll())?.Select(d => new FirebaseDoctor()
                {
                    Id = d.Key,
                    Doctor = d.Object
                });

                if (doctors != null)
                {
                    Doctors = new ObservableCollection<FirebaseDoctor>(doctors);
                }
            }
            catch (Exception ex)
            {
                AppUtils.ShowAlert("No se ha podido obtener la lista de profesionales de la salud. Chequea tu conexión a internet.", AlertType.Warning);
            }

            IsDoctorsListEmpty = Doctors.Count == 0;
            IsLoading = false;
        }
        public void OnDisappearing()
        {
        }
    }
}