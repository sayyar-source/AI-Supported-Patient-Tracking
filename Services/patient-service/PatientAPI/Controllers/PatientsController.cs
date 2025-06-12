using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.Application.DTOs;
using PatientService.Application.Interfaces;

namespace PatientService.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly ILogger<PatientsController> _logger;

    public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
    {
        _patientService = patientService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        _logger.LogInformation("Fetched all patients. Count: {Count}", patients.Count());
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        _logger.LogInformation("Fetched patient by id: {Id}", id);
        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        await _patientService.CreatePatientAsync(dto);
        _logger.LogInformation("Created patient: {@Patient}", dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeletePatientAsync(id);
        _logger.LogInformation("Deleted patient with id: {Id}", id);
        return NoContent();
    }

    [HttpGet("prediction/{id}")]
    public async Task<IActionResult> GetPrediction(Guid id)
    {
        var prediction = await _patientService.GetAIPredictionAsync(id);
        _logger.LogInformation("Fetched prediction for patient id: {Id}", id);
        return Ok(prediction);
    }
}
