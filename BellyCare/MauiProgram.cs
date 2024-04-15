using Barreto.Exe.Maui.Utils;
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
            builder.Services.AddSingleton<ISettingsService, SettingsService>();

            //DI Shells
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginShell>();

            //DI ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();

            //DI Views
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<RegisterView>();
            builder.Services.AddSingleton<HomeView>();
            
            Connectivity.ConnectivityChanged += OnConnectivityChanged;

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //Removes the underline from the Entry
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
#if WINDOWS
            h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0,0,0,0);
#endif
            });

            //Removes the underline from the Picker control
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
            h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
#if WINDOWS
            h.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0,0,0,0);
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
