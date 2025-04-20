using PatientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PatientService.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public DbSet<PatientInfo> Patients { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

}
