using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs.User;
using MyApp.Application.Services;
using MyApp.Domain.Entities;

namespace MyApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); // ideally return a List<UserResponse> instead of full User entity
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user); // same here — map to DTO
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
     
            };

            await _userService.CreateUserAsync(user, request.Password);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user); // better than Ok()
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch");

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            user.Username = request.Username;
            user.Email = request.Email;
            user.UpdatedAt = DateTime.UtcNow;

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
