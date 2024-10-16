using ServiceStack.Data;
using ServiceStack.OrmLite;
using SoftProgres.PatientRegistry.Api.Database.Dtos;
using SoftProgres.PatientRegistry.Api.Helpers;
using SoftProgres.PatientRegistry.Api.ServiceModel.Types;

namespace SoftProgres.PatientRegistry.Api.Database;

public class DataProvider(IDbConnectionFactory dbDbConnectionFactory, IBirthNumberHelper birthNumberHelper)
    : IDataProvider
{
    private IDbConnectionFactory _dbConnectionFactory = dbDbConnectionFactory;
    private IBirthNumberHelper _birthNumberHelper = birthNumberHelper;

    public async Task<bool> BirthNumberExists(string birthNumber, long? currentPatientId = null)
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();
        return await dbConnection.ExistsAsync<Patient>(patient =>
            patient.BirthNumber == birthNumber && (currentPatientId == null || patient.Id != currentPatientId));
    }

    public async Task<List<Patient>> GetPatientsAsync()
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();
        var patients = await dbConnection.SelectAsync<Patient>();
        foreach (var patient in patients)
        {
            await dbConnection.LoadReferencesAsync(patient);
        }
        
        return patients;
    }

    public async Task<Patient> GetPatientAsync(long patientId)
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();
        var patient = await dbConnection.SingleAsync<Patient>(patient => patient.Id == patientId);
        await dbConnection.LoadReferencesAsync(patient);
        return patient;
    }

    // TODO zavolať funkcie z _birthNumberHelper tak, aby sa do databázy uložil k pacientovi jeho vek, pohlavie a dátum narodenia
    public async Task<Patient> CreatePatientAsync(PatientData patientData)
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();

        DateTime dateOfBirth = _birthNumberHelper.GetDateOfBirthFromBirthNumber(patientData.BirthNumber);
        int age = _birthNumberHelper.GetAgeFromBirthNumber(patientData.BirthNumber);
        var sex = _birthNumberHelper.GetSexFromBirthNumber(patientData.BirthNumber);

        // Vytvor objekt nového pacienta, ktorý sa uloží do databázy
        var newPatient = new Patient()
        {
            BirthNumber = patientData.BirthNumber,
            DateOfBirth = dateOfBirth,
            Age = age,
            Sex = sex,
            FirstName = patientData.FirstName,
            LastName = patientData.LastName,
            Email = patientData.Email,
            PhoneNumber = patientData.PhoneNumber,
            StreetAndNumber = patientData.StreetAndNumber,
            City = patientData.City,
            PostalCode = patientData.PostalCode,
            State = patientData.State,
            Workplace = patientData.Workplace
        };

        // Ulož nového pacienta do databázy
        var newId = await dbConnection.InsertAsync(newPatient, selectIdentity: true);
        newPatient.Id = newId;

        // Vrát nového pacienta
        return newPatient;
    }

    // TODO zavolať funkcie z _birthNumberHelper tak, aby sa do databázy uložil k pacientovi jeho vek, pohlavie a dátum narodenia
    public async Task<Patient> UpdatePatientAsync(long patientId, PatientData patientData)
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();

        DateTime dateOfBirth = _birthNumberHelper.GetDateOfBirthFromBirthNumber(patientData.BirthNumber);
        int age = _birthNumberHelper.GetAgeFromBirthNumber(patientData.BirthNumber);
        var sex = _birthNumberHelper.GetSexFromBirthNumber(patientData.BirthNumber);

        // Vytvor objekt pacienta so zmenenými údajmi, ktorý sa uloží do databázy
        var modifiedPatient = new Patient()
        {
            Id = patientId,
            BirthNumber = patientData.BirthNumber,
            DateOfBirth = dateOfBirth,
            Age = age,
            Sex = sex,
            FirstName = patientData.FirstName,
            LastName = patientData.LastName,
            Email = patientData.Email,
            PhoneNumber = patientData.PhoneNumber,
            StreetAndNumber = patientData.StreetAndNumber,
            City = patientData.City,
            PostalCode = patientData.PostalCode,
            State = patientData.State,
            Workplace = patientData.Workplace
        };

        // Ulož pacienta so zmenenými údajmi do databázy
        await dbConnection.UpdateAsync(modifiedPatient);

        // Vráť pacienta so zmenenými údajmi
        return modifiedPatient;
    }

    public async Task<bool> DeletePatientAsync(long patientId)
    {
        using var dbConnection = _dbConnectionFactory.OpenDbConnection();
        var rowsDeleted = await dbConnection.DeleteByIdAsync<Patient>(patientId);
        return rowsDeleted != 0;
    }
}