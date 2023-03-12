using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;

namespace MupenUtilitiesRedux.Views.Avalonia.UserControls;
// https://stackoverflow.com/a/73857903/14472122
public partial class SkiaCanvas : UserControl
{
    public static readonly StyledProperty<bool> IsRealtimeProperty =
        AvaloniaProperty.Register<Joystick, bool>(nameof(IsRealtime));

    // TODO: implement
    public bool IsRealtime
    {
        get => GetValue(IsRealtimeProperty);
        set => SetValue(IsRealtimeProperty, value);
    }
    
    class RenderingLogic : ICustomDrawOperation
    {
        public Action<SKCanvas> RenderCall;
        public Rect Bounds { get; set; }

        public void Dispose() {}

        public bool Equals(ICustomDrawOperation? other) => other == this;

        // not sure what goes here....
        public bool HitTest(Point p) { return false; }

        public void Render(IDrawingContextImpl context)
        {
            var canvas = (context as ISkiaDrawingContextImpl)?.SkCanvas;
            if(canvas != null)
            {
                Render(canvas);
            }
        }

        private void Render(SKCanvas canvas)
        {
             
            RenderCall?.Invoke(canvas);
        }
    }

    RenderingLogic renderingLogic;

    public event Action<SKCanvas> RenderSkia;

    public SkiaCanvas()
    {
        InitializeComponent();

        renderingLogic = new RenderingLogic();
        renderingLogic.RenderCall += (canvas) =>
        {
            Dispatcher.UIThread.InvokeAsync(() => RenderSkia?.Invoke(canvas));
        };
    }

    public override void Render(DrawingContext context)
    {
        renderingLogic.Bounds = new Rect(0, 0, this.Bounds.Width, this.Bounds.Height);

        context.Custom(renderingLogic);

        // Dispatcher.UIThread.InvokeAsync(() => context.Custom(renderingLogic), DispatcherPriority.Render);
        // If you want continual invalidation (like a game):
        //Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
    }
}
