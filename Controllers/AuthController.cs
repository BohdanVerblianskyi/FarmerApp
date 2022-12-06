using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using FarmerApp.Api.Models;
using FarmerApp.Api.Services;
using FarmerApp.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FarmerApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;

    public AuthController(IConfiguration configuration, UserService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    [HttpGet, Authorize]
    public ActionResult<string> GetMe()
    {
        var userName = _userService.GetMyName();
        return Ok(userName);
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserVM userVM)
    {
        var user = await _userService.AddUserAsync(userVM);
             
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserVM userVm)
    {
        var user = await _userService.GetByPasswordAsync(userVm);

        if (user == null)
        {
            return BadRequest("User not found.");
        }
            
        var token = CreateToken(user);
        var refreshToken = GenerateRefreshToken();
        
        SetRefreshToken(refreshToken);
        await _userService.AddNewRefreshTokenAsync(user, refreshToken);
        
        return Ok(token);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<string>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        var user = await _userService.GetByRefreshToken(refreshToken);
        
        if (user == null)
        {
            return Unauthorized("Invalid Refresh Token.");
        }
            
        if(user.TokenExpires < DateTime.Now)
        {
            return Unauthorized("Token expired.");
        }

        var token = CreateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        
        SetRefreshToken(newRefreshToken);
        await _userService.AddNewRefreshTokenAsync(user,newRefreshToken);

       return Ok(token); 
    }

    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7),
            Created = DateTime.Now
        };

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken)
    {
        var cookieOptions = new CookieOptions
        {    
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, user.Username),
            new (ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}