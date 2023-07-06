using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.COMMON.Models;
using ApiProcolombiaPQR.COMMON.Utilities;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private DataContextDB _dbContext;

        public EmployeeController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Employee/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = from EM in _dbContext.Employee
                            select new
                            {
                                Id = EM.Id,
                                Nombre = EM.Name,
                                Email = EM.Email,
                                Division = EM.Division,
                                Cargo = EM.Cargo
                            };

                var queryLinq = await query.ToListAsync();

                var response = new
                {
                    success = true,
                    data = queryLinq
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

        // GET: api/Employee/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = from EM in _dbContext.Employee
                            where EM.Id == Id
                            select new
                            {
                                Id = EM.Id,
                                Nombre = EM.Name,
                                Email = EM.Email,
                                Division = EM.Division,
                                Cargo = EM.Cargo
                            };

                var queryLinq = await query.ToListAsync();

                var response = new
                {
                    success = true,
                    data = queryLinq
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

        // POST: api/Employee/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] EmployeeViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            EmployeeEntity employee = new EmployeeEntity
            {
                Name = modelo.Name,
                Email = modelo.Email,
                Division = modelo.Division,
                Cargo = modelo.Cargo
            };

            _dbContext.Employee.Add(employee);

            try
            {
                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Employee/Update/5
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] EmployeeViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.Employee.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            query.Name = modelo.Name;
            query.Email = modelo.Email;
            query.Division = modelo.Division;
            query.Cargo = modelo.Cargo;

            try
            {
                await _dbContext.SaveChangesAsync();

                var response = new
                {
                    success = true,
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

        // DELETE: api/Employee/Delete/5
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.Employee.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            _dbContext.Employee.Remove(query);

            try
            {
                await _dbContext.SaveChangesAsync();

                var response = new
                {
                    success = true,
                };

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

    }
}
