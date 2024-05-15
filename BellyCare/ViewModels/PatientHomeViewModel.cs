using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.ViewModels;
using BellyCare.Models;
using BellyCare.Repositories;
using BellyCare.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections;
using BellyCare.Views;

namespace BellyCare.ViewModels
{
    [QueryProperty(nameof(PatientId), nameof(PatientId))]
    [QueryProperty(nameof(Patient), nameof(Patient))]
    public partial class PatientHomeViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;
        private readonly BaseOnlineRepository<Doctor> doctorRepository;
        private readonly BaseOnlineRepository<TrackEntry> trackRepository;
        private readonly BaseOnlineRepository<Chat> chatRepository;

        private Chat chat;

        private readonly IEnumerable<dynamic> fruitGrowthData = new List<dynamic>
        {
            new { Week = 8, Size = 1.6, Weight = 1, Fruit = "raspberry" },
            new { Week = 9, Size = 2.3, Weight = 2, Fruit = "olive" },
            new { Week = 10, Size = 3.1, Weight = 4, Fruit = "olive" },
            new { Week = 11, Size = 4.1, Weight = 7, Fruit = "lime" },
            new { Week = 12, Size = 5.4, Weight = 14, Fruit = "lime" },
            new { Week = 13, Size = 7.4, Weight = 23, Fruit = "plum" },
            new { Week = 14, Size = 8.7, Weight = 43, Fruit = "lemon" },
            new { Week = 15, Size = 10.1, Weight = 70, Fruit = "orange" },
            new { Week = 16, Size = 11.6, Weight = 100, Fruit = "avocado" },
            new { Week = 17, Size = 13.0, Weight = 140, Fruit = "onion" },
            new { Week = 18, Size = 14.2, Weight = 190, Fruit = "potato" },
            new { Week = 19, Size = 15.3, Weight = 240, Fruit = "mango" },
            new { Week = 20, Size = 16.4, Weight = 300, Fruit = "banana" },
            new { Week = 21, Size = 26.7, Weight = 360, Fruit = "carrot" },
            new { Week = 22, Size = 27.8, Weight = 430, Fruit = "papaya" },
            new { Week = 23, Size = 28.9, Weight = 501, Fruit = "papaya" },
            new { Week = 24, Size = 30.0, Weight = 600, Fruit = "melon" },
            new { Week = 25, Size = 34.6, Weight = 660, Fruit = "cauliflower" },
            new { Week = 26, Size = 35.6, Weight = 760, Fruit = "lettuce" },
            new { Week = 27, Size = 36.6, Weight = 875, Fruit = "turnip" },
            new { Week = 28, Size = 37.6, Weight = 1005, Fruit = "eggplant" },
            new { Week = 29, Size = 38.6, Weight = 1153, Fruit = "watermelon" },
            new { Week = 30, Size = 39.9, Weight = 1319, Fruit = "pineapple" },
            new { Week = 31, Size = 41.1, Weight = 1502, Fruit = "pineapple" },
            new { Week = 32, Size = 42.4, Weight = 1702, Fruit = "coconut" },
            new { Week = 33, Size = 43.7, Weight = 1918, Fruit = "coconut" },
            new { Week = 34, Size = 45.0, Weight = 2150, Fruit = "coconut" },
            new { Week = 35, Size = 46.2, Weight = 2383, Fruit = "coconut" },
            new { Week = 36, Size = 47.4, Weight = 2622, Fruit = "fetus" },
            new { Week = 37, Size = 48.6, Weight = 2859, Fruit = "fetus" },
            new { Week = 38, Size = 49.8, Weight = 3083, Fruit = "fetus" },
            new { Week = 39, Size = 50.7, Weight = 3288, Fruit = "fetus" },
            new { Week = 40, Size = 51.2, Weight = 3462, Fruit = "fetus" },
            new { Week = 41, Size = 51.7, Weight = 3597, Fruit = "fetus" },
            new { Week = 42, Size = 51.5, Weight = 3685, Fruit = "fetus" }
        };

        #region Properties
        [ObservableProperty]
        bool isDoctor;

        [ObservableProperty]
        string patientId;

        [ObservableProperty]
        Patient patient;

        [ObservableProperty]
        string greeting;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        DateTime probableBithDate;

        [ObservableProperty]
        string probableBithDateFormatted;

        [ObservableProperty]
        int daysLeft;

