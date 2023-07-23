using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;


namespace ApiProcolombiaPQR.API.NEO
{
    public class SalesforceClient
    {
        private const string apiEndPoint = "/services/data/v42.0/";
        public string loginEndpoint { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; } = "00D80000000LZNO!AQEAQAEn1_sbOK5cBrjW5uGSGAovT8jJCINo9rvFgIFj5usyPi3TK5AONYg6LfnUCsPBqH13qxUeJqKiJZcGhVzOdCXXlTM3";
        public string InstanceUrl { get; set; } = "https://procolombia.my.salesforce.com";

        public SalesforceClient()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            Username = InstanceDetailsProduction.username;
            Password = InstanceDetailsProduction.password;
            Token = InstanceDetailsProduction.token;
            ClientId = InstanceDetailsProduction.clientId;
            ClientSecret = InstanceDetailsProduction.clientSecret;
            loginEndpoint = InstanceDetailsProduction.loginEndPoint;
        }

        public void login()
        {
            if (string.IsNullOrEmpty(InstanceUrl))
            {
                throw new InvalidOperationException("InstanceUrl no ha sido configurado.");
            }

            var jsonResponse = default(string);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(InstanceUrl);

                var request = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            {"grant_type", "password"},
            {"client_id", ClientId },
            {"client_secret", ClientSecret },
            {"username",  Username},
            {"password", $"{Password}{Token}"},
        });

                request.Headers.Add("X-PreetyPrint", "1");
                var response = client.PostAsync(loginEndpoint, request).Result;

                jsonResponse = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Response: {jsonResponse}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error en la solicitud a Salesforce. Código de respuesta: {response.StatusCode}. Mensaje: {jsonResponse}");
                }

                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                if (values is not null)
                {
                    AuthToken = values["access_token"];
                    InstanceUrl = values["instance_url"];
                    Console.WriteLine($"AuthToken ={AuthToken}");
                    Console.WriteLine($"InstaceUrl ={InstanceUrl}");
                }
            }
        }



        public TopLayer Query(string nit)
        {
            var result = default(TopLayer);
            string soqlQuery = "Select Id FROM Account where Numero_de_Identificacion__c ='" + nit + "'%";
            using (var client = new HttpClient())
            {
                string restRequest = $"{InstanceUrl}{apiEndPoint}query?q={soqlQuery}";
                using (var request = GetNewHttpGetRequest(restRequest))
                {
                    result = GetResponse(request, client);
                }
            }
            return result;
        }

        private TopLayer GetResponse(HttpRequestMessage request, HttpClient client)
        {
            var response = client.SendAsync(request).Result;
            var content = response.Content.ReadAsStream();
            var textReader = new StreamReader(content);
            var jsonReader = new JsonTextReader(textReader);
            var jsonSerializer = JsonSerializer.CreateDefault();
            var result = jsonSerializer.Deserialize<TopLayer>(jsonReader);
            return result;
        }

        private HttpRequestMessage GetNewHttpGetRequest(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {AuthToken}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("X-PreetyPrint", "1");
            return request;
        }

    }
    internal class InstanceDetailsProduction
    {
        public const string loginEndPoint = "https://procolombia.my.salesforce.com/services/oauth2/token";
        public const string username = "wservice@proexport.com.co";
        public const string password = "colombia2018";
        public const string token = "PUaA4DRbMe1aO5ZiYEii9SGIZ";
        public const string clientId = "3MVG9CVKiXR7Ri5rvhUqm5w9wx1ZUHxSvHBIbAq4G9TDtqy77l4T0xee1XKs3bIe32BoHgPSf0zCUYJZdsywr";
        public const string clientSecret = "1080113624615878709";
    }
    public class TopLayer
    {
        public int totalsize { get; set; }
        public bool done { get; set; }
        public List<sfRecord> records { get; set; }
    }

    public class sfRecord
    {
        
        public string Id { get; set; }

    }

    
}
