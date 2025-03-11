
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHR.Domain.Entities.habitacion;
using SGHR.Persistence.Context;
using SGHR.Persistence.Repositories.habitacion;

namespace SGHR.Persistence.Test.habitacion
{
    public class CategoriaRepositoryTests
    {
        private readonly Mock<SGHRContext> _mockContext;
        private readonly Mock<ILogger<CategoriaRepository>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaRepositoryTests()
        {
            _mockContext = new Mock<SGHRContext>();
            _mockLogger = new Mock<ILogger<CategoriaRepository>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _categoriaRepository = new CategoriaRepository(_mockContext.Object, _mockLogger.Object, _mockConfiguration.Object);
        }

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
    }
}
