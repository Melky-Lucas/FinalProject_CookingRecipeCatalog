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
        private readonly IApplicationValidator _validator;

        public AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator, IObjectMapper mapper, IApplicationValidator validator)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _unitOfWork.Users.GetByEmailWithRoleAsync(dto.Email)
                ?? throw new UnauthorizedException("Invalid credentials.");

            var userPasswordHash = await _unitOfWork.Passwords.GetByIdAsync(user.PasswordId)
                ?? throw new UnauthorizedException("Invalid credentials.");

            var IsValid = _passwordHasher.Verify(dto.Password, userPasswordHash.PasswordHash);

            if (!IsValid)
                throw new UnauthorizedException("Invalid credentials.");

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthResponseDTO(token, user.Username, user.Role.Name);
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterUserDTO dto, bool isAdmin = false)
        {
            await _validator.ValidateAsync(dto);

            var emailExists = await _unitOfWork.Users.HasEmailAsync(dto.Email);

            if (emailExists)
                throw new ConflictException("A user with that email already exists.");

            string roleName = isAdmin ? "Admin" : "User";

            var role = await _unitOfWork.Roles.GetByNameAsync(roleName)
                ?? throw new NotFoundException(nameof(Role), roleName);

            var user = _mapper.Map<RegisterUserDTO, User>(dto);

            user.Role = role;
            user.Password.PasswordHash = _passwordHasher.Hash(dto.Password);

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _tokenGenerator.GenerateToken(user);
            return new AuthResponseDTO(token, user.Username, user.Role.Name);
        }
    }
}
