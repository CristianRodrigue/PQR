using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private DataContextDB _dbContext;

        public RoleController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Country/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.Role.Select(x => new RoleEntity
                {
                    Id = x.Id,
                    Name = x.Name.Trim(),
                }).ToListAsync();

                var response = new RoleResponse
                {
                    Success = true,
                    Data = query
                };

                return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class RoleResponse
        {
            public bool Success { get; set; }
            public List<RoleEntity> Data { get; set; }
        }


    }
}
