using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Users
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        
        public List<RoleModel> Role { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string Rol { get; set; }

    }
}
