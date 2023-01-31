﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace MupenUtilitiesRedux.Views.WPF.Converters;

public class StringEqualityToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((string)value).Equals((string)parameter);
    }

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (string)parameter;
    }
}