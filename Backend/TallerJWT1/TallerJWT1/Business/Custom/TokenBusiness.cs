using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Data.Interfaces;
using Entity.DTOs;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business.Custom
{
    public class TokenBusiness : IToken
    {
        private IConfiguration _config;
        private IUser _user;
        private IRolUserRepository _rolUser;
        public TokenBusiness(IConfiguration config, IUser user, IRolUserRepository rolUser)
        {
            _config = config;
            _user = user;
            _rolUser = rolUser;
        }
       public async Task<string> CreateToken(LoginDto dto)
        {
            var user = await _user.ValidateUser(dto);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas");
            }
            var roles = await GetRoles(user.id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
            };

            // Add each role as a separate Claim
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:Expiration"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }

        public async Task<IEnumerable<string>> GetRoles(int idUser)
        {
            var roles = await _rolUser.GetRolUserAsync(idUser);
            return roles;
        }


        public bool validarToken(string token)
        {
            var ClaimsPrincipal = new ClaimsPrincipal();
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(_config["Jwt:key"]!))
            };

            try
            {
                ClaimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                //Manejar token Expirado
                return false;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                // Manejar firma Invalida
                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

    }
}
