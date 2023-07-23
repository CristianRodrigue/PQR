using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.COMMON.Models;
using ApiProcolombiaPQR.COMMON.Utilities;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
                var query = from PQRS in _dbContext.PQR
                            join Country in _dbContext.Country on PQRS.CountryId equals Country.Id
                            join CaseType in _dbContext.CaseType on PQRS.CaseTypeId equals CaseType.Id
                            join UserType in _dbContext.UserType on PQRS.UserTypeId equals UserType.Id
                            join Status in _dbContext.StatusPQR on PQRS.CaseStatus equals Status.Id
                            // join Files in _dbContext.Files on PQRS.FileId equals Files.Id
                            select new
                            {
                                Id = PQRS.Id,
                                Country = Country.CountryName,
                                CaseType = CaseType.Name,
                                UserType = UserType.Name,
                                RazonSocial = PQRS.RazonSocial,
                                Nit = PQRS.Nit,
                                Cedula = PQRS.Cedula,
                                Nombre = PQRS.Name,
                                Email = PQRS.Email,
                                Telefono = PQRS.PhoneNumber,
                                File = PQRS.FileId,
                                Comentario = PQRS.Comentario,
                                AutorizaTratamientoDatos = PQRS.AutorizaTratamientoDatos,
                                NumeroCaso = PQRS.CaseNumber,
                                Estatus = Status.Name,
                                FechaPQR = PQRS.PQRDate
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
                return new OkObjectResult(response);
            }
        }

        // GET: api/Pqr/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
             
                var query = from PQRS in _dbContext.PQR
                            join Country in _dbContext.Country on PQRS.CountryId equals Country.Id
                            join CaseType in _dbContext.CaseType on PQRS.CaseTypeId equals CaseType.Id
                            join UserType in _dbContext.UserType on PQRS.UserTypeId equals UserType.Id
                            join Status in _dbContext.StatusPQR on PQRS.CaseStatus equals Status.Id
                            // join Files in _dbContext.Files on PQRS.FileId equals Files.Id
                            where PQRS.Id==Id
                            select new
                            {
                                Id = PQRS.Id,
                                Country = Country.CountryName,
                                CaseType = CaseType.Name,


                                UserType = UserType.Name,
                                RazonSocial = PQRS.RazonSocial,
                                Nit = PQRS.Nit,
                                Cedula = PQRS.Cedula,
                                Nombre = PQRS.Name,
                                Email = PQRS.Email,
                                Telefono = PQRS.PhoneNumber,

                                File = PQRS.FileId,

                                Comentario = PQRS.Comentario,
                                AutorizaTratamientoDatos = PQRS.AutorizaTratamientoDatos,
                                NumeroCaso = PQRS.CaseNumber,
                                Estatus = Status.Name,
                                FechaPQR = PQRS.PQRDate
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

        // POST: api/Pqr/CreatePQR
        [HttpPost("[action]")]
        public async Task<IActionResult> CreatePQR([FromBody] PqrViewModel modelo)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Random random = new Random();


            Guid fileId = Guid.Parse("48cc9863-a763-4d68-8883-59d39ccec665");

            if (modelo.File.data != null)
            {
                fileId = Guid.NewGuid();
                FilesEntity file = new FilesEntity
                {
                    Id = fileId,
                    height = modelo.File.height,
                    timestamp = modelo.File.timestamp,
                    uri = modelo.File.uri,
                    fileName = modelo.File.fileName,
                    data = modelo.File.data
                };

                _dbContext.Files.Add(file);
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
                Comentario = modelo.Comentario,
                AutorizaTratamientoDatos = modelo.AutorizaTratamientoDatos,
                CaseNumber = random.Next(10000, 99999),
                CaseStatus = Guid.Parse("7b1bf27e-c376-4723-aebf-d596edf7ee26"),
                PQRDate = DateTime.Now,
                FileId = fileId,

            };
                
                _dbContext.PQR.Add(pqr);
          

            try
                {
                    await _dbContext.SaveChangesAsync();

                    // Enviamos mensaje de correo al usuario para notificar que recibimos su pqr
                    var plantillaUsuario = await _dbContext.MailTemplate.FirstOrDefaultAsync(a => a.Id == Guid.Parse("AD1C2E42-22C9-4608-AED3-0D29A427850E"));

                    Guid IdPlantilla =  plantillaUsuario.Id;
                    var plantilla = await _dbContext.MailTemplate.FirstOrDefaultAsync(e => e.Id == IdPlantilla && e.Enabled == true);

                    var images = new PlantillasHTML();

                if (plantilla != null)
                {
                    
                    string htmlPlantilla = System.Web.HttpUtility.HtmlDecode(plantilla.Html);

                   

                    htmlPlantilla = htmlPlantilla.Replace("{nombre}", pqr.Name);
                    htmlPlantilla = htmlPlantilla.Replace("{texto}", plantillaUsuario.Message);
                    htmlPlantilla = htmlPlantilla.Replace("{Numero}", pqr.CaseNumber.ToString());

                    htmlPlantilla = htmlPlantilla.Replace("images/image-1.png", images.image1);

                    htmlPlantilla = htmlPlantilla.Replace("images/image-2.png", images.image2);

                    htmlPlantilla = htmlPlantilla.Replace("images/image-3.png", images.image3);


                    EmailViewModel correo = new EmailViewModel
                    {
                        Destination = modelo.Email,
                        Suject = "Recibimos tu PQR",
                        Message = htmlPlantilla,
                        IsHtml = true
                    };

                    SendEmail SendCorreo = new SendEmail();
           
                SendCorreo.SendAsync(correo);
                   
                }

         // Enviamos mensaje de correo al administrador para notificarle que hay un nuevo pqr
                var asignacion = await _dbContext.MailTemplate.FirstOrDefaultAsync(a => a.Id == Guid.Parse("87824642-B0D4-41FD-AC78-4C35DC46EF0D"));
                var admin = await _dbContext.Users.FirstOrDefaultAsync(a => a.Role == Guid.Parse("B08FCC3A-EA4B-4D30-AC60-0445EEA65F9C"));

                Guid IdPlantillaAdmin = asignacion.Id;
                var plantillaAdmin = await _dbContext.MailTemplate.FirstOrDefaultAsync(e => e.Id == IdPlantillaAdmin && e.Enabled == true);


                if (plantillaAdmin != null)
                {
                    
                    string htmlPlantilla = System.Web.HttpUtility.HtmlDecode(plantillaAdmin.Html);
                    htmlPlantilla = htmlPlantilla.Replace("{nombre}", "Administrador");
                    htmlPlantilla = htmlPlantilla.Replace("{texto}", plantillaAdmin.Message);
                    htmlPlantilla = htmlPlantilla.Replace("{Numero}", pqr.CaseNumber.ToString());

                    htmlPlantilla = htmlPlantilla.Replace("images/image-1.png", images.image1);

                    htmlPlantilla = htmlPlantilla.Replace("images/image-2.png", images.image2);

                    htmlPlantilla = htmlPlantilla.Replace("images/image-3.png", images.image3);

                    EmailViewModel correo = new EmailViewModel
                    {
                        Destination = admin.Email,
                        Suject = "Se registró nueva PQR",
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

            //query.CountryId = modelo.CountryId;
            /*query.CaseTypeId = modelo.CaseTypeId;
            query.UserTypeId = modelo.UserTypeId;
            query.RazonSocial = modelo.RazonSocial;
            query.Nit = modelo.Nit;
            query.Cedula = modelo.Cedula;
            query.Name = modelo.Name;
            query.Email = modelo.Email;
            query.PhoneNumber = modelo.PhoneNumber;
            
            query.AutorizaTratamientoDatos = modelo.AutorizaTratamientoDatos;
            query.CaseNumber = modelo.CaseNumber;*/
            query.CaseStatus = modelo.CaseStatus;
            /*query.PQRDate = modelo.PQRDate;

            query.FileId = modelo.File;
            */
            

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

        public class PqrResponse
        {
            public bool success { get; set; }
            public List<PqrEntity> data { get; set; }
        }


    }
}
