using Avalonia;
using Avalonia.Controls;

namespace MupenUtilitiesRedux.Views.Avalonia.UserControls;

public partial class KeyedStringField : UserControl
{
	public static readonly StyledProperty<string> LabelProperty =
		AvaloniaProperty.Register<KeyedStringField, string>(nameof(Label));

	public static readonly StyledProperty<string> ValueProperty =
		AvaloniaProperty.Register<KeyedStringField, string>(nameof(Value));

	public static readonly StyledProperty<bool> IsReadOnlyProperty =
		AvaloniaProperty.Register<KeyedStringField, bool>(nameof(IsReadOnly));


	public KeyedStringField()
	{
		InitializeComponent();
	}

	public string Label
	{
		get => GetValue(LabelProperty);
		set => SetValue(LabelProperty, value);
	}

	public string Value
	{
		get => GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public bool IsReadOnly
	{
		get => GetValue(IsReadOnlyProperty);
		set => SetValue(IsReadOnlyProperty, value);
	}
}