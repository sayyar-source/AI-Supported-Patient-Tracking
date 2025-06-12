using Application.DTOs;
using Application.Interfaces;
using Auth.Application.Common;
using Auth.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IAuthLogService _logService;
    public AuthController(IAuthService authService, IAuthLogService logService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        _logService = logService ?? throw new ArgumentNullException(nameof(logService));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(request);

        // Log to Elasticsearch
        await _logService.LogAsync(new AuthLog
        {
            Email = request.Email,
            Action = "Login",
            Success = result.IsSuccess,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty
        });
        if (!result.IsSuccess)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(request.Email, request.Password);

        // Log to Elasticsearch
        await _logService.LogAsync(new AuthLog
        {
            Email = request.Email,
            Action = "Register",
            Success = result.IsSuccess,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty
        });
        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(new { Message = result.Data });
    }

    [HttpPost("signout")]
    public new async Task<IActionResult> SignOut()
    {
        _ = _logService.LogAsync(new AuthLog
        {
            Email = User?.Identity?.Name ?? string.Empty,
            Action = "SignOut",
            Success = true,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
            Timestamp = DateTime.UtcNow
        });

        await _authService.SignOutAsync();
        return Ok();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ForgotPasswordAsync(request.Email);

        // Log to Elasticsearch
        await _logService.LogAsync(new AuthLog
        {
            Email = request.Email,
            Action = "ForgotPassword",
            Success = result.IsSuccess,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
            Timestamp = DateTime.UtcNow
        });

        if (!result.IsSuccess)
            return BadRequest(new { Error = result.Error });

        return Ok(new { Message = result.Data });
    }
}
