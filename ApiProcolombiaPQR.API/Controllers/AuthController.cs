using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY.HttpRequests;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DataContextDB _dbContext;
        private readonly IConfiguration _config;

        public AuthController(DataContextDB dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
        }

        // POST: api/Auth/LoginPQR
        [HttpPost("[action]")]
        public async Task<IActionResult> loginPQR(AuthRequests modelo)
        {
            var email = modelo.email.ToLower();

            try
            {
                // Validar si el usuario existe
                var usuario = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
                
                if (usuario == null)
                {
                    return BadRequest("El usuario no existe");
                }

                var rolUsuario = await _dbContext.Role.FirstOrDefaultAsync(x => x.Id == usuario.Role);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Name.Trim()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", usuario.Id.ToString()),
                    new Claim("RoleId", rolUsuario.Id.ToString()),
                    new Claim("Role", rolUsuario.Name)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Creacion token
                    var token = new JwtSecurityToken(
                        issuer: _config["Issuer"],
                        audience: _config["Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    //Serialización del Token para ser devuelto
                    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(
                    new
                        {
                            status = "Ok",
                            IdUsuario = usuario.Id.ToString(),
                            token = encodedJwt
                        }
                    );
                }

                return BadRequest("Error al validar el usuario");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
