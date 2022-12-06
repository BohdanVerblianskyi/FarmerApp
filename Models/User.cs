using System.ComponentModel.DataAnnotations;

namespace FarmerApp.Api.Models;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(150)] public string Username { get; set; } = string.Empty;
    [Required] public byte[] PasswordHash { get; set; }
    [Required] public byte[] PasswordSalt { get; set; }
    [MaxLength(512)] public string RefreshToken { get; set; } = string.Empty;
    public DateTime TokenCreated { get; set; }
    public DateTime TokenExpires { get; set; }

    public Role Role { get; set; }
}

public enum Role
{
    Admin,
    User
}