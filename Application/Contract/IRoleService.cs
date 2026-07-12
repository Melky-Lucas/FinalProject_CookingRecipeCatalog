using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IRoleService : IBaseService<RoleDTO, CreateRoleDTO, UpdateRoleDTO>
    {
    }
}