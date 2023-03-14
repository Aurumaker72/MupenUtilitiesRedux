using System;
using Avalonia.Markup.Xaml;

namespace MupenUtilitiesRedux.Views.Avalonia.MarkupExtensions;

public class Int32MarkupExtension : MarkupExtension
{
    public Int32MarkupExtension(int value) { Value = value; }
    public int Value { get; }
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Value;
    }
}