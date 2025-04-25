using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int id);

        Task CreateUserAsync(User user, string userPassword);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
    }
}