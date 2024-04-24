using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class PatientProfileView : BaseContentPage
{
	public PatientProfileView(PatientProfileViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
        base.OnAppearing();

		ScrollView.ScrollToAsync(0, 0, false);
    }
}