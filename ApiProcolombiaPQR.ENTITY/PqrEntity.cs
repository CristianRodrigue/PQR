using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class PqrEntity
    {
        public Guid Id { get; set; }

        public Guid CountryId { get; set; }
        
        public CountryEntity? Country { get; }

        public Guid CaseTypeId { get; set; }

        // public CaseTypeEntity CaseType { get; }

        public Guid UserTypeId { get; set; }

        // public UserTypeEntity UserType { get; }

        public string? RazonSocial { get; set; }

        public string? Nit { get; set; }

        public  string? Cedula { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public byte[]? File { get; set; }

        public string? Comentario { get; set; }

        public bool AutorizaTratamientoDatos { get; set; }

    }
}
