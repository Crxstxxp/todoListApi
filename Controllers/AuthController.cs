using Microsoft.AspNetCore.Mvc;
using todoListApi.DTOs;
using todoListApi.Services;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    //private readonly IConfiguration _configuration;
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.Register(request);

        if (result.Errors != null)
            return BadRequest(new { message = result.Errors });

        return Ok(new { token = result.Token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.Login(request);

        if (result.Errors != null && result.Errors.Any())
            return Unauthorized(new { errors = result.Errors });

        return Ok(new { token = result.Token });
    }
}