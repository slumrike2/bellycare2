using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views
{
    public partial class LoginView : BaseContentPage
    {
        public LoginView(LoginViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
