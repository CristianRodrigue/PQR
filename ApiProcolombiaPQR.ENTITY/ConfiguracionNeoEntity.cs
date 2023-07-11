using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class ConfiguracionNeoEntity
    {
        [MaxLength(30)]
        [Key]
        public string Nombre { get; set; }

        [MaxLength(500)]
        public string Valor { get; set; }

        [MaxLength(500)]
        public string Descripcion { get; set; }
    }
}
