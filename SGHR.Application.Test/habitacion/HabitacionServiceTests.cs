using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.habitacion.Habitacion;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;
using Xunit;

namespace SGHR.Tests.Application.Service.habitacion
{
    public class HabitacionServiceTests
    {
        private readonly Mock<IHabitacionRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<HabitacionService>> _loggerMock;
        private readonly HabitacionService _service;

        public HabitacionServiceTests()
        {
            _repositoryMock = new Mock<IHabitacionRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<HabitacionService>>();
            _service = new HabitacionService(_repositoryMock.Object, _loggerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task GeAll_ShouldReturnSuccessResult_WhenHabitacionesExist()
        {
            // Arrange
            var habitaciones = new List<Habitacion>
            {
                new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Detalle 1", Precio = 100, Estado = true },
                new Habitacion { IdHabitacion = 2, Numero = "102", Detalle = "Detalle 2", Precio = 200, Estado = false }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(habitaciones);

            // Act
            var result = await _service.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((IEnumerable<HabitacionDTO>)result.Data).Count());
        }

        [Fact]
        public async Task GeById_ShouldReturnSuccessResult_WhenHabitacionExists()
        {
            // Arrange
            var habitacion = new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Detalle 1", Precio = 100, Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(habitacion);

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, ((HabitacionDTO)result.Data).IdHabitacion);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailureResult_WhenHabitacionDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync((Habitacion)null);
            _configurationMock.Setup(config => config["HabitacionNotFound"]).Returns("Habitacion not found");

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Habitacion not found", result.Message);
        }

        [Fact]
        public async Task Remove_ShouldReturnSuccessResult_WhenHabitacionIsRemoved()
        {
            // Arrange
            var habitacion = new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Detalle 1", Precio = 100, Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(habitacion);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(habitacion)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(new RemoveHabitacionDTO { IdHabitacion = 1 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResult_WhenHabitacionIsSaved()
        {
            // Arrange
            var dto = new SaveHabitacionDTO { Numero = "101", Detalle = "Detalle 1", Precio = 100, Estado = true };
            _repositoryMock.Setup(repo => repo.SaveEntityAsync(It.IsAny<Habitacion>())).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessResult_WhenHabitacionIsUpdated()
        {
            // Arrange
            var habitacion = new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Detalle 1", Precio = 100, Estado = true };
            var dto = new UpdateHabitacionDTO { IdHabitacion = 1, Numero = "102", Detalle = "Updated Detalle", Precio = 150, Estado = false };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(habitacion);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(habitacion)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
