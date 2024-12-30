using Application.Models.Categories;
using Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Intefaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseModel>> GetAll();
        Task<UserResponseModel> GetById(int id);
        Task Add(UserRequestModel entity);
        Task Update(UserRequestModel entity, int id);
        Task Delete(int id);
    }
}
