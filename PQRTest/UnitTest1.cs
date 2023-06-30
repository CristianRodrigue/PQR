using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.API.Models;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQRTest
{
    [TestClass]
    public class UnitTest1
    {
  
            private PqrController _controller;
            private Mock<DataContextDB> _dbContextMock;

            [TestInitialize]
            public void Initialize()
            {
                _dbContextMock = new Mock<DataContextDB>();
                _controller = new PqrController(_dbContextMock.Object);
            }

            [TestMethod]
            public async Task CreatePQR_ValidModel_Returns201Created()
            {
                // Arrange
                var modelo = new PqrViewModel
                {
                    CountryId = Guid.NewGuid(),
                    CaseTypeId = Guid.NewGuid(),
                    UserTypeId = Guid.NewGuid(),
                    RazonSocial = "Test Company",
                    Nit = "123456789",
                    Cedula = "987654321",
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    PhoneNumber = "1234567890",
                    Comentario = "Test comment",
                    AutorizaTratamientoDatos = true,
                    CaseStatus = Guid.NewGuid()
                };

                _dbContextMock.Setup(db => db.PQR.Add(It.IsAny<PqrEntity>())).Verifiable();
                _dbContextMock.Setup(db => db.SaveChangesAsync(default)).Returns(Task.FromResult(0));

                // Act
                var result = await _controller.CreatePQR(modelo);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
                var statusCodeResult = (StatusCodeResult)result;
                Assert.AreEqual(StatusCodes.Status201Created, statusCodeResult.StatusCode);

                _dbContextMock.Verify(db => db.PQR.Add(It.IsAny<PqrEntity>()), Times.Once);
                _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
            }

            [TestMethod]
            public async Task CreatePQR_InvalidModel_ReturnsBadRequest()
            {
                // Arrange
                var modelo = new PqrViewModel();

                // Act
                var result = await _controller.CreatePQR(modelo);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
                var badRequestResult = (BadRequestObjectResult)result;
                Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            }

        }
    
}
