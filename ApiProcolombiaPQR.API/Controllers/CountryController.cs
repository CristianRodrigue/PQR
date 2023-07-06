using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.COMMON.Models;
using ApiProcolombiaPQR.COMMON.Utilities;
using Maddalena;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private DataContextDB _dbContext;

        public CountryController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Country/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                 var query = await _dbContext.Country.Select(x => new CountryEntity
                {
                    Id = x.Id,
                    CountryName = x.CountryName.Trim(),
                }).ToListAsync();
                

                var response = new CountryeResponse
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
        public class CountryeResponse
        {
            public bool Success { get; set; }
            public List<CountryEntity> Data { get; set; }
        }
    }
}
