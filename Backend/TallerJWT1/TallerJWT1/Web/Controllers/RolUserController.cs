using Business.Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RolUserController : ControllerBase
    {
        private readonly RolUserBusiness _rolUserBusiness;
        private readonly ILogger<RolUserController> _logger;

        public RolUserController(RolUserBusiness rolUserBusiness, ILogger<RolUserController> logger)
        {
            _rolUserBusiness = rolUserBusiness;
            _logger = logger;

        }

        //Obtener todo

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolUserDto>), 200)]
        [ProducesResponseType(500)]
        [Authorize]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rolUsers = await _rolUserBusiness.GetAllJoinAsync();
                return Ok(rolUsers);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        //Obtener por id

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolUserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rolUser = await _rolUserBusiness.GetByIdJoinAsync(id);
                return Ok(rolUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para la RolUser con id: {RolUserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "RolUser no encontrada con id: {RolUserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener RolUser con id: {RolUserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        //Crear
        [HttpPost]
        [ProducesResponseType(typeof(RolUserUpdateDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] RolUserUpdateDto rolUserDto)
        {
            try
            {
                var createdRolUser = await _rolUserBusiness.CreateAsync(rolUserDto);
                return CreatedAtAction(nameof(GetById), new { id = createdRolUser.id }, createdRolUser);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear RolUser");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear RolUser");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //Actualizar
        [HttpPut]
        [ProducesResponseType(typeof(RolUserDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] RolUserUpdateDto dto)
        {
            try
            {
                var updated = await _rolUserBusiness.UpdateAsync(dto);
                return Ok(new { message = "Actualizado correctamente" });
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar RolUser con id: {RolUserId}", dto.id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "RolUser no encontrada para actualizar con id: {RolUserId}", dto.id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar RolUser con id: {RolUserId}", dto.id);
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
                await _rolUserBusiness.DeleteAsync(id);
                return Ok(new { message = "Eliminado correctamente" });

            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar RolUser con id: {RolUserId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "RolUser no encontrada para eliminar con id: {RolUserId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar RolUser con id: {RolUserId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
