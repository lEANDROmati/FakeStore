using Domain.Entities;
using Domain.Infrastructure.Data;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories
{
    public class UserRepository :GenericRepository<UserEntity>,IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public async Task<UserEntity?> GetByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await GetByUserNameAsync(username);

            if (user == null)
            {
                return false; // Usuario no encontrado
            }

            if (!user.Password.Equals(password))
            {
                return false; // Contraseña incorrecta
            }

            return true; // Login exitoso
        }
    }
}
