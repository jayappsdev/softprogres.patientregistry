using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.Helpers;

public interface IBirthNumberHelper
{
    public DateTime GetDateOfBirthFromBirthNumber(string birthNumber);
    public int GetAgeFromBirthNumber(string birthNumber);
    public Sex GetSexFromBirthNumber(string birthNumber);
}