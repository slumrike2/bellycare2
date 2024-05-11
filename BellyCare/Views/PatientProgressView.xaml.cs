using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class PatientProgressView : BaseContentPage
{
	public PatientProgressView(PatientProgressViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}