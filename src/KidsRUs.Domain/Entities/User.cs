using KidsRUs.Domain.Common;

namespace KidsRUs.Domain.Entities;

public class User : BaseEntity
{
    public int RoleId { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpires { get; set; }
    public Role Role { get; set; }
}