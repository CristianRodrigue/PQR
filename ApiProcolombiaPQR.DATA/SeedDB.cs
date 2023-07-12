using ApiProcolombiaPQR.ENTITY;
using System.Text;
using ISO3166;

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
            await CheckMailTemplate();
            await CheckEmployee();
            await CheckAssign();
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Country.Any())
            {
                foreach (var country in Country.List)
                {
                    _context.Country.Add(new CountryEntity { CountryName = country.Name });
                }

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
                _context.StatusPQR.Add(new StatusEntity { Id = Guid.Parse("5832b8ac-a7d3-448d-8dde-eb8fea6f4ace"), Name = "Revisado" });
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

        private async Task CheckMailTemplate()
        {
            if (!_context.MailTemplate.Any())
            {
                _context.MailTemplate.Add(new MailTemplateEntity { Id = Guid.Parse("AD1C2E42-22C9-4608-AED3-0D29A427850E"), Name = "Registro PQR", Description = "Correo para usuario al registrar PQR", Html = "GRACIAS POR REGISTRAR SU PQR", Enabled = true });
                _context.MailTemplate.Add(new MailTemplateEntity { Id = Guid.Parse("87824642-B0D4-41FD-AC78-4C35DC46EF0D"), Name = "PQR Admin", Description = "Correo PQR para admin", Html = "Se genero un nuevo PQR", Enabled = true });
                _context.MailTemplate.Add(new MailTemplateEntity { Id = Guid.Parse("081C08F2-1E22-405B-B7B7-4FE992BE27C2"), Name = "Asignacion PQR", Description = "Correo PQR para asignar", Html = "Se le asigno un PQR", Enabled = true });
    
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckEmployee()
        {
            if (!_context.Employee.Any())
            {
                _context.Employee.Add(new EmployeeEntity { Id = Guid.Parse("3A21A637-05D5-43F4-B0C4-5E4583582D07"), Name = "Cristian Rodriguez", Email = "ptecnologia1@procolombia.co", Division = "Tecnologia", Cargo = "Desarrollador practicante" });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAssign()
        {
            if (!_context.Assign.Any())
            {
                _context.Assign.Add(new AssignEntity { Id = Guid.Parse("3C08F18B-99BF-4413-85E5-F155B26D8603"), Name = "Usuario PQRS", IdMailTemplate = Guid.Parse("AD1C2E42-22C9-4608-AED3-0D29A427850E") });
                _context.Assign.Add(new AssignEntity { Id = Guid.Parse("B246118D-823C-41E3-AA03-6E09D99229B1"), Name = "Administrador PQRS", IdMailTemplate = Guid.Parse("87824642-B0D4-41FD-AC78-4C35DC46EF0D") });
                _context.Assign.Add(new AssignEntity { Id = Guid.Parse("FF68BEFF-A89D-416E-B560-959677D48BDE"), Name = "Asignacion PQRS", IdMailTemplate = Guid.Parse("081C08F2-1E22-405B-B7B7-4FE992BE27C2") });


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

        private async Task CheckConfiguracionNEOAsync()
        {
            if (!_context.Configuracion.Any())
            {
                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_API_DATA", Valor = "/services/data/v42.0/", Descripcion = "Recurso para acceso a la data" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_API", Valor = "https://procolombia.my.salesforce.com", Descripcion = "URL conexión API de NEO" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_CONTRASENIA", Valor = "colombia2018", Descripcion = "Contraseña" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_TOKEN", Valor = "PUaA4DRbMe1aO5ZiYEii9SGIZ", Descripcion = "Token de seguridad para acceso a aplicaciones externas" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_USUARIO", Valor = "wservice@proexport.com.co", Descripcion = "Usuario" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_CLIENT_ID", Valor = "3MVG9CVKiXR7Ri5rvhUqm5w9wx1ZUHxSvHBIbAq4G9TDtqy77l4T0xee1XKs3bIe32BoHgPSf0zCUYJZdsywr", Descripcion = "Client ID autenticación OAuth" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_CLIENT_SECRET", Valor = "1080113624615878709", Descripcion = "Client secret autenticación OAuth" });

                _context.Configuracion.Add(new ConfiguracionNeoEntity { Nombre = "NEO_API_TOKEN", Valor = "/services/oauth2/token", Descripcion = "Recurso para obtener token de autenticación" });


                await _context.SaveChangesAsync();
            }
        }

        
    }
}
