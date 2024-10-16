using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SoftProgres.PatientRegistry.Desktop.Converters;

public class FullAdressConverter : IMultiValueConverter
{
    private static readonly char[] trimChars = [',', ' '];

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string streetAndNumber = values[0] as string;
        string postalCode = values[1] as string;
        string city = values[2] as string;
        string state = values[3] as string;

        return $"{streetAndNumber}, {postalCode}, {city}, {state}".Trim(trimChars);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
