using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Application.DTos.habitacion.Categoria;
using SGHR.Application.Service.habitacion;
using SGHR.Domain.Base;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Interfaces.habitacion;


namespace SGHR.Tests.Application.Service.habitacion
{
    public class CategoriaServiceTests
    {
        private readonly Mock<ICategoriaRepository> _repositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<ILogger<CategoriaService>> _loggerMock;
        private readonly CategoriaService _service;

        public CategoriaServiceTests()
        {
            _repositoryMock = new Mock<ICategoriaRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<CategoriaService>>();
            _service = new CategoriaService(_repositoryMock.Object, _configurationMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GeAll_ShouldReturnSuccessResult_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<Categoria>
            {
                new Categoria { IdCategoria = 1, Descripcion = "Category 1", Estado = true },
                new Categoria { IdCategoria = 2, Descripcion = "Category 2", Estado = false }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _service.GeAll();

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(2, ((IEnumerable<CategoriaDTO>)result.Data).Count());
        }

        [Fact]
        public async Task GeById_ShouldReturnSuccessResult_WhenCategoryExists()
        {
            // Arrange
            var category = new Categoria { IdCategoria = 1, Descripcion = "Category 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
            Assert.Equal(1, ((CategoriaDTO)result.Data).IdCategoria);
        }

        [Fact]
        public async Task GeById_ShouldReturnFailureResult_WhenCategoryDoesNotExist()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync((Categoria)null);
            _configurationMock.Setup(config => config["CategoryNotFound"]).Returns("Category not found");

            // Act
            var result = await _service.GeById(1);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Category not found", result.Message);
        }

        [Fact]
        public async Task Remove_ShouldReturnSuccessResult_WhenCategoryIsRemoved()
        {
            // Arrange
            var category = new Categoria { IdCategoria = 1, Descripcion = "Category 1", Estado = true };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(category);
            _repositoryMock.Setup(repo => repo.GetHabitacionByCategoriaId(1)).ReturnsAsync(new OperationResult { Success = false });
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(category)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Remove(new RemoveCategoriaDTO { IdCategoria = 1 });

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Save_ShouldReturnSuccessResult_WhenCategoryIsSaved()
        {
            // Arrange
            var dto = new SaveCategoriaDTO { IdServicio = 1, Descripcion = "Category 1", Estado = true };
            _repositoryMock.Setup(repo => repo.ServicioExiste(1)).ReturnsAsync(true);
            _repositoryMock.Setup(repo => repo.SaveEntityAsync(It.IsAny<Categoria>())).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Save(dto);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Update_ShouldReturnSuccessResult_WhenCategoryIsUpdated()
        {
            // Arrange
            var category = new Categoria { IdCategoria = 1, Descripcion = "Category 1", Estado = true };
            var dto = new UpdateCategoriaDTO { IdCategoria = 1, Descripcion = "Updated Category", Estado = true, IdServicio = 1 };
            _repositoryMock.Setup(repo => repo.GetEntityByIdAsync(1)).ReturnsAsync(category);
            _repositoryMock.Setup(repo => repo.UpdateEntityAsync(category)).ReturnsAsync(new OperationResult { Success = true });

            // Act
            var result = await _service.Update(dto);

            // Assert
            Assert.True(result.Success);
        }
    }
}
