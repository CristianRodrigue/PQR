using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiProcolombiaPQR.API.Controllers
{
    /*[Route("api/[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private DataContextDB _dbContext;

        public UploadController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            /*using (MemoryStream ms = new MemoryStream()) {
                file.CopyTo(ms);
                _dbContext.Files.Add(ms.ToArray());
            }
            

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
    }*/
}