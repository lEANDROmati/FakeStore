using Domain.Entities;
using Domain.Infrastructure.Data;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories
{
    public class RoleRepository:GenericRepository<RoleEntity>,IRoleRepository
    {
        public RoleRepository(DataContext context) : base(context)
        {

        }
    }
}
