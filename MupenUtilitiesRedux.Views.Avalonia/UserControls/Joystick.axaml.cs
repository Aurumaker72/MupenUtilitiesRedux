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
        set
        {
            SetValue(XProperty, value);
            RaisePropertyChanged(nameof(VisualPoint));
            RaisePropertyChanged(nameof(EllipseVisualPoint));
        }
    }

    public sbyte Y
    {
        get => GetValue(YProperty);
        set
        {
            SetValue(YProperty, value);
            RaisePropertyChanged(nameof(VisualPoint));
            RaisePropertyChanged(nameof(EllipseVisualPoint));
        }
    }

    public Point VisualPoint => new(128 + X, 128 + Y);
    public Point EllipseVisualPoint => new(128 + X - (double)(Resources["TipDiameter"]) / 2, 128 + Y - (double)(Resources["TipDiameter"]) / 2);

    public Joystick()
    {
        InitializeComponent();
    }


    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SkiaCanvas_OnRenderSkia(SKCanvas obj)
    {
        obj.DrawLine(0f, 256f / 2f, 256f, 256f / 2f, new SKPaint
        {
            Color = new SKColor(0, 0, 0),
            StrokeWidth = 2f
        });
        obj.DrawLine(256f / 2f, 0, 256f / 2f, 256f, new SKPaint
        {
            Color = new SKColor(0, 0, 0),
            StrokeWidth = 2f
        });
        obj.DrawCircle(256f / 2f, 256f / 2f, 128f, new SKPaint
        {
            Color = new SKColor(0, 0, 0),
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2f
        });

        obj.DrawLine(256f / 2f, 256f / 2f, X, Y, new SKPaint
        {
            Color = new SKColor(0, 0, 255),
            StrokeWidth = 2f
        });

        obj.DrawCircle(X, Y, 4f, new SKPaint
        {
            Color = new SKColor(255, 0, 0)
        });
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