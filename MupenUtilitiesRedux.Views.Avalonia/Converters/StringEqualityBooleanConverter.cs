using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MupenUtilitiesRedux.Views.Avalonia.Converters;

internal class StringEqualityBooleanConverter : IValueConverter
{
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if (value is string a && parameter is string b) return string.Equals(a, b);

		return false;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}