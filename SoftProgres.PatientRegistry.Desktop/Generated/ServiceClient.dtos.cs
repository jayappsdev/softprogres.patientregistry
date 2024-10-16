/* Options:
Date: 2024-10-11 13:43:47
Version: 8.40
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: https://localhost:5001

//GlobalNamespace: 
//MakePartial: True
//MakeVirtual: True
//MakeInternal: False
//MakeDataContractsExtensible: False
//AddNullableAnnotations: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//ExportValueTypes: False
//IncludeTypes: 
//ExcludeTypes: 
//AddNamespaces: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using SoftProgres.PatientRegistry.Api.Database.Dtos;
using SoftProgres.PatientRegistry.Api.ServiceModel.Types;
using SoftProgres.PatientRegistry.Api.ServiceModel;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace SoftProgres.PatientRegistry.Api.Database.Dtos
{
    public partial class PatientData
    {
        public virtual string BirthNumber { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string StreetAndNumber { get; set; }
        public virtual string City { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string State { get; set; }
        public virtual string Workplace { get; set; }
    }

}

namespace SoftProgres.PatientRegistry.Api.ServiceModel
{
    [Route("/api/patients", "POST")]
    public partial class CreatePatient
        : PatientData, IReturn<CreatePatientResponse>, IPost
    {
    }

    public partial class CreatePatientResponse
    {
        public virtual Patient Patient { get; set; }
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/api/patients/{PatientId}", "DELETE")]
    public partial class DeletePatient
        : IReturn<EmptyResponse>, IDelete
    {
        public virtual long PatientId { get; set; }
    }

    [Route("/api/patients/{PatientId}", "GET")]
    public partial class GetPatient
        : IReturn<GetPatientResponse>, IGet
    {
        public virtual long PatientId { get; set; }
    }

    public partial class GetPatientResponse
    {
        public virtual Patient Patient { get; set; }
    }

    [Route("/api/patients", "GET")]
    public partial class GetPatients
        : IReturn<GetPatientsResponse>, IGet
    {
    }

    public partial class GetPatientsResponse
    {
        public GetPatientsResponse()
        {
            Patients = new List<Patient>{};
        }

        public virtual List<Patient> Patients { get; set; }
    }

    [Route("/api/patients/{PatientId}", "PUT")]
    public partial class UpdatePatient
        : PatientData, IReturn<UpdatePatientResponse>, IPut
    {
        public virtual long PatientId { get; set; }
    }

    public partial class UpdatePatientResponse
    {
        public virtual Patient Patient { get; set; }
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

}

namespace SoftProgres.PatientRegistry.Api.ServiceModel.Types
{
    public partial class Patient
        : PatientData
    {
        public virtual long Id { get; set; }
        public virtual int? Age { get; set; }
        public virtual DateTime? DateOfBirth { get; set; }
        public virtual Sex? Sex { get; set; }
    }

    public enum Sex
    {
        Male = 1,
        Female = 2,
    }

}

