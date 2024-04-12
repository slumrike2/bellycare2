using BellyCare.Shells;
using BellyCare.ViewModels;
using BellyCare.Views;
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
                });

            //DI Shells
            builder.Services.AddSingleton<AppShell>();

            //DI ViewModels
            builder.Services.AddSingleton<LoginViewModel>();

            //DI Views
            builder.Services.AddSingleton<LoginView>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
