using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Repositories.habitacion;
using SGHR.Persistence.Test.habitacion.Base;

namespace SGHR.Persistence.Test.habitacion
{
    public class CategoriaRepositoryTests : BaseTest<CategoriaRepository>
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
            var categoria = new Categoria
            {
                IdCategoria = 1,
                Descripcion = "description2",
                Borrado = false
            };

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Act
            var result = await _categoriaRepository.SaveEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenEntityExist()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1, Descripcion = "Existing", Borrado = false };
            await _categoriaRepository.SaveEntityAsync(categoria);

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveEntityAsync_ShouldReturnSuccess_WhenCategoriaParamethersIsNull()
        {
            // Arrange
            var categoria = new Categoria();

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "UpdateEntityAsync"
        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsUpdate()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1, Descripcion = "Updated", Borrado = false };
            await _categoriaRepository.SaveEntityAsync(categoria);
            categoria.Descripcion = "New Description";

            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(categoria);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityIsNull()
        {
            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenEntityDontExist()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 99, Descripcion = "Non-existing", Borrado = false };

            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateEntityAsync_ShouldReturnSuccess_WhenCategoriaParamethersIsNull()
        {
            // Arrange
            var categoria = new Categoria();

            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
        }
        #endregion

        #region "GetAllAsync"
        [Fact]
        public async Task GetAllAsync_ShouldReturnSuccess_WhenDataExists()
        {
            // Arrange
            await _categoriaRepository.SaveEntityAsync(new Categoria { IdCategoria = 1, Descripcion = "Category 1", Borrado = false });

            // Act
            var result = await _categoriaRepository.GetAllAsync();

            // Assert
            Assert.NotEmpty(result);
        }
        #endregion

        #region "GetEntityByIdAsync"
        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNull()
        {
            //Arrange


            // Act
   

            // Assert

        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdIsNegative()
        {
            // Act
            var result = await _categoriaRepository.GetEntityByIdAsync(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntityByIdAsync_ShouldReturnSuccess_WhenIdExceedsTheRange()
        {
            // Act
            var result = await _categoriaRepository.GetEntityByIdAsync(9999);

            // Assert
            Assert.Null(result);
        }
        #endregion

        #region "GetHabitacionByCategoriaId"
        [Fact]
        public async Task GetHabitacionByCategoriaId_ShouldReturnSuccess_WhenCategoriaExists()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1, Descripcion = "Category 1", Borrado = false };
            await _categoriaRepository.SaveEntityAsync(categoria);

            // Act
            var result = await _categoriaRepository.GetHabitacionByCategoriaId(1);

            // Assert
            Assert.NotNull(result);
        }
        #endregion
    }
}
