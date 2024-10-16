using SoftProgres.PatientRegistry.Desktop.Models;

namespace SoftProgres.PatientRegistry.Desktop.Helpers;

public static class ServiceModelConverter
{
    public static Patient FromServiceToLocal(this Api.ServiceModel.Types.Patient patient)
    {
        return new Patient()
        {
            Id = patient.Id,
            BirthNumber = patient.BirthNumber,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Sex = patient.Sex.HasValue ?
                (patient.Sex.Value == Api.ServiceModel.Types.Sex.Male
                    ? Sex.Male
                    : Sex.Female
                ) : Sex.Unknown,
            Email = patient.Email,
            PhoneNumber = patient.PhoneNumber,
            StreetAndNumber = patient.StreetAndNumber,
            City = patient.City,
            PostalCode = patient.PostalCode,
            State = patient.State,
            Workplace = patient.Workplace,
            Age = patient.Age,
            DateOfBirth = patient.DateOfBirth,
        };
    }
}