using Business.Business;
using Business.Interfaces;
using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utilities.Custom;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class LoginController : ControllerBase
    {


        private readonly IToken _token;
        private readonly UserBusiness _userService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IToken token, ILogger<LoginController> logger, UserBusiness userService, ApplicationDbContext context, EncriptePassword utilidades)
        {
            _token = token;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [Route("Registrarse")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Registrarse(UserDto objeto)
        {
            try
            {
                var userCreated = await _userService.CreateAsyncUser(objeto);

                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { isSuccess = false, message = ex.Message });
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var token = await _token.CreateToken(login);

                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token });

                //return Ok(token);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para el inicio de sesión");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear el token");
                return StatusCode(500, new { message = ex.Message });
            }
        }


        [HttpGet]
        [Route("ValidarToken")]
        public IActionResult ValidarToken([FromQuery] string token)

        {

            bool respuesta = _token.validarToken(token);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });

        }


    }



}
