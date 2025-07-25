using Business.Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly UserBusiness _userBusiness;
        private readonly ILogger<UserController> _logger;

        public UserController(UserBusiness userBusiness, ILogger<UserController> logger)
        {
            _userBusiness = userBusiness;
            _logger = logger;

        }

        //Obtener todo

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userBusiness.GetAllAsync();
                return Ok(users);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                return StatusCode(500, new {message = ex.Message});
            }
        }


        //Obtener por id

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var user = await _userBusiness.GetByIdAsync(id);
                return Ok(user);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para la User con id: {UserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "User no encontrada con id: {UserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener User con id: {UserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        //Crear
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            try
            {
                var createdUser = await _userBusiness.CreateAsyncUser(userDto);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.id }, createdUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear User");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear User");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //Actualizar
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] UserDto dto)
        {
            try
            {
                var updated = await _userBusiness.UpdateAsyncUser(dto);
                return Ok(new {message = "Actualizado correctamente"});
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar User con id: {UserId}", dto.id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "User no encontrada para actualizar con id: {UserId}", dto.id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar User con id: {UserId}", dto.id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //Eliminar
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userBusiness.DeleteAsync(id);
                return Ok(new { message = "Eliminado correctamente" });

            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar User con id: {UserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "User no encontrada para eliminar con id: {UserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar User con id: {UserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
