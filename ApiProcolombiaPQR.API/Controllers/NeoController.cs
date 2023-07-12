using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.COMMON.Enums;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Web;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeoController : ControllerBase
    {
        private DataContextDB _dbContext;
        //private DataContextDBNeo _contextNeo;
        private AutenticacionNeo autenticacionNeo = new AutenticacionNeo();
        private NeoConnect credencialesNeo = new NeoConnect();

        public NeoController(DataContextDB dbContext, DataContextDBNeo contextNeo)
        {
            _dbContext = dbContext;
            //_contextNeo = contextNeo;
        }

        // POST: api/Neo/GenerarCasoSaleforce
        [HttpPost("[action]")]
        public async Task<IActionResult> GenerarCasoSaleforce([FromBody] CasoViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EstablecerConexionNeo();

            return StatusCode(StatusCodes.Status201Created);
        }


        // Funciones

        private async void AutenticacionNeo()
        {
            try
            {
                var configuracionNeo = await _dbContext.Configuracion.Select(x => new ConfiguracionNeoEntity
                {
                    Nombre = x.Nombre,
                    Valor = x.Valor,
                    Descripcion = x.Descripcion
                }).ToListAsync();
            }
            catch (Exception ex) {
                
            }

            //autenticacionNeo.
        }

        private void EstablecerConexionNeo()
        {
            AutenticacionNeo();

            using (var client = new HttpClient())
            {
                // Creamos cadena url
                var queryString = new StringBuilder();
                
                queryString.Append("?grant_type=password");
                queryString.Append("&username=");
                /*queryString.Append(HttpUtility.UrlEncode(NeoUsuario));
                queryString.Append("&password=");
                queryString.Append(HttpUtility.UrlEncode(NeoContrasenia));
                queryString.Append(HttpUtility.UrlEncode(NeoToken));
                queryString.Append("&client_id=");
                queryString.Append(NeoClientId);
                queryString.Append("&client_secret=");
                queryString.Append(NeoClientSecret);*/
            }
        }



    }
}
