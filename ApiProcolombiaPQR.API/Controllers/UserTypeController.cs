using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private DataContextDB _dbContext;

        public UserTypeController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Country/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.UserType.Select(x => new UserTypeEntity
                {
                    Id = x.Id,
                    Name = x.Name.Trim(),
                }).ToListAsync();

                var response = new UserTypeResponse
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

        public class UserTypeResponse
        {
            public bool Success { get; set; }
            public List<UserTypeEntity> Data { get; set; }
        }

    }
}
