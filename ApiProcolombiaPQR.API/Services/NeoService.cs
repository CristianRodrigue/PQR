using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.API.Services
{
    public class NeoService: ControllerBase
    {
        private DataContextDBNEO _dbContext;

        public class RespuestaNeo
        {
            public int TotalSize { get; set; }
            public List<Records> Records { get; set; }
        }

        public class Records
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string WebSite { get; set; }
        }

        public class EmpresaNeo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Website { get; set; }
        }

        public string ConsultarConfiguracion(string nombreParametro)
        {
            var configuracion = _dbContext.Configuracion.FirstOrDefault(c => c.Nombre == nombreParametro);

            if (configuracion != null)
            {
                return configuracion.Valor;
            }

            return string.Empty;
        }
        private NeoConnectController NeoConnect { get; set; }

        public NeoService(DataContextDBNEO contextNEO)
        {
            _dbContext = contextNEO;

            var neoApiData = ConsultarConfiguracion("NEO_API_DATA");
            var neoApi = ConsultarConfiguracion("NEO_API");
            var neoContrasenia = ConsultarConfiguracion("NEO_CONTRASENIA");
            var neoToken = ConsultarConfiguracion("NEO_TOKEN");
            var neoUsuario = ConsultarConfiguracion("NEO_USUARIO");
            var neoClientId = ConsultarConfiguracion("NEO_CLIENT_ID");
            var neoClientSecret = ConsultarConfiguracion("NEO_CLIENT_SECRET");
            var neoApiToken = ConsultarConfiguracion("NEO_API_TOKEN");

            NeoConnect = new NeoConnectController(neoApi, neoContrasenia, neoToken, neoUsuario, neoClientId, neoClientSecret, neoApiToken, neoApiData);
        }

        public EmpresaNeo ConsultarEmpresa(string nit)
        {
            EmpresaNeo empresa = null;
            string queryEmpresaNit = "Select Id, Name, Sector_Principal__c, Ciudad__c,Website FROM Account where Numero_de_Identificacion__c ='" + nit + "'";
            RespuestaNeo respuestaNeo = NeoConnect.EjecutarConsulta<RespuestaNeo>(queryEmpresaNit);
            if (respuestaNeo != null && respuestaNeo.TotalSize > 0)
            {
                empresa = new EmpresaNeo()
                {
                    Id = respuestaNeo.Records.ElementAt(0).Id.ToString(),
                    Name = respuestaNeo.Records.ElementAt(0).Name.ToString(),
                    Website = respuestaNeo.Records.ElementAt(0).WebSite.ToString()
                };

            }
            return empresa;
        }

        /*private string ConsultarConfiguracion(string nombre)
        {
            var db = new EjecutorDAO("DfiCtx");
            string valor = null;
            try
            {
                db.Conectar();
                const string query = "SELECT [Valor] FROM [parametro].[Configuracion] WHERE Nombre=@nombre";
                db.CrearComando(query);
                db.AsignarParametroCadena("@nombre", nombre);
                var reader = db.EjecutarConsulta();
                while (reader.Read())
                {
                    valor = reader[0].ToString().Trim();
                }
                /*Insert Area__c, 
                 * Cuenta__c,  
                 * Fecha_Realizada__c, 
                 * Name, 
                 * Tipo__c 
                 * FROM Quejas_y_Reclamos__c values ();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Desconectar();
            }

            return valor;
        }*/
    }
}
