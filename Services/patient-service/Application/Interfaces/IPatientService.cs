using PatientService.Application.DTOs;

namespace PatientService.Application.Interfaces;
public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto> GetPatientByIdAsync(Guid id);
    Task CreatePatientAsync(CreatePatientDto dto);
    Task DeletePatientAsync(Guid id);
    Task<PredictionDto> GetAIPredictionAsync(Guid patientId);
}
