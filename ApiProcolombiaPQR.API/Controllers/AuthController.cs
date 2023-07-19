using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY.HttpRequests;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ApiProcolombiaPQR.ENTITY;
using ApiProcolombiaPQR.API.Models;

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

        /*public AuthController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
            
        }*/

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

                if (!VerifyPasswordHash(modelo.password, usuario.Password_hash, usuario.Password_salt))
                {
                    return NotFound();
                }

                //var rolUsuario = await _dbContext.Role.FirstOrDefaultAsync(x => x.Id == usuario.Role);

                if (usuario != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Name.Trim()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", usuario.Id.ToString()),
                    /*new Claim("RoleId", rolUsuario.Id.ToString()),
                    new Claim("Role", rolUsuario.Name)*/
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

        // POST: api/Auth/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateUserViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string email = modelo.Email.ToLower();

            if (await _dbContext.Users.AnyAsync(u => u.Email == email))
            {
                return BadRequest("El email ya existe");
            }

            CreatePasswordHash(modelo.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var pHash = passwordHash;
            var pSalt = passwordSalt;

            UserEntity user = new UserEntity 
            {
                Name = modelo.Name,
                Email = modelo.Email,
                Password_hash = passwordHash,
                Password_salt = passwordSalt,
                //Role = Guid.Parse(modelo.Role) // Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c")
            };

            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        // GET: api/Auth/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.Users
                    .Select(x => new UserEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        //Role = x.Role
                    }).ToListAsync();

                var response = new AuthResponse
                {
                    success = true,
                    data = query
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Auth/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = await _dbContext.Users.Where(x => x.Id == Id)
                    .Select(x => new UserEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        //Role = x.Role
                    }).ToListAsync();

                var response = new AuthResponse
                {
                    success = true,
                    data = query
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error o devolver un mensaje genérico
                return BadRequest(ex.Message);
            }
        }


        // Validators

        // GET: api/Auth/EmailValidator
        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> EmailValidator([FromRoute] string email)
        {
            try
            {
                var query = await _dbContext.Users.Where(x => x.Email == email)
                    .Select(x => new UserEntity
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        //Role = x.Role
                    }).ToListAsync();

                if (query.Count == 0)
                {
                    var response = new AuthResponse
                    {
                        success = false,
                        data = query
                    };

                    return new OkObjectResult(response);
                }
                else
                {
                    var response = new AuthResponse
                    {
                        success = true,
                        data = query
                    };

                    return new OkObjectResult(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Auth/Delete/Id
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _dbContext.Users.CountAsync();

            if (users > 1)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == Id);

                if (user == null)
                {
                    return NotFound();
                }

                try
                {
                    _dbContext.Users.Remove(user);
                    await _dbContext.SaveChangesAsync();

                    return StatusCode(StatusCodes.Status201Created);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Ok( new { 
                        status = "NoPermitido" 
                        });
        }

        public class AuthResponse
        {
            public bool success { get; set; }
            public List<UserEntity> data { get; set; }
        }

    }
}
