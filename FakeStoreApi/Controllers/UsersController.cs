using Application.Models.Users;
using Application.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtiene todos los usuarios disponibles.(Solo rol Administrador)
        /// </summary>
        /// <remarks>
        /// Este método permite obtener una lista de todos los usuarios almacenados en el sistema.
        /// Si no se encuentran usuarios, se devuelve un error de estado 404.
        /// </remarks>
        /// <returns>Una lista de usuarios.</returns>
        /// <response code="200">Lista de usuarios obtenida exitosamente.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserResponseModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAll();

                if (users == null || !users.Any())
                {
                    return NotFound("No se encontraron usuarios.");
                }

                return Ok(users);
            }
            catch (Exception)
            {
               
                return StatusCode(500, "Ocurrió un error inesperado al intentar obtener los usuarios.");
            }
        }


        /// <summary>
        /// Obtiene un usuario por su ID.(Solo rol Administrador)
        /// </summary>
        /// <param name="id">El ID del usuario que se desea obtener.</param>
        /// <remarks>
        /// Este método permite obtener un único usuario basado en su ID.
        /// Si no se encuentra el usuario, se devuelve un error de estado 404.
        /// </remarks>
        /// <returns>El usuario correspondiente al ID proporcionado.</returns>
        /// <response code="200">Usuario obtenido exitosamente.</response>
        /// <response code="404">No se encontró un usuario con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpGet("{id}")]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userService.GetById(id);

                if (user == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {id}.");
                }

                return Ok(user);
            }
            catch (Exception)
            {
               
                return StatusCode(500, "Ocurrió un error inesperado al intentar obtener el usuario.");
            }
        }

        /// <summary>
        /// Crea un nuevo usuario. (Solo rol Administrador)
        /// </summary>
        /// <param name="entity">Modelo que contiene los datos del usuario.</param>
        /// <remarks>
        /// Este método permite crear un nuevo usuario en el sistema.
        /// Es necesario que el usuario tenga rol de administrador para realizar esta acción.
        /// </remarks>
        /// <returns>Confirmación de la creación del usuario.</returns>
        /// <response code="200">El usuario fue creado exitosamente.</response>
        /// <response code="400">Los datos del usuario son inválidos.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPost]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(UserRequestModel entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _userService.Add(entity);
                return Ok("El usuario fue creado exitosamente.");
            }
            catch (Exception ex)
            {
                // Log the exception details (not shown here)
                return StatusCode(500, "Ocurrió un error inesperado al intentar crear el usuario.");
            }
        }


        /// <summary>
        /// Actualiza los datos de un usuario existente. (Solo rol Administrador)
        /// </summary>
        /// <param name="id">El ID del usuario que se desea actualizar.</param>
        /// <param name="entity">Modelo que contiene los nuevos datos del usuario.</param>
        /// <remarks>
        /// Este método permite actualizar los datos de un usuario existente en el sistema.
        /// Es necesario que el usuario tenga rol de administrador para realizar esta acción.
        /// </remarks>
        /// <returns>Confirmación de la actualización del usuario.</returns>
        /// <response code="200">El usuario fue actualizado exitosamente.</response>
        /// <response code="400">Los datos proporcionados son inválidos.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="404">No se encontró un usuario con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor.</response>

        [HttpPut("{id}")]
        [Authorize(Roles = "2")] // Rol 2 == Administrador

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public async Task<IActionResult> Update(UserRequestModel entity, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {id}.");
                }

                await _userService.Update(entity, id);
                return Ok("El usuario fue actualizado exitosamente.");
            }
            catch (Exception)
            {
                
                return StatusCode(500, "Ocurrió un error inesperado al intentar actualizar el usuario.");
            }
        }


        /// <summary>
        /// Elimina un usuario existente por su ID. (Solo rol Administrador)
        /// </summary>
        /// <param name="id">ID del usuario que se desea eliminar.</param>
        /// <returns>Confirmación de la eliminación del usuario.</returns>
        /// <response code="200">El usuario fue eliminado exitosamente.</response>
        /// <response code="401">No autorizado para realizar esta acción.</response>
        /// <response code="404">No se encontró un usuario con el ID proporcionado.</response>
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
                // Verificar si el usuario existe
                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                {
                    return NotFound($"No se encontró un usuario con el ID {id}.");
                }

                // Eliminar el usuario
                await _userService.Delete(id);
                return Ok($"El usuario con ID {id} fue eliminado exitosamente.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado al intentar eliminar el usuario.");
            }
        }

    }
}
