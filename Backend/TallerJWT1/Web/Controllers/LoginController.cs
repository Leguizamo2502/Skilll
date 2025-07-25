using Entity.LoginEstatico;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] StaticUserDto loginUser)
        {
            var user = ListUser.Users.FirstOrDefault(u =>
                u.Username == loginUser.Username && u.Password == loginUser.Password);

            if (user == null)
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });

            return Ok(new { username = user.Username, rol = user.Rol });
        }

    }
}
