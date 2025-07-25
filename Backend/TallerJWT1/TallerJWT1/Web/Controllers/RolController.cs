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
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly RolBusiness _rolBusiness;
        private readonly ILogger<RolController> _logger;

        public RolController(RolBusiness rolBusiness, ILogger<RolController> logger)
        {
            _rolBusiness = rolBusiness;
            _logger = logger;

        }

        //Obtener todo

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RolDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rols = await _rolBusiness.GetAllAsync();
                return Ok(rols);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener todos los usuarios");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        //Obtener por id

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RolDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rol = await _rolBusiness.GetByIdAsync(id);
                return Ok(rol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para la Rol con id: {RolId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Rol no encontrada con id: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener Rol con id: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        //Crear
        [HttpPost]
        [ProducesResponseType(typeof(RolDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] RolDto rolDto)
        {
            try
            {
                var createdRol = await _rolBusiness.CreateAsync(rolDto);
                return CreatedAtAction(nameof(GetById), new { id = createdRol.id }, createdRol);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear Rol");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear Rol");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //Actualizar
        [HttpPut]
        [ProducesResponseType(typeof(RolDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] RolDto dto)
        {
            try
            {
                var updated = await _rolBusiness.UpdateAsync(dto);
                return Ok(new { message = "Actualizado correctamente" });
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar Rol con id: {RolId}", dto.id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Rol no encontrada para actualizar con id: {RolId}", dto.id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar Rol con id: {RolId}", dto.id);
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
                await _rolBusiness.DeleteAsync(id);
                return Ok(new { message = "Eliminado correctamente" });

            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al eliminar Rol con id: {RolId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Rol no encontrada para eliminar con id: {RolId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar Rol con id: {RolId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
