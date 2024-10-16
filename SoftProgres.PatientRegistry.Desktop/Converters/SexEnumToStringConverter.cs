using System.Globalization;
using System.Windows.Data;

namespace SoftProgres.PatientRegistry.Desktop.Converters;

public class SexEnumToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // TODO Implementujte konverter, ktorý na základe enumu Sex vráti reťazec "muž" alebo "žena" alebo "nevyplnené".
        throw new NotImplementedException();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Tu netreba robiť nič.
        throw new NotImplementedException();
    }
}