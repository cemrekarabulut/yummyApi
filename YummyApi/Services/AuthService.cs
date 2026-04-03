using Microsoft.EntityFrameworkCore;
using YummyApi.Context;
using YummyApi.Dtos.Auth;
using YummyApi.entities;

namespace YummyApi.Services;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<LoginResponseDto?> RefreshAsync(string refreshToken, CancellationToken cancellationToken);
}

public sealed class AuthService(
    ApiContext context,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService,
    IConfiguration configuration) : IAuthService
{
    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        await SeedAdminUserIfMissingAsync(cancellationToken);

        var user = await context.AppUsers
            .Include(x => x.RefreshTokens)
            .FirstOrDefaultAsync(x => x.Username == request.Username && x.IsActive, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var isValid = passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
        if (!isValid)
        {
            return null;
        }

        return await CreateTokensAsync(user, cancellationToken);
    }

    public async Task<LoginResponseDto?> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var tokenEntity = await context.RefreshTokens
            .Include(x => x.AppUser)
            .FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);

        if (tokenEntity is null || tokenEntity.IsRevoked || tokenEntity.IsExpired || !tokenEntity.AppUser.IsActive)
        {
            return null;
        }

        tokenEntity.RevokedAtUtc = DateTime.UtcNow;
        return await CreateTokensAsync(tokenEntity.AppUser, cancellationToken);
    }

    private async Task<LoginResponseDto> CreateTokensAsync(AppUser user, CancellationToken cancellationToken)
    {
        var accessToken = jwtTokenService.GenerateToken(user.Username, user.Role);
        var accessTokenMinutes = int.TryParse(configuration["Jwt:ExpireMinutes"], out var minutes) ? minutes : 60;
        var refreshDays = int.TryParse(configuration["Jwt:RefreshExpireDays"], out var days) ? days : 7;
        var refreshTokenValue = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
        var refreshExpiresAt = DateTime.UtcNow.AddDays(refreshDays);

        var refreshToken = new RefreshToken
        {
            AppUserId = user.AppUserId,
            Token = refreshTokenValue,
            ExpiresAtUtc = refreshExpiresAt
        };

        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync(cancellationToken);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(accessTokenMinutes),
            RefreshToken = refreshTokenValue,
            RefreshTokenExpiresAtUtc = refreshExpiresAt
        };
    }

    private async Task SeedAdminUserIfMissingAsync(CancellationToken cancellationToken)
    {
        var hasUsers = await context.AppUsers.AnyAsync(cancellationToken);
        if (hasUsers)
        {
            return;
        }

        var username = configuration["Auth:AdminUsername"] ?? "admin";
        var password = configuration["Auth:AdminPassword"] ?? "ChangeThisStrongPassword!";
        var role = configuration["Auth:AdminRole"] ?? "Admin";

        var (hash, salt) = passwordHasher.HashPassword(password);
        var adminUser = new AppUser
        {
            Username = username,
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = role,
            IsActive = true
        };

        context.AppUsers.Add(adminUser);
        await context.SaveChangesAsync(cancellationToken);
    }
}
