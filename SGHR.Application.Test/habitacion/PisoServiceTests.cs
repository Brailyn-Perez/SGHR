using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.habitacion.Piso;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;
using Xunit;

namespace SGHR.Tests.Application.Service.habitacion
{
    public class PisoServiceTests
    {
        private readonly Mock<IPisoRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<PisoService>> _loggerMock;
        private readonly PisoService _service;

        public PisoServiceTests()
        {
            _repositoryMock = new Mock<IPisoRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<PisoService>>();
            _service = new PisoService(_repositoryMock.Object, _loggerMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task GeAll_ShouldReturnSuccessResult_WhenPisosExist()
        {
            // Arrange
            var pisos = new List<Piso>
            {
                new Piso { IdPiso = 1, Descripcion = "Piso 1", Estado = true },
                new Piso { IdPiso = 2, Descripcion = "Piso 2", Estado = false }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pisos);

            // Act
            var result = await _service.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((IEnumerable<PisoDTO>)result.Data).Count());
        }

        [Fact]
        public async Task GeById_ShouldReturnSuccessResult_WhenPisoExists()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Piso 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(piso);

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, ((PisoDTO)result.Data).IdPiso);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailureResult_WhenPisoDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync((Piso)null);
            _configurationMock.Setup(config => config["PisoNotFound"]).Returns("Piso not found");

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Piso not found", result.Message);
        }

        [Fact]
        public async Task Remove_ShouldReturnSuccessResult_WhenPisoIsRemoved()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Piso 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(piso);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(piso)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(new RemovePisoDTO { IdPiso = 1 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResult_WhenPisoIsSaved()
        {
            // Arrange
            var dto = new SavePisoDTO { Descripcion = "Piso 1", Estado = true };
            _repositoryMock.Setup(repo => repo.SaveEntityAsync(It.IsAny<Piso>())).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessResult_WhenPisoIsUpdated()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Piso 1", Estado = true };
            var dto = new UpdatePisoDTO { IdPiso = 1, Descripcion = "Updated Piso", Estado = false };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(piso);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(piso)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
