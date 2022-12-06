using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FarmerApp.Api.DTO;
using FarmerApp.Api.Models;
using FarmerApp.Api.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FarmerApp.Api.Services;

public class UserService
{
    private readonly FarmerDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(FarmerDbContext db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetMyName()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext != null)
        {
            result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        return result;
    }

    public async Task<User> AddUserAsync(UserVM user)
    {
        CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var newUser = await _db.Users.AddAsync(new User
        {
            Username = user.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = Role.Admin,
        });

        await _db.SaveChangesAsync();

        return newUser.Entity;
    }

    public async Task<User?> GetByPasswordAsync(UserVM userVm)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == userVm.Username);

        if (user == null)
        {
            return null;
        }

        if (VerifyPasswordHash(userVm.Password, user.PasswordHash, user.PasswordSalt))
        {
            return user;
        }

        return null;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);

        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }
    
    public async Task<User?> GetByRefreshToken(string? refreshToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        return user;
    }

    public async Task AddNewRefreshTokenAsync(User user, RefreshToken newRefreshToken)
    {
        var refreshUser = user;

        refreshUser.RefreshToken = newRefreshToken.Token;
        refreshUser.TokenCreated = newRefreshToken.Created;
        refreshUser.TokenExpires = newRefreshToken.Expires;

        _db.Users.Update(refreshUser);
       await _db.SaveChangesAsync();
    }
}