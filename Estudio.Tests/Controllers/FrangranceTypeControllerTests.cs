using Estudio.API.Controllers;
using Estudio.Application.Interface;
using Estudio.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Estudio.Tests.Controllers
{
    public class FrangranceTypeControllerTests
    {
        //simula un comportamiento de servicio real, sin usar base de datos ni logica
        private readonly Mock<IFragranceTypeService> _serviceMock;
        private readonly FragranceTypeController _controller;

        public FrangranceTypeControllerTests()
        {
            _serviceMock = new Mock<IFragranceTypeService>();
            _controller = new FragranceTypeController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkWithFragranceTypes()
        {
            // Arrange
            //lista simulada de tipos de fragancia
            var fragranceTypes = new List<FragranceTypeDto>
            {
                new FragranceTypeDto { Id = 1, Name = "type A" },
                new FragranceTypeDto { Id = 2, Name = "type B" }
            };

            //configura el mock para que, al llamar GetAllAsync, retorne esa lista
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(fragranceTypes);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<FragranceTypeDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_IfNull()
        {
            // Arrange
            _serviceMock.Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                        .ReturnsAsync((FragranceTypeDto?)null);

            // Act
            var result = await _controller.GetById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction()
        {
            // Arrange
            var dto = new FragranceTypeDto { Name = "Nueva fragrance type" };
            var created = new FragranceTypeDto { Id = 1, Name = "Nueva fragrance type" };

            _serviceMock.Setup(s => s.CreateWithValidationAsync(dto))
                        .ReturnsAsync(created);

            // Act
            var result = await _controller.Create(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<FragranceTypeDto>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }
    }
}
