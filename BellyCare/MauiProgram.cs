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
            builder.Services.AddSingleton(new FirebaseClient(dbUrl));
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            Connectivity.ConnectivityChanged += OnConnectivityChanged;

            return builder.Build();
        }

        private static async void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            await AppUtils.CheckConnection();
        }
    }
}
