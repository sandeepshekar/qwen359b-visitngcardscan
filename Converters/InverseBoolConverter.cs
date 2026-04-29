using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Qwen359b.Converters;

/// <summary>
/// Converter that inverts a boolean value for use in XAML bindings.
/// </summary>
public class InverseBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            if (targetType == typeof(Visibility))
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return !boolValue;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility visibility)
        {
            return visibility == Visibility.Collapsed;
        }
        if (value is bool boolValue)
        {
            return !boolValue;
        }
        return false;
    }
}

