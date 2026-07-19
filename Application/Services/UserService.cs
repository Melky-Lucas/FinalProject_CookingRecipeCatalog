using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class UserService : BaseService<User, ProfileDTO, RegisterUserDTO, UpdateProfileDTO>, IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        protected override IGenericRepository<User> Repository => _unitOfWork.Users;
        public UserService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IPasswordHasher passwordHasher)
            : base(unitOfWork, objectMapper, serviceProvider)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<ServiceResult<ProfileDTO>> CreateAsync(RegisterUserDTO userDTO)
        {
            var hashedPassword = _passwordHasher.Hash(userDTO.Password);
            var user = _mapper.Map<RegisterUserDTO, User>(userDTO);
            user.Password.PasswordHash = hashedPassword;

            Repository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<ProfileDTO>.Success(_mapper.Map<User, ProfileDTO>(user), "User created successfully", 201);
        }
    }
}
