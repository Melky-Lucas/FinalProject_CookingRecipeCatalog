using Application.DTOs;

namespace Application.Contract
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
        Task<AuthResponseDTO> RegisterAsync(RegisterUserDTO dto, bool isAdmin = false);
    }
}
