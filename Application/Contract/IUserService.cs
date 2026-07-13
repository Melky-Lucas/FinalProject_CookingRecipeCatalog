using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IUserService : IBaseService<ProfileDTO, RegisterUserDTO, UpdateProfileDTO>
    {
    }
}