namespace PatientService.Application.DTOs;
public class CreatePatientDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birthdate { get; set; }
}
