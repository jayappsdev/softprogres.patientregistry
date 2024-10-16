using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SoftProgres.PatientRegistry.Desktop.Converters;

public class CollectionNotEmptyToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is ICollection { Count: > 0 } ? Visibility.Visible : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}