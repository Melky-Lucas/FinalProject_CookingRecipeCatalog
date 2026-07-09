using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CreateRoleDTO
    {
        public string Name { get; set; } = null!;
    }

    public class UpdateRoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
