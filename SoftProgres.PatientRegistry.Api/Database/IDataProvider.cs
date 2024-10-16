using SoftProgres.PatientRegistry.Api.Database.Dtos;
using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.Database;

public interface IDataProvider
{
    public Task<bool> BirthNumberExists(string birthNumber, long? currentPatientId = null);
    public Task<List<Patient>> GetPatientsAsync();
    public Task<Patient> GetPatientAsync(long patientId);
    public Task<Patient> CreatePatientAsync(PatientData patientData);
    public Task<Patient> UpdatePatientAsync(long patientId, PatientData patientData);
    public Task<bool> DeletePatientAsync(long patientId);
}