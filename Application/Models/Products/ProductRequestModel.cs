using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Products
{
    public class ProductRequestModel
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio.")]
        public int Price { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "El stock es obligatorio.")]
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        
    }
}
