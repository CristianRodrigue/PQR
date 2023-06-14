﻿using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailTemplateController : ControllerBase
    {
        private DataContextDB _dbContext;

        public MailTemplateController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/MailTemplate/GetAll
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = await _dbContext.MailTemplate
                    .Select(x => new 
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Enabled = x.Enabled
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
                return BadRequest(ex.Message);
            }
        }


        // GET: api/MailTemplate/GetById
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            try
            {
                var query = await _dbContext.MailTemplate.Where(x => x.Id == Id)
                    .Select(x => new
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Html = x.Html,
                        Enabled = x.Enabled
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
                return BadRequest(ex.Message);
            }
        }


        // POST: api/MailTemplate/Create
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] MailTemplateViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string htmlPlantilla = System.Web.HttpUtility.HtmlEncode(modelo.Html);

                MailTemplateEntity template = new MailTemplateEntity 
                { 
                    Name = modelo.Name,
                    Description = modelo.Description,
                    Html = htmlPlantilla,
                    Enabled = false
                };

                _dbContext.MailTemplate.Add(template);
                await _dbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/MailTemplate/Delete/Id
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _dbContext.MailTemplate.FirstOrDefaultAsync(d => d.Id == Id);

            if (template == null)
            {
                return NotFound();
            }

            try
            {
                _dbContext.MailTemplate.Remove(template);
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