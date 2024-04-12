using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class RegisterView : BaseContentPage
{
    public RegisterView(RegisterViewModel viewModel) : base(viewModel)
    {
		InitializeComponent();
	}
}