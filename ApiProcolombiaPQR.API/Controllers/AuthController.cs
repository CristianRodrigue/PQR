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
                Role = Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c")
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

        // ==========================================================================================================
        // GET: api/Pqr
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        /*
         // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            var categories = from c in _dbContext.Categories
                             select new
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 ImageUrl = c.ImageUrl
                             };


            return Ok(categories);
        }

        // GET: api/Categories/5
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var category = (from c in _dbContext.Categories
                            where c.Id == id
                            select new
                            {
                                Id = c.Id,
                                Name = c.Name,
                                ImageUrl = c.ImageUrl
                            }).FirstOrDefault();


            return Ok(category);

        }

        // POST: api/Categories
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryEntity category)
        {
            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                category.ImageUrl = file;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // PUT: api/Categories/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryEntity category)
        {
            var entity = _dbContext.Categories.Find(id);
            if (entity == null)
            {
                return NotFound("No category found against this id...");
            }

            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                entity.Name = category.Name;
                entity.ImageUrl = file;
                _dbContext.SaveChanges();
                return Ok("Category Updated Successfully...");
            }
        }

        // DELETE: api/ApiWithActions/5
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("No category found against this id...");
            }
            else
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return Ok("Category deleted...");
            }
        }
         */


    }
}
