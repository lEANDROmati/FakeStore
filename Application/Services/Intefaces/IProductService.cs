using Application.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Intefaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseModel>> GetAll();
        Task<ProductResponseModel> GetById(int id);
        Task Add(ProductRequestModel entity);
        Task Update(ProductRequestModel entity, int id);
        Task Delete(int id);
    }
}
