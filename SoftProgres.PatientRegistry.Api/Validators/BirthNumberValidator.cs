using System.Text.RegularExpressions;

namespace SoftProgres.PatientRegistry.Api.Validators;

public class BirthNumberValidator : IBirthNumberValidator
{
    /// <summary>
    /// Implementácia validácie rodného čísla podľa zákona 301/1995 Z. z. o rodnom čísle § 2
    /// </summary>
    /// <param name="birthNumber">Vstupné rodné číslo. Za správny vstupný formát sa považuje RČ s lomkou aj bez lomky.</param>
    /// <returns>True ak je rodné číslo v správnom formáte a validné podľa zákona, inak false.</returns>
    public bool IsBirthNumberValid(string birthNumber)
    {
        // TODO Implementujte validáciu rodného čísla na základe zákona 301/1995 Z. z. § 2 
        // TODO https://www.slov-lex.sk/pravne-predpisy/SK/ZZ/1995/301/#paragraf-2
        var regex = new Regex(@"^\d{6}/?\d{3,4}$");
        if (!regex.IsMatch(birthNumber))
        {
            return false;
        }

        string cleanNumber = birthNumber.Replace("/", "");

        if (cleanNumber.Length == 10)
        {
            long birthNumberNumeric = long.Parse(cleanNumber);

            if (birthNumberNumeric % 11 != 0)
            {
                return false;
            }
        }

        return true;
    }
}