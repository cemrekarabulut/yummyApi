namespace YummyApi.entities;

public class RefreshToken
{
    public int RefreshTokenId { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? RevokedAtUtc { get; set; }
    public bool IsRevoked => RevokedAtUtc.HasValue;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
}
