using BellyCare.Views;

namespace BellyCare.Shells
{
    public partial class AdminShell : Shell
    {
        public AdminShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PatientProfileView), typeof(PatientProfileView));
            Routing.RegisterRoute(nameof(PatientHomeView), typeof(PatientHomeView));
        }
    }
}
