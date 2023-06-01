using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PqrController : ControllerBase
    {
        private DataContextDB _dbContext;

        public PqrController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Pqr/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.PQR.Select(x => new
                {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CaseTypeId = x.CaseTypeId,
                    UserTypeId = x.UserTypeId,
                    RazonSocial = x.RazonSocial,
                    Nit = x.Nit,
                    Cedula = x.Cedula,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    File = x.File,
                    Comentario = x.Comentario,
                    AutorizaTratamientoDatos = x.AutorizaTratamientoDatos,
                    CaseNumber = x.CaseNumber,
                    CaseStatus = x.CaseStatus,
                    Date = x.Date,
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
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

        // GET: api/Pqr/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = await _dbContext.PQR.Where(q => q.Id == Id).Select(x => new {
                    Id = x.Id,
                    CountryId = x.CountryId,
                    CaseTypeId = x.CaseTypeId,
                    UserTypeId = x.UserTypeId,
                    RazonSocial = x.RazonSocial,
                    Nit = x.Nit,
                    Cedula = x.Cedula,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    File = x.File,
                    Comentario = x.Comentario,
                    AutorizaTratamientoDatos = x.AutorizaTratamientoDatos,
                    CaseNumber = x.CaseNumber,
                    CaseStatus = x.CaseStatus,
                    Date = x.Date,
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
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }
        }

        // POST: api/Pqr/CreatePQR
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePQR([FromBody] PqrViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: GENERAR CONSECUTIVO PARA ASIGNAROLO

            PqrEntity pqr = new PqrEntity
            {
                CountryId = modelo.CountryId,
                CaseTypeId = modelo.CaseTypeId,
                UserTypeId = modelo.UserTypeId,
                RazonSocial = modelo.RazonSocial,
                Nit = modelo.Nit,
                Cedula = modelo.Cedula,
                Name = modelo.Name,
                Email = modelo.Email,
                PhoneNumber = modelo.PhoneNumber,
                File = modelo.File,
                Comentario = modelo.Comentario,
                AutorizaTratamientoDatos = modelo.AutorizaTratamientoDatos, 
                CaseNumber = 5,
                CaseStatus = Guid.Parse("7b1bf27e-c376-4723-aebf-d596edf7ee26"),
                Date = modelo.Date

            };

            _dbContext.PQR.Add(pqr);

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

        // PUT: api/Pqr/Update/5
        [HttpPut("[action]/{Id}")]
        public async Task<IActionResult> Update([FromRoute] Guid Id, [FromBody] UpdatePQRViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.PQR.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            query.CountryId = modelo.CountryId;
            query.CaseTypeId = modelo.CaseTypeId;
            query.UserTypeId = modelo.UserTypeId;
            query.RazonSocial = modelo.RazonSocial;
            query.Nit = modelo.Nit;
            query.Cedula = modelo.Cedula;
            query.Name = modelo.Name;
            query.Email = modelo.Email;
            query.PhoneNumber = modelo.PhoneNumber;
            query.File = modelo.File;
            query.Comentario = modelo.Comentario;
            query.AutorizaTratamientoDatos = modelo.AutorizaTratamientoDatos;
            query.CaseNumber = modelo.CaseNumber;
            query.CaseStatus = modelo.CaseStatus;
            query.Date = modelo.Date;

            try
            {
                await _dbContext.SaveChangesAsync();

                var response = new
                {
                    success = true,
                };

                return new OkObjectResult(response);
            } 
            catch(Exception ex) 
            {
                var response = new
                {
                    success = false,
                    error = ex.Message,
                };
                return new BadRequestObjectResult(response);
            }

        }

        // DELETE: api/Pqr/Delete/5
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var query = await _dbContext.PQR.FirstOrDefaultAsync(e => e.Id == Id);

            if (query == null)
            {
                return NotFound();
            }

            _dbContext.PQR.Remove(query);
           
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
