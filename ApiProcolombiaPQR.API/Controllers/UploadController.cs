using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private DataContextDB _dbContext;

        public UploadController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(FilesEntity file)
        {
            if (file.File != null && file.File.Length > 0)
            {
                try
                {

                    // Guardar los cambios en la base de datos
                    await _dbContext.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("No se ha seleccionado un archivo para subir.");
            }
        }
    }
}