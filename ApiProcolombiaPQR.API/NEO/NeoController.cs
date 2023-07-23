using ApiProcolombiaPQR.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ApiProcolombiaPQR.API.NEO
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeoController : ControllerBase
    {
        private readonly SalesforceClient _salesforceClient;
        private DataContextDB _dbContext;

        public NeoController(SalesforceClient salesforceClient, DataContextDB dbContext)
        {
            _salesforceClient = salesforceClient;
            _dbContext = dbContext;
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> QuerySalesforceAsync([FromRoute] Guid Id)
        {
            try
            {
                var query = from PQRS in _dbContext.PQR
                            join Country in _dbContext.Country on PQRS.CountryId equals Country.Id
                            join CaseType in _dbContext.CaseType on PQRS.CaseTypeId equals CaseType.Id
                            join UserType in _dbContext.UserType on PQRS.UserTypeId equals UserType.Id
                            join Status in _dbContext.StatusPQR on PQRS.CaseStatus equals Status.Id
                            where PQRS.Id == Id
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
                var result1 = await query.FirstOrDefaultAsync();

                _salesforceClient.InstanceUrl = "https://procolombia.my.salesforce.com";
                _salesforceClient.login();

                // Realizar una consulta en Salesforce
                var result = _salesforceClient.Query(result1.Nit);

                // Verificar si se encontró la empresa correspondiente al NIT consultado
                bool foundNit = result.records.Any();

                if (foundNit)
                {
                    // Obtener el nombre de la cuenta
                    string cuenta = result.records.FirstOrDefault()?.Id;
                    string area = "Subdirección de calidad y sostenibilidad";
                    string tipo = result1.CaseType;
                    DateTime fechaAcusoRecibo = result1.FechaPQR;
                    string fechaFormateada = fechaAcusoRecibo.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    string name = result1.Comentario;


                    var caso = new
                    {
                        Area__c = area,
                        Cuenta__c = cuenta,
                        Fecha_acuso_de_recibo__c = fechaFormateada,
                        Name = name,
                        Tipo__c = tipo
                    };

                    // Convertir el objeto a JSON
                    var casoJson = JsonConvert.SerializeObject(caso);
                    try
                    {
                        // Crear una solicitud HTTP POST para crear el caso en Salesforce
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(_salesforceClient.InstanceUrl); // Utilizar la URL base de Salesforce que has proporcionado

                            var request = new HttpRequestMessage(HttpMethod.Post, "/services/data/v48.0/sobjects/Quejas_y_Reclamos__c"); // Utilizar la URL del endpoint específico para crear casos en Salesforce

                            request.Headers.Add("Authorization", "Bearer " + _salesforceClient.AuthToken);
                            request.Content = new StringContent(casoJson, Encoding.UTF8, "application/json");

                            // Enviar la solicitud HTTP POST y obtener la respuesta
                            var response = await client.SendAsync(request);
                            var responseBody = await response.Content.ReadAsStringAsync();

                            // Verificar si la solicitud fue exitosa
                            if (response.IsSuccessStatusCode)
                            {
                              

                                return Ok(true); // Devolver true si el caso se generó correctamente
                            }
                            else
                            {
                                // Manejar el error de la solicitud y devolver false
                                return Ok(false); // Devolver false si ocurrió un error al generar el caso
                            }
                        }
                    }
                    catch (Exception ex) {
                        return BadRequest("Error al enviar la solicitud a Salesforce: " + ex);
                    }

                    
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest("InstanceUrl no ha sido configurado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al consultar Salesforce: {ex.Message}");
            }
        }
    }
}
