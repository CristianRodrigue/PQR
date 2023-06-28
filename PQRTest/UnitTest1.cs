using ApiProcolombiaPQR.API.Controllers;
using ApiProcolombiaPQR.DATA;
using ApiProcolombiaPQR.ENTITY;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PQRTest
{
    [TestClass]
    public class UnitTest1
    {
        private PqrController _controller;
        private Mock<DataContextDB> _dbContextMock;
        private IEnumerable<PqrEntity> queryLinq;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContextDB>()
                .UseInMemoryDatabase(databaseName: "ProcolombiaDB")
                .Options;

            _dbContextMock = new Mock<DataContextDB>();
            _dbContextMock.Setup(db => db.PQR).Returns(MockDbSet(queryLinq));

            _controller = new PqrController(_dbContextMock.Object);
        }

        [TestMethod]
        public async Task GetAll_Should_Return_Success_Response_With_Data()
        {
            // Arrange
            var expectedResult = new
            {
                success = true,
                data = new List<object>()
            {
                // Define your expected data here
                new { RazonSocial = "Country 1", Name = "CaseType 1" },
                
                // Add more objects as needed
            }
            };

            var queryLinq = new List<ApiProcolombiaPQR.ENTITY.PqrEntity>()
{
    new ApiProcolombiaPQR.ENTITY.PqrEntity {RazonSocial = "Country 1", Name = "CaseType 1" },
    
};

            _dbContextMock.Setup(db => db.PQR).Returns(MockDbSet(queryLinq));


            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as dynamic;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedResult.success, response.success);
            Assert.AreEqual(expectedResult.data.Count, (response.data as List<object>).Count);


        }

        [TestMethod]
        public async Task GetAll_Should_Return_BadRequest_Response_When_Exception_Occurs()
        {
            // Arrange
            var expectedErrorMessage = "An error occurred.";

            // Setup the mock DbContext to throw an exception
            _dbContextMock.Setup(db => db.PQR).Throws(new Exception(expectedErrorMessage));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var response = badRequestResult.Value as dynamic;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.success);
            Assert.AreEqual(expectedErrorMessage, response.error);
        }

        private DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return dbSetMock.Object;
        }
    }
}