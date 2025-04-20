using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;
using PatientService.Infrastructure.Data;

namespace PatientService.Infrastructure.Repositories;
public class PatientRepository : IPatientRepository
{
    private readonly AppDbContext _context;

    public PatientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientInfo>> GetAllAsync()
    {
        return await _context.Patients.Include(p => p.MedicalRecords).ToListAsync();
    }

    public async Task<PatientInfo> GetByIdAsync(Guid id)
    {
        return await _context.Patients.Include(p => p.MedicalRecords).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(PatientInfo patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(PatientInfo patient)
    {
        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
    }
}
