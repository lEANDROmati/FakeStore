using Application.Models.Categories;
using Application.Services.Intefaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
       private readonly ICategoryService _categoryService;
       
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtiene todas las categorías disponibles.
        /// </summary>
        /// <returns>Una lista de categorías.</returns>
        /// <response code="200">Lista de categorías obtenida exitosamente.</response>
        /// <response code="404">No se encontraron categorías disponibles.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAll();

                if (categories == null || !categories.Any())
                {
                    return NotFound("No se encontraron categorías.");
                }

                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor, intente más tarde.");
            }
        }


        /// <summary>
        /// Obtiene una categoría por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría que se desea obtener.</param>
        /// <returns>La categoría correspondiente al ID proporcionado.</returns>
        /// <response code="200">La categoría fue obtenida exitosamente.</response>
        /// <response code="404">No se encontró una categoría con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {


                var category = await _categoryService.GetById(id);

                if (category == null)
                {
                    return NotFound($"No se encontró una categoría con el ID {id}.");
                }

                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado. Por favor, intente más tarde.");
            }
        }


        /// <summary>
        /// Crea una nueva categoría. (Solo rol Administrador)
        /// </summary>
        /// <param name="entity">Modelo que contiene los datos de la categoría.</param>
        /// <returns>Confirmación de la creación de la categoría.</returns>
        /// <response code="400">Los datos proporcionados son inválidos.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPost]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CategoryRequestModel entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _categoryService.Add(entity);

                return Ok("La categoria fue añadida exitosamente");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar crear la categoría.");
            }
        }


        /// <summary>
        /// Edita una categoría existente por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría que se desea editar.</param>
        /// <param name="entity">Modelo que contiene los nuevos datos de la categoría.</param>
        /// <returns>Confirmación de la actualización de la categoría.</returns>
        /// <response code="200">La categoría fue actualizada exitosamente.</response>
        /// <response code="400">Los datos proporcionados son inválidos.</response>
        /// <response code="404">No se encontró una categoría con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPut("{id}")]
        [Authorize(Roles = "2")] // Solo Administradores

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, CategoryRequestModel entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingCategory = await _categoryService.GetById(id);
                if (existingCategory == null)
                {
                    return NotFound($"No se encontró una categoría con el ID {id}.");
                }

                await _categoryService.Update(entity, id);
                return Ok("La categoría fue actualizada exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar actualizar la categoría.");
            }
        }


        /// <summary>
        /// Elimina una categoría existente por su ID. (Solo rol Administrador)
        /// </summary>
        /// <param name="id">El ID de la categoría que se desea eliminar.</param>
        /// <returns>Confirmación de la eliminación de la categoría.</returns>
        /// <response code="200">La categoría fue eliminada exitosamente.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="404">No se encontró una categoría con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")] // Solo Administradores

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingCategory = await _categoryService.GetById(id);
                if (existingCategory == null)
                {
                    return NotFound($"No se encontró una categoría con el ID {id}.");
                }

                await _categoryService.Delete(id);
                return Ok("La categoría fue eliminada exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar eliminar la categoría.");
            }
        }

    }
}
