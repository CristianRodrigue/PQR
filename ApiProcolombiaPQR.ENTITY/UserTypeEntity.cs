using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class UserTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<PqrEntity> PQR { get; }
    }
}
