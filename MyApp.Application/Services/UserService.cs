﻿using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task CreateUserAsync(User user, string userPassword) => await _userRepository.AddAsync(user, userPassword);

        public async Task UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);

        public async Task DeleteUserAsync(int id) => await _userRepository.DeleteAsync(id);
    }
}