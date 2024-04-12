using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views
{
    public partial class LoginView : BaseContentPage
    {
        int count = 0;

        public LoginView(LoginViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