        [ObservableProperty]
        int currentWeek;

        [ObservableProperty]
        double currentProgress;

        [ObservableProperty]
        double currentWeight;

        [ObservableProperty]
        double currentSize;

        [ObservableProperty]
        string currentFruit;

        [ObservableProperty]
        string chatName;

        [ObservableProperty]
        int unreadMessages;
        #endregion

        public PatientHomeViewModel
            (ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository,
            BaseOnlineRepository<Doctor> doctorRepository,
            BaseOnlineRepository<Chat> chatRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            trackRepository = patientRepository.GetChildRepository<TrackEntry>(settings.AccessToken, "TrackEntries");
            this.chatRepository = chatRepository;
        }

        [RelayCommand]
        void ClickLogout()
        {
            Logout();
        }

        [RelayCommand]
        async Task ClickProgressView()
        {
            await navigation.NavigateToAsync<PatientProgressView>(new()
            {
                { "PatientId", PatientId },
            });
        }

        [RelayCommand]
        async Task ClickChat()
        {
            var patient = IsDoctor ? Patient : settings.Patient;

            await navigation.NavigateToAsync<ChatView>(new()
            {
                { "ChatId", patient.ChatId },
                { "ChatRepository", chatRepository },
                { "MessageRepository", chatRepository.GetChildRepository<ChatMessage>(patient.ChatId, "Messages") }
            });
        }

        public async void OnAppearing()
        {
            IsDoctor = settings.UserType == LoggedUserType.Doctor;
            var patient = IsDoctor ? Patient : settings.Patient;

            try
            {
                chat = await chatRepository.GetById(patient.ChatId);
            }
            catch (Exception)
            {
                ChatName = "Error al cargar información del chat.";
            }

            if (IsDoctor)
            {
                Greeting = Patient.Names;
                Name = Patient.Lastnames;
                SetDoctorChatInfo();
            }
            else
            {
                Greeting = "Bienvenida,";
                Name = settings.Patient.Names;
                await SetPatientChatInfo();
            }

            SetCurrentWeekData(patient);
        }

        private void SetCurrentWeekData(Patient patient)
        {
            if (patient.LastMenstruationDate is null) return;

            var lastMenstruationDate = patient.LastMenstruationDate.Value;

            ProbableBithDate = lastMenstruationDate.AddDays(280);
            ProbableBithDateFormatted = string.Format("{0:dd} de {0:MMMM} del {0:yyyy}", ProbableBithDate);

            DaysLeft = (int)(ProbableBithDate - DateTime.Now).TotalDays;
            CurrentWeek = (int)(DateTime.Now - lastMenstruationDate).TotalDays / 7;
            CurrentProgress = (280 - DaysLeft) / 280.0;

            var currentWeekData = fruitGrowthData.FirstOrDefault(data => data.Week >= CurrentWeek || (data.Week == 42 && CurrentWeek > 42));

            if (currentWeekData != null)
            {
                CurrentSize = currentWeekData.Size;
                CurrentWeight = currentWeekData.Weight;
                CurrentFruit = currentWeekData.Fruit + ".png";
            }
            else
            {
                // Handle the case where the current week is beyond the data range
                CurrentSize = 0;
                CurrentWeight = 0;
                CurrentFruit = "fetus.png";
            }
        }

        private async Task SetPatientChatInfo()
        {
            try
            {
                var doctor = (await doctorRepository.GetAllBy(x => x.Object.Code == settings.Patient.DoctorCode)).FirstOrDefault();
                if (doctor != null)
                {
                    switch (doctor.Object.Speciality)
                    {
                        case "Doctor":
                            ChatName = "Dr.";
                            break;
                        default:
                            ChatName = doctor.Object.Speciality;
                            break;
                    }

                    ChatName += " " + doctor.Object.Names + " " + doctor.Object.Lastnames;
                    UnreadMessages = chat.UnreadMessagesByPatient;
                }
                else
                {
                    ChatName = "Profesional de la salud no asignado.";
                }
            }
            catch (Exception ex)
            {
                ChatName = "Error al cargar información del profesional de la salud.";
            }
        }
        private void SetDoctorChatInfo()
        {
            ChatName = $"Chatear con {Patient.Names}";
            UnreadMessages = chat.UnreadMessagesByDoctor;
        }

        public void OnDisappearing()
        {
        }
    }
}