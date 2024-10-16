using System.Globalization;
using System.Windows.Data;
using SoftProgres.PatientRegistry.Desktop.Models;

namespace SoftProgres.PatientRegistry.Desktop.Converters;

public class SexEnumToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // TODO Implementujte konverter, ktorý na základe enumu Sex vráti reťazec "muž" alebo "žena" alebo "nevyplnené".
        if (value is Sex sex)
        {
            switch (sex)
            {
                case Sex.Male:
                    return "Muž";
                case Sex.Female:
                    return "Žena";
                default:
                    return "Neznáme";
            }
        }
        return "Neznáme";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Tu netreba robiť nič.
        throw new NotImplementedException();
    }
}