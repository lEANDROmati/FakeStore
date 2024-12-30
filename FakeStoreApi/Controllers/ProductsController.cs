using Application.Models.Products;
using Application.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtiene todos los productos disponibles.
        /// </summary>
        /// <returns>Una lista de productos.</returns>
        /// <response code="200">Lista de productos obtenida exitosamente.</response>
        /// <response code="404">No se encontraron productos disponibles.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]



        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAll();

                if (products == null || !products.Any())
                {
                    return NotFound("No se encontraron productos.");
                }

                return Ok(products);
            }
            catch
            {

                return StatusCode(500, "Ocurrió un error inesperado. Por favor, intente más tarde.");
            }
        }



        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto que se desea obtener.</param>
        /// <returns>El producto correspondiente al ID proporcionado.</returns>
        /// <response code="200">Producto obtenido exitosamente.</response>
        /// <response code="404">No se encontró un producto con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
               

                var product = await _productService.GetById(id);

                if (product == null)
                {
                    return NotFound($"No se encontró un producto con el ID {id}.");
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor, intente más tarde.");
            }
        }



        /// <summary>
        /// Crea un nuevo producto. (Solo rol Administrador)
        /// </summary>
        /// <param name="product">Modelo que contiene los datos del producto.</param>
        /// <returns>Confirmación de la creación del producto.</returns>
        /// <response code="201">El producto fue creado exitosamente.</response>
        /// <response code="400">Los datos del producto son inválidos.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPost]
        [Authorize(Roles = "2")] // Rol 2 == Administrador


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Add(ProductRequestModel product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _productService.Add(product);


                return Ok("El Producto fue añadido exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar crear el producto.");
            }
        }


        /// <summary>
        /// Edita un producto existente por su ID. (Solo rol Administrador)
        /// </summary>
        /// <param name="id">El ID del producto que se desea editar.</param>
        /// <param name="product">Modelo que contiene los nuevos datos del producto.</param>
        /// <returns>Confirmación de la actualización del producto.</returns>
        /// <response code="200">El producto fue actualizado exitosamente.</response>
        /// <response code="400">Los datos proporcionados son inválidos.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="404">No se encontró un producto con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPut("{id}")]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(ProductRequestModel product, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingProduct = await _productService.GetById(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró un producto con el ID {id}.");
                }

                await _productService.Update(product, id);
                return Ok("El producto fue actualizado exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar actualizar el producto.");
            }
        }

        /// <summary>
        /// Elimina un producto existente por su ID. (Solo rol Administrador)
        /// </summary>
        /// <param name="id">El ID del producto que se desea eliminar.</param>
        /// <returns>Confirmación de la eliminación del producto.</returns>
        /// <response code="200">El producto fue eliminado exitosamente.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="404">No se encontró un producto con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingProduct = await _productService.GetById(id);
                if (existingProduct == null)
                {
                    return NotFound($"No se encontró un producto con el ID {id}.");
                }

                await _productService.Delete(id);
                return Ok("El producto fue eliminado exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar eliminar el producto.");
            }
        }
    }
}
