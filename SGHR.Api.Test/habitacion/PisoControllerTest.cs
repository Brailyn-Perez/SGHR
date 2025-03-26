using Microsoft.AspNetCore.Mvc;
using Moq;
using SGHR.Api.Controllers.habitacion;
using SGHR.Application.DTos.habitacion.Piso;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;

namespace SGHR.Api.Test.habitacion
{
    public class PisoControllerTest
    {
        private readonly Mock<IPisoService> _mockService;
        private readonly PisoController _controller;

        public PisoControllerTest()
        {
            _mockService = new Mock<IPisoService>();
            _controller = new PisoController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            _mockService.Setup(service => service.GeAll()).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Get_ById_ReturnsOkResult()
        {
            // Arrange
            int testId = 1;
            _mockService.Setup(service => service.GeById(testId)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Get(testId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            // Arrange
            var dto = new SavePisoDTO { Descripcion = "Test Description", Estado = true };
            _mockService.Setup(service => service.Save(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Post(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Post_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var dto = new SavePisoDTO { Descripcion = "Test" }; // Invalid due to MinLength
            _controller.ModelState.AddModelError("Descripcion", "MinLength");

            // Act
            var result = await _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOkResult()
        {
            // Arrange
            var dto = new UpdatePisoDTO { IdPiso = 1, Descripcion = "Updated Description", Estado = true };
            _mockService.Setup(service => service.Update(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Put(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Put_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var dto = new UpdatePisoDTO { IdPiso = 1, Descripcion = "Up" }; // Invalid due to MinLength
            _controller.ModelState.AddModelError("Descripcion", "MinLength");

            // Act
            var result = await _controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var dto = new RemovePisoDTO { IdPiso = 1 };
            _mockService.Setup(service => service.Remove(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Delete_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var dto = new RemovePisoDTO { IdPiso = 0 }; // Invalid due to Range
            _controller.ModelState.AddModelError("IdPiso", "Range");

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
