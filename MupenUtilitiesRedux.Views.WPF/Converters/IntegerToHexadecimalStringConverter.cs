﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace MupenUtilitiesRedux.Views.WPF.Converters;

public class IntegerToHexadecimalStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return $"0x{(int)value:X}";
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}