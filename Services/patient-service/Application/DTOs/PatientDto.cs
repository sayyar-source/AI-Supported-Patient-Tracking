namespace PatientService.Application.DTOs;
public class PatientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
    public List<MedicalRecordDto> MedicalRecords { get; set; } = new();
}
