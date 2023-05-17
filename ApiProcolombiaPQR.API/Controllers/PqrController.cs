using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        // POST: api/Pqr/CreatePQR
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePQR([FromBody] PqrViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
                AutorizaTratamientoDatos = modelo.AutorizaTratamientoDatos
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
