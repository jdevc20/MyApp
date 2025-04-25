using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Configuration;
using MyApp.Application.Services;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITwoFactorService _twoFactorService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        ITwoFactorService twoFactorService,
        IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = userRepository;
        _twoFactorService = twoFactorService;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResult> AuthenticateAsync(string email, string password, string twoFactorCode, string twoFactorRecoveryCode)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null || !VerifyPassword(user, password))
            return null;

        if (user.IsTwoFactorEnabled)
        {
            bool isTwoFactorValid = false;

            if (!string.IsNullOrEmpty(twoFactorCode))
                isTwoFactorValid = await _twoFactorService.ValidateTwoFactorCodeAsync(user, twoFactorCode);

            if (!isTwoFactorValid && !string.IsNullOrEmpty(twoFactorRecoveryCode))
                isTwoFactorValid = await _twoFactorService.ValidateTwoFactorRecoveryCodeAsync(user, twoFactorRecoveryCode);

            if (!isTwoFactorValid)
                return null;
        }

        var token = GenerateJwtToken(user);

        return new AuthResult { Token = token };
    }

    private bool VerifyPassword(User user, string password)
    {
        return PasswordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("IsTwoFactorEnabled", user.IsTwoFactorEnabled.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}