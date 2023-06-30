using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeoConnectController : ControllerBase
    {
        private DataContextDB _dbContext;

        public NeoConnectController(DataContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        /*[HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var query = await _dbContext.NEO.Select(x => new
                {
                    NeoApi = x.NeoApi,
                    NeoContrasenia = x.NeoContrasenia,
                    NeoToken = x.NeoToken,
                    NeoUsuario = x.NeoUsuario,
                    NeoClientId = x.NeoClientId,
                    NeoClientSecret = x.NeoClientSecret,
                    NeoApiToken = x.NeoApiToken,
                    NeoApiData = x.NeoApiData,

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

        /*private void EstablecerConexionNeo()
        {
            using (var client = new HttpClient())
            {
                //construirUrl
                var queryString = new StringBuilder();
                queryString.Append("?grant_type=password");

                queryString.Append("&username=");

                queryString.Append(HttpUtility.UrlEncode(NeoUsuario));

                queryString.Append("&password=");
                queryString.Append(HttpUtility.UrlEncode(NeoContrasenia));
                queryString.Append(HttpUtility.UrlEncode(NeoToken));

                queryString.Append("&client_id=");
                queryString.Append(NeoClientId);

                queryString.Append("&client_secret=");
                queryString.Append(NeoClientSecret);

                //1. solicitud de token
                client.BaseAddress = new Uri(NeoApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(NeoApiToken + queryString.ToString(), null).Result;
                if (response.IsSuccessStatusCode)
                {
                    AutenticacionNeo = response.Content.ReadAsAsync<AutenticacionNeo>().Result;
                }
            }
        }*/
    }
}
