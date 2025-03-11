using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class UnitTestTarifaRepository : BaseTest<TarifaRepository>
    {
        private readonly TarifaRepository _repository;

        public UnitTestTarifaRepository()
        {
            _repository = new TarifaRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
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
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenEntityAlreadyExists()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnFailure_WhenTarifaParametersAreNull()
        {
            // Arrange

            // Act

            // Assert
        }

        #endregion

        #region "UpdateEntityAsync"

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdated()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenEntityDoesNotExist()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnFailure_WhenTarifaParametersAreNull()
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
        public async Task GetEntityByIdAsync_ShouldReturnFailure_WhenIdIsNull()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnFailure_WhenIdIsNegative()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnFailure_WhenIdExceedsTheRange()
        {
            // Arrange

            // Act

            // Assert
        }

        #endregion
    }
}
