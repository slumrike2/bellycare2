using Barreto.Exe.Maui.Controls;
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
        private readonly BaseOnlineRepository<Doctor> doctorRepository;

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
        string[] culturalGroups = ["Mestizo", "Montubio", "Afroecuatoriano", "Indígena", "Blanco", "Otro"];

        [ObservableProperty]
        string culturalGroup;

        [ObservableProperty]
        string culturalGroupAlt;

        [ObservableProperty]
        bool isOtherCulturalGroup;

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

        [ObservableProperty]
        string doctorCode;
        #endregion

        #region Events
        [ObservableProperty]
        EventHandler selectedCulturalGroupChanged = (sender, e) =>
        {
            var labeledPicker = sender as LabeledPicker;
            if (labeledPicker?.BindingContext is PatientProfileViewModel vm)
            {
                vm.IsOtherCulturalGroup = vm.CulturalGroup == "Otro";
            }
        };
        #endregion

        public PatientProfileViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository,
            BaseOnlineRepository<Doctor> doctorRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
        }

        [RelayCommand]
        async Task Save()
        {
            bool isFormFilled = IsFormFilled();
            if(!isFormFilled)
            {
                await AppUtils.ShowAlert("Por favor, llene todos los campos.");
                return;
            }

            bool confirmed = await AppUtils.ShowAlert("¿Está seguro de que desea guardar los datos?", hasCancelButton: true);
            if(!confirmed)
            {
                return;
            }

            //Validate doctor code
            if(!string.IsNullOrEmpty(DoctorCode))
            {
                try
                {
                    var doctor = (await doctorRepository.GetAllBy(x => x.Object.Code == DoctorCode.Trim())).FirstOrDefault();

                    if(doctor == null)
                    {
                        await AppUtils.ShowAlert("El código de doctor ingresado no es válido.");
                        return;
                    }
                }
                catch (Exception)
                {
                    await AppUtils.ShowAlert("Error al validar el código de doctor. Por favor, inténtelo de nuevo.");
                    return;
                }
            }


            Patient patient = new()
            {
                IsFullRegistered = true,
                Names = Names.Trim(),
                Lastnames = LastNames.Trim(),
                Email = settings.Patient.Email.Trim(),
                Password = settings.Patient.Password.Trim(),
                IdentificationNumber = IdentificationNumber.Trim(),
                PhoneNumber = Phone.Trim(),
                BirthDate = BirthDate,
                SelectedCulturalGroup = CulturalGroup.Trim() == "Otro" ? CulturalGroupAlt.Trim() : CulturalGroup.Trim(),
                PregnanciesCount = PregnanciesCount,
                NaturalBirthsCount = NaturalBirthsCount,
                CesareanBirthsCount = CesareanBirthsCount,
                LastMenstruationDate = LastMenstruationDate,
                Province = Province.Trim(),
                Canton = Canton.Trim(),
                Parish = Parish.Trim(),
                MainStreet = MainStreet.Trim(),
                SecondaryStreet = SecondaryStreet.Trim(),
                AdressReference = AddressReference.Trim(),
                HasInsurance = HasInsurance,
                InsuranceName = InsuranceName?.Trim() ?? string.Empty,
                DoctorCode = DoctorCode
            };

            try
            {
                patientRepository.Update(settings.AccessToken, patient);
                settings.Patient = patient;
                await AppUtils.ShowAlert("Datos guardados correctamente.", AlertType.Success);
                await navigation.GoBackAsync();
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
                (CulturalGroup != "Otro" || !string.IsNullOrEmpty(CulturalGroupAlt)) &&
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

        private async void PreloadForm()
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

            bool isAltCultureGroup = !CulturalGroups.Contains(patient.SelectedCulturalGroup);

            Names = patient.Names;
            LastNames = patient.Lastnames;
            IdentificationNumber = patient.IdentificationNumber;
            Phone = patient.PhoneNumber;
            BirthDate = patient.BirthDate ?? DateTime.Now;
            CulturalGroup = isAltCultureGroup ? "Otro" : patient.SelectedCulturalGroup;
            CulturalGroupAlt = isAltCultureGroup ? patient.SelectedCulturalGroup : string.Empty;
            PregnanciesCount = patient.PregnanciesCount;
            NaturalBirthsCount = patient.NaturalBirthsCount;
            CesareanBirthsCount = patient.CesareanBirthsCount;
            LastMenstruationDate = patient.LastMenstruationDate ?? DateTime.Now;
            Province = patient.Province;
            Canton = patient.Canton;
            Parish = patient.Parish;
            MainStreet = patient.MainStreet;
            SecondaryStreet = patient.SecondaryStreet;
            AddressReference = patient.AdressReference;
            HasInsurance = patient.HasInsurance;
            InsuranceName = patient.InsuranceName;
            DoctorCode = patient.DoctorCode;
        }

        public void OnAppearing()
        {
            PreloadForm();
        }

        public void OnDisappearing()
        {
        }
    }
}