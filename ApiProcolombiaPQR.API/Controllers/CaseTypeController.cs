using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaseTypeController : ControllerBase
    {
        private DataContextDB _dbContext;

        public CaseTypeController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Country/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.CaseType.Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name.Trim(),
                }).ToListAsync();

                var response = new
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






    }
}
