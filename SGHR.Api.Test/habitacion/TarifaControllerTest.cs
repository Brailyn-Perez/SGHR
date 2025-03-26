using Moq;
using Microsoft.AspNetCore.Mvc;
using SGHR.Api.Controllers.habitacion;
using SGHR.Application.Interfaces.habitacion;
using SGHR.Application.DTos.habitacion.Tarifa;
using SGHR.Domain.Base;

namespace SGHR.Tests.Controllers.habitacion
{
    public class TarifaControllerTests
    {
        private readonly Mock<ITarifaService> _mockService;
        private readonly TarifaController _controller;

        public TarifaControllerTests()
        {
            _mockService = new Mock<ITarifaService>();
            _controller = new TarifaController(_mockService.Object);
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
        public async Task GetById_ReturnsOkResult()
        {
            // Arrange
            int id = 1;
            _mockService.Setup(service => service.GeById(id)).ReturnsAsync(new OperationResult());

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
            var dto = new SaveTarifaDTO { IdHabitacion = 1, FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(1), PrecioPorNoche = 100, Descuento = 10, Descripcion = "Test Description" };
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
            var dto = new SaveTarifaDTO();
            _controller.ModelState.AddModelError("IdHabitacion", "Required");

            // Act
            var result = await _controller.Post(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Put_ReturnsOkResult()
        {
            // Arrange
            var dto = new UpdateTarifaDTO { IdTarifa = 1, IdHabitacion = 1, FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(1), PrecioPorNoche = 100, Descuento = 10, Descripcion = "Test Description" };
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
            var dto = new UpdateTarifaDTO();
            _controller.ModelState.AddModelError("IdTarifa", "Required");

            // Act
            var result = await _controller.Put(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult()
        {
            // Arrange
            var dto = new RemoveTarifaDTO { IdTarifa = 1 };
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
            var dto = new RemoveTarifaDTO();
            _controller.ModelState.AddModelError("IdTarifa", "Required");

            // Act
            var result = await _controller.Delete(dto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
