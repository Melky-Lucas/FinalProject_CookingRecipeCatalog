using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IRoleService : IBaseService<RoleDTO, CreateRoleDTO, UpdateRoleDTO>
    {
    }
}