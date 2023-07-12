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
    public class AssignController : ControllerBase
    {
        private DataContextDB _dbContext;

        public AssignController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Assign/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = from AS in _dbContext.Assign
                            join MT in _dbContext.MailTemplate on AS.IdMailTemplate equals MT.Id
                            //join EM in _dbContext.Employee on AS.IdEmployee equals EM.Id
                            select new
                            {
                                Id = AS.Id,
                                Nombre = AS.Name,
                                MailTemplate = MT.Name,
                                //Empleado = EM.Name
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

        // GET: api/Assign/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = from AS in _dbContext.Assign
                            join MT in _dbContext.MailTemplate on AS.IdMailTemplate equals MT.Id
                            //join EM in _dbContext.Employee on AS.IdEmployee equals EM.Id
                            where AS.Id == Id
                            select new
                            {
                                Id = AS.Id,
                                Nombre = AS.Name,
                                MailTemplate = MT.Name,
                                //Empleado = EM.Name
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

        // POST: api/Assign/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] AssignViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AssignEntity assign = new AssignEntity
            {
                Name = modelo.Name,
                IdMailTemplate = modelo.IdMailTemplate,
                //IdEmployee = modelo.IdEmployee
            };

            _dbContext.Assign.Add(assign);

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

        // PUT: api/Assign/Update/5
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] AssignViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.Assign.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            query.Name = modelo.Name;
            query.IdMailTemplate = modelo.IdMailTemplate;
            //query.IdEmployee = modelo.IdEmployee;

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

        // DELETE: api/Assign/Delete/5
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.Assign.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            _dbContext.Assign.Remove(query);

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

        // POST: api/Assign/AsignarCaso
        [HttpPost("[action]")]
        public async Task<IActionResult> AsignarCaso([FromBody] AssignCaseViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var asignacion = await _dbContext.Assign.FirstOrDefaultAsync(a => a.Id == Guid.Parse("FF68BEFF-A89D-416E-B560-959677D48BDE"));
                var employee = await _dbContext.Employee.FirstOrDefaultAsync(a => a.Id == modelo.Id);

                Guid IdPlantilla = asignacion.IdMailTemplate;
                var plantilla = await _dbContext.MailTemplate.FirstOrDefaultAsync(e => e.Id == IdPlantilla && e.Enabled == true);


                if (plantilla != null)
                {
                    string htmlPlantilla = System.Web.HttpUtility.HtmlDecode(plantilla.Html);
                    htmlPlantilla = htmlPlantilla.Replace("{nombre}", employee.Name);


                    EmailViewModel correo = new EmailViewModel
                    {
                        Destination = modelo.Email,
                        Suject = "Asignacion PQR",
                        Message = htmlPlantilla,
                        IsHtml = true
                    };

                    SendEmail SendCorreo = new SendEmail();
                    SendCorreo.SendAsync(correo);

                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
