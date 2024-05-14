using BellyCare.Views;

namespace BellyCare.Shells
{
    public partial class DoctorShell : Shell
    {
        public DoctorShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PatientProfileView), typeof(PatientProfileView));
            Routing.RegisterRoute(nameof(PatientHomeView), typeof(PatientHomeView));
            Routing.RegisterRoute(nameof(AdminCreateDoctorView), typeof(AdminCreateDoctorView));
        }
    }
}
