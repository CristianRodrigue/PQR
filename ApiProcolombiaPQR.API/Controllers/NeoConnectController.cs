using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ApiProcolombiaPQR.API.Controllers
{
   
    public class AutenticacionNeo
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

        private void EstablecerConexionNeo()
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
        }

       
    }
}
