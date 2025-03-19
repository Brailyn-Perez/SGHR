using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.habitacion.EstadoHabitacion;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;
using Xunit;

namespace SGHR.Tests.Application.Service.habitacion
{
    public class EstadoHabitacionServiceTests
    {
        private readonly Mock<IEstadoHabitacionRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<EstadoHabitacionService>> _loggerMock;
        private readonly EstadoHabitacionService _service;

        public EstadoHabitacionServiceTests()
        {
            _repositoryMock = new Mock<IEstadoHabitacionRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<EstadoHabitacionService>>();
            _service = new EstadoHabitacionService(_repositoryMock.Object, _loggerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task GeAll_ShouldReturnSuccessResult_WhenEstadosExist()
        {
            // Arrange
            var estados = new List<EstadoHabitacion>
            {
                new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado 1", Estado = true },
                new EstadoHabitacion { IdEstadoHabitacion = 2, Descripcion = "Estado 2", Estado = false }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(estados);

            // Act
            var result = await _service.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((IEnumerable<EstadoHabitacionDTO>)result.Data).Count());
        }

        [Fact]
        public async Task GeById_ShouldReturnSuccessResult_WhenEstadoExists()
        {
            // Arrange
            var estado = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(estado);

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, ((EstadoHabitacionDTO)result.Data).IdEstadoHabitacion);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailureResult_WhenEstadoDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync((EstadoHabitacion)null);
            _configurationMock.Setup(config => config["EstadoHabitacionNotFound"]).Returns("Estado not found");

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Estado not found", result.Message);
        }

        [Fact]
        public async Task Remove_ShouldReturnSuccessResult_WhenEstadoIsRemoved()
        {
            // Arrange
            var estado = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(estado);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(estado)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(new RemoveEstadoHabitacionDTO { IdEstadoHabitacion = 1 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResult_WhenEstadoIsSaved()
        {
            // Arrange
            var dto = new SaveEstadoHabitacionDTO { Descripcion = "Estado 1", Estado = true };
            _repositoryMock.Setup(repo => repo.SaveEntityAsync(It.IsAny<EstadoHabitacion>())).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessResult_WhenEstadoIsUpdated()
        {
            // Arrange
            var estado = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado 1", Estado = true };
            var dto = new UpdateEstadoHabitacionDTO { IdEstadoHabitacion = 1, Descripcion = "Updated Estado", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(estado);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(estado)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
