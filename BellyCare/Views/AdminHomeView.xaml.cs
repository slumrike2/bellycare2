using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class AdminHomeView : BaseContentPage
{
	public AdminHomeView(AdminHomeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}