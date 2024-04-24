using Barreto.Exe.Maui.Shells;
using BellyCare.Services;
using BellyCare.Shells;
using BellyCare.Views;

namespace BellyCare
{
    public partial class App : Application, IMultishellApp
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ISettingsService settingsService;
        public App(ISettingsService settingsService, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            this.settingsService = settingsService;
            this.serviceProvider = serviceProvider;
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            PreloadViews();

            bool isLoggedIn = settingsService.IsLoggedIn;
            if (isLoggedIn)
            {
                return new Window(new AppShell());
            }
            else
            {
                return new Window(new LoginShell());
            }
        } 
        
        public void GoToAppShell()
        {
            Current.MainPage = new AppShell();
        }

        public void GoToSessionShell()
        {
            Current.MainPage = new LoginShell();
        }

        public void PreloadViews()
        {
            serviceProvider.GetService<PatientProfileView>();
        }
    }
}
