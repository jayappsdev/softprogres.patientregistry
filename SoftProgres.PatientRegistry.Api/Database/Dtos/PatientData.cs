using ServiceStack.DataAnnotations;
using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.Database.Dtos;

[Alias(nameof(Patient))]
public class PatientData
{
    public string BirthNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; } 
    
    public string StreetAndNumber { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;
    public string? Workplace { get; set; }
    
    
}