using BellyCare.Views;

namespace BellyCare.Shells
{
    public partial class DoctorShell : Shell
    {
        public DoctorShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PatientHomeView), typeof(PatientHomeView));
            Routing.RegisterRoute(nameof(PatientProgressView), typeof(PatientProgressView));
            Routing.RegisterRoute(nameof(PatientCreateTrackView), typeof(PatientCreateTrackView));
        }
    }
}
