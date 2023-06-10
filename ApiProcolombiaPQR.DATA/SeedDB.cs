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
            await CheckConsecutive();
            await CheckStatus();
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
                _context.CaseType.Add(new CaseTypeEntity { Name = "Queja" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Reclamo" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Felicitacion" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Pregunta" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Sugerencia" });

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

        private async Task CheckConsecutive()
        {
            if (!_context.Consecutive.Any())
            {
                _context.Consecutive.Add(new ConsecutiveEntity { Id = Guid.Parse("636a919e-627a-4fb4-990d-7f942914b555"), Number = 1 });
            
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckStatus()
        {
            if (!_context.StatusPQR.Any())
            {
                _context.StatusPQR.Add(new StatusEntity { Id = Guid.Parse("7b1bf27e-c376-4723-aebf-d596edf7ee26"), Name = "Acuso de recibido" });
                _context.StatusPQR.Add(new StatusEntity { Id = Guid.Parse("3ee6cd97-e6c3-4873-a44c-e9ee91b45661"), Name = "Cierre" });

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
                CreatePasswordHash("1234567890", out byte[] passwordHash, out byte[] passwordSalt);

                var pHash = passwordHash;
                var pSalt = passwordSalt;

                _context.Users.Add(new UserEntity { Name = "Administrador", Email = "ptecnologia1@procolombia.co", Password_hash = pHash,  Password_salt = pSalt, Role = Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c") });

                await _context.SaveChangesAsync();
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
