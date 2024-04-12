using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class HomeView : BaseContentPage
{
	public HomeView(HomeViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}