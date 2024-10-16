using SoftProgres.PatientRegistry.Api.Database;
using SoftProgres.PatientRegistry.Api.ServiceModel;
using SoftProgres.PatientRegistry.Api.Validators;

namespace SoftProgres.PatientRegistry.Api.ServiceInterface
{
    public class PatientServices(IDataProvider dataProvider, IBirthNumberValidator birthNumberValidator) : Service
    {
        private IDataProvider _dataProvider = dataProvider;
        private IBirthNumberValidator _birthNumberValidator = birthNumberValidator;

        public async Task<GetPatientsResponse> Get(GetPatients _)
        {
            var patients = await _dataProvider.GetPatientsAsync();
            return new GetPatientsResponse
            {
                Patients = patients
            };
        }

        public async Task<GetPatientResponse> Get(GetPatient request)
        {
            var patient = await _dataProvider.GetPatientAsync(request.PatientId);
            return new GetPatientResponse
            {
                Patient = patient
            };
        }

        // TODO - zavolať validáciu rodného čísla na mieste, kde je to vhodné
        // TODO - v prípade chybného rodného čísla thrownúť HttpError.BadRequest("Rodné číslo nemá správny formát.");
        public async Task<CreatePatientResponse> Post(CreatePatient request)
        {
            if (!_birthNumberValidator.IsBirthNumberValid(request.BirthNumber))
            {
                throw HttpError.BadRequest("Rodné číslo nemá správny formát.");
            }

            // Skontroluj či rodné číslo už existuje
            if (await _dataProvider.BirthNumberExists(request.BirthNumber))
            {
                throw HttpError.Conflict("Rodné číslo už existuje.");
            }
            
            // Vytvor pacienta v databáze s požadovanými údajmi.
            var patient = await _dataProvider.CreatePatientAsync(request);
            
            // Vrát vytvoreného pacienta.
            return new CreatePatientResponse
            {
                Patient = patient
            };
        }

        // TODO - zavolať validáciu rodného čísla na mieste, kde je to vhodné
        // TODO - v prípade chybného rodného čísla thrownúť HttpError.BadRequest("Rodné číslo nemá správny formát.");
        public async Task<UpdatePatientResponse> Put(UpdatePatient request)
        {
            if (!_birthNumberValidator.IsBirthNumberValid(request.BirthNumber))
            {
                throw HttpError.BadRequest("Rodné číslo nemá správny formát.");
            }

            // Skontroluj či rodné číslo už existuje
            if (await _dataProvider.BirthNumberExists(request.BirthNumber, request.PatientId))
            {
                throw HttpError.Conflict("Rodné číslo už existuje.");
            }
            
            // Aktualizuj údaje pacienta v databáze.
            var patient = await _dataProvider.UpdatePatientAsync(request.PatientId, request);
            
            // Vráť pacienta s aktualizovanými údajmi.
            return new UpdatePatientResponse
            {
                Patient = patient
            };
        }

        public async Task<EmptyResponse> Delete(DeletePatient request)
        {
            await _dataProvider.DeletePatientAsync(request.PatientId);
            return new EmptyResponse();
        }
    }
}