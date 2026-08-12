using Application.Base;
using Application.Contract;
using Application.DTOs;
using Application.Interfaces;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;
using Core.Models;

namespace Application.Services
{
    public class RoleService : BaseService<Role, RoleDTO, CreateRoleDTO, UpdateRoleDTO>, IRoleService
    {
        protected override IGenericRepository<Role> Repository => _unitOfWork.Roles;
        public RoleService(IUnitOfWork unitOfWork, IObjectMapper objectMapper, IApplicationValidator validator) 
           : base(unitOfWork, objectMapper, validator)
        {

        }
    }
}
