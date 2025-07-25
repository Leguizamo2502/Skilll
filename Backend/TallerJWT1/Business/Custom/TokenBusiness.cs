//// Business/Custom/TokenBusiness.cs
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Business.Interfaces;
//using Data.Interfaces;
//using Entity.DTOs;
//using Entity.Models;
//using Microsoft.Extensions.Configuration; // Asegúrate que está
//using Microsoft.Extensions.Logging; // Añade si necesitas logging aquí
//using Microsoft.IdentityModel.Tokens;
//using Utilities.Exceptions;

//namespace Business.Custom
//{
//    public class TokenBusiness : IToken
//    {
//        private readonly IConfiguration _config;
//        private readonly IUser _user;
//        private readonly IRolUserRepository _rolUser;
//        private readonly ILogger<TokenBusiness> _logger; // Opcional: Añadir logger

//        public TokenBusiness(IConfiguration config, IUser user, IRolUserRepository rolUser, ILogger<TokenBusiness> logger) // Añadir logger si lo usas
//        {
//            _config = config;
//            _user = user;
//            _rolUser = rolUser;
//            _logger = logger; // Asignar logger
//        }

//        // --- Método existente CreateToken(LoginDto dto) ---
//        public async Task<string> CreateToken(LoginDto dto)
//        {
//            var user = await _user.ValidateUser(dto);
//            if (user == null)
//            {
//                throw new ValidationException("Credenciales inválidas");
//            }
//            return await CreateTokenForUserInternal(user);
//        }

//        // --- NUEVO MÉTODO CreateTokenForUser ---
//        public async Task<string> CreateTokenForUser(User user)
//        {
//            if (user == null)
//            {
//                throw new ArgumentNullException(nameof(user), "El objeto User no puede ser nulo.");
//            }
//            return await CreateTokenForUserInternal(user);
//        }

//        // --- MÉTODO INTERNO para compartir lógica ---
//        private async Task<string> CreateTokenForUserInternal(User user)
//        {
//            var roles = await GetRoles(user.id);

//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
//                new Claim(ClaimTypes.Email, user.email),
//                new Claim(ClaimTypes.Name, user.name ?? string.Empty)
//            };

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            // *** AJUSTE AQUÍ: Usa "Jwt:key" con 'k' minúscula ***
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]!));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                // Leer la expiración como antes
//                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:Expiration"])),
//                SigningCredentials = credentials
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }

//        public async Task<IEnumerable<string>> GetRoles(int idUser)
//        {
//            var roles = await _rolUser.GetRolUserAsync(idUser);
//            return roles;
//        }

//        public bool validarToken(string token)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            // *** AJUSTE AQUÍ: Usa "Jwt:key" con 'k' minúscula ***
//            var key = Encoding.UTF8.GetBytes(_config["Jwt:key"]!);
//            var validationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(key),
//                ValidateIssuer = false,
//                ValidateAudience = false,
//                ValidateLifetime = true,
//                ClockSkew = TimeSpan.Zero
//            };

//            try
//            {
//                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
//                return true;
//            }
//            catch (SecurityTokenExpiredException ex)
//            {
//                _logger?.LogWarning("Token expirado: {Message}", ex.Message); // Usar logger si existe
//                return false;
//            }
//            catch (SecurityTokenInvalidSignatureException ex)
//            {
//                _logger?.LogWarning("Firma de token inválida: {Message}", ex.Message); // Usar logger si existe
//                return false;
//            }
//            catch (Exception ex)
//            {
//                _logger?.LogError(ex, "Error inesperado al validar token."); // Usar logger si existe
//                return false;
//            }
//        }
//    }
//}