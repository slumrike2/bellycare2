using Barreto.Exe.Maui.Utils;
using Barreto.Exe.Maui.ViewModels;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace Barreto.Exe.Maui.Views
{
    public class BaseContentPage : ContentPage
    {
        private readonly IEventfulViewModel viewModel;
        public BaseContentPage(IEventfulViewModel viewModel)
        {
            this.viewModel = viewModel;
            BindingContext = viewModel;

            var rgbColor = AppUtils.GetConfigValue("PRIMARY_COLOR");

            // Create a StatusBarBehavior instance
            StatusBarBehavior statusBarBehavior = new()
            {
                StatusBarColor = Color.FromHex(rgbColor),
            };

            // Add the behavior to the page's behaviors collection
            Behaviors.Add(statusBarBehavior);
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