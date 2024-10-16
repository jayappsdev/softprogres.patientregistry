using ServiceStack.DataAnnotations;
using SoftProgres.PatientRegistry.Api.Database.Dtos;

namespace SoftProgres.PatientRegistry.Api.ServiceModel.Types;

public class Patient : PatientData
{
    [AutoIncrement]
    [PrimaryKey]
    public long Id { get; set; }
    
    public int? Age { get; set; }
    
    public DateTime? DateOfBirth { get; set; }
    
    public Sex? Sex { get; set; }
}