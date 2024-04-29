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
    [QueryProperty(nameof(DoctorId), nameof(DoctorId))]
    [QueryProperty(nameof(Doctor), nameof(Doctor))]
    public partial class AdminCreateDoctorViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Doctor> doctorRepository;

        #region Properties
        [ObservableProperty]
        string doctorId;

        [ObservableProperty]
        Doctor doctor;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmPassword;

        [ObservableProperty]
        bool isPasswordVisible;

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
        string selectedSpeciality;

        [ObservableProperty]
        string professionalCode;
        #endregion

        public AdminCreateDoctorViewModel(
            BaseOnlineRepository<Doctor> doctorRepository,
            ISettingsService settings, 
            INavigationService navigationService) : base(settings, navigationService)
        {
            this.doctorRepository = doctorRepository;
        }

        [RelayCommand]
        async Task Save()
        {
            if (!IsFormFilled())
            {
                await AppUtils.ShowAlert("Por favor, complete todos los campos.", AlertType.Warning);
                return;
            }

            if (string.IsNullOrEmpty(DoctorId) && Password != ConfirmPassword)
            {
                await AppUtils.ShowAlert("Las contraseñas no coinciden.", AlertType.Warning);
                return;
            }

            //If creating doctor
            if (string.IsNullOrEmpty(DoctorId))
            {
                await CreateDoctor();
            }
            //If editing doctor
            else
            {
                await UpdateDoctor();
            }
        }
        async Task CreateDoctor()
        {
            Doctor doctor = new()
            {
                Email = Email,
                Password = Password.ToMd5(),
                Names = Names,
                Lastnames = LastNames,
                IdentificationNumber = IdentificationNumber,
                PhoneNumber = Phone,
                BirthDate = BirthDate,
                Speciality = SelectedSpeciality,
                Code = ProfessionalCode
            };

            bool error = false;

            try
            {
                string key = doctorRepository.Add(doctor);
                if (string.IsNullOrEmpty(key))
                {
                    error = true;
                }
            }
            catch (Exception)
            {
                error = true;
            }

            if (error)
            {
                await AppUtils.ShowAlert("No se pudo crear el profesional. Por favor, intente de nuevo.", AlertType.Error);
            }
            else
            {
                await AppUtils.ShowAlert("Profesional creado exitosamente.", AlertType.Success);
                await navigation.GoBackAsync();
            }
        }
        async Task UpdateDoctor()
        {
            Doctor doctor = new()
            {
                Email = Email,
                Password = !string.IsNullOrEmpty(Password) ? Password.ToMd5() : Doctor.Password,
                Names = Names,
                Lastnames = LastNames,
                IdentificationNumber = IdentificationNumber,
                PhoneNumber = Phone,
                BirthDate = BirthDate,
                Speciality = SelectedSpeciality,
                Code = ProfessionalCode
            };

            try
            {
                doctorRepository.Update(DoctorId, doctor);
                await AppUtils.ShowAlert("Profesional actualizado exitosamente.", AlertType.Success);
                await navigation.GoBackAsync();
            }
            catch (Exception)
            {
                await AppUtils.ShowAlert("No se pudo actualizar el profesional. Por favor, intente de nuevo.", AlertType.Error);
            }
        }

        async Task PreloadForm()
        {
            bool error = false;

            try
            {
                Doctor doctor = await doctorRepository.GetById(DoctorId);

                if (doctor != null)
                {
                    Names = doctor.Names;
                    LastNames = doctor.Lastnames;
                    Email = doctor.Email;
                    Password = string.Empty;
                    ConfirmPassword = string.Empty;
                    IdentificationNumber = doctor.IdentificationNumber;
                    Phone = doctor.PhoneNumber;
                    BirthDate = doctor.BirthDate ?? DateTime.Now;
                    SelectedSpeciality = doctor.Speciality;
                    ProfessionalCode = doctor.Code;
                }
                else
                {
                    error = true;
                }
            }
            catch (Exception)
            {
                error = true;
            }

            if (error)
            {
                await AppUtils.ShowAlert("No se pudo cargar la información del profesional seleccionado. Por favor, inténtelo de nuevo.", AlertType.Error);
                await navigation.GoBackAsync();
            }
        }
        async Task ResetForm()
        {
            Email = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            Names = string.Empty;
            LastNames = string.Empty;
            IdentificationNumber = string.Empty;
            Phone = string.Empty;
            BirthDate = DateTime.Now;
            SelectedSpeciality = string.Empty;
            ProfessionalCode = await GenerateDoctorCode();
        }
        bool IsFormFilled()
        {
            return
                //Validate password and confirm password if creating doctor
                (string.IsNullOrEmpty(DoctorId) || (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword))) &&
                
                //Validate rest of the fields
                !string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Names) &&
                !string.IsNullOrEmpty(LastNames) &&
                !string.IsNullOrEmpty(IdentificationNumber) &&
                !string.IsNullOrEmpty(Phone) &&
                BirthDate != null &&
                !string.IsNullOrEmpty(SelectedSpeciality);
        }
        async Task<string> GenerateDoctorCode()
        {
            string code = "BC";
            Random random = new();

            do
            {
                //Generate random 6 digitsnumber
                code += random.Next(100000, 999999).ToString();

                try
                {
                    bool exists = (await doctorRepository.GetAllBy(x => x.Object.Code == code)).Count > 0;
                    if (!exists)
                    {
                        return code;
                    }
                }
                catch (Exception)
                {
                    await AppUtils.ShowAlert("Ocurrió un error al generar el código del profesional. Por favor, inténtelo de nuevo.", AlertType.Error);
                    await navigation.GoBackAsync();
                    return null;
                }
            } while (true);
        }

        public async void OnAppearing()
        {
            // Is editing doctor
            if(!string.IsNullOrEmpty(DoctorId))
            {
                await PreloadForm();
            }
            // Is creating doctor
            else
            {
                await ResetForm();
            }
        }

        public void OnDisappearing()
        {
        }
    }
}