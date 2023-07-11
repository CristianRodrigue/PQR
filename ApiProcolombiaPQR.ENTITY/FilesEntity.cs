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

        public int height { get; set; }

        public DateTime timestamp { get; set; }

        public string uri { get; set; }

        public string fileName { get; set; }

        public byte[] data { get; set; }
    }
}
