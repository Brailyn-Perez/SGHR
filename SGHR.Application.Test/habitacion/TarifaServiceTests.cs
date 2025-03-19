using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.habitacion.Tarifa;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;
using Xunit;

namespace SGHR.Tests.Application.Service.habitacion
{
    public class TarifaServiceTests
    {
        private readonly Mock<ITarifaRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<TarifaService>> _loggerMock;
        private readonly TarifaService _service;

        public TarifaServiceTests()
        {
            _repositoryMock = new Mock<ITarifaRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<TarifaService>>();
            _service = new TarifaService(_repositoryMock.Object, _loggerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task GeAll_ShouldReturnSuccessResult_WhenTarifasExist()
        {
            // Arrange
            var tarifas = new List<Tarifa>
            {
                new Tarifa { IdTarifa = 1, Descripcion = "Tarifa 1", Estado = true },
                new Tarifa { IdTarifa = 2, Descripcion = "Tarifa 2", Estado = false }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tarifas);

            // Act
            var result = await _service.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((IEnumerable<TarifaDTO>)result.Data).Count());
        }

        [Fact]
        public async Task GeById_ShouldReturnSuccessResult_WhenTarifaExists()
        {
            // Arrange
            var tarifa = new Tarifa { IdTarifa = 1, Descripcion = "Tarifa 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(tarifa);

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, ((TarifaDTO)result.Data).IdTarifa);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailureResult_WhenTarifaDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync((Tarifa)null);
            _configurationMock.Setup(config => config["TarifaNotFound"]).Returns("Tarifa not found");

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Tarifa not found", result.Message);
        }

        [Fact]
        public async Task Remove_ShouldReturnSuccessResult_WhenTarifaIsRemoved()
        {
            // Arrange
            var tarifa = new Tarifa { IdTarifa = 1, Descripcion = "Tarifa 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(tarifa);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(tarifa)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(new RemoveTarifaDTO { IdTarifa = 1 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResult_WhenTarifaIsSaved()
        {
            // Arrange
            var dto = new SaveTarifaDTO { Descripcion = "Tarifa 1", Estado = true };
            _repositoryMock.Setup(repo => repo.SaveEntityAsync(It.IsAny<Tarifa>())).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessResult_WhenTarifaIsUpdated()
        {
            // Arrange
            var tarifa = new Tarifa { IdTarifa = 1, Descripcion = "Tarifa 1", Estado = true };
            var dto = new UpdateTarifaDTO { IdTarifa = 1, Descripcion = "Updated Tarifa", Estado = false };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(tarifa);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(tarifa)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
