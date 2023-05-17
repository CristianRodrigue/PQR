using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DataContextDB _dbContext;

        public AuthController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Pqr
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        /*
         // GET: api/Categories
        [HttpGet]
        public IActionResult Get()
        {
            var categories = from c in _dbContext.Categories
                             select new
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 ImageUrl = c.ImageUrl
                             };


            return Ok(categories);
        }

        // GET: api/Categories/5
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var category = (from c in _dbContext.Categories
                            where c.Id == id
                            select new
                            {
                                Id = c.Id,
                                Name = c.Name,
                                ImageUrl = c.ImageUrl
                            }).FirstOrDefault();


            return Ok(category);

        }

        // POST: api/Categories
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromBody] CategoryEntity category)
        {
            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                category.ImageUrl = file;
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        // PUT: api/Categories/5
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryEntity category)
        {
            var entity = _dbContext.Categories.Find(id);
            if (entity == null)
            {
                return NotFound("No category found against this id...");
            }

            var stream = new MemoryStream(category.ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                entity.Name = category.Name;
                entity.ImageUrl = file;
                _dbContext.SaveChanges();
                return Ok("Category Updated Successfully...");
            }
        }

        // DELETE: api/ApiWithActions/5
        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound("No category found against this id...");
            }
            else
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return Ok("Category deleted...");
            }
        }
         */


    }
}
