using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System;
using System.Net.Http;

using System.Threading.Tasks;
using System.Collections.Generic;


namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AutenticacionNeo : ControllerBase
    {
        public string Id { get; set; }
        // ReSharper disable once InconsistentNaming
        public string Issued_At { get; set; }
        //Por compatibilidad con respuesta de NEO
        // ReSharper disable once InconsistentNaming
        public string Token_Type { get; set; }
        // ReSharper disable once InconsistentNaming
        public string Instance_Url { get; set; }
        public string Signature { get; set; }
        // ReSharper disable once InconsistentNaming
        public string Access_Token { get; set; }
    }
    public class NeoConnectController
    {

        public string NeoApi { get; set; }
        public string NeoContrasenia { get; private set; }
        public string NeoToken { get; private set; }
        public string NeoUsuario { get; private set; }
        public string NeoClientId { get; private set; }
        public string NeoClientSecret { get; private set; }
        public string NeoApiToken { get; private set; }
        public string NeoApiData { get; private set; }
        private AutenticacionNeo AutenticacionNeo { get; set; }

        public NeoConnectController(string neoApi, string neoContrasenia,string neoToken,string neoUsuario, string neoClientId, string neoClientSecret, string neoApiToken, string neoApiData)
        {
            NeoApi = neoApi;
            NeoContrasenia = neoContrasenia;
            NeoToken = neoToken;
            NeoUsuario = neoUsuario;
            NeoClientId = neoClientId;
            NeoClientSecret = neoClientSecret;
            NeoApiToken = neoApiToken;
            NeoApiData = neoApiData;
            EstablecerConexionNeo();
        }

        public T EjecutarConsulta<T>(string query)
        {
            if (AutenticacionNeo == null)
            {
                throw new Exception("No se ha logrado establecer conexión a NEO");
            }

            T respuesta = default(T);

            using (var clienteConsulta = new HttpClient())
            {
                clienteConsulta.BaseAddress = new Uri(AutenticacionNeo.Instance_Url);
                clienteConsulta.DefaultRequestHeaders.Accept.Clear();
                clienteConsulta.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clienteConsulta.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", AutenticacionNeo.Access_Token);
                HttpResponseMessage responseConsulta = clienteConsulta.GetAsync(NeoApiData + "query/?q=" + HttpUtility.UrlEncode(query)).Result;
                if (responseConsulta.IsSuccessStatusCode)
                {
                    respuesta = responseConsulta.Content.ReadAsAsync<T>().Result;
                }
            }
            return respuesta;
        }

        private async Task<bool> EstablecerConexionNeo()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var requestBody = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", NeoUsuario),
                new KeyValuePair<string, string>("password", NeoContrasenia),
                new KeyValuePair<string, string>("client_id", NeoClientId),
                new KeyValuePair<string, string>("client_secret", NeoClientSecret)
            };

                    client.BaseAddress = new Uri(NeoApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var tokenResponse = await client.PostAsync(NeoApiToken, new FormUrlEncodedContent(requestBody));

                    if (tokenResponse.IsSuccessStatusCode)
                    {
                        var tokenResult = await tokenResponse.Content.ReadAsAsync<AutenticacionNeo>();
                        AutenticacionNeo = tokenResult;
                        if (AutenticacionNeo != null)
                        {
                            return true; // La conexión se estableció correctamente
                        }

                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al establecer la conexión con NEO: " + ex.Message);
            }
            return false;
        }
        [HttpGet("[action]")]
        public async Task<ActionResult<string>> EstadoConexionNeo()
        {
            bool conexionEstablecida = await EstablecerConexionNeo();

            if (conexionEstablecida)
            {
                return new OkObjectResult("La conexión con NEO se estableció correctamente.");
            }
            else
            {
                return new BadRequestObjectResult("No se pudo establecer la conexión con NEO.");
            }
        }




    }

}
