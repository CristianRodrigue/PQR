using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.COMMON.Enums
{
    public class NeoConnect
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
    }
}
