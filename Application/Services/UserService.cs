using Application.Models.Categories;
using Application.Models.Users;
using Application.Services.Intefaces;
using Domain.Entities;
using Domain.Infrastructure.Repositories;
using Domain.Repositories;
using IdentityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        
            
        public async Task<IEnumerable<UserResponseModel>> GetAll()
        {
            var User = await _userRepository.GetAllAsync();           
            var role = await _roleRepository.GetAllAsync();

            var response = User.Select(user => new UserResponseModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                Role = (from c in role where c.Id == user.RoleId
                          select new RoleModel
                          {  
                             Id = c.Id,
                             Rol = c.Name
                          }).ToList()
                



            });
            return response;
        }

        public async Task<UserResponseModel> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return (null);
            }

            var Role = await _roleRepository.GetAllAsync();
            var response = new UserResponseModel
            {
                Id = id,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                Role = (from c in Role
                        where c.Id == user.RoleId
                        select new RoleModel
                        {
                            Id = c.Id,
                            Rol = c.Name
                        }).ToList()

            };

            return response;
        }

        public async Task Add(UserRequestModel entity)
        {
            var User = new UserEntity
            {
                UserName = entity.UserName,
                FullName = entity.FullName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                RoleId = entity.RoleId,
                
            };
            await _userRepository.AddAsync(User);
            await _userRepository.SaveChangesAsync();
        }
        public async Task Update(UserRequestModel entity,int id)
        {
            var User = new UserEntity()
            {
                Id= id,
                UserName = entity.UserName,
                FullName = entity.FullName,
                Email = entity.Email,
                Password = entity.Password,
                Address = entity.Address,
                RoleId = entity.RoleId,

            };
            await _userRepository.UpdateAsync(User);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           await _userRepository.DeleteAsync(id);
           await _userRepository.SaveChangesAsync();
        }


    }
}
