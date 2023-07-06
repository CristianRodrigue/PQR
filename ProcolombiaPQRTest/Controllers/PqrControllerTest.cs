using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Newtonsoft.Json;

namespace ProcolombiaPQRTest.Controllers
{
    [TestFixture]
    public class PqrControllerTest
    {
        private PqrController _controller;
        private DbContextOptions<DataContextDB> _dbContextOptions;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContextDB>()
             .UseInMemoryDatabase(databaseName: "TestDatabase")
             .Options;
        }

        [Test]
        public async Task GetAll_ReturnsDataSuccessfully()
        {
            // Arrange
            using (var dbContext = new DataContextDB(_dbContextOptions))
            {
                // Agregar datos de prueba a la base de datos en memoria
                // Aquí debes agregar datos que coincidan con la consulta en el método GetAll

                // Agregar datos a las tablas relacionadas (Country, CaseType, UserType, StatusPQR, Files) según la consulta en el método GetAll
                var country = new CountryEntity { Id = Guid.Parse("c4b129e3-2fcb-460d-96ad-71b375cff943"), CountryName = "Country 1" };
                var caseType = new CaseTypeEntity { Id = Guid.Parse("622f776d-3cf8-4981-95ad-8cd33415793d"), Name = "CaseType 1" };
                var userType = new UserTypeEntity { Id = Guid.Parse("01dee0f4-2a72-4525-87e6-765b6d00c8f0"), Name = "UserType 1" };
                var statusPQR = new StatusEntity { Id = Guid.Parse("29260c84-58e9-48fc-8641-a948d2fb598f"), Name = "StatusPQR 1" };

                dbContext.Country.Add(country);
                dbContext.CaseType.Add(caseType);
                dbContext.UserType.Add(userType);
                dbContext.StatusPQR.Add(statusPQR);

                // Agregar datos a la tabla PQR que coincidan con la consulta en el método GetAll
                var pqr = new PqrEntity
                {
                    Id = Guid.Parse("933ab5c4-df8b-4e4c-9b42-22c92722200d"),
                    CountryId = country.Id,
                    CaseTypeId = caseType.Id,
                    UserTypeId = userType.Id,
                    CaseStatus = statusPQR.Id,
                    RazonSocial = "Razon Social 1",
                    Nit = "Nit 1",
                    Cedula = "Cedula 1",
                    Name = "Name 1",
                    Email = "Email 1",
                    PhoneNumber = "Phone Number 1",
                    Comentario = "Comentario 1",
                    AutorizaTratamientoDatos = true,
                    CaseNumber = 1,
                    PQRDate = DateTime.Now
                };

                dbContext.PQR.Add(pqr);
                await dbContext.SaveChangesAsync();
            }

            // Act
            using (var dbContext = new DataContextDB(_dbContextOptions))
            {
                var controller = new PqrController(dbContext);
                var result = await controller.GetAll();

                // Assert
                Assert.That(result, Is.TypeOf<OkObjectResult>());
                var okResult = (OkObjectResult)result;
                Assert.That(okResult.Value, Is.Not.Null);

                var jsonResponse = JsonConvert.SerializeObject(okResult.Value);
                var response = JsonConvert.DeserializeObject<GetAllResponse>(jsonResponse);

                Assert.That(response.success, Is.True);
                Assert.That(response.data, Is.Not.Null);
            }
        }

    }
    public class GetAllResponse
    {
        public bool success { get; set; }
        public List<PqrEntity> data { get; set; }

        public string error { get; set; }
    }
}
