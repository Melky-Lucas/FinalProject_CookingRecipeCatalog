using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class UserService : BaseService<User, UserDTO, CreateUserDTO, UpdateUserDTO>, IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        protected override IGenericRepository<User> Repository => _unitOfWork.Users;
        public UserService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IServiceProvider serviceProvider, IPasswordHasher passwordHasher, IApplicationValidator validator)
            : base(unitOfWork, objectMapper, serviceProvider, validator)
        {
            _passwordHasher = passwordHasher;
        }

        public override async Task<ServiceResult<UserDTO>> CreateAsync(CreateUserDTO userDTO)
        {
            await _validator.ValidateAsync(userDTO);

            var hashedPassword = _passwordHasher.Hash(userDTO.Password);

            var user = _mapper.Map<CreateUserDTO, User>(userDTO);

            user.Password.PasswordHash = hashedPassword;

            user.Role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId)
                ?? throw new NotFoundException(nameof(Role), user.RoleId);

            Repository.Add(user);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<UserDTO>.Success(_mapper.Map<User, UserDTO>(user), "User created successfully", 201);
        }


        public override async Task<ServiceResult<UserDTO>> UpdateAsync(int id, UpdateUserDTO userDTO)
        {
            await _validator.ValidateAsync(userDTO);

            if (!await _unitOfWork.Users.Exists(id))
                return ServiceResult<UserDTO>.Failure("Entity not found", 404);

            userDTO.Password = _passwordHasher.Hash(userDTO.Password);

            var user = _mapper.Map<UpdateUserDTO, User>(userDTO);
            user.Id = id;

            user.Role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId)
                ?? throw new NotFoundException(nameof(Role), user.RoleId);

            Repository.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<UserDTO>.Success(_mapper.Map<User, UserDTO>(user));
        }

        public override async Task<ServiceResult> DeleteAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                return ServiceResult.Failure("Entity not found", 404);

            _unitOfWork.Passwords.Delete(user.Password);
            Repository.Delete(user);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(statusCode: 204);
        }
    }
}
