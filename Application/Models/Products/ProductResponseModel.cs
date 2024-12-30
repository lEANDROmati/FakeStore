using Application.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Products
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }

        public List<CategoryResponseModel> CategoryId { get; set; } 
    }

   
}
