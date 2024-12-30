using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Categories
{
    public class CategoryRequestModel
    {
        [Required(ErrorMessage = "El nombre de la categoría es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre de la categoría no puede tener más de 100 caracteres.")]
        public string? Name { get; set; }

        
        public string? Description { get; set; }
    }

}
