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
    public class DataContextDB : DbContext
    {
        public DataContextDB(DbContextOptions<DataContextDB> options) : base(options) { }


        public DbSet<UserEntity> Users { get; set; }

        public DbSet<UserTypeEntity> UserType { get; set; }

        public DbSet<CaseTypeEntity> CaseType { get; set; }

        public DbSet<CountryEntity> Country { get; set; }

        public DbSet<PqrEntity> PQR { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<CountryEntity>()
                .HasMany(e => e.Id)
                    .WithOne(e => e.Id)
                .HasForeignKey<PqrEntity>(e => e.Id)
                .IsRequired();
            */


            base.OnModelCreating(modelBuilder);
        }
    }
}
