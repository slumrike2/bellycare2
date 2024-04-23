using Barreto.Exe.Maui.Utils;

namespace Barreto.Exe.Maui.Controls;

public partial class LabeledDatepicker : ContentView
{
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(LabeledDatepicker), string.Empty);
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(LabeledDatepicker), DateTime.Now);
    public DateTime Date
    {
        get => (DateTime)GetValue(DateProperty);
        set => SetValue(DateProperty, value);
    }

    public LabeledDatepicker()
	{
		InitializeComponent();
	}
}