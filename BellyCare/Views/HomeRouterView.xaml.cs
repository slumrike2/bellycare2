using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class HomeRouterView : BaseContentPage
{
	public HomeRouterView(HomeRouterViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}