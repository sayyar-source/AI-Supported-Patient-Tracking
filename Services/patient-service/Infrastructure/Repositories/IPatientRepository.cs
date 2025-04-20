using PatientService.Domain.Entities;

namespace PatientService.Infrastructure.Repositories;
public interface IPatientRepository
{
    Task<IEnumerable<PatientInfo>> GetAllAsync();
    Task<PatientInfo> GetByIdAsync(Guid id);
    Task AddAsync(PatientInfo patient);
    Task DeleteAsync(PatientInfo patient);
}
