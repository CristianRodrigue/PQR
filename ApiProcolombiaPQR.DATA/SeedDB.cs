using ApiProcolombiaPQR.ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.DATA
{
    public class SeedDB
    {
        private readonly DataContextDB _context;

        public SeedDB(DataContextDB context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCaseTypeAsync();
            await CheckUserTypeAsync();
            await CheckRoleAsync();
            await CheckUsersAsync();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Country.Any())
            {
                _context.Country.Add(new CountryEntity { CountryName = "Colombia" });
                _context.Country.Add(new CountryEntity { CountryName = "Mexico" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCaseTypeAsync()
        {
            if (!_context.CaseType.Any())
            {
                _context.CaseType.Add(new CaseTypeEntity { Name = "Quejas" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Reclamos" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUserTypeAsync()
        {
            if (!_context.UserType.Any())
            {
                _context.UserType.Add(new UserTypeEntity { Name = "Persona natural" });
                _context.UserType.Add(new UserTypeEntity { Name = "Empresa" });


                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRoleAsync()
        {
            if (!_context.Role.Any())
            {
                _context.Role.Add(new RoleEntity { Id = Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c"), Name = "Admin" });
                _context.Role.Add(new RoleEntity { Id = Guid.Parse("035f2b37-100c-4312-a458-6c4bfc0ee34a"), Name = "User" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckUsersAsync()
        {
            if (!_context.Users.Any())
            {
                _context.Users.Add(new UserEntity { Name = "Administrador", Email = "ptecnologia1@procolombia.co", Password = "1234567890", Role = Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c") });

                await _context.SaveChangesAsync();
            }
        }

    }
}
