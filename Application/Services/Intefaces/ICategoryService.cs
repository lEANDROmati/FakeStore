using Application.Models.Categories;
using Application.Models.Products;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Intefaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseModel>> GetAll();
        Task<CategoryResponseModel> GetById(int id);
        Task Add(CategoryRequestModel entity);
        Task Update(CategoryRequestModel entity, int id);
        Task Delete(int id);
    }
}
