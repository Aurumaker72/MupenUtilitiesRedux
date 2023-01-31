using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MupenUtilitiesRedux.Views.WPF.UserControls;

/// <summary>
///     Interaction logic for Joystick.xaml
/// </summary>
public partial class Joystick : UserControl
{
    public static readonly DependencyProperty XProperty =
        DependencyProperty.Register("X", typeof(sbyte), typeof(Joystick), new PropertyMetadata((sbyte)0));

    public static readonly DependencyProperty YProperty =
        DependencyProperty.Register("Y", typeof(sbyte), typeof(Joystick), new PropertyMetadata((sbyte)0));


    public Joystick()
    {
        InitializeComponent();
    }


    public sbyte X
    {
        get => (sbyte)GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    public sbyte Y
    {
        get => (sbyte)GetValue(YProperty);
        set => SetValue(YProperty, value);
    }

    private void Border_OnMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed) return;


        SetJoystickPosition(e.GetPosition(Border));
    }

    private void SetJoystickPosition(Point point)
    {
        var x = (sbyte)Math.Clamp(point.X - Border.Width / 2, sbyte.MinValue, sbyte.MaxValue);
        var y = (sbyte)Math.Clamp(point.Y - Border.Height / 2, sbyte.MinValue, sbyte.MaxValue);

        X = x;
        Y = y;
    }

    private void Border_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed) return;

        Border.CaptureMouse();

        SetJoystickPosition(e.GetPosition(Border));
    }

    private void Border_OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        Border.ReleaseMouseCapture();
    }
}