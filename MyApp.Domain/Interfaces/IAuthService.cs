namespace MyApp.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResult> AuthenticateAsync(string email, string password, string twoFactorCode, string twoFactorRecoveryCode);
    }

    public class AuthResult
    {
        public string Token { get; set; }
    }
}