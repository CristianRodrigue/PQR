using ApiProcolombiaPQR.ENTITY;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.DATA
{
    public class DataContextDBNeo : DbContext
    {
        public DataContextDBNeo(DbContextOptions<DataContextDBNeo> options) : base(options) { }

        public DbSet<ConfiguracionNeoEntity> Configuracion { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    }
}
