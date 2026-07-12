using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IUserService : IBaseService<ProfileDTO, RegisterUserDTO, UpdateProfileDTO>
    {
    }
}