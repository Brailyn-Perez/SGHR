using Microsoft.AspNetCore.Mvc;
using Moq;
using SGHR.Api.Controllers.habitacion;
using SGHR.Application.DTos.habitacion.Habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Domain.Base;

namespace SGHR.Api.Test.habitacion
{
    public class HabitacionControllerTest
    {
        private readonly Mock<IHabitacionService> _serviceMock;
        private readonly HabitacionController _controller;

        public HabitacionControllerTest()
        {
            _serviceMock = new Mock<IHabitacionService>();
            _controller = new HabitacionController(_serviceMock.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult()
        {
            // Arrange
            _serviceMock.Setup(service => service.GeAll()).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            _serviceMock.Setup(service => service.GeById(id)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsOkResult()
        {
            // Arrange
            var dto = new SaveHabitacionDTO { Numero = "101", Detalle = "Detalle", Precio = 100.00m };
            _serviceMock.Setup(service => service.Save(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Post(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var dto = new SaveHabitacionDTO { Numero = "", Detalle = "Detalle", Precio = 100.00m };
            _controller.ModelState.AddModelError("Numero", "Required");

            // Act
            var result = await _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOkResult()
        {
            // Arrange
            var dto = new UpdateHabitacionDTO { IdHabitacion = 1, Numero = "101", Detalle = "Detalle", Precio = 100.00m };
            _serviceMock.Setup(service => service.Update(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Put(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Put_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var dto = new UpdateHabitacionDTO { IdHabitacion = 1, Numero = "", Detalle = "Detalle", Precio = 100.00m };
            _controller.ModelState.AddModelError("Numero", "Required");

            // Act
            var result = await _controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var dto = new RemoveHabitacionDTO { IdHabitacion = 1 };
            _serviceMock.Setup(service => service.Remove(dto)).ReturnsAsync(new OperationResult());

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<OperationResult>(okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var dto = new RemoveHabitacionDTO { IdHabitacion = 0 };
            _controller.ModelState.AddModelError("IdHabitacion", "Required");

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
