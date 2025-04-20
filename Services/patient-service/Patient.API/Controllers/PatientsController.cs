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

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto dto)
    {
        await _patientService.CreatePatientAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeletePatientAsync(id);
        return NoContent();
    }
    [HttpGet("{id}/prediction")]
    public async Task<IActionResult> GetPrediction(Guid id)
    {
        var prediction = await _patientService.GetAIPredictionAsync(id);
        return Ok(prediction);
    }
}
