using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Prediction.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PredictionController : ControllerBase
{
    [HttpPost]
    public IActionResult GetPrediction([FromBody] PredictionRequest request)
    {
        return Ok(new { Prediction = "Stable condition, monitor weekly" });
    }
    public record PredictionRequest(Guid PatientId, string MedicalData);
}
