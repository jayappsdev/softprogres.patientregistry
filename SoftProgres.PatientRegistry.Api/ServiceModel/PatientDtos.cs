using SoftProgres.PatientRegistry.Api.Database.Dtos;
using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.ServiceModel;

[Route("/api/patients", "GET")]
public class GetPatients : IGet, IReturn<GetPatientsResponse>
{
    
}

public class GetPatientsResponse
{
    public List<Patient> Patients { get; set; } = [];
}

[Route("/api/patients/{PatientId}", "GET")]
public class GetPatient : IGet, IReturn<GetPatientResponse>
{
    public long PatientId { get; set; }
}

public class GetPatientResponse
{
    public Patient? Patient { get; set; }
}

[Route("/api/patients", "POST")]
public class CreatePatient : PatientData, IPost, IReturn<CreatePatientResponse>
{
}

public class CreatePatientResponse : IHasResponseStatus
{
    public Patient? Patient { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}

[Route("/api/patients/{PatientId}", "PUT")]
public class UpdatePatient : PatientData, IPut, IReturn<UpdatePatientResponse>
{
    public long PatientId { get; set; }
}

public class UpdatePatientResponse : IHasResponseStatus
{
    public Patient? Patient { get; set; }
    public ResponseStatus? ResponseStatus { get; set; }
}


[Route("/api/patients/{PatientId}", "DELETE")]
public class DeletePatient : IDelete, IReturn<EmptyResponse>
{
    public long PatientId { get; set; }
}