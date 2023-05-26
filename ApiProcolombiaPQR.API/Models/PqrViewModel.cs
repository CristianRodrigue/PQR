using ApiProcolombiaPQR.ENTITY;

namespace ApiProcolombiaPQR.API.Models
{
    public class PqrViewModel
    {
        public Guid Id { get; set; }

        public int CaseNumber { get; set; }

        public Guid CountryId { get; set; }

        public Guid CaseTypeId { get; set; }

        public Guid UserTypeId { get; set; }

        public string RazonSocial { get; set; }

        public string? Nit { get; set; }

        public string? Cedula { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[]? File { get; set; }

        public string Comentario { get; set; }

        public bool AutorizaTratamientoDatos { get; set; }

        public Guid CaseStatus { get; set; }
    }
}
