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
                _context.CaseType.Add(new CaseTypeEntity { Name = "Queja" });
                _context.CaseType.Add(new CaseTypeEntity { Name = "Reclamo" });
                _context.Country.Add(new CountryEntity { CountryName = "Argentina" });
                _context.Country.Add(new CountryEntity { CountryName = "Brasil" });
                _context.Country.Add(new CountryEntity { CountryName = "Canadá" });
                _context.Country.Add(new CountryEntity { CountryName = "Chile" });
                _context.Country.Add(new CountryEntity { CountryName = "Perú" });
                _context.Country.Add(new CountryEntity { CountryName = "Ecuador" });
                _context.Country.Add(new CountryEntity { CountryName = "Estados Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "España" });
                _context.Country.Add(new CountryEntity { CountryName = "Francia" });
                _context.Country.Add(new CountryEntity { CountryName = "Italia" });
                _context.Country.Add(new CountryEntity { CountryName = "Japón" });
                _context.Country.Add(new CountryEntity { CountryName = "China" });
                _context.Country.Add(new CountryEntity { CountryName = "India" });
                _context.Country.Add(new CountryEntity { CountryName = "Afganistán" });
                _context.Country.Add(new CountryEntity { CountryName = "Albania" });
                _context.Country.Add(new CountryEntity { CountryName = "Alemania" });
                _context.Country.Add(new CountryEntity { CountryName = "Andorra" });
                _context.Country.Add(new CountryEntity { CountryName = "Angola" });
                _context.Country.Add(new CountryEntity { CountryName = "Antigua y Barbuda" });
                _context.Country.Add(new CountryEntity { CountryName = "Arabia Saudita" });
                _context.Country.Add(new CountryEntity { CountryName = "Argelia" });
                _context.Country.Add(new CountryEntity { CountryName = "Argentina" });
                _context.Country.Add(new CountryEntity { CountryName = "Armenia" });
                _context.Country.Add(new CountryEntity { CountryName = "Australia" });
                _context.Country.Add(new CountryEntity { CountryName = "Austria" });
                _context.Country.Add(new CountryEntity { CountryName = "Azerbaiyán" });
                _context.Country.Add(new CountryEntity { CountryName = "Bahamas" });
                _context.Country.Add(new CountryEntity { CountryName = "Bangladés" });
                _context.Country.Add(new CountryEntity { CountryName = "Barbados" });
                _context.Country.Add(new CountryEntity { CountryName = "Baréin" });
                _context.Country.Add(new CountryEntity { CountryName = "Bélgica" });
                _context.Country.Add(new CountryEntity { CountryName = "Belice" });
                _context.Country.Add(new CountryEntity { CountryName = "Benín" });
                _context.Country.Add(new CountryEntity { CountryName = "Bielorrusia" });
                _context.Country.Add(new CountryEntity { CountryName = "Birmania" });
                _context.Country.Add(new CountryEntity { CountryName = "Bolivia" });
                _context.Country.Add(new CountryEntity { CountryName = "Bosnia y Herzegovina" });
                _context.Country.Add(new CountryEntity { CountryName = "Botsuana" });
                _context.Country.Add(new CountryEntity { CountryName = "Brasil" });
                _context.Country.Add(new CountryEntity { CountryName = "Brunéi" });
                _context.Country.Add(new CountryEntity { CountryName = "Bulgaria" });
                _context.Country.Add(new CountryEntity { CountryName = "Burkina Faso" });
                _context.Country.Add(new CountryEntity { CountryName = "Burundi" });
                _context.Country.Add(new CountryEntity { CountryName = "Bután" });
                _context.Country.Add(new CountryEntity { CountryName = "Cabo Verde" });
                _context.Country.Add(new CountryEntity { CountryName = "Camboya" });
                _context.Country.Add(new CountryEntity { CountryName = "Camerún" });
                _context.Country.Add(new CountryEntity { CountryName = "Canadá" });
                _context.Country.Add(new CountryEntity { CountryName = "Catar" });
                _context.Country.Add(new CountryEntity { CountryName = "Chad" });
                _context.Country.Add(new CountryEntity { CountryName = "Chile" });
                _context.Country.Add(new CountryEntity { CountryName = "China" });
                _context.Country.Add(new CountryEntity { CountryName = "Chipre" });
                _context.Country.Add(new CountryEntity { CountryName = "Ciudad del Vaticano" });
                _context.Country.Add(new CountryEntity { CountryName = "Colombia" });
                _context.Country.Add(new CountryEntity { CountryName = "Comoras" });
                _context.Country.Add(new CountryEntity { CountryName = "Corea del Norte" });
                _context.Country.Add(new CountryEntity { CountryName = "Corea del Sur" });
                _context.Country.Add(new CountryEntity { CountryName = "Costa de Marfil" });
                _context.Country.Add(new CountryEntity { CountryName = "Costa Rica" });
                _context.Country.Add(new CountryEntity { CountryName = "Croacia" });
                _context.Country.Add(new CountryEntity { CountryName = "Cuba" });
                _context.Country.Add(new CountryEntity { CountryName = "Dinamarca" });
                _context.Country.Add(new CountryEntity { CountryName = "Dominica" });
                _context.Country.Add(new CountryEntity { CountryName = "Ecuador" });
                _context.Country.Add(new CountryEntity { CountryName = "Egipto" });
                _context.Country.Add(new CountryEntity { CountryName = "El Salvador" });
                _context.Country.Add(new CountryEntity { CountryName = "Emiratos Árabes Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "Eritrea" });
                _context.Country.Add(new CountryEntity { CountryName = "Eslovaquia" });
                _context.Country.Add(new CountryEntity { CountryName = "Eslovenia" });
                _context.Country.Add(new CountryEntity { CountryName = "España" });
                _context.Country.Add(new CountryEntity { CountryName = "Estados Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "Estonia" });
                _context.Country.Add(new CountryEntity { CountryName = "Etiopía" });
                _context.Country.Add(new CountryEntity { CountryName = "Argentina" });
                _context.Country.Add(new CountryEntity { CountryName = "Brasil" });
                _context.Country.Add(new CountryEntity { CountryName = "Canadá" });
                _context.Country.Add(new CountryEntity { CountryName = "Chile" });
                _context.Country.Add(new CountryEntity { CountryName = "Perú" });
                _context.Country.Add(new CountryEntity { CountryName = "Ecuador" });
                _context.Country.Add(new CountryEntity { CountryName = "Estados Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "España" });
                _context.Country.Add(new CountryEntity { CountryName = "Francia" });
                _context.Country.Add(new CountryEntity { CountryName = "Italia" });
                _context.Country.Add(new CountryEntity { CountryName = "Japón" });
                _context.Country.Add(new CountryEntity { CountryName = "China" });
                _context.Country.Add(new CountryEntity { CountryName = "India" });
                _context.Country.Add(new CountryEntity { CountryName = "Afganistán" });
                _context.Country.Add(new CountryEntity { CountryName = "Albania" });
                _context.Country.Add(new CountryEntity { CountryName = "Alemania" });
                _context.Country.Add(new CountryEntity { CountryName = "Andorra" });
                _context.Country.Add(new CountryEntity { CountryName = "Angola" });
                _context.Country.Add(new CountryEntity { CountryName = "Antigua y Barbuda" });
                _context.Country.Add(new CountryEntity { CountryName = "Arabia Saudita" });
                _context.Country.Add(new CountryEntity { CountryName = "Argelia" });
                _context.Country.Add(new CountryEntity { CountryName = "Argentina" });
                _context.Country.Add(new CountryEntity { CountryName = "Armenia" });
                _context.Country.Add(new CountryEntity { CountryName = "Australia" });
                _context.Country.Add(new CountryEntity { CountryName = "Austria" });
                _context.Country.Add(new CountryEntity { CountryName = "Azerbaiyán" });
                _context.Country.Add(new CountryEntity { CountryName = "Bahamas" });
                _context.Country.Add(new CountryEntity { CountryName = "Bangladés" });
                _context.Country.Add(new CountryEntity { CountryName = "Barbados" });
                _context.Country.Add(new CountryEntity { CountryName = "Baréin" });
                _context.Country.Add(new CountryEntity { CountryName = "Bélgica" });
                _context.Country.Add(new CountryEntity { CountryName = "Belice" });
                _context.Country.Add(new CountryEntity { CountryName = "Benín" });
                _context.Country.Add(new CountryEntity { CountryName = "Bielorrusia" });
                _context.Country.Add(new CountryEntity { CountryName = "Birmania" });
                _context.Country.Add(new CountryEntity { CountryName = "Bolivia" });
                _context.Country.Add(new CountryEntity { CountryName = "Bosnia y Herzegovina" });
                _context.Country.Add(new CountryEntity { CountryName = "Botsuana" });
                _context.Country.Add(new CountryEntity { CountryName = "Brasil" });
                _context.Country.Add(new CountryEntity { CountryName = "Brunéi" });
                _context.Country.Add(new CountryEntity { CountryName = "Bulgaria" });
                _context.Country.Add(new CountryEntity { CountryName = "Burkina Faso" });
                _context.Country.Add(new CountryEntity { CountryName = "Burundi" });
                _context.Country.Add(new CountryEntity { CountryName = "Bután" });
                _context.Country.Add(new CountryEntity { CountryName = "Cabo Verde" });
                _context.Country.Add(new CountryEntity { CountryName = "Camboya" });
                _context.Country.Add(new CountryEntity { CountryName = "Camerún" });
                _context.Country.Add(new CountryEntity { CountryName = "Canadá" });
                _context.Country.Add(new CountryEntity { CountryName = "Catar" });
                _context.Country.Add(new CountryEntity { CountryName = "Chad" });
                _context.Country.Add(new CountryEntity { CountryName = "Chile" });
                _context.Country.Add(new CountryEntity { CountryName = "China" });
                _context.Country.Add(new CountryEntity { CountryName = "Chipre" });
                _context.Country.Add(new CountryEntity { CountryName = "Ciudad del Vaticano" });
                _context.Country.Add(new CountryEntity { CountryName = "Colombia" });
                _context.Country.Add(new CountryEntity { CountryName = "Comoras" });
                _context.Country.Add(new CountryEntity { CountryName = "Corea del Norte" });
                _context.Country.Add(new CountryEntity { CountryName = "Corea del Sur" });
                _context.Country.Add(new CountryEntity { CountryName = "Costa de Marfil" });
                _context.Country.Add(new CountryEntity { CountryName = "Costa Rica" });
                _context.Country.Add(new CountryEntity { CountryName = "Croacia" });
                _context.Country.Add(new CountryEntity { CountryName = "Cuba" });
                _context.Country.Add(new CountryEntity { CountryName = "Dinamarca" });
                _context.Country.Add(new CountryEntity { CountryName = "Dominica" });
                _context.Country.Add(new CountryEntity { CountryName = "Ecuador" });
                _context.Country.Add(new CountryEntity { CountryName = "Egipto" });
                _context.Country.Add(new CountryEntity { CountryName = "El Salvador" });
                _context.Country.Add(new CountryEntity { CountryName = "Emiratos Árabes Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "Eritrea" });
                _context.Country.Add(new CountryEntity { CountryName = "Eslovaquia" });
                _context.Country.Add(new CountryEntity { CountryName = "Eslovenia" });
                _context.Country.Add(new CountryEntity { CountryName = "España" });
                _context.Country.Add(new CountryEntity { CountryName = "Estados Unidos" });
                _context.Country.Add(new CountryEntity { CountryName = "Estonia" });
                _context.Country.Add(new CountryEntity { CountryName = "Etiopía" });

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
