namespace YummyApi.entities;

public class AppUser
{
    public int AppUserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PasswordSalt { get; set; } = string.Empty;
    public string Role { get; set; } = "Admin";
    public bool IsActive { get; set; } = true;
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
