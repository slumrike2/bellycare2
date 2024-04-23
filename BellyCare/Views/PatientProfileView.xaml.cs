using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class PatientProfileView : BaseContentPage
{
	public PatientProfileView(PatientProfileViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}