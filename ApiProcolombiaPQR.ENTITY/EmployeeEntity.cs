using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class EmployeeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Division { get; set; }

        public string Cargo { get; set; }

        public virtual ICollection<PqrEntity> Pqrs { get; set; }
    }
}
