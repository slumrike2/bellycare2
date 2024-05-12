using BellyCare.Views;

namespace BellyCare.Shells
{
    public partial class PatientShell : Shell
    {
        public PatientShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PatientProfileView), typeof(PatientProfileView));
            Routing.RegisterRoute(nameof(PatientHomeView), typeof(PatientHomeView));
            Routing.RegisterRoute(nameof(PatientCreateTrackView), typeof(PatientCreateTrackView));
        }
    }
}
