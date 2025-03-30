using Moq;
using Microsoft.AspNetCore.Mvc;
using SGHR.Api.Controllers.habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Domain.Base;

namespace SGHR.Tests.Controllers.habitacion
{
    public class CategoriaControllerTests
    {
        private readonly Mock<ICategoriaService> _mockService;
        private readonly CategoriaController _controller;

        public CategoriaControllerTests()
        {
            _mockService = new Mock<ICategoriaService>();
            _controller = new CategoriaController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfCategorias()
        {
            // Arrange
            _mockService.Setup(service => service.GeAll()).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(((OperationResult)okResult.Value).Success);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithCategoria()
        {
            // Arrange
            int testId = 1;
            _mockService.Setup(service => service.GeById(testId)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _controller.Get(testId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(((OperationResult)okResult.Value).Success);
        }

        [Fact]
        public async Task Post_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var dto = new SaveCategoriaDTO { Descripcion = "Test Description", IdServicio = 1 };
            _mockService.Setup(service => service.Save(dto)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _controller.Post(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(((OperationResult)okResult.Value).Success);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var dto = new SaveCategoriaDTO { Descripcion = "Test" }; // Invalid DTO
            _controller.ModelState.AddModelError("IdServicio", "Required");

            // Act
            var result = await _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var dto = new UpdateCategoriaDTO { IdCategoria = 1, Descripcion = "Updated Description", IdServicio = 1 };
            _mockService.Setup(service => service.Update(dto)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _controller.Put(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(((OperationResult)okResult.Value).Success);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var dto = new UpdateCategoriaDTO { IdCategoria = 1, Descripcion = "Updated" }; // Invalid DTO
            _controller.ModelState.AddModelError("IdServicio", "Required");

            // Act
            var result = await _controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var dto = new RemoveCategoriaDTO { IdCategoria = 1 };
            _mockService.Setup(service => service.Remove(dto)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True(((OperationResult)okResult.Value).Success);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            var dto = new RemoveCategoriaDTO(); // Invalid DTO
            _controller.ModelState.AddModelError("IdCategoria", "Required");

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
