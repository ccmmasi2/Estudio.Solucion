using Estudio.API.Controllers;
using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Estudio.Tests.Controllers
{
    public class BrandControllerTests
    {
        private readonly Mock<IBrandService> _serviceMock;
        private readonly BrandController _controller;

        public BrandControllerTests()
        {
            _serviceMock = new Mock<IBrandService>();
            _controller = new BrandController(_serviceMock.Object); 
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithBrands()
        {
            // Arrange
            var brands = new List<BrandDto>
            {
                new BrandDto { Id = 1, Name = "Marca A" },
                new BrandDto { Id = 2, Name = "Marca B" }
            };

            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(brands);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<BrandDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfNull()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((BrandDto?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new BrandDto { Name = "Nueva Marca" };
            var created = new BrandDto { Id = 1, Name = "Nueva Marca" };

            _serviceMock.Setup(s => s.CreateWithValidationAsync(dto))
                        .ReturnsAsync(created);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<BrandDto>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
