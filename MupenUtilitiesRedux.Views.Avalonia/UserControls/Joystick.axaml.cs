using System;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using SkiaSharp;

namespace MupenUtilitiesRedux.Views.Avalonia.UserControls;

public partial class Joystick : UserControl, INotifyPropertyChanged
{
    public static readonly StyledProperty<sbyte> XProperty =
        AvaloniaProperty.Register<Joystick, sbyte>(nameof(X));

    public static readonly StyledProperty<sbyte> YProperty =
        AvaloniaProperty.Register<Joystick, sbyte>(nameof(Y));

    public sbyte X
    {
        get => GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    public sbyte Y
    {
        get => GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    public Point VisualPoint => new(128 + X, 128 + Y);
    public Point EllipseVisualPoint => new(128 + X - (double)Resources["TipDiameter"]! / 2, 128 + Y -(double)Resources["TipDiameter"]! / 2);

    public Joystick()
    {
        InitializeComponent();
        XProperty.Changed.Subscribe(delegate
        {
            RaisePropertyChanged(nameof(VisualPoint));
            RaisePropertyChanged(nameof(EllipseVisualPoint));
        });
        YProperty.Changed.Subscribe(delegate
        {
            RaisePropertyChanged(nameof(VisualPoint));
            RaisePropertyChanged(nameof(EllipseVisualPoint));
        });
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SkiaCanvas_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (e.Pointer.Captured == null)
        {
            return;
        }
        var point = e.GetPosition(sender as IInputElement);
        X = (sbyte)Math.Clamp(point.X - 128, sbyte.MinValue, sbyte.MaxValue);
        Y = (sbyte)Math.Clamp(point.Y - 128, sbyte.MinValue, sbyte.MaxValue);
    }

    private void SkiaCanvas_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Pointer.Capture(sender as IInputElement);
    }

    private void SkiaCanvas_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        e.Pointer.Capture(null);
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}