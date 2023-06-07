using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte[] Password_hash { get; set; }

        public byte[] Password_salt { get; set; }

        public Guid Role { get; set; }
    }
}
