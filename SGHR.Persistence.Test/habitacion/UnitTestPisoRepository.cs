using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class UnitTestPisoRepository : BaseTest<PisoRepository>
    {
        private readonly PisoRepository _pisoRepository;

        public UnitTestPisoRepository()
        {
            _pisoRepository = new PisoRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntity"
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var piso = new Piso
            {
                IdPiso = 1,
                Descripcion = "Piso 2",
                Estado = true
            };

            // Act
            var result = await _pisoRepository.SaveEntityAsync(piso);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _pisoRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityAlreadyExists()
        {
            // Arrange
            var piso = new Piso
            {
                IdPiso = 1,
                Descripcion = "Piso 1",
                Estado = true
            };
            await _pisoRepository.SaveEntityAsync(piso);

            // Act
            var result = await _pisoRepository.SaveEntityAsync(piso);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var piso = new Piso();

            // Act
            var result = await _pisoRepository.SaveEntityAsync(piso);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Piso 1", Estado = true , Borrado = false, FechaActualizacion = DateTime.UtcNow};
            await _pisoRepository.SaveEntityAsync(piso);

            piso.Descripcion = "Piso Renovado";

            // Act
            var result = await _pisoRepository.UpdateEntityAsync(piso);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _pisoRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var piso = new Piso { IdPiso = 99, Descripcion = "Piso No Existente", Estado = true };

            // Act
            var result = await _pisoRepository.UpdateEntityAsync(piso);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var piso = new Piso();

            // Act
            var result = await _pisoRepository.UpdateEntityAsync(piso);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            await _pisoRepository.SaveEntityAsync(new Piso { IdPiso = 1, Descripcion = "Piso 2", Estado = true , Borrado = false});

            // Act
            var result = await _pisoRepository.GetAllAsync(pi => pi.Borrado == false);

            // Assert
            Assert.NotEmpty(result.Data);
        }
        #endregion

        #region "GetEntityByIdAsync"

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdIsNegative()
        {
            // Act
            var result = await _pisoRepository.GetEntityByIdAsync(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Act
            var result = await _pisoRepository.GetEntityByIdAsync(9999);

            // Assert
            Assert.Null(result);
        }
        #endregion
    }
}
