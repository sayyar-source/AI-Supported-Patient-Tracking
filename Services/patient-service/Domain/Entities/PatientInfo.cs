namespace PatientService.Domain.Entities;
public class PatientInfo: BaseEntity
{
    public string Name { get;  set; }
    public string Surname { get;  set; }
    public DateTime Birthdate { get;  set; }
    public List<MedicalRecord> MedicalRecords { get;  set; } = new();

    public PatientInfo(string name, string surname, DateTime birthdate)
    {
        Name = name;
        Surname = surname;
        Birthdate = birthdate;
    }

    // For EF Core
    private PatientInfo() { }
}
