using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProcolombiaPQRTest.Controllers
{
    [TestFixture]
    public class AuthControllerTest
    {
        private AuthController _controller;
        private DataContextDB _dbContext;

        [SetUp]
        public void Setup()
        {
            // Configurar el DbContext en memoria para las pruebas
            var options = new DbContextOptionsBuilder<DataContextDB>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new DataContextDB(options);

            CreatePasswordHash("1234567890", out byte[] passwordHash, out byte[] passwordSalt);
            var pHash = passwordHash;
            var pSalt = passwordSalt;

            // Agregar datos de prueba al DbContext
            _dbContext.Users.AddRange(new[]
            {
               

            new UserEntity { Id = Guid.Parse("636a919e-627a-4fb4-990d-7f942914b555"), Name = "Administrador", Email = "ptecnologia1@procolombia.co", Password_hash = pHash,  Password_salt = pSalt, Role = Guid.Parse("b08fcc3a-ea4b-4d30-ac60-0445eea65f9c") },

            new UserEntity { Id = Guid.NewGuid(), Name = "Usuario", Email = "usuario@procolombia.co", Password_hash = pHash,  Password_salt = pSalt, Role = Guid.Parse("c5a8a8e2-1066-44f6-876a-1ee6c476b92e") }


            });
            _dbContext.SaveChanges();

            // Crear la instancia del controlador a probar
            //_controller = new AuthController(_dbContext);
        }

        [Test]
        public async Task GetAll_ReturnsOkResultWithCorrectData()
        {
            try
            {
                // Act
                var result = await _controller.GetAll();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult.Value);

                var response = okResult.Value as AuthController.AuthResponse;
                Assert.IsTrue(response.success);
                Assert.IsNotNull(response.data);

                var data = response.data;
                Assert.IsTrue(data.Any());

                var firstItem = data.First();
                Assert.AreEqual(Guid.Parse("636a919e-627a-4fb4-990d-7f942914b555"), firstItem.Id);
                Assert.AreEqual("Administrador", firstItem.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw; // Re-lanzar la excepción para que se muestre en el resultado de la prueba
            }
        }

        [Test]
        public async Task GetById_ExistingId_ReturnsOkResultWithCorrectData()
        {
            // Arrange
            var id = Guid.Parse("3f3bcb81-79f3-47e3-94c9-7c8a4edc2f34");

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var response = okResult.Value as AuthController.AuthResponse;
            Assert.IsTrue(response.success);
            Assert.IsNotNull(response.data);

            var data = response.data;
            Assert.AreEqual(1, data.Count);

            var user = data.First();
            Assert.AreEqual(id, user.Id);
            Assert.AreEqual("Usuario", user.Name);
            Assert.AreEqual("usuario@procolombia.co", user.Email);
            Assert.AreEqual(Guid.Parse("c5a8a8e2-1066-44f6-876a-1ee6c476b92e"), user.Role);
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
