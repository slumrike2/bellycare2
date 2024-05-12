﻿using Barreto.Exe.Maui.Services.Navigation;
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
    [QueryProperty(nameof(PatientId), nameof(PatientId))]
    public partial class PatientProgressViewModel : BaseViewModel, IEventfulViewModel
    {
        private readonly BaseOnlineRepository<Patient> patientRepository;
        private BaseOnlineRepository<TrackEntry> trackRepository;

        [ObservableProperty]
        ObservableCollection<FirebaseTrackEntry> trackEntries;

        [ObservableProperty]
        string patientId;

        [ObservableProperty]
        bool isLoading;

        [ObservableProperty]
        double currentImc;

        [ObservableProperty]
        string healthStatus;

        public PatientProgressViewModel(
            ISettingsService settings, 
            INavigationService navigationService,
            BaseOnlineRepository<Patient> patientRepository) : base(settings, navigationService)
        {
            this.patientRepository = patientRepository;
        }

        [RelayCommand]
        async Task TapDetail(FirebaseTrackEntry entry)
        {
            await navigation.NavigateToAsync<PatientCreateTrackView>(new()
            {
                { "TrackRepository", trackRepository },
                { "Entry", entry}
            });
        }

        [RelayCommand]
        async Task Create()
        {
            await navigation.NavigateToAsync<PatientCreateTrackView>(new()
            {
                { "TrackRepository", trackRepository },
            });
        }

        void SetHealthStatus()
        {
            double? imc = TrackEntries.OrderByDescending(x => x.TrackEntry.Date)?.FirstOrDefault()?.TrackEntry.IMC;

            if (imc is null)
            {
                CurrentImc = 0;
                HealthStatus = "Sin datos";
                return;
            }

            CurrentImc = imc.Value;

            if (imc < 18.5)
            {
                HealthStatus = "Bajo peso";
            }
            else if (imc < 24.9)
            {
                HealthStatus = "¡Saludable!";
            }
            else if (imc < 29.9)
            {
                HealthStatus = "Sobrepeso";
            }
            else
            {
                HealthStatus = "Obesidad";
            }
        }

        public async void OnAppearing()
        {
            IsLoading = true;

            string patientId = PatientId ?? settings.AccessToken;
            trackRepository = patientRepository.GetChildRepository<TrackEntry>(patientId, "TrackEntries");

            var entries = (await trackRepository.GetAll()).Select(x => new FirebaseTrackEntry()
            {
                Id = x.Key,
                TrackEntry = x.Object

            }).ToList()
            .OrderByDescending(x => x.TrackEntry.Date);

            if (entries is null)
            {
                //TODO - Show message
                IsLoading = false;
                return;
            }

            TrackEntries = new(entries);

            SetHealthStatus();

            IsLoading = false;
        }

        public void OnDisappearing()
        {
        }
    }
}