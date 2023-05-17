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
                _context.CaseType.Add(new CaseTypeEntity { Name = "Reclamo" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Felicitacion" });


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

    }
}
