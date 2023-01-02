using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MupenUtilitiesRedux.Views.WPF.Converters;

internal class SubtractConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is int && parameter is int) return (int)value - (int)parameter;
		if (value is sbyte && parameter is string)
		{
			return (sbyte)value - int.Parse((string)parameter);
		}
		
		return DependencyProperty.UnsetValue;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}