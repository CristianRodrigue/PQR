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

        public DbSet<RoleEntity> Role { get; set; }

        public DbSet<ConsecutiveEntity> Consecutive { get; set; }

        public DbSet<StatusEntity> StatusPQR { get; set; }

        public DbSet<FilesEntity> Files { get; set; }

        public DbSet<MailTemplateEntity> MailTemplate { get; set; }

        public DbSet<EmployeeEntity> Employee { get; set; }

        public DbSet<AssignEntity> Assign { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*
            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.CaseType)
                .WithMany()
                .HasForeignKey(p => p.CaseTypeId);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.UserType)
                .WithMany()
                .HasForeignKey(p => p.UserTypeId);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.CaseStatus);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.File)
                .WithMany()
                .HasForeignKey(p => p.FileId);

            /////////////////////////////////////////////
            modelBuilder.Entity<CaseTypeEntity>()
        .HasMany(ct => ct.Pqrs)
        .WithOne(p => p.CaseType)
        .HasForeignKey(p => p.CaseTypeId);

            modelBuilder.Entity<CountryEntity>()
        .HasMany(c => c.Pqrs)
        .WithOne(p => p.Country)
        .HasForeignKey(p => p.CountryId);

            modelBuilder.Entity<PqrEntity>()
        .HasOne(p => p.Role)
        .WithMany(r => r.Pqrs)
        .HasForeignKey(p => p.Id);

            modelBuilder.Entity<PqrEntity>()
        .HasOne(p => p.Consecutive)
        .WithMany()
        .HasForeignKey(p => p.Id);

            
    

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.MailTemplate)
                .WithMany()
                .HasForeignKey(p => p.Id);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.Id);

            modelBuilder.Entity<PqrEntity>()
                .HasOne(p => p.Assign)
                .WithMany()
                .HasForeignKey(p => p.Id);
            */








            // Resto de relaciones de entidad...

            base.OnModelCreating(modelBuilder);
        }
    }
}
