using ICDify.Application.DTOs.Auth;
using ICDify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ICDify.API.Controllers;

[ApiController]
[Route("api/v0/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service) => _service = service;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
        => Ok(await _service.RegisterAsync(request));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
        => Ok(await _service.LoginAsync(request));
}