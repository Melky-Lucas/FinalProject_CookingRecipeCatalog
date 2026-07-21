namespace Application.DTOs
{
    public class RegisterUserDTO
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public LoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class AuthResponseDTO
    {
        public string Token { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;

        public AuthResponseDTO(string token, string username, string role)
        {
            Token = token;
            Username = username;
            Role = role;
        }
    }
}
