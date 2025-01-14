using BusinessLogic.Product.Interface;
using BusinessLogic.Product.Service;
using DataClasses.Core;
using Moq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<IProductCache> _cacheMock;
        private readonly ProductService _service;
        private readonly Mock<HttpClient> _httpClientMock;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _cacheMock = new Mock<IProductCache>();
            _httpClientMock = new Mock<HttpClient>();
            _service = new ProductService(_httpClientMock.Object, _repositoryMock.Object, _cacheMock.Object);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsCorrectProductResponse()
        {
            // Arrange
            var productId = 1;
            var product = new ProductCore
            {
                ProductId = productId,
                Name = "Test Product",
                Status = 1,
                Stock = 10,
                Description = "Test Description",
                Price = 100
            };
            _repositoryMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);
            _cacheMock.Setup(c => c.GetStatusName(1)).Returns("Active");

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Product 2", result.Name);
            Assert.AreEqual("Active", result.StatusName);
        }

    }
}
