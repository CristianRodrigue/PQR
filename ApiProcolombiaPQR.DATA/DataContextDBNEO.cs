using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.EntityFrameworkCore;

namespace ApiProcolombiaPQR.DATA
{

    public class DataContextDBNEO : DbContext
    {
        public DataContextDBNEO(DbContextOptions<DataContextDBNEO> options) : base(options) { }

        public DbSet<Configuracion> Configuracion { get; set; }

        
    }
}
