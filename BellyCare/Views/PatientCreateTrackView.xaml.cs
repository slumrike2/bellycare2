using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class PatientCreateTrackView : BaseContentPage
{
	public PatientCreateTrackView(PatientCreateTrackViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}