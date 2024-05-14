using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class DoctorHomeView : BaseContentPage
{
	public DoctorHomeView(DoctorHomeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}