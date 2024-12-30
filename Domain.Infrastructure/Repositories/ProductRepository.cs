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
    public class ProductRepository : GenericRepository<ProductEntity>,IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) 
        {

        }
    }
}
