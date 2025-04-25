using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public interface ITwoFactorService
    {
        // Validates the 2FA code (e.g., OTP from an authenticator app)
        Task<bool> ValidateTwoFactorCodeAsync(User user, string code);

        // Validates the recovery code (used if the user cannot access their 2FA method)
        Task<bool> ValidateTwoFactorRecoveryCodeAsync(User user, string recoveryCode);
    }
}