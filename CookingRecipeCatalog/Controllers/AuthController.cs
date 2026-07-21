using Application.Contract;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
            => Ok(await _authService.LoginAsync(dto));

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO dto)
            => Ok(await _authService.RegisterAsync(dto));

        [Authorize(Roles = "Admin")]
        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterUserDTO dto)
            => Ok(await _authService.RegisterAsync(dto, isAdmin: true));
    }
}
