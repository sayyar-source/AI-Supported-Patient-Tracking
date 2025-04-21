using Microsoft.AspNetCore.Http;
using PatientService.Application.DTOs;
using PatientService.Application.Interfaces;
using PatientService.Domain.Entities;
using PatientService.Infrastructure.Repositories;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PatientService.Application.Services;
public class PatientServices : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public PatientServices(IPatientRepository patientRepository, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _patientRepository = patientRepository;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return patients.Select(p => new PatientDto
        {
            Id = p.Id,
            Name = p.Name,
            Surname = p.Surname,
            Birthdate = p.Birthdate,
            MedicalRecords = p.MedicalRecords.Select(m => new MedicalRecordDto
            {
                Id = m.Id,
                DoctorRemarks = m.DoctorRemarks,
                RecordDate = m.RecordDate
            }).ToList()
        });
    }

    public async Task<PatientDto> GetPatientByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");

        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Surname = patient.Surname,
            Birthdate = patient.Birthdate,
            MedicalRecords = patient.MedicalRecords.Select(m => new MedicalRecordDto
            {
                Id = m.Id,
                DoctorRemarks = m.DoctorRemarks,
                RecordDate = m.RecordDate
            }).ToList()
        };
    }

    public async Task CreatePatientAsync(CreatePatientDto dto)
    {
        var patient = new PatientInfo(dto.Name, dto.Surname, dto.Birthdate);
        patient.MedicalRecords.Add(new MedicalRecord("Initial checkup: Healthy", DateTime.UtcNow));
        await _patientRepository.AddAsync(patient);
    }

    public async Task DeletePatientAsync(Guid id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
            throw new Exception("Patient not found");
        await _patientRepository.DeleteAsync(patient);
    }
    public async Task<PredictionDto> GetAIPredictionAsync(Guid patientId)
    {
        // Verify patient exists
        var patient = await _patientRepository.GetByIdAsync(patientId);
        if (patient == null)
            throw new KeyNotFoundException("Patient not found.");

        // Get JWT token from the current request
        var token = _httpContextAccessor.HttpContext?.User?.FindFirst("token")?.Value
            ?? _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
            throw new UnauthorizedAccessException("JWT token is missing.");

        // Prepare HTTP request with JWT token
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7000/api/prediction")
        {
            Content = JsonContent.Create(new { PatientId = patientId, MedicalData = "sample-data" })
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Call PredictionService
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var prediction = await response.Content.ReadFromJsonAsync<PredictionDto>();
        return prediction ?? throw new InvalidOperationException("Failed to retrieve prediction.");
    }
}
