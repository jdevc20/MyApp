using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public class TwoFactorService : ITwoFactorService
    {
        // This is a mock function to simulate validating a 2FA code
        public async Task<bool> ValidateTwoFactorCodeAsync(User user, string code)
        {
            // In a real-world application, you would verify the code against a stored value or use a third-party service like Google Authenticator or Authy.

            // Example logic: Assume we have a stored 2FA code for the user (you should replace this with your actual logic)
            if (string.IsNullOrEmpty(user.TwoFactorCode) || user.TwoFactorCode != code)
            {
                return await Task.FromResult(false); // Invalid code
            }

            return await Task.FromResult(true); // Valid code
        }

        // This is a mock function to simulate validating a recovery code
        public async Task<bool> ValidateTwoFactorRecoveryCodeAsync(User user, string recoveryCode)
        {
            // In a real-world application, you'd store recovery codes in a secure way and check them.
            // Here we're just doing a simple comparison for demonstration purposes.

            if (string.IsNullOrEmpty(user.TwoFactorRecoveryCode) || user.TwoFactorRecoveryCode != recoveryCode)
            {
                return await Task.FromResult(false); // Invalid recovery code
            }

            return await Task.FromResult(true); // Valid recovery code
        }
    }
}