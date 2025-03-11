using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class UnitTestTarifaRepository : BaseTest<TarifaRepository>
    {
        private readonly TarifaRepository _tarifaRepository;

        public UnitTestTarifaRepository()
        {
            _tarifaRepository = new TarifaRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntity"
        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var tarifa = new Tarifa
            {
                IdTarifa = 1,
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(5),
                PrecioPorNoche = 100.00m,
                Descuento = 0.15m,
                Descripcion = "Tarifa estándar 2",
                Estado = true
            };

            // Act
            var result = await _tarifaRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _tarifaRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityWithSameIdExists()
        {
            // Arrange
            var tarifa = new Tarifa
            {
                IdTarifa = 1,
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(5),
                PrecioPorNoche = 100.00m,
                Descuento = 0.15m,
                Descripcion = "Tarifa estándar",
                Estado = true
            };
            await _tarifaRepository.SaveEntityAsync(tarifa);

            // Act
            var result = await _tarifaRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var tarifa = new Tarifa();

            // Act
            var result = await _tarifaRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange
            var tarifa = new Tarifa
            {
                IdTarifa = 1,
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(5),
                PrecioPorNoche = 100.00m,
                Descuento = 0.15m,
                Descripcion = "Tarifa estándar",
                Estado = true
            };
            await _tarifaRepository.SaveEntityAsync(tarifa);
            tarifa.Descripcion = "Tarifa actualizada";

            // Act
            var result = await _tarifaRepository.UpdateEntityAsync(tarifa);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Act
            var result = await _tarifaRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange
            var tarifa = new Tarifa { IdTarifa = 99, Descripcion = "Tarifa inexistente" };

            // Act
            var result = await _tarifaRepository.UpdateEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenParametrosSonNull()
        {
            // Arrange
            var tarifa = new Tarifa();

            // Act
            var result = await _tarifaRepository.UpdateEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            await _tarifaRepository.SaveEntityAsync(new Tarifa
            {
                IdTarifa = 1,
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(5),
                PrecioPorNoche = 100.00m,
                Descuento = 0.15m,
                Descripcion = "Tarifa estándar",
                Estado = true
            });

            // Act
            var result = await _tarifaRepository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
        }
        #endregion

        #region "GetEntityByIdAsync"

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdIsNegative()
        {
            // Act
            var result = await _tarifaRepository.GetEntityByIdAsync(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Act
            var result = await _tarifaRepository.GetEntityByIdAsync(9999);

            // Assert
            Assert.Null(result);
        }
        #endregion
    }
}
