using Application.Contract;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Core.Interfaces;
using Core.Models;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IObjectMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IObjectMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _unitOfWork.Users.GetByEmailWithRoleAsync(dto.Email)
                ?? throw new UnauthorizedException("Invalid credentials.");

            var userPasswordHash = await _unitOfWork.Passwords.GetByUserIdAsync(user.Id)
                ?? throw new UnauthorizedException("Invalid credentials.");

            var IsValid = _passwordHasher.Verify(dto.Password, userPasswordHash.PasswordHash);

            if (!IsValid)
                throw new UnauthorizedException("Invalid credentials.");

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthResponseDTO(token, user.Username, user.Role.Name);
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterUserDTO dto, bool isAdmin = false)
        {
            var emailExists = await _unitOfWork.Users.HasEmailAsync(dto.Email);

            if (emailExists)
                throw new ConflictException("A user with that email already exists.");

            var defaultRole = (isAdmin
                ? await _unitOfWork.Roles.GetByNameAsync("Admin")
                : await _unitOfWork.Roles.GetByNameAsync("User"))
                ?? throw new NotFoundException(nameof(Role), "User");

            var user = _mapper.Map<RegisterUserDTO, User>(dto);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            user.Role = defaultRole;
            var token = _tokenGenerator.GenerateToken(user);
            return new AuthResponseDTO(token, user.Username, user.Role.Name);
        }
    }
}
