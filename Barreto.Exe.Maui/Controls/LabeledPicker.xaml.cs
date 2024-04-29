using Barreto.Exe.Maui.Utils;

namespace Barreto.Exe.Maui.Controls;

public partial class LabeledPicker : ContentView
{
    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(LabeledPicker), string.Empty);
    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(LabeledPicker), IconFont.Arrow_drop_down);
    public string Icon
    {
        get => (string)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty Placeholderroperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(LabeledPicker), string.Empty);
    public string Placeholder
    {
        get => (string)GetValue(Placeholderroperty);
        set => SetValue(Placeholderroperty, value);
    }

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(LabeledPicker), null, BindingMode.TwoWay);
    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(object), typeof(LabeledPicker), null);
    public object ItemsSource
    {
        get => (object)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public static readonly BindableProperty SelectedItemChangedProperty = BindableProperty.Create(nameof(SelectedItemChanged), typeof(EventHandler), typeof(LabeledPicker), null);
    public EventHandler SelectedItemChanged
    {
        get => (EventHandler)GetValue(SelectedItemChangedProperty);
        set => SetValue(SelectedItemChangedProperty, value);
    }

    public LabeledPicker()
	{
		InitializeComponent();
	}

    public void SelectedIndexChangedHandler(object sender, EventArgs e)
    {
        SelectedItemChanged?.Invoke(this, e);
    }

}