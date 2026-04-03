using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YummyApi.Dtos.Auth;
using YummyApi.Services;

namespace YummyApi.Controller;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.LoginAsync(request, cancellationToken);
        if (tokenResponse is null)
        {
            return Unauthorized(new { message = "Kullanici adi veya sifre hatali." });
        }

        return Ok(tokenResponse);
    }

    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.RefreshAsync(request.RefreshToken, cancellationToken);
        if (tokenResponse is null)
        {
            return Unauthorized(new { message = "Refresh token gecersiz veya suresi dolmus." });
        }

        return Ok(tokenResponse);
    }
}
