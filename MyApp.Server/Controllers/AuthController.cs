using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            return BadRequest("Email and Password are required.");
        }

        var result = await _authService.AuthenticateAsync(loginRequest.Email, loginRequest.Password, loginRequest.TwoFactorCode, loginRequest.TwoFactorRecoveryCode);

        if (result == null)
        {
            return Unauthorized("Invalid credentials or two-factor authentication failed.");
        }

        return Ok(new { Token = result.Token });
    }
}