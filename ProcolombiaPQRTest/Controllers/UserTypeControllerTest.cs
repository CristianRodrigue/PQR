﻿using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcolombiaPQRTest.Controllers
{
    [TestFixture]
    public class UserTypeControllerTest
    {
        private UserTypeController _controller;
        private DataContextDB _dbContext;

        [SetUp]
        public void Setup()
        {
            // Configurar el DbContext en memoria para las pruebas
            var options = new DbContextOptionsBuilder<DataContextDB>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new DataContextDB(options);

            // Agregar datos de prueba al DbContext
            _dbContext.UserType.AddRange(new[]
            {
                new UserTypeEntity { Id = Guid.Parse("636a919e-627a-4fb4-990d-7f942914b555"), Name = "Type 1" },

            });
            _dbContext.SaveChanges();

            // Crear la instancia del controlador a probar
            _controller = new UserTypeController(_dbContext);
        }

        [Test]
        public async Task GetAll_ReturnsOkResultWithCorrectData()
        {
            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

            var response = okResult.Value as UserTypeController.UserTypeResponse;
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);

            var data = response.Data;
            Assert.IsTrue(data.Any());

            var firstItem = data.First();
            Assert.AreEqual(Guid.Parse("636a919e-627a-4fb4-990d-7f942914b555"), firstItem.Id);
            Assert.AreEqual("Type 1", firstItem.Name);

            ;
        }
    }
}