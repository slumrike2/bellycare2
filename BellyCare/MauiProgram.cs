using Barreto.Exe.Maui.Services.Navigation;
using Barreto.Exe.Maui.Services.Settings;
using Barreto.Exe.Maui.Utils;
using BellyCare.Repositories;
using BellyCare.Services;
using BellyCare.Shells;
using BellyCare.ViewModels;
using BellyCare.Views;
using Firebase.Database;
using Microsoft.Extensions.Logging;

namespace BellyCare
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "IconFont");
                });

            string dbUrl = AppUtils.GetConfigValue("FIREBASE_URL");

            //DI Services
            builder.Services.AddSingleton(new FirebaseClient(dbUrl, new()
            {
                OfflineDatabaseFactory = (t, s) => new Firebase.Database.Offline.OfflineDatabase(t, s)
            }));
            builder.Services.AddSingleton<IBaseSettingsService, BaseSettingsService>();
            builder.Services.AddSingleton<ISettingsService, SettingsService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton(typeof(BaseOnlineRepository<>));

            //DI Shells
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginShell>();
            builder.Services.AddSingleton<AdminShell>();

            //DI ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<HomeRouterViewModel>();
            builder.Services.AddSingleton<PatientProfileViewModel>();
            builder.Services.AddSingleton<PatientHomeViewModel>();
            builder.Services.AddSingleton<AdminHomeViewModel>();
            builder.Services.AddSingleton<AdminCreateDoctorViewModel>();
            builder.Services.AddSingleton<PatientProgressViewModel>();
            builder.Services.AddSingleton<PatientCreateTrackViewModel>();
            builder.Services.AddSingleton<DoctorHomeViewModel>();


            //DI Views
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<HomeRouterView>();
            builder.Services.AddSingleton<PatientProfileView>();
            builder.Services.AddSingleton<PatientHomeView>();
            builder.Services.AddSingleton<AdminHomeView>();
            builder.Services.AddSingleton<AdminCreateDoctorView>();
            builder.Services.AddSingleton<PatientProgressView>();
            builder.Services.AddSingleton<PatientCreateTrackView>();
            builder.Services.AddSingleton<DoctorHomeView>();

            Connectivity.ConnectivityChanged += OnConnectivityChanged;

#if DEBUG
            builder.Logging.AddDebug();
#endif

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
#if WINDOWS
            h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0,0,0,0);
#endif
            });
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
#if WINDOWS
            h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0,0,0,0);
#endif
            });
            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif

#if WINDOWS
                h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0, 0, 0, 0);
#endif
            });

            return builder.Build();
        }

        private static async void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            await AppUtils.CheckConnection();
        }
    }
}
