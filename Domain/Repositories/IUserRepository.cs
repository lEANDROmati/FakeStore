using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository:IGenericRepository<UserEntity>
    {
        Task<UserEntity> GetByUserNameAsync(string username);
        Task<bool> LoginAsync(string username, string password);
    }
}
