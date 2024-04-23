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
    public partial class PatientProfileViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;

        public PatientProfileViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
        }

        #region Properties
        [ObservableProperty]
        string names;

        [ObservableProperty]
        string lastNames;

        [ObservableProperty]
        string identificationNumber;

        [ObservableProperty]
        string phone;

        [ObservableProperty]
        DateTime birthDate;

        [ObservableProperty]
        string culturalGroup;

        [ObservableProperty]
        int pregnanciesCount;

        [ObservableProperty]
        int naturalBirthsCount;

        [ObservableProperty]
        int cesareanBirthsCount;

        [ObservableProperty]
        DateTime lastMenstruationDate;

        [ObservableProperty]
        string province;

        [ObservableProperty]
        string canton;

        [ObservableProperty]
        string parish;

        [ObservableProperty]
        string mainStreet;

        [ObservableProperty]
        string secondaryStreet;

        [ObservableProperty]
        string addressReference;

        [ObservableProperty]
        bool hasInsurance;

        [ObservableProperty]
        string insuranceName;
        #endregion

        [RelayCommand]
        async Task Save()
        {
            bool isFormFilled = IsFormFilled();
            if(!isFormFilled)
            {
                await AppUtils.ShowAlert("Por favor, llene todos los campos.");
                return;
            }

            Patient patient = new()
            {
                IsFullRegistered = true,
                Names = Names,
                Lastnames = LastNames,
                IdentificationNumber = IdentificationNumber,
                PhoneNumber = Phone,
                SelectedCulturalGroup = CulturalGroup,
                Province = Province,
                Canton = Canton,
                Parish = Parish,
                MainStreet = MainStreet,
                SecondaryStreet = SecondaryStreet,
                AdressReference = AddressReference,
                InsuranceName = InsuranceName
            };

            try
            {
                await patientRepository.Update(settings.AccessToken, patient);
            }
            catch (Exception)
            {
                await AppUtils.ShowAlert("Ocurrió un error al guardar los datos. Por favor, inténtelo de nuevo.");
                return;
            }
        }

        private bool IsFormFilled()
        {
            return 
                !string.IsNullOrEmpty(Names) && 
                !string.IsNullOrEmpty(LastNames) &&
                !string.IsNullOrEmpty(IdentificationNumber) &&
                !string.IsNullOrEmpty(Phone) &&
                BirthDate != null &&
                !string.IsNullOrEmpty(CulturalGroup) &&
                PregnanciesCount > 0 &&
                NaturalBirthsCount >= 0 &&
                CesareanBirthsCount >= 0 &&
                LastMenstruationDate != null &&
                !string.IsNullOrEmpty(Province) &&
                !string.IsNullOrEmpty(Canton) &&
                !string.IsNullOrEmpty(Parish) &&
                !string.IsNullOrEmpty(MainStreet) &&
                !string.IsNullOrEmpty(SecondaryStreet) &&
                !string.IsNullOrEmpty(AddressReference) &&
                (!HasInsurance || !string.IsNullOrEmpty(InsuranceName));
        }

        public void OnAppearing()
        {
        }

        public void OnDisappearing()
        {
        }
    }
}