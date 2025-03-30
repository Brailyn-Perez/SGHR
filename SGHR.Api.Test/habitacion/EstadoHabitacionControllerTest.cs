using Moq;
using Microsoft.AspNetCore.Mvc;
using SGHR.Api.Controllers.habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Domain.Base;

namespace SGHR.Api.Test.habitacion
{
    public class EstadoHabitacionControllerTest
    {
        private readonly Mock<IEstadoHabitacionService> _mockService;
        private readonly EstadoHabitacionController _controller;

        public EstadoHabitacionControllerTest()
        {
            _mockService = new Mock<IEstadoHabitacionService>();
            _controller = new EstadoHabitacionController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfEstadoHabitacion()
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
        public async Task GetById_ReturnsOkResult_WithEstadoHabitacion()
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
            var dto = new SaveEstadoHabitacionDTO { Descripcion = "Valid Description", Estado = true };
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
            var dto = new SaveEstadoHabitacionDTO { Descripcion = "Short", Estado = true };
            _controller.ModelState.AddModelError("Descripcion", "Description is too short");

            // Act
            var result = await _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var dto = new UpdateEstadoHabitacionDTO { IdEstadoHabitacion = 1, Descripcion = "Valid Description", Estado = true };
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
            var dto = new UpdateEstadoHabitacionDTO { IdEstadoHabitacion = 1, Descripcion = "Short", Estado = true };
            _controller.ModelState.AddModelError("Descripcion", "Description is too short");

            // Act
            var result = await _controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenModelIsValid()
        {
            // Arrange
            var dto = new RemoveEstadoHabitacionDTO { IdEstadoHabitacion = 1 };
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
            var dto = new RemoveEstadoHabitacionDTO { IdEstadoHabitacion = 0 };
            _controller.ModelState.AddModelError("IdEstadoHabitacion", "Id must be greater than 0");

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
