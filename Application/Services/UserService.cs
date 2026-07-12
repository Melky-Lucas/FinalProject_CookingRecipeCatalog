using Application.Base;
using Application.Contract;
using Application.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class UserService : BaseService<User, ProfileDTO, RegisterUserDTO, UpdateProfileDTO>, IUserService
    {
        protected override IGenericRepository<User> Repository => _unitOfWork.Users;
        public UserService(IUnitOfWork unitOfWork, IObjectMapper objectMapper)
            : base(unitOfWork, objectMapper)
        {

        }
    }
}
