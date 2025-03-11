using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class CategoriaRepositoryTests : BaseTest
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            _categoriaRepository = new CategoriaRepository(_context, _mockLogger.Object, _mockConfiguration.Object);
        }

        #region "SaveEntity"

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsSaved()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1 };

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {

        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityExist()
        {

        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenCategoriaParamethersIsNull()
        {

        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
        {
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {

        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityDontExist()
        {

        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenCategoriaParamethersIsNull()
        {

        }
        #endregion

        #region "GetAllAsync"
        #endregion

        #region "GetEntityByIdAsync"
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNull()
        {

        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNegative()
        {

        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExceedsTheRange()
        {

        }
        #endregion

        #region "GetHabitacionByCategoriaId"
        #endregion
    }
}
