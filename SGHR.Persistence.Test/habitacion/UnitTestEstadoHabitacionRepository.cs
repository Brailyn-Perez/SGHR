using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class UnitTestEstadoHabitacionRepository : BaseTest<EstadoHabitacionRepository>
    {
        private readonly EstadoHabitacionRepository _estadoHabitacionRepository;

        public UnitTestEstadoHabitacionRepository()
        {
            _estadoHabitacionRepository = new EstadoHabitacionRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntity"
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 1,
                Descripcion = "Estado activo 2",
                Estado = true
            };

            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityExist()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado activo", Estado = true };
            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEstadoParamethersIsNull()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion();

            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Estado activo", Estado = true };
            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);
            estadoHabitacion.Descripcion = "Estado actualizado";

            // Act
            var result = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Act
            var result = await _estadoHabitacionRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityDontExist()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion { IdEstadoHabitacion = 99, Descripcion = "No existente", Estado = false };

            // Act
            var result = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEstadoParamethersIsNull()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion();

            // Act
            var result = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 1,
                Descripcion = "Disponible",
                Estado = true,
                Borrado = false,
            };

            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Act
            var result = await _estadoHabitacionRepository.GetAllAsync(es => es.Borrado == false);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        #endregion

        #region "GetEntityByIdAsync"

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNegative()
        {
            // Act
            var result = await _estadoHabitacionRepository.GetEntityByIdAsync(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExceedsTheRange()
        {
            // Act
            var result = await _estadoHabitacionRepository.GetEntityByIdAsync(9999);

            // Assert
            Assert.Null(result);
        }
        #endregion
    }
}
