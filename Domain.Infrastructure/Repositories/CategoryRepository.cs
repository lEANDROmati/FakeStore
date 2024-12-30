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
    public class CategoryRepository:GenericRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {

        }
    }
}
