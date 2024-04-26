using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class AdminCreateDoctorView : BaseContentPage
{
	public AdminCreateDoctorView(AdminCreateDoctorViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}