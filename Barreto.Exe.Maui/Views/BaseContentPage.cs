using Barreto.Exe.Maui.ViewModels;

namespace Barreto.Exe.Maui.Views
{
    public class BaseContentPage : ContentPage
    {
        private readonly IEventfulViewModel viewModel;
        public BaseContentPage(IEventfulViewModel viewModel)
        {
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.OnDisappearing();
        }
    }
}