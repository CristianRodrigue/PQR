using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class FilesEntity
    {
        public Guid Id { get; set; }

        public byte[] File { get; set; }
    }
}
