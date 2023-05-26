using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
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
                    CaseStatus = x.CaseStatus
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


        /*
        
            try
            {
                var query = await _context.AppUsers.Where(p => p.IdEmpresa == IdEmpresa).Select(x => new {
                    Id = x.Id,
                    Nombre = x.Nombre.Trim(),
                    Apellido = x.Apellido.Trim(),
                    Identificacion = x.Identificacion.Trim(),
                    Email = x.Email.Trim(),
                    Usuario = x.Usuario.Trim(),
                    x.Activo
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
                ErrorViewModel dataException = new ErrorViewModel();

                dataException.IdEmpresa = IdEmpresa;
                dataException.ErrorMessage = ex.Message;
                dataException.ErrorCode = ex.HResult.ToString();
                dataException.InnerError = ex.InnerException.Message;
                dataException.ErrorDate = DateTime.Now;

                ErrorHandling SaveError = new ErrorHandling();
                SaveError.LoggerErrorAsync(dataException);

                var response = new
                {
                    success = false,
                    error = ex.Message,
                    errorCode = ex.HResult,
                    InnerError = ex.InnerException
                };
                return new BadRequestObjectResult(response);
            }

            

        
    }

    // GET: api/AppUser/GetById
    [HttpGet("[action]/{IdEmpresa}/{Id}")]
    public async Task<IActionResult> GetById([FromRoute] int IdEmpresa, Guid Id)
    {
        try
        {
            var query = await _context.AppUsers.Where(p => p.IdEmpresa == IdEmpresa && p.Id == Id).Select(x => new {
                Id = x.Id,
                Nombre = x.Nombre.Trim(),
                Apellido = x.Apellido.Trim(),
                Identificacion = x.Identificacion.Trim(),
                Email = x.Email.Trim(),
                Usuario = x.Usuario.Trim(),
                Password = x.Password.Trim(),
                Activo = x.Activo
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
            ErrorViewModel dataException = new ErrorViewModel();

            dataException.IdEmpresa = IdEmpresa;
            dataException.ErrorMessage = ex.Message;
            dataException.ErrorCode = ex.HResult.ToString();
            dataException.InnerError = ex.InnerException.Message;
            dataException.ErrorDate = DateTime.Now;

            ErrorHandling SaveError = new ErrorHandling();
            SaveError.LoggerErrorAsync(dataException);

            var response = new
            {
                success = false,
                error = ex.Message,
                errorCode = ex.HResult,
                InnerError = ex.InnerException
            };
            return new BadRequestObjectResult(response);
        }
    }


         */

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
                CaseStatus = Guid.Parse("7b1bf27e-c376-4723-aebf-d596edf7ee26")

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

    }
}
