namespace ApiProcolombiaPQR.API.Models
{
    public class AutenticacionNeoViewModel
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
}
