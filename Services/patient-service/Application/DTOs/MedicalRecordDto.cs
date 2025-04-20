namespace PatientService.Application.DTOs;
public class MedicalRecordDto
{
    public Guid Id { get; set; }
    public string DoctorRemarks { get; set; }
    public DateTime RecordDate { get; set; }
}
