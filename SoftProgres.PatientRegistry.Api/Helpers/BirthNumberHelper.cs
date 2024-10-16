using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.Helpers;

public class BirthNumberHelper : IBirthNumberHelper
{
    /// <summary>
    /// Získa dátum narodenia osoby z rodného čísla.
    /// Rodné číslo nie je potrebné validovať, predpokladajte, že je validné.
    /// </summary>
    /// <param name="birthNumber">Validné rodné číslo v tvare s lomkou alebo bez lomky.</param>
    /// <returns>Dátum narodenia osoby</returns>
    public DateTime GetDateOfBirthFromBirthNumber(string birthNumber)
    {
        // TODO implementovať získanie dátumu narodenia osoby z rodného čísla.
        string cleanNumber = birthNumber.Replace("/", "");

        string yearPart = cleanNumber.Substring(0, 2);
        string monthPart = cleanNumber.Substring(2, 2);
        string dayPart = cleanNumber.Substring(4, 2);

        int year = int.Parse(yearPart);
        int month = int.Parse(monthPart);
        int day = int.Parse(dayPart);

        year += year < 54 ? 2000 : 1900;

        if (month > 50)
        {
            month -= 50;
        }

        return new DateTime(year, month, day);
    }

    /// <summary>
    /// Získa dátum narodenia osoby z rodného čísla.
    /// Rodné číslo nie je potrebné validovať, predpokladajte, že je validné.
    /// </summary>
    /// <param name="birthNumber">Validné rodné číslo v tvare s lomkou alebo bez lomky.</param>
    /// <returns>Vek osoby</returns>
    public int GetAgeFromBirthNumber(string birthNumber)
    {
        // TODO implementovať získanie veku osoby z rodného čísla.
        DateTime dateOfBirth = GetDateOfBirthFromBirthNumber(birthNumber);

        DateTime today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;

        if (today.Month < dateOfBirth.Month || (today.Month == dateOfBirth.Month && today.Day < dateOfBirth.Day))
        {
            age--;
        }

        return age; ;
    }

    /// <summary>
    /// Získa pohlavie osoby z rodného čísla.
    /// Rodné číslo nie je potrebné validovať, predpokladajte, že je validné.
    /// </summary>
    /// <param name="birthNumber">Validné rodné číslo v tvare s lomkou alebo bez lomky.</param>
    /// <returns>Pohlavie osoby</returns>
    public Sex GetSexFromBirthNumber(string birthNumber)
    {
        // TODO implementovať získanie pohlavia osoby z rodného čísla.
        string cleanNumber = birthNumber.Replace("/", "");

        int month = int.Parse(cleanNumber.Substring(2, 2));

        return month > 50 ? Sex.Female : Sex.Male;
    }
}