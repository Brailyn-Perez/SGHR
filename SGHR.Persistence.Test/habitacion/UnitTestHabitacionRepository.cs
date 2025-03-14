using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class UnitTestHabitacionRepository : BaseTest<HabitacionRepository>
    {
        private readonly HabitacionRepository _habitacionRepository;

        public UnitTestHabitacionRepository()
        {
            _habitacionRepository = new HabitacionRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntity"
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var habitacion = new Habitacion()
            {
                Numero = "1011",
                Detalle = "Habitación estándar",
                Precio = 150.00m,
                IdEstadoHabitacion = 1,
                IdPiso = 1,
                IdCategoria = 1,
                Estado = true
            };

            // Act
            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _habitacionRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityAlreadyExists()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "101",
                Detalle = "Habitación estándar",
                Precio = 150.00m,
                IdEstadoHabitacion = 1,
                IdPiso = 1,
                IdCategoria = 1,
                Estado = true
            };
            await _habitacionRepository.SaveEntityAsync(habitacion);

            // Act
            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var habitacion = new Habitacion();

            // Act
            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var habitacion = new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Habitación estándar", Precio = 150.00m };
            await _habitacionRepository.SaveEntityAsync(habitacion);
            habitacion.Detalle = "Habitación renovada";

            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(habitacion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var habitacion = new Habitacion { IdHabitacion = 99, Numero = "999", Detalle = "No existente", Precio = 200.00m };

            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var habitacion = new Habitacion();

            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            await _habitacionRepository.SaveEntityAsync(new Habitacion { IdHabitacion = 1, Numero = "101", Detalle = "Habitación estándar", Precio = 150.00m });

            // Act
            var result = await _habitacionRepository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
        }
        #endregion

        #region "GetEntityByIdAsync"

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdIsNegative()
        {
            // Act
            var result = await _habitacionRepository.GetEntityByIdAsync(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Act
            var result = await _habitacionRepository.GetEntityByIdAsync(9999);

            // Assert
            Assert.Null(result);
        }
        #endregion
    }
}
