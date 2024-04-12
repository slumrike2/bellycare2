﻿using Barreto.Exe.Maui.Utils;
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

            string dbUrl = AppEnvironment.GetValue("FIREBASE_URL");

            //DI Services
            builder.Services.AddSingleton(new FirebaseClient(dbUrl));
            builder.Services.AddSingleton<ISettingsService, SettingsService>();

            //DI Shells
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<LoginShell>();

            //DI ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();

            //DI Views
            builder.Services.AddSingleton<LoginView>();
            builder.Services.AddSingleton<RegisterView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}