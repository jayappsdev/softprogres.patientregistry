namespace SoftProgres.PatientRegistry.Api.Validators;

public interface IBirthNumberValidator
{
    public bool IsBirthNumberValid(string birthNumber);
}