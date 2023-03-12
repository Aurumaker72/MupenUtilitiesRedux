using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MupenUtilitiesRedux.Views.Avalonia.Converters;

public class SubtractConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not sbyte @sbyte) return null;
        
        return parameter switch
        {
            int @int => @sbyte - @int,
            double @double => @sbyte - @double,
            _ => null
        };
        
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}