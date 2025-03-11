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

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEstadoParamethersIsNull()
        {
            // Arrange

            // Act

            // Assert
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityDontExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEstadoParamethersIsNull()
        {
            // Arrange

            // Act

            // Assert
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange

            // Act

            // Assert
        }
        #endregion

        #region "GetEntityByIdAsync"
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNegative()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExceedsTheRange()
        {
            // Arrange

            // Act

            // Assert
        }
        #endregion
    }
}
