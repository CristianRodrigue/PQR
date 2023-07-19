using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class MailTemplateEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }

        public bool Enabled { get; set; }

        public string? Message { get; set; }

        public virtual ICollection<PqrEntity> Pqrs { get; set; }
    }
}
