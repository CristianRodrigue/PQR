using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private DataContextDB _dbContext;

        public FileController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/File/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var file = await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == Id);

                if (file != null)
                {
                    
                    var contentType = GetContentType(file.fileName); 

                   
                    return File(file.data, contentType, file.fileName);
                }
                else
                {
                    
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        private string GetContentType(string fileName)
        {
            
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
