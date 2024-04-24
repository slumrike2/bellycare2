using Barreto.Exe.Maui.Views;
using BellyCare.ViewModels;

namespace BellyCare.Views;

public partial class PatientHomeView : BaseContentPage
{
    public PatientHomeView(PatientHomeViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}