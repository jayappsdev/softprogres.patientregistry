using ServiceStack.DataAnnotations;

namespace SoftProgres.PatientRegistry.Api.ServiceModel.Types;

[EnumAsInt]
public enum Sex
{
    Male = 1,
    Female = 2,
}