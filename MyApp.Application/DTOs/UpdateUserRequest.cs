namespace MyApp.Application.DTOs.User
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}